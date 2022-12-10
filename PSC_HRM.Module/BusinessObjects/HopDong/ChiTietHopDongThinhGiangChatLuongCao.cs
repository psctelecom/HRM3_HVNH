using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.HopDong
{
    [DefaultProperty("Lop")]
    [ImageName("BO_Contract")]
    [ModelDefault("Caption", "Chi tiết hợp đồng thỉnh giảng chất lượng cao")]
    public class ChiTietHopDongThinhGiangChatLuongCao : TruongBaseObject
    {
        // Fields...
        private decimal _SoTiet;
        private HopDong_ThinhGiangChatLuongCao _HopDongThinhGiangChatLuongCao;
        private string _MonHoc;   

        [Browsable(false)]
        [ModelDefault("Caption", "Hợp đồng thỉnh giảng chất lượng cao")]
        [Association("HopDong_ThinhGiangChatLuongCao-ListChiTietHopDongThinhGiangChatLuongCao")]
        public HopDong_ThinhGiangChatLuongCao HopDongThinhGiangChatLuongCao
        {
            get
            {
                return _HopDongThinhGiangChatLuongCao; 
            }
            set
            {
                SetPropertyValue("HopDongThinhGiangChatLuongCao", ref _HopDongThinhGiangChatLuongCao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Môn dạy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CurriculumEditor")]
        public string MonHoc
        {
            get
            {
                return _MonHoc;
            }
            set
            {
                SetPropertyValue("MonHoc", ref _MonHoc, value);
            }
        }

        [ModelDefault("Caption", "Số tiết")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTiet
        {
            get
            {
                return _SoTiet;
            }
            set
            {
                SetPropertyValue("SoTiet", ref _SoTiet, value);
            }
        }

        public ChiTietHopDongThinhGiangChatLuongCao(Session session) : base(session) { }
    }

}
