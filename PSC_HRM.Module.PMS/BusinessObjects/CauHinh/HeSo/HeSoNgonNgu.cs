using System;
using System.Linq;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.CauHinh
{
    [DefaultClassOptions]
    [DefaultProperty("TenNgonNgu")]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Hệ số ngôn ngữ")]
    [Appearance("Hide_HeSo_UFM", TargetItems = "BoPhan"
                                               , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat != 'UFM'")]
   
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyHeSo;NgonNguGiangDay", "Ngôn ngữ giảng dạy đã tồn tại")]
    public class HeSoNgonNgu : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        private  string _MaQuanLy;
        private string _TenNgonNgu;
        private decimal _HeSo_NgonNgu;
        private NgonNguEnum _NgonNguGiangDay;
        private BoPhan _BoPhan;

        
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoNgonNgu")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }


        [ModelDefault("Caption", "Mã ngôn ngữ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ImmediatePostData]
        public NgonNguEnum NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set
            {
                SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value);
                if (!IsLoading)
                {
                    TenNgonNgu = NgonNguEnum.BinhThuong.ToString();
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Tên ngôn ngữ")]
        public string TenNgonNgu
        {
            get
            {
                return _TenNgonNgu;
            }
            set
            {
                SetPropertyValue("TenNgonNgu", ref _TenNgonNgu, value);
            }
        }

        [ModelDefault("Caption", "Hệ số")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSo_NgonNgu
        {
            get
            {
                return _HeSo_NgonNgu;
            }
            set
            {
                SetPropertyValue("HeSo_NgonNgu", ref _HeSo_NgonNgu, value);
            }
        }

        [ModelDefault("Caption", "Bộ môn")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        public HeSoNgonNgu(Session session) : base(session) { }
    }

}
