using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;

namespace MISA.Web11.API.Controllers
{
    public class TeachersController : MISABaseController<Teacher>
    {
        #region fields
        ITeacherRepository _teacherRepository;
        ITeacherService _teacherService;
        #endregion
        #region constructor
        public TeachersController(ITeacherRepository teacherRepository, ITeacherService teacherService) : base(teacherService, teacherRepository)
        {
            _teacherRepository = teacherRepository;
            _teacherService = teacherService;
        }
        #endregion

        #region method
        /// <summary>
        /// Lấy mã giáo viên mới
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)

        [HttpGet("newTeacherCode")]
        public IActionResult GetNewTeacherCode()
        {
            try
            {
                var res = _teacherRepository.GetNewTeacherCode();
                return Ok(res);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ValidateErrMsg
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Lấy toàn bộ danh sách giáo viên
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpGet("fulls")] 
        public IActionResult GetTeacherFull()
        {
            try
            {
                var teachers = _teacherRepository.GetTeacherFulls();
                return StatusCode(200, teachers);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Lấy toàn bộ thông tin của danh sách giáo viên theo trang hoặc tìm kiếm
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpGet("full")]
        public IActionResult GetTeacherFull(int PageSize, int PageNumber, string? TextSearch)
        {
            try
            {
                var teacherPaging = _teacherRepository.GetTeacherFull(PageSize, PageNumber, TextSearch);
                var total = _teacherRepository.GetTotalRecord();
                return Ok(new
                {
                    total,
                    PageSize,
                    PageNumber,
                    teacherPaging
                });
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Lấy toàn bộ thông tin của giáo viên qua Id
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpGet("full/{id}")]
        public IActionResult GetTeacherFull(Guid id)
        {
            try
            {
                var teacher = _teacherRepository.GetTeacherFullById(id);
                return StatusCode(200, teacher);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Thêm giáo viên
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpPost("full")]
        public IActionResult Post(TeacherCustom teacher)
        {
            try
            {
                var res = _teacherService.InsertTeacherService(teacher);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                return Ok(res);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Sửa giáo viên
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpPut("teacherfull/{id}")]
        public IActionResult Update(TeacherCustom teacherCustom, Guid id)
        {
            try
            {
                return StatusCode(200, _teacherService.UpdateTeacherService(teacherCustom, id));
            }
            catch(MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Xóa 1 giáo viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpDelete("full/{id}")]
        public IActionResult DeleteTeacherFull(Guid id)
        {
            try
            {
                var res = _teacherService.DeleteTeacherFullById(id);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
            
        }

        /// <summary>
        /// Xóa nhiều giáo viên
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Create By: Thai(16/1/2022)
        [HttpDelete("full/multi")]
        public IActionResult DeleteMultiTeacherFull(List<Guid>listId)
        {
            try
            {
                var res = _teacherService.DeleteMultiTeacherFullByIds(listId);
                return StatusCode(200, res);
            }
            catch (Exception ex)
            {

                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };
                return StatusCode(500, mes);
            }
        }

        /// <summary>
        /// Xuất dữ liệu ra Excel
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// Create By: Thai(18/1/2022)
        [HttpGet("export")]
        public IActionResult Export()
        {
            var stream = _teacherService.ExportExcel();
            string fileName = "Danh_sach_can_bo_giao_vien.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }
        #endregion
    }

}
