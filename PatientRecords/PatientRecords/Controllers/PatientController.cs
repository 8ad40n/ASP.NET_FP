using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PatientRecords.Controllers
{
    public class PatientController : ApiController
    {
        [HttpGet]
        [Route("api/patient")]
        public HttpResponseMessage GetAllUsers()
        {
            try
            {
                var data = PatientService.GetAllUsers();
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
