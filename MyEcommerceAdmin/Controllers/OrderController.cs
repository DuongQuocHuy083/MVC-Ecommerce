using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    // Controller quản lý đơn hàng
    public class OrderController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: Order/Index
        public ActionResult Index()
        {
            // Lấy danh sách các đơn hàng từ database, sắp xếp theo OrderID giảm dần và chuyển sang List
            return View(db.Orders.OrderByDescending(x => x.OrderID).ToList());
        }

        // GET: Order/Details/{id}
        public ActionResult Details(int id)
        {
            // Lấy thông tin đơn hàng dựa trên OrderID
            Order ord = db.Orders.Find(id);

            // Lấy danh sách chi tiết đơn hàng tương ứng với OrderID
            var Ord_details = db.OrderDetails.Where(x => x.OrderID == id).ToList();

            // Tạo Tuple chứa đối tượng Order và danh sách chi tiết đơn hàng
            var tuple = new Tuple<Order, IEnumerable<OrderDetail>>(ord, Ord_details);

            // Tính tổng số tiền các mặt hàng trong đơn hàng
            double SumAmount = Convert.ToDouble(Ord_details.Sum(x => x.TotalAmount));

            // Đặt các thông tin phụ trợ cho View
            ViewBag.TotalItems = Ord_details.Sum(x => x.Quantity); // Tổng số mặt hàng
            ViewBag.Discount = 0; // Giảm giá (có thể mở rộng thêm tính năng này sau)
            ViewBag.TAmount = SumAmount - 0; // Tổng số tiền sau khi áp dụng giảm giá (hiện tại không giảm giá nên trừ 0)
            ViewBag.Amount = SumAmount; // Tổng số tiền trước khi giảm giá

            // Trả về View Details với dữ liệu Tuple
            return View(tuple);
        }
    }
}
