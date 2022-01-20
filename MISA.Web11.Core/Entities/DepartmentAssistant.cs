using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Ex.Core.Entity
{
    public class DepartmentAssistant
    {
        [PrimaryKey]
        public Guid DepartmentAssistantId { get; set; }

        [NotEmpty]
        public Guid? DepartmentId { get; set; }

        [NotEmpty]
        public Guid? TeacherId { get; set; }
    }
}
