using Microsoft.Extensions.Configuration;
using Misa.Ex.Core.Entity;
using MISA.Web11.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class SubjectGroupRepository:BaseRepository<SubjectGroup>, ISubjectGroupRepository
    {
        public SubjectGroupRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
