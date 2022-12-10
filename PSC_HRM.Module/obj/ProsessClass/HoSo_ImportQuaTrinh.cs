using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuaTrinh;

namespace PSC_HRM.Module.Controllers
{
    public class HoSo_ImportQuaTrinh
    {
        public static void XuLy_ImportLichSuBanThan(IObjectSpace obs, string fileName)
        {           
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AE]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            detailLog = new StringBuilder();
                            LichSuBanThan _lichSuBanThan = null;

                            int idx_TuNam = 0;
                            int idx_DenNam = 1;
                            int idx_MaQuanLy = 2;
                            int idx_HoTen = 3;
                            int idx_NoiDung = 4;
                           
                            if (_lichSuBanThan == null)
                            {
                                _lichSuBanThan = new LichSuBanThan(uow);

                                #region Từ năm
                                {
                                    String tuNamText = dr[idx_TuNam].ToString();
                                    if (!string.IsNullOrEmpty(tuNamText))
                                    {
                                        _lichSuBanThan.TuNam = tuNamText;
                                    }                                   
                                }
                                #endregion

                                #region Đến năm
                                {
                                    String denNamText = dr[idx_DenNam].ToString();
                                    if (!string.IsNullOrEmpty(denNamText))
                                    {
                                        _lichSuBanThan.DenNam = denNamText;
                                    } 
                                }
                                #endregion

                                #region Thông tin nhân viên
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    HoSo.HoSo nhanVien = uow.FindObject<HoSo.HoSo>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        _lichSuBanThan.HoSo = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion

                                #region Nội dung
                                {
                                    String noiDungText = dr[idx_NoiDung].ToString();
                                    if (!string.IsNullOrEmpty(noiDungText))
                                    {
                                        _lichSuBanThan.NoiDung = noiDungText;
                                    } 
                                }
                                #endregion
                            }
                           
                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Lịch sử bản thân của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportDienBienLuong(IObjectSpace obs, string fileName)
        {         
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AL]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                 

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            DienBienLuong _dienBienLuong = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_TuNgay = 2;
                            int idx_DenNgay = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;
                            int idx_HeSoLuong = 6;
                            int idx_HSPCVuotKhung = 7;
                            int idx_HSPCChucVu = 8;
                            int idx_HSPCThamNien = 9;
                            int idx_HSPCUuDai = 10;
                            int idx_LyDo = 11;                       

                            if (_dienBienLuong == null)
                            {
                                _dienBienLuong = new DienBienLuong(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        _dienBienLuong.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            _dienBienLuong.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Từ ngày
                                {
                                    String tuNgayText = dr[idx_TuNgay].ToString();
                                    if (!string.IsNullOrEmpty(tuNgayText))
                                    {
                                        try
                                        {
                                            _dienBienLuong.TuNgay = Convert.ToDateTime(tuNgayText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgayText);
                                        }
                                    }
                                   
                                }
                                #endregion

                                #region Đến ngày
                                {
                                    String denNgayText = dr[idx_DenNgay].ToString();
                                    if (!string.IsNullOrEmpty(denNgayText))
                                    {
                                        try
                                        {
                                            _dienBienLuong.DenNgay = Convert.ToDateTime(denNgayText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                        }
                                    }
                                }
                                #endregion

                                #region Mã quản lý
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        _dienBienLuong.ThongTinNhanVien = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion

                                #region Hệ số lương
                                {
                                    String heSoLuongText = dr[idx_HeSoLuong].ToString();
                                    if (!string.IsNullOrEmpty(heSoLuongText))
                                    {
                                        _dienBienLuong.HeSoLuong = Convert.ToDecimal(heSoLuongText);
                                    }
                                }
                                #endregion

                                #region HSPC Vượt khung
                                {
                                    String hspcVuotKhungText = dr[idx_HSPCVuotKhung].ToString();
                                    if (!string.IsNullOrEmpty(hspcVuotKhungText))
                                    {
                                        _dienBienLuong.HSPCVuotKhung = Convert.ToDecimal(hspcVuotKhungText);
                                    }
                                }
                                #endregion

                                #region HSPC Chức vụ
                                {
                                    String hspcChucVuText = dr[idx_HSPCChucVu].ToString();
                                    if (!string.IsNullOrEmpty(hspcChucVuText))
                                    {
                                        _dienBienLuong.HSPCChucVu = Convert.ToDecimal(hspcChucVuText);
                                    }
                                }
                                #endregion

                                #region HSPC Thâm niên
                                {
                                    String hspcThanNienText = dr[idx_HSPCThamNien].ToString();
                                    if (!string.IsNullOrEmpty(hspcThanNienText))
                                    {
                                        _dienBienLuong.HSPCThamNien = Convert.ToDecimal(hspcThanNienText);
                                    }
                                }
                                #endregion

                                #region HSPC Ưu đãi
                                {
                                    String hspcUuDaiText = dr[idx_HSPCUuDai].ToString();
                                    if (!string.IsNullOrEmpty(hspcUuDaiText))
                                    {
                                        _dienBienLuong.HSPCUuDai = Convert.ToDecimal(hspcUuDaiText);
                                    }
                                }
                                #endregion

                                #region Lý do
                                {
                                    String lyDoText = dr[idx_LyDo].ToString();
                                    if (!string.IsNullOrEmpty(lyDoText))
                                    {
                                        _dienBienLuong.LyDo = lyDoText;
                                    }
                                }
                                #endregion
                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Diễn biến lương của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportQuaTrinhBoNhiem(IObjectSpace obs, string fileName)
        {           
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AJ]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                  

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QuaTrinhBoNhiem _quaTrinhBoNhiem = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_TuNam = 2;
                            int idx_DenNam = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;
                            int idx_ChucVu = 6;
                            int idx_HSPCChucVu = 7;
                            int idx_NgayHuongHeSo = 8;                        

                            if (_quaTrinhBoNhiem == null)
                            {
                                _quaTrinhBoNhiem = new QuaTrinhBoNhiem(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        _quaTrinhBoNhiem.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            _quaTrinhBoNhiem.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Từ năm
                                {
                                    String tuNamText = dr[idx_TuNam].ToString();
                                    if (!string.IsNullOrEmpty(tuNamText))
                                    {
                                        _quaTrinhBoNhiem.TuNam = tuNamText;                                         
                                    }
                                }
                                #endregion

                                #region Đến năm
                                {
                                    String denNamText = dr[idx_DenNam].ToString();
                                    if (!string.IsNullOrEmpty(denNamText))
                                    {
                                         _quaTrinhBoNhiem.DenNam = denNamText;                                        
                                    }
                                }
                                #endregion

                                #region Mã quản lý
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        _quaTrinhBoNhiem.ThongTinNhanVien = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion

                                #region Chức vụ
                                {
                                    String chucVuText = dr[idx_ChucVu].ToString();
                                    if (!string.IsNullOrEmpty(chucVuText))
                                    {
                                       ChucVu chucVu = null;
                                       chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", chucVuText));
                                       if (chucVu == null)
                                       {
                                           chucVu = new ChucVu(uow);
                                           chucVu.TenChucVu = chucVuText;
                                           chucVu.MaQuanLy = Guid.NewGuid().ToString();
                                       }                    
                                        _quaTrinhBoNhiem.ChucVu = chucVu;
                                    }
                                }
                                #endregion                              

                                #region HSPC Chức vụ
                                {
                                    String hspcChucVuText = dr[idx_HSPCChucVu].ToString();
                                    if (!string.IsNullOrEmpty(hspcChucVuText))
                                    {
                                        _quaTrinhBoNhiem.HeSoPhuCapChucVu = Convert.ToDecimal(hspcChucVuText);
                                    }
                                }
                                #endregion

                                #region Ngày hưởng hệ số
                                {
                                    String ngayHuongHeSoText = dr[idx_NgayHuongHeSo].ToString();
                                    if (!string.IsNullOrEmpty(ngayHuongHeSoText))
                                    {
                                        try
                                        {
                                            _quaTrinhBoNhiem.NgayHuongHeSo = Convert.ToDateTime(ngayHuongHeSoText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng hệ số không hợp lệ: " + ngayHuongHeSoText);
                                        }
                                    }
                                }
                                #endregion               
                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Quá trình bổ nhiệm của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportQuaTrinhCongTac(IObjectSpace obs, string fileName)
        {          
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AG]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QuaTrinhCongTac _quaTrinhCongTac = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_TuNam = 2;
                            int idx_DenNam = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;                           
                            int idx_NoiDung = 6;                    

                            if (_quaTrinhCongTac == null)
                            {
                                _quaTrinhCongTac = new QuaTrinhCongTac(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        _quaTrinhCongTac.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            _quaTrinhCongTac.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Từ năm
                                {
                                    String tuNamText = dr[idx_TuNam].ToString();
                                    if (!string.IsNullOrEmpty(tuNamText))
                                    {
                                        _quaTrinhCongTac.TuNam = tuNamText;
                                    }
                                }
                                #endregion

                                #region Đến năm
                                {
                                    String denNamText = dr[idx_DenNam].ToString();
                                    if (!string.IsNullOrEmpty(denNamText))
                                    {
                                        _quaTrinhCongTac.DenNam = denNamText;
                                    }
                                }
                                #endregion

                                #region Mã quản lý
                                {
                                    
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    HoSo.HoSo nhanVien = uow.FindObject<HoSo.HoSo>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        _quaTrinhCongTac.HoSo = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion                               

                                #region Nội dung
                                {
                                    String noiDungText = dr[idx_NoiDung].ToString();
                                    if (!string.IsNullOrEmpty(noiDungText))
                                    {
                                        _quaTrinhCongTac.NoiDung = noiDungText;
                                    }
                                }
                                #endregion                             
                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Quá trình công tác của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportQuaTrinhBoiDuong(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AI]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QuaTrinhBoiDuong quaTrinhBoiDuong = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_TuNgay = 2;
                            int idx_DenNgay = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;
                            int idx_NoiDung = 6;
                            int idx_NoiBoiDuong = 7;
                            int idx_HinhThucBoiDuong = 8;

                            if (quaTrinhBoiDuong == null)
                            {
                                quaTrinhBoiDuong = new QuaTrinhBoiDuong(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        quaTrinhBoiDuong.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            quaTrinhBoiDuong.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Từ ngày
                                {
                                    String tuNgayText = dr[idx_TuNgay].ToString();
                                    if (!string.IsNullOrEmpty(tuNgayText))
                                    {
                                        quaTrinhBoiDuong.TuNgay = tuNgayText;
                                    }
                                }
                                #endregion

                                #region Đến ngày
                                {
                                    String denNgayText = dr[idx_DenNgay].ToString();
                                    if (!string.IsNullOrEmpty(denNgayText))
                                    {
                                        quaTrinhBoiDuong.DenNgay = denNgayText;
                                    }
                                }
                                #endregion

                                #region Mã quản lý
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        quaTrinhBoiDuong.ThongTinNhanVien = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion

                                #region Nội dung
                                {
                                    String noiDungText = dr[idx_NoiDung].ToString();
                                    if (!string.IsNullOrEmpty(noiDungText))
                                    {
                                        quaTrinhBoiDuong.NoiDungBoiDuong = noiDungText;
                                    }
                                }
                                #endregion

                                #region Nơi bồi dưỡng
                                {
                                    String noiBoiDuongText = dr[idx_NoiBoiDuong].ToString();
                                    if (!string.IsNullOrEmpty(noiBoiDuongText))
                                    {
                                        quaTrinhBoiDuong.NoiBoiDuong = noiBoiDuongText;
                                    }
                                }
                                #endregion

                                #region Hình thức bồi dưỡng
                                {
                                    LoaiHinhBoiDuong loaiHinhBoiDuong = null;
                                    String loaiHinhBoiDuongText = dr[idx_HinhThucBoiDuong].ToString();
                                    if (!string.IsNullOrEmpty(loaiHinhBoiDuongText))
                                    {
                                        loaiHinhBoiDuong = uow.FindObject<LoaiHinhBoiDuong>(CriteriaOperator.Parse("TenLoaiHinhBoiDuong like ?", loaiHinhBoiDuongText));
                                        if (loaiHinhBoiDuong == null)
                                        {
                                            loaiHinhBoiDuong = new LoaiHinhBoiDuong(uow);
                                            loaiHinhBoiDuong.TenLoaiHinhBoiDuong = loaiHinhBoiDuongText;
                                        }

                                        quaTrinhBoiDuong.LoaiHinhBoiDuong = loaiHinhBoiDuong;
                                    }
                                }
                                #endregion

                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Quá trình bồi dưỡng của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportQuaTrinhDaoTao(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AJ]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QuaTrinhDaoTao quaTrinhDaoTao = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_NamNhapHoc = 2;
                            int idx_NamTotNghiep = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;
                            int idx_HinhThucDaoTao = 6;
                            int idx_TrinhDoChuyenMon = 7;
                            int idx_ChuyenNganhDaoTao = 8;
                            int idx_TruongDaoTao = 9;

                            if (quaTrinhDaoTao == null)
                            {
                                quaTrinhDaoTao = new QuaTrinhDaoTao(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        quaTrinhDaoTao.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            quaTrinhDaoTao.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Năm nhập học
                                {
                                    String namNhapHocText = dr[idx_NamNhapHoc].ToString();
                                    if (!string.IsNullOrEmpty(namNhapHocText))
                                    {
                                        quaTrinhDaoTao.NamNhapHoc = Convert.ToInt16(namNhapHocText);
                                    }
                                }
                                #endregion

                                #region Năm tốt nghiệp
                                {
                                    String namTotNghiepText = dr[idx_NamTotNghiep].ToString();
                                    if (!string.IsNullOrEmpty(namTotNghiepText))
                                    {
                                        quaTrinhDaoTao.NamTotNghiep = Convert.ToInt16(namTotNghiepText);
                                    }
                                }
                                #endregion

                                #region Mã quản lý
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        quaTrinhDaoTao.ThongTinNhanVien = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion

                                #region Hình thức đào tạo
                                {
                                    HinhThucDaoTao hinhThucDaoTao = null;
                                    String hinhThucDaoTaoText = dr[idx_HinhThucDaoTao].ToString();
                                    if (!string.IsNullOrEmpty(hinhThucDaoTaoText))
                                    {
                                        hinhThucDaoTao = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", hinhThucDaoTaoText));
                                        if (hinhThucDaoTao == null)
                                        {
                                            hinhThucDaoTao = new HinhThucDaoTao(uow);
                                            hinhThucDaoTao.TenHinhThucDaoTao = hinhThucDaoTaoText;
                                        }

                                        quaTrinhDaoTao.HinhThucDaoTao = hinhThucDaoTao;
                                    }
                                }
                                #endregion

                                #region Bằng cấp - Trình độ chuyên môn
                                {
                                    TrinhDoChuyenMon trinhDoChuyenMon = null;
                                    String trinhDoChuyenMonText = dr[idx_TrinhDoChuyenMon].ToString();
                                    if (!string.IsNullOrEmpty(trinhDoChuyenMonText))
                                    {
                                        trinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", trinhDoChuyenMonText));
                                        if (trinhDoChuyenMon == null)
                                        {
                                            trinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                            trinhDoChuyenMon.TenTrinhDoChuyenMon = trinhDoChuyenMonText;                                           
                                        }

                                        quaTrinhDaoTao.BangCap = trinhDoChuyenMon;
                                    }
                                }
                                #endregion

                                #region Chuyên ngành đào tạo
                                {
                                    ChuyenMonDaoTao chuyenMonDaoTao = null;
                                    String chuyenMonDaoTaoText = dr[idx_ChuyenNganhDaoTao].ToString();
                                    if (!string.IsNullOrEmpty(chuyenMonDaoTaoText))
                                    {
                                        chuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoText));
                                        if (chuyenMonDaoTao == null)
                                        {
                                            chuyenMonDaoTao = new ChuyenMonDaoTao(uow);
                                            chuyenMonDaoTao.TenChuyenMonDaoTao = chuyenMonDaoTaoText;                                           
                                        }
                                        quaTrinhDaoTao.ChuyenMonDaoTao = chuyenMonDaoTao;
                                    }
                                }
                                #endregion

                                #region Trường đào tạo
                                {
                                    TruongDaoTao truongDaoTao = null;
                                    String truongDaoTaoText = dr[idx_TruongDaoTao].ToString();
                                    if (!string.IsNullOrEmpty(truongDaoTaoText))
                                    {
                                        truongDaoTao = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", truongDaoTaoText));
                                        if (truongDaoTao == null)
                                        {
                                            truongDaoTao = new TruongDaoTao(uow);
                                            truongDaoTao.TenTruongDaoTao = truongDaoTaoText;
                                        }
                                        quaTrinhDaoTao.TruongDaoTao = truongDaoTao;
                                    }
                                }
                                #endregion
                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Quá trình đào tạo của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportQuaTrinhKhenThuong(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AI]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QuaTrinhKhenThuong quaTrinhKhenThuong = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_TuNgay = 2;
                            int idx_DenNgay = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;
                            int idx_DanhHieu = 6;
                            int idx_NgayKhenThuong = 7;
                            int idx_LyDo = 8;                           

                            if (quaTrinhKhenThuong == null)
                            {
                                quaTrinhKhenThuong = new QuaTrinhKhenThuong(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        quaTrinhKhenThuong.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            quaTrinhKhenThuong.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Từ ngày
                                {
                                    String tuNgayText = dr[idx_TuNgay].ToString();
                                    if (!string.IsNullOrEmpty(tuNgayText))
                                    {
                                        try
                                        {
                                            quaTrinhKhenThuong.TuNgay = Convert.ToDateTime(tuNgayText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày bắt đầu không hợp lệ: " + tuNgayText);
                                        }
                                    }
                                }
                                #endregion

                                #region Đến ngày
                                {
                                    String denNgayText = dr[idx_DenNgay].ToString();
                                    if (!string.IsNullOrEmpty(denNgayText))
                                    {
                                        try
                                        {
                                            quaTrinhKhenThuong.DenNgay = Convert.ToDateTime(denNgayText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày kết thúc không hợp lệ: " + denNgayText);
                                        }
                                    }
                                }
                                #endregion                             

                                #region Mã quản lý
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    HoSo.HoSo nhanVien = uow.FindObject<HoSo.HoSo>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        quaTrinhKhenThuong.HoSo = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion                             

                                #region Danh hiệu khen thưởng
                                {
                                    DanhHieuKhenThuong danhHieuKhenThuong = null;
                                    String danhHieuText = dr[idx_DanhHieu].ToString();
                                    if (!string.IsNullOrEmpty(danhHieuText))
                                    {
                                        danhHieuKhenThuong = uow.FindObject<DanhHieuKhenThuong>(CriteriaOperator.Parse("TenDanhHieu like ?", danhHieuText));
                                        if (danhHieuKhenThuong == null)
                                        {
                                            danhHieuKhenThuong = new DanhHieuKhenThuong(uow);
                                            danhHieuKhenThuong.TenDanhHieu = danhHieuText;
                                        }
                                        quaTrinhKhenThuong.DanhHieuKhenThuong = danhHieuKhenThuong;
                                    }
                                }
                                #endregion

                                #region Ngày khen thưởng
                                {
                                    String ngayKhenThuongText = dr[idx_NgayKhenThuong].ToString();
                                    if (!string.IsNullOrEmpty(ngayKhenThuongText))
                                    {
                                        try
                                        {
                                            quaTrinhKhenThuong.NgayKhenThuong = Convert.ToDateTime(ngayKhenThuongText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày khen thưởng không hợp lệ: " + ngayKhenThuongText);
                                        }
                                    }

                                }
                                #endregion

                                #region Lý do
                                {                                  
                                    String lyDoText = dr[idx_LyDo].ToString();
                                    if (!string.IsNullOrEmpty(lyDoText))
                                    {
                                        quaTrinhKhenThuong.LyDo = lyDoText;
                                    }
                                }
                                #endregion
                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Quá trình khen thưởng của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }

        public static void XuLy_ImportQuaTrinhKyLuat(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AH]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();                    

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            QuaTrinhKyLuat quaTrinhKyLuat = null;
                            detailLog = new StringBuilder();

                            int idx_QuyetDinh = 0;
                            int idx_NgayQuyetDinh = 1;
                            int idx_TuNgay = 2;
                            int idx_DenNgay = 3;
                            int idx_MaQuanLy = 4;
                            int idx_HoTen = 5;
                            int idx_HinhThuc = 6;
                            int idx_LyDo = 7;

                            if (quaTrinhKyLuat == null)
                            {
                                quaTrinhKyLuat = new QuaTrinhKyLuat(uow);

                                #region Số quyết định
                                {
                                    String soQuyetDinhText = dr[idx_QuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(soQuyetDinhText))
                                    {
                                        quaTrinhKyLuat.SoQuyetDinh = soQuyetDinhText;
                                    }
                                }
                                #endregion

                                #region Ngày quyết định
                                {
                                    String ngayQuyetDinhText = dr[idx_NgayQuyetDinh].ToString();
                                    if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                    {
                                        try
                                        {
                                            quaTrinhKyLuat.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                        }
                                    }

                                }
                                #endregion

                                #region Từ ngày
                                {
                                    String tuNgayText = dr[idx_TuNgay].ToString();
                                    if (!string.IsNullOrEmpty(tuNgayText))
                                    {
                                        try
                                        {
                                            quaTrinhKyLuat.TuNgay = Convert.ToDateTime(tuNgayText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày bắt đầu không hợp lệ: " + tuNgayText);
                                        }
                                    }
                                }
                                #endregion

                                #region Đến ngày
                                {
                                    String denNgayText = dr[idx_DenNgay].ToString();
                                    if (!string.IsNullOrEmpty(denNgayText))
                                    {
                                        try
                                        {
                                            quaTrinhKyLuat.DenNgay = Convert.ToDateTime(denNgayText);
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + Ngày kết thúc không hợp lệ: " + denNgayText);
                                        }
                                    }
                                }
                                #endregion

                                #region Mã quản lý
                                {
                                    String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                    ThongTinNhanVien nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLyText));
                                    if (!string.IsNullOrEmpty(maQuanLyText))
                                    {
                                        quaTrinhKyLuat.ThongTinNhanVien = nhanVien;
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(
                                            " + Thiếu thông tin mã quản lý");
                                    }
                                }
                                #endregion

                                #region Hình thức kỷ luật
                                {
                                    HinhThucKyLuat hinhThucKyLuat = null;
                                    String hinhThucText = dr[idx_HinhThuc].ToString();
                                    if (!string.IsNullOrEmpty(hinhThucText))
                                    {
                                        hinhThucKyLuat = uow.FindObject<HinhThucKyLuat>(CriteriaOperator.Parse("TenHinhThucKyLuat like ?", hinhThucText));
                                        if (hinhThucKyLuat == null)
                                        {
                                            hinhThucKyLuat = new HinhThucKyLuat(uow);
                                            hinhThucKyLuat.TenHinhThucKyLuat = hinhThucText;
                                        }
                                        quaTrinhKyLuat.HinhThucKyLuat = hinhThucKyLuat;
                                    }
                                }
                                #endregion

                                #region Lý do
                                {
                                    String lyDoText = dr[idx_LyDo].ToString();
                                    if (!string.IsNullOrEmpty(lyDoText))
                                    {
                                        quaTrinhKyLuat.LyDo = lyDoText;
                                    }
                                }
                                #endregion
                            }

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (detailLog.Length > 0)
                                {
                                    mainLog.AppendLine(string.Format("- Quá trình kỷ luật của nhân viên: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                    mainLog.AppendLine(detailLog.ToString());
                                }
                            }
                            #endregion
                        }

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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

                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                            obs.Refresh();
                        }
                    }
                }
            }
        }        

    }
}
