using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Ex.Core.Entity
{
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
