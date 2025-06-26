using BLL.DTOs;
using BLL.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientRecords.Controllers
{
    [RoutePrefix("api/prescription")]
    public class PrescriptionController : ApiController
    {
        // Create a new Prescription
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreatePrescription([FromBody] PrescriptionDTO prescriptionDto)
        {
            try
            {
                var data = PrescriptionService.CreatePrescription(prescriptionDto);
                return Request.CreateResponse(HttpStatusCode.Created, data);  // Return Created response
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);  // Handle exceptions
            }
        }

        // Get a Prescription by ID
        [HttpGet]
        [Route("{id}")]
        public HttpResponseMessage GetPrescriptionById(int id)
        {
            try
            {
                var data = PrescriptionService.GetPrescriptionById(id);
                if (data == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prescription not found");

                return Request.CreateResponse(HttpStatusCode.OK, data);  // Return prescription data
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);  // Handle exceptions
            }
        }

        // Get all Prescriptions
        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAllPrescriptions()
        {
            try
            {
                var data = PrescriptionService.GetAllPrescriptions();
                return Request.CreateResponse(HttpStatusCode.OK, data);  // Return all prescriptions
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);  // Handle exceptions
            }
        }

        // In PrescriptionController

        // PATCH: Update specific fields of a Prescription without altering relationships
        [HttpPatch]
        [Route("{id}")]
        public HttpResponseMessage PatchPrescription(int id, [FromBody] PrescriptionDTO prescriptionDto)
        {
            try
            {
                if (prescriptionDto == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid data.");

                var updatedPrescription = PrescriptionService.UpdatePrescriptionPartial(id, prescriptionDto);

                if (updatedPrescription == null)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prescription not found.");

                return Request.CreateResponse(HttpStatusCode.OK, updatedPrescription); // Return the updated prescription
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message); // Handle any errors
            }
        }



        // Delete a Prescription
        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage DeletePrescription(int id)
        {
            try
            {
                bool isDeleted = PrescriptionService.DeletePrescription(id);
                if (!isDeleted)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prescription not found or could not be deleted due to foreign key constraints.");

                return Request.CreateResponse(HttpStatusCode.OK, "Prescription deleted successfully");
            }
            catch (Exception e)
            {
                // Log the exception if necessary (e.g., using a logging framework)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, $"Error deleting prescription: {e.Message}");
            }
        }

        //[HttpGet]
        //[Route("generate-pdf/{id}")]
        //public HttpResponseMessage GeneratePrescriptionPdf(int id)
        //{
        //    try
        //    {
        //        // Create an instance of PrescriptionService
        //        var prescriptionService = new PrescriptionService();

        //        // Call the instance method to generate the PDF
        //        var pdfPath = prescriptionService.GeneratePrescriptionPdf(id);

        //        if (string.IsNullOrEmpty(pdfPath))
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Prescription not found for PDF generation");

        //        // Assuming the file is accessible via a relative URL like /pdfs/prescription_{id}.pdf
        //        string relativeUrl = "/pdfs/" + Path.GetFileName(pdfPath); // Get the file name from path

        //        // Return the PDF path in the response
        //        return Request.CreateResponse(HttpStatusCode.OK, new { Message = "PDF generated successfully", PdfPath = relativeUrl });
        //    }
        //    catch (Exception e)
        //    {
        //        // Return an internal server error if something goes wrong
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
        //    }
        //}
    }
}
