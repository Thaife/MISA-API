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
    public class SubjectService:BaseService<Subject>, ISubjectService 
    {
        #region Fields
        ISubjectRepository _subjectRepository;
        #endregion

        #region Constructor
        public SubjectService(ISubjectRepository _subjectRepository) : base(_subjectRepository)
        {
            _subjectRepository = _subjectRepository;
        }
        #endregion
    }
}
