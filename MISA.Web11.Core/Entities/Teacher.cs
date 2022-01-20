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
    /// Thông tin cá nhân của teacher
    /// Created by: Thai(13/1/2022)
    public class Teacher
    {
        /// <summary>
        /// Id giáo viên
        /// </summary>
        [PrimaryKey]
        public Guid TeacherId { get; set; }


        /// <summary>
        /// Mã giáo viên
        /// </summary>
        [NotDuplicate]
        public String TeacherCode { get; set; }

        /// <summary>
        /// Tên giáo viên
        /// </summary>
        [NotEmpty]
        public String FullName { get; set; }

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
        public String? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public String? Email { get; set; }

        /// <summary>
        /// Tình trạng công việc
        /// 1: đang làm việc
        /// 0, null: đã nghỉ việc
        /// </summary>
        public int? WorkStatus { get; set; }

        /// <summary>
        /// Tình trạng quản lý thiết bị
        /// 1: đang quản lý
        /// 0, null: không quản lý
        /// </summary>
        public int? EquipmentManagement { get; set; }

        /// <summary>
        /// Ngày nghỉ việc
        /// </summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// Mã tổ bộ môn
        /// </summary>
        public Guid? SubjectGroupId { get; set; }

    }
}

