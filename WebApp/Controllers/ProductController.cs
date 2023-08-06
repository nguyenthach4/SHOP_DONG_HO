using AutoMapper;
using DongHoShop.Common;
using DongHoShop.Model.Models;
using DongHoShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApp.Infrastructure.Core;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int toalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out toalRow);
            var category = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            var totalPage = (int)Math.Ceiling((double)toalRow / pageSize);
            var paginationset = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = toalRow,
                TotalPages = totalPage
            };
            return View(paginationset);
        }
        public ActionResult Detail(int productId)
        {
            var productModel = _productService.GetById(productId);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(productModel);
            var reatedProduct = _productService.GetReatedProduct(productId, 6);
            ViewBag.ReatedProducts = Mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(reatedProduct);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.MoreImages = listImages;

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>,IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(productId));
            return View(productViewModel);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetLstProductByName(keyword);       
            return Json(new
            {
                data = model
            },JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int toalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out toalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            var totalPage = (int)Math.Ceiling((double)toalRow / pageSize);

            ViewBag.KeyWord = keyword;
            var paginationset = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = toalRow,
                TotalPages = totalPage
            };
            return View(paginationset);
        }
        public ActionResult ListByTag(string tagId, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int toalRow = 0;
            var productModel = _productService.GetListProductByTag(tagId, page, pageSize, out toalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            var totalPage = (int)Math.Ceiling((double)toalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag,TagViewModel>(_productService.GetTag(tagId));
            var paginationset = new PaginationSet<ProductViewModel>()
            {
                Items = productViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = toalRow,
                TotalPages = totalPage
            };
            return View(paginationset);
        }
    }
}