using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth_Cls.Models.ViewModel
{
    public class PatientVM
    {
        public PatientVM()
        {
            this.DiseseList=new List<int>();
        }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        [Required,DataType(DataType.Date),DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime BirthDate { get; set; }
        public int Phone { get; set; }
        public string Picture { get; set; }
        public IFormFile PictureFile { get; set; }
        public bool MaritialStatus { get; set; }
        public List<int> DiseseList { get; set; }
    }
}
