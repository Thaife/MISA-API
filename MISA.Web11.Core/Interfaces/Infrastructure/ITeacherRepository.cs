using MISA.Web11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface ITeacherRepository : IBaseRepository<Teacher>
    {
        #region Lấy dữ liệu
        /// <summary>
        /// Lấy mã nhân giáo viên mới
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <returns>a new code</returns>
        public string GetNewTeacherCode();

        /// <summary>
        /// lấy dữ liệu phân trang hoặc tìm kiếm
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNumber"></param>
        /// <param name="TextSearch"></param>
        /// <returns></returns>
        public IEnumerable<object> GetTeacherFull(int PageSize, int PageNumber, string TextSearch);

        /// <summary>
        /// lấy toàn bộ giáo viên
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TeacherCustom> GetTeacherFulls();

        /// <summary>
        /// lấy dữ liệu qua Id
        /// Created by: Thai(13/1/2022)
        /// </summary>
        /// <param name="teacherId"></param>
        /// <returns></returns>
        public object GetTeacherFullById(Guid teacherId);


        /// <summary>
        /// Lấy tổng số bản ghi
        /// Create by: Thai(13/1/2022)
        /// </summary>
        /// <returns>tổng bản ghi (number)</returns>
        public int GetTotalRecord();
        #endregion

        #region Thêm dữ liệu
        /// <summary>
        /// Thực hiện thêm thông tin  giáo viên
        /// Created By: Thai(13/6/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns>Số bản ghi được thực hiện</returns>
        public int InsertTeacherRepository(TeacherCustom teacherCustom);

        /// <summary>
        /// Thực hiện thêm thông tin phòng giáo viên
        /// Created By: Thai(13/6/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns>Số bản ghi được thực hiện</returns>
        public int InsertMuiltDepartment(Guid teacherId, List<Guid> departmentIds);

        /// <summary>
        /// Thực hiện thêm thông tin môn học giáo viên
        /// Created By: Thai(13/6/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns>Số bản ghi được thực hiện</returns>
        public int InsertMuiltSubject(Guid teacherId, List<Guid> subjectIds);
        #endregion

        #region Sửa dữ liệu
        /// <summary>
        /// Thực hiện sửa thông tin  giáo viên
        /// Created By: Thai(13/6/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns>Số bản ghi được thực hiện</returns>
        public int UpdateTeacherRepository(TeacherCustom teacherCustom, Guid teacherId);

        /// <summary>
        /// Thực hiện sửa thông tin phòng giáo viên
        /// Created By: Thai(13/6/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns>Số bản ghi được thực hiện</returns>
        public int UpdateMuiltDepartment(Guid teacherId, List<Guid> departmentIds);

        /// <summary>
        /// Thực hiện sửa thông tin môn học giáo viên
        /// Created By: Thai(13/6/2022)
        /// </summary>
        /// <param name="teacherCustom"></param>
        /// <returns>Số bản ghi được thực hiện</returns>
        public int UpdateMuiltSubject(Guid teacherId, List<Guid> subjectIds);
        #endregion

        #region Xóa dữ liệu
        /// <summary>
        /// Xóa nhiều giáo viên
        /// Created by:Thai(16/1/2011)
        /// </summary>
        /// <param name="listId"></param>
        /// <returns>Số bản ghi bị xóa</returns>
        public int DeleteMultiTeacherByTeacherIds(List<Guid> listId);
        #endregion

    }

}
