using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Service
{
    public class DepartmentService:BaseService<Department>, IDepartmentService
    {

        #region Fields
        IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructor
        public DepartmentService(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        #endregion
    }
}
