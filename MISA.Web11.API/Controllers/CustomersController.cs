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

        /// <summary>
        /// Method Get Customer
        /// </summary>
        /// <returns></returns>
        /// Create by : Thai(28/12/2001)
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

        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(Guid customerId)
        {
            try
            {
                var res = _customerRepository.GetCustomerById(customerId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Method Post Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// Create by : Thai(28/12/2001)
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


        /// <summary>
        /// Method Put Customer
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Create by : Thai(28/12/2001)
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


        /// <summary>
        /// Method Delete Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Create by : Thai(28/12/2001)
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
