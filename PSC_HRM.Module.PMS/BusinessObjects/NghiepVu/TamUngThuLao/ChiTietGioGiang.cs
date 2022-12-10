using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.Enum;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Persistent.Base;
using System.Windows.Forms;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.PMS.GioChuan;

namespace PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao
{
    [ModelDefault("Caption", "Chi tiết giờ giảng")]
    [Appearance("Enabled_SoGio", TargetItems = "SoGio", Enabled = false, Criteria = "CongTru = 0")]
    [Appearance("Show_SoGio", TargetItems = "SoGio", Enabled = true, Criteria = "CongTru = 1")]
    [Appearance("Enabled_Full", TargetItems = "*", Enabled = false, Criteria = "DongBo = 1")]
    public class ChiTietGioGiang : BaseObject
    {
       
        private NhanVien_GioGiang _NhanVien_GioGiang;
        [Association("NhanVien_GioGiang-ListChiTietGioGiang")]
        [ModelDefault("Caption", "Cán bộ")]
        [Browsable(false)]
        [ImmediatePostData]
        public NhanVien_GioGiang NhanVien_GioGiang
        {
            get
            {
                return _NhanVien_GioGiang;
            }
            set
            {
                SetPropertyValue("NhanVien_GioGiang", ref _NhanVien_GioGiang, value);
                if (!IsLoading&& NhanVien_GioGiang != null)
                    ThongTinBangChot = Session.FindObject<ThongTinBangChot>(CriteriaOperator.Parse("NhanVien =? and BangChotThuLao.NamHoc =? and BangChotThuLao.HocKy.MaQuanLy ='HK01'", NhanVien_GioGiang != null ? NhanVien_GioGiang.NhanVien.Oid : Guid.Empty, NhanVien_GioGiang != null ? NhanVien_GioGiang.QuanLyGioGiang.NamHoc.Oid : Guid.Empty));
            }
        }

        private decimal _SoGio;
        private CongTruPMSEnum _CongTru;
        private string _GhiChu;
        private bool _DongBo;
        private bool _KT;
        private bool _DaTinhThuLao;
        //private BangChotThuLao _BangChotThuLao;
        private ThongTinBangChot _ThongTinBangChot;


        [ModelDefault("Caption", "Số giờ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        [RuleRange("ChiTietGioGiang_SoGio", DefaultContexts.Save, 1, 100000, "Số giờ phải > 0 và là khoản trừ!")]
        public decimal SoGio
        {
            get { return _SoGio; }
            set
            {
                SetPropertyValue("SoGio", ref _SoGio, value);
                if (!IsLoading && value != 0)
                {
                    Check();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cộng/Trừ")]
        [ModelDefault("AllowEdit","false")]
        public CongTruPMSEnum CongTru
        {
            get { return _CongTru; }
            set { SetPropertyValue("CongTru", ref _CongTru, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Đồng bộ")]
        [Browsable(false)]
        public bool DongBo
        {
            get { return _DongBo; }
            set { SetPropertyValue("DongBo", ref _DongBo, value); }
        }
        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty("ChiTietGioGiang.KT", DefaultContexts.Save, "Số giờ tạm ứng đã vượt định mức!", SkipNullOrEmptyValues = false)]
        [ModelDefault("Caption", "kiểm tra")]
        public bool KT
        {
            get
            {
                return !_KT;
            }
            set
            {
                SetPropertyValue("KT", ref _KT, value);
            }
        }
        //[ModelDefault("Caption", "Bảng chốt thù lao")]
        //public BangChotThuLao BangChotThuLao
        //{
        //    get { return _BangChotThuLao; }
        //    set { SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value); }
        //}

        [ModelDefault("Caption", "Thông tin bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinBangChot ThongTinBangChot
        {
            get { return _ThongTinBangChot; }
            set { SetPropertyValue("ThongTinBangChot", ref _ThongTinBangChot, value); }
        }
        [ModelDefault("Caption", "Đã tính thù lao")]
        [ModelDefault("AllowEdit", "False")]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }

        private Guid _OidChiTietThuLaoNhanVien;
        [Browsable(false)]
        public Guid OidChiTietThuLaoNhanVien
        {
            get { return _OidChiTietThuLaoNhanVien; }
            set { SetPropertyValue("OidChiTietThuLaoNhanVien", ref _OidChiTietThuLaoNhanVien, value); }
        }
        public ChiTietGioGiang(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            KT = true;
            CongTru = CongTruPMSEnum.Tru;
            GhiChu = "Tạm ứng giờ giảng";
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            if(!IsDeleted)
            {
                NhanVien_GioGiang.XyLyTongGio();
            }           
        }
        protected override void OnDeleting()
        {
            if (DongBo == false)
            {
                base.OnDeleting();
                NhanVien_GioGiang.XyLyTongGio_Xoa(SoGio);
            }
            else
            {
                NhanVien_GioGiang.XuLyThemKhongXoaDongBo(SoGio, DongBo, GhiChu, ThongTinBangChot.Oid);
                MessageBox.Show("Dữ liệu đồng bộ không được xóa, nếu thay đổi cần đồng bộ lại", "Thông báo!");
                this.Save();
            }
        }
        void Check()
        {
            if (ThongTinBangChot != null)
            {
                CriteriaOperator fchitiet = CriteriaOperator.Parse("NhanVien_GioGiang = ?", NhanVien_GioGiang.Oid);
                XPCollection<ChiTietGioGiang> list = new XPCollection<ChiTietGioGiang>(Session, fchitiet);
                decimal TongTru = 0;
                decimal DinhMuc = 0;
                if (CongTru.GetHashCode() == 1)
                    TongTru = TongTru + SoGio;
                foreach (ChiTietGioGiang item in list)
                {
                    if (item.CongTru.GetHashCode() == 1)
                    {
                        TongTru = TongTru + item.SoGio;
                    }
                }
                CriteriaOperator fdinhMuc = CriteriaOperator.Parse("QuanLyGioChuan.ThongTinTruong = ? and NhanVien = ? and QuanLyGioChuan.NamHoc = ?", NhanVien_GioGiang.QuanLyGioGiang.ThongTinTruong.Oid, NhanVien_GioGiang.NhanVien.Oid, NhanVien_GioGiang.QuanLyGioGiang.NamHoc.Oid);
                XPCollection<DinhMucChucVu_NhanVien> listdinhmuc = new XPCollection<DinhMucChucVu_NhanVien>(Session, fdinhMuc);
                foreach (DinhMucChucVu_NhanVien item in listdinhmuc)
                {
                    DinhMuc = (item.SoGioDinhMuc / 2);
                }
                if (DinhMuc == 0)
                {
                    MessageBox.Show("Nhân viên chưa có định mức giờ chuẩn!", "Thông báo!");
                }
                if (TongTru > DinhMuc)
                {
                    KT = true;
                }
                else
                {
                    KT = false;
                }
            }
        }
    }
}