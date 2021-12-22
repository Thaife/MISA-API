using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet("{Number}/{Name}")]
        public int? GetNumber(int? Number, string Name)
        {
            return Number;
        }

        [HttpGet("search")]
        public string GetName(string name)
        {
            return name;
        }

        [HttpPost]
        public object Post(Customer customer, string? name)
        {
            return new
            {
                Name = name,
                Data = customer
            };
        }

        [HttpPut]
        public Customer Put(Customer customer)
        {
            return customer;
        }
    }
}
