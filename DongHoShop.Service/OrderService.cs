using DongHoShop.Data.Infrastructure;
using DongHoShop.Data.Repositories;
using DongHoShop.Model.Models;
using System;
using System.Collections.Generic;

namespace DongHoShop.Service
{
    public interface IOrderService
    {
        Order Create(ref Order order, List<OrderDetail> orderDetails);
        Order Create(Order order);
        OrderDetail CreateDetail(OrderDetail order);
        IEnumerable<Order> GetAll();

        IEnumerable<Order> GetAll(string keyword, int orderId);
        Order Delete(int id);
        void UpdateStatus(int orderId);
        void Save();
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public Order Create(ref Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }

                return order;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Order Create(Order order)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                return order;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public OrderDetail CreateDetail(OrderDetail order)
        {
            return _orderDetailRepository.Add(order);
        }

        public Order Delete(int id)
        {
            return _orderRepository.Delete(id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public IEnumerable<Order> GetAll(string keyword, int orerId)
        {
            if (!string.IsNullOrEmpty(keyword) || orerId != 0)
            {
                return _orderRepository.GetMulti(x => x.CustomerName.Contains(keyword) || x.ID == orerId);
            }
            else
                return _orderRepository.GetAll();
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateStatus(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}