using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop_10.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }

        public ActionResult Category(long id)
        {
            var category = new CategoryDao().ViewDetail(id);
            return View(category);
        }

        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProducts = new ProductDao().ListRelatedProduct(id);
            return View(product);
        }
    }
}