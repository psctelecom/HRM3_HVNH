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
using PSC_HRM.Module.BoiDuong;


namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định tiếp nhận bồi dưỡng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhTiepNhanBoiDuong;ThongTinNhanVien")]
    public class ChiTietTiepNhanBoiDuong : TruongBaseObject
    {
        private QuyetDinhTiepNhanBoiDuong _QuyetDinhTiepNhanBoiDuong;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private ChucVu _ChucVu;
        private TinhTrang _TinhTrang;
        private TinhTrang _TinhTrangMoi;
        private GiayToHoSo _GiayToHoSo;
        
        [Browsable(false)]
        [Association("QuyetDinhTiepNhanBoiDuong-ListChiTietTiepNhanBoiDuong")]
        public QuyetDinhTiepNhanBoiDuong QuyetDinhTiepNhanBoiDuong
        {
            get
            {
                return _QuyetDinhTiepNhanBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhanBoiDuong", ref _QuyetDinhTiepNhanBoiDuong, value);
                if (!IsLoading && value != null)
                {
                    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                    GiayToHoSo.LuuTru = value.LuuTru;
                    GiayToHoSo.TrichYeu = value.NoiDung;
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
        [ModelDefault("Caption", "Chức vụ")]
       // [RuleRequiredField(DefaultContexts.Save)]
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
                    GiayToHoSo.HoSo = value;
                    TinhTrang = value.TinhTrang;
                    ChucVu = value.ChucVu;
                    TinhTrangMoi = HoSoHelper.DangLamViec(Session);
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
        [RuleRequiredField(DefaultContexts.Save)]
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

        public ChiTietTiepNhanBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhTiepNhanBoiDuong != null
                && !IsLoading
                && !QuyetDinhTiepNhanBoiDuong.IsDirty)
                QuyetDinhTiepNhanBoiDuong.IsDirty = true;
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
            if (QuyetDinhTiepNhanBoiDuong.QuyetDinhMoi && QuyetDinhTiepNhanBoiDuong.TuNgay <= HamDungChung.GetServerTime())
            {
                ThongTinNhanVien.TinhTrang = TinhTrangMoi;
                //ThongTinNhanVien.TinhTrang = HoSoHelper.DangLamViec(Session);
                // Xoa dang theo hoc
                BoiDuongHelper.ResetDangTheoHoc(ThongTinNhanVien);
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhTiepNhanBoiDuong.QuyetDinhMoi)
            {
                ThongTinNhanVien.TinhTrang = TinhTrang;
                // dang theo hoc
                BoiDuongHelper.CreateDangTheoHoc(Session, ThongTinNhanVien, QuyetDinhTiepNhanBoiDuong.QuyetDinhBoiDuong.ChuyenNganhDaoTao, QuyetDinhTiepNhanBoiDuong.QuyetDinhBoiDuong.QuocGia);

            }
            
            base.OnDeleting();
        }
    }

}
