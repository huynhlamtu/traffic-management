using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webadmin.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Webadmin.Models.QL;

namespace Webadmin.Controllers
{
    public class TraCuuChiTietDuongController : Controller
    {
        // GET: TraCuuCamera
        [HttpGet]
        public ViewResult Index()
        {
            List<Duongs> List_Duong = new List<Duongs>();


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var respornseTask = client.GetAsync("Duongs");
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var m = result.Content.ReadAsAsync<List<Duongs>>();
                    m.Wait();
                    List_Duong = m.Result;
                }
                else
                {
                    List_Duong.DefaultIfEmpty<Duongs>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");
                }

            }
            return View(List_Duong);
        }

        [HttpGet]
        public ActionResult TimKiemThongTinDuong(string TenDuong)
        {
            List<CT_Duong> List_CT_Duong = new List<CT_Duong>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var link = "CT_Duong/" + TenDuong;
                var respornseTask = client.GetAsync(link);
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var m = result.Content.ReadAsAsync<List<CT_Duong>>();
                    m.Wait();

                    List_CT_Duong = m.Result;
                }
                else
                {
                    List_CT_Duong.DefaultIfEmpty<CT_Duong>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }
            }
            foreach ( var name in List_CT_Duong)
            {
                name.name = TenDuong;
            }    
            return View(List_CT_Duong);
        }
    }
    

}