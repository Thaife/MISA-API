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
        /// CreateBy: TVThai(13/1/2022)
        public IEnumerable<Department> GetDepartmentByTearchId(Guid tearchId);

    }

    
}
