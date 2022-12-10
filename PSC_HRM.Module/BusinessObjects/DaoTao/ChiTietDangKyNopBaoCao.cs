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

namespace PSC_HRM.Module.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [ModelDefault("Caption", "Chi tiết đăng ký nộp báo cáo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "DangKyNopBaoCao;ThongTinNhanVien")]
    public class ChiTietDangKyNopBaoCao : BaseObject, IBoPhan
    {
         // Fields...
        DangKyNopBaoCao _DangKyNopBaoCao;
        private string _TenBaoCao;
        private DateTime _NgayNopBaoCao;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _GhiChu;

        [Browsable(false)]
        [Association("DangKyNopBapCao-ListChiTietDangKyNopBaoCao")]
        public DangKyNopBaoCao DangKyNopBaoCao
        {
            get
            {
                return _DangKyNopBaoCao;
            }
            set
            {
                SetPropertyValue("DangKyNopBaoCao", ref _DangKyNopBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Tên báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenBaoCao
        {
            get
            {
                return _TenBaoCao;
            }
            set
            {
                SetPropertyValue("TenBaoCao", ref _TenBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Ngày nộp báo cáo")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgayNopBaoCao
        {
            get
            {
                return _NgayNopBaoCao;
            }
            set
            {
                SetPropertyValue("NgayNopBaoCao", ref _NgayNopBaoCao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
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

        public ChiTietDangKyNopBaoCao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Lấy dữ liệu mặc định khi thêm mới
            NgayNopBaoCao = HamDungChung.GetServerTime();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
