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
using DevExpress.Data.Filtering;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Hệ số lớp đông")]
    [Appearance("Hide_HUFLIT", TargetItems = "BacDaoTao", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HUFLIT'")]
    [Appearance("Show_VHU", TargetItems = "LoaiHocPhan;BacDaoTao", Visibility = ViewItemVisibility.Show, Criteria = "MaTruong = 'VHU'")]
    [Appearance("Hide_VHU", TargetItems = "NgonNgu;LoaiMonHoc;NgonNguEnum;HeDaoTao", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'VHU'")]
    [Appearance("Hide_HeSoLopDong", TargetItems = "LoaiMonHoc", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'VHU'")]

    public class HeSoLopDong : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoLopDong")]
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
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private int _TuKhoan;
        private int _DenKhoan;
        private decimal _HeSo_LopDong;
        private LoaiMonHoc _LoaiMonHoc;
        private BoPhan _BoPhan;
        private NgonNguGiangDay _NgonNgu;
        private NgonNguEnum _NgonNguEnum;
        private bool _ChuyenNganh;
        private LoaiHocPhanEnum? _LoaiHocPhan;
        private string _MaTruong;

        [ModelDefault("Caption", "Bậc đào tạo")]
        //[VisibleInListView(false)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        //[VisibleInListView(false)]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Từ khoản")]
        public int TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }

        [ModelDefault("Caption", "Đến khoản")]
        public int DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }

        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoLopDong", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_LopDong
        {
            get { return _HeSo_LopDong; }
            set { SetPropertyValue("HeSo_LopDong", ref _HeSo_LopDong, value); }
        }

        [ModelDefault("Caption", "Loại môn học")]
        public LoaiMonHoc LoaiMonHoc
        {
            get { return _LoaiMonHoc; }
            set { SetPropertyValue("LoaiMonHoc", ref _LoaiMonHoc, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ")]
        //[NoForeignKey]
        public NgonNguGiangDay NgonNgu
        {
            get { return _NgonNgu; }
            set { SetPropertyValue("NgonNgu", ref _NgonNgu, value); }
        }

        [ModelDefault("Caption", "Ngôn ngữ")]
        //[NoForeignKey]
        public NgonNguEnum NgonNguEnum
        {
            get { return _NgonNguEnum; }
            set { SetPropertyValue("NgonNguEnum", ref _NgonNguEnum, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành")]
        public bool ChuyenNganh
        {
            get { return _ChuyenNganh; }
            set { SetPropertyValue("ChuyenNganh", ref _ChuyenNganh, value); }
        }

        [ModelDefault("Caption", "Khoa - đơn vị")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhanEnum? LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }


        [ModelDefault("Caption", "Mã trường")]
        [Browsable(false)]
        [NonPersistent]
        public string MaTruong
        {
            get { return _MaTruong; }
            set { SetPropertyValue("MaTruong", ref _MaTruong, value); }
        }
        public HeSoLopDong(Session session)
            : base(session)
        { }

        protected override void OnLoaded()
        {
            MaTruong = TruongConfig.MaTruong;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            TuKhoan = 1;
            DenKhoan = 1000;
        }
    }
}