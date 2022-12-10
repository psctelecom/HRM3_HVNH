using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TaoMaQuanLy;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.HoSo
{
    [DefaultClassOptions]
    [ImageName("BO_Resume")]
    [DefaultProperty("HoTen")]
    [ModelDefault("Caption", "Giảng viên thỉnh giảng")]
    [ModelDefault("EditorTypeName", "PSC_HRM.Module.Win.Editors.CustomCategorizedGiangVienThinhGiangListEditor")]

    [Appearance("Hide_UFM", TargetItems = "MaThinhGiang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UFM'")]
    [Appearance("Hide_GTVT", TargetItems = "MaThinhGiang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'GTVT'")]
    //[Appearance("Hide_QNU", TargetItems = "MaThinhGiang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_UEL", TargetItems = "MaThinhGiang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UEL'")]
    //[Appearance("Hide_NEU", TargetItems = "", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    [Appearance("Hide_CDY", TargetItems = "MaThinhGiang", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'CDY'")]
    //[Appearance("Hide_ChuyenGia", TargetItems = "ChuyenGia", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'HUFLIT'")]
    [Appearance("Hide_HUFLIT", TargetItems = "DonGia;TienTe", Visibility = ViewItemVisibility.Hide, Criteria = "ChuyenGia = false or ChuyenGia is null")]
    [Appearance("Show_HUFLIT", TargetItems = "DonGia;TienTe", Visibility = ViewItemVisibility.Show, Criteria = "ChuyenGia = true")]

    public class GiangVienThinhGiang : NhanVien
    {
        private string _CMND_ThinhGiang;
        private string _DonViCongTac;
        private string _TaiLieuGiangDay;
        private HocVi _HocVi;
        private BoPhan _TaiBoMon;
        private string _MaThinhGiang;
        private bool _Create = false;
        private bool _ChuyenGia;
        private decimal _DonGia;
        private TienTe _TienTe;

        [ModelDefault("Caption", "Mã thỉnh giảng")]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HVNH'")]
        public string MaThinhGiang
        {
            get
            {
                return _MaThinhGiang;
            }
            set
            {
                SetPropertyValue("MaThinhGiang", ref _MaThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "CMND Thỉnh giảng")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'HUFLIT'")]
        public string CMND_ThinhGiang
        {
            get
            {
                return _CMND_ThinhGiang;
            }
            set
            {
                SetPropertyValue("CMND_ThinhGiang", ref _CMND_ThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Tại Bộ môn")]
        public BoPhan TaiBoMon
        {
            get
            {
                return _TaiBoMon;
            }
            set
            {
                SetPropertyValue("TaiBoMon", ref _TaiBoMon, value);
            }
        }

        [ModelDefault("Caption", "Học vị (*)")]
        public HocVi HocVi
        {
            get
            {
                return _HocVi;
            }
            set
            {
                SetPropertyValue("HocVi", ref _HocVi, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị công tác (*)")]
        public string DonViCongTac
        {
            get
            {
                return _DonViCongTac;
            }
            set
            {
                SetPropertyValue("DonViCongTac", ref _DonViCongTac, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Tài liệu giảng dạy")]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.DirectoryEditor")]
        public string TaiLieuGiangDay
        {
            get
            {
                return _TaiLieuGiangDay;
            }
            set
            {
                SetPropertyValue("TaiLieuGiangDay", ref _TaiLieuGiangDay, value);
            }
        }

        [ModelDefault("Caption", "Chuyên gia")]
        [ImmediatePostData]
        public bool ChuyenGia
        {
            get
            {
                return _ChuyenGia;
            }
            set
            {
                SetPropertyValue("ChuyenGia", ref _ChuyenGia, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Loại tiền")]
        public TienTe TienTe
        {
            get { return _TienTe; }
            set { SetPropertyValue("TienTe", ref _TienTe, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Đánh giá")]
        [Association("GiangVienThinhGiang-ListDanhGiaGiangVienThinhGiang")]
        public XPCollection<DanhGiaGiangVienThinhGiang> ListDanhGiaGiangVienThinhGiang
        {
            get
            {
                return GetCollection<DanhGiaGiangVienThinhGiang>("ListDanhGiaGiangVienThinhGiang");
            }
        }

        //[NonPersistent]
        //[ImmediatePostData]
        //private string MaTruong { get; set; }

        public GiangVienThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            //
            if (TruongConfig.MaTruong.Equals("CYD") )
            {
                MaThinhGiang = null;
            }
            else if (TruongConfig.MaTruong.Equals("HUFLIT"))
            {
                object obj = DataProvider.GetObject("spd_HUFLIT_MaThinhGiang", System.Data.CommandType.StoredProcedure);
                string mau = obj.ToString();
                MaThinhGiang = mau;
            }
            else
            {
                MaThinhGiang = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaGiangVienThinhGiang);
            }

            //
            if (TruongConfig.MaTruong.Equals("UFM"))
            {
                MaQuanLy = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaGiangVienThinhGiang);
            }
            else
            {
                MaQuanLy = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaNhanVien);
            }
            //
            HinhThucTuyenDung = HinhThucTuyenDungEnum.XetTuyen;
            //
            LoaiHoSo = LoaiHoSoEnum.GiangVien;
            //
            if (TruongConfig.MaTruong.Equals("HVNH"))
            {
                PhanVien = Session.FindObject<LoaiPhanVien>(CriteriaOperator.Parse("Oid = ?", "1E823662-777F-443F-AFED-440F5126D542"));
                LoadMaGVTFG_HVNH();
            }          
            _Create = true;
        }

        public void LoadMaGVTFG_HVNH()
        {
            if (TruongConfig.MaTruong.Equals("HVNH"))
            {
                object kq = null;
                SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                param[0] = new SqlParameter("@LoaiPhanVien", PhanVien.Oid);
                kq = DataProvider.GetValueFromDatabase("spd_NhanSu_TaoMaGiangVienHVNH_TG", System.Data.CommandType.StoredProcedure, param);
                if (kq != null)
                {
                    MaQuanLy = kq.ToString();
                }
            }
        }

        protected override void AfterBoPhanChanged()
        {
            MaThinhGiang = null;
            base.AfterBoPhanChanged();
            if (TruongConfig.MaTruong.Equals("CYD") && BoPhan != null && string.IsNullOrWhiteSpace(MaThinhGiang))
            {
                MaThinhGiang = GetMaThinhGiang();
            }
        }
        private string GetMaThinhGiang()
        {
            string ma = string.Empty;

            if (string.IsNullOrWhiteSpace(MaThinhGiang))
                ma = MaQuanLyFactory.CreateSoHieuNhanVien(this.BoPhan);

            return ma;
        }
           
        public void onloadGVTG()
        {
            base.OnLoaded();
            MaTruong = TruongConfig.MaTruong;            
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //Đánh mã quản lý tự động nếu chưa có mã
                if(MaTruong != "DNU" && MaTruong != "UFM" && MaTruong != "HVNH" && MaTruong != "HUFLIT")
                {
                    if (_Create == true)
                    {
                        MaQuanLy = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.MaNhanVien);
                    } 
                }                
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (TruongConfig.MaTruong.Equals("UEL") || TruongConfig.MaTruong.Equals("NEU"))
            {
                SqlParameter[] parameter = new SqlParameter[1];
                parameter[0] = new SqlParameter("@NhanVien", Oid);
                DataProvider.ExecuteNonQuery("spd_Web_TaoTaiKhoan_WebHRM", CommandType.StoredProcedure, parameter);
            }
        }
    }

}
