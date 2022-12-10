using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DoanDang
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách tuổi Đoàn")]
    public class DanhSachTuoiDoan : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách Đoàn viên")]
        public XPCollection<TuoiDoan> TuoiDoan { get; set; }

        public DanhSachTuoiDoan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoadData();
        }

        public void LoadData()
        {
            if (TuoiDoan == null)
                TuoiDoan = new XPCollection<TuoiDoan>(Session, false);
            using (XPCollection<DoanVien> dvList = new XPCollection<DoanVien>(Session))
            {
                TuoiDoan.Reload();

                dvList.Criteria = CriteriaOperator.Parse("TruongThanhDoan is null or TruongThanhDoan=0");
                dvList.Sorting = new SortingCollection(new SortProperty("NgayKetNap", DevExpress.Xpo.DB.SortingDirection.Ascending));
                TuoiDoan tuoiDoan;
                foreach (DoanVien item in dvList)
                {
                    tuoiDoan = new TuoiDoan(Session);
                    tuoiDoan.ThongTinNhanVien = item.ThongTinNhanVien;
                    tuoiDoan.NgayKetNap = item.NgayKetNap;
                    tuoiDoan.BoPhan = item.BoPhan;
                    TuoiDoan.Add(tuoiDoan);
                }
            }
        }
    }

}
