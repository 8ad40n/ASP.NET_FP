using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class PatientRepo : Repo, IRepo<Patient, int, Patient>
    {
        // Create Patient
        public Patient Create(Patient obj)
        {
            obj.DateJoined = DateTime.Now;
            db.Patients.Add(obj);
            db.SaveChanges();
            return obj;
        }


        // Delete Patient
        public Patient Delete(int id)
        {
            var patient = db.Patients.Find(id); 
            if (patient != null)
            {
                db.Patients.Remove(patient);   
                db.SaveChanges();               
                return patient;            
            }
            return null; 
        }

        // Get all Patients
        public List<Patient> Read()
        {
            return db.Patients.ToList();  
        }

        // Get a specific Patient by ID
        public Patient Read(int id)
        {
            return db.Patients.Find(id);
        }

        // Update Patient
        public Patient Update(Patient obj)
        {
            var existingPatient = db.Patients.Find(obj.PatientID);  
            if (existingPatient != null)
            {
                existingPatient.FirstName = obj.FirstName;  
                existingPatient.LastName = obj.LastName;
                existingPatient.DOB = obj.DOB;
                existingPatient.Gender = obj.Gender;
                existingPatient.ContactNumber = obj.ContactNumber;
                existingPatient.Email = obj.Email;
                existingPatient.Address = obj.Address;
                existingPatient.DateJoined = obj.DateJoined;

                db.SaveChanges();  
                return existingPatient; 
            }
            return null; 
        }
    }
}
