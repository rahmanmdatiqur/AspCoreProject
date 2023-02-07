using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth_Cls.Models
{
    public class Patient
    {
        public Patient()
        {
           this.TestEntries = new List<TestEntry>();
        }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Phone { get; set; }
        public string Picture { get; set; }
        public bool MaritialStatus { get; set; }
        //nav
        public virtual ICollection<TestEntry> TestEntries { get; set; }

    }
    public class Disese
    {
        public Disese()
        {
            this.TestEntries = new List<TestEntry>();
        }
        public int DiseseId { get; set; }
        public string DiseseName { get; set; }
        //nav
        public virtual ICollection<TestEntry> TestEntries { get; set; }
    }
    public class TestEntry
    {
        public int TestEntryId { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Disese")]
        public int DiseseId { get; set; }
        //nav
        public virtual  Patient Patient { get; set; }
        public virtual Disese Disese { get; set; }
    }
    public class CheckDbContext : DbContext
    {
        public CheckDbContext(DbContextOptions<CheckDbContext>options):base(options) 
        {
        
        }
        public DbSet<Patient>  Patients { get; set; }
        public DbSet<Disese>  Diseses { get; set; }
        public DbSet<TestEntry>  TestEntries { get; set; }
    }
}
