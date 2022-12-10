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
    public class ThoiChuc_ImportQuyetDinhThoiChuc
    {
        public static void XuLy(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:I]"))
                    {
                        QuyetDinhThoiChuc quyetDinhThoiChuc;
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
                                    int ngayThoiHuongHSPCVu = 7;
                                    int quyetDinhBoNhiem = 8;
                      
                    
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
                                        String ngayThoiHuongHSPCVuText = item[ngayThoiHuongHSPCVu].ToString().FullTrim();
                                        String quyetDinhBoNhiemText = item[quyetDinhBoNhiem].ToString().FullTrim();
                                
                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            //Lấy dữ liệu quyết định đào tạo
                                            quyetDinhThoiChuc = new QuyetDinhThoiChuc(uow);
                                            quyetDinhThoiChuc.QuyetDinhMoi = false;

                                            #region Số quyết định
                                            if (!string.IsNullOrEmpty(soQuyetDinhText))
                                            {
                                                QuyetDinhThoiChuc quyetDinh = uow.FindObject<QuyetDinhThoiChuc>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                if (quyetDinh == null)
                                                    quyetDinhThoiChuc.SoQuyetDinh = soQuyetDinhText;
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
                                                    quyetDinhThoiChuc.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
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
                                                    quyetDinhThoiChuc.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
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
                                                quyetDinhThoiChuc.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                quyetDinhThoiChuc.TenCoQuan = coQuanRaQuyetDinhText;

                                                if (!string.IsNullOrEmpty(nguoiKyText))
                                                {
                                                    quyetDinhThoiChuc.NguoiKy1 = nguoiKyText;
                                                }

                                            }
                                            else
                                            {
                                                quyetDinhThoiChuc.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                            }
                                            #endregion

                                            #region Ngày phát sinh biến động
                                            if (!string.IsNullOrEmpty(ngayPhatSinhBienDongText))
                                            {
                                                try
                                                {
                                                    quyetDinhThoiChuc.NgayPhatSinhBienDong = Convert.ToDateTime(ngayPhatSinhBienDongText);
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
                                                quyetDinhThoiChuc.ThongTinNhanVien = nhanVien;
                                                quyetDinhThoiChuc.BoPhan = nhanVien.BoPhan;
                                            }
                                            #endregion

                                            #region Ngày thôi hưởng HSPC chức vụ
                                            if (!string.IsNullOrEmpty(ngayThoiHuongHSPCVuText))
                                            {
                                                try
                                                {
                                                    quyetDinhThoiChuc.NgayThoiHuongHSPCChucVu = Convert.ToDateTime(ngayThoiHuongHSPCVuText);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày thôi hưởng HSPC chức vụ không hợp lệ: " + ngayThoiHuongHSPCVuText);
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Ngày thôi hưởng HSPC chức vụ chưa có dữ liệu");
                                            }
                                            #endregion

                                            #region Quyết định bổ nhiệm
                                            if (!string.IsNullOrEmpty(quyetDinhBoNhiemText))
                                            {
                                                QuyetDinhBoNhiem qdbn = uow.FindObject<QuyetDinhBoNhiem>(CriteriaOperator.Parse("SoQuyetDinh=?", quyetDinhBoNhiemText));
                                                if (qdbn != null)
                                                    quyetDinhThoiChuc.QuyetDinhBoNhiem = qdbn;
                                                else
                                                    detailLog.AppendLine("Quyết định bổ nhiệm không hợp lệ");
                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Quyết định bổ nhiệm chưa có dữ liệu");
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
