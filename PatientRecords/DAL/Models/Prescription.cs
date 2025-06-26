using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [Required]
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [Required]
        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        [Required]
        public DateTime PrescriptionDate { get; set; }

        public virtual Patient Patient { get; set; } 
        public virtual Doctor Doctor { get; set; } 
        public virtual ICollection<Medication> Medications { get; set; }

        public Prescription()
        {
            Medications = new List<Medication>(); 
        }
    }
}
