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
                return Ok("Työntekijä lisätty onnistuneesti" + emp.FirstName);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää:" + e.InnerException);
            }
        }

        // Poista työntekijä
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            try 
            { 
            var työntekijä = db.Employees.Find(id);
            if (työntekijä != null)
            {
                db.Employees.Remove(työntekijä);
                db.SaveChanges();
                return Ok("Työntekijä " + työntekijä.FirstName + " poistettu");
            }
            else
            {
                return NotFound($"Työntekijää {id} ei löytynyt");
            }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää:" + e.InnerException);
            }
        }

        // Päivitä työntekijä
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] Employee emp)
        {
            try
            {
                var työntekijä = db.Employees.Find(id);
                if (työntekijä != null)
                {
                    työntekijä.FirstName = emp.FirstName;
                    työntekijä.LastName = emp.LastName;
                    työntekijä.Title = emp.Title;
                    työntekijä.TitleOfCourtesy = emp.TitleOfCourtesy;
                    työntekijä.BirthDate = emp.BirthDate;
                    työntekijä.HireDate = emp.HireDate;
                    työntekijä.Address = emp.Address;
                    työntekijä.City = emp.City;
                    työntekijä.Region = emp.Region;
                    työntekijä.PostalCode = emp.PostalCode;
                    työntekijä.Country = emp.Country;
                    työntekijä.HomePhone = emp.HomePhone;
                    työntekijä.Extension = emp.Extension;
                    db.SaveChanges();
                    return Ok("Työntekijän tiedot päivitetty onnistuneesti");
                }
                else
                {
                    return NotFound($"Työntekijää {id} ei löytynyt");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää:" + e.InnerException);
            }
        }

        // Hae nimen osalla
        [HttpGet("first/{fname}")]
        public ActionResult GetEmployeeByName(string fname)
        {
            try
            {
                var työntekijät = db.Employees.Where(e => e.FirstName.Contains(fname));
                return Ok(työntekijät);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää:" + e.InnerException);
            }
        }
    }
}

