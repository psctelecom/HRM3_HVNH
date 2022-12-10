using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraTreeList;
using DevExpress.ExpressApp.TreeListEditors.Win;
using PSC_HRM.Module.Win.Common;
using DevExpress.Utils;
using PSC_HRM.Module.Win.Editors;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class CustomListViewController : ViewController
    {
        private ListView _listView;

        public CustomListViewController()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void CustomListViewController_Activated(object sender, EventArgs e)
        {
            //Cài đặt lưới ở đây
            View.ControlsCreated += View_ControlsCreated;
        }
        private void View_ControlsCreated(object sender, EventArgs e)
        {
            //Lấy listview
            _listView = View as ListView;

            if (_listView != null && _listView.Editor is GridListEditor)//Nếu là lưới
            {
                //Ép sang kiểu lưới 
                XafGridView gridView = (_listView.Editor as GridListEditor).GridView;
                if (gridView != null)
                {
                    //Cài đặt lưới theo ý người dùng
                    CustomGridView(gridView);
                }
            }
            else if (_listView != null && _listView.Editor is TreeListEditor)//Nếu là cây
            {
                //Ép sang kiểu cây
                TreeList tree = (_listView.Editor as TreeListEditor).TreeList;
                if (tree != null)
                {
                    //Cài đặt cây theo ý người dùng
                    CustomTreeList(tree);
                }
            }
        }


        private void CustomGridView(GridView gridView)
        {
            if (gridView != null)
            {
                ////Cài đặt thông tin cơ bản của lưới
                GridUtil.InitGridView(gridView);

                {// Tùy chỉnh thông tin theo đối tượng

                    //Hiển thị summaries trên đối tượng
                    SetupSummariesOfObject(gridView);

                    //Không cho tự động giản cột 
                    DisableColumnAutoWidthOfObject(gridView);

                    //Ẩn đi các cột của lưới
                    VisibleColumnsOfObject(gridView);

                    //Hiện dấu check lên lưới
                    SetCheckAllBoxToBooleanGridColumn(gridView);

                    //Cài đặt lại thông tin cơ bản của các lưới đặt biệt
                    SetupBasicInfoGridView(gridView);

                    //Sort gridview
                    SortGridView(gridView);

                    //Lưới nhiều dòng
                    SetupShowMultilineGridCell(gridView);
                }
            }
        }
        private void CustomTreeList(TreeList tree)
        {
            if (tree != null)
            {
                //Cài đặt cây theo ý người dùng
                TreeUtil.InitTreeView(tree);

                //Sắp sếp theo cột của cây
                SortColumnOfTreeList(tree);

                //Ẩn đi các cột của cây
                VisibleColumnsOfObject(tree);

                //Chỉnh lại chiều rộng của cây
                SetWidthOfColumn(tree);

            }
        }

        private void SortGridView(GridView gridView)
        {
            if (_listView.Id == "BangChotThongTinTinhLuong_ListThongTinTinhLuong_ListView")
            {
                //Xóa tất cả sort cũ
                gridView.ClearSorting();

                //Sort theo cột STT
                gridView.Columns["STT"].OptionsColumn.AllowSort = DefaultBoolean.True;

                gridView.Columns["NgayVaoCoQuan"].OptionsColumn.AllowSort = DefaultBoolean.True;

                //Sắp xếp tăng dần
                gridView.Columns["STT"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView.Columns["NgayVaoCoQuan"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
        }

        private void SetupShowMultilineGridCell(GridView gridView)
        {
            if (_listView.Id.Contains("ChonThongTinBangChot_listBangChot_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "TenHoatDong", "LopHocPhan" });
            }
            if (_listView.Id.Contains("BangThuLaoNhanVien_ListChiTietThuLaoNhanVien_ListView"))
            {
                GridUtil.ShowMultilineGridCell(gridView, new string[] { "DienGiai" });
            }
        }

        private void SetupSummariesOfObject(GridView gridView)
        {
            //if (_view.Id.Contains("ThongTinNhanVien_RutGon_ListView"))
            //{
            //    //Hiển thị cột tổng số dòng ở footer
            //    GridUtil.ShowSummaries(gridView, true, false, false, false, false, "Ten");
            //}
            //if (_view.Id.Contains("QuanLyChamCongNhanVien_ChiTietChamCongNhanVienList_ListView"))
            //{
            //    //Hiển thị cột tổng số dòng ở footer
            //    GridUtil.ShowSummaries(gridView, true, true, true, true, true, "NghiThaiSan");
            //}

        }
        private void VisibleColumnsOfObject(GridView gridView)
        {
            //Chú ý: Trưởng hợp này chỉ dùng cho listview dạng lưới còn detailview phải phân quyền trên property
            #region NEU
            if (TruongConfig.MaTruong=="NEU")
            {
                if (_listView.Id == "QuanLyGioChuan_ListDinhMucChucVuNhanVien_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "SoGioDinhMuc_NCHK", "SoGioDinhMuc_Khac", "ChiTinhGioChuan" });
                }        
                else
                    if(_listView.Id=="QuanLyGioChuan_ListDinhMucChucVu_ListView")
                    {
                        GridUtil.InvisibleColumn(gridView, new string[] { "SoGioDinhMuc_NCHK", "SoGioDinhMuc_Khac", "ChiTinhGioChuan" });
                    }
                    else
                        if (_listView.Id == "QuanLyHeSo_ListHeSoGiangDayNgoaiGio_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "HeDaoTao", "BacDaoTao", "Thu" });
                        }
                        else
                            if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                            {
                                GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                            }
                            else if(_listView.Id =="Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                            {
                                GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                            }
            }
            #endregion
            else
            #region UFM
            if (TruongConfig.MaTruong.Equals("UFM"))
            {
                if (_listView.Id == "ThongTinBangChot_Moi_ListChiTietBangChot_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "TongGioA1", "TongGioA2", "SoTietKiemTra", "TongNo", "SoTienThanhToan", "LoaiChuongTrinh", "DaTinhThuLao", "LoaiNhanVien", "LoaiNhanVien.TinhThuLao" });
                }
                else
                if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_Moi_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "TongGioA1", "TongGioA2","SoTienThanhToanHDKhac","SoTienThanhToanVuotGio",
                                                                        "GioBaoLuu","GioBaoLuuNCKH","GioBaoLuuHDQL",
                                                                        "TongGiamGD","TongGiamNCKH","TongGiamHDQL",
                                                                        "TongDinhMucGDCuoi","TongDinhMucNCKHCuoi","TongDinhMucHDQLCuoi",
                                                                        "GioGDVuotThieu","GioNVKHVuotThieu","GioHDQLVuotThieu",
                                                                        "TongGioThucHienHDQL","GioHDQL",
                                                                        "TongTienThanhToanThueTNCN","GioChuanThanhToan","HeSoChucDanh"
                                                                    });
                }
                else
                if (_listView.Id == "QuanLyHeSo_ListHeSoThucHanh_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "TuKhoan", "DenKhoan" });
                }
                else
                if (_listView.Id == "QuanLyHeSo_ListHeSoGiangDayNgoaiGio_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "GioGiangDay", "HeSoHe" });
                }
                else
                if (_listView.Id == "QuanLyHoatDongKhac_listChiTiet_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "VaiTro", "BacDaoTao", "HoatDongKhac", "DienGiai", "SoThanhVien", "DuKien", "XacNhan", "NgayNha", "SoTienThanhToan", "SoTienThanhToanThueTNCN" });
                }
                else
                    if(_listView.Id=="KhoiLuongGiangDay_ListChiTietKhoiLuongGiangDay_Moi_ListView")
                    {
                        GridUtil.InvisibleColumn(gridView, new string[] { "GioQuyDoiThucHanh", "ThongTin", "HeSo_CoSo", "HeSo_TinChi", "NgonNguGiangDay", "HeSo_BacDaoTao", "HeSo_NgonNgu", "TietBD", "TietKT", "Thu", "LoaiChuongTrinh" });
                    }
                    else
                        if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                        }
                        else if (_listView.Id == "Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                        }
            }
            #endregion
            else
            #region VHU
                if (TruongConfig.MaTruong.Equals("VHU"))
            {
                if (_listView.Id == "QuanLyHeSo_ListHeSoChucDanh_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "LoaiGiangVien" });
                }
                else
                    if (_listView.Id == "KhoiLuongGiangDay_ListChiTietKhoiLuongGiangDay_Moi_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "MaLopGhep", "NgayBD", "NgayKT", "PhongHoc" });
                }
                else
                        if (_listView.Id == "QuanLyGioChuan_ListDinhMucChucVuNhanVien_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "HeSoChucDanh", "ChucDanh", "ChucVu" });
                }
                else
                            if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_Moi_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "TongGioA1", "TongGioA2", "SoTienThanhToanHDKhac" });
                }
                else
                                if (_listView.Id == "QuanLyGioChuan_ListDinhMucChucVu_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "SoGioDinhMuc_Khac" });
                }
                else
                                    if (_listView.Id == "CheDoXaHoi_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "TinhTheoThang" });
                }
                else
                                        if (_listView.Id == "QuanLyCheDoXaHoi_ListChiTietCheDoXH_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "SoThang", "GiamTru" });
                }
                else
                                            if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "MaGiangVien", "TrinhDoChuyenMon", "BoPhan", "HocHam",
                                                                                        "ChucVu","SoTietThamQuan", "SoTietDiHoc", "SoTietKiemNhiem",
                                                                                        "ConNho_PhuTaTn_vv", "SoTietDinhMuc", "SoTietHopDong",
                                                                                        "SoTietPhuTroi", "TongTietKiemTra", "TongTietThucHien", "DinhMucGD" });
                }
                else
                                                if (_listView.Id == "QuanLyHoatDongKhac_ListThanhToanKLGD_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "BacDaoTao", "CuNhanTN",
                                                                                            "HeSo_Khac",
                                                                                            "ChiPhiDiLai","DonGiaTietChuan",
                                                                                            "ThanhTien","TongTien",
                                                                                            "NoGioHKTruoc","NoGioHKNay",
                                                                                            "TongTienNo","ThueTNCNTamTru","ConLaiThanhToan"});
                }
                                                else
                                                    if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                                                    {
                                                        GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                                                    }
                                                    else if (_listView.Id == "Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                                                    {
                                                        GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                                                    }
            }
            #endregion
            else
            #region UEL
                    if (TruongConfig.MaTruong.Equals("UEL"))
            {
                if (_listView.Id == "BangThuLaoNhanVien_ListChiTietThuLaoNhanVien_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "GiamTru1", "GiamTru2", "GiamTru3", "DienGiai1", "DienGiai2", "DienGiai3" });
                }
                else
                    if (_listView.Id == "ThongTinBangChot_ListChiTietBangChot_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "DaTinhThuLao", "BacDaoTao" });
                }
                    else
                        if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                        }
                        else if(_listView.Id =="Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "NgayDB","NgayKT","ThoiDiemThanhLy","NgonNgu","LoaiHocPhan"});
                        }
                        else if(_listView.Id =="Quanlythanhtra_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "Khoa" });
                        }
                        else if (_listView.Id == "QuanLyHoatDongKhac_ListView")
                                {
                                    //Ẩn cột
                                    GridUtil.InvisibleColumn(gridView, new string[] { "KyTinhPMS", "HocKy" });
                                }
                                else
                                    if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                                {
                                    GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                                }
            }
            #endregion
            else
            #region QNU
                        if (TruongConfig.MaTruong.Equals("QNU"))
                        {
                            if (_listView.Id == "QuanLyGioChuan_ListDinhMucChucVuNhanVien_ListView")
                            {
                                GridUtil.InvisibleColumn(gridView, new string[] { "SoGioDinhMuc_NCHK", "SoGioDinhMuc_Khac" });
                            }
                            else
                                if (_listView.Id == "QuanLyHeSo_ListHeSoCoSo_ListView")
                                {
                                    GridUtil.InvisibleColumn(gridView, new string[] { "BacDaoTao", "ThoiGiangHoc" });
                                }
                                else
                                    if (_listView.Id == "ThongTinNhanVien_RutGon_ListView")
                                    {
                                        GridUtil.InvisibleColumn(gridView, new string[] { "MaQuanLy" });
                                    }
                                    else
                                        if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_ListView")
                                        {
                                            GridUtil.InvisibleColumn(gridView, new string[] { "SoTietThamQuan", "SoTietDiHoc", "SoTietKiemNhiem", "ConNho_PhuTaTn_vv", "NghienCuuKhoaHoc", "SoTietKhac", "DinhMucGD", "SoTietDinhMuc", "SoTietHopDong", "SoTietPhuTroi", "TongTietThucHien", "TongTietKiemTra" });
                                        }
                                        else
                                            if (_listView.Id == "QuanLyGioChuan_ListView")
                                            {
                                                GridUtil.InvisibleColumn(gridView, new string[] { "HocKy" });
                                            }
                                            else
                                                if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                                                {
                                                    GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                                                }
                                                else if (_listView.Id == "Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                                                {
                                                    GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                                                }

                        }
            #endregion
            else
            #region HVNH
            if (TruongConfig.MaTruong.Equals("HVNH"))
            {
                if (_listView.Id == "BangThuLaoNhanVien_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "HienLenWeb" });
                }
                else
                    if (_listView.Id == "KhoiLuongGiangDay_ListChiTietKhoiLuongGiangDay_ListView")
                    {
                        //
                        GridUtil.InvisibleColumn(gridView, new string[] { "KhoaHoc","LoaiHocPhan", "SoTietThaoLuan", "SoTietThucHanh"
                                                    , "SoNhomThucHanh", "SoGioTNTH", "SoBaiTNTH", "SoBaiTNTH"
                                                    , "SoTiet_DoAn","SoTiet_BaiTapLon"
                                                    , "HeSo_MonMoi","HeSo_TNTH","HeSo_ThaoLuan","HeSo_Luong","HeSo_ChucDanh", "HeSo_CoSo"
                                                    , "SoBaiTNTH_GioChuan","HeSo_DoAn","HeSo_BTL","GioQuyDoiThaoLuan","TongGioLyThuyetThaoLuan"
                                                    , "GioQuyDoiThucHanh","GioQuyDoiDoAn","GioQuyDoiBTL","TongGioTNTH_DA_BTL"
                                                    ,"HocHam","TrinhDoChuyenMon"});
                    }
                    else
                        if (_listView.Id == "BangThuLaoNhanVien_ListChiTietThuLaoNhanVien_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "GiamTru1", "GiamTru2", "GiamTru3", "DienGiai1", "DienGiai2", "DienGiai3" });
                        }
                        else
                            if (_listView.Id == "QuanLyHeSo_ListHeSoGiangDayNgoaiGio_ListView")
                            {
                                GridUtil.InvisibleColumn(gridView, new string[] { "Thu", "TuTiet", "DenTiet", "HeSoHe" });
                            }
                            else
                                if (_listView.Id == "QuanLyHeSo_ListHeSoLopDong_ListView")
                                {
                                    GridUtil.InvisibleColumn(gridView, new string[] { "BacDaoTao", "LoaiMonHoc", "NgonNgu", "ChuyenNganh" });
                                }
                                else
                                    if (_listView.Id == "QuanLyHeSo_ListHeSoNgonNgu_ListView")
                                    {
                                        GridUtil.InvisibleColumn(gridView, new string[] { "MaQuanLy" });
                                    }
                                    else
                                        if (_listView.Id == "BangChotThuLao_ListThongTinBangChot_ListView")
                                        {
                                            GridUtil.InvisibleColumn(gridView, new string[] { "TrinhDoChuyenMon", "TongGioA1", "TongGioA2", "SoTienThanhToanHDKhac", "SoTienThanhToanVuotGio", "TongTienThanhToan", "TongTienThanhToanThueTNCN" });
                                        }
                                        else
                                            if (_listView.Id == "QuanLyGioChuan_ListDinhMucChucVuNhanVien_ListView")
                                            {
                                                GridUtil.InvisibleColumn(gridView, new string[] { "ChucDanh", "HeSoChucDanh" });
                                            }
                                            else
                                                if (_listView.Id == "BangThuLaoNhanVien_ListChiTietThuLaoNhanVien_ListView")
                                                {
                                                    GridUtil.InvisibleColumn(gridView, new string[] { "DaThanhToan" });
                                                }
                                                else
                                                    if (_listView.Id == "QuanLyGioGiang_ListNhanVien_GioGiang_ListView")
                                                    {
                                                        GridUtil.InvisibleColumn(gridView, new string[] { "ThanhTienTamUng", "ThanhTienConLai" });
                                                    }
                                                    else
                                                        if (_listView.Id == "ChonThongTinBangChot_listBangChot_ListView")
                                                        {
                                                            GridUtil.InvisibleColumn(gridView, new string[] { "LopHocPhan", "TenHoatDong", "SoTienThanhToan" });
                                                        }
                                                        else
                                                            if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                                                            {
                                                                GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                                                            }
                                                            else if (_listView.Id == "Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                                                            {
                                                                GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                                                            }
                                                                else if (_listView.Id == "QuanLyGioChuan_ListView")
                                                                {
                                                                    GridUtil.InvisibleColumn(gridView, new string[] { "HocKy" });
                                                                }
            }
            #endregion
            else
            #region DNU
            if (TruongConfig.MaTruong.Equals("DNU"))
            {

                if (_listView.Id == "KhoiLuongGiangDay_ListChiTietKhoiLuongGiangDay_ListView")
                {
                    //
                    GridUtil.InvisibleColumn(gridView, new string[] { "NgonNguGiangDay","MaNhom","GioGiangDay","LoaiHocPhan",
                                                                        "SoBaiTNTH","SoTiet_DoAn","SoTiet_BaiTapLon","SoNhomThucHanh","HeSo_DoAn",
                                                                        "HeSo_Luong","HeSo_GiangDayNgoaiGio","HeSo_MonMoi","HeSo_ThaoLuan","SoBaiTNTH_GioChuan",
                                                                        "HeSo_BTL","HeSo_BacDaoTao","HeSo_NgonNgu","TongHeSo","GioQuyDoiThaoLuan","TongGioLyThuyetThaoLuan",
                                                                        "GioQuyDoiChamBaiTNTH","GioQuyDoiDoAn","GioQuyDoiBT","TongGioTNTH_DA_BTL","SoBaiTNTH_GioChuan",
                                                                        "BacDaoTao","KhoaHoc","SoTietThaoLuan","SoNhomThucHanh","SoGioTNTH","GioQuyDoiBTL", "MaMonHoc"});
                }
                else
                if (_listView.Id == "KhaiBao_KhoiLuongGiangDay_listKetKhai_ListView")
                {
                    //
                    GridUtil.InvisibleColumn(gridView, new string[] { "KiemNhiem", "ConNho", "NghienCuuKhoaHoc", "Khac" });
                }
                else
                    if (_listView.Id == "QuanLyHeSo_ListHeSoLopDong_ListView")
                    {
                        GridUtil.InvisibleColumn(gridView, new string[] { "BacDaoTao", "LoaiMonHoc", "NgonNgu", "ChuyenNganh" });
                    }
                    else
                        if (_listView.Id == "QuanLyThanhToanKhaoThi_ListThongTinThanhToanKhaoThi_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "TienThucLanhTL", "TienThucLanhVD", "TienThucLanhChamThi" });
                        }
                        else if (_listView.Id == "Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                        }

            }
            #endregion
            else
            #region HUFLIT
            if (TruongConfig.MaTruong.Equals("HUFLIT"))
            {
                
                if (_listView.Id == "QuanLyHeSo_ListHeSoLopDong_ListView")
                {
                    GridUtil.InvisibleColumn(gridView, new string[] { "BacDaoTao", "LoaiMonHoc" });
                }
                else
                    if (_listView.Id == "KhoiLuongGiangDay_ListChiTietKhoiLuongGiangDay_Moi_ListView")
                    {
                        GridUtil.InvisibleColumn(gridView, new string[] { "GioQuyDoiLyThuyet", "GioQuyDoiThucHanh", "NgonNguGiangDay", "LoaiHocPhan", "HeSo_DaoTao", "HeSo_TNTH", "HeSo_BacDaoTao", "HeSo_NgonNgu", "GhiChuThanhTra", "Import", "ThoiDiemThanhLy", "SoTietGhiNhan", "LoaiChuongTrinh", "NgayBD", "NgayKT", "HeSoCoSo" });
                    }
                    else
                        if(_listView.Id=="ThongTinBangChotThuLao_ListChiTietBangChot_ListView")
                        {
                            GridUtil.InvisibleColumn(gridView, new string[] { "HocKy" });
                        }
                        else
                            if (_listView.Id == "QuanLyCheDoXaHoi_ListChiTietCheDoXH_ListView")
                            {
                                GridUtil.InvisibleColumn(gridView, new string[] { "SoThang", "GiamTru" });
                            }
                            else
                                if (_listView.Id == "KhoiLuongGiangDay_ThinhGiang_ListChiTietKhoiLuongGiangDay_ThinhGiang_ListView")
                                {
                                    GridUtil.InvisibleColumn(gridView, new string[] { "GioQuyDoiLyThuyet", "GioQuyDoiThucHanh", "NgonNguGiangDay", "LoaiHocPhan", "HeSo_DaoTao", "HeSo_TNTH", "HeSo_BacDaoTao", "HeSo_NgonNgu", "GhiChuThanhTra", "Import", "ThoiDiemThanhLy", "SoTietGhiNhan", "LoaiChuongTrinh", "NgayBD", "NgayKT", "HeSoCoSo" });
                                }
                                else
                                    if (_listView.Id == "QuanLyHoatDongKhac_listChiTiet_ListView")
                                    {
                                        GridUtil.InvisibleColumn(gridView, new string[] { "VaiTro","BacDaoTao","SoThanhVien","SoTienThanhToan","SoTienThanhToanThueTNCN","LoaiHoatDong","DuKien" });
                                    }
                                    else
                                        if (_listView.Id == "QuanLyHoatDongKhac_ListChamBaiCoiThi_ListView")
                                        {
                                            GridUtil.InvisibleColumn(gridView, new string[] { "BacDaoTao","CMND","TongTienThueTNCN","TongTien","KhoanChi","LopHocPhan","SoBaiQuaTrinh;","SoBaiGiuaKy","SoBaiCuoiKy","DonGiaQuaTrinh","DonGiaGiuaKy","DonGiaCuoiKy" });
                                        }
                                        else if (_listView.Id == "Quanlythanhtra_ListChiTietThanhTraGiangDay_ListView")
                                        {
                                            GridUtil.InvisibleColumn(gridView, new string[] { "NgayDay", "LoaiHocPhanObject", "LayDuLieuKhoiLuongGiangDay" });
                                        }

            }
            #endregion
        }

        private void VisibleColumnsOfObject(TreeList tree)
        {
            //Chú ý: Trưởng hợp này chỉ dùng cho listview dạng cây còn detailview phải phân quyền trên property

            if (TruongConfig.MaTruong.Equals("IHU"))
            {
                if (_listView.Id == "BoPhan_ListView")
                {
                    TreeUtil.InvisibleColumn(tree, new string[] { "TrinhDoChuyenMonCaoNhat", "TenVietTat" });
                }
            }

            if (TruongConfig.MaTruong.Equals("LUH"))
            {
                if (_listView.Id == "BoPhan_ListView")
                {
                    TreeUtil.InvisibleColumn(tree, new string[] { "MThamSoLuongKy2", "MThamSoPCTrachNhiem", "TrinhDoChuyenMonCaoNhat", "TenBoPhanVietTat" });
                }
            }

            if (TruongConfig.MaTruong.Equals("UTE"))
            {
                if (_listView.Id == "BoPhan_ListView")
                {
                    TreeUtil.InvisibleColumn(tree, new string[] { "MThamSoLuongKy2", "MThamSoPCTrachNhiem", "TrinhDoChuyenMonCaoNhat", "TenBoPhanVietTat" });
                }
            }

            if (TruongConfig.MaTruong.Equals("BUH"))
            {
                if (_listView.Id == "BoPhan_ListView")
                {
                    TreeUtil.InvisibleColumn(tree, new string[] { "MThamSoLuongKy2", "MThamSoPCTrachNhiem" });
                }
            }

            if (TruongConfig.MaTruong.Equals("HBU"))
            {
                if (_listView.Id == "BoPhan_ListView")
                {
                    TreeUtil.InvisibleColumn(tree, new string[] { "MThamSoLuongKy2", "MThamSoPCTrachNhiem", "TrinhDoChuyenMonCaoNhat", "TenBoPhanVietTat" });
                }
            }

            if (TruongConfig.MaTruong.Equals("DLU"))
            {
                if (_listView.Id == "BoPhan_ListView")
                {
                    TreeUtil.InvisibleColumn(tree, new string[] { "MThamSoLuongKy2", "MThamSoPCTrachNhiem", "TrinhDoChuyenMonCaoNhat", "TenBoPhanVietTat" });
                }
            }
        }
        private void DisableColumnAutoWidthOfObject(GridView gridView)
        {
            //
            if (!_listView.Id.Contains("GiangVienThinhGiang_RutGon_ListView") &&
                !_listView.Id.Contains("ThongTinNhanVien_RutGon_ListView") &&
                !_listView.Id.Contains("LookupListView") &&
                !_listView.Id.Contains("HRMReport") &&
                !_listView.Id.Contains("DoanVien_ListView") &&
                !_listView.Id.Contains("DoanThe_ListView") &&
                !_listView.Id.Contains("DangVien_ListView") &&
                !_listView.Id.Contains("HoSoBaoHiem_ListView") &&
                !_listView.Id.Contains("BangChotThuLao_ListView") &&
                !_listView.Id.Contains("KhoiLuongGiangDay_ListView") &&
                !_listView.Id.Contains("QuanLyKeKhaiSauGiang_ListView") &&
                !_listView.Id.Contains("QuanLyBaiKiemTra_ListView") &&
                !_listView.Id.Contains("QuanLyKhaoThi_ListView") &&
                !_listView.Id.Contains("QuanLyCongTacPhi_ListView") &&
                !_listView.Id.Contains("NhanVienCongNhat_ListView")
               )
            {
                gridView.OptionsView.ColumnAutoWidth = false;

                gridView.BestFitColumns();
                gridView.BestFitMaxRowCount = -1;
            }
        }

        private void SortColumnOfTreeList(TreeList tree)
        {
            if (_listView.Id.Contains("BoPhan_ListView"))
            {
                //Sort theo số thứ tự
                TreeUtil.AllowSortColumn(tree, new string[] { "STT" }, true);
            }
        }

        private void SetWidthOfColumn(TreeList tree)
        {
            if (_listView.Id.Contains("BoPhan_ListView"))
            {
                //Sort theo số thứ tự
                TreeUtil.SetWidthOfColumn(tree, new string[] { "TenBoPhan" }, 300);
            }
        }

        private void SetCheckAllBoxToBooleanGridColumn(GridView gridView)
        {
            if (_listView.Id.Equals("ExportReport_BaoCaoList_ListView")
                || _listView.Id.Equals("HoSo_ChonNhanVien_ListNhanVien_ListView")
                || _listView.Id.Equals("DanhSachSinhNhatCanBo_ListSinhNhatCanBo_ListView")
                || _listView.Id.Equals("DanhSachDenHanNangLuong_DanhSachNhanVien_ListView")
                || _listView.Id.Equals("TamGiuLuongNhanVien_ListChiTietTamGiuLuongNhanVien_ListView")

                || _listView.Id.Equals("NonThongTinNhanVien_ListView")//PMS
                || _listView.Id.Equals("QLyTKB_KhoaDuLieu_listTKB_ListView")//PMS
                || _listView.Id.Equals("ChonKhoiLuongGiangDay_HeSoChucDanhMonHoc_listKhoiLuong_ListView")//PMS
                || _listView.Id.Equals("ChonThongTinBangChot_listBangChot_ListView")
                || _listView.Id == "dsThanhToanThuLao_ListView"
                || _listView.Id == "ChonKhoiLuongGiangDay_listKhoiLuong_ListView"
                || _listView.Id == "ChonNhanVien_listNhanVien_ListView"
                || _listView.Id == "QuanLyTKB_Delete_listTKB_ListView"
                || _listView.Id == "QuanLyKhongTinhTienTKB_LisDanhSach_ListView"

               )
            {

                GridUtil.BooleanCheckAllBox.SetCheckAllBoxToBooleanGridColumn(gridView, gridView.Columns["Chon"], DevExpress.Utils.HorzAlignment.Near);
            }

            if (_listView.Id.Equals("VongTuyenDung_ListChiTietVongTuyenDung_ListView"))
            {
                GridUtil.BooleanCheckAllBox.SetCheckAllBoxToBooleanGridColumn(gridView, gridView.Columns["DuocChuyenQuaVongSau"], DevExpress.Utils.HorzAlignment.Near);
            }
        }

        private void SetupBasicInfoGridView(GridView gridView)
        {
            if (_listView.Id == "CongThucTinhLuong_ListView" ||
                 _listView.Id == "CongThucTinhLuong_ListChiTietCongThucTinhLuong_ListView"
               )
            {
                GridUtil.SetupBasicInfoGridView(gridView);
            }
        }
    }
}