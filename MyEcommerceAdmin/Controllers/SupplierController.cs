using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    public class SupplierController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: Supplier/Index
        public ActionResult Index()
        {
            // Trả về view hiển thị danh sách tất cả các nhà cung cấp từ cơ sở dữ liệu
            return View(db.Suppliers.ToList());
        }

        // CREATE: Supplier/Create
        public ActionResult Create()
        {
            // Trả về view để nhập thông tin nhà cung cấp mới
            return View();
        }

        [HttpPost]
        // Xử lý khi người dùng thêm mới nhà cung cấp
        public ActionResult Create(Supplier suplr)
        {
            if (ModelState.IsValid)
            {
                // Nếu dữ liệu nhập vào hợp lệ, thêm nhà cung cấp vào cơ sở dữ liệu và lưu thay đổi
                db.Suppliers.Add(suplr);
                db.SaveChanges();
                return PartialView("_Success"); // Trả về view thông báo thành công
            }
            return PartialView("_Error"); // Nếu dữ liệu không hợp lệ, trả về view thông báo lỗi
        }

        // EDIT: Supplier/Edit/{id}
        public ActionResult Edit(int id)
        {
            // Tìm nhà cung cấp cần chỉnh sửa từ cơ sở dữ liệu
            Supplier suplr = db.Suppliers.Single(x => x.SupplierID == id);
            if (suplr == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy nhà cung cấp, trả về lỗi 404
            }
            return View(suplr); // Trả về view để chỉnh sửa thông tin nhà cung cấp
        }

        [HttpPost]
        // Xử lý khi người dùng lưu thông tin chỉnh sửa nhà cung cấp
        public ActionResult Edit(Supplier suplr)
        {
            if (ModelState.IsValid)
            {
                // Nếu dữ liệu nhập vào hợp lệ, cập nhật thông tin nhà cung cấp và lưu thay đổi
                db.Entry(suplr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index"); // Chuyển hướng về trang danh sách nhà cung cấp
            }
            return View(suplr); // Nếu dữ liệu không hợp lệ, trả về view để người dùng nhập lại
        }

        // DETAILS: Supplier/Details/{id}
        public ActionResult Details(int id)
        {
            // Tìm thông tin chi tiết của nhà cung cấp
            Supplier suplr = db.Suppliers.Find(id);
            if (suplr == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy nhà cung cấp, trả về lỗi 404
            }
            return View(suplr); // Trả về view hiển thị thông tin chi tiết nhà cung cấp
        }

        // DELETE: Supplier/Delete/{id}
        public ActionResult Delete(int id)
        {
            // Tìm nhà cung cấp cần xóa từ cơ sở dữ liệu
            Supplier suplr = db.Suppliers.Find(id);
            if (suplr == null)
            {
                return HttpNotFound(); // Nếu không tìm thấy nhà cung cấp, trả về lỗi 404
            }
            return View(suplr); // Trả về view xác nhận xóa nhà cung cấp
        }

        // DELETE CONFIRMED: Supplier/DeleteConfirmed/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Xác nhận xóa nhà cung cấp và lưu thay đổi vào cơ sở dữ liệu
            Supplier suplr = db.Suppliers.Find(id);
            db.Suppliers.Remove(suplr);
            db.SaveChanges();
            return RedirectToAction("Index"); // Sau khi xóa, chuyển hướng về trang danh sách nhà cung cấp
        }
    }
}
