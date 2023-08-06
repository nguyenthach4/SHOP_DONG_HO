using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DongHoShop.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Thêm mói
        T Add(T entity);

        // Cập nhật
        void Update(T entity);

        // Xóa
        T Delete(T entity);

        T Delete(int id);

        // Xóa nhiều
        void DeleteMulti(Expression<Func<T, bool>> where);

        // láy Id
        T GetSingleById(int id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll(string[] includes = null);

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);

        IEnumerable<T> GetMultiPaging(Expression<Func<T, bool>> filter, out int total, int index = 0, int size = 50, string[] includes = null);

        int Count(Expression<Func<T, bool>> where);

        bool CheckContains(Expression<Func<T, bool>> predicate);
    }
}
