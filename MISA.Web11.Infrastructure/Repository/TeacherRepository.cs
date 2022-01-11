using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.MISAAttribute;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public string GetNewTeacherCode()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "SELECT MAX(t.TeacherCode) FROM Teacher t";
                var res = sqlConnection.QueryFirstOrDefault<string>(sqlCommand);

                string[] temp = res.Split("-");
                res = temp[0] + "-" + (temp[1].ToString() + 1);

        
                return res;
            }
        }

        public IEnumerable<Teacher> Search(int PageSize, int PageNumber, string TextSearch)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(Teacher).Name;
                var fstRecord = (PageNumber - 1) * PageSize;
                var sqlCommand = $"Select * FROM {tableName} WHERE FullName LIKE '%{TextSearch}%' LIMIT @fstRecord,@pageSize";
                if (string.IsNullOrEmpty(TextSearch))
                {
                    sqlCommand = $"Select * from {tableName} LIMIT @fstRecord,@pageSize";
                }
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@pageSize", PageSize);
                dynamic.Add("@fstRecord", fstRecord);
                var customers = sqlConnection.Query<Teacher>(sqlCommand, param: dynamic);
                return customers;
            }
        }

        public int GetTotalRecord()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(Teacher).Name;
                var sqlCommand = $"Select COUNT(*) FROM {tableName} ";
                var total = sqlConnection.QueryFirstOrDefault<int>(sqlCommand);
                return total;
            }
        }

        public int InsertTeacherRepository(TeacherCustom teacherCustom)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var listColumnName = string.Empty;
                var listColumnParam = string.Empty;

                var parameters = new DynamicParameters();
                var props = teacherCustom.GetType().GetProperties();
                var teacherId = Guid.Empty;

                foreach (var prop in props)
                {
                    var ignore = prop.GetCustomAttributes(typeof(NotMap), true);
                    if (ignore.Length == 0)
                    {
                        // Lấy tên của property:
                        var propName = prop.Name;
                        // Lấy value của property
                        var propValue = prop.GetValue(teacherCustom);
                        // Kiểm tra, tạo mới cho khoá chính
                        var isPrimaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                        if (isPrimaryKey == true)
                        {
                            teacherId = Guid.NewGuid();
                            propValue = teacherId;
                        }

                        listColumnName += $"{propName},";
                        listColumnParam += $"@{propName},";
                        parameters.Add($"@{propName}", propValue);
                    }
                }


                listColumnName = listColumnName.Substring(0, listColumnName.Length - 1);
                listColumnParam = listColumnParam.Substring(0, listColumnParam.Length - 1);
                var sqlCommand = $"INSERT INTO Teacher ({listColumnName}) VALUES ({listColumnParam})";
                var fst = sqlConnection.Execute(sqlCommand, parameters);
                //var mid = InsertMuiltDepartment(teacherId, teacherCustom.ListDepartmentId);
                //var lst = InsertMuiltSubject(teacherId, teacherCustom.ListSubjectId);

                //if (fst + mid + lst >= 3)
                    return 1;

                return -1;
            }
        }

        public int InsertMuiltDepartment(Guid teacherId, List<Guid> departmentIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                /*
                var deleteCommand = "DELETE FROM DepartmentAssistant WHERE TeacherId= @teacherId";
                var parameterDel = new DynamicParameters();
                parameterDel.Add("@teacherId", @teacherId);
                sqlConnection.Execute(deleteCommand, parameterDel);
                */
                var countExecute = 0;
                foreach (var departmentId in departmentIds)
                {
                    var sqlCommad = @"INSERT INTO DepartmentAssistant (TeacherId,DepartmentId) VALUES (@teacherId, @departmentId)";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@teacherId", teacherId);
                    parameters.Add("@departmentId", departmentId);
                    var result = sqlConnection.Execute(sqlCommad, param: parameters);
                    countExecute += result;
                }
                return countExecute;
            }

        }

        public int InsertMuiltSubject(Guid teacherId, List<Guid> subjectIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                /*
                var deleteCommand = "DELETE FROM DepartmentAssistant WHERE TeacherId= @teacherId";
                var parameterDel = new DynamicParameters();
                parameterDel.Add("@teacherId", @teacherId);
                sqlConnection.Execute(deleteCommand, parameterDel);
                */
                var countExecute = 0;
                foreach (var subjectId in subjectIds)
                {
                    var sqlCommad = @"INSERT INTO SubjectAssistant (TeacherId,SubjectId) VALUES (@teacherId, @subjectId)";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@teacherId", teacherId);
                    parameters.Add("@subjectId", subjectId);
                    var result = sqlConnection.Execute(sqlCommad, param: parameters);
                    countExecute += result;
                }
                return countExecute;
            }

        }


        public bool CheckTeacherCodeDuplicate(string TeacherCode)
        {
            throw new NotImplementedException();
        }
    }
}
