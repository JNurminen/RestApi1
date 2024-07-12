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

        // Hae yksi työntekijä id:n perusteella
        [HttpGet("{id}")]
        public ActionResult GetOneEmployeeById(int id)
        {
            var työntekijä = db.Employees.Find(id);
            if (työntekijä != null)
            {
                return Ok(työntekijä);
            }
            else
            {
                return NotFound($"Työntekijää {id} ei löytynyt");
            }
        }

        // Lisää uusi työntekijä
        [HttpPost]
        public ActionResult AddNew([FromBody] Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Ok("Työntekijä lisätty onnistuneesti" + emp.City);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää:" + e.InnerException);
            }
        }
    }
}

