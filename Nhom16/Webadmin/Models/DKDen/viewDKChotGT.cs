using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webadmin.Models.DKDen
{
    public class viewDKChotGT
    {
        public string mes { set; get; }
        public NgaDuong ngaDuong1 { set; get; }
        public NgaDuong ngaDuong2 { set; get; }
        public NgaDuong ngaDuong3 { set; get; }
        public NgaDuong ngaDuong4 { set; get; }
        public NgaDuong ngaDuong6 { set; get; }
        public NgaDuong ngaDuong7 { set; get; }
        public NgaDuong ngaDuong8 { set; get; }
        public NgaDuong ngaDuong9 { set; get; }
        public IEnumerable<Duongs> Duongs { set; get; }
        public List<Camera> Cameras { set; get; }
        public int ma_nga_duong { set; get; }
        public int useScrip { set; get; }
        public int dieuKhien { set; get; }
    }
}