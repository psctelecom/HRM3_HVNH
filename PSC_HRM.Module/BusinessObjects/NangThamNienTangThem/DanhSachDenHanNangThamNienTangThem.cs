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

namespace PSC_HRM.Module.NangThamNienTangThem
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đến hạn nâng thâm niên tăng thêm")]
    public class DanhSachDenHanNangThamNienTangThem : BaseObject
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
        public XPCollection<DenHanNangThamNienTangThem> ThamNienTangThemList { get; set; }

        public DanhSachDenHanNangThamNienTangThem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThamNienTangThemList = new XPCollection<DenHanNangThamNienTangThem>(Session, false);
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
                //if (TruongConfig.MaTruong.Equals("BUH") || TruongConfig.MaTruong.Equals("DLU"))
                { filter = CriteriaOperator.Parse("NhanVienThongTinLuong.MocHuongThamNienTangThem is not null && TinhTrang.TenTinhTrang not like ? and TinhTrang.KhongConCongTacTaiTruong = 0", "%không hưởng lương%"); }

                XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, filter);
                DenHanNangThamNienTangThem thamNien;
                List<Guid> list = new List<Guid>();

                ThamNienTangThemList.Reload();

                foreach (ThongTinNhanVien item in nvList)
                {
                    if (item.NhanVienThongTinLuong.ThamNien == 0)
                    {
                        list.Add(item.Oid);
                        thamNien = new DenHanNangThamNienTangThem(Session);
                        thamNien.SoHieuCongChuc = item.SoHieuCongChuc;
                        thamNien.MaQuanLy = item.MaQuanLy;
                        thamNien.BoPhan = item.BoPhan;
                        thamNien.ThongTinNhanVien = item;
                        thamNien.NgachLuong = item.NhanVienThongTinLuong.NgachLuong;
                        thamNien.MocHuongThamNienTangThemCu = item.NhanVienThongTinLuong.NgayHuongThamNien;
                        thamNien.TinhTrang = item.TinhTrang;

                        ThamNienTangThemList.Add(thamNien);
                    }
                    if (item.NhanVienThongTinLuong.ThamNien > 0
                        && item.NhanVienThongTinLuong.NgayHuongThamNien >= TuNgay
                        && item.NhanVienThongTinLuong.NgayHuongThamNien <= DenNgay)
                    {
                        thamNien = new DenHanNangThamNienTangThem(Session);
                        thamNien.SoHieuCongChuc = item.SoHieuCongChuc;
                        thamNien.MaQuanLy = item.MaQuanLy;
                        thamNien.BoPhan = item.BoPhan;
                        thamNien.ThongTinNhanVien = item;
                        thamNien.NgachLuong = item.NhanVienThongTinLuong.NgachLuong;
                        thamNien.MocHuongThamNienTangThemCu = item.NhanVienThongTinLuong.NgayHuongThamNien;
                        thamNien.TinhTrang = item.TinhTrang;
                        thamNien.HSLTangThemTheoThamNienMoi = null;
                        thamNien.MocHuongThamNienTangThemMoi = item.NhanVienThongTinLuong.NgayHuongThamNien;

                        ThamNienTangThemList.Add(thamNien);
                    }
                }
            }
        }
    }
}
