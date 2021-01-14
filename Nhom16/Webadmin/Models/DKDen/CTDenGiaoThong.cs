using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webadmin.Models.DKDen
{
    public class CTDenGiaoThong
    {
        public int ma_ct_den { get; set; }
        public int do_ { get; set; }
        public int xanh { get; set; }
        public int vang { get; set; }

        public int ma_den { get; set; }
        public int ma_nga_duong { get; set; }
        public string link { get; set; }
    }
}