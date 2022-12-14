using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.QuaTrinh;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định đi công tác")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhDiCongTac;ThongTinNhanVien")]
    [Appearance("ChiTietQuyetDinhDiCongTac.NgoaiTruong", TargetItems = "NhanVienText;ChucVuText", Visibility = ViewItemVisibility.Hide, Criteria = "NgoaiTruong!=1")]
    [Appearance("ChiTietQuyetDinhDiCongTac.TrongTruong", TargetItems = "BoPhan;ThongTinNhanVien", Visibility = ViewItemVisibility.Hide, Criteria = "NgoaiTruong=1")]
    [Appearance("ChiTietQuyetDinhDiCongTac.HideNEU", TargetItems = "NhanVienThayDoi", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong='NEU'")]
    [Appearance("ChiTietQuyetDinhDiCongTac.Hide", TargetItems = "NgoaiTruong", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong!='NEU'")]
    public class ChiTietQuyetDinhDiCongTac : TruongBaseObject
    {
        private QuyetDinhDiCongTac _QuyetDinhDiCongTac;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;
        private ViTriCongTac _ViTriCongTac;
        private ThongTinNhanVien _NhanVienThayDoi;
        private bool _NgoaiTruong;
        private string _NhanVienText;
        private string _ChucVuText;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định đi công tác")]
        [Association("QuyetDinhDiCongTac-ListChiTietQuyetDinhDiCongTac")]
        public QuyetDinhDiCongTac QuyetDinhDiCongTac
        {
            get
            {
                return _QuyetDinhDiCongTac;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiCongTac", ref _QuyetDinhDiCongTac, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //}
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "NgoaiTruong!=1")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                    BoPhanText = value.TenBoPhan;

                }
            }
        }
        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "NgoaiTruong!=1")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    NhanVienText = value.HoTen;
                    if (value.ChucVu != null)
                        ChucVuText = value.ChucVu.TenChucVu;
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                }
            }
        }

        [ModelDefault("Caption", "Vị trí")]
        public ViTriCongTac ViTriCongTac
        {
            get
            {
                return _ViTriCongTac;
            }
            set
            {
                SetPropertyValue("ViTriCongTac", ref _ViTriCongTac, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ thay đổi")]
        [DataSourceProperty("NVThayDoiList", DataSourcePropertyIsNullMode.SelectAll)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien NhanVienThayDoi
        {
            get
            {
                return _NhanVienThayDoi;
            }
            set
            {
                SetPropertyValue("NhanVienThayDoi", ref _NhanVienThayDoi, value);                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngoài trường")]
        public bool NgoaiTruong
        {
            get
            {
                return _NgoaiTruong;
            }
            set
            {
                SetPropertyValue("NgoaiTruong", ref _NgoaiTruong, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "NgoaiTruong=1")]
        public string NhanVienText
        {
            get
            {
                return _NhanVienText;
            }
            set
            {
                SetPropertyValue("NhanVienText", ref _NhanVienText, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "NgoaiTruong=1")]
        public string ChucVuText
        {
            get
            {
                return _ChucVuText;
            }
            set
            {
                SetPropertyValue("ChucVuText", ref _ChucVuText, value);
            }
        }

        //[Aggregated]
        //[Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        //[ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        public ChiTietQuyetDinhDiCongTac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định công tác"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                if (QuyetDinhDiCongTac != null
                    && !IsLoading
                    && !QuyetDinhDiCongTac.IsDirty)
                    QuyetDinhDiCongTac.IsDirty = true;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNVList();
            UpdateGiayToList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 

        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVThayDoiList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void UpdateNVThayDoiList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }
        private void UpdateGiayToList()
        {
            //if (GiayToList == null)
            //    GiayToList = new XPCollection<GiayToHoSo>(Session);

            if (ThongTinNhanVien != null)
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    GiayToList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", ThongTinNhanVien.Oid, "%Quyết định%");
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && (Oid == Guid.Empty || Session is NestedUnitOfWork))
            {
                //4. qua trinh di cong tac
                QuaTrinhHelper.CreateQuaTrinhCongTac(Session, ThongTinNhanVien, this.QuyetDinhDiCongTac);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //4. delete quá trình đi cong tac
                CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                        ThongTinNhanVien, this.QuyetDinhDiCongTac);
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhCongTac>(Session, filter);

                //xóa giấy tờ hồ sơ
                if (GiayToHoSo != null)
                {
                    Session.Delete(GiayToHoSo);
                    Session.Save(GiayToHoSo);
                }
            }
            base.OnDeleting();
        }

        
        public void XuLy_BUH()
        {
            SqlParameter[] parameter = new SqlParameter[3];
            parameter[0] = new SqlParameter("@NhanVien", ThongTinNhanVien.Oid);
            parameter[1] = new SqlParameter("@TuNgay", QuyetDinhDiCongTac.TuNgay);
            parameter[2] = new SqlParameter("@DenNgay", QuyetDinhDiCongTac.DenNgay);

            DataProvider.ExecuteNonQuery("spd_WebChamCong_CC_ChamCongTheoNgay_ChotLai", CommandType.StoredProcedure, parameter);
        }
    }

}
