using BLL.DTOs;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Models;

namespace BLL.Services
{
    public class PatientService
    {
        public static List<PatientDTO> GetAllUsers()
        {
            var data = DataAccessFactory.PatientData().Read();

            // AutoMapper configuration
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Patient, PatientDTO>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<List<PatientDTO>>(data);
            return mapped;
        }
    }
}
