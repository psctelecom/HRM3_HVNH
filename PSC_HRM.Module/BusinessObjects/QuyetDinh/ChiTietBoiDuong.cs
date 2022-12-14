using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết bồi dưỡng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhBoiDuong;ThongTinNhanVien")]
    [Appearance("ChiTietBoiDuong", TargetItems = "NhanVienThayThe;BoPhanThayThe", Visibility = ViewItemVisibility.Hide, Criteria = "QuyetDinhBoiDuong.QuyetDinhBoiDuongThayThe is null")]
    public class ChiTietBoiDuong : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private ThongTinNhanVien _NhanVienThayThe;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private BoPhan _BoPhanThayThe;
        private QuyetDinhBoiDuong _QuyetDinhBoiDuong;
        private GiayToHoSo _GiayToHoSo;

        private TinhTrang _TinhTrang;
        private string _NhiemVu;
        private String _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định bồi dưỡng")]
        [Association("QuyetDinhBoiDuong-ListChiTietBoiDuong")]
        public QuyetDinhBoiDuong QuyetDinhBoiDuong
        {
            get
            {
                return _QuyetDinhBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoiDuong", ref _QuyetDinhBoiDuong, value);
                if (!IsLoading && value != null)
                {
                    //GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                    //GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                    //GiayToHoSo.LuuTru = value.LuuTru;
                    //GiayToHoSo.TrichYeu = value.NoiDung;
                    //
                    UpdateTinhTrangList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    UpdateNhanVienList();
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
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
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
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị cán bộ thay thế")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "QuyetDinhBoiDuong.QuyetDinhBoiDuongThayThe is not null and QuyetDinhBoiDuong.QuyetDinhBoiDuongThayThe.ListChiTietBoiDuong.Count <= 1")]
        public BoPhan BoPhanThayThe
        {
            get
            {
                return _BoPhanThayThe;
            }
            set
            {
                SetPropertyValue("BoPhanThayThe", ref _BoPhanThayThe, value);
                if (!IsLoading)
                {
                    UpdateNhanVienThayTheList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVThayTheList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Thay thế cho")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "QuyetDinhBoiDuong.QuyetDinhBoiDuongThayThe is not null and QuyetDinhBoiDuong.QuyetDinhBoiDuongThayThe.ListChiTietBoiDuong.Count <= 1")]
        public ThongTinNhanVien NhanVienThayThe
        {
            get
            {
                return _NhanVienThayThe;
            }
            set
            {
                SetPropertyValue("NhanVienThayThe", ref _NhanVienThayThe, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhanThayThe == null
                        || value.BoPhan.Oid != BoPhanThayThe.Oid)
                        BoPhanThayThe = value.BoPhan;                    
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("TinhTrangList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Tình trạng hưởng lương")]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Nhiệm vụ")]
        public string NhiemVu
        {
            get
            {
                return _NhiemVu;
            }
            set
            {
                SetPropertyValue("NhiemVu", ref _NhiemVu, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public String GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Browsable(false)]
        public TinhTrang TinhTrangCu { get; set; }

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

        public ChiTietBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định bồi dưỡng"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (QuyetDinhBoiDuong != null
        //        && !IsLoading
        //        && !QuyetDinhBoiDuong.IsDirty)
        //        QuyetDinhBoiDuong.IsDirty = true;
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            UpdateTinhTrangList();
            UpdateGiayToList();
            //if (BoPhan != null)
            //{
            //    BoPhanText = BoPhan.TenBoPhan;
            //} 
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVThayTheList { get; set; }

        [Browsable(false)]
        public XPCollection<TinhTrang> TinhTrangList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
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

        private void UpdateNhanVienThayTheList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);

            if (QuyetDinhBoiDuong != null && QuyetDinhBoiDuong.QuyetDinhBoiDuongThayThe != null)
            {
            }
        }

        private void UpdateTinhTrangList()
        {
            if (TinhTrangList == null)
                TinhTrangList = new XPCollection<TinhTrang>(Session);
            if (this.QuyetDinhBoiDuong.QuocGia != null)
            {
                if (this.QuyetDinhBoiDuong.QuocGia.TenQuocGia.Contains("Việt Nam"))
                    TinhTrangList.Criteria = CriteriaOperator.Parse("LoaiTinhTrang = ? OR LoaiTinhTrang = ?", Trong_NgoaiNuocEnum.TrongNuoc, Trong_NgoaiNuocEnum.CaHai);
                else
                    TinhTrangList.Criteria = CriteriaOperator.Parse("LoaiTinhTrang = ? OR LoaiTinhTrang = ?", Trong_NgoaiNuocEnum.NgoaiNuoc, Trong_NgoaiNuocEnum.CaHai);
            }
        }
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted
                && Oid != Guid.Empty)
            {
                //Cập nhật tình trạng
                TinhTrangCu = ThongTinNhanVien.TinhTrang;
                if (TinhTrang != null && QuyetDinhBoiDuong.QuyetDinhMoi)
                {
                    if (QuyetDinhBoiDuong.TuNgay <= HamDungChung.GetServerTime() &&  TinhTrang != null)
                    {
                        ThongTinNhanVien.TinhTrang = TinhTrang;
                    }
                }

                //1. qua trinh boi duong
                QuaTrinhHelper.CreateQuaTrinhBoiDuong(Session, QuyetDinhBoiDuong, ThongTinNhanVien);

                //2. chứng chỉ
                BoiDuongHelper.CreateChungChi(Session, QuyetDinhBoiDuong, ThongTinNhanVien);

                //3. quá trình đi nước ngoài
                if (QuyetDinhBoiDuong.QuocGia != null)
                {
                    QuocGia current = HamDungChung.GetCurrentQuocGia(Session);
                    if (current != null && current.Oid != QuyetDinhBoiDuong.QuocGia.Oid)
                    {
                        QuaTrinhHelper.CreateQuaTrinhDiNuocNgoai(Session, QuyetDinhBoiDuong, ThongTinNhanVien, QuyetDinhBoiDuong.QuocGia, QuyetDinhBoiDuong.TuNgay, QuyetDinhBoiDuong.DenNgay);
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                //Cập nhật tình trạng
                if (TinhTrang != null)
                {
                    ThongTinNhanVien.TinhTrang = TinhTrangCu;
                }
                //1. xóa quá trình bồi dưỡng
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhBoiDuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhBoiDuong, ThongTinNhanVien));

                //2. Xóa chứng chỉ
                BoiDuongHelper.DeleteChungChi(Session, QuyetDinhBoiDuong, ThongTinNhanVien);

                //3. xóa Qua trinh đi nước ngoài
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhDiNuocNgoai>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhBoiDuong, ThongTinNhanVien));

                //xóa giấy tờ hồ sơ
                if (GiayToHoSo != null)
                {
                    Session.Delete(GiayToHoSo);
                    Session.Save(GiayToHoSo);
                }
            }
            base.OnDeleting();
        }
    }

}
