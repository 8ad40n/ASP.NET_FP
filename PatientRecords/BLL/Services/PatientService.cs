using BLL.DTOs;
using DAL;
using AutoMapper;
using DAL.Models;
using System;
using System.Collections.Generic;
using DAL.Interfaces;
using System.Linq;

namespace BLL.Services
{
    public class PatientService
    {
        private static IRepo<Patient, int, Patient> _patientRepo = DataAccessFactory.PatientData();

        // AutoMapper configuration directly in the service
        private static MapperConfiguration _mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Patient, PatientDTO>();  // Mapping from Patient to PatientDTO
            cfg.CreateMap<PatientDTO, Patient>();  // Mapping from PatientDTO to Patient
        });

        private static IMapper _mapper = _mapperConfig.CreateMapper();

        public static List<PatientDTO> GetAllPatients()
        {
            var data = _patientRepo.Read();
            var mapped = _mapper.Map<List<PatientDTO>>(data);
            return mapped;
        }

        public static PatientDTO GetPatientById(int id)
        {
            var data = _patientRepo.Read(id);
            if (data == null) return null;
            var mapped = _mapper.Map<PatientDTO>(data);
            return mapped;
        }

        public static PatientDTO CreatePatient(PatientDTO patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            var createdPatient = _patientRepo.Create(patient);
            return _mapper.Map<PatientDTO>(createdPatient);
        }

        public static PatientDTO UpdatePatientFields(int patientId, PatientDTO patientDto)
        {
            var patient = _patientRepo.Read(patientId);

            if (patient == null)
                return null; 

            if (!string.IsNullOrEmpty(patientDto.FirstName))
                patient.FirstName = patientDto.FirstName;

            if (!string.IsNullOrEmpty(patientDto.LastName))
                patient.LastName = patientDto.LastName;

            if (patientDto.DOB != null)
                patient.DOB = patientDto.DOB;

            if (!string.IsNullOrEmpty(patientDto.Gender))
                patient.Gender = patientDto.Gender;

            if (!string.IsNullOrEmpty(patientDto.ContactNumber))
                patient.ContactNumber = patientDto.ContactNumber;

            if (!string.IsNullOrEmpty(patientDto.Email))
                patient.Email = patientDto.Email;

            if (!string.IsNullOrEmpty(patientDto.Address))
                patient.Address = patientDto.Address;

            var updatedPatient = _patientRepo.Update(patient);

            if (updatedPatient == null)
                return null; 

            return _mapper.Map<PatientDTO>(updatedPatient);
        }


        public static bool DeletePatient(int id)
        {
            var deletedPatient = _patientRepo.Delete(id);
            return deletedPatient != null;
        }

        public static List<PatientDTO> SearchPatientsByName(string name)
        {
            var data = _patientRepo.Read().Where(p =>
                p.FirstName.ToLower().Contains(name.ToLower()) ||
                p.LastName.ToLower().Contains(name.ToLower())
            ).ToList();

            if (data.Count == 0)
                return null;

            // Map the results to DTO
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Patient, PatientDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<PatientDTO>>(data);
        }

        public static List<PatientDTO> FilterPatientsByGender(string gender)
        {
            var data = _patientRepo.Read().Where(p =>
                p.Gender.ToLower().Equals(gender.ToLower(), StringComparison.OrdinalIgnoreCase)
            ).ToList();

            if (data.Count == 0)
                return null;

            // Map the results to DTO
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Patient, PatientDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<PatientDTO>>(data);
        }



    }
}
