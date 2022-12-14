using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    public class ParamHDLD : ReportParametersObjectBase
    {
        public ParamHDLD(Session session) : base(session) { }
        public override CriteriaOperator GetCriteria()
        {
            if (ThongTinNhanVien != null)
                return CriteriaOperator.Parse("Oid = ?", ThongTinNhanVien.Oid);                
            else
            return null;
        }
        public override SortingCollection GetSorting()
        {
            SortingCollection sorting = new SortingCollection();
            return sorting;
        }
        private BoPhan _BoPhan;
        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ Phận")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                _BoPhan = value;
                if (!IsLoading)
                {
                    ThongTinNhanVien = null;
                    UpdateNVList();
                }
            }
        }

        private ThongTinNhanVien _ThongTinNhanVien;
        [DataSourceProperty("NVList")]
        [ModelDefault("Caption", "Họ tên Nhân viên")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get { return _ThongTinNhanVien; }
            set { _ThongTinNhanVien = value; }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(Session, BoPhan);
        }
    }

}
