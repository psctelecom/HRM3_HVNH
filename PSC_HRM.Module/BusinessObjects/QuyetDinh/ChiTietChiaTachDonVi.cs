using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Chi tiết tách đơn vị")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhChiaTachDonVi;ThongTinNhanVien")]
    public class ChiTietChiaTachDonVi : BaseObject, IBoPhan
    {
        private string _GhiChu;
        private QuyetDinhChiaTachDonVi _QuyetDinhChiaTachDonVi;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định chia tách đơn vị")]
        [Association("QuyetDinhChiaTachDonVi-ListChiTietChiaTachDonVi")]
        public QuyetDinhChiaTachDonVi QuyetDinhChiaTachDonVi
        {
            get
            {
                return _QuyetDinhChiaTachDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhChiaTachDonVi", ref _QuyetDinhChiaTachDonVi, value);
                if (!IsLoading && value != null)
                {
                    BoPhan = value.BoPhan;
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [Size(200)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietChiaTachDonVi(Session session) : base(session) { }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

           // QuyetDinhChiaTachDonVi.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
            UpdateNVList();
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
                QuyetDinhChiaTachDonVi.QuyetDinhMoi &&
                Oid != Guid.Empty)
            {
                ThongTinNhanVien.BoPhan = QuyetDinhChiaTachDonVi.BoPhanMoi;
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhChiaTachDonVi.QuyetDinhMoi)
                ThongTinNhanVien.BoPhan = BoPhan;

            base.OnDeleting();
        }
    }

}
