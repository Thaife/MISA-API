using Microsoft.Extensions.Configuration;
using Misa.Ex.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class SubjectAssistantRepository : BaseRepository<SubjectAssistant>
    {
        public SubjectAssistantRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
