using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
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
        public TeachersController(ITeacherRepository teacherRepository, ITeacherService teacherService):base(teacherService, teacherRepository)
        {
            _teacherRepository = teacherRepository;
            _teacherService = teacherService;
        }
        #endregion
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
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ để được hỗ trợ"
                };
                return StatusCode(500, mes);
            }
        }

        [HttpGet("full")]
        public IActionResult GetTeacherFull(int PageSize, int PageNumber, string? TextSearch)
            {
                try
                {
                    var teacherPaging = _teacherService.GetTeacherFull(PageSize, PageNumber, TextSearch);
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
                        userMsg = "Có lỗi xảy ra, vui lòng liên hệ để được hỗ trợ"
                    };
                    return StatusCode(500, mes);
                }
            }


        [HttpPost("teacherFull")]
        public IActionResult Post(TeacherCustom teacher)
        {
            try
            {
                var res = _teacherService.InsertTeacherServive(teacher);
                if (res > 0)
                {
                    return StatusCode(201, res);
                }
                return Ok(res);
            }
            catch (Exception ex)
            {
                var mes = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi xảy ra, vui lòng liên hệ để được hỗ trợ"
                };
                return StatusCode(500, mes);
            }
        }

    }

}
