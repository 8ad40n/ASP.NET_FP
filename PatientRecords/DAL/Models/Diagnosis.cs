using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisID { get; set; }

        [Required]
        public DateTime DiagnosisDate { get; set; }

        [Required]
        [StringLength(500)]
        public string DiagnosisDetails { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public int PatientID { get; set; }  // Foreign Key
        public virtual Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        [Required]
        public int DoctorID { get; set; }   // Foreign Key
        public virtual Doctor Doctor { get; set; }
    }
}
