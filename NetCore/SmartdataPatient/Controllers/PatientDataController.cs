using Microsoft.AspNetCore.Mvc;
using SmartdataPatient.Models;
using SmartdataPatient.Repository;
using System.Net;

namespace SmartdataPatient.Controllers
{
    public class PatientDataController : Controller
    {
        private IPatientData pat;

        public PatientDataController(IPatientData _pat)
        {
            pat = _pat;
        }

        //get api

        [HttpGet]
        [Route("GetAllPatientData")]
        public IActionResult GetAllPatientData()
        {
            return Ok(pat.GetAllPatientData());
        }

        //post & update api 

        [HttpPost]
        [Route("PostData")]
        public IActionResult PostPatientData([FromBody] PostModel model)
        {
            
            if (!ModelState.IsValid)
            {
                ResponseMessageModel response = new ResponseMessageModel();
                var message = string.Join(" | ", ModelState.Values
                 .SelectMany(v => v.Errors)
                 .Select(e => e.ErrorMessage));
                response.Message = message;

                return Ok(response);

            }
            else
            {
                return Ok(pat.PostPatientData(model));
            }
            
        }

        //delete api

        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeletePatientData(int id, string userName) 
        {
            return Ok(pat.DeletePatientData(id,userName));
        }

        //getById api

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            return Ok(pat.GetById(id));
        }

        //country api
        [HttpGet]
        [Route("Country")]

        public IActionResult Country()
        {
            return Ok(pat.Country());
        }
    }
}
