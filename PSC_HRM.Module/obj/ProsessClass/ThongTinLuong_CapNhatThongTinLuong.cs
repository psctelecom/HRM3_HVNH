using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.ChamCong;
using PSC_HRM.Module;
using System.Text;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.Controllers
{
    public class ThongTinLuong_CapNhatThongTinLuong
    {
        #region DLU
        public static void SoKyDienVaNuoc(IObjectSpace obs, Guid bangChot)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinTinhLuong thongTinLuong;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        int idx_MaQuanLy = 0;
                                        int idx_SoKyDien = 2;
                                        int idx_SoKyNuoc = 3;

                                        //Tìm nhân viên theo mã quản lý
                                        thongTinLuong = uow.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("(ThongTinNhanVien.MaQuanLy like ? or ThongTinNhanVien.SoHieuCongChuc like ?) and BangChotThongTinTinhLuong.Oid = ?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim(), bangChot));
                                        if (thongTinLuong != null)
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Số ký điện và nước của cán bộ [{0}] vào phần mềm được: ", thongTinLuong.ThongTinNhanVien.Ho));
                                                mainLog.AppendLine(detailLog.ToString());

                                                //Thoát 
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã quản lý (Số hiệu công chức) là: {0}", item[0]));
                                        }
                                    }
                                }
                            }
                            if (mainLog.Length > 0)
                            {
                                //Tiến hành trả lại dữ liệu không import vào phần mền
                                uow.RollbackTransaction();
                                //
                                if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                                //
                                DialogUtil.ShowInfo("Cập số ký điện và nước thành công.");
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region QNU
        public static void SoThangThanhToan(IObjectSpace obs, Guid bangChot)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinTinhLuong thongTinLuong;
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        int idx_MaQuanLy = 0;
                                        int idx_SoThangThanhToan = 2;
                                        int idx_SoThangThanhToanPVDT = 3;
                                        int idx_SoThangThanhToanTNQL = 4;
                                        int idx_SoThangThanhToanDTCV = 5;     

                                        //Tìm thông tin tính lương nhân viên theo mã quản lý
                                        thongTinLuong = uow.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("(ThongTinNhanVien.MaQuanLy like ? or ThongTinNhanVien.SoHieuCongChuc like ?) and BangChotThongTinTinhLuong.Oid = ?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim(), bangChot));
                                        if (thongTinLuong != null)
                                        {

                                            //Số tháng thanh toán
                                            if (!item.IsNull(idx_SoThangThanhToan))
                                            {
                                                try
                                                {
                                                    thongTinLuong.SoThangThanhToan = Convert.ToDecimal(item[idx_SoThangThanhToan].ToString().Trim());
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Số tháng thanh toán không hợp lệ:" + item[idx_SoThangThanhToan].ToString());
                                                }
                                            }

                                            //Số tháng thanh toán
                                            if (!item.IsNull(idx_SoThangThanhToanDTCV))
                                            {
                                                try
                                                {
                                                    thongTinLuong.SoThangThanhToanDTCV = Convert.ToDecimal(item[idx_SoThangThanhToanDTCV].ToString().Trim());
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Số tháng thanh toán DTCV không hợp lệ:" + item[idx_SoThangThanhToanDTCV].ToString());
                                                }
                                            }

                                            //Số tháng thanh toán
                                            if (!item.IsNull(idx_SoThangThanhToanPVDT))
                                            {
                                                try
                                                {
                                                    thongTinLuong.SoThangThanhToanPVDT = Convert.ToDecimal(item[idx_SoThangThanhToanPVDT].ToString().Trim());
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Số tháng thanh toán PVDT không hợp lệ:" + item[idx_SoThangThanhToanPVDT].ToString());
                                                }
                                            }

                                            //Số tháng thanh toán
                                            if (!item.IsNull(idx_SoThangThanhToanTNQL))
                                            {
                                                try
                                                {
                                                    thongTinLuong.SoThangThanhToanTNQL = Convert.ToDecimal(item[idx_SoThangThanhToanTNQL].ToString().Trim());
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Số tháng thanh toán TNQL không hợp lệ:" + item[idx_SoThangThanhToanTNQL].ToString());
                                                }
                                            }
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Số tháng thanh toán của cán bộ [{0}] vào phần mềm được: ", thongTinLuong.ThongTinNhanVien.Ho));
                                                mainLog.AppendLine(detailLog.ToString());

                                                //Thoát 
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã quản lý (Số hiệu công chức) là: {0}", item[0]));
                                        }
                                    }
                                }
                            }
                            if (mainLog.Length > 0)
                            {
                                //Tiến hành trả lại dữ liệu không import vào phần mền
                                uow.RollbackTransaction();
                                //
                                if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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
                                //
                                DialogUtil.ShowInfo("Cập Số tháng thanh toán thành công.");
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
