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
using PSC_HRM.Module.BaoHiem;
using DevExpress.Xpo.DB;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết QĐ thôi hưởng phụ cấp trách nhiệm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhThoiHuongPhuCapTrachNhiem;ThongTinNhanVien")]
    public class ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private decimal _HSPCTrachNhiemCu;
         private DateTime _NgayThoiHuongHSPCTrachNhiemCu;
        private string _LyDo;
        private GiayToHoSo _GiayToHoSo;
        private QuyetDinhThoiHuongPhuCapTrachNhiem _QuyetDinhThoiHuongPhuCapTrachNhiem;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định hưởng phụ cấp trách nhiệm")]
        [Association("QuyetDinhThoiHuongPhuCapTrachNhiem-ListChiTietQuyetDinhThoiHuongPhuCapTrachNhiem")]
        public QuyetDinhThoiHuongPhuCapTrachNhiem QuyetDinhThoiHuongPhuCapTrachNhiem
        {
            get
            {
                return _QuyetDinhThoiHuongPhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiHuongPhuCapTrachNhiem", ref _QuyetDinhThoiHuongPhuCapTrachNhiem, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //    NgayThoiHuongHSPCTrachNhiemCu = value.NgayHieuLuc;
                //}
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
                    HSPCTrachNhiemCu = value.NhanVienThongTinLuong.HSPCTrachNhiem;
                }
            }
        }

        [ModelDefault("Caption", "HSPC Trách nhiệm cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiemCu
        {
            get
            {
                return _HSPCTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiemCu", ref _HSPCTrachNhiemCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày thôi hưởng HSPC trách nhiệm cũ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayThoiHuongHSPCTrachNhiemCu
        {
            get
            {
                return _NgayThoiHuongHSPCTrachNhiemCu;
            }
            set
            {
                SetPropertyValue("NgayThoiHuongHSPCTrachNhiemCu", ref _NgayThoiHuongHSPCTrachNhiemCu, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
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

        public ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thôi hưởng phụ cấp"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhThoiHuongPhuCapTrachNhiem != null
                && !IsLoading
                && !QuyetDinhThoiHuongPhuCapTrachNhiem.IsDirty)
                QuyetDinhThoiHuongPhuCapTrachNhiem.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && Oid != Guid.Empty)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                if (QuyetDinhThoiHuongPhuCapTrachNhiem.QuyetDinhMoi)
                {
                    //cập nhật thông tin vào hồ sơ
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (NgayThoiHuongHSPCTrachNhiemCu != DateTime.MinValue)
            {
                if (QuyetDinhThoiHuongPhuCapTrachNhiem.QuyetDinhMoi)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHuongHSPCTrachNhiemMoi>?",
                                                                    ThongTinNhanVien, NgayThoiHuongHSPCTrachNhiemCu);
                    SortProperty sort = new SortProperty("QuyetDinhThoiHuongPhuCapTrachNhiem.NgayHieuLuc", SortingDirection.Descending);
                    using (XPCollection<ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem> qdList = new XPCollection<ChiTietQuyetDinhThoiHuongPhuCapTrachNhiem>(Session, filter, sort))
                    {
                        qdList.TopReturnedObjects = 1;
                        if (qdList.Count == 0 ||
                            (qdList.Count == 1 && qdList[0].QuyetDinhThoiHuongPhuCapTrachNhiem == QuyetDinhThoiHuongPhuCapTrachNhiem))
                        {
                            //cập nhật thông tin vào hồ sơ
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = HSPCTrachNhiemCu;
                        }
                    }
                }
            }

            base.OnDeleting();
        }
    }
}
