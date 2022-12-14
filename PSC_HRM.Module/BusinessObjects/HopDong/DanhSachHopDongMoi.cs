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
    [ModelDefault("Caption", "Danh sách hợp đồng mới")]
    public class DanhSachHopDongMoi : BaseObject
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
        public XPCollection<ChiTietHopDongMoi> ChiTietHopDongMoiList { get; set; }

        public DanhSachHopDongMoi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ChiTietHopDongMoiList = new XPCollection<ChiTietHopDongMoi>(Session, false);

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
                ChiTietHopDongMoiList.Reload();
                CriteriaOperator filter = CriteriaOperator.Parse("TuNgay>=? and TuNgay<=?",
                    TuNgay.SetTime(SetTimeEnum.StartDay), DenNgay.SetTime(SetTimeEnum.EndDay));
                XPCollection<HopDong_NhanVien> hdList = new XPCollection<HopDong_NhanVien>(Session, filter);
                ThongTinNhanVien nhanVien;
                ChiTietHopDongMoi hopDongMoi;
                foreach (HopDong_NhanVien item in hdList)
                {
                    if (item.NhanVien != null)
                    {
                        nhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.NhanVien.Oid);
                        if (nhanVien != null &&
                            nhanVien.TinhTrang.KhongConCongTacTaiTruong == false)
                        {
                            hopDongMoi = new ChiTietHopDongMoi(Session);
                            hopDongMoi.BoPhan = nhanVien.BoPhan;
                            hopDongMoi.ThongTinNhanVien = nhanVien;
                            hopDongMoi.HopDongLaoDong = item;
                            hopDongMoi.LoaiHopDong = item.LoaiHopDong;
                            hopDongMoi.NgayKyHopDong = item.TuNgay;
                            ChiTietHopDongMoiList.Add(hopDongMoi);
                        }
                    }
                }
            }
        }
       
    }

}
