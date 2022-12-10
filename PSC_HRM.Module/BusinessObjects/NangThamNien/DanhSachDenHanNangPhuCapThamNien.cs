using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using System.Collections.Generic;

namespace PSC_HRM.Module.NangThamNien
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đến hạn nâng phụ cấp thâm niên")]
    public class DanhSachDenHanNangPhuCapThamNien : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }   

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<DenHanNangPhuCapThamNien> ThamNienGiangVienList { get; set; }

        public DanhSachDenHanNangPhuCapThamNien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThamNienGiangVienList = new XPCollection<DenHanNangPhuCapThamNien>(Session, false);
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = HamDungChung.SetTime(current, 2);
            DenNgay = HamDungChung.SetTime(current, 3);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue &&
                DenNgay != DateTime.MinValue &&
                TuNgay < DenNgay)
            {
                CriteriaOperator filter;
                if (TruongConfig.MaTruong.Equals("BUH"))
                { 
                    filter = CriteriaOperator.Parse("NhanVienThongTinLuong.NgachLuong.TenNgachLuong like '%Giảng viên%' and NhanVienThongTinLuong.NgayHuongThamNien is not null and TinhTrang.TenTinhTrang not like ? and TinhTrang.KhongConCongTacTaiTruong = 0", "%không hưởng lương%"); 
                }
                else if (TruongConfig.MaTruong.Equals("DLU"))
                { 
                    filter = CriteriaOperator.Parse("(LoaiNhanSu.TenLoaiNhanSu like '%Giảng viên%' or ThamGiaGiangDay = 1) and NhanVienThongTinLuong.NgayHuongThamNien is not null and TinhTrang.TenTinhTrang not like ? and TinhTrang.KhongConCongTacTaiTruong = 0", "%không hưởng lương%"); 
                }
                else if (TruongConfig.MaTruong.Equals("UEL"))
                {
                    filter = CriteriaOperator.Parse("NgayVaoNganhGiaoDuc is not null and (CongViecHienNay.TenCongViec like ? or CongViecHienNay.TenCongViec like ?) and TinhTrang.KhongConCongTacTaiTruong=?",
                            "%giảng viên%", "%cán bộ giảng dạy kiêm quản lý%", "False");
                }
                else
                {
                    filter = CriteriaOperator.Parse("NgayTinhThamNienNhaGiao is not null and (TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ?)",
                    "%có hưởng lương%", "%đang làm việc%", "%nghỉ bhxh%", "%thai sản%", "%hưởng 40% lương%", "%công tác%lương%");
                }
                
                XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, filter);
                DenHanNangPhuCapThamNien thamNien;
                DateTime now = TuNgay;
                int nam;
                List<Guid> gvList = new List<Guid>();
                
                ThamNienGiangVienList.Reload();

                if (TruongConfig.MaTruong.Equals("BUH") || TruongConfig.MaTruong.Equals("DLU"))
                {
                    foreach (ThongTinNhanVien item in nvList)
                    {
                        if (
                                (
                                    item.NhanVienThongTinLuong.ThamNien > 0
                                    && item.NhanVienThongTinLuong.NgayHuongThamNien.AddYears(1) >= TuNgay
                                    && item.NhanVienThongTinLuong.NgayHuongThamNien.AddYears(1) <= DenNgay
                                )
                                ||
                                (
                                    item.NhanVienThongTinLuong.ThamNien == 0
                                    && item.NhanVienThongTinLuong.NgayHuongThamNien.AddYears(5) >= TuNgay
                                    && item.NhanVienThongTinLuong.NgayHuongThamNien.AddYears(5) <= DenNgay
                                )
                            )
                        {
                            gvList.Add(item.Oid);
                            thamNien = new DenHanNangPhuCapThamNien(Session);
                            thamNien.SoHieuCongChuc = item.SoHieuCongChuc;
                            thamNien.MaQuanLy = item.MaQuanLy;
                            thamNien.BoPhan = item.BoPhan;
                            thamNien.ThongTinNhanVien = item;
                            thamNien.NgayVaoNganh = item.NgayTinhThamNienNhaGiao;
                            thamNien.NgachLuong = item.NhanVienThongTinLuong.NgachLuong;
                            thamNien.NgayHuongThamNienCu = item.NhanVienThongTinLuong.NgayHuongThamNien;
                            thamNien.ThamNienCu = item.NhanVienThongTinLuong.ThamNien;
                            thamNien.TinhTrang = item.TinhTrang;
                            thamNien.ThamNienMoi = thamNien.ThamNienCu > 0 ? thamNien.ThamNienCu + 1 : 5;
                            thamNien.NgayHuongThamNienMoi = thamNien.ThamNienCu > 0 ? thamNien.NgayHuongThamNienCu.AddYears(1) : item.NgayTinhThamNienNhaGiao.AddYears(5);

                            ThamNienGiangVienList.Add(thamNien);
                        }
                    }
                }
                else if (TruongConfig.MaTruong.Equals("UEL"))
                {
                    foreach (ThongTinNhanVien item in nvList)
                    {
                        nam = now.Year - item.NgayVaoNganhGiaoDuc.Year;
                        if (item.NgayVaoNganhGiaoDuc.Day >= TuNgay.Day && item.NgayVaoNganhGiaoDuc.Day <= DenNgay.Day && item.NgayVaoNganhGiaoDuc.Month >= TuNgay.Month && item.NgayVaoNganhGiaoDuc.Month <= DenNgay.Month && nam >= 5 && item.NhanVienThongTinLuong.ThamNien < nam)
                        {
                            thamNien = new DenHanNangPhuCapThamNien(Session);
                            thamNien.BoPhan = item.BoPhan;
                            thamNien.ThongTinNhanVien = item;
                            thamNien.NgayVaoNganh = item.NgayVaoNganhGiaoDuc;
                            thamNien.ThamNienCu = item.NhanVienThongTinLuong.ThamNien;
                            thamNien.NgayHuongThamNienCu = item.NhanVienThongTinLuong.NgayHuongThamNien;
                            thamNien.ThamNienMoi = nam;
                            if (thamNien.NgayHuongThamNienCu != DateTime.MinValue)
                                thamNien.NgayHuongThamNienMoi = thamNien.NgayHuongThamNienCu.AddYears(1);
                            else
                                thamNien.NgayHuongThamNienMoi = item.NgayVaoNganhGiaoDuc.AddYears(nam);
                            ThamNienGiangVienList.Add(thamNien);
                        }
                    }
                }
                else
                {
                    foreach (ThongTinNhanVien item in nvList)
                    {
                        nam = now.Year - item.NgayTinhThamNienNhaGiao.Year;
                        if ((item.NhanVienThongTinLuong.NgayHuongThamNien >= TuNgay &&
                            item.NhanVienThongTinLuong.NgayHuongThamNien <= DenNgay) ||
                            (item.NgayTinhThamNienNhaGiao.Day >= TuNgay.Day &&
                            item.NgayTinhThamNienNhaGiao.Day <= DenNgay.Day &&
                            item.NgayTinhThamNienNhaGiao.Month >= TuNgay.Month &&
                            item.NgayTinhThamNienNhaGiao.Month <= DenNgay.Month &&
                            nam >= 5 && item.NhanVienThongTinLuong.ThamNien < nam))
                        {
                            gvList.Add(item.Oid);
                            thamNien = new DenHanNangPhuCapThamNien(Session);
                            thamNien.SoHieuCongChuc = item.SoHieuCongChuc;
                            thamNien.MaQuanLy = item.MaQuanLy;
                            thamNien.BoPhan = item.BoPhan;
                            thamNien.ThongTinNhanVien = item;
                            thamNien.NgayVaoNganh = item.NgayTinhThamNienNhaGiao;
                            thamNien.NgachLuong = item.NhanVienThongTinLuong.NgachLuong;
                            thamNien.NgayHuongThamNienCu = item.NhanVienThongTinLuong.NgayHuongThamNien;
                            thamNien.ThamNienCu = item.NhanVienThongTinLuong.ThamNien;
                            thamNien.TinhTrang = item.TinhTrang;
                            if (item.BienChe || (item.LoaiNhanSu != null && item.LoaiNhanSu.TenLoaiNhanSu.ToLower().Contains("giảng viên")) ||
                                (item.LoaiNhanSu != null && item.LoaiNhanSu.TenLoaiNhanSu.ToLower().Contains("giảng viên")))
                            {
                                thamNien.ThamNienMoi = thamNien.ThamNienCu > 0 ? thamNien.ThamNienCu + 1 : nam;
                                thamNien.NgayHuongThamNienMoi = thamNien.NgayHuongThamNienCu != DateTime.MinValue ? thamNien.NgayHuongThamNienCu.AddYears(1) : item.NgayTinhThamNienNhaGiao.AddYears(nam);
                                thamNien.PhanLoai = "Giảng viên";
                            }
                            else
                            {
                                thamNien.ThamNienMoi = thamNien.ThamNienCu > 0 ? thamNien.ThamNienCu + 0.5m : 5 + ((nam - 5) / 2.0m);
                                thamNien.NgayHuongThamNienMoi = thamNien.NgayHuongThamNienCu != DateTime.MinValue ? thamNien.NgayHuongThamNienCu.AddYears(1) : item.NgayTinhThamNienNhaGiao.AddYears(nam);
                                thamNien.PhanLoai = "Nhân viên";
                            }
                            ThamNienGiangVienList.Add(thamNien);
                        }
                    }
                }
            }
        }
    }

}
