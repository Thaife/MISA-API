using MISA.Web11.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface sử dụng cho khách hàng
    /// Created by : Thai (27/12/2021)
    /// </summary>
    /// <returns></returns>
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        protected bool checkCustomerCodeDuplicate(string customerCode);
    }
}
