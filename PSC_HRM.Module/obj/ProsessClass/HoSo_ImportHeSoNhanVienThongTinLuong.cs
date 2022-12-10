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

namespace PSC_HRM.Module.Controllers
{
    public class HoSo_ImportHeSoNhaVienThongTinLuong
    {
        #region Xử lý UTE
        public static void HSLTangThem_UTE(IObjectSpace obs)
        { //
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
                            ThongTinNhanVien nhanVien;
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

                                        int idx_SoTaiKhoan = 0;
                                        int idx_HSLTangThem = 2;

                                        //Tìm nhân viên theo số tài khoản
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ListTaiKhoanNganHang[SoTaiKhoan=? and TaiKhoanChinh=1]", item[idx_SoTaiKhoan].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            //Hệ số lương tăng thêm
                                            if (!item.IsNull(idx_HSLTangThem) && !string.IsNullOrEmpty(item[idx_HSLTangThem].ToString()))
                                            {
                                                try
                                                {
                                                    nhanVien.NhanVienThongTinLuong.HSLTangThem = Convert.ToDecimal(item[idx_HSLTangThem].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + HSL tăng thêm không hợp lệ:" + item[idx_HSLTangThem].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + HSL tăng thêm không tìm thấy.");
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import HSL tăng thêm của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());

                                                //Thoát 
                                                break;
                                            }

                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Số tài khoản là: {0}", item[0]));
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
                                DialogUtil.ShowInfo("Cập nhật HSL tăng thêm thành công!");

                            }
                        }
                    }
                }
            }

        }
        #endregion

        #region Xử lý DLU
        public static void HSPCDocHai_DLU(IObjectSpace obs)
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
                            ThongTinNhanVien nhanVien;
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
                                        int idx_HSPCDocHai = 2;

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            //Hệ số phụ cấp độc hại
                                            if (!item.IsNull(idx_HSPCDocHai) && !string.IsNullOrEmpty(item[idx_HSPCDocHai].ToString()))
                                            {
                                                try
                                                {
                                                    nhanVien.NhanVienThongTinLuong.HSPCDocHai = Convert.ToDecimal(item[idx_HSPCDocHai].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Hệ số PC độc hại không hợp lệ:" + item[idx_HSPCDocHai].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Hệ số PC độc hại không tìm thấy.");
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Hệ số PC độc hại của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
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
                            //
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
                                DialogUtil.ShowInfo("Cập nhật Hệ số PC độc hại thành công.");
                            }
                        }
                    }
                }
            }
        }
        public static void HSPCTrachNhiem_DLU(IObjectSpace obs)
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
                            ThongTinNhanVien nhanVien;
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
                                        int idx_HSPCTrachNhiem = 2;

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            //Hệ số phụ cấp trách nhiệm
                                            if (!item.IsNull(idx_HSPCTrachNhiem) && !string.IsNullOrEmpty(item[idx_HSPCTrachNhiem].ToString()))
                                            {
                                                try
                                                {
                                                    nhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = Convert.ToDecimal(item[idx_HSPCTrachNhiem].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Hệ số PC trách nhiệm không hợp lệ:" + item[idx_HSPCTrachNhiem].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Hệ số PC trách nhiệm không tìm thấy.");
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Hệ số PC độc hại của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
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
                            //
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
                                DialogUtil.ShowInfo("Cập nhật Hệ số PC trách nhiệm thành công.");
                            }
                        }
                    }
                }
            }
        }
        public static void HSPCKhuVuc_DLU(IObjectSpace obs)
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
                            ThongTinNhanVien nhanVien;
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
                                        int idx_HSPCKhuVuc = 2;

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Hệ số PC khu vực của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
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
                            //
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
                                DialogUtil.ShowInfo("Cập nhật Hệ số PC khu vực thành công.");
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Shared
        public static void PhuCapUuDai(IObjectSpace obs)
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
                            ThongTinNhanVien nhanVien;
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
                                        int idx_PhuCapUuDai = 2;

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", item[idx_MaQuanLy].ToString().Trim(), item[idx_MaQuanLy].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            //Phụ cấp ưu đãi
                                            if (!item.IsNull(idx_PhuCapUuDai) && !string.IsNullOrEmpty(item[idx_PhuCapUuDai].ToString()))
                                            {
                                                try
                                                {
                                                    nhanVien.NhanVienThongTinLuong.PhuCapUuDai = Convert.ToInt32(item[idx_PhuCapUuDai].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Phụ cấp ưu đãi không hợp lệ:" + item[idx_PhuCapUuDai].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Phụ cấp ưu đãi không tìm thấy.");
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import Phụ cấp ưu đãi của cán bộ [{0}] vào phần mềm được: ", nhanVien.HoTen));
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
                                DialogUtil.ShowInfo("Cập nhật Phụ cấp ưu đãi thành công.");
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
