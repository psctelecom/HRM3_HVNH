using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using System.Collections.Generic;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinhService;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DiNuocNgoai;


namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định cho phép nghỉ học")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhChoPhepNghiHoc;ThongTinNhanVien")]
    public class ChiTietChoPhepNghiHoc : TruongBaseObject
    {
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TinhTrang _TinhTrang;
        private TinhTrang _TinhTrangMoi;
        private QuyetDinhChoPhepNghiHoc _QuyetDinhChoPhepNghiHoc;
        private GiayToHoSo _GiayToHoSo;
        
        [Browsable(false)]
        [Association("QuyetDinhChoPhepNghiHoc-ListChiTietChoPhepNghiHoc")]
        public QuyetDinhChoPhepNghiHoc QuyetDinhChoPhepNghiHoc
        {
            get
            {
                return _QuyetDinhChoPhepNghiHoc;
            }
            set
            {
                SetPropertyValue("QuyetDinhChoPhepNghiHoc", ref _QuyetDinhChoPhepNghiHoc, value);
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
                    TinhTrang = value.TinhTrang;
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

        [ModelDefault("Caption", "Tình trạng mới")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "MaTruong != 'BUH'")]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

        public ChiTietChoPhepNghiHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định cho phép nghỉ học"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            TinhTrangMoi = HoSoHelper.DangLamViec(Session);
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
            if (QuyetDinhChoPhepNghiHoc.QuyetDinhMoi)
            {
                if (QuyetDinhChoPhepNghiHoc.TuNgay <= HamDungChung.GetServerTime())
                {
                    ThongTinNhanVien.TinhTrang = TinhTrangMoi;
                    //1. đang theo học
                    DaoTaoHelper.ResetDangTheoHoc(ThongTinNhanVien);
                }
                //2. delete quá trình đi nước ngoài
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinh=?",
                        ThongTinNhanVien, QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao);
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhDiNuocNgoai>(Session, filter);
            }
            
            base.OnSaving();            
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhChoPhepNghiHoc.QuyetDinhMoi)
            {
                ThongTinNhanVien.TinhTrang = TinhTrang;

                //1. đang theo học
                DaoTaoHelper.CreateDangTheoHoc(Session, ThongTinNhanVien, QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao.TrinhDoChuyenMon, QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao.QuocGia);

                //2. qua trinh di nuoc ngoai
                if (DiNuocNgoaiHelper.IsNgoaiNuoc(QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao.QuocGia))
                    QuaTrinhHelper.CreateQuaTrinhDiNuocNgoai(Session, QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao, ThongTinNhanVien, QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao.QuocGia, QuyetDinhChoPhepNghiHoc.QuyetDinhDaoTao.NgayQuyetDinh, DateTime.MinValue);
            }
            base.OnDeleting();

        }
    }

}
