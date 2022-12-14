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

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định hưởng chế độ đi học trong nước")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "ThongTinNhanVien;QuyetDinhHuongCheDoDiHocTrongNuoc")]
    public class ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc : BaseObject
    {
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        //private TinhTrang _TinhTrang;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhHuongCheDoDiHocTrongNuoc _QuyetDinhHuongCheDoDiHocTrongNuoc;
        //private bool _DuocHuongLuongKhiDiHoc;
        private GiayToHoSo _GiayToHoSo; 
        //private TinhTrang _TinhTrangMoi;

        [Browsable(false)]
        [Association("QuyetDinhHuongCheDoDiHocTrongNuoc-ListChiTietQuyetDinhHuongCheDoDiHocTrongNuoc")]
        public QuyetDinhHuongCheDoDiHocTrongNuoc QuyetDinhHuongCheDoDiHocTrongNuoc
        {
            get
            {
                return _QuyetDinhHuongCheDoDiHocTrongNuoc;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongCheDoDiHocTrongNuoc", ref _QuyetDinhHuongCheDoDiHocTrongNuoc, value);
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
                    //TinhTrang = value.TinhTrang;

                    //if (value.LoaiNhanVien != null
                    //    && value.LoaiNhanVien.TenLoaiNhanVien.ToLower().Contains("tập sự"))
                    //    DuocHuongLuongKhiDiHoc = false;
                }
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

        //[ModelDefault("Caption", "Được hưởng lương khi đi học")]
        //[Browsable(false)]
        //public bool DuocHuongLuongKhiDiHoc
        //{
        //    get
        //    {
        //        return _DuocHuongLuongKhiDiHoc;
        //    }
        //    set
        //    {
        //        SetPropertyValue("DuocHuongLuongKhiDiHoc", ref _DuocHuongLuongKhiDiHoc, value);
        //    }
        //}

        //[ModelDefault("Caption", "Tình trạng hưởng lương")] 
        //public TinhTrang TinhTrangMoi
        //{
        //    get
        //    {
        //        return _TinhTrangMoi;
        //    }
        //    set
        //    {
        //        SetPropertyValue("TinhTrangMoi", ref _TinhTrangMoi, value);
        //    }
        //}

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

        //[Browsable(false)]
        //public TinhTrang TinhTrang
        //{
        //    get
        //    {
        //        return _TinhTrang;
        //    }
        //    set
        //    {
        //        SetPropertyValue("TinhTrang", ref _TinhTrang, value);
        //    }
        //}

        [NonPersistent]
        private string MaTruong { get; set; }

        public ChiTietQuyetDinhHuongCheDoDiHocTrongNuoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //DuocHuongLuongKhiDiHoc = true;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            MaTruong = TruongConfig.MaTruong;
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
            MaTruong = TruongConfig.MaTruong;
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

        }

        protected override void OnDeleting()
        {
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }
}
