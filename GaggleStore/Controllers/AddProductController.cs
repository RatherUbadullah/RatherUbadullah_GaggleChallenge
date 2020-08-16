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


//This Controller is used to insert a new product...

namespace GaggleStore.Controllers
{
    public class AddProductController : Controller
    {
        //
        // GET: /AddProduct/
        public ActionResult Index()
        {
            //making a list for category of products....

            List<string> category = new List<string>();
            category.Add("Men");
            category.Add("Women");
            category.Add("Kids");


            //sending the data to View by : ViewBag..

            ViewBag.cat = new SelectList(category);

            //Returning the View..

            return View();
        }


        //This is the Method for Adding a Product in a Database...
        public ActionResult addProduct(GaggleStore.Models.Product p)
        {

            //Creating  an object of DatabaseEntity..
            GaggleStoreDBEntities db1 = new GaggleStoreDBEntities();

            try
            {
                //Creating an instance of Product...
                Product prod = new Product();
                //Assigning the inout values....
                prod.name = p.name;
                prod.id = p.id;
                prod.price = p.price;
                prod.description = p.description;
                prod.quantity = p.quantity;
                prod.category = p.category;


                //Saving the changes to the database..
                db1.Products.Add(prod);
                db1.SaveChanges();


                //Sending  the data to the View of a product added.... 
                ViewBag.result = p.name;
                ViewBag.cat = p.category;


                //Returning the View...
                return View("View1");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	}
}