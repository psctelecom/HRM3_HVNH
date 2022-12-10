using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định điều chỉnh thời gian đi đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhDieuChinhThoiGianDiDaoTao;ThongTinNhanVien")]
    public class ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao : BaseObject
    {
        private TinhTrang _TinhTrang;
        private TinhTrang _TinhTrangMoi;
        private QuyetDinhDieuChinhThoiGianDiDaoTao _QuyetDinhDieuChinhThoiGianDiDaoTao;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;
        private HoChieu _HoChieu;
        private ViTriCongTac _ViTriCongTac;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định đi đào tạo")]
        [Association("QuyetDinhDieuChinhThoiGianDiDaoTao-ListChiTietQuyetDinhDieuChinhThoiGianDiDaoTao")]
        public QuyetDinhDieuChinhThoiGianDiDaoTao QuyetDinhDieuChinhThoiGianDiDaoTao
        {
            get
            {
                return _QuyetDinhDieuChinhThoiGianDiDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDieuChinhThoiGianDiDaoTao", ref _QuyetDinhDieuChinhThoiGianDiDaoTao, value);
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
                    TinhTrang = value.TinhTrang;
                }
            }
        }

        [ModelDefault("Caption", "Hộ chiếu")]
        public HoChieu HoChieu
        {
            get
            {
                return _HoChieu;
            }
            set
            {
                SetPropertyValue("HoChieu", ref _HoChieu, value);
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

        [ModelDefault("Caption", "Tình trạng mới")]
        public TinhTrang TinhTrangMoi
        {
            get
            {
                return _TinhTrangMoi;
            }
            set
            {
                SetPropertyValue("TinhTrangMoi", ref _TinhTrangMoi, value);
            }
        }

        [Browsable(false)]
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

        public ChiTietQuyetDinhDieuChinhThoiGianDiDaoTao(Session session) : base(session) { }

        [NonPersistent]
        [Browsable(false)]
        public string MaTruong { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhDieuChinhThoiGianDiDaoTao != null
                && !IsLoading
                && !QuyetDinhDieuChinhThoiGianDiDaoTao.IsDirty)
                QuyetDinhDieuChinhThoiGianDiDaoTao.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            MaTruong = TruongConfig.MaTruong;
            UpdateNVList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            //if (!IsDeleted &&
            //    Oid != Guid.Empty)
            //{
            //    //1. set tinh trang
            //    if (QuyetDinhDieuChinhThoiGianDiNuocNgoai.DiNuocNgoaiTren30Ngay)
            //    {
            //        if (QuyetDinhDieuChinhThoiGianDiNuocNgoai.TuNgay <= HamDungChung.GetServerTime())
            //        {
            //            ThongTinNhanVien.TinhTrang = HoSoHelper.DiNuocNgoaiTren30Ngay(Session);
            //        }
            //    }

            //    //2. Tao qua trinh di nuoc ngoai
            //    QuaTrinhHelper.CreateQuaTrinhDiNuocNgoai(Session, QuyetDinhDieuChinhThoiGianDiNuocNgoai, ThongTinNhanVien, QuyetDinhDieuChinhThoiGianDiNuocNgoai.QuocGia, QuyetDinhDieuChinhThoiGianDiNuocNgoai.TuNgay, QuyetDinhDieuChinhThoiGianDiNuocNgoai.DenNgay);
            //}
        }

        protected override void OnDeleting()
        {
            //if (ThongTinNhanVien != null)
            //{
            //    //1. reset tinh trang
            //    if (QuyetDinhDieuChinhThoiGianDiNuocNgoai.DiNuocNgoaiTren30Ngay)
            //    {
            //        ThongTinNhanVien.TinhTrang = TinhTrang;
            //    }

            //    //2. Xóa quá trình di nuoc ngoai
            //    QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhDiNuocNgoai>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhDieuChinhThoiGianDiNuocNgoai.Oid, ThongTinNhanVien.Oid));
            //}

            ////xóa giấy tờ hồ sơ
            //if (GiayToHoSo != null)
            //{
            //    Session.Delete(GiayToHoSo);
            //    Session.Save(GiayToHoSo);
            //}

            base.OnDeleting();
        }
    }

}
