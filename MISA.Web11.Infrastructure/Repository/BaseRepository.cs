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
        protected string connectionString = String.Empty;   
        public BaseRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Misa");
        }
        protected MySqlConnection sqlConnection;
        public IEnumerable<T> Get()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
            var tableName = typeof(T).Name;
                var sqlString = $"select * from {tableName}";
                var entities = sqlConnection.Query<T>(sqlString);
                Console.WriteLine("abc");
                return entities;
            }
        }

        public object Get(Guid? entityId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                string sqlString = $"select * from {tableName} where {tableName}Id = @entityId";

                var paras = new DynamicParameters();
                paras.Add("@entityId", entityId);

                var entity = sqlConnection.QueryFirstOrDefault<object>(sqlString, paras);

                return entity;
            }

            
        }

        public int Insert(T entity)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(T).Name;
                var listProperty = string.Empty;
                var listValue = string.Empty;

                var paras = new DynamicParameters();

                var props = typeof(T).GetProperties();
                foreach (var prop in props)
                {
                    

                    //Kiểm tra xem property hiện tại có NotMap k?
                    var notMapProp = prop.GetCustomAttributes(typeof(NotMap), true);

                    if(notMapProp.Length == 0)
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(entity);

                        var propType = prop.PropertyType; 
                        //Kiểm tra xem property hiện tại có phải PK k
                        var isPrimaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                        if (isPrimaryKey == true && propType == typeof(Guid))
                            propValue = Guid.NewGuid();

                        listProperty += $"{propName},";
                        listValue += $"@{propName},";

                        paras.Add($"@{propName}", propValue);
                    }
                }
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
                List<string> listProperty = new List<string>();
                List<string> listValue = new List<string>();

                var listProps = typeof(T).GetProperties();

                var option = string.Empty;
                var paras = new DynamicParameters();
                paras.Add("@entityId", entityId);
                foreach (var prop in listProps)
                {
                    var inor = prop.GetCustomAttributes(typeof(NotMap), true);
                    if(inor.Length == 0)
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(entity);
                        option += $"{propName} = @{propName},";
                        paras.Add($"@{propName}", propValue);
                    }
                    
                }

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
    }
}
