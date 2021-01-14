using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTraCuuCamera.Models
{
    public class Camera_Backup
    {
        public int ma_backup { get; set; }
        public DateTime thoi_gian { get; set; }
        public string images { get; set; }

        public int ma_camera { get; set; }

        public string TenDuong { get; set; }

        //public virtual Camera Camera { get; set; }
    }
}