using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình bảo hiểm")]
    //[Appearance("CauHinhBaoHiem", TargetItems = "TenTaiKhoan", Visibility = ViewItemVisibility.Hide, Criteria = "!TuDongTaoTaiKhoan")]
    public class CauHinhBaoHiem : BaseObject
    {
        // Fields...
        private bool _TuDongTaoHoSoBaoHiem = false;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo hồ sơ bảo hiểm")]
        public bool TuDongTaoHoSoBaoHiem
        {
            get
            {
                return _TuDongTaoHoSoBaoHiem;
            }
            set
            {
                SetPropertyValue("TuDongTaoHoSoBaoHiem", ref _TuDongTaoHoSoBaoHiem, value);
            }
        }

        

        public CauHinhBaoHiem(Session session) : base(session) { }
    }

}
