use master
go
create database QuanLyGiaoThong
go
use QuanLyGiaoThong
go
create table DenGiaoThong
(
	ma_den int identity primary key,
	ten_den Nvarchar(100),
	link varchar(max)

)
create table ChotGiaoThong
(
	ma_ChotGT int identity primary key,
	ten_chot_GT nvarchar(100),
	edit int DEFAULT 0,
	
)
create table Duong
(
	ma_duong int identity primary key,
	ten_duong nvarchar(100)
)

create table CT_Duong
(
	Ma_CT_duong int identity primary key,
	ma_duong int,
	phuong nvarchar(50),
	quan nvarchar(50),

	
)
create table NgaDuong
(
	ma_nga_duong int identity primary key,
	ma_chot_GT int,
	ma_CT_duong int,
	stt int,

	constraint fk_NgaDuongs_ChotGiaoThongs foreign key (ma_chot_GT) references ChotGiaoThong(ma_ChotGT),
	constraint fk_NgaDuongs_CTDuongs foreign key (ma_CT_duong) references CT_Duong(ma_CT_duong)
)
create table CTDenGiaoThong
(
	ma_ct_den int identity primary key,
	ma_den int,
	ma_nga_duong int,
	do_ int DEFAULT 0,
	xanh int DEFAULT 0,
	vang int DEFAULT 0,
	link varchar(max) ,
	constraint fk_CTDenTinHieus_DenTinHieus foreign key (ma_den) references DenGiaoThong(ma_den),
	constraint fk_CTDenTinHieus_NgaDuongs foreign key (ma_nga_duong) references NgaDuong(ma_nga_duong)
)

create table Camera
(
	ma_camera int identity primary key,
	ip_ nvarchar(16),
	images nvarchar(200),
	ma_nga_duong int,
	ma_duong int,
	stt int

	constraint fk_Cameras_NgaDuongs foreign key (ma_nga_duong) references NgaDuong(ma_nga_duong),
	constraint fk_Cameras_Duongs foreign key (ma_duong) references Duong(ma_duong)
)

create table TraCuuThongTin
(
	MaTraCuu int identity primary key,
	ma_duong int,
	ThoiGian date,
	images nvarchar(200),

	constraint fk_TraCuuThongTin_Duongs foreign key (ma_duong) references Duong (ma_duong)
)
create table Camera_Backup
(
	ma_backup int identity primary key,
	thoi_gian date,
	ma_camera int,
	images Nvarchar(200),
	TenDuong Nvarchar(200),

	constraint fk_CameraBackup_Cameras foreign key (ma_camera) references Camera(ma_camera)
)

create table DonCongAn
(
	ma_don_cong_an int identity primary key,
	ten_don_cong_an Nvarchar(200),
	dia_chi nvarchar(200)
)


create table CongAn
(
	ma_cong_an int identity primary key,
	email Nvarchar(100),
	pass_word Nvarchar(20),
	ho_ten Nvarchar(100),
	dia_chi nvarchar(200),
	sdt Nvarchar(15),
	cmnd Nvarchar(15),
	ngaysinh date,
	gioi_tinh Nvarchar(3),
	chuc_vu Nvarchar(50),
	noi_cong_tac int

	constraint fk_CongAns_DonCongAns foreign key (noi_cong_tac) references DonCongAn(ma_don_cong_an)
)

create table Dan
(
	ma_dan int identity primary key,
	email Nvarchar(100),
	pass_word Nvarchar(20),
	ho_ten Nvarchar(100),
	dia_chi nvarchar(200),
	sdt Nvarchar(15),
	cmnd Nvarchar(15),
	ngaysinh date,
	gioi_tinh Nvarchar(3)
)

create table DanDangKy
(
	ma_dang_ky int identity primary key,
	email Nvarchar(100),
	pass_word Nvarchar(20),
	ho_ten Nvarchar(100),
	dia_chi nvarchar(200),
	sdt varchar(15),
	cmnd varchar(15),
	ngaysinh date,
	gioi_tinh varchar(3),
	nguoi_duyet int

	constraint fk_DanDangKys_CongAns foreign key (nguoi_duyet) references CongAn(ma_cong_an)
)

create table Luat
(
	ma_luat int identity primary key,
	ten_luat Nvarchar(100),
	noi_dung nvarchar(200),
	ngay_ban_hanh date,
	muc_xu_phat int
)

create table Xe
(
	bien_so_xe Nvarchar(20) primary key,
	chu_xe int,
	nhan_hieu Nvarchar(100),
	mau_sac nvarchar(100),
	so_khung Nvarchar(20),
	loai_phuong_tien nvarchar(50),
	so_may Nvarchar(20),
	kich_thuoc_bao float,
	kich_thuoc_thung_hang float,
	khoi_luong_xe float,
	khoi_luong_cho_phep float,
	so_nguoi_cho_phep smallint,
	khoi_luong_toan_bo_cho_phep float,
	don_vi_kiem_dinh int,
	ngay_kiem_dinh datetime,
	so_ten_GCN Nvarchar(20),
	hinh_anh_xe Nvarchar(100)

	constraint fk_Xes_DonCongAns foreign key (don_vi_kiem_dinh) references DonCongAn(ma_don_cong_an),
	constraint fk_Xess_Dans foreign key (chu_xe) references Dan(ma_dan)
)

create table XeDangKy
(
	ma_dang_ky int identity primary key,
	chu_xe int,
	nhan_hieu Nvarchar(100),
	loai_phuong_tien Nvarchar(50),
	hinh_anh_xe Nvarchar(100),
	nguoi_duyet int

	constraint fk_Xes_Dans foreign key (chu_xe) references Dan(ma_dan),
	constraint fk_Xes_CongAns foreign key (nguoi_duyet) references CongAn(ma_cong_an),
)

create table DoiChuXe
(
	ma_dang_ky int identity primary key,
	bien_so_xe Nvarchar(20),
	chu_xe_cu int,
	chu_xe_moi int,
	ngay_chuyen_nhuong date,
	nguoi_duyet int

	constraint fk_Xes_ChuXeCus foreign key (chu_xe_cu) references Dan(ma_dan),
	constraint fk_Xes_ChuXeMois foreign key (chu_xe_moi) references Dan(ma_dan),
	constraint fk_DoiChuXes_Xes foreign key (bien_so_xe) references Xe(bien_so_xe),
	constraint fk_DoiChuXes_CongAns foreign key (nguoi_duyet) references CongAn(ma_cong_an),
)

create table ViPham
(
	ma_vi_pham int identity primary key,
	nguoi_vi_pham int,
	nguoi_xu_phat int,
	xe_vi_pham Nvarchar(20),
	[dia_diem_vi_pham] [nvarchar](50),
	tien_phat_them int,
	tong_tien_phat int,
	thoi_gian_vi_pham datetime,
	thoi_gian_xu_phat datetime,
	noi_giam_giu_xe int,
	flag_da_nop_phat bit

	constraint fk_ViPhams_Dans foreign key (nguoi_vi_pham) references Dan(ma_dan),
	constraint fk_ViPhams_CongAns foreign key (nguoi_xu_phat) references CongAn(ma_cong_an),
	constraint fk_ViPhams_Xes foreign key (xe_vi_pham) references Xe(bien_so_xe),
	constraint fk_ViPhams_DonCongAns foreign key (noi_giam_giu_xe) references DonCongAn(ma_don_cong_an)
)

create table ViPhamLuat
(
	ma_vi_pham_luat int identity primary key,
	ma_luat int,
	ma_vi_pham int,
	mo_ta_vi_pham Nvarchar(100)

	constraint fk_ViPhamLuats_Luats foreign key (ma_luat) references Luat(ma_luat),
	constraint fk_ViPhamLuats_ViPhams foreign key (ma_vi_pham) references ViPham(ma_vi_pham)
)

create table PhieuNopPhat
(
	[ma_phieu] [int] IDENTITY(1,1) NOT NULL,
	[ma_vi_pham] [int] NULL,
	[tien_phat] [int] NULL,
	[tien_ship] [int] NULL,
	[tong_tien_nop] [int] NULL,
	[phuong_thuc_dong_phat] [nvarchar](50) NULL,
	[flag_ship] [tinyint] NULL,
	[dia_chi_ship] [nvarchar](200) NULL,
	[ngay_nop_phat] [datetime] NULL,
	[flag_da_nhan_xe] [tinyint] NULL,

	constraint fk_PhieuNopPhats_ViPhams foreign key (ma_vi_pham) references ViPham(ma_vi_pham)
)