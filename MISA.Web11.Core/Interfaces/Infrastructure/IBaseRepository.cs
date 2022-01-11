using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web11.Core.Interfaces.Infrastructure
{
    public interface IBaseRepository<T>
    {
        public IEnumerable<T> Get();
        public object Get(Guid? entityId);

        public int Insert(T entity);

        public int Update(T entity, Guid entityId);

        public int Delete(Guid entityId);

        public int DeleteAll();

        /// <summary>
        /// Hàm kiểm tra có trùng giá trị property k
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns>true-trùng; false-không trùng/returns>
        bool CheckDuplicate(string propName, string propValue);
    }
}
