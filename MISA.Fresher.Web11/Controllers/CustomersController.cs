using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Fresher.Web11.Model;
using MySqlConnector;
using Dapper;
using System.Data;

namespace MISA.Fresher.Web11.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        //Test commit git
        [HttpGet()]
        public ActionResult Get()
        {
            try
            {
                //1. Khai báo thông tin database và Khởi tạo kết nối tới database
                var connectionString = "Server = 47.241.69.179;" +
                    "Port = 3306;" +
                    "Database = MISA.WEB11.TVThai;" +
                    "User Id = dev;" +
                    "Password = manhmisa;";
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //2. Thực hiện lấy dữ liệu trong database
                var sqlString = "select * from Customer";
                var customers = sqlConnection.Query<object>(sqlString);

                //3.Trả dữ liệu về cho client
                //- Nếu có dữ liệu thì trả về mã 200 (kèm dữ liệu)
                //- Nếu không có dữ liệu trả về 204 (k dữ liệu)
                if (customers != null)
                    return StatusCode(200, customers);
                else
                    return StatusCode(204);
            }
            catch (Exception ex)
            {
                var result = new
                {
                    devMsg = ex.Message,
                    UserMsg = "Có lỗi xảy ra, vui lòng liên hệ với MISA",
                    data = ""
                };

                return StatusCode(500, result);
            }

        }

        [HttpGet("{customerId}")]
        public ActionResult GetById(string customerId)
        {
            try
            {
                //1. Khai báo thông tin database và Khởi tạo kết nối tới database
                var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //2.Thực hiện lấy dữ liệu trong database
                string sqlString = $"Select * from Customer where CustomerId = @CustomerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customerId);
                var customer = sqlConnection.QueryFirstOrDefault<object>(sqlString, param: parameters);

                //3.Nếu id khách hàng không tồn tại thì đưa ra thông báo, có thì trả dữ liệu
                if(customer == null)
                {
                    var res = new
                    {
                        devMsg = "",
                        userMsg = "ID khách hàng không tồn tại",
                        data = new
                        {
                            CustomerId = customerId
                        }
                    };

                    return StatusCode(400, res);
                }
                return StatusCode(200, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Post(Customer customer)
        {
            try
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

                //List lưu các thông tin lỗi
                List<string> errsList = new List<string>();

                //Kiểm tra mã khách hàng(không để trống, không trùng)
                var customerCode = customer.CustomerCode;
                if (string.IsNullOrEmpty(customerCode))
                {
                    errsList.Add("Mã khách hàng không được phép để trống");
                }
                else
                {
                    //Thực hiện tìm xem có khách hàng nào trùng mã với this.Mã không
                    string sqlCheck = "select * from Customer where CustomerCode = @customerCode";
                    DynamicParameters parametersCheck = new DynamicParameters();
                    parametersCheck.Add("@customerCode", customerCode);

                    var resCheck = sqlConnection.QueryFirstOrDefault<object>(sqlCheck, parametersCheck);

                    //Nếu trùng đưa ra thông báo
                    if (resCheck != null)
                    {
                        errsList.Add("Mã khách hàng đã tồn tại !");
                    }
                }


                //Kiểm tra tên khách hàng(không để trống)
                var fullName = customer.FullName;
                if (string.IsNullOrEmpty(fullName))
                {
                    errsList.Add("Tên khách hàng không được phép để trống");
                }



                //Return các lỗi nếu có
                if (errsList.Count > 0)
                {
                    var result = new
                    {
                        devMsg = "",
                        userMsg = "Dữ liệu đầu vào không hợp lệ, vui lòng kiểm tra lại dữ liệu",
                        data = errsList
                    };

                    return StatusCode(400, result);
                }

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



                if (res > 0)
                {
                    return StatusCode(201, res);

                }
                else
                {
                    return StatusCode(200, res);
                }

            }
            catch (Exception ex)
            {
                var res = new
                {
                    devMsg = ex.Message,
                    userMsg = "Có lỗi, vui lòng liên hệ MISA để được trợ giúp"
                };

                return StatusCode(500, res);
            }
        }

        [HttpPut("{customerId}")]
        public ActionResult Put(Customer customer, string customerId)
        {
            try
            {
                // Khai báo thông tin database và Khởi tạo kết nối tới database
                var connectionString = "" +
                    "Server = 47.241.69.179;" +
                    "Port = 3306;" +
                    "Database = MISA.WEB11.TVThai;" +
                    "User Id = dev;" +
                    "Password = manhmisa;";
                MySqlConnection sqlConnection = new MySqlConnection(connectionString);

                //Kiểm tra customerId có tồn tại trong database không
                var CustomerId = customerId;
                string existIdString = "select * from Customer where CustomerId = @customerId";
                DynamicParameters existParamesters = new DynamicParameters();
                existParamesters.Add("@customerId", CustomerId);
                var resQuery = sqlConnection.QueryFirstOrDefault<object>(existIdString, existParamesters);

                //Nếu không tồn tại => đưa ra thông báo
                if(resQuery == null)
                {
                    var res = new
                    {
                        devMsg = "customerId not exist",
                        userMsg = "ID khách hàng không tồn tại",
                        data = new
                        {
                            CustomerId = CustomerId
                        }
                    };
                    return StatusCode(400, res);
                } else
                {
                    List<string> errsList = new List<string>();

                    //Lấy mã khách hàng cũ
                    string getOldCodestring = $"select CustomerCode from Customer where CustomerId = '{customerId}'";
                    var oldCustomerCode = sqlConnection.QueryFirstOrDefault<string>(getOldCodestring);

                    //Nếu mã khách hàng thay đổi, kiểm tra xem đã tồn tại chưa
                    string existCodeString = $"select * from Customer where CustomerCode = @customerCode and NOT(CustomerCode = '{oldCustomerCode}')";

                    DynamicParameters paras = new DynamicParameters();
                    paras.Add("@customerCode", customer.CustomerCode);

                    var res = sqlConnection.QueryFirstOrDefault<object>(existCodeString, paras);

                    //Nếu tồn tại rồi => đưa ra thông báo
                    if (res != null)
                    {
                        errsList.Add("Mã khách hàng đã tồn tại");
                    }

                    //Kiểm tra mã khách hàng k được để trống
                    if(customer.FullName == "")
                    {
                        errsList.Add("Tên khách hàng không được để trống");
                    }


                    //Nếu có lỗi trong quá trình validate dữ liệu => đưa ra thông báo
                    if(errsList.Count > 0)
                    {
                        var result = new
                        {
                            userMsg = "Dữ liệu đầu vào không hợp lệ, vui lòng kiểm tra lại dữ liệu",
                            data = errsList
                        };
                        return StatusCode(400, result);
                    }
                }

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
                parameters.Add("@customerId", CustomerId);
                parameters.Add("@customerCode", customer.CustomerCode);
                parameters.Add("@fullName", customer.FullName);
                parameters.Add("@dateOfBirth", customer.DateOfBirth);
                parameters.Add("@gender", customer.Gender);
                parameters.Add("@email", customer.Email);
                parameters.Add("@phoneNumber", customer.PhoneNumber);
                parameters.Add("@address", customer.Address);

                sqlConnection.Execute(sqlString, parameters);

                return StatusCode(200, customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{customerId}")]
        public ActionResult Delete(string customerId)
        {
            // Khai báo thông tin database và Khởi tạo kết nối tới database
            var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);

            //Kiểm tra customerId có tồn tại trong database không
            var CustomerId = customerId;
            string existIdString = "select * from Customer where CustomerId = @customerId";
            DynamicParameters existParamesters = new DynamicParameters();
            existParamesters.Add("@customerId", CustomerId);
            var resQuery = sqlConnection.QueryFirstOrDefault<object>(existIdString, existParamesters);

            //Nếu không tồn tại => đưa ra thông báo
            if (resQuery == null)
            {
                var res = new
                {
                    devMsg = "customerId not exist",
                    userMsg = "ID khách hàng không tồn tại",
                    data = new
                    {
                        CustomerId = CustomerId
                    }
                };
                return StatusCode(400, res);
            }
            //Nếu tồn tại => xóa khách hàng
            else
            {
                var sqlString = "delete from Customer where CustomerId = @customerId";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@customerId", customerId);

                sqlConnection.Execute(sqlString, parameters);

                return StatusCode(200);
            } 
                
        }


        //Xóa toàn bộ dữ liệu:: WARRING
        [HttpDelete("all")]
        public ActionResult DeleteAll()
        {
            var connectionString = "" +
                "Server = 47.241.69.179;" +
                "Port = 3306;" +
                "Database = MISA.WEB11.TVThai;" +
                "User Id = dev;" +
                "Password = manhmisa;";
            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            var sqlString = "delete from Customer";

            sqlConnection.Execute(sqlString);

            return StatusCode(200);
        }
    }
}
