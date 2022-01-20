using Misa.Ex.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface IDepartmentAssistantRepository:IBaseRepository<DepartmentAssistant>
    {
        /// <summary>
        /// Xóa dữ liệu phòng ban của 1 giáo viên
        /// Created by: Thai(16/6/2022)
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns>Số phòng đã xóa</returns>
        public int DeleteDepartmentAssistantByTeacherId(Guid teacherId);

        /// <summary>
        /// Xóa dữ liệu phòng ban của nhiều giáo viên
        /// Created by: Thai(16/6/2022)
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>Số phòng đã xóa</returns>
        public int DeleteMultiDepartmentAssistantByTeacherIds(List<Guid> listId);
    }
}
