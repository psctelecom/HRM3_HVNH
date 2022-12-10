using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Loại học phần")]
    [DefaultProperty("TenLoaiHocPhan")]
    [Appearance("Hide_ThanhTra_UEL", TargetItems = "DonViTinh"
                                       , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong_KiemTra != 'UEL'")]
    //[Appearance("!Hide_ThanhTra_UEL", TargetItems = "DonViTinh"
    //                                   , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong_KiemTra = 'UEL'")]
    public class LoaiHocPhan : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHocPhan;
        private DonViTinh _DonViTinh;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }
        [ModelDefault("Caption", "Tên loại học phần")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiHocPhan
        {
            get { return _TenLoaiHocPhan; }
            set { SetPropertyValue("TenLoaiHocPhan", ref _TenLoaiHocPhan, value); }
        }
        [ModelDefault("Caption", "Đơn vị tính")]
        public DonViTinh DonViTinh
        {
            get { return _DonViTinh; }
            set { SetPropertyValue("DonViTinh", ref _DonViTinh, value); }
        }


        // Dùng để kiểm tra xem thuộc trường nào 
        [NonPersistent]
        private string ThongTinTruong_KiemTra { get; set; }

        public LoaiHocPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong_KiemTra = TruongConfig.MaTruong;
        }
    }
}
