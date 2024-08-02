using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApi1.Models;
using System.Xml.Linq;

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

        // Päivitä tuote
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product tuote)
        {
            try
            {
                var updateTuote = db.Products.Find(id);
                if (updateTuote != null)
                {
                    updateTuote.ProductName = tuote.ProductName;
                    updateTuote.QuantityPerUnit = tuote.QuantityPerUnit;
                    updateTuote.UnitPrice = tuote.UnitPrice;
                    updateTuote.UnitsInStock = tuote.UnitsInStock;
                    updateTuote.UnitsOnOrder = tuote.UnitsOnOrder;
                    updateTuote.ReorderLevel = tuote.ReorderLevel;
                    updateTuote.Discontinued = tuote.Discontinued;
                    updateTuote.Rpaprocessed = tuote.Rpaprocessed;
                    db.SaveChanges();
                    return Ok("Tuote päivitetty onnistuneesti: " + tuote.ProductName);
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

        // Hae nimen osalla
        [HttpGet("productname/{pname}")]
        public ActionResult GetProductsByName(string pname)
        {
            try
            {
                var tuote = db.Products.Where(p => p.ProductName.Contains(pname));
                // var tuote = from p in db.Products where p.ProductName.Contains(pname) select c; <-- toinen tapa
                // var tuote = db.Products.Where(c => p.ProductName == pname); <-- tarkka haku
                return Ok(tuote);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex);
            }
        }
    }
}
