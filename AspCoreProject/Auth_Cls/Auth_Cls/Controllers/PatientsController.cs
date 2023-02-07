using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auth_Cls.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Auth_Cls.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Auth_Cls.Controllers
{
   [Authorize(Roles = "Admin,Executive,Jr.Executive")]
    public class PatientsController : Controller
    {
        private readonly CheckDbContext _context;
        private readonly IWebHostEnvironment _he;

        public PatientsController(CheckDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_context.Patients.Include(x => x.TestEntries).ThenInclude(b => b.Disese).ToList());
        }
        public IActionResult AddNewDisese(int? id)
        {
            ViewBag.Diseses = new SelectList(_context.Diseses.ToList(), "DiseseId", "DiseseName", id.ToString() ?? "");
            return PartialView("_AddNewDisese");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PatientVM patientVM, int[] DiseseId)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient()
                {
                    PatientName = patientVM.PatientName,
                    BirthDate = patientVM.BirthDate,
                    Phone = patientVM.Phone,
                    MaritialStatus = patientVM.MaritialStatus
                };
                //Img
                string webroot = _he.WebRootPath;
                string folder = "Images";
                string imgFileName = Path.GetFileName(patientVM.PictureFile.FileName);
                string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                using (var stream = new FileStream(fileToWrite, FileMode.Create))
                {
                    await patientVM.PictureFile.CopyToAsync(stream);
                    patient.Picture = "/" + folder + "/" + imgFileName;
                }
                foreach (var item in DiseseId)
                {
                    TestEntry testEntry = new TestEntry()
                    {
                        Patient = patient,
                        PatientId=patient.PatientId,
                        DiseseId=item
                    };
                    _context.TestEntries.Add(testEntry);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            Patient patient=_context.Patients.First(x=>x.PatientId== id);
            var diseseList=_context.TestEntries.Where(x=>x.PatientId== id).Select(x=>x.DiseseId).ToList();

            PatientVM patientVM = new PatientVM
            {
                PatientId = patient.PatientId,
                PatientName = patient.PatientName,
                BirthDate = DateTime.Now,
                Phone = patient.Phone,
                MaritialStatus = patient.MaritialStatus,
                DiseseList = diseseList,
                Picture = patient.Picture,
            };
            return View(patientVM);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(PatientVM patientVM, int[] DiseseId)
        {
            if (ModelState.IsValid)
            {
                Patient patient = new Patient()
                {
                    PatientId = patientVM.PatientId,
                    PatientName = patientVM.PatientName,
                    BirthDate = patientVM.BirthDate,
                    Phone = patientVM.Phone,
                    MaritialStatus = patientVM.MaritialStatus,
                    Picture = patientVM.Picture,
                };
                //img
                if (patientVM.PictureFile != null)
                {

                    string webroot = _he.WebRootPath;
                    string folder = "Images";
                    string imgFileName = Path.GetFileName(patientVM.PictureFile.FileName);
                    string fileToWrite = Path.Combine(webroot, folder, imgFileName);

                    using (var stream = new FileStream(fileToWrite, FileMode.Create))
                    {
                        await patientVM.PictureFile.CopyToAsync(stream);
                        patient.Picture = "/" + folder + "/" + imgFileName;
                    }
                }
                //exists diseselist
                var existsDisese = _context.TestEntries.Where(x => x.PatientId == patientVM.PatientId).ToList();
                foreach (var item in existsDisese)
                {
                    _context.TestEntries.Remove(item);
                }
                //add new diseseList
                foreach (var item in DiseseId)
                {
                    TestEntry testEntry = new TestEntry()
                    {
                       PatientId= patient.PatientId,
                       DiseseId=item
                    };
                    _context.TestEntries.Add(testEntry);
                }
                _context.Entry(patient).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
           
        }
        public async Task<IActionResult>Delete(int? id) 
        {
            Patient patient=_context.Patients.First(x=>x.PatientId== id);
            var diseseList=_context.TestEntries.Where(x=>x.PatientId==id).ToList();
            foreach (var item in diseseList)
            {
                _context.TestEntries.Remove(item);
            }
            _context.Entry(patient).State= EntityState.Deleted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}