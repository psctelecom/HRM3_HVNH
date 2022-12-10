using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public class KyLuat_ImportQuyetDinhKyLuat
    {
        public static void XuLy_BUH(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:J]"))
                    {
                        QuyetDinhKyLuat quyetDinhKyLuat;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //
                                    int soQuyetDinh = 0;
                                    int ngayQuyetDinh = 1;
                                    int ngayHieuLuc = 2;
                                    int coQuanRaQuyetDinh = 3;
                                    int nguoiKy = 4;
                                    int maQuanLy = 5;
                                    int tuNgay = 6;
                                    int denNgay= 7;
                                    int hinhThucKyLuat = 8;
                                    int lyDo = 9;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                        String coQuanRaQuyetDinhText = item[coQuanRaQuyetDinh].ToString().FullTrim();
                                        String nguoiKyText = item[nguoiKy].ToString().FullTrim();
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();
                                        String tuNgayText = item[tuNgay].ToString().FullTrim();
                                        String denNgayText = item[denNgay].ToString().FullTrim();
                                        String hinhThucKyLuatText = item[hinhThucKyLuat].ToString().FullTrim();
                                        String lyDoText = item[lyDo].ToString().FullTrim();

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            //Lấy dữ liệu quyết định đào tạo
                                            quyetDinhKyLuat = new QuyetDinhKyLuat(uow);

                                            #region Số quyết định
                                            if (!string.IsNullOrEmpty(soQuyetDinhText))
                                            {
                                                QuyetDinh.QuyetDinh quyetDinh = uow.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                if (quyetDinh == null)
                                                    quyetDinhKyLuat.SoQuyetDinh = soQuyetDinhText;
                                                else
                                                    detailLog.AppendLine("Số quyết định đã tồn tại: " + soQuyetDinhText);
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Ngày quyết định
                                            if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                            {
                                                try
                                                {
                                                    quyetDinhKyLuat.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Ngày hiệu lực
                                            if (!string.IsNullOrEmpty(ngayHieuLucText))
                                            {
                                                try
                                                {
                                                    quyetDinhKyLuat.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Cơ quan ra quyết định - Nguời ký
                                            if (!string.IsNullOrEmpty(coQuanRaQuyetDinhText))
                                            {
                                                quyetDinhKyLuat.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                quyetDinhKyLuat.TenCoQuan = coQuanRaQuyetDinhText;

                                                if (!string.IsNullOrEmpty(nguoiKyText))
                                                {
                                                    quyetDinhKyLuat.NguoiKy1 = nguoiKyText;
                                                }

                                            }
                                            else
                                            {
                                                quyetDinhKyLuat.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                            }
                                            #endregion

                                            #region Nhân viên
                                            if (!string.IsNullOrEmpty(maQuanLyText))
                                            {
                                                quyetDinhKyLuat.ThongTinNhanVien = nhanVien;
                                                quyetDinhKyLuat.BoPhan = nhanVien.BoPhan;
                                            }
                                            #endregion

                                            #region Từ ngày
                                            if (!string.IsNullOrEmpty(tuNgayText))
                                            {
                                                try
                                                {
                                                    quyetDinhKyLuat.TuNgay = Convert.ToDateTime(tuNgayText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgayText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Từ ngày chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Đến ngày
                                            if (!string.IsNullOrEmpty(denNgayText))
                                            {
                                                try
                                                {
                                                    quyetDinhKyLuat.DenNgay = Convert.ToDateTime(denNgayText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Đến ngày chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Hình thức kỷ luật
                                            if (!string.IsNullOrEmpty(hinhThucKyLuatText))
                                            {
                                                HinhThucKyLuat HinhThucKyLuat = uow.FindObject<HinhThucKyLuat>(CriteriaOperator.Parse("TenHinhThucKyLuat = ?",hinhThucKyLuatText));
                                                if (HinhThucKyLuat == null)
                                                {
                                                    HinhThucKyLuat = new HinhThucKyLuat(uow);
                                                    HinhThucKyLuat.MaQuanLy = HamDungChung.TaoChuVietTat(hinhThucKyLuatText);
                                                    HinhThucKyLuat.TenHinhThucKyLuat = hinhThucKyLuatText;
                                                }
                                                quyetDinhKyLuat.HinhThucKyLuat = HinhThucKyLuat;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Hình thức kỷ luật chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Lý do
                                            if (!string.IsNullOrEmpty(lyDoText))
                                            {
                                                quyetDinhKyLuat.LyDo = lyDoText;
                                            }
                                            #endregion

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
                                            }
                                         
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã nhân sự là: {0}", maQuanLy));
                                        }
                                    }
                                }
                            }

                            //
                            if (mainLog.Length > 0)
                            {
                                uow.RollbackTransaction();
                                if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                {
                                    string tenFile = "Import_Log.txt";
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
                                DialogUtil.ShowSaveSuccessful("Import Thành Công!");
                            }

                        }
                    }
                }
            }
        }
    }
}
