using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.TuyenDung;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Utils;
using PSC_HRM.Module;
using DevExpress.Xpo;
using System.ComponentModel;
using PSC_HRM.Module.HopDong;
using System.IO;
using DevExpress.ExpressApp.Xpo;
using System.Text;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDongThinhGiang_ImportHopDongThinhGiangController : ViewController
    {
        private IObjectSpace obs;
        private QuanLyHopDongThinhGiang quanLyHopDongThinhGiang;
        private HopDongThinhGiang_ImportHopDongThinhGiang import;
        bool oke;
        int soNhanVienImportLoi;
        int soNhanVienImportThanhCong;

        public HopDongThinhGiang_ImportHopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDongThinhGiang_ImportHopDongThinhGiangController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            soNhanVienImportLoi = 0;
            soNhanVienImportThanhCong = 0;
            obs = Application.CreateObjectSpace();
            quanLyHopDongThinhGiang = View.CurrentObject as QuanLyHopDongThinhGiang;
            if (quanLyHopDongThinhGiang != null)
            {
                import = obs.CreateObject<HopDongThinhGiang_ImportHopDongThinhGiang>();
                e.View = Application.CreateDetailView(obs, import);
                
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();

            quanLyHopDongThinhGiang = View.CurrentObject as QuanLyHopDongThinhGiang;
            if (TruongConfig.MaTruong.Equals("HBU"))
            {
                if (quanLyHopDongThinhGiang != null)
                {
                    if (import.TaoHopDongThinhGiangEnum == TaoHopDongThinhGiangEnum.HopDongThinhGiang)
                    {
                        #region Import Thỉnh giảng
                        using (OpenFileDialog dialog = new OpenFileDialog())
                        {
                            dialog.FileName = "";
                            dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:M]"))
                                {
                                    if (dt != null)
                                    {
                                        using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                        {
                                            uow.BeginTransaction();

                                            HopDong_ThinhGiang hopDongThinhGiang = new HopDong_ThinhGiang(uow);

                                            var mainLog = new StringBuilder();

                                            int soThuTu = 0;
                                            int soHopDong = 1;
                                            int ngayKyHopDong = 2;
                                            int maQuanLy = 3;
                                            int hoTen = 4;
                                            int monDay = 5;
                                            int lopDay = 6;
                                            int taiKhoa = 7;
                                            int soTietTH = 8;
                                            int soTietLT = 9;
                                            int tuNgay = 10;
                                            int denNgay = 11;
                                            int ghiChu = 12;

                                            XPCollection<HopDong_ThinhGiang> listHopDongThinhGiang = new XPCollection<HopDong_ThinhGiang>(uow);
                                            using (DialogUtil.AutoWait())
                                            {
                                                #region vòng lặp foreach
                                                foreach (DataRow item in dt.Rows)
                                                {
                                                    ChiTietHopDongThinhGiang chitiet;
                                                    bool ktra = true;

                                                    #region lấy dữ liệu từ excel
                                                    String soThuTuText = item[soThuTu].ToString();
                                                    String soHopDongText = item[soHopDong].ToString().Trim();
                                                    String ngayKyHopDongText = item[ngayKyHopDong].ToString().Trim();
                                                    String maQuanLyText = item[maQuanLy].ToString().Trim();
                                                    String hoTenText = item[hoTen].ToString().Trim();
                                                    String monDayText = item[monDay].ToString().Trim();
                                                    String taiKhoaText = item[taiKhoa].ToString().Trim();
                                                    String soTietTHText = item[soTietTH].ToString().Trim();
                                                    String soTietLTText = item[soTietLT].ToString().Trim();
                                                    String lopDayText = item[lopDay].ToString().Trim();
                                                    String tuNgayText = item[tuNgay].ToString().Trim();
                                                    String denNgayText = item[denNgay].ToString().Trim();
                                                    String ghiChuText = item[ghiChu].ToString().Trim();
                                                    #endregion

                                                    var errorLog = new StringBuilder();

                                                    hopDongThinhGiang = uow.FindObject<HopDong_ThinhGiang>(CriteriaOperator.Parse("SoHopDong =?", soHopDongText));
                                                    if (hopDongThinhGiang != null)
                                                    {
                                                        mainLog.AppendLine("Số hợp đồng: " + soHopDongText + " đã tồn tại trong hệ thống !");
                                                        oke = false;
                                                    }
                                                    else
                                                    {
                                                        CriteriaOperator filter = CriteriaOperator.Parse("SoHopDong =?", soHopDongText);
                                                        listHopDongThinhGiang.Filter = filter;
                                                        if (listHopDongThinhGiang.Count > 0)
                                                        {
                                                            #region Hợp đồng có rồi nên chỉ thêm chi tiết
                                                            if (!string.IsNullOrEmpty(monDayText))
                                                            {
                                                                hopDongThinhGiang = listHopDongThinhGiang[0];

                                                                foreach (var itm in hopDongThinhGiang.ListChiTietHopDongThinhGiang)
                                                                {
                                                                    if (itm.TaiKhoa != null && itm.MonHoc != null && itm.Lop != null)
                                                                    if (itm.TaiKhoa.TenBoPhan == taiKhoaText
                                                                        && itm.MonHoc == monDayText
                                                                        && itm.Lop == lopDayText)
                                                                    {
                                                                        errorLog.AppendLine(" + Trùng thông tin lớp giảng dạy.");
                                                                        ktra = false;
                                                                    }

                                                                }
                                                                if (ktra == true)
                                                                {

                                                                    chitiet = new ChiTietHopDongThinhGiang(uow);
                                                                    chitiet.HopDongThinhGiang = hopDongThinhGiang;

                                                                    chitiet.MonHoc = monDayText;
                                                                    chitiet.Lop = lopDayText;

                                                                    if (!string.IsNullOrEmpty(taiKhoaText))
                                                                    {
                                                                        BoPhan _BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan = ?", taiKhoaText));
                                                                        if (_BoPhan != null)
                                                                            chitiet.TaiKhoa = _BoPhan;
                                                                        else
                                                                            errorLog.AppendLine(" + Sai thông tin tại khoa.");
                                                                    }

                                                                    if (!string.IsNullOrEmpty(soTietTHText))
                                                                    {
                                                                        try
                                                                        {
                                                                            chitiet.SoTietTH = Convert.ToDecimal(soTietTHText);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            errorLog.AppendLine(" + Số tiết thực hành không hợp lệ:" + soTietTHText);
                                                                        }
                                                                    }

                                                                    if (!string.IsNullOrEmpty(soTietLTText))
                                                                    {
                                                                        try
                                                                        {
                                                                            chitiet.SoTietLT = Convert.ToDecimal(soTietLTText);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            errorLog.AppendLine(" + Số tiết lý thuyết không hợp lệ:" + soTietLTText);
                                                                        }
                                                                    }

                                                                    //Từ ngày
                                                                    if (!string.IsNullOrEmpty(tuNgayText))
                                                                    {
                                                                        try
                                                                        {
                                                                            DateTime TuNgay = Convert.ToDateTime(tuNgayText);
                                                                            if (TuNgay != null && TuNgay != DateTime.MinValue)
                                                                                chitiet.TuNgay = TuNgay;
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            errorLog.AppendLine(" + Từ ngày không hợp lệ:" + tuNgayText);
                                                                        }
                                                                    }
                                                                    //Đến ngày
                                                                    if (!string.IsNullOrEmpty(denNgayText))
                                                                    {
                                                                        try
                                                                        {
                                                                            DateTime DenNgay = Convert.ToDateTime(denNgayText);
                                                                            if (DenNgay != null && DenNgay != DateTime.MinValue)
                                                                                chitiet.DenNgay = DenNgay;
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            errorLog.AppendLine(" + Đến ngày không hợp lệ:" + denNgayText);
                                                                        }
                                                                    }

                                                                    ktra = false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin lớp giảng dạy.");
                                                                ktra = false;
                                                            }
                                                            #endregion
                                                        }

                                                        if (ktra == true)
                                                        {
                                                            #region Thêm hợp đồng
                                                            hopDongThinhGiang = new HopDong_ThinhGiang(uow);
                                                            hopDongThinhGiang.QuanLyHopDongThinhGiang = uow.GetObjectByKey<QuanLyHopDongThinhGiang>(quanLyHopDongThinhGiang.Oid);

                                                            //Số hợp đồng
                                                            if (!string.IsNullOrEmpty(soHopDongText))
                                                                hopDongThinhGiang.SoHopDong = soHopDongText;
                                                            else
                                                                errorLog.AppendLine(" + Thiếu số họp đồng.");

                                                            //Ngày ký
                                                            if (!string.IsNullOrEmpty(ngayKyHopDongText))
                                                            {
                                                                try
                                                                {
                                                                    DateTime NgayKyHopDong = Convert.ToDateTime(ngayKyHopDongText);
                                                                    if (NgayKyHopDong != null && NgayKyHopDong != DateTime.MinValue)
                                                                        hopDongThinhGiang.NgayKy = NgayKyHopDong;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    errorLog.AppendLine(" + Ngày ký hợp đồng không hợp lệ:" + ngayKyHopDongText);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Ngày ký hợp đồng không tìm thấy.");
                                                            }


                                                            //Người lao động
                                                            if (!string.IsNullOrEmpty(maQuanLyText) && !string.IsNullOrEmpty(hoTenText))
                                                            {
                                                                NhanVien NhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaQuanLy =? && HoTen = ?", maQuanLyText, hoTenText));
                                                                if (NhanVien != null)
                                                                {
                                                                    hopDongThinhGiang.NhanVien = uow.GetObjectByKey<NhanVien>(NhanVien.Oid);
                                                                }
                                                                else
                                                                {
                                                                    errorLog.AppendLine(" + Nhân viên không tồn tại trong hệ thống.");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin nhân viên.");
                                                            }

                                                            //Ghi chú
                                                            if (!string.IsNullOrEmpty(ghiChuText))
                                                                hopDongThinhGiang.GhiChu = ghiChuText;

                                                            //Danh sách lớp

                                                            if (!string.IsNullOrEmpty(monDayText))
                                                            {
                                                                chitiet = new ChiTietHopDongThinhGiang(uow);
                                                                chitiet.HopDongThinhGiang = hopDongThinhGiang;
                                                                chitiet.MonHoc = monDayText;
                                                                chitiet.Lop = lopDayText;

                                                                if (!string.IsNullOrEmpty(taiKhoaText))
                                                                {
                                                                    BoPhan _BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan = ?", taiKhoaText));
                                                                    if (_BoPhan != null)
                                                                        chitiet.TaiKhoa = _BoPhan;
                                                                    else
                                                                        errorLog.AppendLine(" + Sai thông tin tại khoa.");
                                                                }

                                                                //Số tiết thực hành
                                                                if (!string.IsNullOrEmpty(soTietTHText))
                                                                    try
                                                                    {
                                                                        chitiet.SoTietTH = Convert.ToDecimal(soTietTHText);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        errorLog.AppendLine(" + Số tiết thực hành không hợp lệ:" + soTietTHText);
                                                                    }

                                                                //Số tiết lý thuyết
                                                                if (!string.IsNullOrEmpty(soTietLTText))
                                                                    try
                                                                    {
                                                                        chitiet.SoTietLT = Convert.ToDecimal(soTietLTText);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        errorLog.AppendLine(" + Số tiết lý thuyết không hợp lệ:" + soTietLTText);
                                                                    }

                                                                //Từ ngày
                                                                if (!string.IsNullOrEmpty(tuNgayText))
                                                                {
                                                                    try
                                                                    {
                                                                        DateTime TuNgay = Convert.ToDateTime(tuNgayText);
                                                                        if (TuNgay != null && TuNgay != DateTime.MinValue)
                                                                            chitiet.TuNgay = TuNgay;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        errorLog.AppendLine(" + Từ ngày không hợp lệ:" + tuNgayText);
                                                                    }
                                                                }
                                                                //Đến ngày
                                                                if (!string.IsNullOrEmpty(denNgayText))
                                                                {
                                                                    try
                                                                    {
                                                                        DateTime DenNgay = Convert.ToDateTime(denNgayText);
                                                                        if (DenNgay != null && DenNgay != DateTime.MinValue)
                                                                            chitiet.DenNgay = DenNgay;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        errorLog.AppendLine(" + Đến ngày không hợp lệ:" + denNgayText);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Sai thông tin lớp giảng dạy.");
                                                            }
                                                            #endregion
                                                        }

                                                        #region Ghi File log
                                                        {
                                                            //Đưa thông tin bị lỗi vào blog
                                                            if (errorLog.Length > 0)
                                                            {
                                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                                mainLog.AppendLine(errorLog.ToString());
                                                                oke = false;
                                                            }
                                                            else
                                                            {
                                                                listHopDongThinhGiang.Add(hopDongThinhGiang);
                                                                oke = true;
                                                            }
                                                        }
                                                        #endregion

                                                    }

                                                    if (oke == false)
                                                    {
                                                        soNhanVienImportLoi++;
                                                    }
                                                    else
                                                    {
                                                        soNhanVienImportThanhCong++;
                                                    }
                                                }

                                                #endregion
                                            }
                                            if (mainLog.Length > 0)
                                            {
                                                uow.RollbackTransaction();
                                                if (DialogUtil.ShowYesNo("Import Thành Công " + soNhanVienImportThanhCong + " hợp đồng - Số hợp đồng Import không thành công " + soNhanVienImportLoi + ". Bạn có muốn xuất thông tin hợp đồng lỗi") == DialogResult.Yes)
                                                {
                                                    string tenFile = "Import_Log.txt";
                                                    //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                                    StreamWriter writer = new StreamWriter(tenFile);
                                                    writer.WriteLine(mainLog.ToString());
                                                    writer.Flush();
                                                    writer.Close();
                                                    writer.Dispose();
                                                    HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                                    System.Diagnostics.Process.Start(tenFile);
                                                }
                                            }
                                            else
                                            {
                                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                                uow.CommitChanges();
                                                //hoàn tất giao tác
                                                //transaction.Complete();
                                                DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả hợp đồng thỉnh giảng !");
                                                View.ObjectSpace.Refresh();
                                                obs.Refresh();
                                            }

                                        }

                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Import Thỉnh giảng chất lượng cao
                        using (OpenFileDialog dialog = new OpenFileDialog())
                        {
                            dialog.FileName = "";
                            dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                using (DialogUtil.AutoWait())
                                {
                                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:I]"))
                                    {
                                        if (dt != null)
                                        {

                                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                            {
                                                uow.BeginTransaction();
                                                HopDong_ThinhGiangChatLuongCao hopDongThinhGiang = new HopDong_ThinhGiangChatLuongCao(uow);

                                                var mainLog = new StringBuilder();
                                                int iTemp;

                                                int soThuTu = 0;
                                                int soHopDong = 1;
                                                int ngayKyHopDong = 2;
                                                int maQuanLy = 3;
                                                int hoTen = 4;
                                                int soTien1Tiet = 5;
                                                int monDay = 6;
                                                int soTiet = 7;
                                                int ghiChu = 8;

                                                XPCollection<HopDong_ThinhGiangChatLuongCao> listHopDongThinhGiang = new XPCollection<HopDong_ThinhGiangChatLuongCao>(uow);

                                                #region vòng lặp foreach
                                                foreach (DataRow item in dt.Rows)
                                                {
                                                    ChiTietHopDongThinhGiangChatLuongCao chitiet;
                                                    bool ktra = true;

                                                    #region lấy dữ liệu từ excel
                                                    String soThuTuText = item[soThuTu].ToString();
                                                    String soHopDongText = item[soHopDong].ToString().Trim();
                                                    String ngayKyHopDongText = item[ngayKyHopDong].ToString().Trim();
                                                    String maQuanLyText = item[maQuanLy].ToString().Trim();
                                                    String hoTenText = item[hoTen].ToString().Trim();
                                                    String soTien1TietText = item[soTien1Tiet].ToString();
                                                    String monDayText = item[monDay].ToString().Trim();
                                                    String soTietText = item[soTiet].ToString().Trim();
                                                    String ghiChuText = item[ghiChu].ToString().Trim();
                                                    #endregion

                                                    var errorLog = new StringBuilder();

                                                    hopDongThinhGiang = uow.FindObject<HopDong_ThinhGiangChatLuongCao>(CriteriaOperator.Parse("SoHopDong =?", soHopDongText));
                                                    if (hopDongThinhGiang != null)
                                                    {
                                                        mainLog.AppendLine("Số hợp đồng: " + soHopDongText + " đã tồn tại trong hệ thống !");
                                                        oke = false;
                                                    }
                                                    else
                                                    {
                                                        CriteriaOperator filter = CriteriaOperator.Parse("SoHopDong =?", soHopDongText);
                                                        listHopDongThinhGiang.Filter = filter;
                                                        if (listHopDongThinhGiang.Count > 0)
                                                        {
                                                            #region Hợp đồng có rồi nên chỉ thêm chi tiết
                                                            if (!string.IsNullOrEmpty(monDayText) &&
                                                                !string.IsNullOrEmpty(soTietText))
                                                            {
                                                                hopDongThinhGiang = listHopDongThinhGiang[0];

                                                                foreach (var itm in hopDongThinhGiang.ListChiTietHopDongThinhGiangChatLuongCao)
                                                                {
                                                                    if (itm.MonHoc == monDayText)
                                                                    {
                                                                        errorLog.AppendLine(" + Trùng thông tin lớp giảng dạy.");
                                                                        ktra = false;
                                                                    }

                                                                }
                                                                if (ktra == true)
                                                                {
                                                                    chitiet = new ChiTietHopDongThinhGiangChatLuongCao(uow);
                                                                    chitiet.HopDongThinhGiangChatLuongCao = hopDongThinhGiang;

                                                                    chitiet.MonHoc = monDayText;

                                                                    chitiet.SoTiet = Convert.ToDecimal(soTietText);

                                                                    ktra = false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin lớp giảng dạy.");
                                                                ktra = false;
                                                            }
                                                            #endregion
                                                        }

                                                        if (ktra == true)
                                                        {
                                                            #region Thêm hợp đồng
                                                            hopDongThinhGiang = new HopDong_ThinhGiangChatLuongCao(uow);
                                                            hopDongThinhGiang.QuanLyHopDongThinhGiang = uow.GetObjectByKey<QuanLyHopDongThinhGiang>(quanLyHopDongThinhGiang.Oid);

                                                            //Số hợp đồng
                                                            if (!string.IsNullOrEmpty(soHopDongText))
                                                                hopDongThinhGiang.SoHopDong = soHopDongText;
                                                            else
                                                                errorLog.AppendLine(" + Thiếu số họp đồng.");

                                                            //Ngày ký
                                                            if (!string.IsNullOrEmpty(ngayKyHopDongText) &&
                                                                int.TryParse(ngayKyHopDongText, out iTemp))
                                                                hopDongThinhGiang.NgayKy = new DateTime(iTemp, 1, 1);


                                                            //Người lao động
                                                            if (!string.IsNullOrEmpty(maQuanLyText) && !string.IsNullOrEmpty(hoTenText))
                                                            {
                                                                GiangVienThinhGiang GiangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaQuanLy =? && HoTen = ?", maQuanLyText, hoTenText));
                                                                if (GiangVienThinhGiang != null)
                                                                {
                                                                    hopDongThinhGiang.NhanVien = GiangVienThinhGiang;

                                                                }
                                                                else
                                                                {
                                                                    errorLog.AppendLine(" + Giảng viên thỉnh giảng không tồn tại trong hệ thống.");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin giảng viên.");
                                                            }

                                                            //Ghi chú
                                                            if (!string.IsNullOrEmpty(ghiChuText))
                                                                hopDongThinhGiang.GhiChu = ghiChuText;

                                                            //Số tiền một tiết
                                                            if (!string.IsNullOrEmpty(soTien1TietText))
                                                                hopDongThinhGiang.SoTien1Tiet = Convert.ToDecimal(soTien1TietText);
                                                            else
                                                                errorLog.AppendLine(" + Thiếu số tiền 1 tiết.");

                                                            //Danh sách lớp

                                                            if (!string.IsNullOrEmpty(monDayText) &&
                                                                !string.IsNullOrEmpty(soTietText))
                                                            {
                                                                chitiet = new ChiTietHopDongThinhGiangChatLuongCao(uow);
                                                                chitiet.HopDongThinhGiangChatLuongCao = hopDongThinhGiang;
                                                                chitiet.MonHoc = monDayText;

                                                                chitiet.SoTiet = Convert.ToDecimal(soTietText);

                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Sai thông tin lớp giảng dạy.");
                                                            }
                                                            #endregion
                                                        }

                                                        #region Ghi File log
                                                        {
                                                            //Đưa thông tin bị lỗi vào blog
                                                            if (errorLog.Length > 0)
                                                            {
                                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                                mainLog.AppendLine(errorLog.ToString());
                                                                oke = false;
                                                            }
                                                            else
                                                            {
                                                                listHopDongThinhGiang.Add(hopDongThinhGiang);
                                                                oke = true;
                                                            }
                                                        }
                                                        #endregion

                                                    }

                                                    if (oke == false)
                                                    {
                                                        soNhanVienImportLoi++;
                                                    }
                                                    else
                                                    {
                                                        soNhanVienImportThanhCong++;
                                                    }
                                                }
                                                #endregion

                                                if (mainLog.Length > 0)
                                                {
                                                    uow.RollbackTransaction();
                                                    if (DialogUtil.ShowYesNo("Import Thành Công " + soNhanVienImportThanhCong + " hợp đồng - Số hợp đồng Import không thành công " + soNhanVienImportLoi + ". Bạn có muốn xuất thông tin hợp đồng lỗi") == DialogResult.Yes)
                                                    {
                                                        string tenFile = "Import_Log.txt";
                                                        //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                                        StreamWriter writer = new StreamWriter(tenFile);
                                                        writer.WriteLine(mainLog.ToString());
                                                        writer.Flush();
                                                        writer.Close();
                                                        writer.Dispose();
                                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                                        System.Diagnostics.Process.Start(tenFile);
                                                    }
                                                }
                                                else
                                                {
                                                    //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                                    uow.CommitChanges();
                                                    //hoàn tất giao tác
                                                    //transaction.Complete();
                                                    DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả hợp đồng thỉnh giảng chất lượng cao !");
                                                    View.ObjectSpace.Refresh();
                                                    obs.Refresh();
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            else if (TruongConfig.MaTruong.Equals("GTVT"))
            {
                if (quanLyHopDongThinhGiang != null)
                {
                    if (import.TaoHopDongThinhGiangEnum == TaoHopDongThinhGiangEnum.HopDongThinhGiang)
                    {
                        #region Import Thỉnh giảng
                        using (OpenFileDialog dialog = new OpenFileDialog())
                        {
                            dialog.FileName = "";
                            dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:M]"))
                                {
                                    if (dt != null)
                                    {
                                        using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                        {
                                            uow.BeginTransaction();

                                            HopDong_ThinhGiang hopDongThinhGiang = new HopDong_ThinhGiang(uow);

                                            var mainLog = new StringBuilder();

                                            int soThuTu = 0;
                                            int soHopDong = 1;
                                            int ngayKyHopDong = 2;
                                            int maQuanLy = 3;
                                            int hoTen = 4;
                                            int monDay = 5;
                                            int lopDay = 6;
                                            int taiKhoa = 7;
                                            int soTietTH = 8;
                                            int soTietLT = 9;
                                            int tuNgay = 10;
                                            int denNgay = 11;
                                            int ghiChu = 12;

                                            XPCollection<HopDong_ThinhGiang> listHopDongThinhGiang = new XPCollection<HopDong_ThinhGiang>(uow);
                                            using (DialogUtil.AutoWait())
                                            {
                                                #region vòng lặp foreach
                                                foreach (DataRow item in dt.Rows)
                                                {
                                                    ChiTietHopDongThinhGiang chitiet;
                                                    bool ktra = true;

                                                    #region lấy dữ liệu từ excel
                                                    String soThuTuText = item[soThuTu].ToString();
                                                    String soHopDongText = item[soHopDong].ToString().Trim();
                                                    String ngayKyHopDongText = item[ngayKyHopDong].ToString().Trim();
                                                    String maQuanLyText = item[maQuanLy].ToString().Trim();
                                                    String hoTenText = item[hoTen].ToString().Trim();
                                                    String monDayText = item[monDay].ToString().Trim();
                                                    String taiKhoaText = item[taiKhoa].ToString().Trim();
                                                    String soTietTHText = item[soTietTH].ToString().Trim();
                                                    String soTietLTText = item[soTietLT].ToString().Trim();
                                                    String lopDayText = item[lopDay].ToString().Trim();
                                                    String tuNgayText = item[tuNgay].ToString().Trim();
                                                    String denNgayText = item[denNgay].ToString().Trim();
                                                    String ghiChuText = item[ghiChu].ToString().Trim();
                                                    #endregion

                                                    var errorLog = new StringBuilder();

                                                    hopDongThinhGiang = uow.FindObject<HopDong_ThinhGiang>(CriteriaOperator.Parse("SoHopDong =?", soHopDongText));
                                                    if (hopDongThinhGiang != null)
                                                    {
                                                        mainLog.AppendLine("Số hợp đồng: " + soHopDongText + " đã tồn tại trong hệ thống !");
                                                        oke = false;
                                                    }
                                                    else
                                                    {
                                                        CriteriaOperator filter = CriteriaOperator.Parse("SoHopDong =?", soHopDongText);
                                                        listHopDongThinhGiang.Filter = filter;
                                                        if (listHopDongThinhGiang.Count > 0)
                                                        {
                                                            #region Hợp đồng có rồi nên chỉ thêm chi tiết
                                                            if (!string.IsNullOrEmpty(monDayText))
                                                            {
                                                                hopDongThinhGiang = listHopDongThinhGiang[0];

                                                                foreach (var itm in hopDongThinhGiang.ListChiTietHopDongThinhGiang)
                                                                {
                                                                    if (itm.TaiKhoa.TenBoPhan == taiKhoaText
                                                                        && itm.MonHoc == monDayText
                                                                        && itm.Lop == lopDayText)
                                                                    {
                                                                        errorLog.AppendLine(" + Trùng thông tin lớp giảng dạy.");
                                                                        ktra = false;
                                                                    }

                                                                }
                                                                if (ktra == true)
                                                                {

                                                                    chitiet = new ChiTietHopDongThinhGiang(uow);
                                                                    chitiet.HopDongThinhGiang = hopDongThinhGiang;

                                                                    chitiet.MonHoc = monDayText;
                                                                    chitiet.Lop = lopDayText;

                                                                    BoPhan _BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan = ?", taiKhoaText));
                                                                    if (_BoPhan != null)
                                                                        chitiet.TaiKhoa = _BoPhan;
                                                                    else
                                                                        errorLog.AppendLine(" + Sai thông tin tại khoa.");

                                                                    if (!string.IsNullOrEmpty(soTietTHText))
                                                                    {
                                                                        try
                                                                        {
                                                                            //chitiet.SoTietTH = Convert.ToDecimal(soTietTHText);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            //errorLog.AppendLine(" + Số tiết thực hành không hợp lệ:" + soTietTHText);
                                                                        }
                                                                    }

                                                                    if (!string.IsNullOrEmpty(soTietLTText))
                                                                    {
                                                                        try
                                                                        {
                                                                            //chitiet.SoTietLT = Convert.ToDecimal(soTietLTText);
                                                                        }
                                                                        catch (Exception ex)
                                                                        {
                                                                            // errorLog.AppendLine(" + Số tiết lý thuyết không hợp lệ:" + soTietLTText);
                                                                        }
                                                                    }

                                                                    ktra = false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                //errorLog.AppendLine(" + Thiếu thông tin lớp giảng dạy.");
                                                                ktra = false;
                                                            }
                                                            #endregion
                                                        }

                                                        if (ktra == true)
                                                        {
                                                            #region Thêm hợp đồng
                                                            hopDongThinhGiang = new HopDong_ThinhGiang(uow);
                                                            hopDongThinhGiang.QuanLyHopDongThinhGiang = uow.GetObjectByKey<QuanLyHopDongThinhGiang>(quanLyHopDongThinhGiang.Oid);

                                                            //Số hợp đồng
                                                            if (!string.IsNullOrEmpty(soHopDongText))
                                                                hopDongThinhGiang.SoHopDong = soHopDongText;
                                                            else
                                                                //errorLog.AppendLine(" + Thiếu số họp đồng.");

                                                                //Ngày ký
                                                                if (!string.IsNullOrEmpty(ngayKyHopDongText))
                                                                {
                                                                    try
                                                                    {
                                                                        DateTime NgayKyHopDong = Convert.ToDateTime(ngayKyHopDongText);
                                                                        if (NgayKyHopDong != null && NgayKyHopDong != DateTime.MinValue)
                                                                            hopDongThinhGiang.NgayKy = NgayKyHopDong;
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        errorLog.AppendLine(" + Ngày ký hợp đồng không hợp lệ:" + ngayKyHopDongText);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    //errorLog.AppendLine(" + Ngày ký hợp đồng không tìm thấy.");
                                                                }


                                                            //Người lao động
                                                            if (!string.IsNullOrEmpty(maQuanLyText) && !string.IsNullOrEmpty(hoTenText))
                                                            {
                                                                GiangVienThinhGiang GiangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaQuanLy =? && HoTen = ?", maQuanLyText, hoTenText));
                                                                if (GiangVienThinhGiang != null)
                                                                {
                                                                    hopDongThinhGiang.NhanVien = uow.GetObjectByKey<GiangVienThinhGiang>(GiangVienThinhGiang.Oid);
                                                                }
                                                                else
                                                                {
                                                                    errorLog.AppendLine(" + Giảng viên thỉnh giảng không tồn tại trong hệ thống.");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin giảng viên.");
                                                            }

                                                            //Ghi chú
                                                            if (!string.IsNullOrEmpty(ghiChuText))
                                                                hopDongThinhGiang.GhiChu = ghiChuText;

                                                            //Từ ngày
                                                            if (!string.IsNullOrEmpty(tuNgayText))
                                                            {
                                                                try
                                                                {
                                                                    DateTime TuNgay = Convert.ToDateTime(tuNgayText);
                                                                    if (TuNgay != null && TuNgay != DateTime.MinValue)
                                                                        hopDongThinhGiang.TuNgay = TuNgay;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    errorLog.AppendLine(" + Từ ngày hợp đồng không hợp lệ:" + tuNgayText);
                                                                }
                                                            }
                                                            //Đến ngày
                                                            if (!string.IsNullOrEmpty(denNgayText))
                                                            {
                                                                try
                                                                {
                                                                    DateTime DenNgay = Convert.ToDateTime(denNgayText);
                                                                    if (DenNgay != null && DenNgay != DateTime.MinValue)
                                                                        hopDongThinhGiang.DenNgay = DenNgay;
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    errorLog.AppendLine(" + Đến ngày hợp đồng không hợp lệ:" + denNgayText);
                                                                }
                                                            }

                                                            //Danh sách lớp

                                                            //if (!string.IsNullOrEmpty(monDayText))
                                                            {
                                                                chitiet = new ChiTietHopDongThinhGiang(uow);
                                                                chitiet.HopDongThinhGiang = hopDongThinhGiang;
                                                                chitiet.MonHoc = monDayText;
                                                                chitiet.Lop = lopDayText;

                                                                BoPhan _BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan = ?", taiKhoaText));
                                                                if (_BoPhan != null)
                                                                    chitiet.TaiKhoa = _BoPhan;
                                                                else
                                                                    errorLog.AppendLine(" + Sai thông tin tại khoa.");

                                                                //Số tiết thực hành
                                                                if (!string.IsNullOrEmpty(soTietTHText))
                                                                    try
                                                                    {
                                                                        chitiet.SoTietTH = Convert.ToDecimal(soTietTHText);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        //errorLog.AppendLine(" + Số tiết thực hành không hợp lệ:" + soTietTHText);
                                                                    }

                                                                //Số tiết lý thuyết
                                                                if (!string.IsNullOrEmpty(soTietLTText))
                                                                    try
                                                                    {
                                                                        hopDongThinhGiang.SoTien1Tiet = Convert.ToDecimal(soTietLTText);
                                                                        //chitiet.SoTietLT = Convert.ToDecimal(soTietLTText);
                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        //errorLog.AppendLine(" + Số tiết lý thuyết không hợp lệ:" + soTietLTText);
                                                                    }
                                                            }
                                                            //else
                                                            {
                                                                //errorLog.AppendLine(" + Sai thông tin lớp giảng dạy.");
                                                            }
                                                            #endregion
                                                        }

                                                        #region Ghi File log
                                                        {
                                                            //Đưa thông tin bị lỗi vào blog
                                                            if (errorLog.Length > 0)
                                                            {
                                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                                mainLog.AppendLine(errorLog.ToString());
                                                                oke = false;
                                                            }
                                                            else
                                                            {
                                                                listHopDongThinhGiang.Add(hopDongThinhGiang);
                                                                oke = true;
                                                            }
                                                        }
                                                        #endregion

                                                    }

                                                    if (oke == false)
                                                    {
                                                        soNhanVienImportLoi++;
                                                    }
                                                    else
                                                    {
                                                        soNhanVienImportThanhCong++;
                                                    }
                                                }

                                                #endregion
                                            }
                                            if (mainLog.Length > 0)
                                            {
                                                uow.RollbackTransaction();
                                                if (DialogUtil.ShowYesNo("Import Thành Công " + soNhanVienImportThanhCong + " hợp đồng - Số hợp đồng Import không thành công " + soNhanVienImportLoi + ". Bạn có muốn xuất thông tin hợp đồng lỗi") == DialogResult.Yes)
                                                {
                                                    string tenFile = "Import_Log.txt";
                                                    //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                                    StreamWriter writer = new StreamWriter(tenFile);
                                                    writer.WriteLine(mainLog.ToString());
                                                    writer.Flush();
                                                    writer.Close();
                                                    writer.Dispose();
                                                    HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                                    System.Diagnostics.Process.Start(tenFile);
                                                }
                                            }
                                            else
                                            {
                                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                                uow.CommitChanges();
                                                //hoàn tất giao tác
                                                //transaction.Complete();
                                                DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả hợp đồng thỉnh giảng !");
                                                View.ObjectSpace.Refresh();
                                                obs.Refresh();
                                            }

                                        }

                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region Import Thỉnh giảng chất lượng cao
                        using (OpenFileDialog dialog = new OpenFileDialog())
                        {
                            dialog.FileName = "";
                            dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                using (DialogUtil.AutoWait())
                                {
                                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:I]"))
                                    {
                                        if (dt != null)
                                        {

                                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                            {
                                                uow.BeginTransaction();
                                                HopDong_ThinhGiangChatLuongCao hopDongThinhGiang = new HopDong_ThinhGiangChatLuongCao(uow);

                                                var mainLog = new StringBuilder();
                                                int iTemp;

                                                int soThuTu = 0;
                                                int soHopDong = 1;
                                                int ngayKyHopDong = 2;
                                                int maQuanLy = 3;
                                                int hoTen = 4;
                                                int soTien1Tiet = 5;
                                                int monDay = 6;
                                                int soTiet = 7;
                                                int ghiChu = 8;

                                                XPCollection<HopDong_ThinhGiangChatLuongCao> listHopDongThinhGiang = new XPCollection<HopDong_ThinhGiangChatLuongCao>(uow);

                                                #region vòng lặp foreach
                                                foreach (DataRow item in dt.Rows)
                                                {
                                                    ChiTietHopDongThinhGiangChatLuongCao chitiet;
                                                    bool ktra = true;

                                                    #region lấy dữ liệu từ excel
                                                    String soThuTuText = item[soThuTu].ToString();
                                                    String soHopDongText = item[soHopDong].ToString().Trim();
                                                    String ngayKyHopDongText = item[ngayKyHopDong].ToString().Trim();
                                                    String maQuanLyText = item[maQuanLy].ToString().Trim();
                                                    String hoTenText = item[hoTen].ToString().Trim();
                                                    String soTien1TietText = item[soTien1Tiet].ToString();
                                                    String monDayText = item[monDay].ToString().Trim();
                                                    String soTietText = item[soTiet].ToString().Trim();
                                                    String ghiChuText = item[ghiChu].ToString().Trim();
                                                    #endregion

                                                    var errorLog = new StringBuilder();

                                                    hopDongThinhGiang = uow.FindObject<HopDong_ThinhGiangChatLuongCao>(CriteriaOperator.Parse("SoHopDong =?", soHopDongText));
                                                    if (hopDongThinhGiang != null)
                                                    {
                                                        mainLog.AppendLine("Số hợp đồng: " + soHopDongText + " đã tồn tại trong hệ thống !");
                                                        oke = false;
                                                    }
                                                    else
                                                    {
                                                        CriteriaOperator filter = CriteriaOperator.Parse("SoHopDong =?", soHopDongText);
                                                        listHopDongThinhGiang.Filter = filter;
                                                        if (listHopDongThinhGiang.Count > 0)
                                                        {
                                                            #region Hợp đồng có rồi nên chỉ thêm chi tiết
                                                            if (!string.IsNullOrEmpty(monDayText) &&
                                                                !string.IsNullOrEmpty(soTietText))
                                                            {
                                                                hopDongThinhGiang = listHopDongThinhGiang[0];

                                                                foreach (var itm in hopDongThinhGiang.ListChiTietHopDongThinhGiangChatLuongCao)
                                                                {
                                                                    if (itm.MonHoc == monDayText)
                                                                    {
                                                                        errorLog.AppendLine(" + Trùng thông tin lớp giảng dạy.");
                                                                        ktra = false;
                                                                    }

                                                                }
                                                                if (ktra == true)
                                                                {
                                                                    chitiet = new ChiTietHopDongThinhGiangChatLuongCao(uow);
                                                                    chitiet.HopDongThinhGiangChatLuongCao = hopDongThinhGiang;

                                                                    chitiet.MonHoc = monDayText;

                                                                    chitiet.SoTiet = Convert.ToDecimal(soTietText);

                                                                    ktra = false;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin lớp giảng dạy.");
                                                                ktra = false;
                                                            }
                                                            #endregion
                                                        }

                                                        if (ktra == true)
                                                        {
                                                            #region Thêm hợp đồng
                                                            hopDongThinhGiang = new HopDong_ThinhGiangChatLuongCao(uow);
                                                            hopDongThinhGiang.QuanLyHopDongThinhGiang = uow.GetObjectByKey<QuanLyHopDongThinhGiang>(quanLyHopDongThinhGiang.Oid);

                                                            //Số hợp đồng
                                                            if (!string.IsNullOrEmpty(soHopDongText))
                                                                hopDongThinhGiang.SoHopDong = soHopDongText;
                                                            else
                                                                errorLog.AppendLine(" + Thiếu số họp đồng.");

                                                            //Ngày ký
                                                            if (!string.IsNullOrEmpty(ngayKyHopDongText) &&
                                                                int.TryParse(ngayKyHopDongText, out iTemp))
                                                                hopDongThinhGiang.NgayKy = new DateTime(iTemp, 1, 1);


                                                            //Người lao động
                                                            if (!string.IsNullOrEmpty(maQuanLyText) && !string.IsNullOrEmpty(hoTenText))
                                                            {
                                                                GiangVienThinhGiang GiangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaQuanLy =? && HoTen = ?", maQuanLyText, hoTenText));
                                                                if (GiangVienThinhGiang != null)
                                                                {
                                                                    hopDongThinhGiang.NhanVien = GiangVienThinhGiang;

                                                                }
                                                                else
                                                                {
                                                                    errorLog.AppendLine(" + Giảng viên thỉnh giảng không tồn tại trong hệ thống.");
                                                                }

                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Thiếu thông tin giảng viên.");
                                                            }

                                                            //Ghi chú
                                                            if (!string.IsNullOrEmpty(ghiChuText))
                                                                hopDongThinhGiang.GhiChu = ghiChuText;

                                                            //Số tiền một tiết
                                                            if (!string.IsNullOrEmpty(soTien1TietText))
                                                                hopDongThinhGiang.SoTien1Tiet = Convert.ToDecimal(soTien1TietText);
                                                            else
                                                                errorLog.AppendLine(" + Thiếu số tiền 1 tiết.");

                                                            //Danh sách lớp

                                                            if (!string.IsNullOrEmpty(monDayText) &&
                                                                !string.IsNullOrEmpty(soTietText))
                                                            {
                                                                chitiet = new ChiTietHopDongThinhGiangChatLuongCao(uow);
                                                                chitiet.HopDongThinhGiangChatLuongCao = hopDongThinhGiang;
                                                                chitiet.MonHoc = monDayText;

                                                                chitiet.SoTiet = Convert.ToDecimal(soTietText);

                                                            }
                                                            else
                                                            {
                                                                errorLog.AppendLine(" + Sai thông tin lớp giảng dạy.");
                                                            }
                                                            #endregion
                                                        }

                                                        #region Ghi File log
                                                        {
                                                            //Đưa thông tin bị lỗi vào blog
                                                            if (errorLog.Length > 0)
                                                            {
                                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                                mainLog.AppendLine(errorLog.ToString());
                                                                oke = false;
                                                            }
                                                            else
                                                            {
                                                                listHopDongThinhGiang.Add(hopDongThinhGiang);
                                                                oke = true;
                                                            }
                                                        }
                                                        #endregion

                                                    }

                                                    if (oke == false)
                                                    {
                                                        soNhanVienImportLoi++;
                                                    }
                                                    else
                                                    {
                                                        soNhanVienImportThanhCong++;
                                                    }
                                                }
                                                #endregion

                                                if (mainLog.Length > 0)
                                                {
                                                    uow.RollbackTransaction();
                                                    if (DialogUtil.ShowYesNo("Import Thành Công " + soNhanVienImportThanhCong + " hợp đồng - Số hợp đồng Import không thành công " + soNhanVienImportLoi + ". Bạn có muốn xuất thông tin hợp đồng lỗi") == DialogResult.Yes)
                                                    {
                                                        string tenFile = "Import_Log.txt";
                                                        //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                                        StreamWriter writer = new StreamWriter(tenFile);
                                                        writer.WriteLine(mainLog.ToString());
                                                        writer.Flush();
                                                        writer.Close();
                                                        writer.Dispose();
                                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                                        System.Diagnostics.Process.Start(tenFile);
                                                    }
                                                }
                                                else
                                                {
                                                    //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                                    uow.CommitChanges();
                                                    //hoàn tất giao tác
                                                    //transaction.Complete();
                                                    DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả hợp đồng thỉnh giảng chất lượng cao !");
                                                    View.ObjectSpace.Refresh();
                                                    obs.Refresh();
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("HBU") || TruongConfig.MaTruong.Equals("GTVT"))
            {
                popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyHopDongThinhGiang>() &&
                                    HamDungChung.IsWriteGranted<HopDong_ThinhGiang>();
            }
            else
            {
                popupWindowShowAction1.Active["TruyCap"] = false;
            }
        }
    }
}
