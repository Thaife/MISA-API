using MISA.Web11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Service
{
    public interface ITeacherService:IBaseService<Teacher>
    {
        public IEnumerable<object> GetTeacherFull(int PageSize, int PageNumber, string TextSearch);

        public int InsertTeacherServive(TeacherCustom teacherCustom);
    }

}
