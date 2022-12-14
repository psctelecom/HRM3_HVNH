using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ChamCong
{
    [NonPersistent]
    [ImageName("BO_NghiHuu")]
    [ModelDefault("Caption", "Danh sách cán bộ")]
    public class DanhSachCanBoTheoHinhThucNghi : TruongBaseObject, ISupportController, IBoPhan
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DateTime _Ngay;
     
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }
        [ModelDefault("Caption", "Cán bộ")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Ngày")]
        public DateTime Ngay
        {
            get
            {
                return _Ngay;
            }
            set
            {
                SetPropertyValue("Ngay", ref _Ngay, value);
            }
        }

        public DanhSachCanBoTheoHinhThucNghi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
