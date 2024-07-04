using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    // Controller quản lý giỏ hàng của người dùng
    public class MyCartController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: MyCart
        public ActionResult Index()
        {
            var data = this.GetDefaultData(); // Lấy dữ liệu mặc định cho giỏ hàng

            return View(data); // Trả về view Index với dữ liệu giỏ hàng

        }

        // Phương thức xử lý xóa sản phẩm khỏi giỏ hàng
        public ActionResult Remove(int id)
        {
            TempShpData.items.RemoveAll(x => x.ProductID == id); // Xóa sản phẩm khỏi danh sách sản phẩm tạm thời
            return RedirectToAction("Index"); // Chuyển hướng về trang Index của MyCart

        }

        // Phương thức xử lý khi người dùng đi đến thanh toán
        [HttpPost]
        public ActionResult ProcedToCheckout(FormCollection formcoll)
        {
            var a = TempShpData.items.ToList(); // Lấy danh sách sản phẩm tạm thời và chuyển sang List

            // Duyệt qua các sản phẩm được chọn để thanh toán
            for (int i = 0; i < formcoll.Count / 2; i++)
            {
                // Lấy ProductID và chi tiết đơn hàng tương ứng
                int pID = Convert.ToInt32(formcoll["shcartID-" + i + ""]);
                var ODetails = TempShpData.items.FirstOrDefault(x => x.ProductID == pID);

                // Lấy số lượng sản phẩm từ form và cập nhật lại chi tiết đơn hàng
                int qty = Convert.ToInt32(formcoll["Qty-" + i + ""]);
                ODetails.Quantity = qty;
                ODetails.UnitPrice = ODetails.UnitPrice;
                ODetails.TotalAmount = qty * ODetails.UnitPrice;

                // Xóa sản phẩm khỏi danh sách tạm thời
                TempShpData.items.RemoveAll(x => x.ProductID == pID);

                // Nếu danh sách tạm thời rỗng, khởi tạo mới
                if (TempShpData.items == null)
                {
                    TempShpData.items = new List<OrderDetail>();
                }

                // Thêm lại chi tiết đơn hàng đã cập nhật vào danh sách tạm thời
                TempShpData.items.Add(ODetails);

            }

            // Chuyển hướng sang trang thanh toán (CheckOut/Index)
            return RedirectToAction("Index", "CheckOut");
        }


    }
}
