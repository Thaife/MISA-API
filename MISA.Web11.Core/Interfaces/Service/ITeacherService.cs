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
        
        /// <summary>
        /// Xử lý nghiệp vụ thêm record
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns></returns>
        public int InsertTeacherService(TeacherCustom teacherCustom);

        /// <summary>
        /// Xử lý nghiệp vụ sửa dữ liệu
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public int UpdateTeacherService(TeacherCustom teacherCustom, Guid teacherId);

        /// <summary>
        /// Xử lý nghiệp vụ xóa record qua Id
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public int DeleteTeacherFullById(Guid teacherId);

        /// <summary>
        /// Xử lý nghiệp vụ xóa nhiều record qua nhiều Id
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public int DeleteMultiTeacherFullByIds(List<Guid> listTeacherId);

        /// <summary>
        /// Xuất dữ liệu ra file Excel
        /// Created by: Thai(18/1/2022)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Stream ExportExcel();
    }

}
