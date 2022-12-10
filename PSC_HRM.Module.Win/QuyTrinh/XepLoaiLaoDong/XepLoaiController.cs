using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.Win.QuyTrinh.XepLoaiLaoDong
{
    public partial class XepLoaiController<T> : TheoDoiBaseController where T : BaseObject
    {
        public XepLoaiController(XafApplication app, IObjectSpace obs, List<T> dataSource, string[] properties, string[] captions, int[] widths)
            : base(app, obs)
        {
            InitializeComponent();

            gridDanhSachNghiHuu.DataSource = dataSource;
            gridViewDanhSachNghiHuu.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewDanhSachNghiHuu.ReadOnlyColumns(new string[] { "ThongTinNhanVien.HoTen", "BoPhan.TenBoPhan" });
            gridViewDanhSachNghiHuu.ShowField(properties, captions, widths);
        }
    }
}
