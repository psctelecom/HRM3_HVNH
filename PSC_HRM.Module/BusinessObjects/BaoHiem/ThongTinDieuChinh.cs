using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_DieuChinhHoSo")]
    [ModelDefault("Caption", "Thông tin điều chỉnh")]
    public class ThongTinDieuChinh : BaseObject
    {
        // Fields...
        private DieuChinhHoSo _DieuChinhHoSo;
        private string _LyDoDieuChinh;
        private string _NoiDungMoi;
        private string _NoiDungCu;
        private string _NoiDungThayDoi;

        [Browsable(false)]
        [Association("DieuChinhHoSo-ListThongTinDieuChinh")]
        public DieuChinhHoSo DieuChinhHoSo
        {
            get
            {
                return _DieuChinhHoSo;
            }
            set
            {
                SetPropertyValue("DieuChinhHoSo", ref _DieuChinhHoSo, value);
            }
        }
        
        [ModelDefault("Caption", "Nội dung thay đổi")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NoiDungThayDoi
        {
            get
            {
                return _NoiDungThayDoi;
            }
            set
            {
                SetPropertyValue("NoiDungThayDoi", ref _NoiDungThayDoi, value);
            }
        }

        [ModelDefault("Caption", "Nội dung cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NoiDungCu
        {
            get
            {
                return _NoiDungCu;
            }
            set
            {
                SetPropertyValue("NoiDungCu", ref _NoiDungCu, value);
            }
        }

        [ModelDefault("Caption", "Nội dung mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NoiDungMoi
        {
            get
            {
                return _NoiDungMoi;
            }
            set
            {
                SetPropertyValue("NoiDungMoi", ref _NoiDungMoi, value);
            }
        }

        [ModelDefault("Caption", "Lý do điều chỉnh")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string LyDoDieuChinh
        {
            get
            {
                return _LyDoDieuChinh;
            }
            set
            {
                SetPropertyValue("LyDoDieuChinh", ref _LyDoDieuChinh, value);
            }
        }

        public ThongTinDieuChinh(Session session) : base(session) { }
    }

}
