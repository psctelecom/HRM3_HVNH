using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.QuaTrinh;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định tham dự hội thi tay nghề trẻ")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhThamDuHoiThiTayNgheTre;ThongTinNhanVien")]
    public class ChiTietQuyetDinhThamDuHoiThiTayNgheTre : BaseObject
    {
        private QuyetDinhThamDuHoiThiTayNgheTre _QuyetDinhThamDuHoiThiTayNgheTre;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;
        private ViTriCongTac _ViTriCongTac;
        private ChuyenMonDaoTao _NgheThamGia;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định tham dự hội thi tay nghề trẻ")]
        [Association("QuyetDinhThamDuHoiThiTayNgheTre-ListChiTietQuyetDinhThamDuHoiThiTayNgheTre")]
        public QuyetDinhThamDuHoiThiTayNgheTre QuyetDinhThamDuHoiThiTayNgheTre
        {
            get
            {
                return _QuyetDinhThamDuHoiThiTayNgheTre;
            }
            set
            {
                SetPropertyValue("QuyetDinhThamDuHoiThiTayNgheTre", ref _QuyetDinhThamDuHoiThiTayNgheTre, value);
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
                }
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

        [ModelDefault("Caption", "Nghề tham gia")]
        public ChuyenMonDaoTao NgheThamGia
        {
            get
            {
                return _NgheThamGia;
            }
            set
            {
                SetPropertyValue("NgheThamGia", ref _NgheThamGia, value);
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

        public ChiTietQuyetDinhThamDuHoiThiTayNgheTre(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhThamDuHoiThiTayNgheTre != null
                && !IsLoading
                && !QuyetDinhThamDuHoiThiTayNgheTre.IsDirty)
                QuyetDinhThamDuHoiThiTayNgheTre.IsDirty = true;
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

            if (!IsDeleted &&
                Oid != Guid.Empty)
            {
                //4. qua trinh di cong tac
                QuaTrinhHelper.CreateQuaTrinhCongTac(Session, ThongTinNhanVien, this.QuyetDinhThamDuHoiThiTayNgheTre);
            }
        }

        protected override void OnDeleting()
        {
            //4. delete quá trình đi cong tac
            CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    ThongTinNhanVien, this.QuyetDinhThamDuHoiThiTayNgheTre);
            QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhCongTac>(Session, filter);

            base.OnDeleting();
        }
    }

}
