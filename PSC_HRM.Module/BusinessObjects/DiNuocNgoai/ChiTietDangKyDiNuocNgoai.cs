using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.DiNuocNgoai
{
    [ImageName("BO_QuanLyDiNuocNgoai")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Đăng ký đi nước ngoài")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DangKyDiNuocNgoai;ThongTinNhanVien")]
    public class ChiTietDangKyDiNuocNgoai : BaseObject, IBoPhan
    {
        // Fields...
        private string _GhiChu;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private DangKyDiNuocNgoai _DangKyDiNuocNgoai;
        private ViTriCongTac _ViTriCongTac;

        [Browsable(false)]
        [ModelDefault("Caption", "Đăng ký đi nước ngoài")]
        [Association("DangKyDiNuocNgoai-ListChiTietDangKyDiNuocNgoai")]
        public DangKyDiNuocNgoai DangKyDiNuocNgoai
        {
            get
            {
                return _DangKyDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("DangKyDiNuocNgoai", ref _DangKyDiNuocNgoai, value);
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
                }
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

        [Size(300)]
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

        public ChiTietDangKyDiNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNVList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

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
    }

}
