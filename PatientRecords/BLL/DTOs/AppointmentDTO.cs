using System;

namespace BLL.DTOs
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }  // Status: Scheduled, Completed, Canceled
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
    }
}
