using MISA.Web11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface ITeacherRepository:IBaseRepository<Teacher>
    {
        public string GetNewTeacherCode();
        public IEnumerable<Teacher> Search(int PageSize, int PageNumber, string TextSearch);
        public int GetTotalRecord();

        public int InsertTeacherRepository(TeacherCustom teacherCustom);

        public int InsertMuiltDepartment(Guid teacherId, List<Guid> departmentIds);

        public int InsertMuiltSubject(Guid teacherId, List<Guid> subjectIds);
        protected bool CheckTeacherCodeDuplicate(string TeacherCode);
    }
}
