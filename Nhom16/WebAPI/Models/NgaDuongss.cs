﻿using DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class NgaDuongss
    {
        public int ma_nga_duong { get; set; }
        public int stt { get; set; }

        public int ma_chot_GT { get; set; }
        public int ma_CT_duong { get; set; }

        public List<CTDenGiaoThong> listDen { set; get; }
    }
}