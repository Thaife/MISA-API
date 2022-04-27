using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Entities
{
    public class Unit
    {
        [PrimaryKey]
        public Guid UnitId { get; set; }
        [NotEmpty]
        public string UnitName { get; set; }
    }
}
