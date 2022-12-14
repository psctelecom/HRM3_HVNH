using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuyetDinh;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThoiViec
{
    [ImageName("BO_NghiHuu")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết thôi việc")]
    public class ChiTietThoiViec : BaseObject
    {
        // Fields...
        private string _LyDo;
        private QuyetDinhCaNhan _QuyetDinhThoiViec;
        private DateTime _NghiViecTuNgay;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyThoiViec _QuanLyThoiViec;

        [Browsable(false)]
        [Association("QuanLyThoiViec-ListChiTietThoiViec")]
        [ModelDefault("Caption", "Quản lý thôi việc")]
        public QuanLyThoiViec QuanLyThoiViec
        {
            get
            {
                return _QuanLyThoiViec;
            }
            set
            {
                SetPropertyValue("QuanLyThoiViec", ref _QuanLyThoiViec, value);
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhCaNhan QuyetDinhThoiViec
        {
            get
            {
                return _QuyetDinhThoiViec;
            }
            set
            {
                SetPropertyValue("QuyetDinhThoiViec", ref _QuyetDinhThoiViec, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Nghỉ việc từ ngày")]
        public DateTime NghiViecTuNgay
        {
            get
            {
                return _NghiViecTuNgay;
            }
            set
            {
                SetPropertyValue("NghiViecTuNgay", ref _NghiViecTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public ChiTietThoiViec(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
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
