using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.QuyetDinh;

namespace PSC_HRM.Module.BoNhiem
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách hết hạn bổ nhiệm")]
    public class DanhSachHetHanBoNhiem : BaseObject
    {
        private DateTime _DenNgay;
        private DateTime _TuNgay;

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

        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<HetHanBoNhiem> ListChiTietBoNhiem { get; set; }

        public DanhSachHetHanBoNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DateTime current = HamDungChung.GetServerTime();
            TuNgay = HamDungChung.SetTime(current, 2);
            DenNgay = HamDungChung.SetTime(current, 3);
            ListChiTietBoNhiem = new XPCollection<HetHanBoNhiem>(Session, false);
        }

        public void LoadData()
        {
            ListChiTietBoNhiem.Reload();
            CriteriaOperator filter = CriteriaOperator.Parse("NgayHetNhiemKy>=? and NgayHetNhiemKy<=?",
                HamDungChung.SetTime(TuNgay, 2), HamDungChung.SetTime(DenNgay, 3));
            XPCollection<QuyetDinhBoNhiem> qdListBoNhiem = new XPCollection<QuyetDinhBoNhiem>(Session, filter);
            HetHanBoNhiem chiTiet;
            QuyetDinhBoNhiem boNhiem;
            foreach (QuyetDinhBoNhiem qd in qdListBoNhiem)
            {
                boNhiem = Session.FindObject<QuyetDinhBoNhiem>(CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHieuLuc>?", 
                    qd.ThongTinNhanVien, qd.NgayHieuLuc));
                if (boNhiem == null)
                {
                    chiTiet = new HetHanBoNhiem(Session);
                    chiTiet.QuyetDinh = qd;
                    chiTiet.ThongTinNhanVien = qd.ThongTinNhanVien;
                    chiTiet.BoPhan = qd.BoPhan;
                    chiTiet.ChucVu = qd.ChucVuMoi;
                    chiTiet.ChucVuKiemNhiem = false;
                    chiTiet.NgayBoNhiem = qd.NgayHieuLuc;
                    chiTiet.NgayHetNhiemKy = qd.NgayHetNhiemKy;

                    ListChiTietBoNhiem.Add(chiTiet);
                }
            }

            filter = CriteriaOperator.Parse("NgayHetNhiemKy>=? and NgayHetNhiemKy<=?",
                HamDungChung.SetTime(TuNgay, 2), HamDungChung.SetTime(DenNgay, 3));
            XPCollection<QuyetDinhBoNhiemKiemNhiem> qdListBoNhiemKiemNhiem = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter);
            QuyetDinhBoNhiemKiemNhiem boNhiemKiemNhiem;
            foreach (QuyetDinhBoNhiemKiemNhiem qd in qdListBoNhiemKiemNhiem)
            {
                boNhiemKiemNhiem = Session.FindObject<QuyetDinhBoNhiemKiemNhiem>(CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHieuLuc>?",
                    qd.ThongTinNhanVien, qd.NgayHieuLuc));
                if (boNhiemKiemNhiem == null)
                {
                    chiTiet = new HetHanBoNhiem(Session);
                    chiTiet.QuyetDinh = qd;
                    chiTiet.ThongTinNhanVien = qd.ThongTinNhanVien;
                    chiTiet.BoPhan = qd.BoPhan;
                    chiTiet.ChucVu = qd.ChucVuKiemNhiemMoi;
                    chiTiet.ChucVuKiemNhiem = true;
                    chiTiet.NgayBoNhiem = qd.NgayHieuLuc;
                    chiTiet.NgayHetNhiemKy = qd.NgayHetNhiemKy;

                    ListChiTietBoNhiem.Add(chiTiet);
                }
            }
        }
    }

}
