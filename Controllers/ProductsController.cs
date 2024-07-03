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
    }
}
