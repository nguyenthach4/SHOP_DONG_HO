using DongHoShop.Common.ViewModels;
using DongHoShop.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DongHoShop.Service
{
    public interface IStatisticService
    {
        IEnumerable<RevenuseStatisticViewModel> GetRevenuseStatistic(string fromDate, string toDate);
    }
    public class StatisticService : IStatisticService
    {
        IOrderRepository _orderRepository;
        public StatisticService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IEnumerable<RevenuseStatisticViewModel> GetRevenuseStatistic(string fromDate, string toDate)
        {
            return _orderRepository.GetRevenuseStatistic(fromDate, toDate);
        }
    }
}
