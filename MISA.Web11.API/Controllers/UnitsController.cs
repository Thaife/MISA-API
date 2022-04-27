using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;

namespace MISA.Web11.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UnitsController : MISABaseController<Unit>
    {
        #region fields
        IUnitRepository _unitRepository;
        IUnitService _unitService;
        #endregion
        #region constructor
        public UnitsController(IUnitRepository unitRepository, IUnitService unitService) : base(unitService, unitRepository)
        {
            _unitRepository = unitRepository;
            _unitService = unitService;
        }
        #endregion

        /// <summary>
        /// Lấy toàn bộ Đơn Vị
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4)
        [HttpGet()]
        public IActionResult Get()
        {
            try
            {
                var employees = _unitRepository.Get();
                return StatusCode(200, employees);
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
        /// Lấy Đơn Vị qua Id
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var employee = _unitRepository.Get(id);
                return StatusCode(200, employee);
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
        /// Thêm Đơn Vị
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4)
        [HttpPost]
        public IActionResult Post(Unit unit)
        {
            try
            {
                var res = _unitService.InsertService(unit);
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
        /// Sửa Đơn Vị
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4)
        [HttpPut("{id}")]
        public IActionResult Update(Unit unit, Guid id)
        {
            try
            {
                return StatusCode(200, _unitService.UpdateService(unit, id));
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
        /// Xóa 1 Đơn Vị
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Create By: Thai(19/4)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var res = _unitRepository.Delete(id);
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

    }
}
