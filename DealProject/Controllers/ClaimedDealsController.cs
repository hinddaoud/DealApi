using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DealProject.Models;

namespace DealProject.Controllers
{
    public class ClaimedDealsController : Controller
    {
        // GET: ClaimedDeals
        public ActionResult Index()
        {
            IEnumerable<ClaimedVM> claimedList;
            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("ClaimedDeals").Result;
            claimedList = response.Content.ReadAsAsync<IEnumerable<ClaimedVM>>().Result;
            return View(claimedList);
        }

        // GET: ClaimedDeals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClaimedDeals/Create
        public ActionResult Create()
        {
            IEnumerable<UsersVM> userList;
            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("Users").Result;
            userList = response.Content.ReadAsAsync<IEnumerable<UsersVM>>().Result;


            IEnumerable<DealsVM> dealList;
            HttpResponseMessage responseDeal = GlobalVarables.WebApiClient.GetAsync("Deals").Result;
            dealList = responseDeal.Content.ReadAsAsync<IEnumerable<DealsVM>>().Result;


            ClaimedVM model = new ClaimedVM();
            ViewBag.Users = new SelectList(userList, "ID", "Name");
            ViewBag.Deals = new SelectList(dealList, "ID", "Name");
            return View(model);

            
          //  return View();
        }

        // POST: ClaimedDeals/Create
        [HttpPost]
        public ActionResult Create(ClaimedVM claimedVM)
        {
            try
            {
                // TODO: Add insert logic here
                HttpResponseMessage response = GlobalVarables.WebApiClient.PostAsJsonAsync("ClaimedDeals", claimedVM).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClaimedDeals/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("ClaimedDeals/" + id.ToString()).Result;


            // usersVM = response.Content.ReadAsAsync<UsersVM>().Result;
            //usersVM = await response.Content.ReadAsAsync<UsersVM>().Result;

            return View(response.Content.ReadAsAsync<ClaimedVM>().Result);
        }

        // POST: ClaimedDeals/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClaimedVM claimedVM)
        {
            try
            {
                // TODO: Add update logic here

                HttpResponseMessage response = GlobalVarables.WebApiClient.PutAsJsonAsync("ClaimedDeals/" + claimedVM.ID, claimedVM).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //// GET: ClaimedDeals/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: ClaimedDeals/Delete/5
        
        public ActionResult Delete(int id)
        {
            try
            {

                HttpResponseMessage response = GlobalVarables.WebApiClient.DeleteAsync("ClaimedDeals/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
