using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DoanDang
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách tuổi Đảng")]
    public class DanhSachTuoiDang : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách Đảng viên")]
        public XPCollection<TuoiDang> TuoiDang { get; set; }

        public DanhSachTuoiDang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoadData();
        }

        private void LoadData()
        {
            if (TuoiDang == null)
                TuoiDang = new XPCollection<DoanDang.TuoiDang>(Session, false);
            using (XPCollection<DangVien> dvList = new XPCollection<DangVien>(Session))
            {
                TuoiDang.Reload();
                dvList.Sorting = new SortingCollection(new SortProperty("NgayDuBi", DevExpress.Xpo.DB.SortingDirection.Ascending));
                TuoiDang tuoiDang;
                foreach (DangVien item in dvList)
                {
                    tuoiDang = new TuoiDang(Session);
                    tuoiDang.ChiBo = item.ChiBoDang;
                    tuoiDang.DangVien = item;
                    tuoiDang.NgayVaoDang = item.NgayDuBi;
                    tuoiDang.NgayVaoDangChinhThuc = item.NgayVaoDangChinhThuc;
                    tuoiDang.NgayQuyetDinhChinhThuc = item.NgayQuyetDinhChinhThuc;
                    
                    TuoiDang.Add(tuoiDang);
                }
            }
        }
    }

}
