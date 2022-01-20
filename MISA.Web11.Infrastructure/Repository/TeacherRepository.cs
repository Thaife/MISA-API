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
                int numberCode = Int32.Parse(temp[1]);
                string nextTeacherCode = numberCode < 9 ? "0" + (numberCode + 1) : numberCode + 1 + "";
                res = temp[0] + "-" + nextTeacherCode;


                return res;
            }
        }

        public IEnumerable<TeacherCustom> GetTeacherFulls()
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var sqlCommand = @"SELECT
                              T.*,
                              DAI.DepartmentNames,
                              SAI.SubjectNames,
                              SGI.SubjectGroupName
                            FROM Teacher T
                              LEFT JOIN (SELECT
                                  DA.TeacherId,
                                  GROUP_CONCAT(D.DepartmentName SEPARATOR ', ') AS DepartmentNames,
                                  GROUP_CONCAT(D.DepartmentId SEPARATOR ', ') AS DepartmentIds
                                FROM DepartmentAssistant DA
                                  JOIN Department D
                                    ON DA.DepartmentId = D.DepartmentId
                                WHERE D.DepartmentId IS NOT NULL
                                GROUP BY DA.TeacherId) AS DAI
                                ON DAI.TeacherId = T.TeacherId

                              LEFT JOIN (SELECT
                                  SA.TeacherId,
                                  GROUP_CONCAT(S.SubjectName SEPARATOR ', ') AS SubjectNames,
                                  GROUP_CONCAT(S.SubjectId SEPARATOR ', ') AS SubjectIds
                                FROM SubjectAssistant SA
                                  JOIN Subject S
                                    ON SA.SubjectId = S.SubjectId
                                WHERE S.SubjectId IS NOT NULL
                                GROUP BY SA.TeacherId) AS SAI
                                ON SAI.TeacherId = T.TeacherId

                            LEFT JOIN
                                (SELECT SG.SubjectGroupId, SG.SubjectGroupName
                                FROM SubjectGroup SG
                                WHERE SG.SubjectGroupId IS NOT NULL) AS SGI
                                ON SGI.SubjectGroupId = T.SubjectGroupId
                            ORDER BY T.TeacherCode DESC";

                var teachers = sqlConnection.Query<TeacherCustom>(sqlCommand);
                return teachers;
            }
        }

        public IEnumerable<Object> GetTeacherFull(int PageSize, int PageNumber, string TextSearch)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var fstRecord = (PageNumber - 1) * PageSize;
                DynamicParameters dynamic = new DynamicParameters();
                var whereCond = $" WHERE T.TeacherCode LIKE '%{TextSearch}%' OR T.FullName LIKE '%{TextSearch}%'";
                //Sql string join 3 bảng bảng lấy dữ liệu
                var sqlCommand = @"SELECT
                              T.*,
                              DAI.DepartmentNames,
                              DAI.DepartmentIds,
                              SAI.SubjectIds,
                              SAI.SubjectNames,
                              SGI.SubjectGroupName
                            FROM Teacher T
                              LEFT JOIN (SELECT
                                  DA.TeacherId,
                                  GROUP_CONCAT(D.DepartmentName SEPARATOR ', ') AS DepartmentNames,
                                  GROUP_CONCAT(D.DepartmentId SEPARATOR ', ') AS DepartmentIds
                                FROM DepartmentAssistant DA
                                  JOIN Department D
                                    ON DA.DepartmentId = D.DepartmentId
                                WHERE D.DepartmentId IS NOT NULL
                                GROUP BY DA.TeacherId) AS DAI
                                ON DAI.TeacherId = T.TeacherId

                              LEFT JOIN (SELECT
                                  SA.TeacherId,
                                  GROUP_CONCAT(S.SubjectName SEPARATOR ', ') AS SubjectNames,
                                  GROUP_CONCAT(S.SubjectId SEPARATOR ', ') AS SubjectIds
                                FROM SubjectAssistant SA
                                  JOIN Subject S
                                    ON SA.SubjectId = S.SubjectId
                                WHERE S.SubjectId IS NOT NULL
                                GROUP BY SA.TeacherId) AS SAI
                                ON SAI.TeacherId = T.TeacherId

                            LEFT JOIN
                                (SELECT SG.SubjectGroupId, SG.SubjectGroupName
                                FROM SubjectGroup SG
                                WHERE SG.SubjectGroupId IS NOT NULL) AS SGI
                                ON SGI.SubjectGroupId = T.SubjectGroupId 
                             
";
                //Nếu TextSearch tồn tại => add string tìm kiếm
                if (!string.IsNullOrEmpty(TextSearch))
                {
                    sqlCommand += whereCond;
                }
                //add string paging
                else
                {
                    sqlCommand += "ORDER BY T.TeacherCode DESC LIMIT @fstRecord,@pageSize";
                    dynamic.Add("@pageSize", PageSize);
                    dynamic.Add("@fstRecord", fstRecord);
                }


                var teachers = sqlConnection.Query<Object>(sqlCommand, param: dynamic);
                return teachers;
            }

        }

        public object GetTeacherFullById(Guid teacherId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //Sql string join 3 bảng bảng lấy dữ liệu
                var sqlCommand = @"SELECT
                              T.*,
                              DAI.DepartmentNames,
                              DAI.DepartmentIds,
                              SAI.SubjectIds,
                              SAI.SubjectNames,
                              SGI.SubjectGroupName
                            FROM Teacher T
                              LEFT JOIN (SELECT
                                  DA.TeacherId,
                                  GROUP_CONCAT(D.DepartmentName SEPARATOR ', ') AS DepartmentNames,
                                  GROUP_CONCAT(D.DepartmentId SEPARATOR ', ') AS DepartmentIds
                                FROM DepartmentAssistant DA
                                  JOIN Department D
                                    ON DA.DepartmentId = D.DepartmentId
                                WHERE D.DepartmentId IS NOT NULL
                                GROUP BY DA.TeacherId) AS DAI
                                ON DAI.TeacherId = T.TeacherId

                              LEFT JOIN (SELECT
                                  SA.TeacherId,
                                  GROUP_CONCAT(S.SubjectName SEPARATOR ', ') AS SubjectNames,
                                  GROUP_CONCAT(S.SubjectId SEPARATOR ', ') AS SubjectIds
                                FROM SubjectAssistant SA
                                  JOIN Subject S
                                    ON SA.SubjectId = S.SubjectId
                                WHERE S.SubjectId IS NOT NULL
                                GROUP BY SA.TeacherId) AS SAI
                                ON SAI.TeacherId = T.TeacherId

                            LEFT JOIN
                                (SELECT SG.SubjectGroupId, SG.SubjectGroupName
                                FROM SubjectGroup SG
                                WHERE SG.SubjectGroupId IS NOT NULL) AS SGI
                                ON SGI.SubjectGroupId = T.SubjectGroupId
                            Where T.TeacherId = @teacherId
                                            
";

                DynamicParameters paras = new DynamicParameters();
                paras.Add("@teacherId", teacherId);
                var teacher = sqlConnection.QueryFirstOrDefault<Object>(sqlCommand, param: paras);
                return teacher;
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
                //Khai báo 2 chuỗi sẽ dùng khi thực hiện insert
                var listColumnName = string.Empty;
                var listColumnParam = string.Empty;

                var parameters = new DynamicParameters();
                //Lấy list prop của TeacherCustom(bao gồm đầy đủ thông tin của teacher)
                var props = typeof(TeacherCustom).GetProperties();
                var teacherId = Guid.Empty;

                foreach (var prop in props)
                {
                    //Kiểm tra nếu prop hiện tại có attri [NotMap] thì bỏ qua
                    var ignore = prop.GetCustomAttributes(typeof(NotMap), true);
                    if (ignore.Length == 0)
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(teacherCustom);
                        // Kiểm tra, tạo mới cho khoá chính
                        var isPrimaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                        if (isPrimaryKey == true)
                        {
                            teacherId = Guid.NewGuid();
                            propValue = teacherId;
                        }

                        //add thêm giá trị vào 2 chuỗi sẽ xử dụng để insert
                        listColumnName += $"{propName},";
                        listColumnParam += $"@{propName},";
                        parameters.Add($"@{propName}", propValue?.ToString());
                    }
                }

                //Xóa kí tự thừa "," ở cuối 2 chuỗi
                listColumnName = listColumnName.Substring(0, listColumnName.Length - 1);
                listColumnParam = listColumnParam.Substring(0, listColumnParam.Length - 1);
                var sqlCommand = $"INSERT INTO Teacher ({listColumnName}) VALUES ({listColumnParam})";
                var fst = sqlConnection.Execute(sqlCommand, parameters);

                //gọi hàm insert các phòng ban và môn học mà giáo viên quản lý
                var mid = InsertMuiltDepartment(teacherId, teacherCustom.DepartmentIds);
                var lst = InsertMuiltSubject(teacherId, teacherCustom.SubjectIds);


                return fst + mid + lst;
            }
        }

        public int InsertMuiltDepartment(Guid teacherId, List<Guid> departmentIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //khai báo biến lưu trữ số phòng sẽ được insert
                var countExecute = 0;
                if (departmentIds != null)
                {
                    //Nếu có dữ liệu phòng ban, loop và insert
                    foreach (var departmentId in departmentIds)
                    {
                        var sqlCommad = "INSERT INTO DepartmentAssistant (DepartmentAssistantId,TeacherId,DepartmentId) VALUES (@departmentAssistantId,@teacherId, @departmentId)";
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@departmentAssistantId", Guid.NewGuid());
                        parameters.Add("@teacherId", teacherId);
                        parameters.Add("@departmentId", departmentId);
                        var result = sqlConnection.Execute(sqlCommad, param: parameters);
                        countExecute += result;
                    }
                }

                return countExecute;

            }

        }

        public int InsertMuiltSubject(Guid teacherId, List<Guid> subjectIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //khai báo biến lưu trữ số môn học sẽ được insert
                var countExecute = 0;
                if (subjectIds != null)
                {
                    //Nếu có dữ liệu môn học, loop và insert
                    foreach (var subjectId in subjectIds)
                    {
                        var sqlCommad = "INSERT INTO SubjectAssistant (SubjectAssistantId, TeacherId,SubjectId) VALUES (@subjectAssistantId, @teacherId,@subjectId)";
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@subjectAssistantId", Guid.NewGuid());
                        parameters.Add("@teacherId", teacherId);
                        parameters.Add("@subjectId", subjectId);
                        var result = sqlConnection.Execute(sqlCommad, param: parameters);
                        countExecute += result;
                    }
                }

                return countExecute;

            }

        }



        public int UpdateTeacherRepository(TeacherCustom teacherCustom, Guid teacherId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //Khai báo chuỗi dùng để thực hiện update
                var stringSqlSet = string.Empty;

                var paras = new DynamicParameters();
                var props = typeof(TeacherCustom).GetProperties();

                foreach (var prop in props)
                {
                    //Nếu prop hiện tại chứa [NotMap] => bỏ qua
                    var ignore = prop.GetCustomAttributes(typeof(NotMap), true);
                    if (ignore.Length == 0)
                    {
                        var propName = prop.Name;
                        var propValue = prop.GetValue(teacherCustom);
                        // Nếu prop hiện tại là khóa chính => bỏ qua
                        var isPrimaryKey = Attribute.IsDefined(prop, typeof(PrimaryKey));
                        if (isPrimaryKey == false)
                        {
                            //Add thông tin update
                            stringSqlSet += $"{propName}=@{propName},";
                        }
                        paras.Add($"@{propName}", propValue);
                    }
                }

                //Xóa kí tự thừa "," cuối chuỗi
                stringSqlSet = stringSqlSet.Substring(0, stringSqlSet.Length - 1);
                var sqlCommand = $"update Teacher set {stringSqlSet} where TeacherId = @teacherId";
                var fst = sqlConnection.Execute(sqlCommand, paras);

                //gọi hàm insert các phòng ban và môn học mà giáo viên quản lý
                var mid = UpdateMuiltDepartment(teacherId, teacherCustom.DepartmentIds);
                var lst = UpdateMuiltSubject(teacherId, teacherCustom.SubjectIds);

                return fst + mid + lst;

            }
        }

        public int UpdateMuiltDepartment(Guid teacherId, List<Guid> departmentIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //Xóa các phòng ban id đang quản lý để sau insert lại
                var deleteCommand = "DELETE FROM DepartmentAssistant WHERE TeacherId=@teacherId";
                var parameters = new DynamicParameters();
                parameters.Add("@teacherId", teacherId);
                sqlConnection.Execute(deleteCommand, parameters);

                var countExecute = 0;
                if (departmentIds != null)
                {
                    //Nếu cập nhật phòng(khác rỗng), loop qua và insert lại
                    foreach (var departmentId in departmentIds)
                    {
                        var sqlCommad = "INSERT INTO DepartmentAssistant (DepartmentAssistantId,TeacherId,DepartmentId) VALUES (@departmentAssistantId,@teacherId, @departmentId)";
                        parameters.Add("@departmentAssistantId", Guid.NewGuid());
                        parameters.Add("@departmentId", departmentId);
                        var result = sqlConnection.Execute(sqlCommad, param: parameters);
                        countExecute += result;
                    }
                }

                return countExecute;

            }

        }

        public int UpdateMuiltSubject(Guid teacherId, List<Guid> subjectIds)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                //Xóa các phòng ban id đang quản lý để sau insert lại
                var deleteCommand = "DELETE FROM SubjectAssistant WHERE TeacherId= @teacherId";
                var parameterDel = new DynamicParameters();
                parameterDel.Add("@teacherId", teacherId);
                sqlConnection.Execute(deleteCommand, parameterDel);


                var countExecute = 0;

                if (subjectIds != null)
                {
                    //Nếu cập nhật phòng(khác rỗng), loop qua và insert lại
                    foreach (var subjectId in subjectIds)
                    {
                        var sqlCommad = "INSERT INTO SubjectAssistant (SubjectAssistantId, TeacherId,SubjectId) VALUES (@subjectAssistantId, @teacherId,@subjectId)";
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@subjectAssistantId", Guid.NewGuid());
                        parameters.Add("@teacherId", teacherId);
                        parameters.Add("@subjectId", subjectId);
                        var result = sqlConnection.Execute(sqlCommad, param: parameters);
                        countExecute += result;
                    }
                }

                return countExecute;

            }

        }


        public int DeleteMultiTeacherByTeacherIds(List<Guid> listId)
        {
            using (sqlConnection = new MySqlConnection(connectionString))
            {
                var tableName = typeof(Teacher).Name;
                //Khai báo chuỗi làm điều kiện where
                var conditionString = string.Empty;
                var length = listId.Count;
                DynamicParameters paras = new DynamicParameters();
                //Loop danh sách id
                for (int i = 0; i < length; i++)
                {
                    //add id cần xóa vào chuỗi làm điều kiện where
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
