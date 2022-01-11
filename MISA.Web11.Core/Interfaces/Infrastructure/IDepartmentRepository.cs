using MISA.Web11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {
        /// <summary>
        /// lấy thông tin phòng mà giáo viên phụ trách
        /// </summary>
        /// <param name="tearchId">id giáo viên</param>
        /// <returns>Thông tin phòng ban</returns>
        /// CreateBy: TVThai()
        public IEnumerable<Department> GetDepartmentByTearchId(Guid tearchId);
        /// <summary>
        /// Thêm nhiều phòng ban do giáo viên phụ trách
        /// </summary>
        /// <param name="teacherId">id cán bộ giáo viên</param>
        /// <param name="departmentIds">danh dách id các phòng ban được thêm</param>
        /// <returns>sô phòng ban được thêm</returns>
        /// CreateBy: Phạm Văn Hải (5/1/2022)
        public int InsertMuiltService(Guid teacherId, List<Guid> departmentIds);
    }

    
}
