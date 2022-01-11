using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.MISAAttribute
{
    [AttributeUsage(AttributeTargets.All)]
    public class NotMap:Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class PrimaryKey : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class NotEmpty : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.All)]
    public class NotDuplicate : Attribute
    {
    }
}
