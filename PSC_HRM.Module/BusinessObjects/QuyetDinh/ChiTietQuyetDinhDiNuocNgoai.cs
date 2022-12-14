using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định đi nước ngoài")]
    [Appearance("Hide_NEU", TargetItems = "HoChieu;ViTriCongTac", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'NEU'")]
    [Appearance("Hide_QNU", TargetItems = "TruPhepNam;NgayPhepConLai;NgayPhepTru;NghiPhepTuNgay;NghiPhepDenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_SoNgayPhepConLai", TargetItems = "NgayPhepConLai", Visibility = ViewItemVisibility.Hide, Criteria = "TruPhepNam != 1")]
    [Appearance("Hide_SoNgayPhepTru", TargetItems = "NgayPhepTru;NghiPhepTuNgay;NghiPhepDenNgay", Visibility = ViewItemVisibility.Hide, Criteria = "NgayPhepConLai <= 0")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhDiNuocNgoai;ThongTinNhanVien")]
    public class ChiTietQuyetDinhDiNuocNgoai : BaseObject
    {
        private TinhTrang _TinhTrang;
        private TinhTrang _TinhTrangMoi;
        private QuyetDinhDiNuocNgoai _QuyetDinhDiNuocNgoai;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;
        private HoChieu _HoChieu;
        private ViTriCongTac _ViTriCongTac;
        private bool _TruPhepNam;
        private decimal _NgayPhepConLai;
        private decimal _NgayPhepTru;
        private DateTime _NghiPhepTuNgay;
        private DateTime _NghiPhepDenNgay;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định đi nước ngoài")]
        [Association("QuyetDinhDiNuocNgoai-ListChiTietQuyetDinhDiNuocNgoai")]
        public QuyetDinhDiNuocNgoai QuyetDinhDiNuocNgoai
        {
            get
            {
                return _QuyetDinhDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiNuocNgoai", ref _QuyetDinhDiNuocNgoai, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Trừ phép năm")]
        public bool TruPhepNam
        {
            get
            {
                return _TruPhepNam;
            }
            set
            {
                SetPropertyValue("TruPhepNam", ref _TruPhepNam, value);
                if (!IsLoading && value == true)
                    NgayPhepConLai = HamDungChung.GetSoNgayPhepNamConLai(Session, QuyetDinhDiNuocNgoai.TuNgay, ThongTinNhanVien);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày phép năm còn lại")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayPhepConLai
        {
            get
            {
                return _NgayPhepConLai;
            }
            set
            {
                SetPropertyValue("NgayPhepConLai", ref _NgayPhepConLai, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày phép năm trừ")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal NgayPhepTru
        {
            get
            {
                return _NgayPhepTru;
            }
            set
            {
                SetPropertyValue("NgayPhepTru", ref _NgayPhepTru, value);
                if (!IsLoading)
                {
                    if (value > 0)
                    {
                        NghiPhepTuNgay = QuyetDinhDiNuocNgoai.TuNgay;
                        NghiPhepDenNgay = NghiPhepTuNgay.AddDays((double)value);
                    }
                    else
                    {
                        NghiPhepTuNgay = DateTime.MinValue;
                        NghiPhepDenNgay = DateTime.MinValue;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Nghỉ phép từ ngày")]
        public DateTime NghiPhepTuNgay
        {
            get
            {
                return _NghiPhepTuNgay;
            }
            set
            {
                SetPropertyValue("NghiPhepTuNgay", ref _NghiPhepTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ phép đến ngày")]
        public DateTime NghiPhepDenNgay
        {
            get
            {
                return _NghiPhepDenNgay;
            }
            set
            {
                SetPropertyValue("NghiPhepDenNgay", ref _NghiPhepDenNgay, value);
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

        [ModelDefault("Caption", "Tình trạng mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        public ChiTietQuyetDinhDiNuocNgoai(Session session) : base(session) { }

        [NonPersistent]
        [Browsable(false)]
        public string MaTruong { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định đi nước ngoài"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);

        //    if (QuyetDinhDiNuocNgoai != null
        //        && !IsLoading
        //        && !QuyetDinhDiNuocNgoai.IsDirty)
        //        QuyetDinhDiNuocNgoai.IsDirty = true;
        //}

        protected override void OnLoaded()
        {
            base.OnLoaded();
            MaTruong = TruongConfig.MaTruong;
            UpdateNVList();
            UpdateGiayToList();
            //if (BoPhan != null)
            //{
            //    BoPhanText = BoPhan.TenBoPhan;
            //} 
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
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

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted &&
                Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                //1. set tinh trang
                if (QuyetDinhDiNuocNgoai.DiNuocNgoaiTren30Ngay)
                {
                    if (QuyetDinhDiNuocNgoai.TuNgay <= HamDungChung.GetServerTime())
                    {
                       // ThongTinNhanVien.TinhTrang = HoSoHelper.DiNuocNgoaiTren30Ngay(Session);
                        ThongTinNhanVien.TinhTrang = TinhTrangMoi;
                    }
                }

                //2. Tao qua trinh di nuoc ngoai
                QuaTrinhHelper.CreateQuaTrinhDiNuocNgoai(Session, QuyetDinhDiNuocNgoai, ThongTinNhanVien, QuyetDinhDiNuocNgoai.QuocGia, QuyetDinhDiNuocNgoai.TuNgay, QuyetDinhDiNuocNgoai.DenNgay);

                if (MaTruong.Equals("NEU") && QuyetDinhDiNuocNgoai != null && QuyetDinhDiNuocNgoai.TuNgay != DateTime.MinValue && QuyetDinhDiNuocNgoai.DenNgay != DateTime.MinValue)
                    QuyetDinhDiNuocNgoai.GhiChuTG = HamDungChung.GetThoiGianViecRieng(Session, QuyetDinhDiNuocNgoai.TuNgay, QuyetDinhDiNuocNgoai.DenNgay, ThongTinNhanVien, NgayPhepTru);
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                //1. reset tinh trang
                if (QuyetDinhDiNuocNgoai.DiNuocNgoaiTren30Ngay)
                {
                    ThongTinNhanVien.TinhTrang = TinhTrang;
                }               

                //2. Xóa quá trình di nuoc ngoai
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhDiNuocNgoai>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhDiNuocNgoai.Oid, ThongTinNhanVien.Oid));
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
