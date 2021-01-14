using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webadmin.Models.DKDen
{
    public class Camera
    {
        public int ma_camera { get; set; }
        public string ip_ { get; set; }
        public string images { get; set; }
        public int stt { get; set; }

        public int ma_nga_duong { get; set; }
        public int ma_duong { get; set; }
        public int tenNga { get; set; }

        public string fisrt { get; set; }
    }
}