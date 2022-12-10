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
    public class ChiTietYKienGV : BaseObject
    {
        #region Key
        private ThongTinTongYKienGV _ThongTinTongYKienGV;
        [ModelDefault("Caption", "Tổng ý kiến")]
        [Association("ThongTinTongYKienGV-ListChiTietYKienGV")]
        [Browsable(false)]
        public ThongTinTongYKienGV ThongTinTongYKienGV
        {
            get { return _ThongTinTongYKienGV; }
            set { SetPropertyValue("ThongTinTongYKienGV", ref _ThongTinTongYKienGV, value); }
        }
        #endregion
        private string _HocKy;
        private string _LopHocPhan;
        private string _TenMonHoc;
        private string _XacNhan;
        private string _YKienGiangVien;

        [ModelDefault("Caption", "Học kỳ")]
        public string HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }


        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        public string XacNhan
        {
            get { return _XacNhan; }
            set { SetPropertyValue("XacNhan", ref _XacNhan, value); }
        }

        [ModelDefault("Caption", "Ý kiến GV")]
        [Size(-1)]
        public string YKienGiangVien
        {
            get { return _YKienGiangVien; }
            set { SetPropertyValue("YKienGiangVien", ref _YKienGiangVien, value); }
        }

        public ChiTietYKienGV(Session session)
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