using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Ex.Core.Entity;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;

namespace MISA.Web11.API.Controllers
{
    public class SubjectGroupsController : MISABaseController<SubjectGroup>
    {
        public SubjectGroupsController(IBaseService<SubjectGroup> baseService, IBaseRepository<SubjectGroup> baseRepository) : base(baseService, baseRepository)
        {

        }
    }
}
