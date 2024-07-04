using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using MyEcommerceAdmin.Models;

namespace MyEcommerceAdmin.Controllers
{
    // Controller quản lý sản phẩm
    public class ProductController : Controller
    {
        MyEcommerceDbContext db = new MyEcommerceDbContext();

        // GET: Product/Index
        public ActionResult Index()
        {
            // Hiển thị danh sách sản phẩm
            return View(db.Products.ToList());
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            // Tạo mới sản phẩm
            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVM pvm)
        {
            // Xử lý tạo mới sản phẩm
            if (ModelState.IsValid)
            {
                if (pvm.Picture != null)
                {
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                    pvm.Picture.SaveAs(Server.MapPath(filePath));

                    Product p = new Product
                    {
                        ProductID = pvm.ProductID,
                        Name = pvm.Name,
                        SupplierID = pvm.SupplierID,
                        CategoryID = pvm.CategoryID,
                        SubCategoryID = pvm.SubCategoryID,
                        UnitPrice = pvm.UnitPrice,
                        OldPrice = pvm.OldPrice,
                        Discount = pvm.Discount,
                        UnitInStock = pvm.UnitInStock,
                        ProductAvailable = pvm.ProductAvailable,
                        ShortDescription = pvm.ShortDescription,
                        Note = pvm.Note,
                        PicturePath = filePath
                    };
                    db.Products.Add(p);
                    db.SaveChanges();
                    return PartialView("_Success");
                }
            }

            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return PartialView("_Error");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            // Chỉnh sửa sản phẩm
            Product p = db.Products.Find(id);

            ProductVM pvm = new ProductVM
            {
                ProductID = p.ProductID,
                Name = p.Name,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                SubCategoryID = p.SubCategoryID,
                UnitPrice = p.UnitPrice,
                OldPrice = p.OldPrice,
                Discount = p.Discount,
                UnitInStock = p.UnitInStock,
                ProductAvailable = p.ProductAvailable,
                ShortDescription = p.ShortDescription,
                Note = p.Note,
                PicturePath = p.PicturePath
            };

            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View(pvm);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductVM pvm)
        {
            // Xử lý lưu thay đổi sau khi chỉnh sửa sản phẩm
            if (ModelState.IsValid)
            {
                string filePath = pvm.PicturePath;
                if (pvm.Picture != null)
                {
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                    pvm.Picture.SaveAs(Server.MapPath(filePath));
                }

                Product p = new Product
                {
                    ProductID = pvm.ProductID,
                    Name = pvm.Name,
                    SupplierID = pvm.SupplierID,
                    CategoryID = pvm.CategoryID,
                    SubCategoryID = pvm.SubCategoryID,
                    UnitPrice = pvm.UnitPrice,
                    OldPrice = pvm.OldPrice,
                    Discount = pvm.Discount,
                    UnitInStock = pvm.UnitInStock,
                    ProductAvailable = pvm.ProductAvailable,
                    ShortDescription = pvm.ShortDescription,
                    Note = pvm.Note,
                    PicturePath = filePath
                };

                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View(pvm);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            // Hiển thị chi tiết sản phẩm
            Product p = db.Products.Find(id);

            ProductVM pvm = new ProductVM
            {
                ProductID = p.ProductID,
                Name = p.Name,
                SupplierID = p.SupplierID,
                CategoryID = p.CategoryID,
                SubCategoryID = p.SubCategoryID,
                UnitPrice = p.UnitPrice,
                OldPrice = p.OldPrice,
                Discount = p.Discount,
                UnitInStock = p.UnitInStock,
                ProductAvailable = p.ProductAvailable,
                ShortDescription = p.ShortDescription,
                Note = p.Note,
                PicturePath = p.PicturePath
            };

            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View(pvm);
        }

        // POST: Product/Details/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(ProductVM pvm)
        {
            // Xử lý lưu chi tiết sản phẩm
            if (ModelState.IsValid)
            {
                string filePath = pvm.PicturePath;
                if (pvm.Picture != null)
                {
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pvm.Picture.FileName));
                    pvm.Picture.SaveAs(Server.MapPath(filePath));
                }

                Product p = new Product
                {
                    ProductID = pvm.ProductID,
                    Name = pvm.Name,
                    SupplierID = pvm.SupplierID,
                    CategoryID = pvm.CategoryID,
                    SubCategoryID = pvm.SubCategoryID,
                    UnitPrice = pvm.UnitPrice,
                    OldPrice = pvm.OldPrice,
                    Discount = pvm.Discount,
                    UnitInStock = pvm.UnitInStock,
                    ProductAvailable = pvm.ProductAvailable,
                    ShortDescription = pvm.ShortDescription,
                    Note = pvm.Note,
                    PicturePath = filePath
                };

                db.Entry(p).State = System.Data.Entity.EntityState.Unchanged;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supplierList = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            ViewBag.categoryList = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.SubCategoryList = new SelectList(db.SubCategories, "SubCategoryID", "Name");
            return View(pvm);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            // Xác nhận xóa sản phẩm
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // Xác nhận xóa sản phẩm
            Product product = db.Products.Find(id);
            string file_name = product.PicturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }

            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
