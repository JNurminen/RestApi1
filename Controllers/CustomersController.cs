using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi1.Models;

namespace RestApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Luodaan tietokantayhteys
        northwindContext db = new northwindContext();

        // Hae kaikki asiakkaat
        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            try
            {
                var asiakkaat = db.Customers.ToList();
                return Ok(asiakkaat);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        // Hae yksi asiakas id:n perusteella  
        [HttpGet("{id}")]
        public ActionResult GetOneCustomerById(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                {
                    return Ok(asiakas);
                }
                else
                {
                    return NotFound($"Asiakasta {id} ei löytynyt"); // string interpolation tapa
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Lisää uusi asiakas
        [HttpPost]
        public ActionResult AddNew([FromBody] Customer asiakas)
        {
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                return Ok("Asiakas lisätty onnistuneesti: " + asiakas.CompanyName);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // Poista asiakas
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null) // Jos asiakas löytyy, poistetaan se
                {
                    db.Customers.Remove(asiakas);
                    db.SaveChanges();
                    return Ok("Asiakas poistettu onnistuneesti: " + asiakas.CompanyName);
                }
                else
                {
                    return NotFound($"Asiakasta {id} ei löytynyt");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Päivitä asiakas
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(string id, [FromBody] Customer asiakas)
        {
            try
            {
                var updateAsiakas = db.Customers.Find(id);
                if (updateAsiakas != null)
                {
                    updateAsiakas.CompanyName = asiakas.CompanyName;
                    updateAsiakas.ContactName = asiakas.ContactName;
                    updateAsiakas.Address = asiakas.Address;
                    updateAsiakas.City = asiakas.City;
                    updateAsiakas.Region = asiakas.Region;
                    updateAsiakas.PostalCode = asiakas.PostalCode;
                    updateAsiakas.Country = asiakas.Country;
                    updateAsiakas.Phone = asiakas.Phone;
                    updateAsiakas.Fax = asiakas.Fax;

                    db.SaveChanges();
                    return Ok("Asiakas päivitetty onnistuneesti: " + asiakas.CompanyName);
                }
                else
                {
                    return NotFound($"Asiakasta {id} ei löytynyt");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }

        // Hae nimen osalla
        [HttpGet("companyname/{cname}")]
        public ActionResult GetCustomerByName(string cname)
        {
            try
            {
                var asiakkaat = db.Customers.Where(c => c.CompanyName.Contains(cname));
                // var asiakkaat = from c in db.Customers where c.CompanyName.Contains(cname) select c; <-- toinen tapa
                // var asiakkaat = db.Customers.Where(c => c.CompanyName == cname); <-- tarkka haku
                return Ok(asiakkaat);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex);
            }
        }
    }
}
