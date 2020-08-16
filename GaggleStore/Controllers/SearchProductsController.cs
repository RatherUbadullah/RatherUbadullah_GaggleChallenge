using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Threading;
using System.Net;
using System.IO;
using GaggleStore.Controllers;
using GaggleStore.Models;



//This Control is used to Search all the Products and Product by Unique Id...

namespace GaggleStore.Controllers
{
    public class SearchProductsController : Controller
    {
        //
        // GET: /SearchProducts/
        //This Method will get all available Products..
        public ActionResult Index()
        {

            GaggleStoreDBEntities gdb = new GaggleStoreDBEntities();
            ViewBag.productLists = gdb.Products.ToList();

            return View();
        }

        //This Method will find the Product by Unique ID....
        public ActionResult findById(GaggleStore.Models.Product s)
        {
            GaggleStoreDBEntities db = new GaggleStoreDBEntities();

            if (db.Products.Where(x => x.id == s.id).Count() != 0)
            {
                ViewBag.productFound = db.Products.Where(x => x.id == s.id).ToList();
            }
            else
            {
                ViewBag.productFound = null;
            }
            return View("SearchById");
        }
	}
}