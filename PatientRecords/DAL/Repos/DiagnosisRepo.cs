using DAL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class DiagnosisRepo : Repo, IRepo<Diagnosis, int, Diagnosis>
    {
        // Create Diagnosis
        public Diagnosis Create(Diagnosis obj)
        {
            obj.DiagnosisDate = DateTime.Now; 
            db.Diagnoses.Add(obj);
            db.SaveChanges();
            return obj;
        }

        // Delete Diagnosis
        public Diagnosis Delete(int id)
        {
            var diagnosis = db.Diagnoses.Find(id);
            if (diagnosis != null)
            {
                db.Diagnoses.Remove(diagnosis);
                db.SaveChanges();
                return diagnosis;
            }
            return null;
        }

        // Get all Diagnoses
        public List<Diagnosis> Read()
        {
            return db.Diagnoses.ToList(); 
        }

        // Get a specific Diagnosis by ID
        public Diagnosis Read(int id)
        {
            return db.Diagnoses.Find(id);  
        }

        // Update Diagnosis
        public Diagnosis Update(Diagnosis obj)
        {
            var existingDiagnosis = db.Diagnoses.Find(obj.DiagnosisID);
            if (existingDiagnosis != null)
            {
                existingDiagnosis.PatientID = obj.PatientID;  
                existingDiagnosis.DoctorID = obj.DoctorID;    
                existingDiagnosis.DiagnosisDate = obj.DiagnosisDate;  
                existingDiagnosis.DiagnosisDetails = obj.DiagnosisDetails;  

                db.SaveChanges();  
                return existingDiagnosis;
            }
            return null;
        }
    }
}
