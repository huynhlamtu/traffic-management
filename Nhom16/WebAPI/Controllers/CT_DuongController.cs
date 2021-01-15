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
    public class CT_DuongController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/CT_Duong
        public IQueryable<CT_Duong> GetCT_Duong()
        {
            return db.CT_Duong;
        }

        // GET: api/CT_Duong/5
        [ResponseType(typeof(CT_Duong))]
        public IHttpActionResult GetCT_Duong(int id)
        {
            CT_Duong cT_Duong = db.CT_Duong.Find(id);
            if (cT_Duong == null)
            {
                return NotFound();
            }

            return Ok(cT_Duong);
        }
        [ResponseType(typeof(CT_Duong))]
        [Route("api/CT_Duong/{TenDuong}")]
        public IHttpActionResult GetCT_DuongName(string TenDuong)
        {
            List<Duong> duong = (from ct in db.Duong where ct.ten_duong == TenDuong select ct).ToList();
            int ma_duong1 = duong[0].ma_duong;
            List<CT_Duong> cT_Duong = (from ct1 in db.CT_Duong where ct1.ma_duong == ma_duong1 select ct1).ToList();
            if (cT_Duong == null)
            {
                return NotFound();
            }

            return Ok(cT_Duong);
        }
        // PUT: api/CT_Duong/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCT_Duong(int id, CT_Duong cT_Duong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cT_Duong.ma_CT_duong)
            {
                return BadRequest();
            }

            db.Entry(cT_Duong).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CT_DuongExists(id))
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

        // POST: api/CT_Duong
        [ResponseType(typeof(CT_Duong))]
        public IHttpActionResult PostCT_Duong(CT_Duong cT_Duong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CT_Duong.Add(cT_Duong);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cT_Duong.ma_CT_duong }, cT_Duong);
        }

        // DELETE: api/CT_Duong/5
        [ResponseType(typeof(CT_Duong))]
        public IHttpActionResult DeleteCT_Duong(int id)
        {
            CT_Duong cT_Duong = db.CT_Duong.Find(id);
            if (cT_Duong == null)
            {
                return NotFound();
            }

            db.CT_Duong.Remove(cT_Duong);
            db.SaveChanges();

            return Ok(cT_Duong);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CT_DuongExists(int id)
        {
            return db.CT_Duong.Count(e => e.ma_CT_duong == id) > 0;
        }
    }
}