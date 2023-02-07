using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auth_Cls.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Auth_Cls.Controllers
{
    [Authorize(Roles = "Admin,Jr.Executive")]
    public class DiseseController : Controller
    {
        private readonly CheckDbContext _context;

        public DiseseController(CheckDbContext context)
        {
            _context = context;
        }

        // GET: Disese
        public async Task<IActionResult> Index()
        {
            return View(await _context.Diseses.ToListAsync());
        }

        // GET: Disese/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disese = await _context.Diseses
                .FirstOrDefaultAsync(m => m.DiseseId == id);
            if (disese == null)
            {
                return NotFound();
            }

            return View(disese);
        }

        // GET: Disese/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Disese/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiseseId,DiseseName")] Disese disese)
        {
            if (ModelState.IsValid)
            {
                _context.Add(disese);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(disese);
        }

        // GET: Disese/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disese = await _context.Diseses.FindAsync(id);
            if (disese == null)
            {
                return NotFound();
            }
            return View(disese);
        }

        // POST: Disese/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiseseId,DiseseName")] Disese disese)
        {
            if (id != disese.DiseseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(disese);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiseseExists(disese.DiseseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(disese);
        }

        // GET: Disese/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var disese = await _context.Diseses
                .FirstOrDefaultAsync(m => m.DiseseId == id);
            if (disese == null)
            {
                return NotFound();
            }

            return View(disese);
        }

        // POST: Disese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var disese = await _context.Diseses.FindAsync(id);
            _context.Diseses.Remove(disese);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiseseExists(int id)
        {
            return _context.Diseses.Any(e => e.DiseseId == id);
        }
    }
}
