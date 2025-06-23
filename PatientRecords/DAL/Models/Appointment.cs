using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Reason { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; }  // Status: Scheduled, Completed, Canceled

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
