using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinhService;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ModelDefault("Caption", "Chi tiết QĐ tiếp nhận viên chức đi nước ngoài")]
    public class ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai : TruongBaseObject, IBoPhan
    {
        private TinhTrang _TinhTrang;
        private QuyetDinhTiepNhanVienChucDiNuocNgoai _QuyetDinhTiepNhanVienChucDiNuocNgoai;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;
        private DateTime _MocNangLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định tiếp nhận viên chức đi nước ngoài")]
        [Association("QuyetDinhTiepNhanVienChucDiNuocNgoai-ListChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai")]
        public QuyetDinhTiepNhanVienChucDiNuocNgoai QuyetDinhTiepNhanVienChucDiNuocNgoai
        {
            get
            {
                return _QuyetDinhTiepNhanVienChucDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhTiepNhanVienChucDiNuocNgoai", ref _QuyetDinhTiepNhanVienChucDiNuocNgoai, value);
                //if (!IsLoading && value != null && GiayToHoSo != null)
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
                    ThongTinNhanVien = null;
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
        [DataSourceProperty("NVList")]
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
                if (!IsLoading && value != null && GiayToHoSo != null)
                {
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    TinhTrang = value.TinhTrang;
                    MocNangLuong = value.NhanVienThongTinLuong.MocNangLuong;
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
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

        public ChiTietQuyetDinhTiepNhanVienChucDiNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định tiếp nhận đi nước ngoài"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhTiepNhanVienChucDiNuocNgoai != null
                && !IsLoading
                && !QuyetDinhTiepNhanVienChucDiNuocNgoai.IsDirty)
                QuyetDinhTiepNhanVienChucDiNuocNgoai.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
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

            SystemContainer.Resolver<IQuyetDinhTiepNhanVienChucDiNuocNgoaiService>("QDTiepNhanVienChucDiNuocNgoai" + TruongConfig.MaTruong).Save(Session, this);
        }

        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhTiepNhanVienChucDiNuocNgoaiService>("QDTiepNhanVienChucDiNuocNgoai" + TruongConfig.MaTruong).Delete(Session, this);

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
