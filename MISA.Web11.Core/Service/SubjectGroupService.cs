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
    public class SubjectGroupService:BaseService<SubjectGroup>, ISubjectGroupService
    {
        #region Fields
        ISubjectGroupRepository _subjectGroupRepository;
        #endregion

        #region Constructor
        public SubjectGroupService(ISubjectGroupRepository subjectGroupRepository) : base(subjectGroupRepository)
        {
            _subjectGroupRepository = subjectGroupRepository;
        }
        #endregion
    }
}
