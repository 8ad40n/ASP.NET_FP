using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class AppointmentRepo : Repo, IRepo<Appointment, int, Appointment>
    {
        // Create Appointment
        public Appointment Create(Appointment obj)
        {
            obj.Status = "Pending";  // Initially set status to Pending
            db.Appointments.Add(obj);
            db.SaveChanges();  // Save changes to database
            return obj;  // Return the created appointment
        }

        // Delete Appointment
        public Appointment Delete(int id)
        {
            var appointment = db.Appointments.Find(id);  // Find the appointment by ID
            if (appointment != null)
            {
                db.Appointments.Remove(appointment);  // Remove the appointment from the db
                db.SaveChanges();  // Save changes
                return appointment;  // Return the deleted appointment
            }
            return null;  // Return null if the appointment was not found
        }

        // Get all Appointments
        public List<Appointment> Read()
        {
            return db.Appointments.ToList();  // Retrieve all appointments from the database
        }

        // Get a specific Appointment by ID
        public Appointment Read(int id)
        {
            return db.Appointments.Find(id);  // Retrieve the appointment by ID
        }

        // Update Appointment (status change or other updates)
        public Appointment Update(Appointment obj)
        {
            var existingAppointment = db.Appointments.Find(obj.AppointmentID);
            if (existingAppointment != null)
            {
                existingAppointment.Status = obj.Status; // Update only status
                db.SaveChanges();
                return existingAppointment;
            }
            return null;
        }

    }
}
