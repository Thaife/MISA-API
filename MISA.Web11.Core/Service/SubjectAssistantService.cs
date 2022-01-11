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
    public class SubjectAssistantService:BaseService<SubjectAssistant>, ISubjectAssistantService
    {

        #region Fields
        ISubjectAssistantRepository _subjectAssistantRepository;
        #endregion

        #region Constructor
        public SubjectAssistantService(ISubjectAssistantRepository subjectAssistantRepository) : base(subjectAssistantRepository)
        {
            _subjectAssistantRepository = subjectAssistantRepository;
        }
        #endregion
    }
}
