using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.BusinessObjects.DanhMuc
{
    [ModelDefault("Caption", "Phân quyền sử dụng PMS (WEB)")]
    [Appearance("NEU_Hide", TargetItems = "HocKy;KyTinhPMS", Visibility = ViewItemVisibility.Hide)]
    [Appearance("VHU_Hide", TargetItems = "NamHoc", Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'VHU'")]
    public class PhanQuyen_PMS_WEB : ThongTinChungPMS
    {
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết")]
        [Association("PhanQuyen_PMS_WEB-ListChiTiet")]
        public XPCollection<ChiTietPhanQuyen_PMS_WEB> ListChiTiet
        {
            get
            {
                return GetCollection<ChiTietPhanQuyen_PMS_WEB>("ListChiTiet");
            }
        }
        public PhanQuyen_PMS_WEB(Session session)
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