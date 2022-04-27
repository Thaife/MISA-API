using Microsoft.Extensions.Configuration;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class UnitRepository:BaseRepository<Unit>, IUnitRepository
    {
        public UnitRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
