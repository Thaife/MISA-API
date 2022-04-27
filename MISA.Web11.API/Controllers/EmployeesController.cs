using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using System.Linq;

namespace MISA.Web11.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MISABaseController<Employee>
    {
        #region fields
        IEmployeeRepository _employeeRepository;
        IEmployeeService _employeeService;
        #endregion
        #region constructor
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeService employeeService) : base(employeeService, employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _employeeService = employeeService;
        }
        #endregion

        #region method
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)

        [HttpGet("newcode")]
        public IActionResult GetNewTeacherCode()
        {
            try
            {
                var res = _employeeRepository.GetNewEmployeeCode();
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
        /// Lấy toàn bộ danh sách nhân viên
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        
        [HttpGet()]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = _employeeRepository.GetEmployees();
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
        /// Lấy toàn bộ thông tin của danh sách nhân viên theo trang hoặc tìm kiếm
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        [HttpGet("paging")]
        public IActionResult GetEmployeePaging(int pageSize, int pageNumber, string? textSearch)
        {
            try
            {
                var employees = _employeeRepository.GetEmployeePaging(pageSize, pageNumber, textSearch);
                object total;
                if (!string.IsNullOrEmpty(textSearch))
                {
                    total = _employeeRepository.GetTotalRecordSearch(textSearch);
                } else
                {
                    total = _employeeRepository.GetTotalRecord();

                }
                return Ok(new
                {
                    total,
                    pageSize,
                    pageNumber,
                    employees
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
        /// Lấy toàn bộ thông tin của nhân viên qua Id
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
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
        /// Thêm nhân viên
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        [HttpPost]
        public IActionResult Post(Employee employee)
        {
            try
            {
                var res = _employeeService.InsertEmployeeService(employee);
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
        /// Sửa nhân viên
        /// </summary>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        [HttpPut("{id}")]
        public IActionResult Update(Employee employee, Guid id)
        {
            try
            {
                var res = _employeeService.UpdateEmployeeService(employee, id);
                return StatusCode(200, res);
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
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            try
            {
                var res = _employeeService.DeleteEmployeeById(id);
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
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        /// Create By: Thai(19/4/2022)
        [HttpPut("multi")]
        public IActionResult DeleteMultiEmployee(List<Guid> listId)
        {
            try
            {
                var res = _employeeRepository.DeleteMulti(listId);
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
            var stream = _employeeService.ExportExcel();
            string fileName = $"{Core.Properties.Resources.Excel_Title_Employee}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }
        
        #endregion
    }
}
