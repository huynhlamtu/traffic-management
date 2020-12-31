﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.entity
{
    public class CongAn
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int ma_cong_an { get; set; }
		public string email { get; set; }
		public string pass_word { get; set; }
		public string ho_ten { get; set; }
		public string dia_chi { get; set; }
		public string sdt { get; set; }
		public string cmnd { get; set; }
		public DateTime ngaysinh { get; set; }
		public string gioi_tinh { get; set; }
		public string chuc_vu { get; set; }

		public int noi_cong_tac { get; set; }

		//public virtual DonCongAn noi_cong_tac { get; set; }
	}
}
