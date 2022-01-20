using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Ex.Core.Entity
{
    /// <summary>
    /// Thông tin môn
    /// Created by: Thai(13/1/2022)
    /// </summary>
    public class Subject
    {
        /// <summary>
        /// Id môn học
        /// </summary>
        [PrimaryKey]
        public Guid SubjectId { get; set; }

        /// <summary>
        /// Tên môn học
        /// </summary>
        [NotEmpty]
        public string SubjectName { get; set; }
    }
}
