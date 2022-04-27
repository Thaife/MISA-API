using MISA.Web11.Core.Enum;
using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Entities
{
    public class Employee
    {
        /// <summary>
        /// Id nhân viên
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }


        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotDuplicate]
        [NotEmpty]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [NotEmpty]
        public string FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// 0: nữ,
        /// 1: nam,
        /// 2: khác,
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Tên giới tính
        /// </summary>
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
        /// Số điện thoại
        /// </summary>
        [PhoneNumber]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Email]
        public string? Email { get; set; }

        /// <summary>
        /// Có là khách hàng ?
        /// 1: Là khách hàng
        /// 0, else
        /// </summary>
        public int? Customer { get; set; }

        /// <summary>
        /// Có là nhà cung cấp
        /// 1: Có
        /// 0, Không
        /// </summary>
        public int? Producer { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? Landline { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Date]
        public string? DateOfBirth { get; set; }

        /// <summary>
        /// Vị trí
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// Chứng minh nhân dân
        /// </summary>
        public string? IdentityCard { get; set; }

        /// <summary>
        /// Ngày cấp chứng minh nhân dân
        /// </summary>
        [Date]
        public string? DateOfIdentityCard { get; set; }

        /// <summary>
        ///Nơi cấp chứng minh nhân dân
        /// </summary>
        public string? PlaceOfIdentityCard { get; set; }

        /// <summary>
        /// Số tài khoản
        /// </summary>
        public string? BankNumber {get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranch { get; set; }

        /// <summary>
        /// Mã tổ bộ môn
        /// </summary>
        public Guid UnitId { get; set; }

        /// <summary>
        /// Tên tổ bộ môn
        /// </summary>
        [NotMap]
        public string? UnitName { get; set; }

    }
}
