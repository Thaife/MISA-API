using Misa.Ex.Core.Entity;
using MISA.Web11.Core.Entities;
using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using MISA.Web11.Core.MISAAttribute;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Service
{
    public class TeacherService : BaseService<Teacher>, ITeacherService
    {
        #region field
        ITeacherRepository _teacherRepository;
        ISubjectRepository _subjectRepository;
        ISubjectAssistantRepository _subjectAssistantRepository;
        IDepartmentAssistantRepository _departmentAssistantRepository;
        IDepartmentRepository _departmentRepository;
        ISubjectGroupRepository _subjectGroupRepository;

        IBaseRepository<Teacher> _baseRepository;
        List<Object> errLstMsgs = new List<Object>();

        #endregion

        #region constructor
        public TeacherService(
            ITeacherRepository _teacherRepository,
            ISubjectRepository _subjectRepository,
            IDepartmentRepository _departmentRepository,
            ISubjectGroupRepository _subjectGroupRepository,
            ISubjectAssistantRepository _subjectAssistantRepository,
            IDepartmentAssistantRepository _departmentAssistantRepository, 
            IBaseRepository<Teacher> _baseRepository
        ) : base(_teacherRepository)
        {
            this._teacherRepository = _teacherRepository;
            this._subjectRepository = _subjectRepository;
            this._departmentRepository = _departmentRepository;
            this._subjectGroupRepository = _subjectGroupRepository;
            this._departmentAssistantRepository = _departmentAssistantRepository;
            this._subjectAssistantRepository = _subjectAssistantRepository;
            this._baseRepository = _baseRepository;
        }
        #endregion

        #region method


        public int InsertTeacherService(TeacherCustom teacherCustom)
        {
            //validate trước khi thêm
            ValidateDuplicateCode(teacherCustom);
            ValidateObject(teacherCustom);
            return _teacherRepository.InsertTeacherRepository(teacherCustom);
        }
        public int DeleteTeacherFullById(Guid teacherId)
        {
            var l = _subjectAssistantRepository.DeleteSubjectAssistantByTeacherId(teacherId);
            var s = _departmentAssistantRepository.DeleteDepartmentAssistantByTeacherId(teacherId);
            var f = _teacherRepository.Delete(teacherId);

            return f + s + l;
        }

        public int DeleteMultiTeacherFullByIds(List<Guid> listTeacherId)
        {
            var l = _subjectAssistantRepository.DeleteMultiSubjectAssistantByTeacherIds(listTeacherId);
            var s = _departmentAssistantRepository.DeleteMultiDepartmentAssistantByTeacherIds(listTeacherId);
            var f = _teacherRepository.DeleteMultiTeacherByTeacherIds(listTeacherId);

            return f + s + l;
        }
        public int UpdateTeacherService(TeacherCustom teacherCustom, Guid teacherId)
        {
            //validate trước khi sửa
            ValidateObject(teacherCustom);
            return _teacherRepository.UpdateTeacherRepository(teacherCustom, teacherId);
        }

        public void ValidateDuplicateCode(TeacherCustom teacherCustom)
        {
            var codeProps = typeof(TeacherCustom).GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Code)));
            //biến lưu trạng thái validate: true-trùng
            bool isDuplicate = false;
            //biến lưu tên prop
            var propName = String.Empty;

            if (codeProps is not null)
            {
                foreach (var prop in codeProps)
                {
                    var propValue = prop.GetValue(teacherCustom);
                    propName = prop.Name;
                    isDuplicate = _baseRepository.CheckDuplicate(propName, propValue.ToString());
                }
            }
            //Nếu bị trùng code => add vào list err
            if (isDuplicate)
            {
                errLstMsgs.Add(new
                {
                    field = propName,
                    mess = Properties.Resources.ValidateTeacherCodeMess
                });
            }
        }

        public void  ValidateObject(TeacherCustom teacherCustom)
        {
            bool isValid = true;

            // Dữ liệu bắt buộc nhập
            var notEmptyProps = teacherCustom.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(NotEmpty)));
            if(notEmptyProps is not null)
            {
                foreach (var prop in notEmptyProps)
                {
                    var propValue = prop.GetValue(teacherCustom);
                    //Nếu trường này null hoặc bỏ trống => add vào list err
                    if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                        errLstMsgs.Add(new
                        {
                            field = prop.Name,
                            mess = Properties.Resources.ValidateNotEmptyMess
                        });
                }
            }
            

            // Dữ liệu là Alphabe
            var alphabetProps = teacherCustom.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof (Alphabet)));
            if(alphabetProps is not null)
            {
                var regexAlphabet = new Regex(@"[0-9.-]");
                foreach (var prop in alphabetProps)
                {
                    var propValue = prop.GetValue(teacherCustom);
                    //Nếu người dùng nhập kí tự khác Alphabet => add vào list err
                    if (propValue is not null && regexAlphabet.IsMatch(propValue.ToString())) {
                        errLstMsgs.Add(new
                        {
                            field = prop.Name,
                            mess = $"Trường này chỉ được nhập ký tự Alphabet"
                        });
                    }
                }
            }

            // Dữ liệu không trùng lặp
            //var notDuplicateProps = teacherCustom.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(NotDuplicate)));
            //foreach (var prop in notDuplicateProps)
            //{
            //    //Truy cập database kiểm tra, nếu propValue đã tồn tại => add vào list err
            //    var isDuplicate = _baseRepository.CheckDuplicate(prop.Name, prop.GetValue(teacherCustom).ToString());
            //    if (isDuplicate)
            //    {
            //        errLstMsgs.Add(new
            //        {
            //            field = prop.Name,
            //            mess = $"Trường này không được phép trùng"

            //        });
            //    }
            //}

            // dữ liệu là Email
            var emailProps = teacherCustom.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Email)));
            if (alphabetProps is not null)
            {
                var regexEmail = new Regex(@"^\w+([.-]?\w+)*@\w+([.-]?\w+)*(.\w{2,3})+$");
                foreach (var prop in emailProps)
                {
                    var propValue = prop.GetValue(teacherCustom);
                    //Nếu người dùng nhập Email không hợp lệ => add vào list err
                    if (propValue is not null && !regexEmail.IsMatch(propValue.ToString()))
                    {
                        errLstMsgs.Add(new
                        {
                            field = prop.Name,
                            mess = Properties.Resources.ValidateEmailMess
                        });
                    }
                }
            }

            //Dữ liệu là số điện thoại đầu mạng Việt Nam
            var phoneNumberVNProps = teacherCustom.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PhoneNumber)));
            if (phoneNumberVNProps is not null)
            {
                var regexPhoneNumber = new Regex(@"(84|0[3|5|7|8|9])+([0-9]{8}$)");
                foreach (var prop in phoneNumberVNProps)
                {
                    var propValue = prop.GetValue(teacherCustom);
                    //Nếu người dùng nhập số điện thoại không hợp lệ => add vào list err
                    if (propValue is not null && !regexPhoneNumber.IsMatch(propValue.ToString()))
                    {
                        errLstMsgs.Add(new
                        {
                            field = prop.Name,
                            mess = Properties.Resources.ValidatePhoneNMess
                        });
                    }
                }
            }

            //Dữ liệu Date không được lớn hơn ngày hiện tại
            var dateProps = teacherCustom.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(Date)));
            if (dateProps is not null)
            {
                foreach (var prop in dateProps)
                {
                    var propValue = prop.GetValue(teacherCustom);
                    //Nếu người dùng nhập số điện thoại không hợp lệ => add vào list err
                    if (propValue is not null)
                    {
                        string[] arr = (propValue as String).Split("-");
                        int y = Int32.Parse(arr[0]);
                        int m = Int32.Parse(arr[1]);
                        int d = Int32.Parse(arr[2]);
                        DateTime propDate = new DateTime(y, m, d);
                        DateTime localDate = DateTime.Now;
                        if(propDate > localDate)
                        {
                            errLstMsgs.Add(new
                            {
                                field = prop.Name,
                                mess = Properties.Resources.ValidateDateMess
                            });
                        }
                        
                    }
                }
            }

            //Kiểm tra nếu có lỗi thì throw danh sách lỗi
            if (errLstMsgs.Count > 0)
            {
                isValid = false;
                var res = new
                {
                    userMsg = Properties.Resources.ValidateErrMsg,
                    errlst = errLstMsgs
                };
                throw new MISAValidateException(res);
            }

        }

        public Stream ExportExcel()
        {
            //Lấy tất cả dữ liệu
            var data = _teacherRepository.GetTeacherFulls().ToList<TeacherCustom>();

            //tạo bộ nhớ stream để đọc file trên RAM
            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using var package = new ExcelPackage(stream);

            //tên bảng
            var workSheet = package.Workbook.Worksheets.Add(Properties.Resources.Excel_Title_Teacher);

            //căn chỉnh 
            workSheet.Column(1).Width = 5;//STT
            workSheet.Column(2).Width = 15;//Số hiệu cán bộ
            workSheet.Column(3).Width = 30;//Họ và tên
            workSheet.Column(4).Width = 15;//Số điện thoại
            workSheet.Column(5).Width = 20;//Tổ chuyên môn
            workSheet.Column(6).Width = 30;//Quản lý thiết bị môn
            workSheet.Column(7).Width = 40;//Quản lý kho phòng
            workSheet.Column(8).Width = 16;//Đào tạo QLTB
            workSheet.Column(9).Width = 16;//Đang làm việc

            //set giá trị từ A1 đến I1
            using (var range = workSheet.Cells["A1:I1"])
            {
                range.Merge = true; // hợp nhất
                range.Value = Properties.Resources.Excel_Title_Teacher; //set giá trị
                range.Style.Font.Bold = true;// in đậm
                range.Style.Font.Size = 20;
                range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center; //căn giữa
            }

            //header
            workSheet.Cells[3, 1].Value = Properties.Resources.Excel_col_1;
            workSheet.Cells[3, 2].Value = Properties.Resources.Excel_col_2;
            workSheet.Cells[3, 3].Value = Properties.Resources.Excel_col_3;
            workSheet.Cells[3, 4].Value = Properties.Resources.Excel_col_4;
            workSheet.Cells[3, 5].Value = Properties.Resources.Excel_col_5;
            workSheet.Cells[3, 6].Value = Properties.Resources.Excel_col_6;
            workSheet.Cells[3, 7].Value = Properties.Resources.Excel_col_7;
            workSheet.Cells[3, 8].Value = Properties.Resources.Excel_col_8;
            workSheet.Cells[3, 9].Value = Properties.Resources.Excel_col_9;

            //style header
            using (var range = workSheet.Cells["A3:I3"])
            {
                range.Style.Fill.PatternType = ExcelFillStyle.Solid; // phủ kín background
                range.Style.Fill.BackgroundColor.SetColor(Color.LightGray); //set nền
                range.Style.Font.Bold = true; // in đậm text
                range.Style.Border.BorderAround(ExcelBorderStyle.Thin); // viền xung quanh
                range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa cell
            }

            // đổ dữ liệu vào
            for (int i = 0; i < data.Count(); i++)
            {


                workSheet.Cells[i + 4, 1].Value = i + 1;
                workSheet.Cells[i + 4, 2].Value = data[i].TeacherCode;
                workSheet.Cells[i + 4, 3].Value = data[i].FullName;
                workSheet.Cells[i + 4, 4].Value = data[i].PhoneNumber;
                workSheet.Cells[i + 4, 5].Value = data[i].SubjectGroupName;
                workSheet.Cells[i + 4, 6].Value = data[i].SubjectNames;
                workSheet.Cells[i + 4, 7].Value = data[i].DepartmentNames;
                workSheet.Cells[i + 4, 8].Value = data[i].EquipmentManagement > 0 ? "x" : "";
                workSheet.Cells[i + 4, 9].Value = data[i].WorkStatus > 0 ? "x" : "";

                //Căn giữa: Số thứ tự, Quản lý đào tạo, Tình trạng làm việc
                workSheet.Cells[i + 4, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa cell
                workSheet.Cells[i + 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa cell
                workSheet.Cells[i + 4, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // căn giữa cell


                //viền cho tất cả 9 cột
                using (var range = workSheet.Cells[i + 4, 1, i + 4, 9])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin); 
                }
            }

            // lưu
            package.Save();
            //reset stream
            stream.Position = 0;
            //trả ra kết quả
            return package.Stream;
        }



        #endregion
    }
}
