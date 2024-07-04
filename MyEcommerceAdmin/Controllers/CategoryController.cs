using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    public class CategoryController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext(); 

        // GET: Category/Index
        // Hiển thị danh sách danh mục sản phẩm (chưa cài đặt trong mã)
        public ActionResult Index()
        {
            return View(); 
        }

        // GET: Category/Create
        // Hiển thị form tạo mới danh mục sản phẩm
        public ActionResult Create()
        {
            return View(); 
        }

        // POST: Category/Create
        // Xử lý tạo mới danh mục sản phẩm
        [HttpPost]
        public ActionResult Create(Category ctg)
        {
            if (ModelState.IsValid) 
            {
                db.Categories.Add(ctg); 
                db.SaveChanges(); 
                return PartialView("_Success");
            }
            return PartialView("_Error"); 
        }
    }
}
