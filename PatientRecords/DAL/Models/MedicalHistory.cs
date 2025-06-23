using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MedicalHistory
    {
        [Key]
        public int HistoryID { get; set; }

        [Required]
        [StringLength(500)]
        public string Details { get; set; }

        [Required]
        public DateTime HistoryDate { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public int PatientID { get; set; }  // Foreign Key
        public virtual Patient Patient { get; set; }
    }
}
