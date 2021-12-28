using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Server;
using MISA.Web11.Core.Server;
using MISA.Web11.Infrastructure.Repository;

namespace MISA.Web11.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        ICustomerServer _customerServer;
        ICustomerRepository _customerRepository;

        public CustomersController(ICustomerServer customerServer, ICustomerRepository customerRepository)
        {
            this._customerServer = customerServer;
            this._customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = _customerRepository.GetCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }

        [HttpPost]
        public IActionResult Insert(Customer customer)
        {
            try
            {
                var res = _customerServer.InsertServer(customer);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                else
                {
                    return StatusCode(200, res);
                }
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {

                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi, vui lòng liên hệ MISA để được trợ giúp !!"
                };

                return StatusCode(500, res);
            }
        }

        [HttpPut("{customerId}")]
        public IActionResult Update(Customer customer, Guid customerId)
        {
            try
            {
                var res = _customerServer.UpdateServer(customer, customerId);
                if (res > 0)
                {
                    return StatusCode(200, customer);
                }
                else
                {
                    throw new MISAValidateException(null);
                }
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {

                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi, vui lòng liên hệ MISA để được trợ giúp !!"
                };

                return StatusCode(500, res);
            }

        }

        [HttpDelete("{customerId}")]
        public IActionResult Delete(Guid customerId)
        {
            try
            {
                var res = _customerRepository.Delete(customerId);
                if(res > 0)
                {
                    return StatusCode(200, res);
                }
                else
                {
                    return StatusCode(400);
                }
            }
            catch (Exception ex)
            {

                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi, vui lòng liên hệ MISA để được trợ giúp !!"
                };

                return StatusCode(500, res);
            }
        }
    } 
}
