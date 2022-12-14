using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.SinhNhat
{
    [ImageName("BO_NghiHuu")]
    [ModelDefault("Caption", "Chi tiết tặng quà sinh nhật")]
    public class ChiTietTangQuaSinhNhat : BaseObject
    {
        // Fields...
        private decimal _SoTien;
        private DateTime _NgaySinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyTangQuaSinhNhat _QuanLyTangQuaSinhNhat;

        [Browsable(false)]
        [Association("QuanLyTangQuaSinhNhat-ListChiTietTangQuaSinhNhat")]
        [ModelDefault("Caption", "Quản lý tặng quà sinh nhật")]
        public QuanLyTangQuaSinhNhat QuanLyTangQuaSinhNhat
        {
            get
            {
                return _QuanLyTangQuaSinhNhat;
            }
            set
            {
                SetPropertyValue("QuanLyTangQuaSinhNhat", ref _QuanLyTangQuaSinhNhat, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đơn vị")]
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
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Cán bộ")]
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
                    NgaySinh = value.NgaySinh;
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        public ChiTietTangQuaSinhNhat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (HamDungChung.CauHinhChung != null
                && HamDungChung.CauHinhChung.CauHinhHoSo != null)
                SoTien = HamDungChung.CauHinhChung.CauHinhHoSo.QuaSinhNhat;
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
