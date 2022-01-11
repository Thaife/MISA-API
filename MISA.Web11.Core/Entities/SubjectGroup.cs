using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Ex.Core.Entity
{
    public class SubjectGroup
    {
        [PrimaryKey]
        public Guid SubjectGroupId { get; set; }

        public string? SubjectGroupName { get; set; }
    }
}
