using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;

namespace MISA.Web11.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<T> : ControllerBase
    {
        IBaseService<T> _baseService;
        IBaseRepository<T> _baseRepository;
        public MISABaseController(IBaseService<T> baseService, IBaseRepository<T> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var data = _baseRepository.Get();
                return StatusCode(200, data);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };

                return StatusCode(500, res);
            }
        }

        [HttpGet("{id}")]
        public virtual IActionResult Get(Guid id)
        {
            try
            {
                var data = _baseRepository.Get(id);
                return StatusCode(200, data);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };

                return StatusCode(500, res);
            }
        }

        [HttpPost]
        public virtual IActionResult Post(T entity)
        {
            try
            {
                var res = _baseService.InsertService(entity);
                if(res > 0) return StatusCode(201, res);
                return Ok(res);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };

                return StatusCode(500, res);
            }
        }

        [HttpPut("{id}")]
        public virtual IActionResult Update(T entity, Guid id)
        {
            try
            {
                var res = _baseService.UpdateService(entity, id);
                
                return StatusCode(200, res);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };

                return StatusCode(500, res);
            }
        }


        [HttpDelete("{id}")]
        public virtual IActionResult Delete(Guid id)
        {
            try
            {
                var data = _baseRepository.Delete(id);
                return StatusCode(200, data);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };

                return StatusCode(500, res);
            }
        }

        [HttpDelete("all")]
        public virtual IActionResult DeleteAll()
        {
            try
            {
                var data = _baseRepository.DeleteAll();
                return StatusCode(200, data);
            }
            catch (MISAValidateException ex)
            {
                return StatusCode(400, ex.Data);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = Core.Properties.Resources.ExceptionMISA
                };

                return StatusCode(500, res);
            }
        }
    }
}
