using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Server
{
    public class CustomerServer : ICustomerServer
    {
        #region Fields
        ICustomerRepository _customerRepository;

        public CustomerServer(ICustomerRepository customerRepository)
        {
            this._customerRepository = customerRepository;
        }
        #endregion

        #region Constructor

        #endregion

        #region Methods

        public int InsertServer(Customer customer)
        {
            //List lưu các thông tin lỗi
            List<string> errsList = new List<string>();

            //Kiểm tra mã khách hàng(không để trống, không trùng)
            var customerCode = customer.CustomerCode;
            if (string.IsNullOrEmpty(customerCode))
            {
                errsList.Add("Mã khách hàng không được phép để trống");
            }
            else
            {
                //Thực hiện kiểm tra trùng mã trong database
                var isDuplicate = _customerRepository.checkCustomerCodeDuplicate(customer.CustomerCode);
                if (isDuplicate)
                {
                    errsList.Add("Mã khách hàng đã tồn tại !");
                }


                //Kiểm tra tên khách hàng(không để trống)
                var fullName = customer.FullName;
                if (string.IsNullOrEmpty(fullName))
                {
                    errsList.Add("Tên khách hàng không được phép để trống");
                }
            }




            //Return các lỗi nếu có
            if (errsList.Count > 0)
            {
                var result = new
                {
                    devMsg = "",
                    userMsg = "Dữ liệu đầu vào không hợp lệ, vui lòng kiểm tra lại dữ liệu",
                    data = errsList
                };

                throw new MISAValidateException(result);
            }
            else
            {
                //Thêm mới dữ liệu vào database
                return _customerRepository.Insert(customer);
            }

        }

        public int UpdateServer(Customer customer, Guid customerId)
        {


            //Nếu không tồn tại => đưa ra thông báo
            List<string> errsList = new List<string>();



            //Nếu tồn tại rồi => đưa ra thông báo
            var res = _customerRepository.checkCustomerCodeDuplicate_NotCurrentCode(customerId, customer.CustomerCode);
            if (res)
            {
                errsList.Add("Mã khách hàng đã tồn tại");
            }

            //Kiểm tra mã khách hàng k được để trống
            if (customer.FullName == "")
            {
                errsList.Add("Tên khách hàng không được để trống");
            }


            //Nếu có lỗi trong quá trình validate dữ liệu => đưa ra thông báo
            if (errsList.Count > 0)
            {
                var result = new
                {
                    userMsg = "Dữ liệu đầu vào không hợp lệ, vui lòng kiểm tra lại dữ liệu",
                    data = errsList
                };
                throw new MISAValidateException(result);
            }
            else
            {
                return _customerRepository.Update(customer, customerId);
            }
        }

        #endregion

    }
}
