using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.Ex.Core.Entity;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;

namespace MISA.Web11.API.Controllers
{
    public class SubjectAssistantsController : MISABaseController<SubjectAssistant>
    {
        public SubjectAssistantsController(IBaseService<SubjectAssistant> baseService, IBaseRepository<SubjectAssistant> baseRepository):base(baseService, baseRepository)
        {

        }

    }
}
