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
    public class CamerasController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Cameras
        public IQueryable<Camera> GetCamera()
        {
            return db.Camera;
        }

        // GET: api/Cameras/5
        [ResponseType(typeof(Camera))]
        public IHttpActionResult GetCamera(int id)
        {
            Camera camera = db.Camera.Find(id);
            if (camera == null)
            {
                return NotFound();
            }

            return Ok(camera);
        }

        

        // PUT: api/Cameras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCamera(int id, Camera camera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != camera.ma_camera)
            {
                return BadRequest();
            }

            db.Entry(camera).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CameraExists(id))
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

        // POST: api/Cameras
        [ResponseType(typeof(Camera))]
        public IHttpActionResult PostCamera(Camera camera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Camera.Add(camera);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = camera.ma_camera }, camera);
        }

        // DELETE: api/Cameras/5
        [ResponseType(typeof(Camera))]
        public IHttpActionResult DeleteCamera(int id)
        {
            Camera camera = db.Camera.Find(id);
            if (camera == null)
            {
                return NotFound();
            }

            db.Camera.Remove(camera);
            db.SaveChanges();

            return Ok(camera);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CameraExists(int id)
        {
            return db.Camera.Count(e => e.ma_camera == id) > 0;
        }
    }
}