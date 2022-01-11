using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using MISA.Web11.Core.Service;
using MISA.Web11.Infrastructure.Repository;

namespace MISA.Web11.API.Controllers
{
    public class CustomersController : MISABaseController<Customer>
    {
        ICustomerService _customerService;
        ICustomerRepository _customerRepository;

        public CustomersController(ICustomerService customerService, ICustomerRepository customerRepository):base(customerService, customerRepository)
        {
            this._customerService = customerService;
            this._customerRepository = customerRepository;
        }

    } 
}
