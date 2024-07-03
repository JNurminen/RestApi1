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
            var asiakkaat = db.Customers.ToList();
            return Ok(asiakkaat);
        }
    }
}
