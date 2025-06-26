// DiagnosisController.cs
using BLL.DTOs;
using BLL.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientRecords.Controllers
{
    [RoutePrefix("api/Diagnosis")]
    public class DiagnosisController : ApiController
    {
        // POST: api/Diagnosis/CreateDiagnosis
        [HttpPost]
        [Route("CreateDiagnosis")]
        public IHttpActionResult CreateDiagnosis([FromBody] DiagnosisDTO diagnosisDto)
        {
            try
            {
                if (diagnosisDto == null)
                    return BadRequest("Invalid diagnosis data.");

                var createdDiagnosis = DiagnosisService.CreateDiagnosis(diagnosisDto);
                return Ok(new { Message = "Diagnosis created successfully", Diagnosis = createdDiagnosis });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Internal server error: {ex.Message}"));
            }
        }

        // GET: api/Diagnosis/GetDiagnosisById/{id}
        [HttpGet]
        [Route("GetDiagnosisById/{id}")]
        public IHttpActionResult GetDiagnosisById(int id)
        {
            try
            {
                var diagnosis = DiagnosisService.GetDiagnosisById(id);
                if (diagnosis == null)
                    return NotFound();

                return Ok(diagnosis);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Internal server error: {ex.Message}"));
            }
        }

        // GET: api/Diagnosis/GetAllDiagnoses
        [HttpGet]
        [Route("GetAllDiagnoses")]
        public IHttpActionResult GetAllDiagnoses()
        {
            try
            {
                var diagnoses = DiagnosisService.GetAllDiagnoses();
                return Ok(diagnoses);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Internal server error: {ex.Message}"));
            }
        }

        // DELETE: api/Diagnosis/DeleteDiagnosis/{id}
        [HttpDelete]
        [Route("DeleteDiagnosis/{id}")]
        public IHttpActionResult DeleteDiagnosis(int id)
        {
            try
            {
                var result = DiagnosisService.DeleteDiagnosis(id);
                if (!result)
                    return NotFound();

                return Ok(new { Message = "Diagnosis deleted successfully" });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Internal server error: {ex.Message}"));
            }
        }
    }
}
