using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định đền bù chi phí đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhDenBuChiPhiDaoTao;ThongTinNhanVien")]
    public class ChiTietDenBuChiPhiDaoTao : TruongBaseObject
    {
        private decimal _HSPCChuyenMon;
        private TinhTrang _TinhTrang;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhDenBuChiPhiDaoTao _QuyetDinhDenBuChiPhiDaoTao;
        private GiayToHoSo _GiayToHoSo;
        private decimal _HSPCChuyenMonMoi;
        private DateTime _NgayHuongHSPCChuyenMonMoi;

        [Browsable(false)]
        [Association("QuyetDinhDenBuChiPhiDaoTao-ListChiTietDenBuChiPhiDaoTao")]
        public QuyetDinhDenBuChiPhiDaoTao QuyetDinhDenBuChiPhiDaoTao
        {
            get
            {
                return _QuyetDinhDenBuChiPhiDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDenBuChiPhiDaoTao", ref _QuyetDinhDenBuChiPhiDaoTao, value);
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
                    HSPCChuyenMon = value.NhanVienThongTinLuong.HSPCChuyenMon;
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

        [ModelDefault("Caption", "HSPC chuyên môn cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChuyenMon
        {
            get
            {
                return _HSPCChuyenMon;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMon", ref _HSPCChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "HSPC chuyên môn mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChuyenMonMoi
        {
            get
            {
                return _HSPCChuyenMonMoi;
            }
            set
            {
                SetPropertyValue("HSPCChuyenMonMoi", ref _HSPCChuyenMonMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPC chuyên môn mới")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayHuongHSPCChuyenMonMoi
        {
            get
            {
                return _NgayHuongHSPCChuyenMonMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongHSPCChuyenMonMoi", ref _NgayHuongHSPCChuyenMonMoi, value);
            }
        }

        public ChiTietDenBuChiPhiDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định đền bù chi phí đào tạo"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhDenBuChiPhiDaoTao != null
                && !IsLoading
                && !QuyetDinhDenBuChiPhiDaoTao.IsDirty)
                QuyetDinhDenBuChiPhiDaoTao.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            if(BoPhan!=null)
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

            //SystemContainer.Resolver<IQuyetDinhCongNhanDaoTaoService>("QDCongNhanDaoTao" + TruongConfig.MaTruong).Save(Session, this);

            //if (QuyetDinhCongNhanDaoTao.QuyetDinhMoi && QuyetDinhCongNhanDaoTao.TuNgay <= HamDungChung.GetServerTime())
            //{
            //    ThongTinNhanVien.TinhTrang = HoSoHelper.DangLamViec(Session);
            //    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai = 40;
            //    // Xoa dang theo hoc
            //    DaoTaoHelper.ResetDangTheoHoc(ThongTinNhanVien);
            //}
        }

        protected override void OnDeleting()
        {
            //SystemContainer.Resolver<IQuyetDinhCongNhanDaoTaoService>("QDCongNhanDaoTao" + TruongConfig.MaTruong).Delete(Session, this);

            //if (QuyetDinhCongNhanDaoTao.QuyetDinhMoi)
            //{
            //    ThongTinNhanVien.TinhTrang = TinhTrang;
            //    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai = 0;
            //    // dang theo hoc
            //    DaoTaoHelper.CreateDangTheoHoc(Session, ThongTinNhanVien, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao.TrinhDoChuyenMon, QuyetDinhCongNhanDaoTao.QuyetDinhDaoTao.QuocGia);
            //}

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
