using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi1.Models;

namespace RestApi1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Luodaan tietokantayhteys
        northwindContext db = new northwindContext();

        // Hae kaikki tuotteet
        [HttpGet]
        public ActionResult GetAllProducts()
        {
            var tuotteet = db.Products.ToList();
            return Ok(tuotteet);
        }

        [HttpGet("{id}")]
        public ActionResult GetOneProductById(int id)
        {
            var tuote = db.Products.Find(id);
            if (tuote != null)
            {
                return Ok(tuote);
            }
            else
            {
                return NotFound($"Tuotetta {id} ei löytynyt");
            }
        }

        // Lisää uusi tuote
        [HttpPost]
        public ActionResult AddNew([FromBody] Product tuote)
        {
            try
            {
                db.Products.Add(tuote);
                db.SaveChanges();
                return Ok("Tuote lisätty onnistuneesti: " + tuote.ProductName);
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää:" + e.InnerException);
            }
        }

        // Poista tuote
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try 
            { 
            var tuote = db.Products.Find(id);
            if (tuote != null) // Jos tuote löytyy, poistetaan se
            {
                db.Products.Remove(tuote);
                db.SaveChanges();
                return Ok("Tuote " + tuote.ProductName + " poistettu");
            }
            else
            {
                return NotFound("Tuotetta ei löytynyt");
            }
            }
            catch (Exception e)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + e);
            }
        }
    }
}
