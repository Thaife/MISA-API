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
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public string GetNewEmployeeCode()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "SELECT MAX(e.EmployeeCode) FROM Employee e where length(e.EmployeeCode) = (select max(Length(employee.EmployeeCode)) from employee)";
                var res = sqlConnection.QueryFirstOrDefault<string>(sqlCommand);

                string[] temp = res.Split("-");
                int numberCode = Int32.Parse(temp[1]);
                string nextEmployeeCode = numberCode < 9 ? "0" + (numberCode + 1) : numberCode + 1 + "";
                res = temp[0] + "-" + nextEmployeeCode;


                return res;
            }
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = "select E.*, U.UnitName from Employee E join Unit U on E.UnitId = U.UnitId";

                var Employees = sqlConnection.Query<Employee>(sqlCommand);
                return Employees;
            }
        }

        public IEnumerable<Employee> GetEmployeePaging(int PageSize, int PageNumber, string? TextSearch)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var fstRecord = (PageNumber - 1) * PageSize;
                DynamicParameters dynamic = new DynamicParameters();
                dynamic.Add("@textSearch", TextSearch);
                dynamic.Add("@pageSize", PageSize);
                dynamic.Add("@fstRecord", fstRecord);
                //Sql string join 2 bảng bảng lấy dữ liệu
                var sqlCommand = "select E.*, U.UnitName from Employee E join Unit U on E.UnitId = U.UnitId";

                //Nếu TextSearch tồn tại => add string tìm kiếm
                if (!string.IsNullOrEmpty(TextSearch))
                {
                    sqlCommand = "SELECT* FROM ( " + sqlCommand + $" WHERE E.EmployeeCode LIKE '%{TextSearch}%' OR E.FullName LIKE '%{TextSearch}%' ) as T ORDER BY Length(T.EmployeeCode) DESC,T.EmployeeCode DESC LIMIT @fstRecord,@pageSize "; ;
                    var Employees = sqlConnection.Query<Employee>(sqlCommand, param: dynamic);
                    return Employees;
                }
                //add string paging
                else
                {
                    sqlCommand += " ORDER BY Length(E.EmployeeCode) DESC,E.EmployeeCode DESC LIMIT @fstRecord,@pageSize";
                    var Employees = sqlConnection.Query<Employee>(sqlCommand, param: dynamic);
                    return Employees;
                }

            }

        }

        public object GetEmployeeById(Guid employeeId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@employeeId", employeeId);
                var sqlCommand = "select E.*, U.UnitName from Employee E join Unit U on E.UnitId = U.UnitId where E.EmployeeId = @employeeId";

                var Employee = sqlConnection.QueryFirstOrDefault<Employee>(sqlCommand, paras);
                return Employee;
            }
        }

        public int GetTotalRecord()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(Employee).Name;
                var sqlCommand = $"Select COUNT(*) FROM {tableName} ";
                var total = sqlConnection.QueryFirstOrDefault<int>(sqlCommand);
                return total;
            }
        }
        public int GetTotalRecordSearch(string textSearch)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(Employee).Name;
                DynamicParameters paras = new DynamicParameters();
                var sqlCommand = $"Select COUNT(*) FROM Employee E WHERE E.EmployeeCode LIKE '%{textSearch}% ' OR E.FullName LIKE '%{textSearch}%'";
                paras.Add("@textSearch", textSearch);
                var total = sqlConnection.QueryFirstOrDefault<int>(sqlCommand);
                return total;

            }

        }

        public int DeleteMultiEmployeeByIds(List<Guid> listId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(Employee).Name;
                //Khai báo chuỗi làm điều kiện where
                var conditionString = string.Empty;
                var length = listId.Count;
                DynamicParameters paras = new DynamicParameters();
                //Loop danh sách id
                for (int i = 0; i < length; i++)
                {
                    //add id cần xóa vào chuỗi làm điều kiện where
                    if (i == length - 1)
                        conditionString += $"EmployeeId=@item{i}";
                    else
                        conditionString += $"EmployeeId=@item{i} OR ";

                    paras.Add($"@item{i}", listId[i]);
                }

                string sqlString = $"delete from {tableName} where {conditionString}";
                var res = sqlConnection.Execute(sqlString, paras);

                return res;
            }
        }
    }
}
