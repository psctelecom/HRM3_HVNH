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
    public class BoNhiem_ImportQuyetDinhBoNhiem
    {
        public static void XuLy(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:L]"))
                    {
                        QuyetDinhBoNhiem quyetDinhBoNhiem;
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
                                    int ngayPhatSinhBienDong = 5;
                                    int maQuanLy = 6;
                                    int boPhanMoi = 7;
                                    int chucVuMoi= 8;
                                    int ngayHuongHSPCMoi = 9;
                                    int soNamNhiemKy = 10;
                                    int ngayHetNhiemKy = 11;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                        String coQuanRaQuyetDinhText = item[coQuanRaQuyetDinh].ToString().FullTrim();
                                        String nguoiKyText = item[nguoiKy].ToString().FullTrim();
                                        String ngayPhatSinhBienDongText = item[ngayPhatSinhBienDong].ToString().FullTrim();
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();
                                        String boPhanMoiText = item[boPhanMoi].ToString().FullTrim();
                                        String chucVuMoiText = item[chucVuMoi].ToString().FullTrim();
                                        String ngayHuongHSPCMoiText = item[ngayHuongHSPCMoi].ToString().FullTrim();
                                        String soNamNhiemKyText = item[soNamNhiemKy].ToString().FullTrim();
                                        String ngayHetNhiemKyText = item[ngayHetNhiemKy].ToString().FullTrim();

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            //Lấy dữ liệu quyết định đào tạo
                                            quyetDinhBoNhiem = new QuyetDinhBoNhiem(uow);
                                            quyetDinhBoNhiem.QuyetDinhMoi = false;

                                            #region Số quyết định
                                            if (!string.IsNullOrEmpty(soQuyetDinhText))
                                            {
                                                QuyetDinhBoNhiem quyetDinh = uow.FindObject<QuyetDinhBoNhiem>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                if (quyetDinh == null)
                                                    quyetDinhBoNhiem.SoQuyetDinh = soQuyetDinhText;
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
                                                    quyetDinhBoNhiem.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
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
                                                    quyetDinhBoNhiem.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
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
                                                quyetDinhBoNhiem.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                quyetDinhBoNhiem.TenCoQuan = coQuanRaQuyetDinhText;

                                                if (!string.IsNullOrEmpty(nguoiKyText))
                                                {
                                                    quyetDinhBoNhiem.NguoiKy1 = nguoiKyText;
                                                }

                                            }
                                            else
                                            {
                                                quyetDinhBoNhiem.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                            }
                                            #endregion

                                            #region Ngày phát sinh biến động
                                            if (!string.IsNullOrEmpty(ngayPhatSinhBienDongText))
                                            {
                                                try
                                                {
                                                    quyetDinhBoNhiem.NgayPhatSinhBienDong = Convert.ToDateTime(ngayPhatSinhBienDongText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày phát sinh không hợp lệ: " + ngayPhatSinhBienDongText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Ngày phát sinh chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Nhân viên
                                            if (!string.IsNullOrEmpty(maQuanLyText))
                                            {
                                                quyetDinhBoNhiem.ThongTinNhanVien = nhanVien;
                                                quyetDinhBoNhiem.BoPhan = nhanVien.BoPhan;
                                                quyetDinhBoNhiem.ChucVuCu = nhanVien.ChucVu;
                                                quyetDinhBoNhiem.NgayHuongHeSoCu = nhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu;
                                                quyetDinhBoNhiem.HSPCChucVuCu = nhanVien.NhanVienThongTinLuong.HSPCChucVu;
                                            }
                                            #endregion

                                            #region Bộ phận mới
                                            if (!string.IsNullOrEmpty(boPhanMoiText))
                                            {
                                                BoPhan BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan=?", boPhanMoiText));
                                                if (BoPhan != null)
                                                    quyetDinhBoNhiem.BoPhanMoi = BoPhan;
                                                else
                                                    detailLog.AppendLine("Bộ phận mới không hợp lệ");
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Bộ phận mới chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Chức vụ mới
                                            if (!string.IsNullOrEmpty(chucVuMoiText))
                                            {
                                                ChucVu ChucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu=?", chucVuMoiText));
                                                if (ChucVu != null)
                                                    quyetDinhBoNhiem.ChucVuMoi = ChucVu;
                                                else
                                                    detailLog.AppendLine("Chức vụ mới không hợp lệ");
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Chức vụ mới chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Ngày hưởng HSPC mới
                                            if (!string.IsNullOrEmpty(ngayHuongHSPCMoiText))
                                            {
                                                try
                                                {
                                                    quyetDinhBoNhiem.NgayHuongHeSoMoi = Convert.ToDateTime(ngayHuongHSPCMoiText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng HSPC mới không hợp lệ: " + ngayHuongHSPCMoiText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Ngày hưởng HSPC mới chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Số năm nhiệm kỳ 
                                            if (!string.IsNullOrEmpty(soNamNhiemKyText))
                                            {
                                                try
                                                {
                                                    quyetDinhBoNhiem.NhiemKy = Convert.ToInt32(soNamNhiemKyText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Số năm nhiệm kỳ không hợp lệ: " + soNamNhiemKyText);
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("Số năm nhiệm kỳ chưa có dữ liệu");
                                            //}
                                            #endregion

                                            #region Ngày hết nhiệm kỳ
                                            if (!string.IsNullOrEmpty(ngayHetNhiemKyText))
                                            {
                                                try
                                                {
                                                    quyetDinhBoNhiem.NgayHetNhiemKy = Convert.ToDateTime(ngayHetNhiemKyText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày hết nhiệm kỳ không hợp lệ: " + ngayHetNhiemKyText);
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("Ngày hết nhiệm kỳ chưa có dữ liệu");
                                            //}
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
