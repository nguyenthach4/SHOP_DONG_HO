using AutoMapper;
using DongHoShop.Common;
using DongHoShop.Model.Models;
using DongHoShop.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApp.App_Start;
using WebApp.Infrastructure.Extensions;
using WebApp.Infrastructure.NganLuongAPI;
using WebApp.Models;
using PayPal.Api;

namespace WebApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;
        private IOrderService _orderService;
        private ApplicationUserManager _userManager;

        public ShoppingCartController(IProductService productService, IOrderService orderService, ApplicationUserManager userManager)
        {
            this._productService = productService;
            this._orderService = orderService;
            this._userManager = userManager;

        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            CheckShoppingCart();
            return View();
        }
        public ActionResult CreateOrder(string orderViewModel, string Cancel = null)
        {
            
            var order = new JavaScriptSerializer().Deserialize<OrderViewModel>(orderViewModel);
            var orderNew = new DongHoShop.Model.Models.Order();
            bool isEnough = true;
            orderNew.UpdateOrder(order);
            if (Request.IsAuthenticated)
            {
                orderNew.CustomerId = User.Identity.GetUserId();
                orderNew.CreatedBy = User.Identity.GetUserName();
            }
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in cart)
            {
                var detail = new OrderDetail();
                detail.ProductID = item.ProductId;
                detail.Quantity = item.Quantity;
                detail.Price = item.Product.Price;
                orderDetails.Add(detail);
                isEnough = _productService.SellProduct(item.ProductId, item.Quantity);
                break;
            }
            if (isEnough)
            {
                var orderReturn = _orderService.Create(ref orderNew, orderDetails);
                _productService.Save();
                if (orderReturn.PaymentMethod == "CASH")
                {
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    APIContext apiContext = PaypalConfiguration.GetAPIContext();

                    try
                    {
                        //A resource representing a Payer that funds a payment Payment Method as paypal
                        //Payer Id will be returned when payment proceeds or click to pay
                        string payerId = Request.Params["PayerID"];

                        if (string.IsNullOrEmpty(payerId))
                        {
                            //this section will be executed first because PayerID doesn't exist
                            //it is returned by the create function call of the payment class

                            // Creating a payment
                            // baseURL is the url on which paypal sendsback the data.
                            string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                        "/ShoppingCart/SuccessView?";

                            //here we are generating guid for storing the paymentID received in session
                            //which will be used in the payment execution

                            var guid = Convert.ToString((new Random()).Next(100000));

                            //CreatePayment function gives us the payment approval url
                            //on which payer is redirected for paypal account payment

                            var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                            //get links returned from paypal in response to Create function call

                            var links = createdPayment.links.GetEnumerator();

                            string paypalRedirectUrl = null;

                            while (links.MoveNext())
                            {
                                Links lnk = links.Current;

                                if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                                {
                                    //saving the payapalredirect URL to which user will be redirected for payment
                                    paypalRedirectUrl = lnk.href;
                                }
                            }

                            // saving the paymentID in the key guid
                            Session.Add(guid, createdPayment.id);

                            return Json(new
                            {
                                status = true,
                                urlCheckOut = paypalRedirectUrl,
                            });
                        }
                        else
                        {
                            // This function exectues after receving all parameters for the payment

                            var guid = Request.Params["guid"];

                            var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                            //If executed payment failed then we will show payment failure message to user
                            if (executedPayment.state.ToLower() != "approved")
                            {
                                return Json(new
                                {
                                    status = false,
                                    url = "/ShoppingCart/FailureView",
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json(new
                        {
                            status = false,
                            url = "/ShoppingCart/FailureView",
                        });
                    }

                    //on successful payment, show success page to user.
                    return  Json(new
                    {
                        status = true,
                        url = "/ShoppingCart/SuccessView",
                    });
                }


            }

            else
            {
                return Json(new
                {
                    status = false,
                    message = "Không đủ hàng !"
                });
            }

        }
      
        public ActionResult SuccessView()
        {
            return View();
        }
        public ActionResult FailureView()
        {
            return View();
        }
        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            var itemList = new ItemList() { items = new List<Item>() };
            List<ShoppingCartViewModel> listItens = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var item in listItens)
            {
                itemList.items.Add(new Item()
                {
                    name = item.Product.Name,
                    currency = "USD",
                    price = item.Product.Price.ToString(),
                    quantity = item.Quantity.ToString(),
                    sku = "sku"
                });
            }
            //Adding Item Details like name, currency, price etc


            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            // Adding Tax, shipping and Subtotal details
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = listItens.Sum(x => x.Quantity * x.Product.Price).ToString(),
            };

            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Huu Duy Test Transaction description.",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }
        public JsonResult GetAll()
        {
            CheckShoppingCart();
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];

            return Json(new
            {
                data = cart,
                count = cart.Count(),
                status = true,
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUser()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = _userManager.FindById(userId);
                return Json(new
                {
                    data = user,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            var product = _productService.GetById(productId);
            if (cart == null)
            {
                cart = new List<ShoppingCartViewModel>();
            }
            if (product.Quantity == 0)
            {
                return Json(new
                {
                    status = false,
                    message = "Sản phảm này đang hết hàng !"
                });
            }
            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var item in cart)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity += 1;
                    }
                }
            }
            else
            {
                ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                newItem.ProductId = productId;

                newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                newItem.Quantity = 1;
                cart.Add(newItem);
            }
            Session[CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true,
            });

        }
        [HttpPost]
        public JsonResult Update(string carData)
        {
            var cartViewModel = new JavaScriptSerializer().Deserialize<List<ShoppingCartViewModel>>(carData);
            var sessionCart = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            foreach (var itemCartVM in cartViewModel)
            {
                foreach (var itemSessionCart in sessionCart)
                {
                    if (itemCartVM.ProductId == itemSessionCart.ProductId)
                    {
                        itemCartVM.Quantity = itemSessionCart.Quantity;
                    }
                }
            }
            Session[CommonConstants.SessionCart] = sessionCart;
            return Json(new
            {
                status = true,
            });
        }
        [HttpPost]
        public JsonResult DeleteItem(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[CommonConstants.SessionCart];
            if (cartSession != null)
            {
                cartSession.RemoveAll(x => x.ProductId == productId);
                Session[CommonConstants.SessionCart] = cartSession;
                return Json(new
                {
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }
        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
            return Json(new
            {
                status = true
            });
        }
        private void CheckShoppingCart()
        {
            var cartSession = Session[CommonConstants.SessionCart];
            if (cartSession == null)
                Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
        }

        public ActionResult ConfirmOder()
        {
            return View();
        }
        public ActionResult CancelOrder()
        {
            return View();
        }




    }
}
