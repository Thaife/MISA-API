using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class DepartmentRepository:BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<Department> GetDepartmentByTearchId(Guid teacherId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCheck = @"SELECT * FROM Department d JOIN DepartmentAssistant da ON d.DepartmentId = da.DepartmentId JOIN Teacher t ON da.TeacherId = t.TeacherId WHERE t.TeacherId = @teacherId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@teacherId", teacherId);
                var result = sqlConnection.Query<Department>(sqlCheck, param: parameters);
                return result;
            }

        }

        public int InsertMuiltService(Guid teacherId, List<Guid> departmentIds)
        {
            throw new NotImplementedException();
        }
    }
}
