using System;
using System.Collections.Generic;

namespace BLL.DTOs
{
    public class PrescriptionDTO
    {
        public int PrescriptionID { get; set; }

        public int PatientID { get; set; }

        public int DoctorID { get; set; }

        public DateTime PrescriptionDate { get; set; }

        public List<MedicationDTO> Medications { get; set; }

        public PrescriptionDTO()
        {
            Medications = new List<MedicationDTO>();
        }
    }

    public class MedicationDTO
    {
        public int MedicationID { get; set; }

        public string MedicationName { get; set; }

        public string Dosage { get; set; }

        public string Duration { get; set; }
    }
}
