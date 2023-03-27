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
    public class ClaimedDealsController : ApiController
    {
        private DealsCopmanyDatabase db = new DealsCopmanyDatabase();

        // GET: api/ClaimedDeals
        public IQueryable<ClaimedDeal> GetClaimedDeals()
        {
            return db.ClaimedDeals;
        }

        // GET: api/ClaimedDeals/5
        [ResponseType(typeof(ClaimedDeal))]
        public async Task<IHttpActionResult> GetClaimedDeal(int id)
        {
            ClaimedDeal claimedDeal = await db.ClaimedDeals.FindAsync(id);
            if (claimedDeal == null)
            {
                return NotFound();
            }

            return Ok(claimedDeal);
        }

        // PUT: api/ClaimedDeals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutClaimedDeal(int id, ClaimedDeal claimedDeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != claimedDeal.ID)
            {
                return BadRequest();
            }

            db.Entry(claimedDeal).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClaimedDealExists(id))
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

        // POST: api/ClaimedDeals
        [ResponseType(typeof(ClaimedDeal))]
        public async Task<IHttpActionResult> PostClaimedDeal(ClaimedDeal claimedDeal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            claimedDeal.Server_DateTime = DateTime.Now;
            claimedDeal.DateTime_UTC = DateTime.UtcNow;
            db.ClaimedDeals.Add(claimedDeal);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = claimedDeal.ID }, claimedDeal);
        }

        // DELETE: api/ClaimedDeals/5
        [ResponseType(typeof(ClaimedDeal))]
        public async Task<IHttpActionResult> DeleteClaimedDeal(int id)
        {
            ClaimedDeal claimedDeal = await db.ClaimedDeals.FindAsync(id);
            if (claimedDeal == null)
            {
                return NotFound();
            }

            db.ClaimedDeals.Remove(claimedDeal);
            await db.SaveChangesAsync();

            return Ok(claimedDeal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClaimedDealExists(int id)
        {
            return db.ClaimedDeals.Count(e => e.ID == id) > 0;
        }
    }
}