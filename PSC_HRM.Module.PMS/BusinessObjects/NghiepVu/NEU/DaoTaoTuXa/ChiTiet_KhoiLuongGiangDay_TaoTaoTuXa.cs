using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa
{
    [ModelDefault("Caption","Chi tiết khối lượng")]
    public class ChiTiet_KhoiLuongGiangDay_TaoTaoTuXa : ChiTietThongTinChungPMS
    { 
        #region key
        private KhoiLuongGiangDay_TaoTaoTuXa _KhoiLuongGiangDay_TaoTaoTuXa;
        [Association("KhoiLuongGiangDay_TaoTaoTuXa-ListChiTiet")]
        [ModelDefault("Caption", "Khối lượng giảng dạy")]
        [Browsable(false)]
        public KhoiLuongGiangDay_TaoTaoTuXa KhoiLuongGiangDay_TaoTaoTuXa
        {
            get
            {
                return _KhoiLuongGiangDay_TaoTaoTuXa;
            }
            set
            {
                SetPropertyValue("KhoiLuongGiangDay_TaoTaoTuXa", ref _KhoiLuongGiangDay_TaoTaoTuXa, value);
            }
        }
        #endregion

        #region Hệ số
        private decimal _HeSo_QuyMo; 
        private decimal _HeSo_TuXa;
        private decimal _HeSo_NgoaiNEU;

        [ModelDefault("Caption", "HS quy mô")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_QuyMo
        {
            get { return _HeSo_QuyMo; }
            set { SetPropertyValue("HeSo_QuyMo", ref _HeSo_QuyMo, value); }
        }
        [ModelDefault("Caption", "HS từ xa")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_TuXa
        {
            get { return _HeSo_TuXa; }
            set { SetPropertyValue("HeSo_TuXa", ref _HeSo_TuXa, value); }
        }
        [ModelDefault("Caption", "HS ngoài NEU")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_NgoaiNEU
        {
            get { return _HeSo_NgoaiNEU; }
            set { SetPropertyValue("HeSo_NgoaiNEU", ref _HeSo_NgoaiNEU, value); }
        }
        #endregion

        public ChiTiet_KhoiLuongGiangDay_TaoTaoTuXa(Session session)
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