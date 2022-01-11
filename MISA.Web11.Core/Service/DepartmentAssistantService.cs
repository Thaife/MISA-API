using Misa.Ex.Core.Entity;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Service
{
    public class DepartmentAssistantService:BaseService<DepartmentAssistant>, IDepartmentAssistantService
    {
        #region Fields
        IDepartmentAssistantRepository _departmentAssistantRepository;
        #endregion

        #region Constructor
        public DepartmentAssistantService(IDepartmentAssistantRepository departmentAssistantRepository) : base(departmentAssistantRepository)
        {
            _departmentAssistantRepository = departmentAssistantRepository;
        }
        #endregion
    }
}
