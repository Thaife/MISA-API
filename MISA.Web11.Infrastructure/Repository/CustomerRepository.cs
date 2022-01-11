using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration) { }

        public bool checkCustomerCodeDuplicate(string customerCode)
        {
            // Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Thực hiện tìm xem có khách hàng nào trùng mã với this.Mã không
            string sqlCheck = "select * from Customer where CustomerCode = @customerCode";
            DynamicParameters parametersCheck = new DynamicParameters();
            parametersCheck.Add("@customerCode", customerCode);

            var resCheck = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, parametersCheck);

            if(resCheck != null)
            {
                return true;
            }
            return false;
        }
    }
}
