using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.MISAAttribute;
using MySqlConnector;

namespace MISA.Web11.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        #region field and constructor
        protected string connectionString = String.Empty;   
        protected MySqlConnection sqlConnection;
        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Misa");
        }
        #endregion

        #region method
        public IEnumerable<T> Get()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
            var tableName = typeof(T).Name;
                var sqlString = $"select * from {tableName}";
                var entities = sqlConnection.Query<T>(sqlString);
                return entities;
            }
        }

        public T Get(Guid? entityId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                string sqlString = $"select * from {tableName} where {tableName}Id = @entityId";

                var paras = new DynamicParameters();
                paras.Add("@entityId", entityId);

                var entity = sqlConnection.QueryFirstOrDefault<T>(sqlString, paras);

                return entity;
            }

            
        }

        public IEnumerable<object> GetPaging(int PageSize, int PageNumber, string TextSearch)
        {
            return new List<object>();
//            using (sqlConnection = new MySqlConnection(connectionString))
//            {
//                var fstRecord = (PageNumber - 1) * PageSize;
//                DynamicParameters dynamic = new DynamicParameters();
//                var whereCond = $" WHERE T.EmployeeCode LIKE '%{TextSearch}%' OR T.FullName LIKE '%{TextSearch}%'";
//                //Sql string join 3 bảng bảng lấy dữ liệu
//                var sqlCommand = @"SELECT
//                              T.*,
//                              DAI.DepartmentNames,
//                              DAI.DepartmentIds,
//                              SAI.SubjectIds,
//                              SAI.SubjectNames,
//                              SGI.SubjectGroupName
//                            FROM Employee T
//                              LEFT JOIN (SELECT
//                                  DA.EmployeeId,
//                                  GROUP_CONCAT(D.DepartmentName SEPARATOR ', ') AS DepartmentNames,
//                                  GROUP_CONCAT(D.DepartmentId SEPARATOR ', ') AS DepartmentIds
//                                FROM DepartmentAssistant DA
//                                  JOIN Department D
//                                    ON DA.DepartmentId = D.DepartmentId
//                                WHERE D.DepartmentId IS NOT NULL
//                                GROUP BY DA.EmployeeId) AS DAI
//                                ON DAI.EmployeeId = T.EmployeeId

//                              LEFT JOIN (SELECT
//                                  SA.EmployeeId,
//                                  GROUP_CONCAT(S.SubjectName SEPARATOR ', ') AS SubjectNames,
//                                  GROUP_CONCAT(S.SubjectId SEPARATOR ', ') AS SubjectIds
//                                FROM SubjectAssistant SA
//                                  JOIN Subject S
//                                    ON SA.SubjectId = S.SubjectId
//                                WHERE S.SubjectId IS NOT NULL
//                                GROUP BY SA.EmployeeId) AS SAI
//                                ON SAI.EmployeeId = T.EmployeeId

//                            LEFT JOIN
//                                (SELECT SG.SubjectGroupId, SG.SubjectGroupName
//                                FROM SubjectGroup SG
//                                WHERE SG.SubjectGroupId IS NOT NULL) AS SGI
//                                ON SGI.SubjectGroupId = T.SubjectGroupId 
                             
//";
//                //Nếu TextSearch tồn tại => add string tìm kiếm
//                if (!string.IsNullOrEmpty(TextSearch))
//                {
//                    sqlCommand += whereCond;
//                }
//                //add string paging
//                else
//                {
//                    sqlCommand += "ORDER BY T.EmployeeCode DESC LIMIT @fstRecord,@pageSize";
//                    dynamic.Add("@pageSize", PageSize);
//                    dynamic.Add("@fstRecord", fstRecord);
//                }
//                var Employees = sqlConnection.Query<Object>(sqlCommand, param: dynamic);
//                return Employees;
//            }

        }

        public int Insert(T entity)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;

                //Khai báo 2 chuỗi sẽ dùng khi thực hiện insert
                var listProperty = string.Empty;
                var listValue = string.Empty;

                var paras = new DynamicParameters();
                var props = typeof(T).GetProperties();
                foreach (var prop in props)
                {
                    

                    //Kiểm tra nếu prop hiện tại có attri [NotMap] thì bỏ qua
                    var notMapProp = prop.GetCustomAttributes(typeof(NotMap), true);

                    if(notMapProp.Length == 0)
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(entity);

                        var propType = prop.PropertyType; 
                        //Kiểm tra nếu property hiện tại là PK => tạo mã mới để insert
                        var isPrimaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                        if (isPrimaryKey == true && propType == typeof(Guid))
                            propValue = Guid.NewGuid();

                        //add thêm giá trị vào 2 chuỗi sẽ xử dụng để insert
                        listProperty += $"{propName},";
                        listValue += $"@{propName},";

                        paras.Add($"@{propName}", propValue);
                    }
                }
                //Xóa kí tự thừa "," ở cuối 2 chuỗi
                listProperty = listProperty.Substring(0, listProperty.Length - 1);
                listValue = listValue.Substring(0, listValue.Length - 1);

                var sqlString = $"insert into {tableName}({listProperty}) values({listValue})";
                var res = sqlConnection.Execute(sqlString, paras);
                return res;
            }
        }

        public int Update(T entity, Guid entityId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                var listProps = typeof(T).GetProperties();

                //Khai báo 1 chuỗi option sử dụng để Update dữ liệu
                var option = string.Empty;
                var paras = new DynamicParameters();
                paras.Add("@entityId", entityId);
                foreach (var prop in listProps)
                {
                    //Kiểm tra nếu prop hiện tại chưa [NotMap] => bỏ qua
                    var inor = prop.GetCustomAttributes(typeof(NotMap), true);
                    if(inor.Length == 0)
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(entity);

                        //add thêm giá trị vào chuỗi sẽ xử dụng để update
                        option += $"{propName} = @{propName},";
                        //add dữ liệu cho các tham số sẽ dùng khi update
                        paras.Add($"@{propName}", propValue);
                    }
                    
                }
                //Xóa kí tự thừa "," ở cuối chuỗi
                option = option.Substring(0, option.Length - 1);

                var sqlString = $"update {tableName} set {option} where {tableName}Id = @entityId";

                var res = sqlConnection.Execute(sqlString, paras);
                return res;
            }
        }

        public int Delete(Guid entityId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                var sqlString = $"delete from {tableName} where {tableName}Id = @entityId";

                var paras = new DynamicParameters();
                paras.Add("@entityId", entityId);

                var res = sqlConnection.Execute(sqlString, paras);

                return res;
            }
        }

        public int DeleteAll()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                string sqlString = $"Delete from {tableName}";

                var res = sqlConnection.Execute(sqlString);

                return res;
            }
           
        }
        public int DeleteMulti(List<Guid> ids)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                List<string> temp = new List<string>();

                foreach (var id in ids)
                {
                    string idString = $" {tableName}Id = '{id}' ";
                    temp.Add(idString);
                }
                string condition = String.Join("OR", temp);
                string sqlString = $"Delete from {@tableName} where {@condition}";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@tableName", tableName);
                paras.Add("@condition", condition);
                var res = sqlConnection.Execute(sqlString);

                return res;
            }
        }

        public bool CheckDuplicate(string propName, string propValue)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                string sqlString = $"Select * from {tableName} where {propName} = @propValue";
                DynamicParameters paras = new DynamicParameters();
                paras.Add("@propValue", propValue);

                var res = sqlConnection.QueryFirstOrDefault<object>(sqlString, paras);

                if (res != null)
                    return true;
                return false;
            }
        }

        
        #endregion
    }
}
