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
using PSC_HRM.Module.ThuNhap.LuongWeb;
using PSC_HRM.Module.ThuNhap.Luong;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import bảng lương web")]
    public class ImportBangLuongNhanVienWeb : ImportBase
    {
        //
        public ImportBangLuongNhanVienWeb(Session session) : base(session) { }

        public override void XuLy(IObjectSpace obs, object obj)
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
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:H]"))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                BangLuongNhanVienWeb bangLuong = obj as BangLuongNhanVienWeb;
                                ChiTietLuongNhanVienWeb chiTietLuong;
                                ThongTinNhanVien nhanVien;
                                //
                                var mainLog = new StringBuilder();
                                //
                                int sttIndex = 0;
                                int maQuanLyIndex = 1;
                                int hoTenIndex = 2;
                                int boPhanIndex = 3;
                                int noiDungIndex = 4;
                                int ngayChiIndex = 5;
                                int soTienIndex = 6;
                                int ghiChuIndex = 7;

                                using (DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        #region lấy dữ liệu từ excel
                                        String soThuTuText = item[sttIndex].ToString();
                                        String maQuanLyText = item[maQuanLyIndex].ToString().Trim();
                                        String hoTenText = item[hoTenIndex].ToString().Trim();
                                        String ngayLapText = item[ngayChiIndex].ToString().Trim();
                                        String boPhanText = item[boPhanIndex].ToString().Trim();
                                        String soTienText = item[soTienIndex].ToString().Trim();
                                        String ghiChuText = item[ghiChuIndex].ToString().Trim();
                                        String noiDungText = item[noiDungIndex].ToString().Trim();
                                        #endregion

                                        var errorLog = new StringBuilder();
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy =?", maQuanLyText));

                                        if (nhanVien == null)
                                        {
                                            mainLog.AppendLine(" - Không có nhân viên nào có mã quản lý: [" + maQuanLyText + "]");
                                        }
                                        else
                                        {

                                            //Chi tiết Thu nhập khác
                                            chiTietLuong = uow.FindObject<ChiTietLuongNhanVienWeb>(CriteriaOperator.Parse("BangLuongNhanVienWeb.Oid=? and ThongTinNhanVien=?", uow.GetObjectByKey<BangLuongNhanVienWeb>(bangLuong.Oid), nhanVien));
                                            if (chiTietLuong == null)
                                            {
                                                chiTietLuong = new ChiTietLuongNhanVienWeb(uow);
                                                chiTietLuong.BangLuongNhanVienWeb = uow.GetObjectByKey<BangLuongNhanVienWeb>(bangLuong.Oid);
                                                chiTietLuong.BoPhan = nhanVien.BoPhan;
                                                chiTietLuong.ThongTinNhanVien = nhanVien;
                                            }

                                            #region Nội dung
                                            if (!string.IsNullOrEmpty(noiDungText))
                                            {
                                               chiTietLuong.NoiDung = noiDungText;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu dữ liệu nội dung.");
                                            }
                                            #endregion

                                            #region Số tiền
                                            if (!string.IsNullOrEmpty(soTienText))
                                            {
                                                try
                                                {
                                                    chiTietLuong.SoTien = Convert.ToDecimal(soTienText);
                                                }
                                                catch { errorLog.AppendLine(" + Số tiền không đúng định dạng: " + soTienText); }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu dữ liệu số tiền.");
                                            }
                                            #endregion

                                            #region Ngày chi
                                            if (!string.IsNullOrEmpty(ngayLapText))
                                            {
                                                try
                                                {
                                                    chiTietLuong.NgayChi = Convert.ToDateTime(ngayLapText);
                                                }
                                                catch
                                                {
                                                    errorLog.AppendLine(" + Ngày chi không đúng định dạng: " + ngayLapText);
                                                }
                                            }
                                            #endregion

                                            #region Ghi chú
                                            if (!string.IsNullOrEmpty(ghiChuText))
                                                chiTietLuong.GhiChu = ghiChuText;
                                            #endregion
                                        }

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên: " + hoTenText);
                                                mainLog.AppendLine(errorLog.ToString());
                                            }
                                        }
                                        #endregion
                                    }
                                }

                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    //
                                    if (DialogUtil.ShowYesNo("Import dữ liệu thất bại. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                        System.Diagnostics.Process.Start(tenFile);
                                    }
                                }
                                else
                                {
                                    uow.CommitChanges();
                                    obs.Refresh();
                                    DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả dữ liệu !");
                                   
                                }
                            }
                        }
                    }
                }
            }
        }
    }

}
