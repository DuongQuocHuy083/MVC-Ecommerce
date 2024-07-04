using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    public class admin_LoginController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();
        // GET: admin_Login
        public ActionResult Index()
        {
            return View();
        }
        /*  [HttpPost]*/
        /* public ActionResult Login(admin_Login login)
         {
             if (ModelState.IsValid)
             {
                 var model = (from m in db.admin_Login
                              where m.UserName == login.UserName && m.Password == login.Password
                              select m).Any();

                 if (model)
                 {
                     var loginInfo = db.admin_Login.Where(x => x.UserName == login.UserName && x.Password == login.Password).FirstOrDefault();

                     Session["username"] = loginInfo.UserName;
                     TemData.EmpID = loginInfo.EmpID;
                     return RedirectToAction("Index", "Dashboard");
                 }
             }

             return View();
         }*/

        [HttpPost]
        public ActionResult Login(admin_Login login)
        {
            if (ModelState.IsValid)
            {
                var model = db.admin_Login.Any(m => m.UserName == login.UserName && m.Password == login.Password);

                if (model)
                {
                    var loginInfo = db.admin_Login.FirstOrDefault(x => x.UserName == login.UserName && x.Password == login.Password);

                    Session["username"] = loginInfo.UserName;
                    TemData.EmpID = loginInfo.EmpID;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không chính xác.");
                    return RedirectToAction("Index", "admin_Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return RedirectToAction("Index", "admin_Login");
            }

            return View();
        }

        // Logout Server Code
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "admin_Login");
        }
    }
}