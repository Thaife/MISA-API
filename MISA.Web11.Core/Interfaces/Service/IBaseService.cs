﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Service
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Xử lý nghiệp vụ thêm mới dữ liệu
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Số bản ghi thêm mới thành công</returns>
        /// Created by : Thai (27/12/2021)
        public int? InsertService(T customerGroup);


        /// <summary>
        /// Xử lý nghiệp vụ sửa dữ liệu
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Số bản ghi thêm mới thành công</returns>
        /// Created by : Thai (28/12/2021)
        public int? UpdateService(T entity, Guid entityId);
    }
}