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
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Controllers
{
    public class HoSo_ImportThongTinHoSo
    {
        #region Shared
        public static void ThongTinSucKhoe(IObjectSpace obs)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNhanVien nhanVien;
                            //
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            //
                            int idx_SoHieuCongChuc = 0;
                            int idx_HoTen = 1;
                            int idx_TinhTrangSucKhoe = 2;
                            int idx_ChieuCao = 3;
                            int idx_CanNang = 4;
                            int idx_NhomMau = 5;
                            //
                            using(DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                        //
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? or MaQuanLy = ?", item[idx_SoHieuCongChuc].ToString(), item[idx_SoHieuCongChuc].ToString()));
                                        if (nhanVien != null)
                                        {
                                            #region Tình trạng sức khỏe
                                            if (!item.IsNull(idx_TinhTrangSucKhoe) && !string.IsNullOrEmpty(item[idx_TinhTrangSucKhoe].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenSucKhoe like ?", item[idx_TinhTrangSucKhoe].ToString());
                                                SucKhoe tinhTrangSucKhoe = uow.FindObject<SucKhoe>(filter);
                                                if (tinhTrangSucKhoe != null)
                                                {
                                                    nhanVien.TinhTrangSucKhoe = tinhTrangSucKhoe;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy tình trạng sức khỏe: " + item[idx_TinhTrangSucKhoe].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Tình trạng sức khỏe không tìm thấy.");
                                            }
                                            #endregion

                                            #region Chiều cao
                                            if (!item.IsNull(idx_ChieuCao) && !string.IsNullOrEmpty(item[idx_ChieuCao].ToString()))
                                            {
                                                try
                                                {
                                                    int chieuCao = Convert.ToInt32(item[idx_ChieuCao].ToString());
                                                    nhanVien.ChieuCao = chieuCao;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Chiều cao không đúng định dạng: " + item[idx_ChieuCao].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Chiều cao không tìm thấy.");
                                            }
                                            #endregion

                                            #region Cân nặng
                                            if (!item.IsNull(idx_CanNang) && !string.IsNullOrEmpty(item[idx_CanNang].ToString()))
                                            {
                                                try
                                                {
                                                    int canNang = Convert.ToInt32(item[idx_CanNang].ToString());
                                                    nhanVien.CanNang = canNang;
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Cân nặng không đúng định dạng: " + item[idx_CanNang].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Cân nặng không tìm thấy.");
                                            }
                                            #endregion

                                            #region Nhóm máu
                                            if (!item.IsNull(idx_NhomMau) && !string.IsNullOrEmpty(item[idx_NhomMau].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenNhomMau like ?", item[idx_NhomMau].ToString());
                                                NhomMau nhomMau = uow.FindObject<NhomMau>(filter);
                                                if (nhomMau != null)
                                                {
                                                    nhanVien.NhomMau = nhomMau;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy nhóm máu: " + item[idx_TinhTrangSucKhoe].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Nhóm máu không tìm thấy.");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Không tìm thấy nhân viên nào có Mã quản lý (Số hiệu công chức) là: " + item[idx_SoHieuCongChuc].ToString());
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không cập nhật thông tin sức khỏe [{0}] vào phần mềm được: ", item[idx_HoTen].ToString()));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }
                                }
                                //
                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();

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

                                    //Refesh lại dữ liệu
                                    obs.Refresh();

                                    //Xuất thông báo thành công
                                    DialogUtil.ShowInfo("Quá trình cập nhật thông tin sức khỏe thành công.");
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Xử lý DUL
        public static void CongViecHienNay(IObjectSpace obs)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNhanVien nhanVien;
                            //
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            //
                            int idx_SoHieuCongChuc = 0;
                            int idx_HoTen = 1;
                            int idx_CongViecHienNay = 2;
                            //
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                        //
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? or MaQuanLy = ?", item[idx_SoHieuCongChuc].ToString(), item[idx_SoHieuCongChuc].ToString()));
                                        if (nhanVien != null)
                                        {
                                            #region Công việc hiện nay
                                            if (!item.IsNull(idx_CongViecHienNay) && !string.IsNullOrEmpty(item[idx_CongViecHienNay].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenCongViec like ?", item[idx_CongViecHienNay].ToString());
                                                CongViec congViec = uow.FindObject<CongViec>(filter);
                                                if (congViec != null)
                                                {
                                                    nhanVien.CongViecHienNay = congViec;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy Công việc: " + item[idx_CongViecHienNay].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Công việc hiện nay không tìm thấy.");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Không tìm thấy nhân viên nào có Mã quản lý (Số hiệu công chức) là: " + item[idx_SoHieuCongChuc].ToString());
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không cập nhật Công việc hiện nay [{0}] vào phần mềm được: ", item[idx_HoTen].ToString()));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }
                                }
                                //
                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();

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

                                    //Refesh lại dữ liệu
                                    obs.Refresh();

                                    //Xuất thông báo thành công
                                    DialogUtil.ShowInfo("Quá trình cập nhật Công việc hiện nay thành công.");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void TinhTrang(IObjectSpace obs)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNhanVien nhanVien;
                            //
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            //
                            int idx_SoHieuCongChuc = 0;
                            int idx_HoTen = 1;
                            int idx_TinhTrang = 2;
                            //
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                        //
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? or MaQuanLy = ?", item[idx_SoHieuCongChuc].ToString(), item[idx_SoHieuCongChuc].ToString()));
                                        if (nhanVien != null)
                                        {
                                            #region Loại hợp đồng
                                            if (!item.IsNull(idx_TinhTrang) && !string.IsNullOrEmpty(item[idx_TinhTrang].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", item[idx_TinhTrang].ToString());
                                                TinhTrang tinhTrang = uow.FindObject<TinhTrang>(filter);
                                                if (tinhTrang != null)
                                                {
                                                    nhanVien.TinhTrang = tinhTrang;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy Tình trạng: " + item[idx_TinhTrang].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Tình trạng không tìm thấy.");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Không tìm thấy nhân viên nào có Mã quản lý (Số hiệu công chức) là: " + item[idx_SoHieuCongChuc].ToString());
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không cập nhật Tình trạng [{0}] vào phần mềm được: ", item[idx_HoTen].ToString()));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }
                                }
                                //
                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();

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

                                    //Refesh lại dữ liệu
                                    obs.Refresh();

                                    //Xuất thông báo thành công
                                    DialogUtil.ShowInfo("Quá trình cập nhật Tình trạng thành công.");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void LoaiHopDong(IObjectSpace obs)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNhanVien nhanVien;
                            //
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            //
                            int idx_SoHieuCongChuc = 0;
                            int idx_HoTen = 1;
                            int idx_LoaiHopDong = 2;
                            //
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                        //
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? or MaQuanLy = ?", item[idx_SoHieuCongChuc].ToString(), item[idx_SoHieuCongChuc].ToString()));
                                        if (nhanVien != null)
                                        {
                                            #region Tình trạng
                                            if (!item.IsNull(idx_LoaiHopDong) && !string.IsNullOrEmpty(item[idx_LoaiHopDong].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenLoaiNhanVien like ?", item[idx_LoaiHopDong].ToString());
                                                LoaiNhanVien loaiNhanVien = uow.FindObject<LoaiNhanVien>(filter);
                                                if (loaiNhanVien != null)
                                                {
                                                    nhanVien.LoaiNhanVien = loaiNhanVien;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy Loại hợp đồng: " + item[idx_LoaiHopDong].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Loại hợp đồng không tìm thấy.");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Không tìm thấy nhân viên nào có Mã quản lý (Số hiệu công chức) là: " + item[idx_SoHieuCongChuc].ToString());
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không cập nhật Loại hợp đồng [{0}] vào phần mềm được: ", item[idx_HoTen].ToString()));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }
                                }
                                //
                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();

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

                                    //Refesh lại dữ liệu
                                    obs.Refresh();

                                    //Xuất thông báo thành công
                                    DialogUtil.ShowInfo("Quá trình cập nhật Loại hợp đồng thành công.");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void LoaiNhanSu(IObjectSpace obs)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNhanVien nhanVien;
                            //
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            //
                            int idx_SoHieuCongChuc = 0;
                            int idx_HoTen = 1;
                            int idx_LoaiNhanSu = 2;
                            //
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                        //
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? or MaQuanLy = ?", item[idx_SoHieuCongChuc].ToString(), item[idx_SoHieuCongChuc].ToString()));
                                        if (nhanVien != null)
                                        {
                                            #region Loại nhân sự
                                            if (!item.IsNull(idx_LoaiNhanSu) && !string.IsNullOrEmpty(item[idx_LoaiNhanSu].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenLoaiNhanSu like ?", item[idx_LoaiNhanSu].ToString());
                                                LoaiNhanSu loaiNhanSu = uow.FindObject<LoaiNhanSu>(filter);
                                                if (loaiNhanSu != null)
                                                {
                                                    nhanVien.LoaiNhanSu = loaiNhanSu;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy Loại nhận sự: " + item[idx_LoaiNhanSu].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Loại nhận sự không tìm thấy.");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Không tìm thấy nhân viên nào có Mã quản lý (Số hiệu công chức) là: " + item[idx_SoHieuCongChuc].ToString());
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không cập nhật Loại hợp đồng [{0}] vào phần mềm được: ", item[idx_HoTen].ToString()));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }
                                }
                                //
                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();

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

                                    //Refesh lại dữ liệu
                                    obs.Refresh();

                                    //Xuất thông báo thành công
                                    DialogUtil.ShowInfo("Quá trình cập nhật Loại nhận sự thành công.");
                                }
                            }
                        }
                    }
                }
            }
        }
        public static void ChucVu(IObjectSpace obs)
        {
            //
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                        {
                            ThongTinNhanVien nhanVien;
                            //
                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;
                            //
                            int idx_SoHieuCongChuc = 0;
                            int idx_HoTen = 1;
                            int idx_ChucVu = 2;
                            //
                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();
                                        //
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? or MaQuanLy = ?", item[idx_SoHieuCongChuc].ToString(), item[idx_SoHieuCongChuc].ToString()));
                                        if (nhanVien != null)
                                        {
                                            #region Chức vụ
                                            if (!item.IsNull(idx_ChucVu) && !string.IsNullOrEmpty(item[idx_ChucVu].ToString()))
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("TenChucVu like ?", item[idx_ChucVu].ToString());
                                                ChucVu chucVu = uow.FindObject<ChucVu>(filter);
                                                if (chucVu != null)
                                                {
                                                    nhanVien.ChucVu = chucVu;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Không tìm thấy Chức vụ: " + item[idx_ChucVu].ToString() + " trong hệ thống.");
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Chức vụ không tìm thấy.");
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Không tìm thấy nhân viên nào có Mã quản lý (Số hiệu công chức) là: " + item[idx_SoHieuCongChuc].ToString());
                                        }

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không cập nhật Chức vụ [{0}] vào phần mềm được: ", item[idx_HoTen].ToString()));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                    }
                                }
                                //
                                if (mainLog.Length > 0)
                                {
                                    //Tiến hành trả lại dữ liệu không import vào phần mền
                                    uow.RollbackTransaction();

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

                                    //Refesh lại dữ liệu
                                    obs.Refresh();

                                    //Xuất thông báo thành công
                                    DialogUtil.ShowInfo("Quá trình cập nhật Chức vụ thành công.");
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
