using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using DealApi.Models;

namespace DealApi.Controllers
{
    public class DealsController : ApiController
    {
        private DealsCopmanyDatabase db = new DealsCopmanyDatabase();

        // GET: api/Deals
        public IQueryable<Deal> GetDeals()
        {
            return db.Deals;
        }

        // GET: api/Deals/5
        [ResponseType(typeof(Deal))]
        public async Task<IHttpActionResult> GetDeal(int id)
        {
            Deal deal = await db.Deals.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }

            return Ok(deal);
        }

        // PUT: api/Deals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDeal(int id, Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deal.ID)
            {
                return BadRequest();
            }
            deal.Update_DateTime_UTC = DateTime.UtcNow;
            db.Entry(deal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DealExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Deals
        [ResponseType(typeof(Deal))]
        public async Task<IHttpActionResult> PostDeal(Deal deal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            deal.Server_DateTime = DateTime.Now;
            deal.DateTime_UTC = DateTime.UtcNow;
            db.Deals.Add(deal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = deal.ID }, deal);
        }

        // DELETE: api/Deals/5
        [ResponseType(typeof(Deal))]
        public async Task<IHttpActionResult> DeleteDeal(int id)
        {
            Deal deal = await db.Deals.FindAsync(id);
            if (deal == null)
            {
                return NotFound();
            }

            db.Deals.Remove(deal);
            await db.SaveChangesAsync();

            return Ok(deal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DealExists(int id)
        {
            return db.Deals.Count(e => e.ID == id) > 0;
        }
    }
}