using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
  
    public static class LoadDataController
    {
       
        static MyEcommerceDbContext db = new MyEcommerceDbContext();

      
        public static List<OrderDetail> GetDefaultData(this ControllerBase controller)
        {
            // Kiểm tra và khởi tạo danh sách sản phẩm tạm thời nếu chưa có
            if (TempShpData.items == null)
            {
                TempShpData.items = new List<OrderDetail>();
            }
            var data = TempShpData.items.ToList(); // Lấy danh sách sản phẩm từ TempData và chuyển sang List

            // Gán danh sách sản phẩm vào ViewBag để hiển thị trong giỏ hàng
            controller.ViewBag.cartBox = data.Count == 0 ? null : data;
            controller.ViewBag.NoOfItem = data.Count(); // Số lượng sản phẩm trong giỏ hàng
            int? SubTotal = Convert.ToInt32(data.Sum(x => x.TotalAmount)); // Tính tổng tiền của giỏ hàng
            controller.ViewBag.Total = SubTotal;

            int Discount = 0; // Giảm giá (nếu có)
            controller.ViewBag.SubTotal = SubTotal; // Tổng tiền (chưa tính giảm giá)
            controller.ViewBag.Discount = Discount; // Giảm giá
            controller.ViewBag.TotalAmount = SubTotal - Discount; // Tổng tiền phải thanh toán

            // Lấy số lượng sản phẩm trong Wishlist của người dùng và gán vào ViewBag
            controller.ViewBag.WlItemsNo = db.Wishlists.Where(x => x.CustomerID == TempShpData.UserID).ToList().Count();

            return data; // Trả về danh sách sản phẩm
        }
    }
}
