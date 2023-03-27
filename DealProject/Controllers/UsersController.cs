using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealProject.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;

namespace DealProject.Controllers
{
    public class UsersController : Controller
    {
       // UsersVM user = new UsersVM();
        // GET: Users
        public ActionResult Index()
        {
            IEnumerable<UsersVM> userList;
            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("Users").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<UsersVM>>().Result;
            return View(userList);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult  Create(UsersVM usersVM, HttpPostedFileBase file)
        {

            try
            {


               

                string fileName = "";
                
                if (file != null)
                {
                    fileName = file.FileName;
                    string fileNameFullPath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    file.SaveAs(fileNameFullPath);
                }

                usersVM.Image = fileName;
               
                HttpResponseMessage response = GlobalVarables.WebApiClient.PostAsJsonAsync("Users",usersVM).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)

        {

            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("Users/"+id.ToString()).Result;
      
            
           // usersVM = response.Content.ReadAsAsync<UsersVM>().Result;
            //usersVM = await response.Content.ReadAsAsync<UsersVM>().Result;
           
            return View( response.Content.ReadAsAsync<UsersVM>().Result);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UsersVM usersVM, HttpPostedFileBase file)
        {
            try
            {

                string fileName = "";

                if (file != null)
                {
                    fileName = file.FileName;
                    string fileNameFullPath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    file.SaveAs(fileNameFullPath);
                }

                usersVM.Image = fileName;
                HttpResponseMessage response = GlobalVarables.WebApiClient.PutAsJsonAsync("Users/"+usersVM.ID, usersVM).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {


            return View();
        }
        [HttpPost]
        public ActionResult Login( LoginVM loginVM)
        {
            try { 
            HttpResponseMessage response = GlobalVarables.WebApiClient.PostAsJsonAsync("Login", loginVM).Result;

            return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.error = "userName or password id wrong";
                return View();
            }

        }





        //// GET: Users/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

     //   POST: Users/Delete/5
      //  [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                HttpResponseMessage response = GlobalVarables.WebApiClient.DeleteAsync("Users/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult DeleteSelected(string[] ids)
        {
            try
            {

                HttpResponseMessage response = GlobalVarables.WebApiClient.DeleteAsync("Users/" + ids).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
