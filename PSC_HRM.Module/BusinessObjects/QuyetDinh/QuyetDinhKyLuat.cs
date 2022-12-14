using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinhService;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định kỷ luật")]    
    [Appearance("Hide_NEU",TargetItems = "TruLuongTangThem", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    
    public class QuyetDinhKyLuat : QuyetDinhCaNhan
    {
        private bool _QuyetDinhMoi;
        private HinhThucKyLuat _HinhThucKyLuat;
        private string _LyDo;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private bool _TruLuongTangThem;

        [ModelDefault("Caption", "Hình thức kỷ luật")]
        public HinhThucKyLuat HinhThucKyLuat
        {
            get
            {
                return _HinhThucKyLuat;
            }
            set
            {
                SetPropertyValue("HinhThucKyLuat", ref _HinhThucKyLuat, value);
            }
        }

        [Size(500)]
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

        [ModelDefault("Caption", "Từ ngày")]
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

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Quyết định mới")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        [ModelDefault("Caption", "Trừ lương tăng thêm")]
        [Appearance("TruLuongTangThem_UTE", TargetItems = "TruLuongTangThem", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'IUH'")]
        public bool TruLuongTangThem
        {
            get
            {
                return _TruLuongTangThem;
            }
            set
            {
                SetPropertyValue("TruLuongTangThem", ref _TruLuongTangThem, value);
            }
        }

        public QuyetDinhKyLuat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhKyLuat;

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định kỷ luật"));
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
                    if (GiayToList.Count > 0)
                        GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
                }
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            SystemContainer.Resolver<IQuyetDinhKyLuatService>("QDKyLuat" + TruongConfig.MaTruong).Save(Session, this);
        }

        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhKyLuatService>("QDKyLuat" + TruongConfig.MaTruong).Delete(Session, this);

            base.OnDeleting();
        }
    }
}
