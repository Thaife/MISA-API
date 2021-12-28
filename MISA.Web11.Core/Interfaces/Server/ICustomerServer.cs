using MISA.Web11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Server
{
    public interface ICustomerServer
    {
        /// <summary>
        /// Xử lý nghiệp vụ thêm mới dữ liệu
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Số bản ghi thêm mới thành công</returns>
        /// Created by : Thai (27/12/2021)
        int InsertServer(Customer customer);

        /// <summary>
        /// Xử lý nghiệp vụ sửa dữ liệu
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Số bản ghi thêm mới thành công</returns>
        /// Created by : Thai (28/12/2021)
        int UpdateServer(Customer customer, Guid customerId);


    }
}
