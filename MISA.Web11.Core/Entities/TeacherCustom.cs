using Misa.Ex.Core.Entity;
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
    /// Toàn bộ thông tin của teacher
    /// </summary>
    /// Created by: Thai(13/1/2022)
    public class TeacherCustom
    {
        #region prop Teacher
        /// <summary>
        /// Id giáo viên
        /// </summary>
        [PrimaryKey]
        public Guid TeacherId { get; set; }


        /// <summary>
        /// Mã giáo viên
        /// </summary>
        [NotDuplicate]
        [NotEmpty]
        [Code]
        public String TeacherCode { get; set; }

        /// <summary>
        /// Tên giáo viên
        /// </summary>
        [NotEmpty]
        [Alphabet]
        public String? FullName { get; set; }

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
        public String? PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Email]
        public String? Email { get; set; }
        public int? WorkStatus { get; set; }
        public int? EquipmentManagement { get; set; }
        [Date]
        public String? ResignDate { get; set; }
        public Guid? SubjectGroupId { get; set; }

        /// <summary>
        /// Tên chuyên môn
        /// </summary>
        [NotMap]
        public String? SubjectGroupName { get; set; }

        #endregion

        #region Subject
        /// <summary>
        /// Mã môn học
        /// </summary>
        [NotMap]
        public List<Guid>? SubjectIds { get; set; }

        /// <summary>
        /// Mã môn học
        /// </summary>
        [NotMap]
        public String? SubjectNames { get; set; }
        #endregion

        #region Department
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        [NotMap]
        public List<Guid>? DepartmentIds { get; set; }

        /// <summary>
        /// Tên môn học
        /// </summary>
        [NotMap]
        public String? DepartmentNames { get; set; }
        #endregion
    }
}
