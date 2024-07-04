using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;
using System.Data;

namespace MyEcommerceAdmin.Controllers
{
    public class CheckOutController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext(); // Đối tượng DbContext để kết nối với cơ sở dữ liệu

        // GET: CheckOut/Index
        // Hiển thị trang thanh toán và chọn phương thức thanh toán
        public ActionResult Index()
        {
            ViewBag.PayMethod = new SelectList(db.PaymentTypes, "PayTypeID", "TypeName"); // Danh sách phương thức thanh toán

            var data = this.GetDefaultData(); // Lấy dữ liệu mặc định cho trang thanh toán
            return View(data); // Trả về view Index với dữ liệu đã lấy
        }

        // PLACE ORDER--LAST STEP
        // Xử lý đặt hàng - bước cuối cùng
        public ActionResult PlaceOrder(FormCollection getCheckoutDetails)
        {
            // Tạo ID mới cho ShippingDetail, Payment và Order
            int shpID = 1;
            if (db.ShippingDetails.Count() > 0)
            {
                shpID = db.ShippingDetails.Max(x => x.ShippingID) + 1;
            }

            int payID = 1;
            if (db.Payments.Count() > 0)
            {
                payID = db.Payments.Max(x => x.PaymentID) + 1;
            }

            int orderID = 1;
            if (db.Orders.Count() > 0)
            {
                orderID = db.Orders.Max(x => x.OrderID) + 1;
            }

            // Tạo ShippingDetail mới từ thông tin được nhập
            ShippingDetail shpDetails = new ShippingDetail();
            shpDetails.ShippingID = shpID;
            shpDetails.FirstName = getCheckoutDetails["FirstName"];
            shpDetails.LastName = getCheckoutDetails["LastName"];
            shpDetails.Email = getCheckoutDetails["Email"];
            shpDetails.Mobile = getCheckoutDetails["Mobile"];
            shpDetails.Address = getCheckoutDetails["Address"];
            shpDetails.City = getCheckoutDetails["City"];
            shpDetails.PostCode = getCheckoutDetails["PostCode"];

            // Lưu ShippingDetail vào cơ sở dữ liệu
            db.ShippingDetails.Add(shpDetails);
            db.SaveChanges();

            // Tạo Payment mới từ thông tin phương thức thanh toán được chọn
            Payment pay = new Payment();
            pay.PaymentID = payID;
            pay.Type = Convert.ToInt32(getCheckoutDetails["PayMethod"]);

            // Lưu Payment vào cơ sở dữ liệu
            db.Payments.Add(pay);
            db.SaveChanges();

            // Tạo Order mới từ các thông tin được nhập và lưu vào cơ sở dữ liệu
            Order o = new Order();
            o.OrderID = orderID;
            o.CustomerID = TempShpData.UserID; // Lấy ID của khách hàng từ dữ liệu tạm thời
            o.PaymentID = payID;
            o.ShippingID = shpID;
            o.Discount = Convert.ToInt32(getCheckoutDetails["discount"]); // Chiết khấu (nếu có)
            o.TotalAmount = Convert.ToInt32(getCheckoutDetails["totalAmount"]); // Tổng số tiền thanh toán
            o.isCompleted = true; // Đã hoàn thành đơn hàng
            o.OrderDate = DateTime.Now; // Ngày đặt hàng

            // Lưu Order vào cơ sở dữ liệu
            db.Orders.Add(o);
            db.SaveChanges();

            // Thêm các chi tiết đơn hàng vào OrderDetails
            foreach (var OD in TempShpData.items)
            {
                OD.OrderID = orderID; // Gán ID của Order cho từng chi tiết đơn hàng
                OD.Order = db.Orders.Find(orderID); // Lấy thông tin Order từ cơ sở dữ liệu
                OD.Product = db.Products.Find(OD.ProductID); // Lấy thông tin sản phẩm từ cơ sở dữ liệu
                db.OrderDetails.Add(OD); // Thêm chi tiết đơn hàng vào cơ sở dữ liệu
                db.SaveChanges();
            }

            // Chuyển hướng đến trang cảm ơn sau khi đặt hàng thành công
            return RedirectToAction("Index", "ThankYou");
        }
    }
}
