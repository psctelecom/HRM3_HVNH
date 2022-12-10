using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.Metadata;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using System.IO;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.HoSo;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using PSC_HRM.Module.CauHinh;
using DevExpress.ExpressApp.Security;
using System.Windows.Forms;
using PSC_HRM.Module.MailMerge;
using System.Diagnostics;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using System.Configuration;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.NghiPhep;

namespace PSC_HRM.Module
{
    public static class HamDungChung
    {
        public static void DebugTrace(string message)
        {
            Debug.WriteLine("-- hrm --: " + message);
        }

        public static string NangLuong(NangLuongEnum phanLoai, int vuotKhung)
        {
            if (phanLoai == NangLuongEnum.ThuongXuyen)
                return vuotKhung > 0 ? "Nâng phụ cấp vượt khung thường xuyên" : "Nâng lương thường xuyên";
            else if (phanLoai == NangLuongEnum.CoThanhTichXuatSac)
                return "Nâng lương do có thành tích xuất sắc";
            else
                return "Nâng lương trước khi nghỉ hưu";
        }

        public static string NangLuong_GhiChu(NangLuongEnum phanLoai, int vuotKhung)
        {
            if (phanLoai == NangLuongEnum.ThuongXuyen)
                return vuotKhung > 0 ? "VK" : "TX";
            else if (phanLoai == NangLuongEnum.CoThanhTichXuatSac)
                return "TH";
            else
                return "Hưu";
        }

        public static bool KhongHienNhanVienKhiChonTruong
        {
            get
            {
                if (CauHinhChung != null && CauHinhChung.CauHinhHoSo != null)
                    return CauHinhChung.CauHinhHoSo.KhongHienNhanVienKhiChonTruong;
                return false;
            }
        }

        /// <summary>
        /// Get default template
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MailMergeTemplate GetTemplate(IObjectSpace obs, string name)
        {
            MailMergeTemplate template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ? and SuDungMacDinh = ?", name, "True"));
            if (template == null)
                template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", name));
            return template;
        }

        /// <summary>
        /// Trung viet them
        /// Tinh so thang
        /// </summary>
        /// <param name="batDau">thang bat dau</param>
        /// <param name="ketThuc">thang ket thuc</param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static int TinhSoThang(DateTime batDau, DateTime ketThuc)
        {
            int count = 0;
            int temp = 0;

            //tính số tháng
            if (batDau.CompareTo(DateTime.MinValue) != 0 && ketThuc.CompareTo(DateTime.MinValue) != 0)
            {
                count = 12 * (ketThuc.Year - batDau.Year) + (ketThuc.Month - batDau.Month);
            }
            //tính tháng do chênh lệch ngày
            //ví dụ: 1/1/2012 -> 31/5/2012 => 5 tháng
            //if (batDau.Day == 1)
            //{
            //    if (DateTime.DaysInMonth(ketThuc.Year, ketThuc.Month) == ketThuc.Day)
            //        temp = 1;
            //}

            return Math.Abs(count) + temp;
        }

        /// <summary>
        /// Tinh so nam
        /// </summary>
        /// <param name="batDau"></param>
        /// <param name="ketThuc"></param>
        /// <returns></returns>
        public static int TinhSoNam(DateTime batDau, DateTime ketThuc)
        {
            DateTime temp = new DateTime(ketThuc.Year, batDau.Month, batDau.Day);
            if (temp == ketThuc.AddDays(-1))
            {
                return (ketThuc.Year - batDau.Year) + 1;
            }
            else
            {
                return ketThuc.Year - batDau.Year;
            }
        }

        /// <summary>
        /// Tính thời gian
        /// </summary>
        /// <param name="batDau"></param>
        /// <param name="ketThuc"></param>
        /// <returns></returns>
        public static decimal GetSoNgayPhepNamConLai(Session session, DateTime ngay, ThongTinNhanVien nhanVien)
        {
            decimal soNgayPhep = 0;
            QuanLyNghiPhep quanlynghiphep = session.FindObject<QuanLyNghiPhep>(CriteriaOperator.Parse("Nam = ?", ngay.Year));
            if (quanlynghiphep != null && nhanVien != null)
            {
                ThongTinNghiPhep thongTinNghiPhep = session.FindObject<ThongTinNghiPhep>(CriteriaOperator.Parse("ThongTinNhanVien = ? and QuanLyNghiPhep = ?", nhanVien.Oid, quanlynghiphep.Oid));
                if (thongTinNghiPhep != null)
                    soNgayPhep = thongTinNghiPhep.SoNgayPhepConLai;
            }
            return soNgayPhep;
        }

        /// <summary>
        /// Tính thời gian
        /// </summary>
        /// <param name="batDau"></param>
        /// <param name="ketThuc"></param>
        /// <returns></returns>
        public static int GetSoNgayLe(Session session, DateTime batDau, DateTime ketThuc)
        {
            XPCollection<NgayNghiTrongNam> ngayNghiTrongNamList = new XPCollection<NgayNghiTrongNam>(session);
            ngayNghiTrongNamList.Criteria = CriteriaOperator.Parse("NgayNghi >= ? AND NgayNghi <= ?", Convert.ToDateTime(batDau.ToShortDateString()), Convert.ToDateTime(ketThuc.ToShortDateString()));
            int soNgayNghiLe = ngayNghiTrongNamList.Count;
            return soNgayNghiLe;
        }

        /// <summary>
        /// Tính thời gian
        /// </summary>
        /// <param name="batDau"></param>
        /// <param name="ketThuc"></param>
        /// <returns></returns>
        public static string GetThoiGian(DateTime batDau, DateTime ketThuc)
        {
            string thoiGianText = string.Empty;
            if (batDau != DateTime.MinValue && ketThuc != DateTime.MinValue)
            {
                decimal soNgay = batDau.TinhSoNgay(ketThuc);
                if (soNgay < 30)
                {
                    if (soNgay < 10)
                        thoiGianText = string.Concat("0", soNgay.ToString(), " ngày");
                    else
                        thoiGianText = string.Concat(soNgay.ToString(), " ngày");
                }
                else
                {
                    decimal soThang = batDau.TinhSoThang(ketThuc);
                    if (soThang > 12)
                    {
                        decimal soNam = batDau.TinhSoNam(ketThuc);
                        if (soNam < 10)
                            thoiGianText = string.Concat("0", soNam.ToString(), " năm");
                        else
                            thoiGianText = string.Concat(soNam.ToString(), " năm");
                    }
                    else
                    {
                        if (soThang < 10)
                            thoiGianText = string.Concat("0", soThang.ToString(), " tháng");
                        else
                            thoiGianText = string.Concat(soThang.ToString(), " tháng");
                    }
                }
            }
            return thoiGianText;
        }

        /// <summary>
        /// Tính thời gian
        /// </summary>
        /// <param name="batDau"></param>
        /// <param name="ketThuc"></param>
        /// <returns></returns>
        public static string GetThoiGianViecRieng(Session session, DateTime batDau, DateTime ketThuc, ThongTinNhanVien nhanVien, decimal ngayTru)
        {
            string thoiGianText = string.Empty;
            decimal soNgay = batDau.TinhSoNgay(ketThuc);
            if (soNgay < 30)
            {
                if (soNgay < 10)
                    thoiGianText = string.Concat("0", soNgay.ToString(), " ngày");
                else
                    thoiGianText = string.Concat(soNgay.ToString(), " ngày");
            }
            else
            {
                decimal soThang = batDau.TinhSoThang(ketThuc);
                if (soThang > 12)
                {
                    decimal soNam = batDau.TinhSoNam(ketThuc);
                    if (soNam < 10)
                        thoiGianText = string.Concat("0", soNam.ToString(), " năm");
                    else
                        thoiGianText = string.Concat(soNam.ToString(), " năm");
                }
                else
                {
                    if (soThang < 10)
                        thoiGianText = string.Concat("0", soThang.ToString(), " tháng");
                    else
                        thoiGianText = string.Concat(soThang.ToString(), " tháng");
                }
            }
            
            int soNgayNghiLe = GetSoNgayLe(session, batDau, ketThuc);
            if (soNgayNghiLe > 0)
                thoiGianText = string.Concat(thoiGianText, " bao gồm ", soNgayNghiLe.ToString(), " ngày lễ");

            decimal soNgayPhep = 0;
            if (nhanVien != null)
                soNgayPhep = GetSoNgayPhepNamConLai(session, batDau, nhanVien);
            if (ngayTru > 0 && soNgayNghiLe > 0)
                thoiGianText = string.Concat(thoiGianText, " ,", ngayTru.ToString(), " ngày phép");
            else if (ngayTru > 0 && soNgayNghiLe <= 0)
                thoiGianText = string.Concat(thoiGianText, " bao gồm ", ngayTru.ToString(), " ngày phép");

            return thoiGianText;
        }

        /// <summary>
        /// Create username
        /// </summary>
        /// <param name="hoTen"></param>
        /// <returns></returns>
        public static string CreateTenTaiKhoan(string ho, string ten)
        {
            string result = string.Empty;

            return result;
        }

        /// <summary>
        /// Get current quy
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static QuyEnum GetCurrentQuy()
        {
            DateTime current = GetServerTime();
            QuyEnum quy;
            if (current.Month < 4)
                quy = QuyEnum.QuyI;
            else if (current.Month < 7)
                quy = QuyEnum.QuyII;
            else if (current.Month < 10)
                quy = QuyEnum.QuyIII;
            else
                quy = QuyEnum.QuyIV;
            return quy;
        }

        /// <summary>
        /// Get phan quyen bao cao
        /// </summary>
        /// <returns></returns>
        public static PhanQuyenBaoCao GetPhanQuyenBaoCao()
        {
            NguoiSuDung user = CurrentUser() as NguoiSuDung;
            if (user != null && user.PhanQuyenBaoCao!=null)
                return user.PhanQuyenBaoCao;
            return null;
        }

        /// <summary>
        /// Search nam hoc
        /// </summary>
        /// <param name="session"></param>
        /// <param name="ngay"></param>
        /// <returns></returns>
        public static NamHoc SearchNamHoc(Session session, DateTime ngay)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("NgayBatDau<=? and NgayKetThuc>=?",
                    ngay, ngay);
            NamHoc namHoc = session.FindObject<NamHoc>(filter);
            if (namHoc == null)
            {
                using (UnitOfWork uow = new UnitOfWork(session.DataLayer))
                {
                    namHoc = new NamHoc(session);
                    namHoc.NgayBatDau = new DateTime(ngay.Year, 6, 1);
                    namHoc.NgayKetThuc = new DateTime(ngay.Year + 1, 5, 31);
                    uow.CommitChanges();
                }
                namHoc = session.GetObjectByKey<NamHoc>(namHoc.Oid);
            }
            return namHoc;
        }

        /// <summary>
        /// Kiểm tra xem có được cấp quyền sửa không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsWriteGranted<T>() where T : BaseObject
        {

            return SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(T), null, null, SecurityOperations.Write));
        }

        /// <summary>
        /// Kiểm tra xem có được cấp quyền create không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsCreateGranted<T>() where T : BaseObject
        {
            return SecuritySystem.IsGranted(new ClientPermissionRequest(typeof(T), null, null, SecurityOperations.Create));
        }

        /// <summary>
        /// Kiểm tra xem có được cấp quyền truy cập không
        /// </summary>
        /// <typeparam name="T">typeof(BaseObject)</typeparam>
        /// <returns></returns>
        public static bool IsAccessGranted(Type type)
        {
            return SecuritySystem.IsGranted(new ClientPermissionRequest(type, null, null, SecurityOperations.Navigate));
        }

        public static void ShowSuccessMessage(string message)
        {
            XtraMessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarningMessage(string message)
        {
            XtraMessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowErrorMessage(string message)
        {
            XtraMessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowMessage(string message, MessageBoxButtons button)
        {
            return XtraMessageBox.Show(message, "Thông báo", button, MessageBoxIcon.Information);
        }

        /// <summary>
        /// get tên bộ phận
        /// if bo phan la bo mon thi tra ve tên bo phan cha
        /// else tra ve ten cua no
        /// </summary>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static string GetTenBoPhan(BoPhan boPhan)
        {
            string result = string.Empty;
            if (boPhan != null)
            {
                if (boPhan.LoaiBoPhan == LoaiBoPhanEnum.BoMonTrucThuocKhoa &&
                    boPhan.BoPhanCha != null)
                    result = String.Format("{0} - {1}", boPhan.TenBoPhan, boPhan.BoPhanCha.TenBoPhan);
                else
                    result = boPhan.TenBoPhan;
            }
            return result;
        }

        /// <summary>
        /// get chuc vụ cao nhất trong đơn vị
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucVuCaoNhatTrongDonVi(BoPhan boPhan)
        {
            string result = string.Empty;
            if (boPhan != null)
            {
                if (boPhan.TenBoPhan.Contains("Cơ sở"))
                {
                    result = "Trưởng cơ sở";
                }
                else if (boPhan.TenBoPhan.Contains("Phòng"))
                {
                    result = "Trưởng phòng";
                }
                else if (boPhan.TenBoPhan.Contains("Viện"))
                {
                    result = "Viện trưởng";
                }
                else if (boPhan.TenBoPhan.Contains("Khoa"))
                {
                    result = "Trưởng khoa";
                }
                else if (boPhan.TenBoPhan.Contains("Văn phòng"))
                {
                    result = "Chánh văn phòng";
                }
                else if (boPhan.TenBoPhan.Contains("Bộ môn"))
                {
                    result = "Chủ nhiệm bộ môn";
                }
                else
                {
                    result = "Giám đốc";
                }
            }
            return result;
        }

         /// <summary>
        /// get tất cả chức vụ của nhân viên
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucVuNhanVien(ThongTinNhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien.NhanVienThongTinLuong.NgachLuong != null)
                result = nhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong;
            if (nhanVien.ChucVu != null)
                result = String.Concat(result, ", ", nhanVien.ChucVu.TenChucVu.Replace("khoa", string.Empty).Replace("bộ môn", string.Empty), GetChucVuDonVi(nhanVien));
            if (nhanVien.ChucVuKiemNhiem != null)
                result = String.Concat(result, ", ", nhanVien.ChucVuKiemNhiem.TenChucVu);
            if (nhanVien.ChucVuDang != null)
                result = String.Concat(result, ", ", nhanVien.ChucVuDang.TenChucVuDang);
            if (nhanVien.ChucVuDoan != null)
                result = String.Concat(result, ", ", nhanVien.ChucVuDoan.TenChucVuDoan);
            if (nhanVien.ChucVuDoanThe != null)
                result = String.Concat(result, ", ", nhanVien.ChucVuDoanThe.TenChucVuDoanThe);
            return result;
        }

        /// <summary>
        /// get tên đơn vị của nhân viên
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucVuDonVi(ThongTinNhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien.ChucVu != null)
            {
                if (nhanVien.BoPhan.TenBoPhan.ToLower().Contains("ban giám hiệu"))
                    result = nhanVien.ThongTinTruong.TenBoPhan;
                else if (nhanVien.ChucVu.TenChucVu.Contains("bộ môn") && nhanVien.TaiBoMon != null && nhanVien.TaiBoMon.LoaiBoPhan == LoaiBoPhanEnum.BoMonTrucThuocKhoa)
                    result = String.Concat(nhanVien.TaiBoMon.TenBoPhan," - ", nhanVien.TaiBoMon.BoPhanCha.TenBoPhan);
                else
                    result = nhanVien.BoPhan.TenBoPhan;
            }
            else
                result = nhanVien.BoPhan.TenBoPhan;
            return result;
        }

        public static string TenMonHocTuPhanMenUIS(string maMonHoc)
        {
            string tenMonHoc = string.Empty;
            //
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_UIS.bin")))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                string query = @"Select CurriculumId as ID, CurriculumName as Name From dbo.psc_Curriculums Order By CurriculumName";

                using (DataTable dt = DataProvider.GetDataTable(cnn, query, CommandType.Text))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["ID"].ToString().Equals(maMonHoc))
                            {
                                tenMonHoc = row["Name"].ToString();
                            }
                        }
                    }
                }
            }

            return tenMonHoc;
        }


        /// <summary>
        /// get chuc danh
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucDanh(NhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien != null)
            {               
                if (nhanVien.NhanVienTrinhDo.HocHam != null)
                {
                    result = nhanVien.NhanVienTrinhDo.HocHam.MaQuanLy;
                    if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                        result += "." + nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                }
                else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                    (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("tiến") ||
                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("thạc")))
                    result = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                else if (nhanVien.GioiTinh == GioiTinhEnum.Nam)
                    result = "Ông";
                else
                    result = "Bà";                             
            }
            return result;
        }

        /// <summary>
        /// get chuc danh
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucDanhEnglish(NhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien != null)
            {
                if (nhanVien.NhanVienTrinhDo.HocHam != null)
                {
                    result = nhanVien.NhanVienTrinhDo.HocHam.MaQuanLyEng;
                    if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                        result += "." + nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLyEng;
                }
                else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                    (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("tiến") ||
                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("thạc")))
                    result = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLyEng;
                else if (nhanVien.GioiTinh == GioiTinhEnum.Nam)
                    result = "Mr.";
                else
                    result = "Ms.";
            }
            return result;
        }

        /// <summary>
        /// get chuc danh
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucDanhVietThuong(NhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien != null)
            {
                if (nhanVien.NhanVienTrinhDo.HocHam != null)
                {
                    result = nhanVien.NhanVienTrinhDo.HocHam.MaQuanLy;
                    if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                        result += "." + nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                }
                else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                    (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("tiến") ||
                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("thạc")))
                    result = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                else if (nhanVien.GioiTinh == GioiTinhEnum.Nam)
                    result = "ông";
                else
                    result = "bà";
            }
            return result;
        }

        /// <summary>
        /// get chuc danh
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetChucDanhNguoiKy(ThongTinNhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien != null)
            {
                if (nhanVien.NhanVienTrinhDo.HocHam != null)
                {
                    result = nhanVien.NhanVienTrinhDo.HocHam.MaQuanLy;
                    if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                        result += "." + nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                }
                else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                    (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("tiến") ||
                    nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("thạc")))
                    result = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                else if (nhanVien.ChucDanh != null)
                    result = nhanVien.ChucDanh.MaQuanLy;
                else
                    result = "";
            }
            return result;
        }

        /// <summary>
        /// get hoc vi
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetHocVi(ThongTinNhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien.NhanVienTrinhDo.HocHam != null)
            {
                result = nhanVien.NhanVienTrinhDo.HocHam.TenHocHam;
                if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                    result += "." + nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
            }
            else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("tiến") ||
                nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("thạc")))
                result = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon;
            else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("cử") ||
                nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("kỹ") ||
                nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("đại")))
                result = "Cử nhân";
            return result;
        }

        /// <summary>
        /// get hoc ham
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetHocHam(ThongTinNhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien.NhanVienTrinhDo.HocHam != null)
            {
                result = nhanVien.NhanVienTrinhDo.HocHam.TenHocHam;
                
            }
            else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("tiến") ||
                nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("thạc")))
                result = nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon;
            else if (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null &&
                (nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("cử") ||
                nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("kỹ") ||
                nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon.ToLower().Contains("đại")))
                result = "Cử nhân";
            return result;
        }


        /// <summary>
        /// Lấy loại nhân viên
        /// </summary>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static string GetLoaiNhanVien(ThongTinNhanVien nhanVien)
        {
            string result = string.Empty;
            if (nhanVien.ChucVu != null)
                result = nhanVien.ChucVu.TenChucVu;
            else if (nhanVien.LoaiNhanSu != null)
                result = nhanVien.LoaiNhanSu.TenLoaiNhanSu;
            return result;
        }

        /// <summary>
        /// Lấy Số tiền 1 tiết
        /// </summary>
        /// <param name="nhanvien"></param>
        /// <returns></returns>
        public static decimal GetSoTien1Tiet(NhanVien nv, Session session)
        {
            decimal sotien = 0;

            GiangVienThinhGiang giangVienThinhGiang = nv as GiangVienThinhGiang;
            //
            if (giangVienThinhGiang != null && giangVienThinhGiang.NhanVienTrinhDo != null)
            {
                CriteriaOperator filter;
                if (giangVienThinhGiang.NhanVienTrinhDo.HocHam != null && giangVienThinhGiang.HocVi == null)
                   filter = CriteriaOperator.Parse("HocHam = ? and HocVi is null", giangVienThinhGiang.NhanVienTrinhDo.HocHam);
                else if (giangVienThinhGiang.NhanVienTrinhDo.HocHam == null && giangVienThinhGiang.HocVi != null)
                    filter = CriteriaOperator.Parse("HocVi = ? and HocHam is null", giangVienThinhGiang.HocVi);
                else
                    filter = CriteriaOperator.Parse("HocHam = ? and HocVi = ?", giangVienThinhGiang.NhanVienTrinhDo.HocHam,giangVienThinhGiang.HocVi);
                //
                DanhMucTinhTienThinhGiang tienThinhGiang = session.FindObject<DanhMucTinhTienThinhGiang>(filter);
                if (tienThinhGiang != null)
                {
                    sotien = tienThinhGiang.DonGia;
                }
            }
            return sotien;
        }
        /// <summary>
        /// Lấy Số tiền Chất lượng cao
        /// </summary>
        /// <param name="nhanvien"></param>
        /// <returns></returns>
        public static decimal GetSoTienChatLuongCao(NhanVien nv, Session session)
        {
            decimal sotien = 0;

            GiangVienThinhGiang giangVienThinhGiang = nv as GiangVienThinhGiang;
            //
            if (giangVienThinhGiang != null && giangVienThinhGiang.NhanVienTrinhDo != null)
            {
                CriteriaOperator filter;
                if (giangVienThinhGiang.NhanVienTrinhDo.HocHam != null && giangVienThinhGiang.HocVi == null)
                    filter = CriteriaOperator.Parse("HocHam = ? and HocVi is null", giangVienThinhGiang.NhanVienTrinhDo.HocHam);
                else if (giangVienThinhGiang.NhanVienTrinhDo.HocHam == null && giangVienThinhGiang.HocVi != null)
                    filter = CriteriaOperator.Parse("HocVi = ? and HocHam is null", giangVienThinhGiang.HocVi);
                else
                    filter = CriteriaOperator.Parse("HocHam = ? and HocVi = ?", giangVienThinhGiang.NhanVienTrinhDo.HocHam, giangVienThinhGiang.HocVi);
                //
                DanhMucTinhTienThinhGiang tienThinhGiang = session.FindObject<DanhMucTinhTienThinhGiang>(filter);
                if (tienThinhGiang != null)
                {
                    sotien = tienThinhGiang.DonGiaCLC;
                }
            }
            return sotien;
        }


        /// <summary>
        /// Get ten lop tu database CoreUIS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTenLop(string id)
        {
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_PMS.bin")))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                const string query = @"spd_HRM_GetTenLop";
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@MaLop", id);
                param[1] = new SqlParameter("@TenLop", SqlDbType.NVarChar, 1000);
                param[1].Direction = ParameterDirection.Output;
                SqlCommand cmd = DataProvider.GetCommand(query, CommandType.StoredProcedure, param);

                DataProvider.ExecuteNonQuery(cnn, cmd);

                string result = param[1].Value.ToString();
                return result;
            }
        }

        /// <summary>
        /// Get ten mon tu database CoreUIS
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetTenMon(string id)
        {
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_UIS.bin")))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();
                string query = @"SELECT CurriculumName 
                                FROM CoreUis.dbo.psc_Curriculums
	                            WHERE CurriculumID = @MaMon";
                SqlParameter param = new SqlParameter("@MaMon", id);
                SqlCommand cmd = DataProvider.GetCommand(query, CommandType.Text, param);
                DataProvider.ExecuteNonQuery(cnn, cmd);
                string result = param.Value.ToString();
                return result;

                //const string query = @"spd_HRM_GetTenLop";
                //SqlParameter[] param = new SqlParameter[2];
                //param[0] = new SqlParameter("@MaMon", id);
                //param[1] = new SqlParameter("@TenMon", SqlDbType.NVarChar, 1000);
                //param[1].Direction = ParameterDirection.Output;
                //SqlCommand cmd = DataProvider.GetCommand(query, CommandType.StoredProcedure, param);

                //HamExecuteNonQuery(cnn, cmd);

                //string result = param[1] == null ? " " : param[1].Value.ToString();
                //return result;
            }
        }

        /// <summary>
        /// Tình hệ số theo công thức lập sẵn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static decimal GetHeSo(string congThucTinhHeSo)
        {
            decimal result = 0;
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString(DataProvider.DataBase)))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();
                string query = @"Select " + congThucTinhHeSo.Trim() + "";
                //
                object obj = DataProvider.GetObject(query, CommandType.Text);
                if (obj != null)
                {
                    result = Convert.ToDecimal(obj.ToString());
                }
                return result;
            }
        }

        /// <summary>
        /// lấy phân quyền bộ phận để tính lương
        /// </summary>
        /// <returns></returns>
        public static string GetPhanQuyenBoPhan()
        {
            PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
            if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                return permission.Quyen;
            return string.Empty;
        }

        /// <summary>
        /// Tạo bộ lọc người ký quyết định, hợp đồng
        /// </summary>
        /// <param name="phanLoai"></param>
        /// <param name="chucVu"></param>
        /// <returns></returns>
        public static CriteriaOperator GetNguoiKyTenCriteria(NguoiKyEnum phanLoai, ChucVu chucVu)
        {
            CriteriaOperator filter;
            if (phanLoai == NguoiKyEnum.DangTaiChuc)
                filter = CriteriaOperator.Parse("ChucVu=?", chucVu);
            else if (phanLoai == NguoiKyEnum.DangKhongTaiChuc)
            {
                using (DataTable dt = DataProvider.GetDataTable("spd_System_GetChucVuDaQua", CommandType.StoredProcedure, new SqlParameter("@ChucVu", chucVu.Oid)))
                {
                    List<Guid> oid = new List<Guid>();
                    foreach (DataRow item in dt.Rows)
                    {
                        oid.Add(Guid.Parse(item[0].ToString()));
                    }
                    filter = new InOperator("Oid", oid);
                }
            }
            else
                filter = CriteriaOperator.Parse("ChucVu=?", chucVu);

            return filter;
        }

        /// <summary>
        /// Lấy thông tin nơi làm việc cho quá trình BHXH
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static string NoiLamViec(Session session, ThongTinNhanVien nhanVien, BoPhan boPhan)
        {
            ThongTinTruong truong = ThongTinTruong(session);
            string chucDanh = String.Format("{0} {1}, ", (nhanVien.ChucVu != null ? nhanVien.ChucVu.TenChucVu : "Nhân viên"), boPhan.TenBoPhan);
            string luong = "";
            if (nhanVien.NhanVienThongTinLuong.PhanLoai != ThongTinLuongEnum.LuongHeSo)
                luong = String.Format("mức lương {0:N0}, ", nhanVien.NhanVienThongTinLuong.LuongKhoan);
            else if (nhanVien.NhanVienThongTinLuong.NgachLuong != null &&
                nhanVien.NhanVienThongTinLuong.NgachLuong.TotKhung != null &&
                nhanVien.NhanVienThongTinLuong.BacLuong != null)
                luong = String.Format("bậc {0}/{1} {2}, ", nhanVien.NhanVienThongTinLuong.BacLuong.MaQuanLy,
                    nhanVien.NhanVienThongTinLuong.NgachLuong.TotKhung.MaQuanLy,
                    nhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy);
            string coQuan = "";
            if (truong != null && truong.DiaChi != null)
                coQuan = String.Format("{0}, {1}", truong.TenBoPhan, truong.DiaChi.FullDiaChi);

            string result = String.Format("{0}{1}{2}", chucDanh, luong, coQuan);

            return result;
        }

        /// <summary>
        /// Lấy filter giảng viên thỉnh giảng
        /// </summary>
        /// <param name="session"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static CriteriaOperator GetCriteriaGiangVienThinhGiang(Session session)
        {
            CriteriaOperator result = CriteriaOperator.Parse("");
            SqlParameter param;
     
            StringBuilder sb = new StringBuilder();
            foreach (string item in DanhSachBoPhanDuocPhanQuyen(session))
            {
                sb.Append(item + ",");
            }
            param = new SqlParameter("@BoPhan", sb.ToString());
 
            using (DataTable dt = DataProvider.GetDataTable("spd_Filter_GiangVienThinhGiang", CommandType.StoredProcedure, param))
            {
                List<Guid> oid = new List<Guid>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        oid.Add(new Guid(item[0].ToString()));
                    }
                    result = new InOperator("Oid", oid);
                }
            }
            return result;
        }

        /// <summary>
        /// Lấy filter thông tin nhân viên
        /// </summary>
        /// <param name="session"></param>
        /// <param name="boPhan"></param>
        /// <returns></returns>
        public static CriteriaOperator GetCriteriaThongTinNhanVien(BoPhan boPhan,Session session)
        {
            CriteriaOperator result = CriteriaOperator.Parse("");
            SqlParameter param;
            if (boPhan == null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string item in DanhSachBoPhanDuocPhanQuyen(session))
                {
                    sb.Append(item + ",");
                }
                param = new SqlParameter("@BoPhan", sb.ToString());
            }
            else
            {
                param = new SqlParameter("@BoPhan", boPhan.Oid.ToString());
            }
            using (DataTable dt = DataProvider.GetDataTable("spd_Filter_ThongTinNhanVien", CommandType.StoredProcedure, param))
            {
                List<Guid> oid = new List<Guid>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        oid.Add(new Guid(item[0].ToString()));
                    }
                    result = new InOperator("Oid", oid);
                }
            }
            return result;
        }

        /// <summary>
        /// Nơi lưu trữ giấy tờ trên server
        /// </summary>
        public static string NoiLuuTruGiayTo
        {
            get
            {
                if (CauHinhChung != null)
                {
                    return CauHinhChung.NoiLuuTruGiayTo;
                }
                return string.Empty;
            }
        }

        public static CauHinhNhacViec CauHinhNhacViec
        {
            get
            {
                if (CauHinhChung != null)
                    return CauHinhChung.CauHinhNhacViec;
                return null;
            }
        }

        /// <summary>
        /// Get current quoc gia
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static QuocGia GetCurrentQuocGia(Session session)
        {
            if (CauHinhChung != null && CauHinhChung.QuocGia != null)
                return session.GetObjectByKey<QuocGia>(CauHinhChung.QuocGia.Oid);
            return session.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", "%Việt Nam%"));
        }

        /// <summary>
        /// Cấu hình chung: Quốc gia, năm học, học kỳ, nơi lưu trữ giấy tờ
        /// </summary>
        public static CauHinhChung CauHinhChung
        {
            get
            {
                if (SecuritySystem.CurrentUser != null &&
                    (SecuritySystem.CurrentUser is NguoiSuDung) &&
                    (SecuritySystem.CurrentUser as NguoiSuDung).ThongTinTruong != null)
                {
                    return (SecuritySystem.CurrentUser as NguoiSuDung).ThongTinTruong.CauHinhChung;
                }
                return null;
            }
        }


        /// <summary>
        /// Get criteria nhan vien
        /// </summary>
        /// <param name="bp">Bộ phận</param>
        /// <returns></returns>
        public static GroupOperator CriteriaGetAllNhanVien(BoPhan bp)
        {
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", DanhSachBoPhanDuocPhanQuyen(bp)));

            return go;
        }

        /// <summary>
        /// Get criteria nhân viên khác nghỉ hưu, nghỉ việc theo bộ phận
        /// </summary>
        /// <param name="bp">Bộ phận</param>
        /// <returns></returns>
        public static GroupOperator CriteriaGetNhanVien(BoPhan bp)
        {
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong=?", "False"));
            go.Operands.Add(new InOperator("BoPhan", DanhSachBoPhanDuocPhanQuyen(bp)));

            return go;
        }

        /// <summary>
        /// Get năm học hiện tại
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>NamHoc</returns>
        public static NamHoc GetCurrentNamHoc(Session session)
        {
            NamHoc namHoc = null;
            if (CauHinhChung != null && CauHinhChung.NamHoc != null)
                namHoc = session.GetObjectByKey<NamHoc>(CauHinhChung.NamHoc.Oid);
            else
            {
                DateTime ngay = GetServerTime();
                namHoc = session.FindObject<NamHoc>(CriteriaOperator.Parse("NgayBatDau<=? and NgayKetThuc>=?", ngay, ngay));
            }
            return namHoc;
        }

        /// <summary>
        /// Get học kỳ hiện tại
        /// </summary>
        /// <param name="session">Session</param>
        /// <returns>Học kỳ</returns>
        public static HocKy GetCurrentHocKy(Session session)
        {
            HocKy hocKy = null;
            if (CauHinhChung != null && CauHinhChung.HocKy != null)
                hocKy = session.GetObjectByKey<HocKy>(CauHinhChung.HocKy.Oid);
            else
            {
                DateTime current = GetServerTime();
                hocKy = session.FindObject<HocKy>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=?", current, current));
            }
            return hocKy;
        }

        /// <summary>
        /// Get học kỳ hiện tại
        /// </summary>
        /// <param name="session">Session</param>
        /// <returns>Học kỳ</returns>
        public static KyHopDong GetCurrentKyHopDong(Session session)
        {
            KyHopDong kyHopDong = null;

            DateTime current = GetServerTime();
            kyHopDong = session.FindObject<KyHopDong>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=?", current, current));
            //
            return kyHopDong;
        }

        /// <summary>
        /// Get Criteria tìm kiếm nhân viên theo điều kiện
        /// </summary>
        /// <param name="session"></param>
        /// <param name="query"></param>
        /// <param name="type"></param>
        /// <param name="criteria"></param>
        /// <param name="args">từ ngày, đến ngày</param>
        /// <returns></returns>
        public static List<Guid> GetCriteria(IObjectSpace obs, string query, CommandType type, string criteria, params object[] args)
        {
            List<Guid> result = new List<Guid>();
            DataTable dt = new DataTable();

            SqlParameter param = new SqlParameter("@Criteria", criteria.XuLyDieuKien(obs, true, args));

            SqlCommand cmd = DataProvider.GetCommand(query, type, param);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    result.Add(new Guid(item[0].ToString()));
                }
            }
            return result;
        }

        /// <summary>
        /// Đổi giờ phút ra phút
        /// </summary>
        /// <param name="gio">giờ phút có dạng: 00:00</param>
        /// <returns></returns>
        public static int DoiGioRaPhut(string gio)
        {
            int itemp;
            string[] split;
            int result = 0;
            if (!String.IsNullOrEmpty(gio))
            {
                split = gio.Split(':');
                if (split != null && split.Length == 2)
                {
                    gio = split[0].Trim();
                    if (int.TryParse(gio, out itemp))
                        result = itemp * 60;
                    gio = split[1].Trim();
                    if (int.TryParse(gio, out itemp))
                        result += itemp;
                }
            }
            return result;
        }

        //Lưu vết để ẩn hiện column
        public static string AcountType = string.Empty;

        /// <summary>
        /// Hide column of Object --- Chỉ sử dụng để phần quyền column đối phó với UTE
        /// </summary>
        /// <returns></returns>
        public static string CheckAcountType()
        {
            if (CurrentUser() != null && CurrentUser().PhanQuyenBoPhan.Ten.Equals("TaiChinh"))
            {
                AcountType = "TaiChinh";
            }
            else if (CurrentUser() != null && !CurrentUser().PhanQuyenBoPhan.Ten.Equals("TaiChinh") && !CurrentUser().PhanQuyenBoPhan.Ten.Equals("PSC"))
            {
                AcountType = "ToChuc";
            }
            else
            {
                AcountType = string.Empty;
            }
            return AcountType;
        }

        /// <summary>
        /// get current user
        /// </summary>
        /// <returns></returns>
        public static NguoiSuDung CurrentUser()
        {
            if (SecuritySystem.CurrentUser != null)
                return SecuritySystem.CurrentUser as NguoiSuDung;
            return null;
        }


        /// <summary>
        /// Lấy đơn vị hiện tại của người sử dụng
        /// </summary>
        /// <returns></returns>
        public static ThongTinTruong ThongTinTruong(Session session)
        {
            NguoiSuDung user = CurrentUser();
            if (user != null)
            {
                if (user.ThongTinTruong != null)
                    return session.GetObjectByKey<ThongTinTruong>(user.ThongTinTruong.Oid);
                return null;
            }
            else
                return null;
        }

        /// <summary>
        /// Tính giá trị biểu thức được lập bởi tên caption của field
        /// </summary>
        /// <param name="source">object chứa các giá trị để tính toán</param>
        /// <param name="CongThuc">chuổi công thức</param>
        /// <returns></returns>
        public static decimal Eval(BaseObject source, string CongThuc)
        {
            //cắt lấy từng giá trị trong [] để chuyển sang tên field
            int i = 0;
            int b, e;
            string s1, s2, kq = "";
            while (i < CongThuc.Length)
            {
                b = CongThuc.IndexOf("{", i);
                if (b >= 0)
                {
                    if (b > i)
                        kq += CongThuc.Substring(i, b - i);

                    e = CongThuc.IndexOf("}", i);
                    s1 = CongThuc.Substring(b + 1, e - b - 1);
                    s2 = Eval_Expression(s1, source.ClassInfo);
                    kq += s2;
                    i = e + 1;
                }
                else
                {
                    kq += CongThuc.Substring(i, CongThuc.Length - i);
                    i = CongThuc.Length;
                }
            }
            //tính giá trị
            try
            {
                return Convert.ToDecimal(source.Evaluate(CriteriaOperator.Parse(kq)));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Lỗi xử lý công thức : {0}\r\n{1}", CongThuc, ex.Message));
            }
        }

        private static string Eval_Expression(string source, XPClassInfo info)
        {
            //đổi từ caption sang tên property
            //tìm nếu có dấu . thì cần đệ qui phân tích exp
            int i;
            string s, kq;
            i = source.IndexOf(".");
            if (i >= 0)
                s = source.Substring(0, i);
            else
                s = source;
            //đổi captio sang field
            string field = "";
            XPClassInfo f = null;
            foreach (XPMemberInfo p in info.PersistentProperties)
            {
                foreach (var a in p.Attributes)
                    if (a is CustomAttribute && (a as CustomAttribute).Name == "Caption" && (a as CustomAttribute).Value == s)
                    {
                        f = p.ReferenceType;
                        field = p.Name;
                        break;
                    }
                if (field != "") break;
            }
            if (field != "")
                kq = field;
            else
                kq = s;
            if (i >= 0 && f != null)
                kq += "." + Eval_Expression(source.Substring(i + 1, source.Length - i - 1), f);

            return kq;
        }

        /// <summary>
        /// Get phân quyền đơn vị
        /// </summary>
        /// <returns></returns>
        public static List<Guid> GetCriteriaBoPhan()
        {
            List<Guid> result = new List<Guid>();
            string[] split = null;
            PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
            if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            if (split != null && split.Length > 0)
                foreach (string item in split)
                    result.Add(new Guid(item));

            return result;
        }
       
        /// <summary>
        /// Lấy danh sách id của Đơn vị theo phân quyền của user hiện tại
        /// </summary>
        public static List<string> DanhSachBoPhanDuocPhanQuyen()
        {
            List<string> resultList = new List<string>();
            //
            string[] split = null;
            if (SecuritySystem.CurrentUser != null)
            {
                PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
                if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                {
                    //Lợi thêm lọc bộ phận ngưng hoạt động
                    CriteriaOperator criteria = CriteriaOperator.Parse("NgungHoatDong=true");
                    XPCollection<BoPhan> boPhanList = new XPCollection<BoPhan>(permission.Session, criteria);

                    foreach (BoPhan item in boPhanList)
                    {
                        permission.Quyen = permission.Quyen.ToString().Replace(item.Oid.ToString(), string.Empty);
                        permission.Quyen = permission.Quyen.ToString().Replace(";;", ";");
                    }
                    //Lợi thêm lọc bộ phận ngưng hoạt động

                    if (!HamDungChung.CheckAdministrator()) //
                    permission.Quyen = permission.Quyen.ToString().Replace(HamDungChung.CurrentUser().ThongTinTruong.Oid.ToString() + ";", string.Empty);
                    //
                    split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);



                    if (split.Length > 0)
                        resultList.AddRange(split);
                }
            }
            //
            return resultList;
        }

        /// <summary>
        /// Loại bỏ bộ phận ngừng hoạt động khỏi cây thông tin nhân viên
        /// </summary>
        public static void CapNhatBoPhanNgungHoatDong(bool type)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Type", type);
            DataProvider.ExecuteNonQuery("spd_HoSo_CapNhatBoPhanNgungHoatDong", CommandType.StoredProcedure, param);
        }

        /// <summary>
        /// Lấy danh sách id của Đơn vị theo phân quyền của user hiện tại
        /// </summary>
        public static List<string> DanhSachBoPhanDuocPhanQuyen(Session session)
        {
            List<string> resultList = new List<string>();
            //
            string[] split = null;
            if (SecuritySystem.CurrentUser != null && !CheckAdministrator())
            {
                PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
                if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                {
                    if (!HamDungChung.CheckAdministrator()) //
                    permission.Quyen = permission.Quyen.ToString().Replace(HamDungChung.CurrentUser().ThongTinTruong.Oid.ToString() + ";", string.Empty);
                    //
                    split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (split.Length > 0)
                        resultList.AddRange(split);
                }
            }
            else
            {
                CriteriaOperator criteria = CriteriaOperator.Parse("NgungHoatDong=false");
                XPCollection<BoPhan> boPhanList = new XPCollection<BoPhan>(session, criteria);
                foreach (BoPhan item in boPhanList)
                {
                    resultList.Add(item.Oid.ToString());
                }
            }

            //
            return resultList;
        }

        /// <summary>
        /// Lấy danh sách id của Đơn vị theo phân quyền của user hiện tại
        /// </summary>
        public static List<string> DanhSachBoPhanDuocPhanQuyen(BoPhan boPhan)
        {
            List<string> danhSachBoPhanDuocPhanQuyenList = new List<string>();
            List<string> resultList = new List<string>();
            List<string> resultBoPhanConList = new List<string>();

            if (boPhan != null)
            {
                string[] split = null;
                if (SecuritySystem.CurrentUser != null)
                {
                    PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
                    if (permission != null && !string.IsNullOrEmpty(permission.Quyen))
                    {
                        if (!HamDungChung.CheckAdministrator()) //
                        permission.Quyen = permission.Quyen.ToString().Replace(HamDungChung.CurrentUser().ThongTinTruong.Oid.ToString() + ";", string.Empty);
                        //
                        split = permission.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        if (split.Length > 0)
                            danhSachBoPhanDuocPhanQuyenList.AddRange(split);
                    }
                }
                //Lấy danh sách kết quả
                resultList = GetTatCaBoPhanCon(boPhan, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                //
            }
            else
            {
                resultList = DanhSachBoPhanDuocPhanQuyen();
            }

            return resultList;
        }
        /// Kiểm tra tài khoản hiện tại có phải là admin hay không
        public static bool CheckAdministrator()
        {
            if ((SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan != null && (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan.IsAdministrator)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Lấy danh sách ngân hàng đang hoạt động chính
        /// </summary>
        public static List<Guid> DanhSachNganHangDangHoatDongChinh()
        {
            List<Guid> list = new List<Guid>();
            //
            DataTable dt = new DataTable();
            //
            SqlCommand cmd = DataProvider.GetCommand("spd_TaiChinh_DanhSachNganHangDangHoatDongChinh", CommandType.StoredProcedure);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    list.Add(new Guid(item["Oid"].ToString()));
                }
            }

            return list;
        }

        /// <summary>
        /// Lấy danh sách bộ phận con
        /// </summary>

        private static List<string> GetTatCaBoPhanCon(BoPhan bp, List<string> danhSachBoPhanDuocPhanQuyenList, List<string> resultBoPhanConList)
        {

            if (bp.NgungHoatDong == false && !resultBoPhanConList.Contains(bp.Oid.ToString()))
                resultBoPhanConList.Add(bp.Oid.ToString());

            //
            if (CheckAdministrator())
            {
                //tìm Đơn vị con đưa vào danh sách
                foreach (BoPhan item in bp.ListBoPhanCon)
                {
                    resultBoPhanConList.Add(item.Oid.ToString());
                    //
                    if (item.NgungHoatDong == false && item.ListBoPhanCon.Count > 0)
                        GetTatCaBoPhanCon(item, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                }
            }
            else
            {
                //tìm Đơn vị con đưa vào danh sách
                foreach (BoPhan item in bp.ListBoPhanCon)
                {
                    foreach (string boPhanPhanQuyen in danhSachBoPhanDuocPhanQuyenList)
                    {
                        if (item.Oid.ToString().Trim() == boPhanPhanQuyen.Trim())
                            resultBoPhanConList.Add(item.Oid.ToString());
                    }
                    //
                    if (item.ListBoPhanCon.Count > 0)
                        GetTatCaBoPhanCon(item, danhSachBoPhanDuocPhanQuyenList, resultBoPhanConList);
                }
            }

            return resultBoPhanConList;
        }

        /// <summary>
        /// Lấy ngày giờ hệ thống dựa trên time sql của server
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            using (SqlCommand cm = new SqlCommand("Select getdate()", DataProvider.GetConnection()))
            {
                return Convert.ToDateTime(cm.ExecuteScalar());
            }
        }

        /// <summary>
        /// Lấy ngày
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateFromString(string dateText)
        {
            DateTime date = DateTime.MinValue;
            string ngay = dateText.Substring(0, 2);
            string thang = dateText.Substring(2, 2);
            string nam = dateText.Substring(4, 4);
            try
            {
                date = Convert.ToDateTime(string.Concat(ngay,"/",thang,"/",nam));
                return date;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Sử dụng dùng để copy các property của 1 object persistent, hiện tại không copy property là 1 collection
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="session">Session của object sau khi copy</param>
        /// <param name="source">object dữ liệu copy</param>
        /// <returns></returns>
        public static T Copy<T>(Session session, T source) where T : IXPSimpleObject
        {
            T copy = (T)source.ClassInfo.CreateNewObject(session);
            foreach (XPMemberInfo m in source.ClassInfo.PersistentProperties)
            {
                if (m is DevExpress.Xpo.Metadata.Helpers.ServiceField || m.IsKey)
                    continue;
                if (m.ReferenceType != null)
                {
                    if (m.GetValue(source) != null)
                        m.SetValue(copy, session.GetObjectByKey(m.ReferenceType, source.Session.GetKeyValue(m.GetValue(source))));
                }
                else
                    m.SetValue(copy, m.GetValue(source));
            }
            return copy;
        }

        /// <summary>
        /// Tạo chữ viết tắt từ một chuỗi phân cách bởi dấu space " "
        /// </summary>
        /// <param name="stChuoiCanVietTat"></param>
        public static string TaoChuVietTat(string stChuoiCanVietTat)
        {
            StringBuilder sb = new StringBuilder();
            string[] CacTu = stChuoiCanVietTat.Trim().Split(' ');
            foreach (string stTu in CacTu)
            {
                sb.Append(stTu.Substring(0, 1).ToUpper());
            }
            return sb.ToString();
        }

        public static string VietHoaChuDau(string[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string item in value)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    sb.Append(item.Substring(0, 1).ToUpper());
                    sb.Append(item.Substring(1).ToLower());
                    sb.Append(" ");
                }
            }

            return sb.ToString().Trim();
        }

        public static void WriteLog(string path, string data)
        {
            StreamWriter writer = new StreamWriter(path);
            writer.WriteLine(data);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }


        /// <summary>
        /// Đọc số tiền
        /// </summary>
        /// <param name="NumCurrency">số tiền</param>
        /// <returns></returns>
        public static string DocTien(decimal NumCurrency)
        {
            string SoRaChu = "";
            NumCurrency = Math.Abs(NumCurrency);
            if (NumCurrency == 0)
            {
                SoRaChu = "Không đồng";
                return SoRaChu;
            }

            string[] CharVND = new string[10] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string BangChu;
            int I;
            //As String, BangChu As String, I As Integer
            int SoLe, SoDoi;
            string PhanChan, Ten;
            string DonViTien, DonViLe;
            int NganTy, Ty, Trieu, Ngan;
            int Dong, Tram, Muoi, DonVi;

            SoDoi = 0;
            Muoi = 0;
            Tram = 0;
            DonVi = 0;

            Ten = "";
            DonViTien = "đồng";
            DonViLe = "xu";


            SoLe = (int)((NumCurrency - (Int64)NumCurrency) * 100); //'2 kí so^' le?
            PhanChan = ((Int64)NumCurrency).ToString().Trim();

            int khoangtrang = 15 - PhanChan.Length;
            for (int i = 0; i < khoangtrang; i++)
                PhanChan = "0" + PhanChan;

            NganTy = int.Parse(PhanChan.Substring(0, 3));
            Ty = int.Parse(PhanChan.Substring(3, 3));
            Trieu = int.Parse(PhanChan.Substring(6, 3));
            Ngan = int.Parse(PhanChan.Substring(9, 3));
            Dong = int.Parse(PhanChan.Substring(12, 3));

            if (NganTy == 0 & Ty == 0 & Trieu == 0 & Ngan == 0 & Dong == 0)
            {
                BangChu = String.Format("không {0} ", DonViTien);
                I = 5;
            }
            else
            {
                BangChu = "";
                I = 0;
            }

            while (I <= 5)
            {
                switch (I)
                {
                    case 0:
                        SoDoi = NganTy;
                        Ten = "ngàn tỷ";
                        break;
                    case 1:
                        SoDoi = Ty;
                        Ten = "tỷ";
                        break;
                    case 2:
                        SoDoi = Trieu;
                        Ten = "triệu";
                        break;
                    case 3:
                        SoDoi = Ngan;
                        Ten = "ngàn";
                        break;
                    case 4:
                        SoDoi = Dong;
                        Ten = DonViTien;
                        break;
                    case 5:
                        SoDoi = SoLe;
                        Ten = DonViLe;
                        break;
                }

                if (SoDoi != 0)
                {
                    Tram = (int)(SoDoi / 100);
                    Muoi = (int)((SoDoi - Tram * 100) / 10);
                    DonVi = (SoDoi - Tram * 100) - Muoi * 10;
                    BangChu = BangChu.Trim() + (BangChu.Length == 0 ? "" : " ") + (Tram != 0 ? CharVND[Tram].Trim() + " trăm " : "");
                    if (Muoi == 0 & Tram == 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "không trăm lẻ ";
                    else if (Muoi != 0 & Tram == 0 & (DonVi == 0 || DonVi != 0) & BangChu != "")
                        BangChu = BangChu + "không trăm ";
                    else if (Muoi == 0 & Tram != 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "lẻ ";
                    if (Muoi != 0 & Muoi > 0)
                        BangChu = BangChu + ((Muoi != 0 & Muoi != 1) ? CharVND[Muoi].Trim() + " mươi " : "mười ");

                    if (Muoi != 0 & DonVi == 5)
                        BangChu = String.Format("{0}lăm {1} ", BangChu, Ten);
                    else if (Muoi > 1 & DonVi == 1)
                        BangChu = String.Format("{0}mốt {1} ", BangChu, Ten);
                    else
                        BangChu = BangChu + ((DonVi != 0) ? String.Format("{0} {1} ", CharVND[DonVi].Trim(), Ten) : Ten + " ");
                }
                else
                    BangChu = BangChu + ((I == 4) ? DonViTien + " " : "");

                I = I + 1;
            }

            BangChu = BangChu[0].ToString().ToUpper() + BangChu.Substring(1);
            SoRaChu = BangChu;
            return SoRaChu;
        }

        /// <summary>
        /// Đọc số tiền
        /// </summary>
        /// <param name="NumCurrency">số tiền</param>
        /// <returns></returns>
        public static string DocTienEnglish(decimal NumCurrency)
        {
            string SoRaChu = "";
            NumCurrency = Math.Abs(NumCurrency);
            if (NumCurrency == 0)
            {
                SoRaChu = "zero dong";
                return SoRaChu;
            }

            string[] CharVND = new string[10] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string BangChu;
            int I;
            //As String, BangChu As String, I As Integer
            int SoLe, SoDoi;
            string PhanChan, Ten;
            string DonViTien, DonViLe;
            int NganTy, Ty, Trieu, Ngan;
            int Dong, Tram, Muoi, DonVi;

            SoDoi = 0;
            Muoi = 0;
            Tram = 0;
            DonVi = 0;

            Ten = "";
            DonViTien = "dong";
            DonViLe = "xu";


            SoLe = (int)((NumCurrency - (Int64)NumCurrency) * 100); //'2 kí so^' le?
            PhanChan = ((Int64)NumCurrency).ToString().Trim();

            int khoangtrang = 15 - PhanChan.Length;
            for (int i = 0; i < khoangtrang; i++)
                PhanChan = "0" + PhanChan;

            NganTy = int.Parse(PhanChan.Substring(0, 3));
            Ty = int.Parse(PhanChan.Substring(3, 3));
            Trieu = int.Parse(PhanChan.Substring(6, 3));
            Ngan = int.Parse(PhanChan.Substring(9, 3));
            Dong = int.Parse(PhanChan.Substring(12, 3));

            if (NganTy == 0 & Ty == 0 & Trieu == 0 & Ngan == 0 & Dong == 0)
            {
                BangChu = String.Format("không {0} ", DonViTien);
                I = 5;
            }
            else
            {
                BangChu = "";
                I = 0;
            }

            while (I <= 5)
            {
                switch (I)
                {
                    case 0:
                        SoDoi = NganTy;
                        Ten = "thousand billion";
                        break;
                    case 1:
                        SoDoi = Ty;
                        Ten = "billion";
                        break;
                    case 2:
                        SoDoi = Trieu;
                        Ten = "million";
                        break;
                    case 3:
                        SoDoi = Ngan;
                        Ten = "thousand";
                        break;
                    case 4:
                        SoDoi = Dong;
                        Ten = DonViTien;
                        break;
                    case 5:
                        SoDoi = SoLe;
                        Ten = DonViLe;
                        break;
                }

                if (SoDoi != 0)
                {
                    Tram = (int)(SoDoi / 100);
                    Muoi = (int)((SoDoi - Tram * 100) / 10);
                    DonVi = (SoDoi - Tram * 100) - Muoi * 10;
                    BangChu = BangChu.Trim() + (BangChu.Length == 0 ? "" : " ") + (Tram != 0 ? CharVND[Tram].Trim() + " hundred " : "");
                    if (Muoi == 0 & Tram == 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "not hundred and ";
                    else if (Muoi != 0 & Tram == 0 & (DonVi == 0 || DonVi != 0) & BangChu != "")
                        BangChu = BangChu + "not hundred ";
                    else if (Muoi == 0 & Tram != 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "and ";
                    if (Muoi != 0 & Muoi == 1)
                        BangChu = BangChu +  " ten ";
                    else if (Muoi != 0 & Muoi == 2)
                        BangChu = BangChu + " twenty ";
                    else if (Muoi != 0 & Muoi == 3)
                        BangChu = BangChu + " thirty ";
                    else if (Muoi != 0 & Muoi == 5)
                        BangChu = BangChu + " fifty ";
                    else if (Muoi != 0 & Muoi == 8)
                        BangChu = BangChu + " eighty ";
                    else if (Muoi != 0)
                        BangChu = BangChu + String.Format("{0}{1} ", CharVND[Muoi].Trim(), "ty");
                    if (Muoi != 0 & DonVi == 1)
                        BangChu = String.Format("{0}and {1} ", BangChu, "ten");                  
                    else
                        BangChu = BangChu + ((DonVi != 0) ? String.Format("{0} {1} ", CharVND[DonVi].Trim(), Ten) : Ten + " ");
                }
                else
                    BangChu = BangChu + ((I == 4) ? DonViTien + " " : "");

                I = I + 1;
            }

            BangChu = BangChu[0].ToString().ToUpper() + BangChu.Substring(1);
            SoRaChu = BangChu;
            return SoRaChu;
        }

        /// <summary>
        /// Đọc số tiền USD
        /// </summary>
        /// <param name="NumCurrency">số tiền</param>
        /// <returns></returns>
        public static string DocTienUSD(decimal NumCurrency)
        {
            string SoRaChu = "";
            NumCurrency = Math.Abs(NumCurrency);
            if (NumCurrency == 0)
            {
                SoRaChu = "Không đô la";
                return SoRaChu;
            }

            string[] CharVND = new string[10] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string BangChu;
            int I;
            //As String, BangChu As String, I As Integer
            int SoLe, SoDoi;
            string PhanChan, Ten;
            string DonViTien, DonViLe;
            int NganTy, Ty, Trieu, Ngan;
            int Dong, Tram, Muoi, DonVi;

            SoDoi = 0;
            Muoi = 0;
            Tram = 0;
            DonVi = 0;

            Ten = "";
            DonViTien = "đô la";
            DonViLe = "xu";


            SoLe = (int)((NumCurrency - (Int64)NumCurrency) * 100); //'2 kí so^' le?
            PhanChan = ((Int64)NumCurrency).ToString().Trim();

            int khoangtrang = 15 - PhanChan.Length;
            for (int i = 0; i < khoangtrang; i++)
                PhanChan = "0" + PhanChan;

            NganTy = int.Parse(PhanChan.Substring(0, 3));
            Ty = int.Parse(PhanChan.Substring(3, 3));
            Trieu = int.Parse(PhanChan.Substring(6, 3));
            Ngan = int.Parse(PhanChan.Substring(9, 3));
            Dong = int.Parse(PhanChan.Substring(12, 3));

            if (NganTy == 0 & Ty == 0 & Trieu == 0 & Ngan == 0 & Dong == 0)
            {
                BangChu = String.Format("không {0} ", DonViTien);
                I = 5;
            }
            else
            {
                BangChu = "";
                I = 0;
            }

            while (I <= 5)
            {
                switch (I)
                {
                    case 0:
                        SoDoi = NganTy;
                        Ten = "ngàn tỷ";
                        break;
                    case 1:
                        SoDoi = Ty;
                        Ten = "tỷ";
                        break;
                    case 2:
                        SoDoi = Trieu;
                        Ten = "triệu";
                        break;
                    case 3:
                        SoDoi = Ngan;
                        Ten = "ngàn";
                        break;
                    case 4:
                        SoDoi = Dong;
                        Ten = DonViTien;
                        break;
                    case 5:
                        SoDoi = SoLe;
                        Ten = DonViLe;
                        break;
                }

                if (SoDoi != 0)
                {
                    Tram = (int)(SoDoi / 100);
                    Muoi = (int)((SoDoi - Tram * 100) / 10);
                    DonVi = (SoDoi - Tram * 100) - Muoi * 10;
                    BangChu = BangChu.Trim() + (BangChu.Length == 0 ? "" : " ") + (Tram != 0 ? CharVND[Tram].Trim() + " trăm " : "");
                    if (Muoi == 0 & Tram == 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "không trăm lẻ ";
                    else if (Muoi != 0 & Tram == 0 & (DonVi == 0 || DonVi != 0) & BangChu != "")
                        BangChu = BangChu + "không trăm ";
                    else if (Muoi == 0 & Tram != 0 & DonVi != 0 & BangChu != "")
                        BangChu = BangChu + "lẻ ";
                    if (Muoi != 0 & Muoi > 0)
                        BangChu = BangChu + ((Muoi != 0 & Muoi != 1) ? CharVND[Muoi].Trim() + " mươi " : "mười ");

                    if (Muoi != 0 & DonVi == 5)
                        BangChu = String.Format("{0}lăm {1} ", BangChu, Ten);
                    else if (Muoi > 1 & DonVi == 1)
                        BangChu = String.Format("{0}mốt {1} ", BangChu, Ten);
                    else
                        BangChu = BangChu + ((DonVi != 0) ? String.Format("{0} {1} ", CharVND[DonVi].Trim(), Ten) : Ten + " ");
                }
                else
                    BangChu = BangChu + ((I == 4) ? DonViTien + " " : "");

                I = I + 1;
            }

            BangChu = BangChu[0].ToString().ToUpper() + BangChu.Substring(1);
            SoRaChu = BangChu;
            return SoRaChu;
        }

        public static string DocSo(string number)
        {
            if (number.Contains("."))
                number = number.Replace(".", "");

            Regex regex = new Regex(@"^\d+(\,?\d+)?$");
            if (regex.IsMatch(number))
            {
                string[] split = number.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (split.Length == 2)
                    return String.Format("{0}phẩy {1}", ChuyenSo(split[0]), ChuyenSo(split[1]));
                else
                    return ChuyenSo(number);
            }
            return "";
        }

        private static string ChuyenSo(string number)
        {
            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỷ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv;
            bool state = false;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3)
                                    doc += RemoveEmptyString(cs[0]);
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0')
                                        doc += "lẻ ";
                                    ddv = 0;
                                }
                                break;
                            case '1':
                                if (n - j == 3)
                                    doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0)
                                        k = 0;
                                    else
                                        k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += RemoveEmptyString(cs[1]);
                                }
                                break;
                            case '5':
                                if (j == n - 1)
                                    doc += "lăm ";
                                else
                                    doc += RemoveEmptyString(cs[5]);
                                break;
                            default:
                                doc += RemoveEmptyString(cs[(int)number[i + j] - 48]);
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += RemoveEmptyString(dv[n - j - 1]);
                        }
                    }
                }


                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (found != 0)
                        {
                            state = true;
                            for (k = 0; k < (len - i - n) / 9; k++)
                            {
                                doc += "tỷ ";
                            }
                        }
                        else
                        {
                            if (!state)
                                doc += "tỷ ";
                        }
                    }
                    else
                        if (found != 0)
                            doc += RemoveEmptyString(dv[((len - i - n + 1) % 9) / 3 + 2]);
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5')
                    return cs[(int)number[0] - 48] + " ";

            return doc;
        }

        private static string RemoveEmptyString(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return input + " ";
            return "";
        }

        /// <summary>
        /// Set Time for DateTime value (use in Tax calculator)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="style">0: start hour of day, 1: end hour of day, 2: start day of month, 3: end day of month, 4: start month of year, 5: end month of year</param>
        /// <returns></returns>
        public static DateTime SetTime(DateTime source, int style)
        {
            int hh, mm, ss;
            if (style == 0)
            {
                hh = source.Hour;
                mm = source.Minute;
                ss = source.Second;

                source = source.AddHours(-hh);
                source = source.AddMinutes(-mm);
                source = source.AddSeconds(-ss);
            }
            else if (style == 1)
            {
                hh = 23 - source.Hour;
                mm = 59 - source.Minute;
                ss = 59 - source.Second;

                source = source.AddHours(hh);
                source = source.AddMinutes(mm);
                source = source.AddSeconds(ss);
            }
            else if (style == 2)
            {
                source = new DateTime(source.Year, source.Month, 1);
                source = SetTime(source, 0);
            }
            else if (style == 3)
            {
                source = new DateTime(source.Year, source.Month, 1).AddMonths(1).AddDays(-1);
                source = SetTime(source, 1);
            }
            else if (style == 4)
            {
                source = new DateTime(source.Year, 1, 1);
                source = SetTime(source, 0);
            }
            else if (style == 5)
            {
                source = new DateTime(source.Year, 12, 31);
                source = SetTime(source, 1);
            }


            return source;
        }

        /// <summary>
        /// Init grid lookup edit
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="autoFilter"></param>
        /// <param name="autoPopup"></param>
        /// <param name="textEditStyle"></param>
        public static void InitGridLookUp(GridLookUpEdit grid, bool autoFilter, bool autoPopup, TextEditStyles textEditStyle)
        {
            //Show filter
            grid.Properties.View.OptionsView.ShowAutoFilterRow = autoFilter;
            grid.Properties.TextEditStyle = textEditStyle;
            grid.Properties.ImmediatePopup = autoPopup;
            grid.Properties.PopupFilterMode = PopupFilterMode.Contains;
            //grid.Properties.BestFitMode = BestFitMode.BestFit;
            for (int i = 0; i < grid.Properties.View.Columns.Count; i++)
                grid.Properties.View.Columns[i].Visible = false;
        }

        /// <summary>
        /// Init GridView
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="autoFilter"></param>
        /// <param name="multiSelect"></param>
        /// <param name="selectMode"></param>
        /// <param name="detailButton"></param>
        /// <param name="groupPanel"></param>
        public static void InitGridView(GridView grid, bool autoFilter, bool multiSelect, GridMultiSelectMode selectMode, bool detailButton, bool groupPanel)
        {
            //Show filter
            grid.OptionsView.ShowAutoFilterRow = autoFilter;
            //Show multi select
            grid.OptionsSelection.MultiSelect = multiSelect;
            //Show multi select mode
            grid.OptionsSelection.MultiSelectMode = selectMode;
            //Show detail button
            grid.OptionsView.ShowDetailButtons = detailButton;
            grid.OptionsView.ShowChildrenInGroupPanel = detailButton;
            grid.OptionsDetail.ShowDetailTabs = detailButton;
            //Show group panel
            grid.OptionsView.ShowGroupPanel = groupPanel;

            for (int i = 0; i < grid.Columns.Count; i++)
                grid.Columns[i].Visible = false;
        }

        /// <summary>
        /// Show field with caption, width
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(GridView grid, string[] fieldName, string[] caption)
        {
            grid.OptionsView.ColumnAutoWidth = true;
            for (int i = 0; i < fieldName.Length; i++)
            {
                grid.Columns.AddField(fieldName[i]);
                grid.Columns[fieldName[i]].Visible = true;
                grid.Columns[fieldName[i]].Caption = caption[i];
                grid.Columns[fieldName[i]].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            }
        }

        /// <summary>
        /// Init popup form
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public static void InitPopupFormSize(GridLookUpEdit grid, int width, int height)
        {
            grid.Properties.PopupFormSize = new Size(width, height);
        }
        private static readonly string[] VietNamChar = new string[] 
            { 
                "aAeEoOuUiIdDyY", 
                "áàạảãâấầậẩẫăắằặẳẵ", 
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", 
                "éèẹẻẽêếềệểễ", 
                "ÉÈẸẺẼÊẾỀỆỂỄ", 
                "óòọỏõôốồộổỗơớờợởỡ", 
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", 
                "úùụủũưứừựửữ", 
                "ÚÙỤỦŨƯỨỪỰỬỮ", 
                "íìịỉĩ", 
                "ÍÌỊỈĨ", 
                "đ", 
                "Đ", 
                "ýỳỵỷỹ", 
                "ÝỲỴỶỸ" 
            };
        public static string BoDauTiengViet(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
        #region List danh sách
        public static XPCollection<BoPhanView> getBoPhan(Session session)
        {
            XPCollection<BoPhanView> listBoPhan = new XPCollection<BoPhanView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách đơn vị", "Vui lòng chờ...."))
            {
                PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
                string sql = "SELECT bp.Oid,bp.TenBoPhan";
                sql += " FROM dbo.func_SplitString('" + permission.Quyen + "',';') ds";
                sql += " JOIN dbo.BoPhan bp ON bp.Oid = ds.VALUE";
                sql += " ORDER BY bp.BoPhanCha, bp.TenBoPhan";
                DataTable dtBoPhan = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dtBoPhan != null)
                {
                    foreach (DataRow item in dtBoPhan.Rows)
                    {
                        BoPhanView bp = new BoPhanView(session);
                        bp.OidBoPhan = new Guid(item["Oid"].ToString());
                        bp.TenBoPhan = item["TenBoPhan"].ToString();
                        listBoPhan.Add(bp);
                    }
                }
            }
            return listBoPhan;
        }
        public static XPCollection<BoPhanView> getBoPhanThinhGiang(Session session)
        {
            XPCollection<BoPhanView> listBoPhan = new XPCollection<BoPhanView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách đơn vị", "Vui lòng chờ...."))
            {
                PhanQuyenDonVi permission = (SecuritySystem.CurrentUser as NguoiSuDung).PhanQuyenBoPhan;
                string sql = "SELECT bp.Oid,bp.TenBoPhan";
                sql += " FROM dbo.func_SplitString('" + permission.Quyen + "',';') ds";
                sql += " JOIN dbo.BoPhan bp ON bp.Oid = ds.VALUE";
                sql += " JOIN dbo.NhanVien NhanVien ON bp.Oid = NhanVien.BoPhan";
                sql += " JOIN dbo.GiangVienThinhGiang GiangVienThinhGiang ON GiangVienThinhGiang.Oid = NhanVien.Oid";
                sql += " GROUP BY bp.TenBoPhan, bp.Oid, bp.BoPhanCha";
                sql += " ORDER BY bp.BoPhanCha, bp.TenBoPhan";
                DataTable dtBoPhan = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dtBoPhan != null)
                {
                    foreach (DataRow item in dtBoPhan.Rows)
                    {
                        BoPhanView bp = new BoPhanView(session);
                        bp.OidBoPhan = new Guid(item["Oid"].ToString());
                        bp.TenBoPhan = item["TenBoPhan"].ToString();
                        listBoPhan.Add(bp);
                    }
                }
            }
            return listBoPhan;
        }
        public static XPCollection<BoPhanView> getBoPhanChuyenGia(Session session)
        {
            XPCollection<BoPhanView> listBoPhan = new XPCollection<BoPhanView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách đơn vị", "Vui lòng chờ...."))
            {
                string sql = "SELECT bp.Oid,bp.TenBoPhan";
                sql += " FROM dbo.BoPhan bp";
                sql += " JOIN dbo.BoPhan_ChuyenGiaTheoGio bpchuyengia ON bp.Oid = bpchuyengia.BoPhan";
                sql += " ORDER BY bp.BoPhanCha, bp.TenBoPhan";
                DataTable dtBoPhan = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dtBoPhan != null)
                {
                    foreach (DataRow item in dtBoPhan.Rows)
                    {
                        BoPhanView bp = new BoPhanView(session);
                        bp.OidBoPhan = new Guid(item["Oid"].ToString());
                        bp.TenBoPhan = item["TenBoPhan"].ToString();
                        listBoPhan.Add(bp);
                    }
                }
            }
            return listBoPhan;
        }
        public static XPCollection<BoPhanView> getBoPhanKhongChuyenGia(Session session)
        {
            XPCollection<BoPhanView> listBoPhan = new XPCollection<BoPhanView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách đơn vị", "Vui lòng chờ...."))
            {
                string sql = "SELECT bp.Oid,bp.TenBoPhan";
                sql += " FROM dbo.BoPhan bp";
                sql += " WHERE bp.Oid NOT IN (SELECT BoPhan FROM dbo.BoPhan_ChuyenGiaTheoGio)";
                sql += " ORDER BY bp.BoPhanCha, bp.TenBoPhan";
                DataTable dtBoPhan = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dtBoPhan != null)
                {
                    foreach (DataRow item in dtBoPhan.Rows)
                    {
                        BoPhanView bp = new BoPhanView(session);
                        bp.OidBoPhan = new Guid(item["Oid"].ToString());
                        bp.TenBoPhan = item["TenBoPhan"].ToString();
                        listBoPhan.Add(bp);
                    }
                }
            }
            return listBoPhan;
        }
        public static XPCollection<NhanVienView> getNhanVien(Session session, Guid OidBoPhan)
        {
            XPCollection<NhanVienView> listNhanVien = new XPCollection<NhanVienView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách nhân viên", "Vui lòng chờ...."))
            {
                string sql = "SELECT HoSo.Oid, HoTen,CMND, ISNULL(TenBoPhan,'') AS TenBoPhan,ISNULL(TenTinhTrang,'') AS TinhTrang";
                sql += " FROM dbo.HoSo";
                sql += " JOIN dbo.NhanVien ON NhanVien.Oid = HoSo.Oid";
                sql += " LEFT JOIN dbo.BoPhan ON BoPhan.Oid = NhanVien.BoPhan";
                sql += " LEFT JOIN dbo.TinhTrang ON TinhTrang.Oid = NhanVien.TinhTrang";
                sql += " WHERE HoSo.GCRecord IS NULL";
                sql += " AND  BoPhan='" + OidBoPhan + "' OR '" + OidBoPhan + "'='00000000-0000-0000-0000-000000000000'";
                DataTable dsNhanVien = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dsNhanVien != null)
                {
                    foreach (DataRow item in dsNhanVien.Rows)
                    {
                        NhanVienView nv = new NhanVienView(session);
                        nv.OidNhanVien = new Guid(item["Oid"].ToString());
                        nv.HoTen = item["HoTen"].ToString();
                        nv.TenBoPhan = item["TenBoPhan"].ToString();
                        nv.CMND = item["CMND"].ToString();
                        nv.TinhTrang = item["TinhTrang"].ToString();
                        listNhanVien.Add(nv);
                    }
                }
            }
            return listNhanVien;
        }
        public static XPCollection<NhanVienView> getNhanVienThinhGiang(Session session, Guid OidBoPhan)
        {
            XPCollection<NhanVienView> listNhanVien = new XPCollection<NhanVienView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách nhân viên", "Vui lòng chờ...."))
            {
                string sql = "SELECT HoSo.Oid, HoTen,CMND, ISNULL(TenBoPhan,'') AS TenBoPhan,ISNULL(TenTinhTrang,'') AS TinhTrang";
                sql += " FROM dbo.HoSo";
                sql += " JOIN dbo.NhanVien ON NhanVien.Oid = HoSo.Oid";
                sql += " JOIN dbo.GiangVienThinhGiang ON NhanVien.Oid = GiangVienThinhGiang.Oid";
                sql += " LEFT JOIN dbo.BoPhan ON BoPhan.Oid = NhanVien.BoPhan";
                sql += " LEFT JOIN dbo.TinhTrang ON TinhTrang.Oid = NhanVien.TinhTrang";
                sql += " WHERE HoSo.GCRecord IS NULL";
                sql += " AND  BoPhan='" + OidBoPhan + "' OR '" + OidBoPhan + "'='00000000-0000-0000-0000-000000000000'";
                DataTable dsNhanVien = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dsNhanVien != null)
                {
                    foreach (DataRow item in dsNhanVien.Rows)
                    {
                        NhanVienView nv = new NhanVienView(session);
                        nv.OidNhanVien = new Guid(item["Oid"].ToString());
                        nv.HoTen = item["HoTen"].ToString();
                        nv.TenBoPhan = item["TenBoPhan"].ToString();
                        nv.CMND = item["CMND"].ToString();
                        nv.TinhTrang = item["TinhTrang"].ToString();
                        listNhanVien.Add(nv);
                    }
                }
            }
            return listNhanVien;
        }
        #endregion

        public static void SaveFilePDF(byte[] File, string strTenFile)
        {
            try
            {
                FileStream fs = new FileStream(strTenFile, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter w = new BinaryWriter(fs);
                w.Flush();
                w.Write(File);
                w.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
