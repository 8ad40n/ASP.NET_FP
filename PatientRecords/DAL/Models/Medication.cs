using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Medication
    {
        [Key]
        public int MedicationID { get; set; }

        [Required]
        [StringLength(100)]
        public string MedicationName { get; set; }

        [Required]
        [StringLength(20)]
        public string Dosage { get; set; }

        [Required]
        [StringLength(50)]
        public string Duration { get; set; }

        [ForeignKey("Prescription")]
        [Required]
        public int PrescriptionID { get; set; }  // Foreign Key
        public virtual Prescription Prescription { get; set; }
    }
}
