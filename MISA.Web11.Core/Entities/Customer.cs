using MISA.Web11.Core.Enum;
using MISA.Web11.Core.MISAAttribute;
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
        [PrimaryKey]
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        [NotEmpty]
        [NotDuplicate]
        public string CustomerCode { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        /// <summary>
        /// Tên khách hàng
        /// </summary>
        [NotEmpty]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính khách hàng
        /// </summary>
        public Gender? Gender { get; set; }

        [NotMap]
        public string? GenderName
        {
            get
            {
                switch (Gender)
                {
                    case Enum.Gender.Male:
                        return Properties.Resources.Enum_Gender_Male;
                    case Enum.Gender.Female:
                        return Properties.Resources.Enum_Gender_Female;
                    case Enum.Gender.Other:
                        return Properties.Resources.Enum_Gender_Other;
                    default:
                        return null;
                }
            }
        }


        /// <summary>
        /// Email khách hàng
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// SĐT khách hàng
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ khách hàng
        /// </summary>
        public string? Address { get; set; }

        #endregion

        

    }
}
