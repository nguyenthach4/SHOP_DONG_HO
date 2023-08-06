using AutoMapper;
using DongHoShop.Common;
using DongHoShop.Model.Models;
using DongHoShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;
        ISystemConfigService _systemConfigService;
        public HomeController(IProductCategoryService productCategoryService, ISystemConfigService systemConfigService, ICommonService commonService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
            _systemConfigService = systemConfigService;
        }
        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewMoel = new HomeViewModel();
            homeViewMoel.Slides = slideView;

            var lastestProductModel = _productService.GetLastest(3);
            var hotProductModel = _productService.GetHotProduct(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var hotProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(hotProductModel);
            homeViewMoel.LastesProduct = lastestProductViewModel;
            homeViewMoel.TopSaleProduct = hotProductViewModel;
            try
            {
                homeViewMoel.Title = _systemConfigService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
                homeViewMoel.MetaKeyword = _systemConfigService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
                homeViewMoel.MetaDescription = _systemConfigService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
            }
            catch
            {
                throw;
            }
            
            return View(homeViewMoel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

    }
}