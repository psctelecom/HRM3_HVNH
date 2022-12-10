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
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.PMS.NghiepVu
{

    [ModelDefault("Caption", "Cố vấn học tập")]
    [Appearance("Hide_CoVanHocTap", TargetItems = "Caption", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Hide_!HUFLIT", TargetItems = "Lop;SoLuong;ToMau"
                                            , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyCoVanHocTap.ThongTinTruong.TenVietTat != 'HUFLIT'")]
    [Appearance("ToMauCoVanHocTap", TargetItems = "*", BackColor = "Yellow", FontColor = "Black", Criteria = "ToMau")]
    [DefaultProperty("Caption")]
    public class CoVanHocTap : BaseObject
    {
        private QuanLyCoVanHocTap _QuanLyCoVanHocTap;
        private NhanVien _NhanVien;
        private string _Lop;
        private int _SoLuong; 
        private string _GhiChu;
        private bool _ToMau;

        [Association("QuanLyCVHT-ListCoVanHocTap")]
        [ModelDefault("Caption", "Quản lý cố vấn học tập")]
        [Browsable(false)]
        public QuanLyCoVanHocTap QuanLyCoVanHocTap
        {
            get
            {
                return _QuanLyCoVanHocTap;
            }
            set
            {
                SetPropertyValue("QuanLyCoVanHocTap", ref _QuanLyCoVanHocTap, value);
            }
        }
        //

        [ModelDefault("Caption", "Giảng viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Lớp")]
        public string Lop
        {
            get { return _Lop; }
            set { SetPropertyValue("Lop", ref _Lop, value); }
        }
        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public int SoLuong
        {
            get { return _SoLuong; }
            set { SetPropertyValue("SoLuong", ref _SoLuong, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [ImmediatePostData]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        [ModelDefault("Caption", "Tô màu")]
        [ModelDefault("AllowEdit", "false")]
        public bool ToMau
        {
            get { return _ToMau; }
            set { SetPropertyValue("ToMau", ref _ToMau, value); }
        }
        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} {1}", NhanVien != null ? NhanVien.HoTen : "", GhiChu);
            }
        }
        protected override void EndEdit()
        {
            base.EndEdit();
            if (GhiChu != null)    
                ToMau = true;
        }
        public CoVanHocTap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            ToMau = false;
            base.AfterConstruction();
        }   
    }
}
