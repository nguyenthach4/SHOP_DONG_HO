using DongHoShop.Common.ViewModels;
using DongHoShop.Data.Infrastructure;
using DongHoShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHoShop.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<RevenuseStatisticViewModel> GetRevenuseStatistic(string fromDate, string toDate);
    }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<RevenuseStatisticViewModel> GetRevenuseStatistic(string fromDate, string toDate)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@fromDate",fromDate),
                 new SqlParameter("@toDate",toDate)
            };
            return DbContext.Database.SqlQuery<RevenuseStatisticViewModel>("GetRevenuseStatistic @fromDate,@toDate", parameters);
        }
    }
}
