using MISA.Web11.Core.Entities;
using System;
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
    public interface ICustomerRepository
    {
        /// <summary>
        /// Lấy toàn bộ danh sách khách hàng
        /// Created by : Thai (27/12/2021)
        /// </summary>
        /// <returns></returns>
        IEnumerable<Customer> GetCustomers();

        int Insert(Customer customer);
        int Update(Customer customer, Guid customerId);
        int Delete(Guid customerId);

        /// <summary>
        /// Kiểm tra mã khách hàng đã có chưa
        /// </summary>
        /// <param name="customerCode"></param>
        /// <returns>true: đã tồn tại; false: chưa tồn tại</returns>
        /// Created by : Thai(27/12/2021)
        bool checkCustomerCodeDuplicate(string customerCode);



        /// <summary>
        /// Kiểm tra mã khách hàng đã có chưa nếu giá trị bị thay đổi
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="customerCode"></param>
        /// <returns>true: đã tồn tại; false: chưa tồn tại</returns>
        /// Created by : Thai(28/12/2021)
        bool checkCustomerCodeDuplicate_NotCurrentCode(Guid customerId, string customerCode);
    }
}
