using Misa.Ex.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface ISubjectAssistantRepository:IBaseRepository<SubjectAssistant>
    {
        /// <summary>
        /// Xóa dữ liệu môn học của 1 giáo viên
        /// Created by: Thai(16/1/2022)
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns>Số môn học đã xóa</returns>
        public int DeleteSubjectAssistantByTeacherId(Guid teacherId);

        /// <summary>
        /// Xóa dữ liệu môn học của 1 giáo viên
        /// Created by: Thai(16/1/2022)
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>Số môn học đã xóa</returns>
        public int DeleteMultiSubjectAssistantByTeacherIds(List<Guid> listId);
    }
}
