using Misa.Ex.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface ISubjectRepository:IBaseRepository<Subject>
    {
        /// <summary>
        /// Lấy danh sách các môn học được giáo viên quản lý
        /// </summary>
        /// <param name="teacherId">Id giáo viên</param>
        /// <returns>Danh sách các môn học được giáo viên quản lý</returns>
        /// CreateBy: TVThai(13/1/2022)
        public IEnumerable<Subject> GetSubjectByTearchId(Guid teacherId);
    }
}
