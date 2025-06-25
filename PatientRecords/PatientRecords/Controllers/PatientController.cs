using BLL.Services;
using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientRecords.Controllers
{
    [RoutePrefix("api/patient")]
    public class PatientController : ApiController
    {
        // Get all Patients
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllPatients()
        {
            try
            {
                var data = PatientService.GetAllPatients();
                return Request.CreateResponse(HttpStatusCode.OK, data); 
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);  
            }
        }

        // Get a Patient by ID
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetPatientById(int id)
        {
            try
            {
                var data = PatientService.GetPatientById(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found");  

                return Request.CreateResponse(HttpStatusCode.OK, data); 
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // Create a new Patient
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreatePatient([FromBody] PatientDTO patientDto)
        {
            try
            {
                var data = PatientService.CreatePatient(patientDto);  
                return Request.CreateResponse(HttpStatusCode.Created, data);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); 
            }
        }

        // Update an existing Patient
        [HttpPatch]
        [Route("{id}")]
        public HttpResponseMessage UpdatePatient(int id, [FromBody] PatientDTO patientDto)
        {
            try
            {
                patientDto.PatientID = id;

                var data = PatientService.UpdatePatientFields(id, patientDto);

                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found");

                return Request.CreateResponse(HttpStatusCode.OK, data); 
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);  
            }
        }


        // Delete a Patient
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeletePatient(int id)
        {
            try
            {
                bool isDeleted = PatientService.DeletePatient(id); 
                if (!isDeleted)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Patient not found"); 

                return Request.CreateResponse(HttpStatusCode.OK, "Patient deleted successfully");  
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); 
            }
        }


        // Search by name
        [HttpGet]
        [Route("search")]
        public HttpResponseMessage SearchPatientsByName(string name)
        {
            try
            {
                var data = PatientService.SearchPatientsByName(name); 

                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No patients found matching the name");

                return Request.CreateResponse(HttpStatusCode.OK, data);  
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);  
            }
        }

        // Filter by gender
        [HttpGet]
        [Route("filter")]
        public HttpResponseMessage FilterPatientsByGender(string gender)
        {
            try
            {
                var data = PatientService.FilterPatientsByGender(gender); 

                if (data == null || data.Count == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No patients found matching the gender");

                return Request.CreateResponse(HttpStatusCode.OK, data);  
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); 
            }
        }




    }
}
