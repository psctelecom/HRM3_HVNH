using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using PSC_HRM.Module.CauHinh;
using System.Data;

namespace PSC_HRM.Module.BaoMat
{
    [DefaultClassOptions]
    [ModelDefault("IsCloneable", "True")]
    [ImageName("BO_User")]
    [DefaultProperty("UserName")]
    [ModelDefault("Caption", "Người sử dụng")]
    [Appearance("NguoiSuDung.Hide", TargetItems = "PhanLoai", Visibility = ViewItemVisibility.Hide, Criteria = "Hide='VHU'")]
    public class NguoiSuDung : SecuritySystemUser, IThongTinTruong
    {
        // Fields...
        private bool _MoKhoaSoLuong;
        private bool _DuocSuDungBanLamViec;
        private AccountTypeEnum _PhanLoai;
        private ThongTinTruong _ThongTinTruong;
        private ThongTinNhanVien _ThongTinNhanVien;
        private PhanQuyenBaoCao _PhanQuyenBaoCao;
        private PhanQuyenDonVi _PhanQuyenBoPhan;
        private string _MatKhau;
        private string _Hide;
        private string _GhiChu2;
        private bool _DaCapNhat;

        [ModelDefault("Caption", "Phân quyền đơn vị")]
        [Browsable(false)]
        public string MatKhau
        {
            get { return _MatKhau; }
            set { SetPropertyValue("MatKhau", ref _MatKhau, value); }
        }

        [ModelDefault("Caption", "Phân quyền đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanQuyenDonVi PhanQuyenBoPhan
        {
            get
            {
                return _PhanQuyenBoPhan;
            }
            set
            {
                SetPropertyValue("PhanQuyenBoPhan", ref _PhanQuyenBoPhan, value);
            }
        }

        [ModelDefault("Caption", "Phân quyền báo cáo")]
        public PhanQuyenBaoCao PhanQuyenBaoCao
        {
            get
            {
                return _PhanQuyenBaoCao;
            }
            set
            {
                SetPropertyValue("PhanQuyenBaoCao", ref _PhanQuyenBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Phân loại")]
        public AccountTypeEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
            }
        }

        [ModelDefault("Caption", "Được sử dụng bàn làm việc")]
        public bool DuocSuDungBanLamViec
        {
            get
            {
                return _DuocSuDungBanLamViec;
            }
            set
            {
                SetPropertyValue("DuocSuDungBanLamViec", ref _DuocSuDungBanLamViec, value);
            }
        }

        [ModelDefault("Caption", "Mở khóa sổ lương")]
        public bool MoKhoaSoLuong
        {
            get
            {
                return _MoKhoaSoLuong;
            }
            set
            {
                SetPropertyValue("MoKhoaSoLuong", ref _MoKhoaSoLuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [Association("ThongTinTruong-ListNguoiSuDung")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
                if (!IsLoading && value != null)
                {
                    //
                    UpdateNVList();
                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if(!IsLoading && value != null)
                {
                    MatKhau = "QNU@" + value.MaQuanLy;
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Người ký tên báo cáo")]
        [Association("NguoiSuDung-ListNguoiKyTenBaoCao")]
        public XPCollection<NguoiKyTen> ListNguoiKyTenBaoCao
        {
            get
            {
                return GetCollection<NguoiKyTen>("ListNguoiKyTenBaoCao");
            }
        }

        [ModelDefault("Caption", "Ẩn")]
        [NonPersistent]
        [Browsable(false)]
        public string Hide
        {
            get { return _Hide; }
            set { SetPropertyValue("Hide", ref _Hide, value); }
        }

        [ModelDefault("Caption", "Ghi chứ 2")]
        [Browsable(false)]
        [Size(-1)]
        public string GhiChu2
        {
            get { return _GhiChu2; }
            set { SetPropertyValue("GhiChu2", ref _GhiChu2, value); }
        }

        [ModelDefault("Caption", "Đã cập nhật")]
        [Browsable(false)]
        public bool DaCapNhat
        {
            get
            {
                return _DaCapNhat;
            }
            set
            {
                SetPropertyValue("DaCapNhat", ref _DaCapNhat, value);
            }
        }


        public NguoiSuDung(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            Hide = TruongConfig.MaTruong;

        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            Hide = TruongConfig.MaTruong;
        }
        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }
        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyTenList { get; set; }

        public void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = CriteriaOperator.Parse("ThongTinTruong=?", ThongTinTruong.Oid);
        }
        protected override void OnSaved()
        {
            base.OnSaved();

            //
            if (!IsDeleted)
            {

                CauHinhChung cauHinhChung = HamDungChung.CauHinhChung;
                if (cauHinhChung != null && cauHinhChung.DongBoTaiKhoan && !DaCapNhat)
                {

                    DataProvider.ExecuteNonQuery("UPDATE nsd SET nsd.DaCapNhat = 1, nsd.MatKhau = N'QNU@"+ ThongTinNhanVien.SoHieuCongChuc.ToString() +"' FROM dbo.NguoiSuDung nsd WHERE Oid = '" + Oid + "'", CommandType.Text);

                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@User", Oid);
                    param[1] = new SqlParameter("@UserDangLogin", HamDungChung.CurrentUser().UserName);
                    DataProvider.ExecuteNonQuery("spd_HeThong_CreateUserURM", System.Data.CommandType.StoredProcedure, param);
                }
            }
        }

    }

}
