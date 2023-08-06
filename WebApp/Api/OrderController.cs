using AutoMapper;
using DongHoShop.Model.Models;
using DongHoShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebApp.Infrastructure.Core;
using WebApp.Models;

namespace WebApp.Api
{
    [RoutePrefix("api/order")]
    public class OrderController : ApiControllerBase
    {
        private IErrorService _errorService;
        private IOrderService _orderService;
        private IProductService _productService;

        public OrderController(IErrorService errorService, IOrderService orderService, IProductService productService) : base(errorService)
        {
            this._errorService = errorService;
            this._orderService = orderService;
            this._productService = productService;
        }

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int orderId, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll(keyword, orderId);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query.AsEnumerable());

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }
        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldOrder = _orderService.Delete(id);
                    _orderService.Save();

                    var responseData = Mapper.Map<Order, OrderViewModel>(oldOrder);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedOrders)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listOrder = new JavaScriptSerializer().Deserialize<List<int>>(checkedOrders);
                    foreach (var item in listOrder)
                    {
                        _orderService.Delete(item);
                    }

                    _orderService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listOrder.Count);
                }

                return response;
            });
        }
        [HttpPost]
        [Route("create")]
        public HttpResponseMessage Create(HttpRequestMessage request, OrderViewModel orderVm)
        {
            if (ModelState.IsValid)
            {
                var newOrder = new Order();
              
                try
                {
                    var listOrderDetails = new List<OrderDetail>();
                    foreach (var item in orderVm.OrderDetails)
                    {
                        var productPrice = _productService.GetById(item.ProductID).Price;
                        listOrderDetails.Add(new OrderDetail()
                        {
                            
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            Price = productPrice
                          
                        });
                    }
                    newOrder.OrderDetails = listOrderDetails;
                    var result = _orderService.Create(newOrder);
                    var model = Mapper.Map<Order, OrderViewModel>(result);
                    return request.CreateResponse(HttpStatusCode.OK, model);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

    }
}