using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;


namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết")]
    public class BaoHiem_XuatThongTinImportBHXH_Item : BaseObject, ISupportController
    {
        private string _V2;
        private string _HoTen;
        private string _temp1;
        private string _TinhTrang;
        private string _MaTinhTrang;
        private string _MaPhongBan;
        private string _ChucVu;
        private string _MaNLD;
        private string _temp2;
        private string _MaSoSoBHXH;
        private string _MaTheBHYT;
        private string _DiaChiKhaiSinh;
        private string _DiaChiThuongTru;
        private string _DiaChiLienHe;
        private DateTime _NgaySinh;
        private string _ChiCoNamSinh;
        private string _TinhKhaiSinh;
        private string _HuyenKhaiSinh;
        private string _XaKhaiSinh;
        private string _MaTinhKhaiSinh;
        private string _MaHuyenKhaiSinh;
        private string _MaXaKhaiSinh;
        private string _CMND;
        private DateTime _NgayCapCMND;
        private string _NoiCapCMND;
        private string _GioiTinh;
        private string _QuocTich;
        private string _DanToc;
        private string _temp3;
        private string _temp4;
        private string _temp5;
        private string _temp6;
        private string _DiaChiThuongTru1;
        private string _TinhDiaChiThuongTru;
        private string _HuyenDiaChiThuongTru;
        private string _XaDiaChiThuongTru;
        private string _MaTinhDiaChiThuongTru;
        private string _MaHuyenDiaChiThuongTru;
        private string _MaXaDiaChiThuongTru;
        private string _DiaChiLienHe1;
        private string _TinhDiaChiLienHe;
        private string _HuyenDiaChiLienHe;
        private string _XaDiaChiLienHe;
        private string _MaTinhDiaChiLienHe;
        private string _MaHuyenDiaChiLienHe;
        private string _MaXaDiaChiLienHe;
        private string _DienThoai;
        private string _Email;
        private string _SoHopDong;
        private DateTime _NgayKyHopDong;
        private string _LoaiHopDong;
        private string _DienGiaiHopDong;
        private DateTime _NgayHieuLucHopDong;
        private string _DoiTuongThamGia;
        private string _TinhKhamChuaBenh;
        private string _MaTinhKhamChuaBenh;
        private string _BenhVien;
        private string _MaBenhVien;
        private string _DoiTuongThamGiaBHXH;
        private DateTime _NgayTraTheBHYT;
        private decimal _TienLuong;
        private decimal _HeSoLuong;
        private decimal _PCCV;
        private decimal _PCTNVK;
        private decimal _PCTNN;
        private decimal _PCKhac;
        private decimal _MucLuongPC;
        private decimal _MucLuongBS;
        private string _temp7;
        private string _MaNLD1;
        private string _MST;
        private string _SoTKNganHang;
        private string _TaiNganHang;
        private string _VungSinhSong;

        [ModelDefault("Caption", "V2")]
        public string V2
        {
            get
            {
                return _V2;
            }
            set
            {
                SetPropertyValue("V2", ref _V2, value);
            }
        }


        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp1
        {
            get
            {
                return _temp1;
            }
            set
            {
                SetPropertyValue("temp1", ref _temp1, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public string TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Mã tình trạng")]
        public string MaTinhTrang
        {
            get
            {
                return _MaTinhTrang;
            }
            set
            {
                SetPropertyValue("MaTinhTrang", ref _MaTinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Mã phòng ban")]
        public string MaPhongBan
        {
            get
            {
                return _MaPhongBan;
            }
            set
            {
                SetPropertyValue("MaPhongBan", ref _MaPhongBan, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public string ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Mã NLD")]
        public string MaNLD
        {
            get
            {
                return _MaNLD;
            }
            set
            {
                SetPropertyValue("MaNLD", ref _MaNLD, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp2
        {
            get
            {
                return _temp2;
            }
            set
            {
                SetPropertyValue("temp2", ref _temp2, value);
            }
        }

        [ModelDefault("Caption", "Mã số sổ BHXH")]
        public string MaSoSoBHXH
        {
            get
            {
                return _MaSoSoBHXH;
            }
            set
            {
                SetPropertyValue("MaSoSoBHXH", ref _MaSoSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "Mã thẻ BHYT")]
        public string MaTheBHYT
        {
            get
            {
                return _MaTheBHYT;
            }
            set
            {
                SetPropertyValue("MaTheBHYT", ref _MaTheBHYT, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ khai sinh (ngăn cách tỉnh huyện xã bởi dấu (-) hoặc (,))")]
        public string DiaChiKhaiSinh
        {
            get
            {
                return _DiaChiKhaiSinh;
            }
            set
            {
                SetPropertyValue("DiaChiKhaiSinh", ref _DiaChiKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ thường trú (ngăn cách tỉnh huyện xã bởi dấu (-) hoặc (,))")]
        public string DiaChiThuongTru
        {
            get
            {
                return _DiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru", ref _DiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ liên hệ(ngăn cách tỉnh huyện xã bởi dấu (-) hoặc (,))")]
        public string DiaChiLienHe
        {
            get
            {
                return _DiaChiLienHe;
            }
            set
            {
                SetPropertyValue("DiaChiLienHe", ref _DiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Chỉ có năm sinh")]
        public string ChiCoNamSinh
        {
            get
            {
                return _ChiCoNamSinh;
            }
            set
            {
                SetPropertyValue("ChiCoNamSinh", ref _ChiCoNamSinh, value);
            }
        }

        [ModelDefault("Caption", "Tỉnh khai sinh")]
        public string TinhKhaiSinh
        {
            get
            {
                return _TinhKhaiSinh;
            }
            set
            {
                SetPropertyValue("TinhKhaiSinh", ref _TinhKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Huyện khai sinh")]
        public string HuyenKhaiSinh
        {
            get
            {
                return _HuyenKhaiSinh;
            }
            set
            {
                SetPropertyValue("HuyenKhaiSinh", ref _HuyenKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Xã khai sinh")]
        public string XaKhaiSinh
        {
            get
            {
                return _XaKhaiSinh;
            }
            set
            {
                SetPropertyValue("XaKhaiSinh", ref _XaKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Mã tỉnh khai sinh")]
        public string MaTinhKhaiSinh
        {
            get
            {
                return _MaTinhKhaiSinh;
            }
            set
            {
                SetPropertyValue("MaTinhKhaiSinh", ref _MaTinhKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Mã huyện khai sinh")]
        public string MaHuyenKhaiSinh
        {
            get
            {
                return _MaHuyenKhaiSinh;
            }
            set
            {
                SetPropertyValue("MaHuyenKhaiSinh", ref _MaHuyenKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Mã xã khai sinh")]
        public string MaXaKhaiSinh
        {
            get
            {
                return _MaXaKhaiSinh;
            }
            set
            {
                SetPropertyValue("MaXaKhaiSinh", ref _MaXaKhaiSinh, value);
            }
        }

        [ModelDefault("Caption", "Số Chứng minh thư")]
        public string CMND
        {
            get
            {
                return _CMND;
            }
            set
            {
                SetPropertyValue("CMND", ref _CMND, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp CMT")]
        public DateTime NgayCapCMND
        {
            get
            {
                return _NgayCapCMND;
            }
            set
            {
                SetPropertyValue("NgayCapCMND", ref _NgayCapCMND, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public string NoiCapCMND
        {
            get
            {
                return _NoiCapCMND;
            }
            set
            {
                SetPropertyValue("NoiCapCMND", ref _NoiCapCMND, value);
            }
        }

        [ModelDefault("Caption", "Giới tính")]
        public string GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public string QuocTich
        {
            get
            {
                return _QuocTich;
            }
            set
            {
                SetPropertyValue("QuocTich", ref _QuocTich, value);
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public string DanToc
        {
            get
            {
                return _DanToc;
            }
            set
            {
                SetPropertyValue("DanToc", ref _DanToc, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp3
        {
            get
            {
                return _temp3;
            }
            set
            {
                SetPropertyValue("temp3", ref _temp3, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp4
        {
            get
            {
                return _temp4;
            }
            set
            {
                SetPropertyValue("temp4", ref _temp4, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp5
        {
            get
            {
                return _temp5;
            }
            set
            {
                SetPropertyValue("temp5", ref _temp5, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp6
        {
            get
            {
                return _temp6;
            }
            set
            {
                SetPropertyValue("temp6", ref _temp6, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ thường trú số nhà, đường … (*)")]
        public string DiaChiThuongTru1
        {
            get
            {
                return _DiaChiThuongTru1;
            }
            set
            {
                SetPropertyValue("DiaChiThuongTru1", ref _DiaChiThuongTru1, value);
            }
        }

        [ModelDefault("Caption", "Tỉnh địa chỉ thường trú (*)")]
        public string TinhDiaChiThuongTru
        {
            get
            {
                return _TinhDiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("TinhDiaChiThuongTru", ref _TinhDiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Huyện địa chỉ thường trú (*)")]
        public string HuyenDiaChiThuongTru
        {
            get
            {
                return _HuyenDiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("HuyenDiaChiThuongTru", ref _HuyenDiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Xã địa chỉ thường trú (*)")]
        public string XaDiaChiThuongTru
        {
            get
            {
                return _XaDiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("XaDiaChiThuongTru", ref _XaDiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Mã tỉnh địa chỉ thường trú (*)")]
        public string MaTinhDiaChiThuongTru
        {
            get
            {
                return _MaTinhDiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("MaTinhDiaChiThuongTru", ref _MaTinhDiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Mã huyện địa chỉ thường trú (*)")]
        public string MaHuyenDiaChiThuongTru
        {
            get
            {
                return _MaHuyenDiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("MaHuyenDiaChiThuongTru", ref _MaHuyenDiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Mã xã địa chỉ thường trú (*)")]
        public string MaXaDiaChiThuongTru
        {
            get
            {
                return _MaXaDiaChiThuongTru;
            }
            set
            {
                SetPropertyValue("MaXaDiaChiThuongTru", ref _MaXaDiaChiThuongTru, value);
            }
        }

        [ModelDefault("Caption", "Địa chỉ liên hệ số nhà, đường … (*)")]
        public string DiaChiLienHe1
        {
            get
            {
                return _DiaChiLienHe1;
            }
            set
            {
                SetPropertyValue("DiaChiLienHe1", ref _DiaChiLienHe1, value);
            }
        }

        [ModelDefault("Caption", "Tỉnh liên hệ (*)")]
        public string TinhDiaChiLienHe
        {
            get
            {
                return _TinhDiaChiLienHe;
            }
            set
            {
                SetPropertyValue("TinhDiaChiLienHe", ref _TinhDiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Huyện liên hệ (*)")]
        public string HuyenDiaChiLienHe
        {
            get
            {
                return _HuyenDiaChiLienHe;
            }
            set
            {
                SetPropertyValue("HuyenDiaChiLienHe", ref _HuyenDiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Xã liên hệ (*)")]
        public string XaDiaChiLienHe
        {
            get
            {
                return _XaDiaChiLienHe;
            }
            set
            {
                SetPropertyValue("XaDiaChiLienHe", ref _XaDiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Mã tỉnh liên hệ (*)")]
        public string MaTinhDiaChiLienHe
        {
            get
            {
                return _MaTinhDiaChiLienHe;
            }
            set
            {
                SetPropertyValue("MaTinhDiaChiLienHe", ref _MaTinhDiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Mã huyện liên hệ (*)")]
        public string MaHuyenDiaChiLienHe
        {
            get
            {
                return _MaHuyenDiaChiLienHe;
            }
            set
            {
                SetPropertyValue("MaHuyenDiaChiLienHe", ref _MaHuyenDiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Mã xã liên hệ (*)")]
        public string MaXaDiaChiLienHe
        {
            get
            {
                return _MaXaDiaChiLienHe;
            }
            set
            {
                SetPropertyValue("MaXaDiaChiLienHe", ref _MaXaDiaChiLienHe, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại")]
        public string DienThoai
        {
            get
            {
                return _DienThoai;
            }
            set
            {
                SetPropertyValue("DienThoai", ref _DienThoai, value);
            }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                SetPropertyValue("Email", ref _Email, value);
            }
        }

        [ModelDefault("Caption", "Số hợp đồng")]
        public string SoHopDong
        {
            get
            {
                return _SoHopDong;
            }
            set
            {
                SetPropertyValue("SoHopDong", ref _SoHopDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày ký hợp đồng")]
        public DateTime NgayKyHopDong
        {
            get
            {
                return _NgayKyHopDong;
            }
            set
            {
                SetPropertyValue("NgayKyHopDong", ref _NgayKyHopDong, value);
            }
        }

        [ModelDefault("Caption", "Loại hợp đồng")]
        public string LoaiHopDong
        {
            get
            {
                return _LoaiHopDong;
            }
            set
            {
                SetPropertyValue("LoaiHopDong", ref _LoaiHopDong, value);
            }
        }

        [ModelDefault("Caption", "Diễn giải hợp đồng")]
        public string DienGiaiHopDong
        {
            get
            {
                return _DienGiaiHopDong;
            }
            set
            {
                SetPropertyValue("DienGiaiHopDong", ref _DienGiaiHopDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày hiệu lực hợp đồng")]
        public DateTime NgayHieuLucHopDong
        {
            get
            {
                return _NgayHieuLucHopDong;
            }
            set
            {
                SetPropertyValue("NgayHieuLucHopDong", ref _NgayHieuLucHopDong, value);
            }
        }

        [ModelDefault("Caption", "Đối tượng tham gia")]
        public string DoiTuongThamGia
        {
            get
            {
                return _DoiTuongThamGia;
            }
            set
            {
                SetPropertyValue("DoiTuongThamGia", ref _DoiTuongThamGia, value);
            }
        }

        [ModelDefault("Caption", "Tỉnh khám chữa bệnh")]
        public string TinhKhamChuaBenh
        {
            get
            {
                return _TinhKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("TinhKhamChuaBenh", ref _TinhKhamChuaBenh, value);
            }
        }

        [ModelDefault("Caption", "Mã tỉnh KCB")]
        public string MaTinhKhamChuaBenh
        {
            get
            {
                return _MaTinhKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("MaTinhKhamChuaBenh", ref _MaTinhKhamChuaBenh, value);
            }
        }

        [ModelDefault("Caption", "Bệnh viện")]
        public string BenhVien
        {
            get
            {
                return _BenhVien;
            }
            set
            {
                SetPropertyValue("BenhVien", ref _BenhVien, value);
            }
        }

        [ModelDefault("Caption", "Mã bệnh viện")]
        public string MaBenhVien
        {
            get
            {
                return _MaBenhVien;
            }
            set
            {
                SetPropertyValue("MaBenhVien", ref _MaBenhVien, value);
            }
        }

        [ModelDefault("Caption", "Đối tượng tham gia BHXH")]
        public string DoiTuongThamGiaBHXH
        {
            get
            {
                return _DoiTuongThamGiaBHXH;
            }
            set
            {
                SetPropertyValue("DoiTuongThamGiaBHXH", ref _DoiTuongThamGiaBHXH, value);
            }
        }

        [ModelDefault("Caption", "Ngày trả thẻ BHYT")]
        public DateTime NgayTraTheBHYT
        {
            get
            {
                return _NgayTraTheBHYT;
            }
            set
            {
                SetPropertyValue("NgayTraTheBHYT", ref _NgayTraTheBHYT, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TienLuong
        {
            get
            {
                return _TienLuong;
            }
            set
            {
                SetPropertyValue("TienLuong", ref _TienLuong, value);
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }


        [ModelDefault("Caption", "Phụ cấp chức vụ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal PCCV
        {
            get
            {
                return _PCCV;
            }
            set
            {
                SetPropertyValue("PCCV", ref _PCCV, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp trách nhiệm vượt khung")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal PCTNVK
        {
            get
            {
                return _PCTNVK;
            }
            set
            {
                SetPropertyValue("PCTNVK", ref _PCTNVK, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp thâm niên nghề")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal PCTNN
        {
            get
            {
                return _PCTNN;
            }
            set
            {
                SetPropertyValue("PCTNN", ref _PCTNN, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp khác")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal PCKhac
        {
            get
            {
                return _PCKhac;
            }
            set
            {
                SetPropertyValue("PCKhac", ref _PCKhac, value);
            }
        }

        [ModelDefault("Caption", "Mức lương phụ cấp")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal MucLuongPC
        {
            get
            {
                return _MucLuongPC;
            }
            set
            {
                SetPropertyValue("MucLuongPC", ref _MucLuongPC, value);
            }
        }

        [ModelDefault("Caption", "Mức lương bổ sung")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal MucLuongBS
        {
            get
            {
                return _MucLuongBS;
            }
            set
            {
                SetPropertyValue("MucLuongBS", ref _MucLuongBS, value);
            }
        }

        [ModelDefault("Caption", " ")]
        public string temp7
        {
            get
            {
                return _temp7;
            }
            set
            {
                SetPropertyValue("temp7", ref _temp7, value);
            }
        }

        [ModelDefault("Caption", "Mã NLĐ")]
        public string MaNLD1
        {
            get
            {
                return _MaNLD1;
            }
            set
            {
                SetPropertyValue("MaNLD1", ref _MaNLD1, value);
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MST
        {
            get
            {
                return _MST;
            }
            set
            {
                SetPropertyValue("MST", ref _MST, value);
            }
        }

        [ModelDefault("Caption", "Số tài khoản ngân hàng")]
        public string SoTKNganHang
        {
            get
            {
                return _SoTKNganHang;
            }
            set
            {
                SetPropertyValue("SoTKNganHang", ref _SoTKNganHang, value);
            }
        }

        [ModelDefault("Caption", "Tại ngân hàng")]
        public string TaiNganHang
        {
            get
            {
                return _TaiNganHang;
            }
            set
            {
                SetPropertyValue("TaiNganHang", ref _TaiNganHang, value);
            }
        }

        [ModelDefault("Caption", "Vùng sinh sống")]
        public string VungSinhSong
        {
            get
            {
                return _VungSinhSong;
            }
            set
            {
                SetPropertyValue("VungSinhSong", ref _VungSinhSong, value);
            }
        }

        public BaoHiem_XuatThongTinImportBHXH_Item(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
