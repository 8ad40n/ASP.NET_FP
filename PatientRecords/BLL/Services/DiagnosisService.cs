// DiagnosisService.cs
using BLL.DTOs;
using DAL.Models;
using DAL.Repos;
using AutoMapper;
using System;
using System.Collections.Generic;
using DAL.Interfaces;
using DAL;

namespace BLL.Services
{
    public class DiagnosisService
    {
        private static IRepo<Diagnosis, int, Diagnosis> _diagnosisRepo = DataAccessFactory.DiagnosisData();  // Diagnosis repository

        private static IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DiagnosisDTO, Diagnosis>().ReverseMap();  // Map DTO to Model
        }).CreateMapper();

        // Create a new Diagnosis
        public static DiagnosisDTO CreateDiagnosis(DiagnosisDTO diagnosisDto)
        {
            try
            {
                var diagnosis = _mapper.Map<Diagnosis>(diagnosisDto);
                diagnosis.DiagnosisDate = DateTime.Now;  // Set Diagnosis date
                var createdDiagnosis = _diagnosisRepo.Create(diagnosis);

                return _mapper.Map<DiagnosisDTO>(createdDiagnosis);  // Return created diagnosis as DTO
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating diagnosis: {ex.Message}");
            }
        }

        // Get Diagnosis by ID
        public static DiagnosisDTO GetDiagnosisById(int id)
        {
            try
            {
                var diagnosis = _diagnosisRepo.Read(id);
                if (diagnosis == null)
                    return null;

                return _mapper.Map<DiagnosisDTO>(diagnosis);  // Return the found diagnosis as DTO
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching diagnosis by ID: {ex.Message}");
            }
        }

        // Get all Diagnoses
        public static List<DiagnosisDTO> GetAllDiagnoses()
        {
            try
            {
                var diagnoses = _diagnosisRepo.Read();
                return _mapper.Map<List<DiagnosisDTO>>(diagnoses);  // Return all diagnoses as DTOs
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching all diagnoses: {ex.Message}");
            }
        }


        // Delete Diagnosis
        public static bool DeleteDiagnosis(int id)
        {
            try
            {
                var result = _diagnosisRepo.Delete(id);
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting diagnosis: {ex.Message}");
            }
        }
    }
}
