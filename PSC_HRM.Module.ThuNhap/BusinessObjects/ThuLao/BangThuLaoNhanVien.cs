using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.Enum;


namespace PSC_HRM.Module.ThuNhap.ThuLao
{
    [DefaultClassOptions]
    [ImageName("BO_ThuLao")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng thù lao cán bộ")]
    [Appearance("BangThuLaoNhanVien.Khoa", TargetItems = "KyTinhLuong;NgayLap", Enabled = false,
        Criteria = "(KyTinhLuong is not null and KyTinhLuong.KhoaSo) or ChungTu is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangThuLaoNhanVien.Unique", DefaultContexts.Save, "KyTinhLuong;ThongTinTruong;ChungTu")]
    [Appearance("Hide_HVNH", TargetItems = "HienLenWeb"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]
    [Appearance("Hide_UEL", TargetItems = "ListChiTietTheoDoiTruTietChuan;LoaiGiangVien;KyTinhPMS"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat <> 'UEL'")]
    [Appearance("Hide_CoHuu", TargetItems = "KyTinhPMS"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "LoaiGiangVien = 0")]
    [Appearance("Hide_ThinhGiang", TargetItems = "ChungTu"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "LoaiGiangVien = 1")]



    public class BangThuLaoNhanVien : BaseObject, IThongTinTruong
    {
        private ChungTu.ChungTu _ChungTu;
        private KyTinhLuong _KyTinhLuong;
        private DateTime _NgayLap;
        private ThongTinTruong _ThongTinTruong;
        private bool _HienLenWeb;
        private LoaiGiangVienEnum _LoaiGiangVien;
        private PSC_HRM.Module.PMS.DanhMuc.KyTinhPMS _KyTinhPMS;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Hiện lên web")]
        public bool HienLenWeb
        {
            get
            {
                return _HienLenWeb;
            }
            set
            {
                SetPropertyValue("HienLenWeb", ref _HienLenWeb, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
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
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("BangThuLaoNhanVien-ListChiTietThuLaoNhanVien")]
        public XPCollection<ChiTietThuLaoNhanVien> ListChiTietThuLaoNhanVien
        {
            get
            {
                return GetCollection<ChiTietThuLaoNhanVien>("ListChiTietThuLaoNhanVien");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết theo dõi trừ tiết chuẩn")]
        [Association("BangThuLaoNhanVien-ListChiTietTheoDoiTruTietChuan")]
        public XPCollection<ChiTietTheoDoiTruTietChuan> ListChiTietTheoDoiTruTietChuan
        {
            get
            {
                return GetCollection<ChiTietTheoDoiTruTietChuan>("ListChiTietTheoDoiTruTietChuan");
            }
        }

        [ModelDefault("Caption","Chứng từ")]
        //chỉ dùng để truy vết
        //[Browsable(false)]
        public ChungTu.ChungTu ChungTu
        {
            get
            {
                return _ChungTu;
            }
            set
            {
                SetPropertyValue("ChungTu", ref _ChungTu, value);
            }
        }
        #region Thêm mới - dùng cho Thỉnh giảng
        [ModelDefault("Caption", "Áp dụng")]
        [ImmediatePostData]
        public LoaiGiangVienEnum LoaiGiangVien
        {
            get
            {
                return _LoaiGiangVien;
            }
            set
            {
                SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value);
            }
        }
        [ModelDefault("Caption", "Đợt chi trả")]
        //[ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public PSC_HRM.Module.PMS.DanhMuc.KyTinhPMS KyTinhPMS
        {
            get
            {
                return _KyTinhPMS;
            }
            set
            {
                SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value);
            }
        }
        #endregion
        public BangThuLaoNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);

            //if (ThongTinTruong != null)
            //    KyTinhLuongList.Criteria = CriteriaOperator.Parse("ThongTinTruong=? and !KhoaSo", ThongTinTruong);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            LoaiGiangVien = LoaiGiangVienEnum.CoHuu;
            UpdateKyTinhLuongList();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NgayLap = HamDungChung.GetServerTime();
            if (ThongTinTruong != null)
                KyTinhLuong = Session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("ThongTinTruong = ? and TuNgay<=? and DenNgay>=? and !KhoaSo", ThongTinTruong, NgayLap, NgayLap));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateKyTinhLuongList();
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            //if(ChungTu!=null)
            //{
            //    XtraMessageBox.Show("Đã lập chứng từ - không thể xóa!", "Thông báo");
            //    return;
            //}
            //else
            //{
            //    string user = "";
            //    using (DialogUtil.AutoWait("Đang xóa dữ liệu"))
            //    {
            //        user = HamDungChung.CurrentUser().UserName.ToString();
            //        SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
            //        param[0] = new SqlParameter("@BangThuLao", this.Oid);
            //        param[1] = new SqlParameter("@User", user != string.Empty ? user : "");
            //        DataProvider.ExecuteNonQuery("spd_PMS_BangThuLaoNhanVien_XoaFullChiTietThuLao", System.Data.CommandType.StoredProcedure, param);
            //    }
            //}
        }
    }

}
