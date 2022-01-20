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

                var sqlString = $"insert {tableName}({listProperty}) values({listValue})";
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
