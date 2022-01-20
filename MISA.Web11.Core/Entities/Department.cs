using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Entities
{
    /// <summary>
    /// Thông tin phòng ban
    /// Created by: Thai(13/1/2022)
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Id phòng ban
        /// </summary>
        [PrimaryKey]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [NotEmpty]
        public string DepartmentName { get; set; }
    }
}
