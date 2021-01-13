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
    public class Camera_BackupController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Camera_Backup
        public IQueryable<Camera_Backup> GetCamera_Backup()
        {
            return db.Camera_Backup;
        }


        [ResponseType(typeof(Camera_Backup))]
        [Route("api/Camera_Backup/{TenDuong}")]
        public IHttpActionResult GetCamera_Backup1(string TenDuong)
        {

            var camera_backup = (from ct in db.Camera_Backup where ct.TenDuong == TenDuong select ct).ToList();

            if (camera_backup == null)
            {
                return NotFound();
            }

            return Ok(camera_backup);
        }

        [ResponseType(typeof(Camera_Backup))]
        public IHttpActionResult GetCamera_Backup2(DateTime thoi_gian)
        {
            string sql = "select * from Camera_Backup where  thoi_gian=" + thoi_gian;

            var camera_backup = (from ct in db.Camera_Backup where ct.thoi_gian == thoi_gian select ct).ToList();
            if (camera_backup == null)
            {
                return NotFound();
            }

            return Ok(camera_backup);
        }

        [ResponseType(typeof(Camera_Backup))]
        [Route("api/Camera_Backup/{TenDuong}/{thoi_gian}")]
        public IHttpActionResult GetCamera_Backup3(string TenDuong, DateTime thoi_gian)
        {
            string sql = "select * from Camera_Backup where  thoi_gian=" + thoi_gian;

            var camera_backup = (from ct in db.Camera_Backup where ct.thoi_gian == thoi_gian && ct.TenDuong == TenDuong select ct).ToList();
            if (camera_backup == null)
            {
                return NotFound();
            }

            return Ok(camera_backup);
        }




        // GET: api/Camera_Backup/5
        [ResponseType(typeof(Camera_Backup))]
        public IHttpActionResult GetCamera_Backup(int id)
        {
            Camera_Backup camera_Backup = db.Camera_Backup.Find(id);
            if (camera_Backup == null)
            {
                return NotFound();
            }

            return Ok(camera_Backup);
        }

        // PUT: api/Camera_Backup/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCamera_Backup(int id, Camera_Backup camera_Backup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != camera_Backup.ma_backup)
            {
                return BadRequest();
            }

            db.Entry(camera_Backup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Camera_BackupExists(id))
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

        // POST: api/Camera_Backup
        [ResponseType(typeof(Camera_Backup))]
        public IHttpActionResult PostCamera_Backup(Camera_Backup camera_Backup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Camera_Backup.Add(camera_Backup);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = camera_Backup.ma_backup }, camera_Backup);
        }

        // DELETE: api/Camera_Backup/5
        [ResponseType(typeof(Camera_Backup))]
        public IHttpActionResult DeleteCamera_Backup(int id)
        {
            Camera_Backup camera_Backup = db.Camera_Backup.Find(id);
            if (camera_Backup == null)
            {
                return NotFound();
            }

            db.Camera_Backup.Remove(camera_Backup);
            db.SaveChanges();

            return Ok(camera_Backup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Camera_BackupExists(int id)
        {
            return db.Camera_Backup.Count(e => e.ma_backup == id) > 0;
        }
    }
}