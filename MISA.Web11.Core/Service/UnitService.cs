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
    public class UnitService:BaseService<Unit>, IUnitService
    {
        #region field
        IUnitRepository _unitRepository;


        IBaseRepository<Unit> _baseRepository;
        List<Object> errLstMsgs = new List<Object>();

        #endregion

        #region constructor
        public UnitService(
            IUnitRepository _unitRepository,
            IBaseRepository<Unit> _baseRepository
        ) : base(_unitRepository)
        {
            this._baseRepository = _baseRepository;
        }
        #endregion
    }
}
