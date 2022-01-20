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
        [PrimaryKey]
        public Guid SubjectAssistantId { get; set; }

        [NotEmpty]
        public Guid? SubjectId { get; set; }

        [NotEmpty]
        public Guid? TeacherId { get; set; }
    }
}
