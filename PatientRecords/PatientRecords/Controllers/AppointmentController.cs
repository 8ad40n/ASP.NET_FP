using BLL.DTOs;
using BLL.Services;
using System;
using System.Web.Http;

namespace PatientRecords.Controllers
{
    [RoutePrefix("api/appointments")]
    public class AppointmentController : ApiController
    {
        // Create an Appointment
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateAppointment(AppointmentDTO appointmentDto)
        {
            try
            {
                var createdAppointment = AppointmentService.CreateAppointment(appointmentDto);
                return Ok(new { Message = "Appointment created successfully", Appointment = createdAppointment });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error: {ex.Message}"));
            }
        }

        // Get all Appointments
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllAppointments()
        {
            try
            {
                var appointments = AppointmentService.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error: {ex.Message}"));
            }
        }

        // Get an Appointment by ID
        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetAppointmentById(int id)
        {
            try
            {
                var appointment = AppointmentService.GetAppointmentById(id);
                if (appointment == null)
                    return NotFound();
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error: {ex.Message}"));
            }
        }

        // Change Appointment Status (PATCH)
        [HttpPatch]
        [Route("change-status/{id}")]
        public IHttpActionResult ChangeAppointmentStatus(int id, AppointmentDTO appointmentStatusDto)
        {
            try
            {
                // Call the service to change the status
                var updatedAppointment = AppointmentService.ChangeAppointmentStatus(id, appointmentStatusDto.Status);

                if (updatedAppointment == null)
                    return NotFound();

                // Send email to the patient after successful update
                AppointmentService.SendStatusUpdateEmail(updatedAppointment);

                return Ok(new { Message = "Appointment status updated successfully", Appointment = updatedAppointment });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error: {ex.Message}"));
            }
        }

        // Delete an Appointment
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteAppointment(int id)
        {
            try
            {
                var isDeleted = AppointmentService.DeleteAppointment(id);
                if (!isDeleted)
                    return NotFound();
                return Ok(new { Message = "Appointment deleted successfully" });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"Error: {ex.Message}"));
            }
        }
    }
}
