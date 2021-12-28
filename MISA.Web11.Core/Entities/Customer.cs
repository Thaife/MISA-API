using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Entities
{
    /// <summary>
    /// Thông tin khách hàng
    /// Created by Thai (27/12/2021)
    /// </summary>
    public class Customer
    {
        #region Property

        /// <summary>
        /// ID : Khóa Chính
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerCode { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Ngày sinh khách hàng
        /// </summary>
        public string DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính khách hàng
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// Email khách hàng
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// SĐT khách hàng
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public string Address { get; set; }

        #endregion

    }
}
