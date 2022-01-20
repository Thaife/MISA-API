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
    public class SubjectAssistantRepository : BaseRepository<SubjectAssistant>, ISubjectAssistantRepository
    {
        public SubjectAssistantRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public int DeleteSubjectAssistantByTeacherId(Guid teacherId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(SubjectAssistant).Name;
                string sqlString = $"delete from {tableName} where TeacherId = @teacherId";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@teacherId", teacherId);
                var res = sqlConnection.Execute(sqlString, paras);

                return res;
            }
        }

        public int DeleteMultiSubjectAssistantByTeacherIds(List<Guid> listId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(SubjectAssistant).Name;
                var conditionString = string.Empty;
                var length = listId.Count;
                DynamicParameters paras = new DynamicParameters();
                for (int i = 0; i < length; i++)
                {
                    if (i == length - 1)
                        conditionString += $"TeacherId=@item{i}";
                    else
                        conditionString += $"TeacherId=@item{i} OR ";

                    paras.Add($"@item{i}", listId[i]);
                }

                string sqlString = $"delete from {tableName} where {conditionString}";

                var res = sqlConnection.Execute(sqlString, paras);

                return res;
            }
        }
    }
}
