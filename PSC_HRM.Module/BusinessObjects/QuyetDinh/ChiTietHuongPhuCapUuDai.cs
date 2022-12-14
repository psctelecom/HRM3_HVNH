using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết hưởng phụ cấp ưu đãi")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhHuongPhuCapUuDai")]
    //DLU
    public class ChiTietHuongPhuCapUuDai : TruongBaseObject
    {
        private QuyetDinhHuongPhuCapUuDai _QuyetDinhHuongPhuCapUuDai;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private GiayToHoSo _GiayToHoSo;
        private int _PhuCapUuDai;
        private int _PhuCapUuDaiMoi;
        private DateTime _TuNgay;
        private string _GhiChu;
        
        [Browsable(false)]
        [Association("QuyetDinhHuongPhuCapUuDai-ListChiTietHuongPhuCapUuDai")]
        public QuyetDinhHuongPhuCapUuDai QuyetDinhHuongPhuCapUuDai
        {
            get
            {
                return _QuyetDinhHuongPhuCapUuDai;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongPhuCapUuDai", ref _QuyetDinhHuongPhuCapUuDai, value);
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
                    PhuCapUuDai = value.NhanVienThongTinLuong.PhuCapUuDai;
                    PhuCapUuDaiMoi = value.NhanVienThongTinLuong.PhuCapUuDai;
                }
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

        [ModelDefault("Caption", "% PC ưu đãi cũ")]
        public int PhuCapUuDai
        {
            get
            {
                return _PhuCapUuDai;
            }
            set
            {
                SetPropertyValue("PhuCapUuDai", ref _PhuCapUuDai, value);
            }
        }

        [ModelDefault("Caption", "% PC ưu đãi mới")]
        public int PhuCapUuDaiMoi
        {
            get
            {
                return _PhuCapUuDaiMoi;
            }
            set
            {
                SetPropertyValue("PhuCapUuDaiMoi", ref _PhuCapUuDaiMoi, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
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

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "PhuCapUuDaiMoi<>PhuCapUuDai")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        public ChiTietHuongPhuCapUuDai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định hưởng phụ cấp ưu đãi"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            }  
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

            if (!IsDeleted)
            {
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai = PhuCapUuDaiMoi;
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai = PhuCapUuDai;

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
