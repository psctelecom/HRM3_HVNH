using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TuyenDung
{
    [ImageName("BO_Resume")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Hội đồng hết tập sự")]
    [DefaultProperty("ThongTinNhanVien")]
    public class HoiDongHetTapSu : BaseObject, IBoPhan
    {
        // Fields...
        private ChucVu _ChucVu;
        private QuyetDinhThanhLapHoiDongDanhGiaHetTapSu _QuyetDinhThanhLapHoiDongDanhGiaHetTapSu;
        private string _VaiTroDamNhiem;
        private ChucDanhHoiDongTuyenDung _ChucDanh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private GiayToHoSo _GiayToHoSo;
        //private QuanLyTuyenDung _QuanLyTuyenDung;

        //[Browsable(false)]
        //[Association("QuanLyTuyenDung-ListHoiDongTuyenDung")]
        //public QuanLyTuyenDung QuanLyTuyenDung
        //{
        //    get
        //    {
        //        return _QuanLyTuyenDung;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
        //    }
        //}

        [Browsable(false)]
        [Association("QuyetDinhThanhLapHoiDongHetTapSu-ListHoiDongHetTapSu")]
        public QuyetDinhThanhLapHoiDongDanhGiaHetTapSu QuyetDinhThanhLapHoiDongDanhGiaHetTapSu
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongDanhGiaHetTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongDanhGiaHetTapSu", ref _QuyetDinhThanhLapHoiDongDanhGiaHetTapSu, value);
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
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    ChucVu = value.ChucVu;
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                }
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanhHoiDongTuyenDung ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Vai trò đảm nhiệm")]
        public string VaiTroDamNhiem
        {
            get
            {
                return _VaiTroDamNhiem;
            }
            set
            {
                SetPropertyValue("VaiTroDamNhiem", ref _VaiTroDamNhiem, value);
            }
        }

        [Aggregated]
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

        public HoiDongHetTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //if (!IsDeleted && ThongTinNhanVien != null &&
            //    QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.IsSave &&
            //    QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.Oid != Guid.Empty &&
            //    !String.IsNullOrWhiteSpace(QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.SoQuyetDinh))
            //{
            //    //luu tro giay to ho so
            //    GiayToHoSoHelper.CreateGiayToQuyetDinh(Session, QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.SoQuyetDinh, ThongTinNhanVien,
            //        QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.NgayHieuLuc, QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.LuuTru, QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.NoiDung);
            //}
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, QuyetDinhThanhLapHoiDongDanhGiaHetTapSu.SoQuyetDinh);
            base.OnDeleting();
        }
    }

}
