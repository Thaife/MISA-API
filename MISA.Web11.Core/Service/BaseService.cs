using MISA.Web11.Core.Exceptions;
using MISA.Web11.Core.Interfaces.Infrastructure;
using MISA.Web11.Core.Interfaces.Service;
using MISA.Web11.Core.MISAAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Service
{
    public class BaseService<T> : IBaseService<T>
    {
        List<String> errLstMsgs = new List<string>();
        IBaseRepository<T> _baseRepository;

        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;   
        }
        public int? InsertService(T entity)
        {
            var isValid = ValidateObject(entity);
            if(isValid == true)
            {
                isValid = ValidateCustom(entity);
            }
            //Validate dữ liệu
            if (true)
            {
                //Thực hiện thêm mới dữ liệu
                var res = _baseRepository.Insert(entity);
                return res;
            }
            return null;
            
        }
        /// <summary>
        /// Hàm thực hiện validate riêng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool ValidateCustom(T entity)
        {
            return true;
        }

        /// <summary>
        /// Hàm thực hiện validate chung
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>true-dữ liệu hợp lệ; false-dữ liệu không hợp lệ</returns>
        /// Create by: Thai()

        public int? UpdateService(T entity, Guid entityId)
        {
            //Validate dữ liệu
            if (ValidateObject(entity) == true)
            {
                //Thực hiện thêm mới dữ liệu
                var res = _baseRepository.Update(entity, entityId);
                return res;
            }

            return null;
        }

        private bool ValidateObject(T entity)
        {
            bool isValid = true;
            // validate chung:

            // dữ liệu bắt buộc nhập
            var propNotEmpties = entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(NotEmpty)));
            foreach (var prop in propNotEmpties)
            {   
                var propValue = prop.GetValue(entity);
                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                    errLstMsgs.Add($"Thông tin {prop.Name} không được phép để trống");
            }
            /*
            // check dữ liệu có trùng lặp hay không
            var propNotDuplicate = entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(NotDuplicate)));
            foreach (var prop in propNotDuplicate)
            {
                //Truy cập database kiểm tra
                var isDuplicate = _baseRepository.CheckDuplicate(prop.Name, prop.GetValue(entity).ToString());
                if(isDuplicate)
                {
                    errLstMsgs.Add($"Thông tin {prop.Name} không được phép trùng");
                }
            }
            */

            // check định dạng dữ liệu (Email...)

            if (errLstMsgs.Count > 0)
            {
                isValid = false;
                var res = new
                {
                    userMsg = Properties.Resources.ValidateErrMsg,
                    data = errLstMsgs
                };
                throw new MISAValidateException(res);
            }

            return isValid;
        }
    }
}
