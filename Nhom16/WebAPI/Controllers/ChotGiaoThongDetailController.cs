using DataRepository;
using DataRepository.Context;
using DataRepository.entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ChotGiaoThongDetailController : ApiController
    {
        private DataContext db = new DataContext();

        [ResponseType(typeof(ChotGiaoThongDetail))]
        public IHttpActionResult GetChotGTDetail(int id)
        {
            List<NgaDuong> ngaDuong = (from nd in db.NgaDuong where nd.ma_chot_GT == id select nd).ToList();
            
            ChotGiaoThongDetail chotGiaoThongDetail = new ChotGiaoThongDetail();
            chotGiaoThongDetail.ngaDuongs = ngaDuong;
            foreach(var ctduong in chotGiaoThongDetail.ngaDuongs)
            {
                ctduong.cTDens = (from ct in db.CTDenGiaoThong where ct.ma_nga_duong == ctduong.ma_nga_duong select ct).ToList();
                foreach (var Ct in ctduong.cTDens)
                {
                    var den = db.DenGiaoThong.Find(Ct.ma_den);
                    Ct.link = den.link;
                }    
            }    
            return Ok(chotGiaoThongDetail);
        }

        
        [ResponseType(typeof(ChotGiaoThongDetail))]
        public IHttpActionResult Postchotgiaothongdetail(ChotGiaoThongDetail chotGiaoThongDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach(NgaDuong nga in chotGiaoThongDetail.ngaDuongs)
            {
                foreach(CTDenGiaoThong ct in nga.cTDens)
                {
                    CTDenGiaoThong temp = db.CTDenGiaoThong.Find(ct.ma_ct_den);
                    temp.vang = ct.vang;
                    temp.xanh = ct.xanh;
                    temp.do_ = ct.do_;
                    db.Entry(temp).State = EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (db.CTDenGiaoThong.Count(e => e.ma_ct_den == temp.ma_ct_den) == 0)
                        {
              
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }    
            }
            var kq = GetChotGTDetail(chotGiaoThongDetail.ngaDuongs[0].ma_chot_GT);
            return kq;
        }


    }
}
