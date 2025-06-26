using BLL.DTOs;
using DAL.Models;
using DAL.Repos;
using AutoMapper;
using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL;
using System.Net.Mail;
using System.Net;

namespace BLL.Services
{
    public class AppointmentService
    {
        private static IRepo<Appointment, int, Appointment> _appointmentRepo = DataAccessFactory.AppointmentData();
        private static IRepo<Patient, int, Patient> _patientRepo = DataAccessFactory.PatientData();

        private static IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AppointmentDTO, Appointment>().ReverseMap();  // Map between AppointmentDTO and Appointment Model
        }).CreateMapper();

        // Create a new Appointment
        public static AppointmentDTO CreateAppointment(AppointmentDTO appointmentDto)
        {
            try
            {
                var appointment = _mapper.Map<Appointment>(appointmentDto);
                var createdAppointment = _appointmentRepo.Create(appointment);
                return _mapper.Map<AppointmentDTO>(createdAppointment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating appointment: {ex.Message}");
            }
        }

        // Get all Appointments
        public static List<AppointmentDTO> GetAllAppointments()
        {
            try
            {
                var appointments = _appointmentRepo.Read();
                return _mapper.Map<List<AppointmentDTO>>(appointments);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching all appointments: {ex.Message}");
            }
        }

        // Get a specific Appointment by ID
        public static AppointmentDTO GetAppointmentById(int id)
        {
            try
            {
                var appointment = _appointmentRepo.Read(id);
                if (appointment == null)
                    return null;
                return _mapper.Map<AppointmentDTO>(appointment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching appointment by ID: {ex.Message}");
            }
        }

        // Delete an Appointment
        public static bool DeleteAppointment(int id)
        {
            try
            {
                var appointment = _appointmentRepo.Delete(id);
                return appointment != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting appointment: {ex.Message}");
            }
        }

        public static AppointmentDTO ChangeAppointmentStatus(int appointmentId, string newStatus)
        {
            try
            {
                // Fetch the appointment from the database
                var appointment = _appointmentRepo.Read(appointmentId);
                if (appointment == null)
                    throw new Exception("Appointment not found.");

                // Update only the status
                appointment.Status = newStatus;

                // Save the changes
                var updatedAppointment = _appointmentRepo.Update(appointment);

                // Return the updated appointment as DTO
                return new AppointmentDTO
                {
                    AppointmentID = updatedAppointment.AppointmentID,
                    AppointmentDate = updatedAppointment.AppointmentDate,
                    Reason = updatedAppointment.Reason,
                    Status = updatedAppointment.Status,
                    PatientID = updatedAppointment.PatientID,
                    DoctorID = updatedAppointment.DoctorID
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error changing appointment status: {ex.Message}");
            }
        }

        // Send email to patient after status update
        public static void SendStatusUpdateEmail(AppointmentDTO appointment)
        {
            try
            {
                var patientId = appointment.PatientID;
                var patientEmail = _patientRepo.Read(patientId).Email;

                if (string.IsNullOrEmpty(patientEmail))
                    throw new Exception("Patient email is missing.");

                // Setup the email message
                var fromAddress = new MailAddress("bnjoytb@gmail.com", "BNJ Clinic");
                var toAddress = new MailAddress(patientEmail);
                const string subject = "Appointment Status Updated";
                string body = $"Dear Patient,\n\n" +
                              $"Your appointment with Doctor ID: {appointment.DoctorID} has been updated.\n" +
                              $"New Status: {appointment.Status}\n" +
                              $"Appointment Date: {appointment.AppointmentDate}\n\n" +
                              $"Thank you for choosing our clinic.\n\n" +
                              "Best regards,\nYour Clinic Team";

                // SMTP setup (example using Gmail SMTP)
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("bnjoytb@gmail.com", "otaz jmcf yqyo ymhd")
                };

                // Send the email
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                // Handle email sending failure (optional)
                throw new Exception($"Error sending email: {ex.Message}");
            }
        }
    }
}
