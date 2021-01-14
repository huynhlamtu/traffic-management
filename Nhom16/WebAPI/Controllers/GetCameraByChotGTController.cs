using DataRepository;
using DataRepository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace WebAPI.Controllers
{
    public class GetCameraByChotGTController : ApiController
    {
        private DataContext db = new DataContext();
        [ResponseType(typeof(Camera))]
        public IHttpActionResult GetCamerabymachotGiaoThong(int id)
        {
            List<NgaDuong> nga = (from ct in db.NgaDuong where ct.ma_chot_GT == id select ct).ToList();

            List<Camera> list = new List<Camera>();
            foreach (var item in nga)
            {
                List<Camera> tem = (from ct in db.Camera where ct.ma_nga_duong == item.ma_nga_duong select ct).ToList();
                foreach (var a in tem)
                {
                    list.Add(a);
                }

            }
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }
    }
}
