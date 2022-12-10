using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.Win.QuyTrinh.TuyenDung
{
    public partial class DuyetDangKyTuyenDungController : BaseController
    {
        private IObjectSpace _ObjectSpace;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private List<DangKyTuyenDungItem> _DataSource;

        public DuyetDangKyTuyenDungController(IObjectSpace obs, QuanLyTuyenDung quanLyTuyenDung)
        {
            InitializeComponent();

            _ObjectSpace = obs;
            _QuanLyTuyenDung = obs.GetObjectByKey<QuanLyTuyenDung>(quanLyTuyenDung.Oid);
        }

        private void DuyetDangKyTuyenDungController_Load(object sender, EventArgs e)
        {
            gridViewObjects.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewObjects.ShowField(new string[] { "Chon", "BoPhan.TenBoPhan", "BoMon.TenBoPhan", "ViTriTuyenDung.Caption", "SoLuongTuyen", "SoLuongPheDuyet" },
                new string[] { "Chọn", "Đơn vị", "Bộ môn", "Vị trí tuyển dụng", "Số lượng đăng ký", "Số lượng phê duyệt" },
                new int[] { 60, 150, 150, 150, 90, 90 });
            gridViewObjects.ReadOnlyColumns(new string[] { "BoPhan.TenBoPhan", "BoMon.TenBoPhan", "ViTriTuyenDung.Caption", "SoLuongTuyen" });

            LoadData();
        }

        private void LoadData()
        {
            _DataSource = (from d in _QuanLyTuyenDung.ListDangKyTuyenDung
                           select new DangKyTuyenDungItem
                           {
                               BoPhan = d.BoPhan,
                               BoMon = d.BoMon,
                               ViTriTuyenDung = d.ViTriTuyenDung,
                               SoLuongTuyen = d.SoLuongTuyen,
                               SoLuongPheDuyet = d.SoLuongTuyen
                           }).ToList<DangKyTuyenDungItem>();

            gridObjects.DataSource = _DataSource;
        }

        public void XuLy()
        {
            var data = from d in _DataSource
                       where d.Chon == true
                       select d;

            NhuCauTuyenDung duyetDangKy;
            foreach (DangKyTuyenDungItem item in data)
            {
                CriteriaOperator filter;
                if (item.BoMon == null)
                    filter = CriteriaOperator.Parse("QuanLyTuyenDung=? and BoPhan=?", _QuanLyTuyenDung.Oid, item.BoPhan.Oid);
                else
                    filter = CriteriaOperator.Parse("QuanLyTuyenDung=? and BoPhan=? and BoMon=?", _QuanLyTuyenDung.Oid, item.BoPhan.Oid, item.BoMon.Oid);

                duyetDangKy = _ObjectSpace.FindObject<NhuCauTuyenDung>(filter);
                if (duyetDangKy == null)
                {
                    duyetDangKy = _ObjectSpace.CreateObject<NhuCauTuyenDung>();
                    duyetDangKy.QuanLyTuyenDung = _QuanLyTuyenDung;
                    duyetDangKy.BoPhan = _ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    if (item.BoMon != null)
                        duyetDangKy.BoMon = _ObjectSpace.GetObjectByKey<BoPhan>(item.BoMon.Oid);
                    duyetDangKy.ViTriTuyenDung = _ObjectSpace.GetObjectByKey<ViTriTuyenDung>(item.ViTriTuyenDung.Oid);
                    duyetDangKy.SoLuongTuyen = item.SoLuongPheDuyet;
                }
            }
            _ObjectSpace.CommitChanges();
        }
    }

    public class DangKyTuyenDungItem
    {
        [DisplayName("Chọn")]
        public bool Chon { get; set; }
        [DisplayName("Đơn vị")]
        public BoPhan BoPhan { get; set; }
        [DisplayName("Bộ môn")]
        public BoPhan BoMon { get; set; }
        [DisplayName("Vị trí tuyển dụng")]
        public ViTriTuyenDung ViTriTuyenDung { get; set; }
        [DisplayName("Số lượng đăng ký")]
        public int SoLuongTuyen { get; set; }
        [DisplayName("Số lượng phê duyệt")]
        public int SoLuongPheDuyet { get; set; }
    }
}
