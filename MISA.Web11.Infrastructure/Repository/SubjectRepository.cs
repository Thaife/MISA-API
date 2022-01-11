using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.Ex.Core.Entity;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class SubjectRepository:BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Subject> GetSubjectByTearchId(Guid teacherId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCheck = @"SELECT * FROM Subject s JOIN SubjectAssistant sa ON s.SubjectId = sa.SubjectId JOIN Teacher t ON sa.TeacherId = t.TeacherId WHERE t.TeacherId = @teacherId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@teacherId", teacherId);
                var result = sqlConnection.Query<Subject>(sqlCheck, param: parameters);
                return result;
            }

        }

        public int PostMulti(Guid teacherId, List<Guid> subjects)
        {
            throw new NotImplementedException();
        }
    }
}
