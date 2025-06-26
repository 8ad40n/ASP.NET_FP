using DAL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class DoctorRepo : IRepo<Doctor, int, Doctor>
    {
        private DBContext db;

        public DoctorRepo()
        {
            db = new DBContext();
        }

        // Create a new Doctor
        public Doctor Create(Doctor obj)
        {
            db.Doctors.Add(obj);
            db.SaveChanges();
            return obj;
        }

        // Get a Doctor by ID
        public Doctor Read(int id)
        {
            return db.Doctors.Find(id);
        }

        // Get all Doctors
        public List<Doctor> Read()
        {
            return db.Doctors.ToList();
        }

        // Update an existing Doctor
        public Doctor Update(Doctor obj)
        {
            var existingDoctor = db.Doctors.Find(obj.DoctorID);
            if (existingDoctor != null)
            {
                existingDoctor.FirstName = obj.FirstName;
                existingDoctor.LastName = obj.LastName;
                existingDoctor.Specialization = obj.Specialization;
                existingDoctor.ContactNumber = obj.ContactNumber;
                existingDoctor.Email = obj.Email;
                //existingDoctor.Address = obj.Address;

                db.SaveChanges();
                return existingDoctor;
            }
            return null;
        }

        // Delete a Doctor
        public Doctor Delete(int id)
        {
            var doctor = db.Doctors.Find(id);
            if (doctor != null)
            {
                db.Doctors.Remove(doctor);
                db.SaveChanges();
                return doctor;
            }
            return null;
        }
    }
}
