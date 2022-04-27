using MISA.Web11.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository:IBaseRepository<Employee>
    {
        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns></returns>
        /// Created by: Thai(19/3/2022)
        public string GetNewEmployeeCode();

        /// <summary>
        /// Lấy toàn bộ nhân viên
        /// </summary>
        /// <returns></returns>
        /// Created by: Thai(19/3/2022)
        public IEnumerable<Employee> GetEmployees();

        /// <summary>
        /// Lấy nhân viên theo pagin
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="PageNumber"></param>
        /// <param name="TextSearch"></param>
        /// <returns></returns>
        /// Created by: Thai(19/3/2022)
        public IEnumerable<Employee> GetEmployeePaging(int PageSize, int PageNumber, string? TextSearch);


        /// <summary>
        /// Lấy nhân viên theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: Thai(19/3/2022)
        public object GetEmployeeById(Guid id);

        /// <summary>
        /// Lấy tổng số bản ghi
        /// </summary>
        /// <returns></returns>
        /// Created by: Thai(19/3/2022)
        public int GetTotalRecord();

        /// <summary>
        /// Lấy tổng số bản ghi paging
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created by: Thai(19/3/2022)
        public int GetTotalRecordSearch(string textSearch);

        
        public int DeleteMultiEmployeeByIds(List<Guid>listId);
    }
}
