using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DealApi.Models;

namespace DealApi.Controllers
{
    public class UsersController : ApiController
    {
        private DealsCopmanyDatabase db = new DealsCopmanyDatabase();

        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return db.Users.ToList();
        }

        // GET: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user, HttpPostedFileBase file)
        {
           
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != user.ID)
            {
                return BadRequest();
            }

            user.Update_DateTime_UTC = DateTime.UtcNow;
            db.Entry(user).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(User))]
        public IHttpActionResult PostUser(User user)
        {

            User recorduser = db.Users.Where(x => x.Email == user.Email && x.UserType == "Normal").FirstOrDefault();
            User recordAdmin = db.Users.Where(x => x.Email == user.Email && x.UserType == "Admin").FirstOrDefault();
            if (recorduser == null&& recordAdmin==null)
            {
                //{
                //    return BadRequest(ModelState);
                //}
                user.Server_DateTime = Convert.ToDateTime(DateTime.Now);
                user.DateTime_UTC = DateTime.UtcNow;
                db.Users.Add(user);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = user.ID }, user);
            }
            return BadRequest();
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {

            User usreToDeleted = db.Users.Where(x => x.ID == id).FirstOrDefault();

            List<ClaimedDeal> claimedDeal = db.ClaimedDeals.Where(x => x.User_ID == id).ToList();
            if (claimedDeal != null)
            {
                db.ClaimedDeals.RemoveRange(claimedDeal);
            }

            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            
            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }




        /// <summary>
        /// ///////////// delet list of user
        /// </summary>

        
        [Route("Users/login")]
        public IHttpActionResult Login(User user )
        {

            User userInfoAdmin = db.Users.Where(x => x.Email == user.Email && x.Password == user.Password && x.UserType == "Admin").FirstOrDefault();
            User userInfoNormal = db.Users.Where(x => x.Email == user.Email && x.Password == user.Password && x.UserType == "Normal").FirstOrDefault();
            if (userInfoAdmin != null)
            {
               
                userInfoAdmin.Last_Login_DateTime_UTC = DateTime.Now;
                db.SaveChanges();
                return Ok(user);
                // return RedirectToAction("info", "EmployeeAllowance");
            }
            else if (userInfoNormal != null)
            {

                return Ok(user);

                //  return RedirectToAction("info", "EmployeeAllowance");
                userInfoNormal.Last_Login_DateTime_UTC = DateTime.Now;
                db.SaveChanges();
               

            }
            return NotFound();
        }

        public IHttpActionResult DeleteSelected(string[] ids)
        {
            try
            {
                if (ids == null || ids.Length == 0)
                {
                    return BadRequest();
                    ModelState.AddModelError("", "No item selected to delete");

                }

               // bind the task collection into list
                List<int> TaskIds = ids.Select(x => Int32.Parse(x)).ToList();

             
                for (var i = 0; i < TaskIds.Count(); i++)
                {
                    var users = db.Users.Find(TaskIds[i]);
                    //remove the record from the database
                    db.Users.Remove(users);
                    //call save changes action otherwise the table will not be updated
                    db.SaveChanges();
                    return Ok(users);
                }
                return NotFound();
                //redirect to index view once record is deleted

            }
            catch
            {
                // return Json(new { status = "the user have more than Claimed" }, JsonRequestBehavior.AllowGet);
                // ViewBag.messageDelet = Json(new { status = "the user have more than Claimed" }, JsonRequestBehavior.AllowGet);
                // Session["msg"] = "the user have more than Claimed";
                return NotFound();
            }






        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.ID == id) > 0;
        }
    }
}