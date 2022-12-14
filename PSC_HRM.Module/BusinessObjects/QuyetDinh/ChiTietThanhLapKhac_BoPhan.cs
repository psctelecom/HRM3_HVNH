using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định thành lập khác - Bộ phận")]
    public class ChiTietThanhLapKhac_BoPhan : TruongBaseObject
    {
        private int _SoThuTu;
        private BoPhan _BoPhan; 
        private string _BoPhanText;

        private ChiTietThanhLapKhac_ToChuc _ChiTietThanhLapKhac_ToChuc;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định khác")]
        [Association("ChiTietThanhLapKhac_ToChuc-ListChiTietThanhLapKhac_BoPhan")]
        public ChiTietThanhLapKhac_ToChuc ChiTietThanhLapKhac_ToChuc
        {
            get
            {
                return _ChiTietThanhLapKhac_ToChuc;
            }
            set
            {
                SetPropertyValue("ChiTietThanhLapKhac_ToChuc", ref _ChiTietThanhLapKhac_ToChuc, value);
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    BoPhanText = value.TenBoPhan;
                }
            }
        }

        [ModelDefault("Caption", "Tên đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public String BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }
                
        public ChiTietThanhLapKhac_BoPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
                
        protected override void OnLoaded()
        {
            base.OnLoaded();
        }

    }
}
