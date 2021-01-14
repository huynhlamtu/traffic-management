using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Webadmin.Models.DKDen;


namespace Webadmin.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public ViewResult Index()
        {
            
            return View();
        }

    }
}