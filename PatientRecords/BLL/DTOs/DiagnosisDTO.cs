using System;

namespace BLL.DTOs
{
    public class DiagnosisDTO
    {
        public int DiagnosisID { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string DiagnosisDetails { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
    }
}
