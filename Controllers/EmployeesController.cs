using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi1.Models;

namespace RestApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Luodaan tietokantayhteys
        northwindContext db = new northwindContext();

        // Hae kaikki työntekijät
        [HttpGet]
        public ActionResult GetAllEmployees()
        {
            var työntekijät = db.Employees.ToList();
            return Ok(työntekijät);
        }
    }
}
