using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webadmin.Models.DKDen;

namespace Webadmin.Controllers
{
    public class DieuKhienDenGiaoThongController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            IEnumerable<Duongs> duong = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var respornseTask = client.GetAsync("Duongs");
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chotGT = result.Content.ReadAsAsync<List<Duongs>>();
                    chotGT.Wait();
                    duong = chotGT.Result.OrderBy(s => s.ten_duong);
                }
                else
                {
                    duong = Enumerable.Empty<Duongs>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }

            }

            viewDKChotGT chotGiaoThongDetail = new viewDKChotGT();
            chotGiaoThongDetail.Duongs = duong;
            return View(chotGiaoThongDetail);
        }

        [HttpPost]
        public ActionResult Index(FormCollection ten_chotGT)
        {
            viewDKChotGT chotGiaoThongDetail = new viewDKChotGT();
            var a = ten_chotGT.Get("duong1").ToString();
            var b = ten_chotGT.Get("duong2").ToString();
            if (a == b)
            {
                var mes = "Không có cốt giao thông nào là: " + a + " - " + b + " hết!!!!";
                IEnumerable<Duongs> duong = null;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44327/api/");
                    var respornseTask = client.GetAsync("Duongs");
                    respornseTask.Wait();

                    var result = respornseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var chotGTss = result.Content.ReadAsAsync<List<Duongs>>();
                        chotGTss.Wait();
                        duong = chotGTss.Result.OrderBy(s => s.ten_duong);
                    }
                    else
                    {
                        duong = Enumerable.Empty<Duongs>();
                        ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                    }

                }
                chotGiaoThongDetail.Duongs = duong;
                chotGiaoThongDetail.mes = mes;

                return View(chotGiaoThongDetail);
            }
            else
            {
                List<ChotGiaoThongs> chot = null;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44327/api/");
                    var link = "ChotGiaoThongs?name1=" + a + "&name2=" + b;
                    var respornseTask = client.GetAsync(link);
                    respornseTask.Wait();

                    var result = respornseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var chotGTs = result.Content.ReadAsAsync<List<ChotGiaoThongs>>();
                        chotGTs.Wait();
                        chot = chotGTs.Result;
                    }
                    else
                    {
                        chot = null;
                        ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                    }

                }
                if (chot == null)
                {
                    var mes = "Không có cốt giao thông nào là: " + a + " - " + b + " hết!!!!";
                    IEnumerable<Duongs> duong = null;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44327/api/");
                        var respornseTask = client.GetAsync("Duongs");
                        respornseTask.Wait();

                        var result = respornseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var chotGTss = result.Content.ReadAsAsync<List<Duongs>>();
                            chotGTss.Wait();
                            duong = chotGTss.Result.OrderBy(s => s.ten_duong);
                        }
                        else
                        {
                            duong = Enumerable.Empty<Duongs>();
                            ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                        }

                    }
                    chotGiaoThongDetail.Duongs = duong;
                    chotGiaoThongDetail.mes = mes;
                }
                else
                {
                    var chotGT = chot[0];
                    chotGiaoThongDetail = GetDKChotGT(chotGT.ma_chotGT);
                }
            }
            return View(chotGiaoThongDetail);

        }

        [HttpPost]
        public ActionResult Index1(FormCollection ten_chotGT)
        {
            viewDKChotGT chotGiaoThongDetail = new viewDKChotGT();
            
            var temp = ten_chotGT.Get("ma_nga_duong").ToString();
            int a = Int32.Parse(temp);
            ChotGiaoThongDetail chotdetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var link = "ChotGiaoThongDetail/" + a;
                var respornseTask = client.GetAsync(link);
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chotGTs = result.Content.ReadAsAsync<ChotGiaoThongDetail>();
                    chotGTs.Wait();
                    chotdetail = chotGTs.Result;
                }
                else
                {
                    chotdetail = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }

            }
            foreach (NgaDuong nga in chotdetail.ngaDuongs)
            {
                foreach (CTDenGiaoThong ct in nga.cTDens)
                {
                    int ktden = 0;
                    switch (nga.stt)
                    {
                        case 1:
                            string temp1 = ten_chotGT.Get("duong1" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp1);
                            break;
                        case 2:
                            string temp2 = ten_chotGT.Get("duong2" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp2);
                            break;
                        case 3:
                            string temp3 = ten_chotGT.Get("duong3" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp3);
                            break;
                        case 4:
                            string temp4 = ten_chotGT.Get("duong4" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp4);
                            break;
                        case 6:
                            string temp6 = ten_chotGT.Get("duong6" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp6);
                            break;
                        case 7:
                            string temp7 = ten_chotGT.Get("duong7" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp7);
                            break;
                        case 8:
                            string temp8 = ten_chotGT.Get("duong8" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp8);
                            break;
                        case 9:
                            string temp9 = ten_chotGT.Get("duong9" + ct.ma_den).ToString();
                            ktden = Int32.Parse(temp9);
                            break;
                    }

                    switch (ktden)
                    {
                        case 1:
                            ct.vang = 0;
                            ct.xanh = 1;
                            ct.do_ = 0;
                            break;
                        case 2:
                            ct.vang = 0;
                            ct.xanh = 0;
                            ct.do_ = 1;
                            break;

                    }

                }


            }
            //post
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/ChotGiaoThongDetail");

                var postchotgiaothong = client.PostAsJsonAsync<ChotGiaoThongDetail>("ChotGiaoThongDetail", chotdetail);
                postchotgiaothong.Wait();
                var result = postchotgiaothong.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chotGTs = result.Content.ReadAsAsync<ChotGiaoThongDetail>();
                    chotGTs.Wait();
                    chotdetail = chotGTs.Result;
                }
                else
                {
                    chotdetail = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }


            }

            if (chotdetail == null)
            {
                chotGiaoThongDetail.mes = "Cập nhật Không Thành Công";
            }
            else
            {
                chotGiaoThongDetail = GetDKChotGT(a);
            }

                
            chotGiaoThongDetail = GetDKChotGT(a);
           
            return View(chotGiaoThongDetail);
        }


        public viewDKChotGT GetDKChotGT(int a)
        {
            viewDKChotGT chotGiaoThongDetail = new viewDKChotGT();



            ChotGiaoThongDetail chotdetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var link = "ChotGiaoThongDetail/" + a;
                var respornseTask = client.GetAsync(link);
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chotGTs = result.Content.ReadAsAsync<ChotGiaoThongDetail>();
                    chotGTs.Wait();
                    chotdetail = chotGTs.Result;
                }
                else
                {
                    chotdetail = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }

            }

            IEnumerable<Duongs> duong = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var respornseTask = client.GetAsync("Duongs");
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chotGTss = result.Content.ReadAsAsync<List<Duongs>>();
                    chotGTss.Wait();
                    duong = chotGTss.Result.OrderBy(s => s.ten_duong);
                }
                else
                {
                    duong = Enumerable.Empty<Duongs>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }

            }
            chotGiaoThongDetail.Duongs = duong;

            foreach (NgaDuong ngaDuong in chotdetail.ngaDuongs)
            {
                switch (ngaDuong.stt)
                {
                    case 1:
                        chotGiaoThongDetail.ngaDuong1 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong1.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 2:
                        chotGiaoThongDetail.ngaDuong2 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong2.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 3:
                        chotGiaoThongDetail.ngaDuong3 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong3.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 4:
                        chotGiaoThongDetail.ngaDuong4 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong4.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 6:
                        chotGiaoThongDetail.ngaDuong6 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong6.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 7:
                        chotGiaoThongDetail.ngaDuong7 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong7.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 8:
                        chotGiaoThongDetail.ngaDuong8 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong8.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                    case 9:
                        chotGiaoThongDetail.ngaDuong9 = ngaDuong;
                        chotGiaoThongDetail.ngaDuong9.tenDuong = getNameDuong(ngaDuong.ma_CT_duong);
                        break;
                }
            }
            chotGiaoThongDetail.ma_nga_duong = chotdetail.ngaDuongs[0].ma_chot_GT;


            List<Camera> listcameras = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var respornseTask = client.GetAsync("GetCameraByChotGT/" + a);
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var chotGTss = result.Content.ReadAsAsync<List<Camera>>();
                    chotGTss.Wait();
                    listcameras = chotGTss.Result;
                }
                else
                {
                    listcameras = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }

            }
            if (listcameras.Count() == 0)
            {
                chotGiaoThongDetail.Cameras = null;
            }
            else
            {
                int count = 0;
                chotGiaoThongDetail.Cameras = listcameras;
                foreach (var te in chotGiaoThongDetail.Cameras)
                {
                    if (count == 0)
                    {
                        te.fisrt = "active";

                    }
                    count++;
                    if (chotGiaoThongDetail.ngaDuong1 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong1.ma_nga_duong)
                    {
                        te.tenNga = chotGiaoThongDetail.ngaDuong1.stt;
                    }
                    else
                    {
                        if (chotGiaoThongDetail.ngaDuong2 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong2.ma_nga_duong)
                        {
                            te.tenNga = chotGiaoThongDetail.ngaDuong2.stt;
                        }
                        else
                        {
                            if (chotGiaoThongDetail.ngaDuong3 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong3.ma_nga_duong)
                            {
                                te.tenNga = chotGiaoThongDetail.ngaDuong3.stt;
                            }
                            else
                            {
                                if (chotGiaoThongDetail.ngaDuong4 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong4.ma_nga_duong)
                                {
                                    te.tenNga = chotGiaoThongDetail.ngaDuong4.stt;
                                }
                                else
                                {
                                    if (chotGiaoThongDetail.ngaDuong6 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong6.ma_nga_duong)
                                    {
                                        te.tenNga = chotGiaoThongDetail.ngaDuong6.stt;
                                    }
                                    else
                                    {
                                        if (chotGiaoThongDetail.ngaDuong7 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong7.ma_nga_duong)
                                        {
                                            te.tenNga = chotGiaoThongDetail.ngaDuong7.stt;
                                        }
                                        else
                                        {
                                            if (chotGiaoThongDetail.ngaDuong8 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong8.ma_nga_duong)
                                            {
                                                te.tenNga = chotGiaoThongDetail.ngaDuong8.stt;
                                            }
                                            else
                                            {
                                                if (chotGiaoThongDetail.ngaDuong9 != null && te.ma_nga_duong == chotGiaoThongDetail.ngaDuong9.ma_nga_duong)
                                                {
                                                    te.tenNga = chotGiaoThongDetail.ngaDuong9.stt;
                                                }
                                                else
                                                {
                                                    te.tenNga = 0;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            chotGiaoThongDetail.Cameras = chotGiaoThongDetail.Cameras.OrderBy(s => s.tenNga).ToList();
            return chotGiaoThongDetail;
        }
        public string getNameDuong(int id)
        {
            Duongs duongs = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44327/api/");
                var link = "GetNameDuongByMaCTDuong/" + id;
                var respornseTask = client.GetAsync(link);
                respornseTask.Wait();

                var result = respornseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var duons = result.Content.ReadAsAsync<Duongs>();
                    duons.Wait();
                    duongs = duons.Result;
                }
                else
                {
                    duongs = null;
                    ModelState.AddModelError(string.Empty, "Server error. Please contract admin for help");

                }

            }
            return duongs.ten_duong;
        }


    }

    
}