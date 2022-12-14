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
    [ModelDefault("Caption", "Chi tiết QĐ hưởng phụ cấp trách nhiệm")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhHuongPhuCapTrachNhiem;ThongTinNhanVien")]
    public class ChiTietQuyetDinhHuongPhuCapTrachNhiem : BaseObject
    {
        // Fields...
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private decimal _HSPCTrachNhiemMoi;
        private DateTime _NgayHuongHSPCTrachNhiemMoi;
        private string _LyDo;
        private GiayToHoSo _GiayToHoSo;
        private QuyetDinhHuongPhuCapTrachNhiem _QuyetDinhHuongPhuCapTrachNhiem;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định hưởng phụ cấp trách nhiệm")]
        [Association("QuyetDinhHuongPhuCapTrachNhiem-ListChiTietQuyetDinhHuongPhuCapTrachNhiem")]
        public QuyetDinhHuongPhuCapTrachNhiem QuyetDinhHuongPhuCapTrachNhiem
        {
            get
            {
                return _QuyetDinhHuongPhuCapTrachNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongPhuCapTrachNhiem", ref _QuyetDinhHuongPhuCapTrachNhiem, value);
                if (!IsLoading && value != null)
                {
                    //GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                    //GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                    //GiayToHoSo.LuuTru = value.LuuTru;
                    //GiayToHoSo.TrichYeu = value.NoiDung;
                    NgayHuongHSPCTrachNhiemMoi = value.NgayHieuLuc;
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

        [ModelDefault("Caption", "HSPC Trách nhiệm mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCTrachNhiemMoi
        {
            get
            {
                return _HSPCTrachNhiemMoi;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiemMoi", ref _HSPCTrachNhiemMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày hưởng HSPC trách nhiệm mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongHSPCTrachNhiemMoi
        {
            get
            {
                return _NgayHuongHSPCTrachNhiemMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCTrachNhiemMoi", ref _NgayHuongHSPCTrachNhiemMoi, value);
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

        public ChiTietQuyetDinhHuongPhuCapTrachNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định hưởng phụ cấp trách nhiệm"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
            
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhHuongPhuCapTrachNhiem != null
                && !IsLoading
                && !QuyetDinhHuongPhuCapTrachNhiem.IsDirty)
                QuyetDinhHuongPhuCapTrachNhiem.IsDirty = true;
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
                if (QuyetDinhHuongPhuCapTrachNhiem.QuyetDinhMoi)
                {
                    //cập nhật thông tin vào hồ sơ
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = HSPCTrachNhiemMoi;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (NgayHuongHSPCTrachNhiemMoi != DateTime.MinValue)
            {
                if (QuyetDinhHuongPhuCapTrachNhiem.QuyetDinhMoi)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHuongHSPCTrachNhiemMoi>?",
                                                                    ThongTinNhanVien, NgayHuongHSPCTrachNhiemMoi);
                    SortProperty sort = new SortProperty("QuyetDinhHuongPhuCapTrachNhiem.NgayHieuLuc", SortingDirection.Descending);
                    using (XPCollection<ChiTietQuyetDinhHuongPhuCapTrachNhiem> qdList = new XPCollection<ChiTietQuyetDinhHuongPhuCapTrachNhiem>(Session, filter, sort))
                    {
                        qdList.TopReturnedObjects = 1;
                        if (qdList.Count == 0 ||
                            (qdList.Count == 1 && qdList[0].QuyetDinhHuongPhuCapTrachNhiem == QuyetDinhHuongPhuCapTrachNhiem))
                        {
                            //cập nhật thông tin vào hồ sơ
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                        }
                    }
                }
            }

            //xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }
}
