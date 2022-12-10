using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.PMS.NghiepVu.NhatKyYKienGiangVien
{

    [ModelDefault("Caption", "Chi tiết công tác phí")]
    public class ThongTinTongYKienGV : BaseObject
    {
        #region Key
        private QuanLyNhatKyYKienGV _QuanLyNhatKyYKienGV;
        [ModelDefault("Caption", "Quản lý ý kiến GV")]
        [Association("QuanLyNhatKyYKienGV-ListThongTinTongYKienGV")]
        [Browsable(false)]
        public QuanLyNhatKyYKienGV QuanLyNhatKyYKienGV
        {
            get { return _QuanLyNhatKyYKienGV; }
            set { SetPropertyValue("QuanLyNhatKyYKienGV", ref _QuanLyNhatKyYKienGV, value); }
        }
        #endregion

        private NhanVien _NhanVien;
        private int _TongHocPhan;
        private int _SoHPDaXacNhan;
        private int _SoHPChuaXacNhan;
        private string _YKienGiangVien;

        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Tổng học phần")]
        public int TongHocPhan
        {
            get { return _TongHocPhan; }
            set { SetPropertyValue("TongHocPhan", ref _TongHocPhan, value); }
        }

        [ModelDefault("Caption", "Số học phần đã xác nhận")]
        public int SoHPDaXacNhan
        {
            get { return _SoHPDaXacNhan; }
            set { SetPropertyValue("SoHPDaXacNhan", ref _SoHPDaXacNhan, value); }
        }

        [ModelDefault("Caption", "Số học phần chưa xác nhận")]
        public int SoHPChuaXacNhan
        {
            get { return _SoHPChuaXacNhan; }
            set { SetPropertyValue("SoHPChuaXacNhan", ref _SoHPChuaXacNhan, value); }
        }

        [ModelDefault("Caption", "Ý kiến GV")]
        [Size(-1)]
        public string YKienGiangVien
        {
            get { return _YKienGiangVien; }
            set { SetPropertyValue("YKienGiangVien", ref _YKienGiangVien, value); }
        }

        [Aggregated]
        [Association("ThongTinTongYKienGV-ListChiTietYKienGV")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietYKienGV> ListChiTietYKienGV
        {
            get
            {
                return GetCollection<ChiTietYKienGV>("ListChiTietYKienGV");
            }
        }


        public ThongTinTongYKienGV(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}