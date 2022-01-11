using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        

        #region Fields
        ICustomerRepository _customerRepository;
        #endregion

        #region Constructor
        public CustomerService(ICustomerRepository customerRepository):base(customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        #endregion

        protected override bool ValidateCustom(Customer customer)
        {
            //Validate dữ liệu customer
            return true;
        }
    }


}
