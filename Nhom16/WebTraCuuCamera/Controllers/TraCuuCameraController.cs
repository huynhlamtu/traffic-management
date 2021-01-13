using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebTraCuuCamera.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace WebTraCuuCamera.Controllers
{
    public class TraCuuCameraController : Controller
    {
        // GET: TraCuuCamera
        [HttpGet]
        public ViewResult Index()
        {
            List<Camera_Backup> List_Camera_Backup = new List<Camera_Backup>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var respornseTask = client.GetAsync("Camera_Backup");
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var m = result.Content.ReadAsAsync<List<Camera_Backup>>();
                    m.Wait();
                    List_Camera_Backup = m.Result;
                }
                else
                {
                    List_Camera_Backup.DefaultIfEmpty<Camera_Backup>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");
                }

            }
            return View(List_Camera_Backup);
        }

        [HttpGet]
        public ActionResult TimKiemThongTin(string TenDuong, string thoi_gian)
        {
            List<Camera_Backup> List_Camera_Backup = new List<Camera_Backup>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var link = "Camera_Backup/" + TenDuong + "/" + thoi_gian;
                var respornseTask = client.GetAsync(link);
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var m = result.Content.ReadAsAsync<List<Camera_Backup>>();
                    m.Wait();

                    List_Camera_Backup = m.Result;
                }
                else
                {
                    List_Camera_Backup.DefaultIfEmpty<Camera_Backup>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }
            }
            return View(List_Camera_Backup);
        }
    }
}