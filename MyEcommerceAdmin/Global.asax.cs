using MyEcommerceAdmin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyEcommerceAdmin.Migrations;


namespace MyEcommerceAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyEcommerceDbContext, Configuration>());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

    }
    public class NpgsqlInitializer : DropCreateDatabaseIfModelChanges<MyEcommerceDbContext>
    {
        protected override void Seed(MyEcommerceDbContext context)
        {
            // Thêm dữ liệu mẫu vào cơ sở dữ liệu nếu cần
        }
    }
}
