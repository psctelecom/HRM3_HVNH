using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.ThanhToan
{

    [ModelDefault("Caption", "Chi tiết thù lao giảng dạy")]
    [DefaultProperty("Caption")]
    
    public class ChiTietThuLaoGiangDay : BaseObject
    {
        #region key
        private BangThuLaoGiangDay _BangThuLaoGiangDay;
        [Association("BangThuLaoGiangDay-listChiTiet")]
        [ModelDefault("Caption", "Bảng thù lao")]
        [Browsable(false)]
        public BangThuLaoGiangDay BangThuLaoGiangDay
        {
            get
            {
                return _BangThuLaoGiangDay;
            }
            set
            {
                SetPropertyValue("BangThuLaoGiangDay", ref _BangThuLaoGiangDay, value);
            }
        }
        #endregion

        #region Khai báo
        private ThongTinBangChot _ThongTinBangChot;

        private NhanVien _NhanVien;
        private decimal _SoTienThanhToanVuotGio;
        private decimal _SoTienThanhToanTienMat;
        private decimal _TongTienThanhToan;
        #endregion

        #region Sử dụng

        [ModelDefault("Caption", "ThongTinBangChot")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinBangChot ThongTinBangChot
        {
            get { return _ThongTinBangChot; }
            set { SetPropertyValue("ThongTinBangChot", ref _ThongTinBangChot, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        #endregion
        public ChiTietThuLaoGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
       
    }
}
