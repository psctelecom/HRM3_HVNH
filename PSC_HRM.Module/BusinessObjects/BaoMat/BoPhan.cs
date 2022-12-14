using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using System.Data.SqlClient;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty("TenBoPhan")]
    [ModelDefault("Caption", "Đơn vị, phòng ban")]
    [ModelDefault("AllowNew", "False")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "MaQuanLy;TenBoPhan;BoPhanCha")]
    [RuleCombinationOfPropertiesIsUnique("Ktra_MQL_Trung", DefaultContexts.Save, "MaQuanLy")]
    public class BoPhan : TruongBaseObject, IBoPhan, ITreeNode, ITreeNodeImageProvider, IThongTinTruong
    {
        private string _MaQuanLy;
        private string _MaDonVi;
        private string _MaQuanLy_UIS;
        private string _TenBoPhan;
        private string _TenBoPhanENG;
        private int _STT;
        private LoaiBoPhanEnum _LoaiBoPhan = LoaiBoPhanEnum.PhongBan;
        private BoPhan _BoPhanCha;
        private bool _NgungHoatDong;
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhanChaOld;
        private string _TenBoPhanVietTat;
        private decimal _HeSo_TNTH;
        private DonViTrucThuocEnum _DonViTrucThuoc;

        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("1",DefaultContexts.Save, TargetCriteria="MaTruong='NEU'")]
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
        [ModelDefault("Caption", "Mã đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong='NEU'")]
        [RuleUniqueValue(DefaultContexts.Save, TargetCriteria = "MaTruong='NEU'")]
        public string MaDonVi
        {
            get
            {
                return _MaDonVi;
            }
            set
            {
                SetPropertyValue("MaDonVi", ref _MaDonVi, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý (UIS)")]
        [ModelDefault("DisplayFormat", "###")]
        [ModelDefault("EditMask", "###")]        
        //[ModelDefault("AllowEdit", "False")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy_UIS
        {
            get
            {
                return _MaQuanLy_UIS;
            }
            set
            {
                SetPropertyValue("MaQuanLy_UIS", ref _MaQuanLy_UIS, value);
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBoPhan
        {
            get
            {
                return _TenBoPhan;
            }
            set
            {
                SetPropertyValue("TenBoPhan", ref _TenBoPhan, value);
                if (!IsLoading && !String.IsNullOrEmpty(value))
                {
                    AfterChangeTenBoPhan();
                }
            }
        }        

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại đơn vị")]
        public LoaiBoPhanEnum LoaiBoPhan
        {
            get
            {
                return _LoaiBoPhan;
            }
            set
            {
                SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Thuộc Đơn vị")]
        [Association("BoPhanCha-BoPhanCon")]
        [VisibleInListView(false)]
        public BoPhan BoPhanCha
        {
            get
            {
                return _BoPhanCha;
            }
            set
            {
                SetPropertyValue("BoPhanCha", ref _BoPhanCha, value);
            }
        }

        [Browsable(false)]
        public BoPhan BoPhanChaOld
        {
            get
            {
                return _BoPhanChaOld;
            }
            set
            {
                SetPropertyValue("BoPhanChaOld", ref _BoPhanChaOld, value);
            }
        }

        [Association("BoPhanCha-BoPhanCon")]
        [ModelDefault("Caption", "Danh sách Đơn vị trực thuộc")]
        [Aggregated]
        public XPCollection<BoPhan> ListBoPhanCon
        {
            get
            {
                return GetCollection<BoPhan>("ListBoPhanCon");
            }
        }

        [Browsable(false)]
        [Association("BoPhan-ListNhanVien")]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<NhanVien> ListNhanVien
        {
            get
            {
                return GetCollection<NhanVien>("ListNhanVien");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngừng theo dõi")]
        public bool NgungHoatDong
        {
            get
            {
                return _NgungHoatDong;
            }
            set
            {
                SetPropertyValue("NgungHoatDong", ref _NgungHoatDong, value);
                if (!IsLoading)
                {
                    foreach (BoPhan item in this.ListBoPhanCon)
                    {
                        item.NgungHoatDong = NgungHoatDong;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Tên viết tắt")]
        public string TenBoPhanVietTat
        {
            get
            {
                return _TenBoPhanVietTat;
            }
            set
            {
                SetPropertyValue("TenBoPhanVietTat", ref _TenBoPhanVietTat, value);
            }
        }

        [ModelDefault("Caption", "Tên Đơn vị (English)")]
        public string TenBoPhanENG
        {
            get
            {
                return _TenBoPhanENG;
            }
            set
            {
                SetPropertyValue("TenBoPhanENG", ref _TenBoPhanENG, value);
            }
        }

        public System.Drawing.Image GetImage(out string imageName)
        {
            imageName = "BO_GiaDinh_32x32";
            return ImageLoader.Instance.GetImageInfo(imageName).Image;
        }

        [ModelDefault("Caption", "Hệ số TNTH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoTNTH", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_TNTH
        {
            get { return _HeSo_TNTH; }
            set { SetPropertyValue("HeSo_TNTH", ref _HeSo_TNTH, value); }
        }

        protected virtual void AfterChangeTenBoPhan()
        {
            TenBoPhanVietTat = HamDungChung.TaoChuVietTat(TenBoPhan);
        }

        //Chỉ dùng để phân quyền
        [NonPersistent]
        [Browsable(false)]
        BoPhan IBoPhan.BoPhan
        {
            get { return this; }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        private void SetThongTinTruong(BoPhan bp)
        {
            if (bp != null)
            {
                if (bp is ThongTinTruong)
                {
                    ThongTinTruong = bp as ThongTinTruong;
                }
                else
                    SetThongTinTruong(bp.BoPhanCha);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đơn vị trực thuộc")]
        public DonViTrucThuocEnum DonViTrucThuoc
        {
            get
            {
                return _DonViTrucThuoc;
            }
            set
            {
                SetPropertyValue("DonViTrucThuoc", ref _DonViTrucThuoc, value);
            }
        }
        public BoPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiBoPhan = LoaiBoPhanEnum.PhongBan;
            NgungHoatDong = false;
            if (LoaiBoPhan != LoaiBoPhanEnum.Truong)
                ThongTinTruong = HamDungChung.ThongTinTruong(Session);          
        }

        protected override void OnLoaded()
        {
            base.OnLoaded(); 

            //Lấy mã trường hiện tại dùng để phân quyền
            MaTruong = TruongConfig.MaTruong;       
        }

        [ImmediatePostData]
        IBindingList ITreeNode.Children
        {
            get { return ListBoPhanCon; }
        }

        string ITreeNode.Name
        {
            get { return TenBoPhan; }
        }

        ITreeNode ITreeNode.Parent
        {
            get { return _BoPhanCha; }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted && string.IsNullOrWhiteSpace(MaQuanLy_UIS) && TruongConfig.MaTruong !="VLU" && TruongConfig.MaTruong != "VHU")
                LayMaQuanLyUIS();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (TruongConfig.MaTruong == "DNU" || TruongConfig.MaTruong == "HUFLIT")
            {
                SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                param[0] = new SqlParameter("@OidBoPhan", this.Oid);
                DataProvider.ExecuteNonQuery("spd_PMS_DongBoDuLieu_BoPhan_UIS", System.Data.CommandType.StoredProcedure, param);
            }
        }

        protected void LayMaQuanLyUIS()
        {
            try
            {
                string sql = "SELECT TOP 1 ISNULL(MaQuanLy_UIS,0)"
                            + " FROM BoPhan"
                            + " ORDER BY CONVERT(INT,MaQuanLy_UIS) DESC";
                object kq = null;
                kq = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                int ma = Convert.ToInt32(kq) + 1;
                if (ma < 10)
                {
                    MaQuanLy_UIS = string.Concat("0", (Convert.ToInt32(kq) + 1).ToString());
                }
                else
                    MaQuanLy_UIS = ma.ToString();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Mã quản lý UIS của danh mục Bộ phận không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

    }
}
