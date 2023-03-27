using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DealProject.Models;

namespace DealProject.Controllers
{
    public class DealsController : Controller
    {
        // GET: Deals
        public ActionResult Index()
        {
            IEnumerable<DealsVM> dealList;
            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("Deals").Result;
            dealList = response.Content.ReadAsAsync<IEnumerable<DealsVM>>().Result;
            return View(dealList);
        }

        // GET: Deals/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Deals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Deals/Create
        [HttpPost]
        public ActionResult Create(DealsVM dealsVM)
        {
            try
            {
                // TODO: Add insert logic here
                HttpResponseMessage response = GlobalVarables.WebApiClient.PostAsJsonAsync("Deals", dealsVM).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Deals/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = GlobalVarables.WebApiClient.GetAsync("Deals/" + id.ToString()).Result;


            

            return View(response.Content.ReadAsAsync<DealsVM>().Result);
        }

        // POST: Deals/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DealsVM dealsVM)
        {
            try
            {
                // TODO: Add update logic here

                HttpResponseMessage response = GlobalVarables.WebApiClient.PutAsJsonAsync("Deals/" + dealsVM.ID, dealsVM).Result;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Deals/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Deals/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
           try {

                HttpResponseMessage response = GlobalVarables.WebApiClient.DeleteAsync("Deals/" + id.ToString()).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
