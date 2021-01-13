using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataRepository;
using DataRepository.Context;
using DataRepository.entity;


namespace WebAPI.Controllers
{
    public class DuongsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Duongs
        public IEnumerable<Duong> GetDuong()
        {
            return db.Duong.ToList();
        }

        // GET: api/Duongs/5
        [ResponseType(typeof(Duong))]
        public IHttpActionResult GetDuong(int id)
        {
            Duong duong = db.Duong.Find(id);
            
            if (duong == null)
            {
                return NotFound();
            }

            return Ok(duong);
        }

        // PUT: api/Duongs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDuong(int id, Duong duong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != duong.ma_duong)
            {
                return BadRequest();
            }

            db.Entry(duong).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DuongExists(id))
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

        // POST: api/Duongs
        [ResponseType(typeof(Duong))]
        public IHttpActionResult PostDuong(Duong duong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Duong.Add(duong);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = duong.ma_duong }, duong);
        }

        // DELETE: api/Duongs/5
        [ResponseType(typeof(Duong))]
        public IHttpActionResult DeleteDuong(int id)
        {
            Duong duong = db.Duong.Find(id);
            if (duong == null)
            {
                return NotFound();
            }

            db.Duong.Remove(duong);
            db.SaveChanges();

            return Ok(duong);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DuongExists(int id)
        {
            return db.Duong.Count(e => e.ma_duong == id) > 0;
        }
    }
}