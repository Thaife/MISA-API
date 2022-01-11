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
        /// CreateBy: TVThai()
        public IEnumerable<Subject> GetSubjectByTearchId(Guid teacherId);
        /// <summary>
        /// Thêm nhiều môn học mới do giáo viên phụ trách
        /// </summary>
        /// <param name="teacherId">Id của giáo viên</param>
        /// <param name="subjects">Danh sách id môn học</param>
        /// <returns>số môn học được thêm</returns>
        /// CreateBy: TVThai()
        public int PostMulti(Guid teacherId, List<Guid> subjects);
    }
}
