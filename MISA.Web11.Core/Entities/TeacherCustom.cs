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
    public class TeacherCustom
    {
        public class teacherCustome
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
            public String TeacherCode { get; set; }

            /// <summary>
            /// Tên giáo viên
            /// </summary>
            [NotEmpty]
            public String FullName { get; set; }
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
            public String? PhoneNumber { get; set; }
            public String? Email { get; set; }
            public int? WorkStatus { get; set; }
            public int? EquipmentManagement { get; set; }
            public DateTime? ResignDate { get; set; }
            public Guid SubjectGroupId { get; set; }

            #endregion

            #region Subject
            [NotMap]
            public List<Guid> ListSubjectId { get; set; }
            #endregion

            #region Department
            [NotMap]
            public List<Guid> ListDepartmentId { get; set; }
            #endregion
        }
    }
}
