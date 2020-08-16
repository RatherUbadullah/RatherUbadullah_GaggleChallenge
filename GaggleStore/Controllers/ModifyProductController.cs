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
using System.Data.Entity;



//This Controller is all we use the most ...for Updating and Deleting the Existing Products...

namespace GaggleStore.Controllers
{
    public class ModifyProductController : Controller
    {
        //
        // GET: /ModifyProduct/

        //This Method will load the Index view...
        //It will load the View for Removing the Product...
        public ActionResult Index()
        {

            //Creating an instance of Database Entity...
            GaggleStoreDBEntities db = new GaggleStoreDBEntities();

            //Converting the Products data into list and Storing in the ViewBag for sending to the View...

            ViewBag.pList = db.Products.ToList();

            return View();
        }



       

        //This Method is used to Remove Product ....
        //It will pick the Id Of Product in Hidden Feild.
        public ActionResult remove(GaggleStore.Models.ProductList p)
        {
            //Creating Instance of Database Entity..
            GaggleStoreDBEntities db = new GaggleStoreDBEntities();

            //Fetching the Product to check if that Exists...
            var recordTodelete = db.Products.Where(x => x.id == p.id).FirstOrDefault();

            //Now, Deleting that Product...
            db.Entry(recordTodelete).State = EntityState.Deleted;
            db.SaveChanges();

            //Redirecting back to the list of products to remove...
            return RedirectToAction("Index", "ModifyProduct");
        }


        //Method to update the Existing Product... This wil call the View to Update the Product
        public ActionResult update()
        {

            //Making a list of Product Ids that are already available...
            List<string> ids = new List<string>();
            GaggleStoreDBEntities db  = new GaggleStoreDBEntities();

             
            foreach(var  i in db.Products.Select(x=>x.id))
            {
                ids.Add(i);
            }

            //Making a List of Category 

            List<string> category = new List<string>();
            category.Add("Men");
            category.Add("Women");
            category.Add("Kids");

            ViewBag.cat = new SelectList(category);
            ViewBag.pIds = new SelectList(ids);
            

            return View("updateProduct");
        }


        //This will Update the Product Details...
        [HttpPost]

        public ActionResult editProduct(GaggleStore.Models.Product prod)
        {
            GaggleStoreDBEntities db = new GaggleStoreDBEntities();
            var record = db.Products.Where(x=>x.id == prod.id).FirstOrDefault();
            try
            {
                
                if(prod.name != null)
                    record.name = prod.name;
                if(prod.price != null)
                    record.price = prod.price;
                if(prod.description !=null)
                    record.description = prod.description;
                if(prod.quantity !=null)
                    record.quantity = prod.quantity;
                if(prod.category !=null)
                    record.category = prod.category;

                db.Entry(record).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index", "SearchProducts");
            }
            catch(Exception e)
            {
                throw e;
            }

            

        }


	}
}