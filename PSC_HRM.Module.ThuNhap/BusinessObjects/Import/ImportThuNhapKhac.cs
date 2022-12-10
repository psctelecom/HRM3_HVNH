using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.ThuNhap.ThuNhapKhac;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using System.IO;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import thu nhập khác")]
    public class ImportThuNhapKhac : ImportBase
    {
        bool oke = true;
        int ImportLoi;
        int ImportThanhCong;


        public ImportThuNhapKhac(Session session) : base(session) { }

        public override void XuLy(IObjectSpace obs, object obj)
        {
            if (TruongConfig.MaTruong == "UEL")
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.FileName = "";
                        dialog.Multiselect = false;
                        dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls;*.xlsx";

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    ImportLoi = 0;
                                    ImportThanhCong = 0;

                                    BangThuNhapKhac bangThuNhapKhac = obj as BangThuNhapKhac;
                                    ChiTietThuNhapKhac chiTietThuNhapKhac;
                                    ThongTinNhanVien nhanVien;

                                    var mainLog = new StringBuilder();


                                    int sttIndex = 0;
                                    int maQuanLyIndex = 1;
                                    int hoTenIndex = 2;
                                    int boPhanIndex = 3;
                                    int ngayLapIndex = 4;
                                    int soTienIndex = 5;
                                    int soTienChiuThueIndex = 6;
                                    int ghiChuIndex = 7;

                                    using (DialogUtil.AutoWait())
                                    {
                                        foreach (DataRow item in dt.Rows)
                                        {
                                            #region lấy dữ liệu từ excel
                                            String soThuTuText = item[sttIndex].ToString();
                                            String maQuanLyText = item[maQuanLyIndex].ToString().Trim();
                                            String hoTenText = item[hoTenIndex].ToString().Trim();
                                            String ngayLapText = item[ngayLapIndex].ToString().Trim();
                                            String boPhanText = item[boPhanIndex].ToString().Trim();
                                            String soTienText = item[soTienIndex].ToString().Trim();
                                            String soTienChiuThueText = item[soTienChiuThueIndex].ToString().Trim();
                                            String ghiChuText = item[ghiChuIndex].ToString().Trim();
                                            #endregion

                                            var errorLog = new StringBuilder();
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc =?", maQuanLyText));

                                            if (nhanVien == null)
                                            {
                                                mainLog.AppendLine(" - Không có nhân viên nào có số hiệu công chức: " + maQuanLyText);
                                            }
                                            else
                                            {

                                                //Chi tiết Thu nhập khác
                                                chiTietThuNhapKhac = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? and ThongTinNhanVien=?",
                                                    uow.GetObjectByKey<BangThuNhapKhac>(bangThuNhapKhac.Oid),
                                                    nhanVien));
                                                if (chiTietThuNhapKhac == null)
                                                {
                                                    chiTietThuNhapKhac = new ChiTietThuNhapKhac(uow);
                                                    chiTietThuNhapKhac.BangThuNhapKhac = uow.GetObjectByKey<BangThuNhapKhac>(bangThuNhapKhac.Oid);
                                                    chiTietThuNhapKhac.BoPhan = nhanVien.BoPhan;
                                                    chiTietThuNhapKhac.ThongTinNhanVien = nhanVien;
                                                }

                                                #region Số tiền
                                                if (!string.IsNullOrEmpty(soTienText))
                                                {
                                                    try
                                                    {
                                                        chiTietThuNhapKhac.SoTien = Convert.ToDecimal(soTienText);
                                                    }
                                                    catch { errorLog.AppendLine(" + Số tiền không đúng định dạng: " + soTienText); }
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Thiếu dữ liệu số tiền");
                                                }
                                                #endregion

                                                #region Ngày lập
                                                if (!string.IsNullOrEmpty(ngayLapText))
                                                {
                                                    try
                                                    {
                                                        chiTietThuNhapKhac.NgayLap = Convert.ToDateTime(ngayLapText);
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Ngày lập không đúng định dạng: " + ngayLapText);
                                                    }
                                                }
                                                #endregion


                                                #region Số tiền chịu thuế
                                                if (!string.IsNullOrEmpty(soTienChiuThueText))
                                                {
                                                    try
                                                    {
                                                        chiTietThuNhapKhac.SoTienChiuThue = Convert.ToDecimal(soTienChiuThueText);
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Số tiền chịu thuế không đúng định dạng: " + soTienChiuThueText);
                                                    }
                                                }
                                                #endregion

                                                #region Ghi chú
                                                if (!string.IsNullOrEmpty(ghiChuText))
                                                    chiTietThuNhapKhac.GhiChu = ghiChuText;
                                                #endregion
                                            }

                                            #region Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (errorLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                    mainLog.AppendLine(errorLog.ToString());
                                                    ImportLoi++;
                                                }
                                                else
                                                {
                                                    ImportThanhCong++;
                                                }
                                            }
                                            #endregion

                                        }
                                    }

                                    if (mainLog.Length > 0)
                                    {
                                        uow.RollbackTransaction();
                                        if (DialogUtil.ShowYesNo("Import Thành Công " + ImportThanhCong + " - Import không thành công " + ImportLoi + ". Bạn có muốn xuất thông tin lỗi") == DialogResult.Yes)
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
                                        uow.CommitChanges();
                                        DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả dữ liệu !");
                                        obs.Refresh();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    using (OpenFileDialog dialog = new OpenFileDialog())
                    {
                        dialog.FileName = "";
                        dialog.Multiselect = false;
                        dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls;*.xlsx";

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    ImportLoi = 0;
                                    ImportThanhCong = 0;

                                    BangThuNhapKhac bangThuNhapKhac = obj as BangThuNhapKhac;
                                    ChiTietThuNhapKhac chiTietThuNhapKhac;
                                    ThongTinNhanVien nhanVien;

                                    var mainLog = new StringBuilder();

                                    int sttIndex = 0;
                                    int maQuanLyIndex = 1;
                                    int hoTenIndex = 2;
                                    int boPhanIndex = 3;
                                    int ngayLapIndex = 4;
                                    int soTienIndex = 5;
                                    int soTienChiuThueIndex = 6;
                                    int ghiChuIndex = 7;

                                    using (DialogUtil.AutoWait())
                                    {
                                        foreach (DataRow item in dt.Rows)
                                        {
                                            #region lấy dữ liệu từ excel
                                            String soThuTuText = item[sttIndex].ToString();
                                            String maQuanLyText = item[maQuanLyIndex].ToString().Trim();
                                            String hoTenText = item[hoTenIndex].ToString().Trim();
                                            String ngayLapText = item[ngayLapIndex].ToString().Trim();
                                            String boPhanText = item[boPhanIndex].ToString().Trim();
                                            String soTienText = item[soTienIndex].ToString().Trim();
                                            String soTienChiuThueText = item[soTienChiuThueIndex].ToString().Trim();
                                            String ghiChuText = item[ghiChuIndex].ToString().Trim();
                                            #endregion

                                            var errorLog = new StringBuilder();
                                            nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ? or SoHieuCongChuc = ?", maQuanLyText, maQuanLyText));

                                            if (nhanVien == null)
                                            {
                                                mainLog.AppendLine(" - Không có nhân viên nào có mã quản lý: " + maQuanLyText);
                                            }
                                            else
                                            {

                                                //Chi tiết Thu nhập khác
                                                chiTietThuNhapKhac = uow.FindObject<ChiTietThuNhapKhac>(CriteriaOperator.Parse("BangThuNhapKhac=? and ThongTinNhanVien=?",
                                                    uow.GetObjectByKey<BangThuNhapKhac>(bangThuNhapKhac.Oid),
                                                    nhanVien));
                                                if (chiTietThuNhapKhac == null)
                                                {
                                                    chiTietThuNhapKhac = new ChiTietThuNhapKhac(uow);
                                                    chiTietThuNhapKhac.BangThuNhapKhac = uow.GetObjectByKey<BangThuNhapKhac>(bangThuNhapKhac.Oid);
                                                    chiTietThuNhapKhac.BoPhan = nhanVien.BoPhan;
                                                    chiTietThuNhapKhac.ThongTinNhanVien = nhanVien;
                                                }

                                                #region Số tiền
                                                if (!string.IsNullOrEmpty(soTienText))
                                                {
                                                    try
                                                    {
                                                        chiTietThuNhapKhac.SoTien = Convert.ToDecimal(soTienText);
                                                    }
                                                    catch { errorLog.AppendLine(" + Số tiền không đúng định dạng: " + soTienText); }
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Thiếu dữ liệu số tiền");
                                                }
                                                #endregion

                                                #region Ngày lập
                                                if (!string.IsNullOrEmpty(ngayLapText))
                                                {
                                                    try
                                                    {
                                                        chiTietThuNhapKhac.NgayLap = Convert.ToDateTime(ngayLapText);
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Ngày lập không đúng định dạng: " + ngayLapText);
                                                    }
                                                }
                                                #endregion

                                                #region Số tiền chịu thuế
                                                if (!string.IsNullOrEmpty(soTienChiuThueText))
                                                {
                                                    try
                                                    {
                                                        chiTietThuNhapKhac.SoTienChiuThue = Convert.ToDecimal(soTienChiuThueText);
                                                    }
                                                    catch
                                                    {
                                                        errorLog.AppendLine(" + Số tiền chịu thuế không đúng định dạng: " + soTienChiuThueText);
                                                    }
                                                }
                                                #endregion

                                                #region Ghi chú
                                                if (!string.IsNullOrEmpty(ghiChuText))
                                                    chiTietThuNhapKhac.GhiChu = ghiChuText;
                                                #endregion
                                            }

                                            #region Ghi File log
                                            {
                                                //Đưa thông tin bị lỗi vào blog
                                                if (errorLog.Length > 0)
                                                {
                                                    mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                    mainLog.AppendLine(errorLog.ToString());
                                                    ImportLoi++;
                                                }
                                                else
                                                {
                                                    ImportThanhCong++;
                                                }
                                            }
                                            #endregion

                                        }
                                    }

                                    if (mainLog.Length > 0)
                                    {
                                        uow.RollbackTransaction();
                                        if (DialogUtil.ShowYesNo("Import Thành Công " + ImportThanhCong + " - Import không thành công " + ImportLoi + ". Bạn có muốn xuất thông tin lỗi") == DialogResult.Yes)
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
                                        uow.CommitChanges();
                                        DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả dữ liệu !");
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
