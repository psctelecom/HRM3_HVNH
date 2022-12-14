using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.HopDong
{
    [NonPersistent]
    [ImageName("BO_HopDong")]
    [ModelDefault("Caption", "Danh sách hết hạn hợp đồng")]
    public class DanhSachHetHanHopDong : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
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

        [ModelDefault("AllowEdit", "True")]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietHetHanHopDong> ChiTietHetHanHopDongList { get; set; }

        public DanhSachHetHanHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ChiTietHetHanHopDongList = new XPCollection<ChiTietHetHanHopDong>(Session, false);

            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue &&
                DenNgay != DateTime.MinValue &&
                 TuNgay < DenNgay)
            {
                ChiTietHetHanHopDongList.Reload();
                CriteriaOperator filter = CriteriaOperator.Parse("DenNgay>=? and DenNgay<=?",
                    TuNgay.SetTime(SetTimeEnum.StartDay), DenNgay.SetTime(SetTimeEnum.EndDay));
                XPCollection<HopDong_NhanVien> hdList = new XPCollection<HopDong_NhanVien>(Session, filter);
                ThongTinNhanVien nhanVien;
                ChiTietHetHanHopDong hetHanHopDong;
                foreach (HopDong_NhanVien item in hdList)
                {
                    if (item.NhanVien != null)
                    {
                        nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.NhanVien.Oid);
                        if (nhanVien != null &&
                            nhanVien.HopDongHienTai != null &&
                            nhanVien.HopDongHienTai.Oid == item.Oid &&
                            nhanVien.TinhTrang.KhongConCongTacTaiTruong == false)
                        {
                            hetHanHopDong = new ChiTietHetHanHopDong(Session);
                            hetHanHopDong.BoPhan = nhanVien.BoPhan;
                            hetHanHopDong.ThongTinNhanVien = nhanVien;
                            hetHanHopDong.HopDongLaoDong = nhanVien.HopDongHienTai;
                            hetHanHopDong.LoaiHopDong = nhanVien.HopDongHienTai.LoaiHopDong;
                            hetHanHopDong.NgayHetHan = item.DenNgay;
                            ChiTietHetHanHopDongList.Add(hetHanHopDong);
                        }
                    }
                }
            }
        }
        public void LoadDataIUH()
        {
            if (TuNgay != DateTime.MinValue &&
                DenNgay != DateTime.MinValue &&
                 TuNgay < DenNgay)
            {
                ChiTietHetHanHopDongList.Reload();

                XPCollection<ThongTinNhanVien> nhanVienList = new XPCollection<ThongTinNhanVien>(Session);
                ChiTietHetHanHopDong hetHanHopDong;
                foreach (ThongTinNhanVien nhanVien in nhanVienList)
                {
                    if (nhanVien.LoaiNhanSu!=null && !nhanVien.LoaiNhanSu.TenLoaiNhanSu.Contains("Hợp đồng không thời hạn"))
                    {
                        hetHanHopDong = new ChiTietHetHanHopDong(Session);
                        hetHanHopDong.BoPhan = nhanVien.BoPhan;
                        hetHanHopDong.ThongTinNhanVien = nhanVien;
                        hetHanHopDong.HopDongLaoDong = nhanVien.HopDongHienTai != null ? nhanVien.HopDongHienTai : null;
                        hetHanHopDong.LoaiHopDong = nhanVien.LoaiNhanVien != null ? nhanVien.LoaiNhanVien.TenLoaiNhanVien : "Chưa xác định loại hợp đồng";
                        ChiTietHetHanHopDongList.Add(hetHanHopDong);
                    }
                }
            }
        }
    }

}
