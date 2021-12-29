using Dapper;
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
    public class CustomerRepository : ICustomerRepository
    {
        public int Delete(Guid customerId)
        {
            ///1. Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            var sqlString = $"delete from Customer where CustomerId = '{customerId.ToString()}'";

            var res = sqlConnection.Query<object>(sqlString);

            return res != null ? 1 : 0;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            ///1. Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //2. Thực hiện lấy dữ liệu trong database
            var sqlString = "select * from Customer";
            var customers = sqlConnection.Query<Customer>(sqlString);

            return customers;
        }

        public object GetCustomerById(Guid customerId)
        {
            ///1. Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //2. Thực hiện lấy dữ liệu trong database
            var sqlString = "select * from Customer where CustomerId = @customerId";
            DynamicParameters paras = new DynamicParameters();
            paras.Add("@customerId", customerId.ToString());
            var customer = sqlConnection.QueryFirstOrDefault<Customer>(sqlString, paras);

            return customer;
        }

        public int Insert(Customer customer)
        {
            // Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Sinh ID mới cho customer
            customer.CustomerId = Guid.NewGuid();

            //sau khi validate tất cả dữ liệu => bắt đầu post
            string sqlString = "insert into Customer values(" +
                "@CustomerId, " +
                "@CustomerCode, " +
                "@FullName, " +
                "@DateOfBirth, " +
                "@Gender, " +
                "@Email, " +
                "@PhoneNumber, " +
                "@Address" +
                ")";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@CustomerId", customer.CustomerId.ToString());
            parameters.Add("@CustomerCode", customer.CustomerCode);
            parameters.Add("@FullName", customer.FullName);
            parameters.Add("@DateOfBirth", customer.DateOfBirth);
            parameters.Add("@Gender", customer.Gender);
            parameters.Add("@Email", customer.Email);
            parameters.Add("@PhoneNumber", customer.PhoneNumber);
            parameters.Add("@Address", customer.Address);

            var res = sqlConnection.Execute(sqlString, param: parameters);

            return res;
        }

        public int Update(Customer customer, Guid customerId)
        {
            // Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Tiến hành update dữ liệu
            var sqlString = "Update Customer set " +
                "CustomerCode = @customerCode, " +
                "FullName = @fullName, " +
                "DateOfBirth = @dateOfBirth, " +
                "Gender = @gender, " +
                "Email = @email, " +
                "PhoneNumber = @phoneNumber, " +
                "Address = @address " +
                "where CustomerId = @customerId";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@customerId", customerId.ToString());
            parameters.Add("@customerCode", customer.CustomerCode);
            parameters.Add("@fullName", customer.FullName);
            parameters.Add("@dateOfBirth", customer.DateOfBirth);
            parameters.Add("@gender", customer.Gender);
            parameters.Add("@email", customer.Email);
            parameters.Add("@phoneNumber", customer.PhoneNumber);
            parameters.Add("@address", customer.Address);

            var res = sqlConnection.Execute(sqlString, parameters);
            return res;
        }

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


        /// <summary>
        /// Hàm chỉ xóa
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// Create by : Thai
        public bool checkCustomerCodeDuplicate_NotCurrentCode(Guid customerId, string customerCode)
        {
            // Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Lấy mã khách hàng cũ
            string getOldCodestring = $"select CustomerCode from Customer where CustomerId = '{customerId.ToString()}'";
            var oldCustomerCode = sqlConnection.QueryFirstOrDefault<string>(getOldCodestring);

            //Nếu mã khách hàng thay đổi, kiểm tra xem đã tồn tại chưa
            string existCodeString = $"select * from Customer where CustomerCode = @customerCode and NOT(CustomerCode = '{oldCustomerCode}')";

            DynamicParameters paras = new DynamicParameters();
            paras.Add("@customerCode", customerCode);

            var res = sqlConnection.QueryFirstOrDefault<object>(existCodeString, paras);

            if(res != null)
            {
                return true;
            }
            return false;
        }
    }
}
