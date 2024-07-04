using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    public class HomeController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: Home
        public ActionResult Index()
        {
           
            // Lấy danh sách sản phẩm theo từng danh mục và gán vào ViewBag
            ViewBag.MenProduct = db.Products.Where(x => x.Category.Name.Equals("Thuốc")).ToList();
            ViewBag.WomenProduct = db.Products.Where(x => x.Category.Name.Equals("Thực phẩm chức năng")).ToList();
            ViewBag.AccessoriesProduct = db.Products.Where(x => x.Category.Name.Equals("Mẹ và bé")).ToList();
            ViewBag.ElectronicsProduct = db.Products.Where(x => x.Category.Name.Equals("Thiết bị y tế")).ToList();

            var data = this.GetDefaultData(); // Lấy dữ liệu mặc định cho trang thanh toán
            return View(data);
        }

        // Phương thức để lấy dữ liệu mặc định khác
        
    }
}
