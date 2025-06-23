using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }

        [Required]
        public int PatientID { get; set; }  // Foreign Key

        [Required]
        public int DoctorID { get; set; }   // Foreign Key

        [Required]
        public DateTime PrescriptionDate { get; set; }

        public virtual ICollection<Medication> Medications { get; set; }  // Navigation Property to Medication Table

        public Prescription()
        {
            Medications = new List<Medication>();
        }
    }
}
