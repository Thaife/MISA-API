using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Ex.Core.Entity
{
    public class SubjectAssistant
    {
        /// <summary>
        /// Id bảng phụ Teacher-Subject
        /// </summary>
        [PrimaryKey]
        public Guid SubjectAssistantId { get; set; }

        /// <summary>
        /// Id môn học
        /// </summary>
        [NotEmpty]
        public Guid SubjectId { get; set; }

        /// <summary>
        /// Id giáo viên
        /// </summary>
        [NotEmpty]
        public Guid TeacherId { get; set; }
    }
}
