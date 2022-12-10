using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.TreeListEditors.Win;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class FilterNamHocController : ViewController
    {
        private IObjectSpace _obs;
        private XPCollection<NamHoc> _NamHoc;
        private Session _ses;
        public FilterNamHocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HeThong_FilterNamHocContrller_ViewControlsCreated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            if (listView != null && 
                ((listView.CollectionSource.DisplayableProperties.Contains("NamHoc")
                || listView.CollectionSource.DisplayableProperties.Contains("KyTinhLuong")
                || listView.CollectionSource.DisplayableProperties.Contains("SoQuyetDinh")
                || listView.Id.Contains("KyTinhLuong")
                || listView.Id.Contains("SoQuyetDinh"))
                //&& listView.Id != "NhanVien_ListTaiKhoanNganHang_ListView"
                //&& listView.Id != "NhanVien_ListHopDong_ListView"
                //&& listView.Id != "ThongTinNhanVien_ListQuanHeGiaDinh_ListView"
                //&& listView.Id != "ThongTinNhanVien_ListGiamTruGiaCanh_ListView"
                //&& listView.Id != "HoSo_ListGiayToHoSo_ListView"
                //&& listView.Id != "ThongTinNhanVien_ListChucVuKiemNhiem_ListView"
                //&& listView.Id != "ThongTinNhanVien_RutGon_ListView"
                //&& listView.Id != "GiangVienThinhGiang_RutGon_ListView"
                //&& listView.Id != "GiangVienThinhGiang_ListDanhGiaGiangVienThinhGiang_ListView"
                //&& listView.Id != "LichSuBanThan_ListView"
                //&& listView.Id != "DienBienLuong_ListView"
                //&& listView.Id != "ThamGiaLucLuongVuTrang_ListView"
                //&& listView.Id != "KhoiLuongGiangDay_ListKhoaLuanTotNghiep_ListView"
                //&& !(listView.Id.Contains("QuaTrinh") && listView.Id.Contains("_ListView"))
                //&& !(listView.Id.Contains("_ListHeSo") && listView.Id.Contains("_ListView"))
                //&& !(listView.Id.Contains("_ListDinhMuc") && listView.Id.Contains("_ListView"))
                //&& !(listView.Id.Contains("_ListThongTin") && listView.Id.Contains("_ListView"))
                //&& !(listView.Id.Contains("ListChiTiet") && listView.Id.Contains("_ListView"))
                //&& listView.Id != "HoSo_ListVanBang_ListView"
                && !listView.Id.Contains("_LookupListView")))
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                //CriteriaOperator filter = CriteriaOperator.Parse("ISNULL(KeKhai,0) = 0");
                _NamHoc = new XPCollection<NamHoc>(_ses);

                SortingCollection sorting = new SortingCollection();
                sorting.Add(new SortProperty("NgayBatDau", DevExpress.Xpo.DB.SortingDirection.Ascending));
                _NamHoc.Sorting = sorting;

                singleChoiceAction1.Items.Clear();
                ChoiceActionItem subItem;
                subItem = new ChoiceActionItem();
                subItem.Id = "00000000-0000-0000-0000-000000000000";
                subItem.Caption = "Tất cả năm học";
                singleChoiceAction1.Items.Add(subItem);
                foreach (NamHoc item in _NamHoc)
                {
                    subItem = new ChoiceActionItem();
                    subItem.Id = item.Oid.ToString();
                    subItem.Caption = "Năm học: " + item.TenNamHoc;
                    singleChoiceAction1.Items.Add(subItem);
                    if (item == HamDungChung.GetCurrentNamHoc(_ses))
                    {
                        singleChoiceAction1.SelectedItem = subItem;
                        Execute(_ses.GetObjectByKey<NamHoc>(Guid.Parse(subItem.Id)));
                    }
                }
            }

        }
        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Execute(_ses.GetObjectByKey<NamHoc>(Guid.Parse(e.SelectedChoiceActionItem.Id)));
        }

        public void Execute(NamHoc namHoc)
        {
            ListView listView = View as ListView;
            GridView gridView = null;
            TreeList treeList = null;
            bool _KyTinhLuong = false;
            bool _QuyetDinh = false;
            bool _NamHoc = false;

            if (View is ListView)
            {
                if (listView != null && listView.Editor is GridListEditor && listView.IsRoot)
                {
                    gridView = (((DevExpress.ExpressApp.ListView)View).Editor as GridListEditor).GridView;
                    foreach (GridColumn item in gridView.Columns)
                    {
                        if (item.FieldName == "NamHoc!")
                        {
                            _NamHoc = true;
                            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc.Oid = ? OR '00000000-0000-0000-0000-000000000000' = ?", namHoc == null ? Guid.Empty : namHoc.Oid, namHoc == null ? Guid.Empty : namHoc.Oid);
                            listView.CollectionSource.Criteria["FilterNamHoc"] = filter;
                        }
                        if (item.FieldName == "KyTinhLuong!")
                        {
                            _KyTinhLuong = true;
                        }
                        if (item.FieldName == "SoQuyetDinh")
                        {
                            _QuyetDinh = true;
                        }
                    }
                    if (_NamHoc == false && _KyTinhLuong == true)
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse(" (? <= KyTinhLuong.DenNgay AND KyTinhLuong.DenNgay < ? ) OR '00000000-0000-0000-0000-000000000000' = ?", namHoc == null ? DateTime.MinValue.Date : namHoc.NgayBatDau, namHoc == null ? DateTime.MaxValue.Date : namHoc.NgayKetThuc, namHoc == null ? Guid.Empty : namHoc.Oid);
                        listView.CollectionSource.Criteria["FilterNamHoc"] = filter;
                    }
                    if (_NamHoc == false && _QuyetDinh == true)
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse(" (? <= NgayHieuLuc AND NgayHieuLuc < ? ) OR '00000000-0000-0000-0000-000000000000' = ?", namHoc == null ? DateTime.MinValue.Date : namHoc.NgayBatDau, namHoc == null ? DateTime.MaxValue.Date : namHoc.NgayKetThuc, namHoc == null ? Guid.Empty : namHoc.Oid);
                        listView.CollectionSource.Criteria["FilterNamHoc"] = filter;
                    }
                }
                else if (listView != null && listView.Editor is TreeListEditor && listView.IsRoot)
                {
                    treeList = (((DevExpress.ExpressApp.ListView)View).Editor as TreeListEditor).TreeList;
                    foreach (TreeListColumn item in treeList.Columns)
                    {
                        if (item.FieldName == "NamHoc")
                        {
                            _NamHoc = true;
                            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc.Oid = ? OR '00000000-0000-0000-0000-000000000000' = ?", namHoc == null ? Guid.Empty : namHoc.Oid, namHoc == null ? Guid.Empty : namHoc.Oid);
                            listView.CollectionSource.Criteria["FilterNamHoc"] = filter;
                        }
                        if (item.FieldName == "KyTinhLuong")
                        {
                            _KyTinhLuong = true;
                        }
                    }
                    if (_NamHoc == false && _KyTinhLuong == true)
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse(" (? <= KyTinhLuong.DenNgay AND KyTinhLuong.DenNgay < ? ) OR '00000000-0000-0000-0000-000000000000' = ?", namHoc == null ? DateTime.MinValue : namHoc.NgayBatDau, namHoc == null ? DateTime.MaxValue : namHoc.NgayKetThuc, namHoc == null ? Guid.Empty : namHoc.Oid);
                        listView.CollectionSource.Criteria["FilterNamHoc"] = filter;
                    }
                }
            }
        }
    }
}
