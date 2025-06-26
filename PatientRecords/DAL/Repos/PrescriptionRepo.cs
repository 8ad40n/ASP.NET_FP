using DAL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class PrescriptionRepo : Repo, IRepo<Prescription, int, Prescription>
    {
        // Create Prescription
        public Prescription Create(Prescription obj)
        {
            obj.PrescriptionDate = DateTime.Now;
            db.Prescriptions.Add(obj);
            db.SaveChanges();
            return obj;
        }

        // Delete Prescription
        public Prescription Delete(int id)
        {
            var prescription = db.Prescriptions.Find(id);
            if (prescription != null)
            {
                db.Prescriptions.Remove(prescription);
                db.SaveChanges();
                return prescription;
            }
            return null;
        }

        // Get all Prescriptions
        public List<Prescription> Read()
        {
            return db.Prescriptions.ToList();
        }

        // Get a specific Prescription by ID
        public Prescription Read(int id)
        {
            return db.Prescriptions.Find(id);
        }

        // Update Prescription
        public Prescription Update(Prescription obj)
        {
            var existingPrescription = db.Prescriptions.Find(obj.PrescriptionID);
            if (existingPrescription != null)
            {
                existingPrescription.PatientID = obj.PatientID;
                existingPrescription.DoctorID = obj.DoctorID;
                existingPrescription.PrescriptionDate = obj.PrescriptionDate;

                // Update medications if necessary
                existingPrescription.Medications = obj.Medications;

                db.SaveChanges();
                return existingPrescription;
            }
            return null;
        }
    }
}
