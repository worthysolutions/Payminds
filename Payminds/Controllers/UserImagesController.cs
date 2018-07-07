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
using PaymindsDataAccess;

namespace Payminds.Controllers
{
    public class UserImagesController : ApiController
    {
        private PaymindsEntities db = new PaymindsEntities();

        // GET: api/UserImages
        public IQueryable<UserImage> GetUserImages()
        {
            return db.UserImages;
        }

        // GET: api/UserImages/5
        [ResponseType(typeof(UserImage))]
        public async Task<IHttpActionResult> GetUserImage(int id)
        {
            UserImage userImage = await db.UserImages.FindAsync(id);
            if (userImage == null)
            {
                return NotFound();
            }

            return Ok(userImage);
        }

        // PUT: api/UserImages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserImage(int id, UserImage userImage)
        {
            DateTime localDate = DateTime.Now;
            userImage.upload_time = localDate;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userImage.id)
            {
                return BadRequest();
            }

            db.Entry(userImage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserImageExists(id))
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

        // POST: api/UserImages
        [ResponseType(typeof(UserImage))]
        public async Task<IHttpActionResult> PostUserImage(UserImage userImage)
        {
            DateTime dateTime = DateTime.Now;
            userImage.upload_time = dateTime;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserImages.Add(userImage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userImage.id }, userImage);
        }

        // DELETE: api/UserImages/5
        [ResponseType(typeof(UserImage))]
        public async Task<IHttpActionResult> DeleteUserImage(int id)
        {
            UserImage userImage = await db.UserImages.FindAsync(id);
            if (userImage == null)
            {
                return NotFound();
            }

            db.UserImages.Remove(userImage);
            await db.SaveChangesAsync();

            return Ok(userImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserImageExists(int id)
        {
            return db.UserImages.Count(e => e.id == id) > 0;
        }
    }
}