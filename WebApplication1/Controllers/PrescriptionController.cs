using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/prescription")]
    public class PrescriptionController : ControllerBase
    {
        private IDbService _dbService;

        public PrescriptionController(IDbService iDbService)
        {
            _dbService = iDbService;
        }
        
        [HttpGet("{id}")]
        public IActionResult GetPrescription(int id)
        {
            Prescription prescription = _dbService.GetPrescription(id);
            if (prescription == null)
                return NotFound("Prescription Not Found");
            return Ok(prescription);
        }

        [HttpPost]
        public IActionResult CreatePrescription(Prescription prescription)
        {
            Prescription newPrescription = _dbService.CreatePrescription(prescription);
            if (newPrescription==null)
            {
                return BadRequest("Bad Request");
            }

            return Ok(prescription);
        }
        
    }
}