using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class PatientRepo : Repo, IRepo<Patient, int, Patient>
    {
        public Patient Create(Patient obj)
        {
            throw new NotImplementedException();
        }

        public Patient Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Patient> Read()
        {
            return db.Patients.ToList();
        }

        public Patient Read(int id)
        {
            throw new NotImplementedException();
        }

        public Patient Update(Patient obj)
        {
            throw new NotImplementedException();
        }
    }
}
