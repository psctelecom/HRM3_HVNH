using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.Xpo;
using PSC_HRM.Module.QuaTrinh;
using System.Windows.Forms;
using System.Data;
using System.Text;

namespace PSC_HRM.Module.Controllers
{
    internal static class StringEx
    {
        internal static String FullTrim(this String source)
        {
            return source.Trim().Replace("  ", " ");
        }

        internal static String RemoveEmpty(this String source)
        {
            return source.Trim().Replace(" ", "");
        }
    }

    public partial class HopDong_ImportHopDongController : ViewController
    {
        private IObjectSpace obs;
        private HopDong_ImportHopDong importHopDong;
        private QuanLyHopDong quanLyHopDong;

        public HopDong_ImportHopDongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_ImportHopDongController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            importHopDong = obs.CreateObject<HopDong_ImportHopDong>();
            e.View = Application.CreateDetailView(obs, importHopDong);
        }

        private void HopDong_ImportHopDongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<HopDong.HopDong>()
                && HamDungChung.IsWriteGranted<HopDong_NhanVien>()
                && HamDungChung.IsWriteGranted<HopDong_LamViec>()
                && HamDungChung.IsWriteGranted<HopDong_Khoan>()
                && HamDungChung.IsWriteGranted<HopDong_LaoDong>();
        }

        private void popupWindowShowAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            QuanLyHopDong quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            //
            if (quanLyHopDong != null)
            {
                if (TruongConfig.MaTruong.Equals("DLU"))
                {
                    XuLy_DLU();
                }
                else
                {
                    XuLy();
                }
            }
        }

        private void XuLy()
        {
            if (importHopDong != null && importHopDong.LoaiHopDong == TaoHopDongEnum.HopDongKhoan)
            {
                DialogUtil.ShowError("Không import hợp đồng khoán.");
                return;
            }
            if (importHopDong.NguoiKy == null)
            {
                DialogUtil.ShowWarning("Chưa chọn người ký.");
                return;
            }
            //Lưu bảng quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            //
            quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            //
            if (quanLyHopDong != null)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            //
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                //
                                using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:K]"))
                                {
                                    StringBuilder mainLog = new StringBuilder();
                                    StringBuilder detailLog;
                                    ThongTinNhanVien nhanVien;

                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        foreach (DataRow item in dt.Rows)
                                        {
                                            detailLog = new StringBuilder();
                                            //
                                            int idx_MaQuanLy = 0;
                                            int idx_SoHopDong = 3;
                                            int idx_HinhThucHopDong = 4;
                                            int idx_TuNgay = 5;
                                            int idx_DenNgay = 6;
                                            int idx_Huong85PhanTram = 7;
                                            int idx_GhiChu = 8;

                                            if (string.IsNullOrEmpty(item[idx_MaQuanLy].ToString().Trim()))//Nếu không có mã thì ngừng
                                            {
                                                break;
                                            }
                                            //Tìm nhân viên theo mã quản lý
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", item[idx_MaQuanLy].ToString().Trim()));
                                            if (nhanVien != null)
                                            {
                                                #region Hợp đồng làm việc
                                                if (importHopDong.LoaiHopDong == TaoHopDongEnum.HopDongLamViec)
                                                {
                                                    HopDong_LamViec hd = uow.FindObject<HopDong_LamViec>(CriteriaOperator.Parse("QuanLyHopDong=? and SoHopDong=?", quanLyHopDong.Oid, item[idx_SoHopDong].ToString().Trim()));
                                                    if (hd == null)
                                                    {
                                                        hd = new HopDong_LamViec(uow);
                                                        hd.QuanLyHopDong = uow.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                                                        //
                                                        hd.NhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                        hd.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                        hd.PhanLoai = importHopDong.PhanLoaiHDLV;
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString().Trim();
                                                        hd.NgayKy = importHopDong.NgayKy;
                                                        hd.PhanLoaiNguoiKy = importHopDong.PhanLoaiNguoiKy;
                                                        hd.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(importHopDong.ChucVuNguoiKy.Oid);
                                                        hd.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(importHopDong.NguoiKy.Oid);
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_HinhThucHopDong].ToString().RemoveEmpty()) && hd.PhanLoai != HopDongLamViecEnum.KhongThoiHan)
                                                        {
                                                            HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong like ?", item[idx_HinhThucHopDong].ToString().Trim()));
                                                            if (hinhThucHopDong != null)
                                                                hd.HinhThucHopDong = hinhThucHopDong;
                                                            else
                                                                detailLog.AppendLine(" + Hình thức hợp đồng không hợp lệ:" + item[idx_HinhThucHopDong].ToString());
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_TuNgay].ToString().Trim()))
                                                        {
                                                            try
                                                            {
                                                                hd.TuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + item[idx_TuNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_DenNgay].ToString().Trim()) && hd.PhanLoai != HopDongLamViecEnum.KhongThoiHan)
                                                        {
                                                            try
                                                            {
                                                                hd.DenNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + item[idx_DenNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (item[idx_Huong85PhanTram].ToString().Trim().ToLower() == "x")
                                                            hd.DieuKhoanHopDong.Huong85PhanTramMucLuong = true;
                                                        else
                                                            hd.DieuKhoanHopDong.Huong85PhanTramMucLuong = false;
                                                        //
                                                        hd.GhiChu = item[idx_GhiChu].ToString().Trim();

                                                        //Nếu không phải hợp đồng không thời hạn thì bắt buộc nhập hình thức hợp đồng
                                                        if (hd.PhanLoai != HopDongLamViecEnum.KhongThoiHan && hd.HinhThucHopDong == null)
                                                        {
                                                            detailLog.AppendLine(" + Hình thức hợp đồng không được trống.");
                                                        }
                                                    }
                                                    else
                                                        detailLog.AppendLine(" + Hợp đồng đã tồn tại: " + item[idx_SoHopDong].ToString() + " - Năm " + quanLyHopDong.NamHoc);
                                                }
                                                #endregion
                                                #region Hợp đồng hệ số
                                                else if (importHopDong.LoaiHopDong == TaoHopDongEnum.HopDongHeSo)
                                                {
                                                    HopDong_LaoDong hd = uow.FindObject<HopDong_LaoDong>(CriteriaOperator.Parse("QuanLyHopDong=? and SoHopDong=?", quanLyHopDong.Oid, item[idx_SoHopDong].ToString().Trim()));
                                                    if (hd == null)
                                                    {
                                                        hd = new HopDong_LaoDong(uow);
                                                        //
                                                        hd.QuanLyHopDong = uow.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                                                        //
                                                        hd.NhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                        hd.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                        hd.PhanLoai = importHopDong.PhanLoaiHDLD;
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString().Trim();
                                                        hd.NgayKy = importHopDong.NgayKy;
                                                        hd.PhanLoaiNguoiKy = importHopDong.PhanLoaiNguoiKy;
                                                        hd.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(importHopDong.ChucVuNguoiKy.Oid);
                                                        hd.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(importHopDong.NguoiKy.Oid);
                                                        hd.HinhThucThanhToan = importHopDong.HinhThucThanhToan;
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_HinhThucHopDong].ToString().RemoveEmpty()) && hd.PhanLoai != HopDongLaoDongEnum.KhongThoiHan)
                                                        {
                                                            HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("TenHinhThucHopDong like ?", item[idx_HinhThucHopDong].ToString().Trim()));
                                                            if (hinhThucHopDong != null)
                                                                hd.HinhThucHopDong = hinhThucHopDong;
                                                            else
                                                                detailLog.AppendLine(" + Hình thức đồng không hợp lệ:" + item[idx_HinhThucHopDong].ToString());
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_TuNgay].ToString().Trim()))
                                                        {
                                                            try
                                                            {
                                                                hd.TuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + item[idx_TuNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_DenNgay].ToString().Trim()) && hd.PhanLoai != HopDongLaoDongEnum.KhongThoiHan)
                                                        {
                                                            try
                                                            {
                                                                hd.DenNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + item[idx_DenNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (item[idx_Huong85PhanTram].ToString().Trim().ToLower() == "x")
                                                            hd.DieuKhoanHopDong.Huong85PhanTramMucLuong = true;
                                                        else
                                                            hd.DieuKhoanHopDong.Huong85PhanTramMucLuong = false;
                                                        //
                                                        hd.GhiChu = item[idx_GhiChu].ToString().Trim();

                                                        //Nếu không phải hợp đồng không thời hạn thì bắt buộc nhập hình thức hợp đồng
                                                        if (hd.PhanLoai != HopDongLaoDongEnum.KhongThoiHan && hd.HinhThucHopDong == null)
                                                        {
                                                            detailLog.AppendLine(" + Hình thức hợp đồng không được trống.");
                                                        }

                                                    }
                                                    else
                                                        detailLog.AppendLine(" + Hợp đồng đã tồn tại: " + item[idx_SoHopDong].ToString() + " - Năm" + quanLyHopDong.NamHoc);
                                                }
                                                #endregion
                                                #region Hợp đồng khoán
                                                //    else
                                                //    {
                                                //        HopDong_Khoan hd = new HopDong_Khoan(uow);
                                                //        hd.QuanLyHopDong = quanLyHopDong;
                                                //        if (!string.IsNullOrEmpty(item[idx_LoaiHopDong].ToString()))
                                                //        {
                                                //            hd.SoHopDong = item[idx_SoHopDong].ToString().Trim();
                                                //        }
                                                //        hd.NgayKy = HamDungChung.GetServerTime();
                                                //        //
                                                //        if (!string.IsNullOrEmpty(item[idx_TuNgay].ToString().Trim()))
                                                //        {
                                                //            try
                                                //            {
                                                //                hd.TuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                                //            }
                                                //            catch (Exception ex)
                                                //            {
                                                //                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + item[idx_TuNgay].ToString());
                                                //            }
                                                //        }
                                                //        //
                                                //        if (!string.IsNullOrEmpty(item[idx_DenNgay].ToString().Trim()))
                                                //        {
                                                //            try
                                                //            {
                                                //                hd.TuNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                                //            }
                                                //            catch (Exception ex)
                                                //            {
                                                //                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + item[idx_DenNgay].ToString());
                                                //            }
                                                //        }
                                                //        hd.NhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                //        hd.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);

                                                //        //Chức danh chuyên môn
                                                //        CriteriaOperator filter = CriteriaOperator.Parse("Oid=? and LoaiNhanSu is not null", nhanVien.Oid);
                                                //        ThongTinNhanVien thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(filter);
                                                //        if (thongTinNhanVien != null && thongTinNhanVien.LoaiNhanSu != null)
                                                //            hd.ChucDanhChuyenMon = thongTinNhanVien.LoaiNhanSu.TenLoaiNhanSu;
                                                //    }
                                                #endregion

                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine(string.Format("- Không import hợp đồng của cán bộ [{0} - {1}] vào phần mềm được: ", nhanVien.MaQuanLy, nhanVien.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                }
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(" + Không tìm thấy mã nhân sự: [" + item[idx_MaQuanLy].ToString() + "] trong hệ thống.");
                                            }

                                        }
                                        if (mainLog.Length > 0)
                                        {
                                            uow.RollbackTransaction();

                                            if (DialogUtil.ShowYesNo("Không thể tạo hợp đồng vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                                            {
                                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                                {
                                                    saveFile.Filter = "Text files (*.txt)|*.txt";

                                                    if (saveFile.ShowDialog() == DialogResult.OK)
                                                    {
                                                        HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                            uow.CommitChanges();
                                            DialogUtil.ShowInfo("Import hợp đồng thành công.");

                                            View.ObjectSpace.Refresh();
                                            obs.Refresh();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void XuLy_DLU()
        {
            if (importHopDong.NguoiKy == null)
            {
                DialogUtil.ShowWarning("Chưa chọn người ký.");
                return;
            }
            //Lưu bảng quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            //
            quanLyHopDong = View.CurrentObject as QuanLyHopDong;
            //
            if (quanLyHopDong != null)
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DialogUtil.AutoWait())
                        {
                            //
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                //
                                using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:N]"))
                                {
                                    StringBuilder mainLog = new StringBuilder();
                                    StringBuilder detailLog;
                                    ThongTinNhanVien nhanVien;

                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        foreach (DataRow item in dt.Rows)
                                        {
                                            detailLog = new StringBuilder();
                                            //
                                            int idx_MaQuanLy = 1;
                                            int idx_SoHopDong = 4;
                                            int idx_HinhThucHopDong = 5;    
                                            int idx_TuNgay = 6;
                                            int idx_DenNgay = 7;                                                                                 
                                            int idx_NgachLuong = 8;
                                            int idx_BacLuong = 9;
                                            int idx_GhiChu = 10;

                                            if (string.IsNullOrEmpty(item[idx_MaQuanLy].ToString().Trim()))//Nếu không có mã thì ngừng
                                            {
                                                break;
                                            }
                                            //Tìm nhân viên theo mã quản lý
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", item[idx_MaQuanLy].ToString().Trim()));
                                            //quanLyHopDong = uow.FindObject<QuanLyHopDong>(CriteriaOperator.Parse("NamHoc.TenNamHoc=?", item[idx_TenNamHoc].ToString().Trim()));
                                            if (nhanVien != null && quanLyHopDong != null)
                                            {
                                                #region Hợp đồng làm việc
                                                if (importHopDong.LoaiHopDong == TaoHopDongEnum.HopDongLamViec)
                                                {
                                                    HopDong_LamViec hd = uow.FindObject<HopDong_LamViec>(CriteriaOperator.Parse("QuanLyHopDong=? and SoHopDong=?", quanLyHopDong.Oid, item[idx_SoHopDong].ToString().Trim()));
                                                    if (hd == null)
                                                    {
                                                        hd = new HopDong_LamViec(uow);
                                                        hd.QuanLyHopDong = quanLyHopDong;
                                                            //uow.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                                                        //
                                                        hd.NhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                        hd.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                        hd.PhanLoai = importHopDong.PhanLoaiHDLV;
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString().Trim();
                                                        hd.NgayKy = importHopDong.NgayKy;
                                                        hd.PhanLoaiNguoiKy = importHopDong.PhanLoaiNguoiKy;
                                                        hd.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(importHopDong.ChucVuNguoiKy.Oid);
                                                        hd.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(importHopDong.NguoiKy.Oid);
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_HinhThucHopDong].ToString().RemoveEmpty()) && hd.PhanLoai != HopDongLamViecEnum.KhongThoiHan)
                                                        {
                                                            HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_HinhThucHopDong].ToString().Trim()));
                                                            if (hinhThucHopDong != null)
                                                                hd.HinhThucHopDong = hinhThucHopDong;
                                                            else
                                                                detailLog.AppendLine(" + Hình thức hợp đồng không hợp lệ:" + item[idx_HinhThucHopDong].ToString());
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_TuNgay].ToString().Trim()))
                                                        {
                                                            try
                                                            {
                                                                hd.TuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + item[idx_TuNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_DenNgay].ToString().Trim()) && hd.PhanLoai != HopDongLamViecEnum.KhongThoiHan)
                                                        {
                                                            try
                                                            {
                                                                hd.DenNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + item[idx_DenNgay].ToString());
                                                            }
                                                        }

                                                        #region Ngạch lương
                                                        if (!item.IsNull(idx_NgachLuong) && !string.IsNullOrEmpty(item[idx_NgachLuong].ToString()))
                                                        {

                                                            NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuong].ToString().Trim()));
                                                            if (ngachLuong != null)
                                                            {
                                                                //
                                                                hd.DieuKhoanHopDong.NgachLuong = ngachLuong;

                                                                //Bậc lương - Hệ số lương
                                                                if (!item.IsNull(idx_BacLuong) && !string.IsNullOrEmpty(item[idx_BacLuong].ToString()))
                                                                {
                                                                    BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? ", ngachLuong, item[idx_BacLuong].ToString().Trim()));
                                                                    if (bacLuong != null)
                                                                    {
                                                                        hd.DieuKhoanHopDong.BacLuong = bacLuong;
                                                                        hd.DieuKhoanHopDong.HeSoLuong = bacLuong.HeSoLuong;
                                                                    }
                                                                    else
                                                                    {
                                                                        detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_BacLuong].ToString());
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_NgachLuong].ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                                        }
                                                        #endregion
                                                        
                                                        hd.GhiChu = item[idx_GhiChu].ToString().Trim();

                                                        //Nếu không phải hợp đồng không thời hạn thì bắt buộc nhập hình thức hợp đồng
                                                        if (hd.PhanLoai != HopDongLamViecEnum.KhongThoiHan && hd.HinhThucHopDong == null)
                                                        {
                                                            detailLog.AppendLine(" + Hình thức hợp đồng không được trống.");
                                                        }
                                                    }
                                                    else
                                                        detailLog.AppendLine(" + Hợp đồng đã tồn tại: " + item[idx_SoHopDong].ToString() + " - Năm " + quanLyHopDong.NamHoc);
                                                }
                                                #endregion

                                                #region Hợp đồng hệ số
                                                else if (importHopDong.LoaiHopDong == TaoHopDongEnum.HopDongHeSo)
                                                {
                                                    HopDong_LaoDong hd = uow.FindObject<HopDong_LaoDong>(CriteriaOperator.Parse("QuanLyHopDong=? and SoHopDong=?", quanLyHopDong.Oid, item[idx_SoHopDong].ToString().Trim()));
                                                    if (hd == null)
                                                    {
                                                        hd = new HopDong_LaoDong(uow);
                                                        //
                                                        hd.QuanLyHopDong = quanLyHopDong;
                                                            //uow.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                                                        //
                                                        hd.NhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                        hd.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                        hd.PhanLoai = importHopDong.PhanLoaiHDLD;
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString().Trim();
                                                        hd.NgayKy = importHopDong.NgayKy;
                                                        hd.PhanLoaiNguoiKy = importHopDong.PhanLoaiNguoiKy;
                                                        hd.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(importHopDong.ChucVuNguoiKy.Oid);
                                                        hd.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(importHopDong.NguoiKy.Oid);
                                                        hd.HinhThucThanhToan = importHopDong.HinhThucThanhToan;
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_HinhThucHopDong].ToString().RemoveEmpty()) && hd.PhanLoai != HopDongLaoDongEnum.KhongThoiHan)
                                                        {
                                                            HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_HinhThucHopDong].ToString().Trim()));
                                                            if (hinhThucHopDong != null)
                                                                hd.HinhThucHopDong = hinhThucHopDong;
                                                            else
                                                                detailLog.AppendLine(" + Hình thức đồng không hợp lệ:" + item[idx_HinhThucHopDong].ToString());
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_TuNgay].ToString().Trim()))
                                                        {
                                                            try
                                                            {
                                                                hd.TuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + item[idx_TuNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_DenNgay].ToString().Trim()) && hd.PhanLoai != HopDongLaoDongEnum.KhongThoiHan)
                                                        {
                                                            try
                                                            {
                                                                hd.DenNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + item[idx_DenNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        #region Ngạch lương
                                                        if (!item.IsNull(idx_NgachLuong) && !string.IsNullOrEmpty(item[idx_NgachLuong].ToString()))
                                                        {

                                                            NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuong].ToString().Trim()));
                                                            if (ngachLuong != null)
                                                            {
                                                                //
                                                                hd.DieuKhoanHopDong.NgachLuong = ngachLuong;

                                                                //Bậc lương - Hệ số lương
                                                                if (!item.IsNull(idx_BacLuong) && !string.IsNullOrEmpty(item[idx_BacLuong].ToString()))
                                                                {
                                                                    BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? ", ngachLuong, item[idx_BacLuong].ToString().Trim()));
                                                                    if (bacLuong != null)
                                                                    {
                                                                        hd.DieuKhoanHopDong.BacLuong = bacLuong;
                                                                        hd.DieuKhoanHopDong.HeSoLuong = bacLuong.HeSoLuong;
                                                                    }
                                                                    else
                                                                    {
                                                                        detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_BacLuong].ToString());
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_NgachLuong].ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                                        }
                                                        #endregion
                                                        
                                                        //
                                                        hd.GhiChu = item[idx_GhiChu].ToString().Trim();

                                                        //Nếu không phải hợp đồng không thời hạn thì bắt buộc nhập hình thức hợp đồng
                                                        if (hd.PhanLoai != HopDongLaoDongEnum.KhongThoiHan && hd.HinhThucHopDong == null)
                                                        {
                                                            detailLog.AppendLine(" + Hình thức hợp đồng không được trống.");
                                                        }

                                                    }
                                                    else
                                                        detailLog.AppendLine(" + Hợp đồng đã tồn tại: " + item[idx_SoHopDong].ToString() + " - Năm" + quanLyHopDong.NamHoc);
                                                }
                                                #endregion

                                                #region Hợp đồng khoán
                                                else if (importHopDong.LoaiHopDong == TaoHopDongEnum.HopDongKhoan)
                                                {
                                                    HopDong_Khoan hd = uow.FindObject<HopDong_Khoan>(CriteriaOperator.Parse("QuanLyHopDong=? and SoHopDong=?", quanLyHopDong.Oid, item[idx_SoHopDong].ToString().Trim()));
                                                    if (hd == null)
                                                    {
                                                        hd = new HopDong_Khoan(uow);
                                                        //
                                                        hd.QuanLyHopDong = quanLyHopDong;
                                                        //uow.GetObjectByKey<QuanLyHopDong>(quanLyHopDong.Oid);
                                                        //
                                                        hd.NhanVien = uow.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                                        hd.BoPhan = uow.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                                        hd.PhanLoai = importHopDong.PhanLoaiHDK;
                                                        hd.SoHopDong = item[idx_SoHopDong].ToString().Trim();
                                                        hd.NgayKy = importHopDong.NgayKy;
                                                        hd.PhanLoaiNguoiKy = importHopDong.PhanLoaiNguoiKy;
                                                        hd.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(importHopDong.ChucVuNguoiKy.Oid);
                                                        hd.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(importHopDong.NguoiKy.Oid);
                                                        hd.HinhThucThanhToan = importHopDong.HinhThucThanhToan;
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_HinhThucHopDong].ToString().RemoveEmpty()))
                                                        {
                                                            HinhThucHopDong hinhThucHopDong = uow.FindObject<HinhThucHopDong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_HinhThucHopDong].ToString().Trim()));
                                                            if (hinhThucHopDong != null)
                                                                hd.HinhThucHopDong = hinhThucHopDong;
                                                            else
                                                                detailLog.AppendLine(" + Hình thức đồng không hợp lệ:" + item[idx_HinhThucHopDong].ToString());
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_TuNgay].ToString().Trim()))
                                                        {
                                                            try
                                                            {
                                                                hd.TuNgay = Convert.ToDateTime(item[idx_TuNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + item[idx_TuNgay].ToString());
                                                            }
                                                        }
                                                        //
                                                        if (!string.IsNullOrEmpty(item[idx_DenNgay].ToString().Trim()))
                                                        {
                                                            try
                                                            {
                                                                hd.DenNgay = Convert.ToDateTime(item[idx_DenNgay].ToString().Trim());
                                                            }
                                                            catch (Exception ex)
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + item[idx_DenNgay].ToString());
                                                            }
                                                        }

                                                        //
                                                        hd.GhiChu = item[idx_GhiChu].ToString().Trim();
                                                        //Nếu không phải hợp đồng không thời hạn thì bắt buộc nhập hình thức hợp đồng
                                                        if (hd.HinhThucHopDong == null)
                                                        {
                                                            detailLog.AppendLine(" + Hình thức hợp đồng không được trống.");
                                                        }

                                                    }
                                                    else
                                                        detailLog.AppendLine(" + Hợp đồng đã tồn tại: " + item[idx_SoHopDong].ToString() + " - Năm " + quanLyHopDong.NamHoc);
                                                }
                                                #endregion


                                                //Đưa thông tin bị lỗi vào blog
                                                if (detailLog.Length > 0)
                                                {
                                                    mainLog.AppendLine(string.Format("- Không import hợp đồng của cán bộ [{0} - {1}] vào phần mềm được: ", nhanVien.MaQuanLy, nhanVien.HoTen));
                                                    mainLog.AppendLine(detailLog.ToString());
                                                }
                                            }
                                            else
                                            {
                                                mainLog.AppendLine(" + Không tìm thấy mã nhân sự: [" + item[idx_MaQuanLy].ToString() + "] trong hệ thống.");
                                            }

                                        }
                                        if (mainLog.Length > 0)
                                        {
                                            uow.RollbackTransaction();

                                            if (DialogUtil.ShowYesNo("Không thể tạo hợp đồng vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                                            {
                                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                                {
                                                    saveFile.Filter = "Text files (*.txt)|*.txt";

                                                    if (saveFile.ShowDialog() == DialogResult.OK)
                                                    {
                                                        HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                            uow.CommitChanges();
                                            DialogUtil.ShowInfo("Import hợp đồng thành công.");

                                            View.ObjectSpace.Refresh();
                                            obs.Refresh();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}