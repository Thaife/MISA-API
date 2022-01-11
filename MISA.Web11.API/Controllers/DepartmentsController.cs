using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;

namespace MISA.Web11.API.Controllers
{
    public class DepartmentsController : MISABaseController<Department>
    {
        public DepartmentsController(IBaseService<Department> baseService, IBaseRepository<Department> baseRepository) : base(baseService, baseRepository)
        {
        }
    }
}
