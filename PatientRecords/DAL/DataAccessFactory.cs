using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Patient, int, Patient> PatientData()
        {
            return new PatientRepo();
        }
        public static IRepo<Prescription, int, Prescription> PrescriptionData()
        {
            return new PrescriptionRepo();
        }
        public static IRepo<Doctor, int, Doctor> DoctorData()
        {
            return new DoctorRepo();
        }
        public static IRepo<Diagnosis, int, Diagnosis> DiagnosisData()
        {
            return new DiagnosisRepo();
        }
        public static IRepo<Appointment, int, Appointment> AppointmentData()
        {
            return new AppointmentRepo();
        }
    }
}
