using BLL.DTOs;
using DAL.Models;
using DAL.Repos;
using AutoMapper;
using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL;
using System.Linq;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.IO.Image;
using System.IO;
using iText.Kernel.Font;
using System.Threading.Tasks;
using iText.Html2pdf;
using QuestPDF.Fluent;

namespace BLL.Services
{
    public class PrescriptionService
    {
        private static IRepo<Prescription, int, Prescription> _prescriptionRepo = DataAccessFactory.PrescriptionData();  // Prescription repository
        private static IRepo<Patient, int, Patient> _patientRepo = DataAccessFactory.PatientData();  // Patient repository
        private static IRepo<Doctor, int, Doctor> _doctorRepo = DataAccessFactory.DoctorData();  // Doctor repository

        private static IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<PrescriptionDTO, Prescription>().ReverseMap();  // Map DTO to Model
            cfg.CreateMap<MedicationDTO, Medication>().ReverseMap();  // Map MedicationDTO and Medication Model
        }).CreateMapper();

        // Create a new Prescription
        public static PrescriptionDTO CreatePrescription(PrescriptionDTO prescriptionDto)
        {
            try
            {
                // Validate Patient and Doctor existence
                var patient = _patientRepo.Read(prescriptionDto.PatientID);
                var doctor = _doctorRepo.Read(prescriptionDto.DoctorID);

                if (patient == null)
                    throw new Exception("Patient not found");
                if (doctor == null)
                    throw new Exception("Doctor not found");

                // Map DTO to Model
                var prescription = _mapper.Map<Prescription>(prescriptionDto);
                prescription.PrescriptionDate = DateTime.Now; // Set the current date as prescription date

                // Create Prescription in DB
                var createdPrescription = _prescriptionRepo.Create(prescription);

                // Return the created Prescription as DTO
                return _mapper.Map<PrescriptionDTO>(createdPrescription);
            }
            catch (Exception ex)
            {
                // Log the exception (you can implement your logging logic here)
                // Logger.LogError(ex);

                throw new Exception($"Error creating prescription: {ex.Message}");
            }
        }

        // Get Prescription by ID
        public static PrescriptionDTO GetPrescriptionById(int id)
        {
            try
            {
                var prescription = _prescriptionRepo.Read(id);
                if (prescription == null)
                    return null;

                return _mapper.Map<PrescriptionDTO>(prescription);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Logger.LogError(ex);

                throw new Exception($"Error fetching prescription by ID: {ex.Message}");
            }
        }

        // Get all Prescriptions
        public static List<PrescriptionDTO> GetAllPrescriptions()
        {
            try
            {
                var prescriptions = _prescriptionRepo.Read();
                return _mapper.Map<List<PrescriptionDTO>>(prescriptions);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Logger.LogError(ex);

                throw new Exception($"Error fetching all prescriptions: {ex.Message}");
            }
        }

        // PATCH: Update specific fields of Prescription without altering relationships
        public static PrescriptionDTO UpdatePrescriptionPartial(int prescriptionId, PrescriptionDTO prescriptionDto)
        {
            var existingPrescription = _prescriptionRepo.Read(prescriptionId);

            if (existingPrescription == null)
                return null;

            // Update only the fields that are provided in the PATCH request (i.e., the ones that are non-null)
            if (prescriptionDto.PrescriptionDate != default(DateTime))
                existingPrescription.PrescriptionDate = prescriptionDto.PrescriptionDate;

            if (prescriptionDto.Medications != null && prescriptionDto.Medications.Any())
                existingPrescription.Medications = _mapper.Map<List<Medication>>(prescriptionDto.Medications); // Update medications

            // Save changes
            var updatedPrescription = _prescriptionRepo.Update(existingPrescription);

            if (updatedPrescription == null)
                return null;

            return _mapper.Map<PrescriptionDTO>(updatedPrescription);
        }



        // Delete a Prescription
        public static bool DeletePrescription(int id)
        {
            try
            {
                var prescription = _prescriptionRepo.Read(id); // Fetch the prescription to delete

                if (prescription == null)
                    return false;

                // Check if there are related entities that need to be handled (like foreign keys)
                // For example, you can remove the medications associated with the prescription before deleting
                if (prescription.Medications != null && prescription.Medications.Any())
                {
                    // Optionally, you can remove medications or handle them before deleting the prescription
                    foreach (var medication in prescription.Medications)
                    {
                        // Example: You might want to remove or nullify related medications
                        // _medicationRepo.Delete(medication.MedicationID); // If you want to delete medications too
                    }
                }

                // Delete the prescription now
                var deletedPrescription = _prescriptionRepo.Delete(id);
                return deletedPrescription != null;
            }
            catch (Exception ex)
            {
                // Log the exception (if using a logging framework)
                throw new Exception($"Error deleting prescription: {ex.Message}");
            }
        }


        //// Generate the Prescription PDF using QuestPDF
        //public string GeneratePrescriptionPdf(int id)
        //{
        //    try
        //    {
        //        // Fetch the Prescription from DB
        //        var prescription = _prescriptionRepo.Read(id);  // Fetch prescription from DB by ID

        //        if (prescription == null)
        //            return null;

        //        // Define the directory path where the PDFs will be stored
        //        string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pdfs");

        //        // Check if the directory exists, if not, create it
        //        if (!Directory.Exists(directoryPath))
        //        {
        //            Directory.CreateDirectory(directoryPath);  // Create the directory if it doesn't exist
        //        }

        //        // PDF file path (it will be stored in the "pdfs" folder in the base directory)
        //        string filePath = Path.Combine(directoryPath, $"prescription_{id}.pdf");

        //        // Generate the PDF with QuestPDF
        //        QuestPDF.Fluent.Document.Create(container =>
        //        {
        //            container.Page(page =>
        //            {
        //                //page.Size(QuestPDF.Fluent.PageSizes.A4).Margin(20);

        //                page.Content().Column(col =>
        //                {
        //                    col.Item().Text("Prescription")
        //                        .Bold()
        //                        .FontSize(24)
        //                        .AlignCenter();

        //                    col.Item().Text($"Prescription ID: {prescription.PrescriptionID}");
        //                    col.Item().Text($"Patient ID: {prescription.PatientID}");
        //                    col.Item().Text($"Doctor ID: {prescription.DoctorID}");
        //                    col.Item().Text($"Prescription Date: {prescription.PrescriptionDate:MMMM dd, yyyy}");

        //                    col.Item().Text("Medications:");

        //                    foreach (var medication in prescription.Medications)
        //                    {
        //                        col.Item().Text($"{medication.MedicationName} - {medication.Dosage}, {medication.Duration}");
        //                    }
        //                });
        //            });
        //        })
        //        .GeneratePdf(filePath);  // Generate the PDF and write it to the specified file path

        //        // Return the file path of the generated PDF
        //        return filePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        throw new Exception($"Error generating PDF for prescription: {ex.Message}");
        //    }
        //}
    }
}
