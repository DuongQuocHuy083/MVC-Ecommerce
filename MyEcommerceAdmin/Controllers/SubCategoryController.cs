using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    public class SubCategoryController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: SubCategory/Index
        public ActionResult Index()
        {
            return View(); // Trả về view mặc định cho action Index (chưa được triển khai)
        }

        // CREATE: SubCategory/Create
        public ActionResult Create()
        {
            // Chuẩn bị danh sách các danh mục cha để hiển thị trong dropdownlist
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            return View(); // Trả về view để nhập thông tin mới cho SubCategory
        }

        [HttpPost]
        // Xử lý khi người dùng thêm mới SubCategory
        public ActionResult Create(SubCategory sctg)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(sctg); // Thêm SubCategory vào cơ sở dữ liệu
                db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                return PartialView("_Success"); // Trả về view thông báo thành công
            }
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            return PartialView("_Error"); // Nếu dữ liệu không hợp lệ, trả về view thông báo lỗi
        }
    }
}
