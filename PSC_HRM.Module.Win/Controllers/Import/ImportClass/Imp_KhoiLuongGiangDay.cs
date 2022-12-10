using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.Import.ImportClass
{
    public class Imp_KhoiLuongGiangDay
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, KhoiLuongGiangDay OidQuanLy, Guid _BacDaoTao, Guid _HeDaoTao)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        //Loại file
                        //LoaiOfficeEnum loaiOffice = LoaiOfficeEnum.Office2003;
                        //if (open.SafeFileName.Contains(".xlsx"))
                        //{ loaiOffice = LoaiOfficeEnum.Office2010; }
                        if (TruongConfig.MaTruong == "UFM")
                        {
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[KhoiLuongGiangDay$A1:X]"))
                            {
                                #region Khởi tạo các Idx

                                const int idTenMonHoc = 2;
                                const int idSoTC = 3;
                                const int idSoTiet = 4;
                                const int idCoSo = 5;
                                const int idPhong = 6;
                                const int idNgayBD = 7;
                                const int idNgayKT = 8;
                                const int idLopHocPhan = 9;
                                const int idLop = 10;
                                const int idGhiChu = 11;
                                const int idLopGhep = 12;
                                const int idSiSo = 13;
                                //const int idHeSoDaoTaoQuyDoi = 14;
                                const int idHeSoNgoaiGio = 15;
                                //const int idHeSoCongThem = 16;
                                const int idHeSoLopDong = 14;
                                //const int idSoTietQuyDoi = 18;
                                const int idMaGiangVien = 16;
                                const int idHoTen = 19;
                                const int idHocKy = 21;

                                #endregion
                                 using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                     int STT=0;
                                    uow.BeginTransaction();
                                    #region Khai Báo
                                        string sql = "";
                                        string TenMonHoc = "";
                                        string SoTC = "";
                                        string SoTiet = "";
                                        string CoSo = "";
                                        string Phong = "";
                                        string NgayBD = "";
                                        string NgayKT = "";
                                        string LopHocPhan = "";
                                        string LopSinhVien = "";
                                        string GhiChu = "";
                                        string MaGhepLop = "";
                                        string SiSo = "";
                                        //string HeSoDaoTaoQuyDoi = "";
                                        string HeSoNgoaiGio = "";
                                        //string HeSoCongThem = "";
                                        string HeSoLopDong = "";
                                        //string SoTietQuyDoi = "";
                                        string MaGiangVien = "";
                                        string HoTenGiangVien = "";
                                        string HocKy = "";
                                        NhanVien nhanVien = null;
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (OidQuanLy != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();

                                            #region ĐỌC DỮ LIỆU
                                            if (dr[idTenMonHoc] != string.Empty)
                                                TenMonHoc = dr[idTenMonHoc].ToString().Replace(",", ".");
                                            if (dr[idSoTC] != string.Empty)
                                                SoTC = dr[idSoTC].ToString().Replace(",", ".");
                                            if (dr[idSoTiet] != string.Empty)
                                                SoTiet = dr[idSoTiet].ToString().Replace(",", ".");
                                            if (dr[idCoSo] != string.Empty)
                                                CoSo = dr[idCoSo].ToString().Replace(",", ".");
                                            if (dr[idPhong] != string.Empty)
                                                Phong = dr[idPhong].ToString().Replace(",", ".");
                                            if (dr[idNgayBD] != string.Empty)
                                                NgayBD = dr[idNgayBD].ToString().Replace(",", ".");
                                            if (dr[idNgayKT] != string.Empty)
                                                NgayKT = dr[idNgayKT].ToString().Replace(",", ".");
                                            if (dr[idLopHocPhan] != string.Empty)
                                                LopHocPhan = dr[idLopHocPhan].ToString().Replace(",", ".");
                                            if (dr[idLop] != string.Empty)
                                                LopSinhVien = dr[idLop].ToString().Replace(",", ".");
                                            if (dr[idGhiChu] != string.Empty)
                                                GhiChu = dr[idGhiChu].ToString().Replace(",", ".");
                                            if (dr[idLopGhep] != string.Empty)
                                                MaGhepLop = dr[idLopGhep].ToString().Replace(",", ".");
                                            if (dr[idSiSo] != string.Empty)
                                                SiSo = dr[idSiSo].ToString().Replace(",", ".");
                                            //if (dr[idHeSoDaoTaoQuyDoi] != string.Empty)
                                            //    HeSoDaoTaoQuyDoi = dr[idHeSoDaoTaoQuyDoi].ToString().Replace(",", ".");
                                            if (dr[idHeSoNgoaiGio] != string.Empty)
                                                HeSoNgoaiGio = dr[idHeSoNgoaiGio].ToString().Replace(",", ".");
                                            //if (dr[idHeSoCongThem] != string.Empty)
                                            //    HeSoCongThem = dr[idHeSoCongThem].ToString().Replace(",", ".");
                                            if (dr[idHeSoLopDong] != string.Empty)
                                                HeSoLopDong = dr[idHeSoLopDong].ToString().Replace(",", ".");
                                            //if (dr[idSoTietQuyDoi] != string.Empty)
                                            //    SoTietQuyDoi = dr[idSoTietQuyDoi].ToString().Replace(",", ".");
                                            if (dr[idMaGiangVien] != string.Empty)
                                                MaGiangVien = dr[idMaGiangVien].ToString().Replace(",", ".");
                                            if (dr[idHoTen] != string.Empty)
                                                HoTenGiangVien = dr[idHoTen].ToString().Replace(",", ".");
                                            if (dr[idHocKy] != string.Empty)
                                                HocKy = dr[idHocKy].ToString().Replace(",", ".");
                                            #endregion

                                            #region KIỂM TRA VÀ LẤY DỮ LIỆU
                                            if (MaGiangVien != string.Empty)
                                            {
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", MaGiangVien);
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                                if (nhanVien != null)
                                                {
                                                    sql += " Union All select N'" + MaGiangVien + "' as MaQuanLy"
                                                                    + ", N'" + HoTenGiangVien + " " + HoTenGiangVien + "' as HoTen"
                                                                    + ", N'" + TenMonHoc + "' as TenMonHoc"
                                                                    + ", N'" + SoTC + "' as SoTinChi"
                                                                    + ", N'" + SoTiet + "' as SoTiet"
                                                                    + ", N'" + CoSo + "' as CoSo"
                                                                    + ", N'" + Phong + "' as PhongHoc"
                                                                    + ", N'" + NgayBD + "' as NgayBD"
                                                                    + ", N'" + NgayKT + "' as NgayKT"
                                                                    + ", N'" + LopHocPhan + "' as LopHocPhan"
                                                                    + ", N'" + LopSinhVien + "' as LopSinhVien"
                                                                    + ", N'" + GhiChu + "' as GhiChu"
                                                                    + ", N'" + MaGhepLop + "' as MaGhepLop"
                                                                    + ", N'" + SiSo + "' as SiSo"
                                                                    //+ ", N'" + HeSoDaoTaoQuyDoi + "' as HeSoDaoTaoQuyDoi"
                                                                    + ", N'" + HeSoNgoaiGio + "' as HeSoNgoaiGio"
                                                                    //+ ", N'" + HeSoCongThem + "' as HeSoCongThem"
                                                                    + ", N'" + HeSoLopDong + "' as HeSoLopDong"
                                                                    //+ ", N'" + SoTietQuyDoi + "' as SoTietQuyDoi"
                                                                    + ", N'" + HocKy + "' as HocKy"
                                                                    + ", N'" + _BacDaoTao + "' as BacDaoTao"
                                                                    + ", N'" + _HeDaoTao + "' as HeDaoTao";
                                                }
                                                else
                                                {
                                                    erorrNumber++;
                                                    errorLog.AppendLine("- STT: " + MaGiangVien + " - " +HoTenGiangVien + " Không tồn tại nhân viên.");
                                                }
                                            }
                                            #endregion

                                            #region Ghi File log
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////     
                                            if (sucessImport)
                                            {
                                                uow.CommitChanges();////Lưu                                        
                                                sucessNumber++;
                                            }
                                            else
                                            {
                                                uow.RollbackTransaction(); ////trả lại dữ liệu ban đầu
                                                erorrNumber++;
                                                sucessImport = true;
                                            }
                                            #endregion
                                        }
                                    }

                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        //uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                        DialogUtil.ShowInfo("Số dòng không thành công " + erorrNumber + " !");
                                    }
                                    else
                                    {
                                        SqlParameter[] pImport = new SqlParameter[2];
                                        pImport[0] = new SqlParameter("@KhoiLuongGiangDay", OidQuanLy.Oid);
                                        pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                        DataProvider.GetValueFromDatabase("spd_PMS_Import_KhoiLuongGiangDay", CommandType.StoredProcedure, pImport); uow.CommitChanges();//Lưu
                                    }
                                }
                            }
                        }
                        else if (TruongConfig.MaTruong == "QNU")
                        {
                            #region Trường QNU
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[GIODAY$A8:AK]"))
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các Idx

                                const int idMaKhoa = 0;
                                const int idHoTen = 1;
                                const int idMaNhanVien = 2;
                                const int idTT = 3;
                                const int idMaHP = 4;
                                const int idTenHP = 5;
                                const int idHK = 6;
                                const int idSoTC = 7;
                                const int idSoTietLyThuyet = 8;
                                const int idSoTietThaoLuan = 9;
                                const int idSoTietTNTH = 10;
                                const int idSoBaiTNTH = 11;
                                const int idSoGioTNTH = 12;
                                const int idLoaiNhomTNTH = 13;
                                const int idSoNhomTNTH = 14;
                                const int idSoLuongDoAn = 15;
                                const int idSoLuongBTL = 16;
                                const int idLopHP = 17;
                                const int idKhoaQLHP = 18;
                                const int idSoLuongSV = 19;
                                const int idHeSoLopDong = 20;
                                const int idHeSoTinhChi = 21;
                                const int idHeSoChucDanh = 22;
                                const int idHeSoHocKy = 23;
                                const int idTongHeSo = 24;
                                const int idQuyDoiLyThuyet = 25;
                                const int idQuyDoiThaoLuan = 26;
                                const int idTongGioLT_TH = 27;
                                const int idQuyDoiTNTH = 28;
                                const int idQuyDoiChamBaiTNTH = 29;
                                const int idQuyDoiDoAn = 30;
                                const int idQuyDoiBTL = 31;
                                const int idTongGioTNTH_DA_BTL = 32;
                                const int idLoaiHeSoLopDong = 33;
                                const int idMaKhoaHocQuanLy = 34;
                                const int idHeSOTNTH = 35;
                                const int idID = 36;
                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    #region Khai báo
                                    psc_UIS_GiangVien nhanVien;
                                    ChiTietKhoiLuongGiangDay ct;
                                    KhoiLuongGiangDay qly = uow.GetObjectByKey<KhoiLuongGiangDay>(OidQuanLy.Oid);
                                    CriteriaOperator fCauHinh = CriteriaOperator.Parse("NamHoc =?", OidQuanLy.NamHoc.Oid);
                                    CauHinhQuyDoiPMS cauhinh = uow.FindObject<CauHinhQuyDoiPMS>(fCauHinh);
                                    int STT = 0;
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (qly != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            #region
                                            decimal txtSoTC = 0;
                                            decimal txtSoTietLyThuyet = 0;
                                            decimal txtSoTietThaoLuan = 0;
                                            decimal txtSoTietTNTH = 0;
                                            decimal txtSoBaiTNTH = 0;
                                            decimal txtSoGioTNTH = 0;
                                            int txtSoNhomTNTH = 0;
                                            decimal txtSoLuongDoAn = 0;
                                            decimal txtSoLuongBTL = 0;
                                            int txtSoLuongSV = 0;
                                            decimal txtHeSoLopDong = 0;
                                            decimal txtHeSoTinhChi = 0;
                                            decimal txtHeSoChucDanh = 0;
                                            decimal txtHeSoHocKy = 0;
                                            decimal txtTongHeSo = 0;
                                            decimal txtQuyDoiLyThuyet = 0;
                                            decimal txtQuyDoiThaoLuan = 0;
                                            decimal txtTongGioLT_TH = 0;
                                            decimal txtQuyDoiTNTH = 0;
                                            decimal txtQuyDoiChamBaiTNTH = 0;
                                            decimal txtQuyDoiDoAn = 0;
                                            decimal txtQuyDoiBTL = 0;
                                            decimal txtTongGioTNTH_DA_BTL = 0;
                                            decimal txtHeSoTNTH = 0;
                                            int txtTT = 0;
                                            #endregion
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            //

                                            #region Mã khoa
                                            string txtMaKhoa = dr[idMaKhoa].ToString();
                                            #endregion

                                            #region Họ tên
                                            string txtHoTen = dr[idHoTen].ToString();
                                            #endregion

                                            #region Mã nhân viên
                                            string txtMaNhanVien = dr[idMaNhanVien].ToString();
                                            #endregion

                                            #region Thứ tự
                                            if (!string.IsNullOrEmpty(dr[idTT].ToString()))
                                                txtTT = Convert.ToInt32(dr[idTT].ToString());
                                            #endregion

                                            #region Mã học phần
                                            string txtMaHP = dr[idMaHP].ToString();
                                            #endregion

                                            #region Tên học phần
                                            string txtTenHP = dr[idTenHP].ToString();
                                            #endregion

                                            #region Học kỳ
                                            string txtHocKy = dr[idHK].ToString();
                                            #endregion

                                            #region Số TC
                                            if (!string.IsNullOrEmpty(dr[idSoTC].ToString()))
                                                txtSoTC = Convert.ToDecimal(dr[idSoTC].ToString());
                                            #endregion

                                            #region Số tiết lý thuyết
                                            if (!string.IsNullOrEmpty(dr[idSoTietLyThuyet].ToString()))
                                                txtSoTietLyThuyet = Convert.ToDecimal(dr[idSoTietLyThuyet].ToString());
                                            #endregion

                                            #region Số tiết thảo luận
                                            if (!string.IsNullOrEmpty(dr[idSoTietThaoLuan].ToString()))
                                                txtSoTietThaoLuan = Convert.ToDecimal(dr[idSoTietThaoLuan].ToString());
                                            #endregion

                                            #region Số tiết TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoTietTNTH].ToString()))
                                                txtSoTietTNTH = Convert.ToDecimal(dr[idSoTietTNTH].ToString());
                                            #endregion

                                            #region Số bài TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoBaiTNTH].ToString()))
                                                txtSoBaiTNTH = Convert.ToDecimal(dr[idSoBaiTNTH].ToString());
                                            #endregion

                                            #region Số giờ TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoGioTNTH].ToString()))
                                                txtSoGioTNTH = Convert.ToDecimal(dr[idSoGioTNTH].ToString());
                                            #endregion

                                            #region Số nhóm TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoNhomTNTH].ToString()))
                                                txtSoNhomTNTH = Convert.ToInt32(dr[idSoNhomTNTH].ToString());
                                            #endregion

                                            #region Số lượng đồ án
                                            if (!string.IsNullOrEmpty(dr[idSoLuongDoAn].ToString()))
                                                txtSoLuongDoAn = Convert.ToDecimal(dr[idSoLuongDoAn].ToString());
                                            #endregion

                                            #region Số lượng BTL
                                            if (!string.IsNullOrEmpty(dr[idSoLuongBTL].ToString()))
                                                txtSoLuongBTL = Convert.ToDecimal(Convert.ToDecimal(dr[idSoLuongBTL].ToString()));
                                            #endregion

                                            #region Lớp học phần
                                            string txtLopHP = dr[idLopHP].ToString();
                                            #endregion

                                            #region Số lượng SV
                                            if (!string.IsNullOrEmpty(dr[idSoLuongSV].ToString()))
                                                txtSoLuongSV = Convert.ToInt32(dr[idSoLuongSV].ToString());
                                            #endregion

                                            #region Hệ số lớp đông
                                            if (!string.IsNullOrEmpty(dr[idHeSoLopDong].ToString()))
                                                txtHeSoLopDong = Convert.ToDecimal(dr[idHeSoLopDong].ToString());
                                            #endregion

                                            #region Hệ số tính chỉ
                                            if (!string.IsNullOrEmpty(dr[idHeSoTinhChi].ToString()))
                                                txtHeSoTinhChi = Convert.ToDecimal(dr[idHeSoTinhChi].ToString());
                                            #endregion

                                            #region Hệ số chức danh
                                            if (!string.IsNullOrEmpty(dr[idHeSoChucDanh].ToString()))
                                                txtHeSoChucDanh = Convert.ToDecimal(dr[idHeSoChucDanh].ToString());
                                            #endregion

                                            #region Hệ số học kỳ
                                            if (!string.IsNullOrEmpty(dr[idHeSoChucDanh].ToString()))
                                                txtHeSoHocKy = Convert.ToDecimal(dr[idHeSoHocKy].ToString());
                                            #endregion

                                            #region Tổng hệ số
                                            if (!string.IsNullOrEmpty(dr[idTongHeSo].ToString()))
                                                txtTongHeSo = Convert.ToDecimal(dr[idTongHeSo].ToString());
                                            #endregion

                                            #region Quy đổi lý thuyết
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiLyThuyet].ToString()))
                                                txtQuyDoiLyThuyet = Convert.ToDecimal(dr[idQuyDoiLyThuyet].ToString());
                                            #endregion

                                            #region Quy đổi thảo luận
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiThaoLuan].ToString()))
                                                txtQuyDoiThaoLuan = Convert.ToDecimal(dr[idQuyDoiThaoLuan].ToString());
                                            #endregion

                                            #region Tổng giờ LTTH
                                            if (!string.IsNullOrEmpty(dr[idTongGioLT_TH].ToString()))
                                                txtTongGioLT_TH = Convert.ToDecimal(dr[idTongGioLT_TH].ToString());
                                            #endregion

                                            #region Quy đổi TNTH
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiTNTH].ToString()))
                                                txtQuyDoiTNTH = Convert.ToDecimal(dr[idQuyDoiTNTH].ToString());
                                            #endregion

                                            #region Quy đổi chấm bài TNTH
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiChamBaiTNTH].ToString()))
                                                txtQuyDoiChamBaiTNTH = Convert.ToDecimal(dr[idQuyDoiChamBaiTNTH].ToString());
                                            #endregion

                                            #region Quy đổi đồ án
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiDoAn].ToString()))
                                                txtQuyDoiDoAn = Convert.ToDecimal(dr[idQuyDoiDoAn].ToString());
                                            #endregion

                                            #region Quy đổi BTL
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiBTL].ToString()))
                                                txtQuyDoiBTL = Convert.ToDecimal(dr[idQuyDoiBTL].ToString());
                                            #endregion

                                            #region Tổng giờ TNTH, DA, BTL
                                            if (!string.IsNullOrEmpty(dr[idTongGioTNTH_DA_BTL].ToString()))
                                                txtTongGioTNTH_DA_BTL = Convert.ToDecimal(dr[idTongGioTNTH_DA_BTL].ToString());
                                            #endregion

                                            #region Hệ số TNTH
                                            if (!string.IsNullOrEmpty(dr[idHeSOTNTH].ToString()))
                                                txtHeSoTNTH = Convert.ToDecimal(dr[idHeSOTNTH].ToString());
                                            #endregion

                                            //
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                            #region Kiểm tra dữ liệu
                                            if (!string.IsNullOrEmpty(txtHoTen) || !string.IsNullOrEmpty(txtMaNhanVien) || !string.IsNullOrEmpty(txtTenHP) || !string.IsNullOrEmpty(txtLopHP))
                                            {
                                                NhanVien NV = null;
                                                CriteriaOperator fNV;
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("ProfessorID =?", txtMaNhanVien);
                                                nhanVien = uow.FindObject<psc_UIS_GiangVien>(fNhanVien);
                                                if (nhanVien == null)
                                                {
                                                    fNV = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                                    NV = uow.FindObject<NhanVien>(fNV);
                                                    if (NV == null)
                                                    {
                                                        errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                                    }
                                                }
                                                else
                                                {
                                                    if (nhanVien.OidNhanVien != Guid.Empty)
                                                    {
                                                        fNV = CriteriaOperator.Parse("Oid =?", nhanVien.OidNhanVien);
                                                        NV = uow.FindObject<NhanVien>(fNV);
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine("- STT: " + STT + "- Nhân viên " + txtMaNhanVien + " - " + txtHoTen + " chưa kết nối HRM.");
                                                    }
                                                }
                                                if (NV != null)
                                                {
                                                    CriteriaOperator filter = CriteriaOperator.Parse("KhoiLuongGiangDay=? and NhanVien = ? and TenMonHoc=? and LopHocPhan = ? and STT = ?", qly.Oid, NV.Oid, txtTenHP, txtLopHP, txtTT);
                                                    XPCollection<ChiTietKhoiLuongGiangDay> dsChiTietKhoiLuongGiangDay = new XPCollection<ChiTietKhoiLuongGiangDay>(uow, filter);

                                                    if (dsChiTietKhoiLuongGiangDay.Count == 0)
                                                    {
                                                        ct = new ChiTietKhoiLuongGiangDay(uow);
                                                        ct.KhoiLuongGiangDay = qly;
                                                        ct.HocKy = txtHocKy;
                                                        ct.NhanVien = uow.GetObjectByKey<NhanVien>(NV.Oid);
                                                        ct.BoPhan = ct.NhanVien.BoPhan;
                                                        ct.HocHam = NV.NhanVienTrinhDo.HocHam;
                                                        ct.TrinhDoChuyenMon = NV.NhanVienTrinhDo.TrinhDoChuyenMon;
                                                        ct.MaGiangVien = uow.GetObjectByKey<NhanVien>(NV.Oid).MaQuanLy;
                                                        ct.TenMonHoc = txtTenHP;
                                                        ct.SoTinChi = txtSoTC;
                                                        ct.LopHocPhan = txtLopHP;
                                                        ct.SoLuongSV = txtSoLuongSV;
                                                        ct.LopHocPhan = txtLopHP;
                                                        ct.LoaiHocPhan = txtSoTietLyThuyet > 0 ? LoaiHocPhanEnum.LyThuyet : LoaiHocPhanEnum.ThucHanh;
                                                        ct.KhoaHoc = "";
                                                        ct.SoTietLyThuyet = txtSoTietLyThuyet;
                                                        ct.SoTietThucHanh = txtSoTietTNTH;
                                                        ct.SoTietThaoLuan = txtSoTietThaoLuan;
                                                        ct.SoNhomThucHanh = txtSoNhomTNTH;
                                                        ct.SoGioTNTH = txtSoGioTNTH;
                                                        ct.SoBaiTNTH = txtSoBaiTNTH;
                                                        ct.SoTiet_DoAn = txtSoLuongDoAn;
                                                        ct.SoTiet_BaiTapLon = txtSoLuongBTL;
                                                        ct.HeSo_ChucDanh = txtHeSoChucDanh;
                                                        ct.HeSo_LopDong = txtHeSoLopDong;
                                                        ct.HeSo_TinChi = txtHeSoTinhChi;
                                                        ct.HeSo_TNTH = txtHeSoTNTH;
                                                        ct.HeSo_HocKy = txtHeSoHocKy;
                                                        ct.TongHeSo = txtTongHeSo;
                                                        ct.GioQuyDoiLyThuyet = txtQuyDoiLyThuyet;
                                                        ct.GioQuyDoiThaoLuan = txtQuyDoiThaoLuan;
                                                        ct.TongGioLyThuyetThaoLuan = txtTongGioLT_TH;
                                                        ct.GioQuyDoiThucHanh = txtQuyDoiTNTH;
                                                        ct.GioQuyDoiChamBaiTNTH = txtQuyDoiChamBaiTNTH;
                                                        ct.GioQuyDoiDoAn = txtQuyDoiDoAn;
                                                        ct.GioQuyDoiBTL = txtQuyDoiBTL;
                                                        ct.TongGioTNTH_DA_BTL = txtTongGioTNTH_DA_BTL;
                                                        ct.HeSo_ThaoLuan = cauhinh.HeSo_ThaoLuan;
                                                        ct.HeSo_DoAn = cauhinh.HeSo_DoAn;
                                                        ct.HeSo_BTL = cauhinh.HeSo_BTL;
                                                        ct.SoBaiTNTH_GioChuan = cauhinh.SoBaiTNTH_GioChuan;
                                                        ct.STT = txtTT;
                                                        ct.TongGio = txtTongGioLT_TH + txtTongGioTNTH_DA_BTL;
                                                    }
                                                }
                                            }
                                            #endregion

                                            #region Ghi File log
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////     
                                            if (sucessImport)
                                            {
                                                uow.CommitChanges();////Lưu                                        
                                                sucessNumber++;
                                            }
                                            else
                                            {
                                                uow.RollbackTransaction(); ////trả lại dữ liệu ban đầu
                                                erorrNumber++;
                                                sucessImport = true;
                                            }
                                            #endregion
                                        }
                                    }

                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    }
                                    else
                                    {
                                        uow.CommitChanges();//Lưu
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Trường khác
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A8:AJ]"))
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các Idx

                                const int idMaKhoa = 0;
                                const int idHoTen = 1;
                                const int idMaNhanVien = 2;
                                const int idTT = 3;
                                const int idMaHP = 4;
                                const int idTenHP = 5;
                                const int idHK = 6;
                                const int idSoTC = 7;
                                const int idSoTietLyThuyet = 8;
                                const int idSoTietThaoLuan = 9;
                                const int idSoTietTNTH = 10;
                                const int idSoBaiTNTH = 11;
                                const int idSoGioTNTH = 12;
                                const int idSoNhomTNTH = 14;
                                const int idSoLuongDoAn = 15;
                                const int idSoLuongBTL = 16;
                                const int idLopHP = 17;
                                const int idSoLuongSV = 19;
                                const int idHeSoLopDong = 20;
                                const int idHeSoTinhChi = 21;
                                const int idHeSoChucDanh = 22;
                                const int idTongHeSo = 23;
                                const int idQuyDoiLyThuyet = 24;
                                const int idQuyDoiThaoLuan = 25;
                                const int idTongGioLT_TH = 26;
                                const int idQuyDoiTNTH = 27;
                                const int idQuyDoiChamBaiTNTH = 28;
                                const int idQuyDoiDoAn = 29;
                                const int idQuyDoiBTL = 30;
                                const int idTongGioTNTH_DA_BTL = 31;
                                const int idHeSOTNTH = 34;

                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    #region Khai báo
                                    psc_UIS_GiangVien nhanVien;
                                    ChiTietKhoiLuongGiangDay ct;
                                    KhoiLuongGiangDay qly = uow.GetObjectByKey<KhoiLuongGiangDay>(OidQuanLy.Oid);
                                    CriteriaOperator fCauHinh = CriteriaOperator.Parse("NamHoc =?", OidQuanLy.NamHoc.Oid);
                                    CauHinhQuyDoiPMS cauhinh = uow.FindObject<CauHinhQuyDoiPMS>(fCauHinh);
                                    int STT = 0;
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (qly != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            #region
                                            decimal txtSoTC = 0;
                                            decimal txtSoTietLyThuyet = 0;
                                            decimal txtSoTietThaoLuan = 0;
                                            decimal txtSoTietTNTH = 0;
                                            decimal txtSoBaiTNTH = 0;
                                            decimal txtSoGioTNTH = 0;
                                            int txtSoNhomTNTH = 0;
                                            decimal txtSoLuongDoAn = 0;
                                            decimal txtSoLuongBTL = 0;
                                            int txtSoLuongSV = 0;
                                            decimal txtHeSoLopDong = 0;
                                            decimal txtHeSoTinhChi = 0;
                                            decimal txtHeSoChucDanh = 0;
                                            decimal txtTongHeSo = 0;
                                            decimal txtQuyDoiLyThuyet = 0;
                                            decimal txtQuyDoiThaoLuan = 0;
                                            decimal txtTongGioLT_TH = 0;
                                            decimal txtQuyDoiTNTH = 0;
                                            decimal txtQuyDoiChamBaiTNTH = 0;
                                            decimal txtQuyDoiDoAn = 0;
                                            decimal txtQuyDoiBTL = 0;
                                            decimal txtTongGioTNTH_DA_BTL = 0;
                                            decimal txtHeSoTNTH = 0;
                                            int txtTT = 0;
                                            #endregion
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            //

                                            #region Mã khoa
                                            string txtMaKhoa = dr[idMaKhoa].ToString();
                                            #endregion

                                            #region Họ tên
                                            string txtHoTen = dr[idHoTen].ToString();
                                            #endregion

                                            #region Mã nhân viên
                                            string txtMaNhanVien = dr[idMaNhanVien].ToString();
                                            #endregion

                                            #region Thứ tự
                                            if (!string.IsNullOrEmpty(dr[idTT].ToString()))
                                                txtTT = Convert.ToInt32(dr[idTT].ToString());
                                            #endregion

                                            #region Mã học phần
                                            string txtMaHP = dr[idMaHP].ToString();
                                            #endregion

                                            #region Tên học phần
                                            string txtTenHP = dr[idTenHP].ToString();
                                            #endregion

                                            #region Học kỳ
                                            string txtHocKy = dr[idHK].ToString();
                                            #endregion

                                            #region Số TC
                                            if (!string.IsNullOrEmpty(dr[idSoTC].ToString()))
                                                txtSoTC = Convert.ToDecimal(dr[idSoTC].ToString());
                                            #endregion

                                            #region Số tiết lý thuyết
                                            if (!string.IsNullOrEmpty(dr[idSoTietLyThuyet].ToString()))
                                                txtSoTietLyThuyet = Convert.ToDecimal(dr[idSoTietLyThuyet].ToString());
                                            #endregion

                                            #region Số tiết thảo luận
                                            if (!string.IsNullOrEmpty(dr[idSoTietThaoLuan].ToString()))
                                                txtSoTietThaoLuan = Convert.ToDecimal(dr[idSoTietThaoLuan].ToString());
                                            #endregion

                                            #region Số tiết TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoTietTNTH].ToString()))
                                                txtSoTietTNTH = Convert.ToDecimal(dr[idSoTietTNTH].ToString());
                                            #endregion

                                            #region Số bài TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoBaiTNTH].ToString()))
                                                txtSoBaiTNTH = Convert.ToDecimal(dr[idSoBaiTNTH].ToString());
                                            #endregion

                                            #region Số giờ TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoGioTNTH].ToString()))
                                                txtSoGioTNTH = Convert.ToDecimal(dr[idSoGioTNTH].ToString());
                                            #endregion

                                            #region Số nhóm TNTH
                                            if (!string.IsNullOrEmpty(dr[idSoNhomTNTH].ToString()))
                                                txtSoNhomTNTH = Convert.ToInt32(dr[idSoNhomTNTH].ToString());
                                            #endregion

                                            #region Số lượng đồ án
                                            if (!string.IsNullOrEmpty(dr[idSoLuongDoAn].ToString()))
                                                txtSoLuongDoAn = Convert.ToDecimal(dr[idSoLuongDoAn].ToString());
                                            #endregion

                                            #region Số lượng BTL
                                            if (!string.IsNullOrEmpty(dr[idSoLuongBTL].ToString()))
                                                txtSoLuongBTL = Convert.ToDecimal(Convert.ToDecimal(dr[idSoLuongBTL].ToString()));
                                            #endregion

                                            #region Lớp học phần
                                            string txtLopHP = dr[idLopHP].ToString();
                                            #endregion

                                            #region Số lượng SV
                                            if (!string.IsNullOrEmpty(dr[idSoLuongSV].ToString()))
                                                txtSoLuongSV = Convert.ToInt32(dr[idSoLuongSV].ToString());
                                            #endregion

                                            #region Hệ số lớp đông
                                            if (!string.IsNullOrEmpty(dr[idHeSoLopDong].ToString()))
                                                txtHeSoLopDong = Convert.ToDecimal(dr[idHeSoLopDong].ToString());
                                            #endregion

                                            #region Hệ số tính chỉ
                                            if (!string.IsNullOrEmpty(dr[idHeSoTinhChi].ToString()))
                                                txtHeSoTinhChi = Convert.ToDecimal(dr[idHeSoTinhChi].ToString());
                                            #endregion

                                            #region Hệ số chức danh
                                            if (!string.IsNullOrEmpty(dr[idHeSoChucDanh].ToString()))
                                                txtHeSoChucDanh = Convert.ToDecimal(dr[idHeSoChucDanh].ToString());
                                            #endregion

                                            #region Tổng hệ số
                                            if (!string.IsNullOrEmpty(dr[idTongHeSo].ToString()))
                                                txtTongHeSo = Convert.ToDecimal(dr[idTongHeSo].ToString());
                                            #endregion

                                            #region Quy đổi lý thuyết
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiLyThuyet].ToString()))
                                                txtQuyDoiLyThuyet = Convert.ToDecimal(dr[idQuyDoiLyThuyet].ToString());
                                            #endregion

                                            #region Quy đổi thảo luận
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiThaoLuan].ToString()))
                                                txtQuyDoiThaoLuan = Convert.ToDecimal(dr[idQuyDoiThaoLuan].ToString());
                                            #endregion

                                            #region Tổng giờ LTTH
                                            if (!string.IsNullOrEmpty(dr[idTongGioLT_TH].ToString()))
                                                txtTongGioLT_TH = Convert.ToDecimal(dr[idTongGioLT_TH].ToString());
                                            #endregion

                                            #region Quy đổi TNTH
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiTNTH].ToString()))
                                                txtQuyDoiTNTH = Convert.ToDecimal(dr[idQuyDoiTNTH].ToString());
                                            #endregion

                                            #region Quy đổi chấm bài TNTH
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiChamBaiTNTH].ToString()))
                                                txtQuyDoiChamBaiTNTH = Convert.ToDecimal(dr[idQuyDoiChamBaiTNTH].ToString());
                                            #endregion

                                            #region Quy đổi đồ án
                                            if (!string.IsNullOrEmpty(dr[idQuyDoiDoAn].ToString()))
                                                txtQuyDoiDoAn = Convert.ToDecimal(dr[idQuyDoiDoAn].ToString());
                                            #endregion

                                            #region Quy đổi BTL
                                            if (!string.IsNullOrEmpty(idQuyDoiBTL.ToString()))
                                                txtQuyDoiBTL = Convert.ToDecimal(dr[idQuyDoiBTL].ToString());
                                            #endregion

                                            #region Tổng giờ TNTH, DA, BTL
                                            if (!string.IsNullOrEmpty(dr[idTongGioTNTH_DA_BTL].ToString()))
                                                txtTongGioTNTH_DA_BTL = Convert.ToDecimal(dr[idTongGioTNTH_DA_BTL].ToString());
                                            #endregion

                                            #region Hệ số TNTH
                                            if (!string.IsNullOrEmpty(dr[idHeSOTNTH].ToString()))
                                                txtHeSoTNTH = Convert.ToDecimal(dr[idHeSOTNTH].ToString());
                                            #endregion

                                            //
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                            #region Kiểm tra dữ liệu
                                            if (!string.IsNullOrEmpty(txtHoTen) || !string.IsNullOrEmpty(txtMaNhanVien) || !string.IsNullOrEmpty(txtTenHP) || !string.IsNullOrEmpty(txtLopHP))
                                            {
                                                NhanVien NV = null;
                                                CriteriaOperator fNV;
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("ProfessorID =?", txtMaNhanVien);
                                                nhanVien = uow.FindObject<psc_UIS_GiangVien>(fNhanVien);
                                                if (nhanVien == null)
                                                {
                                                    fNV = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                                    NV = uow.FindObject<NhanVien>(fNV);
                                                    if (NV == null)
                                                    {
                                                        errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                                    }
                                                }
                                                else
                                                {
                                                    if (nhanVien.OidNhanVien != Guid.Empty)
                                                    {
                                                        fNV = CriteriaOperator.Parse("Oid =?", nhanVien.OidNhanVien);
                                                        NV = uow.FindObject<NhanVien>(fNV);
                                                    }
                                                    else
                                                    {
                                                        errorLog.AppendLine("- STT: " + STT + "- Nhân viên " + txtMaNhanVien + " - " + txtHoTen + " chưa kết nối HRM.");
                                                    }
                                                }

                                                CriteriaOperator filter = CriteriaOperator.Parse("KhoiLuongGiangDay=? and NhanVien = ? and TenMonHoc=? and LopHocPhan = ? and STT = ?", qly.Oid, nhanVien.OidNhanVien, txtTenHP, txtLopHP, txtTT);
                                                XPCollection<ChiTietKhoiLuongGiangDay> dsChiTietKhoiLuongGiangDay = new XPCollection<ChiTietKhoiLuongGiangDay>(uow, filter);

                                                if (dsChiTietKhoiLuongGiangDay.Count == 0)
                                                {
                                                    ct = new ChiTietKhoiLuongGiangDay(uow);
                                                    ct.KhoiLuongGiangDay = qly;
                                                    ct.HocKy = txtHocKy;
                                                    ct.NhanVien = uow.GetObjectByKey<NhanVien>(NV.Oid);
                                                    ct.BoPhan = ct.NhanVien.BoPhan;
                                                    ct.HocHam = NV.NhanVienTrinhDo.HocHam;
                                                    ct.TrinhDoChuyenMon = NV.NhanVienTrinhDo.TrinhDoChuyenMon;
                                                    ct.MaGiangVien = uow.GetObjectByKey<NhanVien>(NV.Oid).MaQuanLy;
                                                    ct.TenMonHoc = txtTenHP;
                                                    ct.SoTinChi = txtSoTC;
                                                    ct.LopHocPhan = txtLopHP;
                                                    ct.SoLuongSV = txtSoLuongSV;
                                                    ct.LopHocPhan = txtLopHP;
                                                    ct.LoaiHocPhan = txtSoTietLyThuyet > 0 ? LoaiHocPhanEnum.LyThuyet : LoaiHocPhanEnum.ThucHanh;
                                                    ct.KhoaHoc = "";
                                                    ct.SoTietLyThuyet = txtSoTietLyThuyet;
                                                    ct.SoTietThucHanh = txtSoTietTNTH;
                                                    ct.SoTietThaoLuan = txtSoTietThaoLuan;
                                                    ct.SoNhomThucHanh = txtSoNhomTNTH;
                                                    ct.SoGioTNTH = txtSoGioTNTH;
                                                    ct.SoBaiTNTH = txtSoBaiTNTH;
                                                    ct.SoTiet_DoAn = txtSoLuongDoAn;
                                                    ct.SoTiet_BaiTapLon = txtSoLuongBTL;
                                                    ct.HeSo_ChucDanh = txtHeSoChucDanh;
                                                    ct.HeSo_LopDong = txtHeSoLopDong;
                                                    ct.HeSo_TinChi = txtHeSoTinhChi;
                                                    ct.HeSo_TNTH = txtHeSoTNTH;
                                                    ct.TongHeSo = txtTongHeSo;
                                                    ct.GioQuyDoiLyThuyet = txtQuyDoiLyThuyet;
                                                    ct.GioQuyDoiThaoLuan = txtQuyDoiThaoLuan;
                                                    ct.TongGioLyThuyetThaoLuan = txtTongGioLT_TH;
                                                    ct.GioQuyDoiThucHanh = txtQuyDoiTNTH;
                                                    ct.GioQuyDoiChamBaiTNTH = txtQuyDoiChamBaiTNTH;
                                                    ct.GioQuyDoiDoAn = txtQuyDoiDoAn;
                                                    ct.GioQuyDoiBTL = txtQuyDoiBTL;
                                                    ct.TongGioTNTH_DA_BTL = txtTongGioTNTH_DA_BTL;
                                                    ct.HeSo_ThaoLuan = cauhinh.HeSo_ThaoLuan;
                                                    ct.HeSo_DoAn = cauhinh.HeSo_DoAn;
                                                    ct.HeSo_BTL = cauhinh.HeSo_BTL;
                                                    ct.SoBaiTNTH_GioChuan = cauhinh.SoBaiTNTH_GioChuan;
                                                    ct.STT = txtTT;
                                                    ct.TongGio = txtTongGioLT_TH + txtTongGioTNTH_DA_BTL;
                                                }
                                            }
                                            #endregion

                                            #region Ghi File log
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////     
                                            if (sucessImport)
                                            {
                                                uow.CommitChanges();////Lưu                                        
                                                sucessNumber++;
                                            }
                                            else
                                            {
                                                uow.RollbackTransaction(); ////trả lại dữ liệu ban đầu
                                                erorrNumber++;
                                                sucessImport = true;
                                            }
                                            #endregion
                                        }
                                    }

                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    }
                                    else
                                    {
                                        uow.CommitChanges();//Lưu
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số dòng không thành công " + erorrNumber + " " + s + "!");

                    #region Mở file log lỗi lên
                    if (erorrNumber > 0)
                    {
                        string tenFile = "Import_Log.txt";
                        StreamWriter writer = new StreamWriter(tenFile);
                        writer.WriteLine(mainLog.ToString());
                        writer.Flush();
                        writer.Close();
                        writer.Dispose();
                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                        Process.Start(tenFile);
                    }
                    #endregion
                }
            }
        }
    }
    #endregion
}