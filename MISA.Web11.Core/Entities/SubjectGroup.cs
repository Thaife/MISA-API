using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Ex.Core.Entity
{
    /// <summary>
    /// Thông tin tổ môn
    /// Created by: Thai(13/1/2022)
    public class SubjectGroup
    {
        /// <summary>
        /// Id tổ chuyên môn
        /// </summary>
        [PrimaryKey]
        public Guid SubjectGroupId { get; set; }

        /// <summary>
        /// Tên tổ chuyên môn
        /// </summary>
        public string? SubjectGroupName { get; set; }
    }
}
