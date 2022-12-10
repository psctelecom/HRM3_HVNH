using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.GioChuan;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu;
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
    public class Imp_DinhMucGioChuan_NhanVien
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyGioChuan OidQuanLy)
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
                        if (TruongConfig.MaTruong == "QNU")
                        {
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[DinhMucGioChuan$A8:M]"))
                            {
                                const int idHo = 1;
                                const int idTen = 2;
                                const int idHoTen = 3;
                                const int idMaQuanLy = 4;
                                const int idDonVi = 5;
                                const int idChucVuCongViec = 6;
                                const int idHeSoChucDanh = 8;
                                const int idGioDinhMuc = 9;
                                const int idGioChuanSauGiamTru = 11;
                                const int idGhiChu = 12;
                                int Stt = 0;
                                string sql = "";
                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    if (OidQuanLy != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            Stt++;
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            string Ho = "";
                                            if (dr[idHo].ToString() != string.Empty)
                                                Ho = dr[idHo].ToString().Replace(",", ".");
                                            string Ten = "";
                                            if (dr[idTen].ToString() != string.Empty)
                                                Ten = dr[idTen].ToString().Replace(",", ".");
                                            string HoTen = "";
                                            if (dr[idHoTen].ToString() != string.Empty)
                                                HoTen = dr[idHoTen].ToString().Replace(",", ".");
                                            string MaQuanLy = "";
                                            if (dr[idMaQuanLy].ToString() != string.Empty)
                                                MaQuanLy = dr[idMaQuanLy].ToString().Replace(",", ".");
                                            string DonVi = "";
                                            if (dr[idDonVi].ToString() != string.Empty)
                                                DonVi = dr[idDonVi].ToString().Replace(",", ".");
                                            string ChucVuCongViec = "";
                                            if (dr[idChucVuCongViec].ToString() != string.Empty)
                                                ChucVuCongViec = dr[idChucVuCongViec].ToString().Replace(",", ".");
                                            string HeSoChucDanh = "";
                                            if (dr[idHeSoChucDanh].ToString() != string.Empty)
                                                HeSoChucDanh = dr[idHeSoChucDanh].ToString().Replace(",", ".");
                                            string GioDinhMuc = "";
                                            if (dr[idGioDinhMuc].ToString() != string.Empty)
                                                GioDinhMuc = dr[idGioDinhMuc].ToString().Replace(",", ".");
                                            string GioChuanSauGiamTru = "";
                                            if (dr[idGioChuanSauGiamTru].ToString() != string.Empty)
                                                GioChuanSauGiamTru = dr[idGioChuanSauGiamTru].ToString().Replace(",", ".");
                                            string GhiChu = "";
                                            if (dr[idGhiChu].ToString() != string.Empty)
                                                GhiChu = dr[idGhiChu].ToString().Replace(",", ".");
                                            #endregion
        #endregion
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Xử lý sql
                                            sql += " Union All select N'" + Ho + "' as Ho"
                                                           + ", N'" + Ten + "' as Ten"
                                                           + ", N'" + HoTen + "' as HoTen"
                                                           + ", N'" + MaQuanLy + "' as MaQuanLy"
                                                           + ", N'" + DonVi + "' as DonVi"
                                                           + ", N'" + ChucVuCongViec + "' as ChucVuCongViec"
                                                           + ", N'" + HeSoChucDanh + "' as HeSoChucDanh"
                                                           + ", N'" + GioDinhMuc + "' as GioDinhMuc"
                                                           + ", N'" + GioChuanSauGiamTru + "' as GioChuanSauGiamTru"
                                                           + ", N'" + GhiChu + "' as GhiChu";
                                            sucessNumber++;
                                        }
                                        #region Insert SQL
                                        object kq = null;
                                        SqlParameter[] pImport = new SqlParameter[3];
                                        pImport[0] = new SqlParameter("@OidQuanLy", OidQuanLy.Oid);
                                        pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                        pImport[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                                        kq = DataProvider.GetValueFromDatabase("spd_PMS_Import_DinhMucGioChuan", CommandType.StoredProcedure, pImport);
                                        if (kq != null)
                                            sucessNumber = Convert.ToInt32(kq.ToString());
                                        uow.CommitChanges();//Lưu
                                        #endregion
                                    }
                                }
                            }
                        }
                        #region Trường khác
                        else
                        {
                            DataTable dt = null;
                           if (TruongConfig.MaTruong == "UFM")
                           {
                              dt = DataProvider.GetDataTable(open.FileName, "[DinhMucGioChuan$A11:R]");
                           }
                           else{
                               dt = DataProvider.GetDataTable(open.FileName, "[DinhMucGioChuan$A11:Q]");
                           }
                            using (dt)
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các Idx
                                const int idMaGV = 1;
                                const int idHo = 2;
                                const int idTen = 3;
                                const int idChucDanh = 6;
                                const int idChucVu = 7;
                                const int idTrinhDo = 8;
                                const int idDinhMuc = 9;
                                const int idChucVuChinhQuyen = 10;
                                const int idKiemNhiem = 11;
                                const int idNuoiConNho = 12;
                                const int idCoVanHocTap = 13;
                                const int idGiangVienMoi = 14;
                                const int idHocNghienCuuSinh = 15;
                                const int idDinhMucThucHien = 16;
                                const int idGhiChu = 17;
                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    #region Khai báo
                                    int STT = 0;
                                    NhanVien nhanVien = null;
                                    string sql = "";
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (OidQuanLy != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            //

                                            #region Mã giảng viên
                                            string txtMaGiangVien = dr[idMaGV].ToString();
                                            #endregion

                                            #region Họ Tên
                                            string txtHoTen = dr[idHo].ToString() + " " + dr[idTen].ToString();
                                            #endregion

                                            #region Chức danh
                                            string txtChucDanh = "";
                                            if (dr[idChucDanh].ToString() != string.Empty)
                                                txtChucDanh = dr[idChucDanh].ToString();
                                            #endregion

                                            #region Chức vụ
                                            string txtChucVu = "";
                                            if (dr[idChucVu].ToString() != string.Empty)
                                                txtChucVu = dr[idChucVu].ToString();
                                            #endregion

                                            #region Trình độ
                                            string txtTrinhDo = "";
                                            if (dr[idTrinhDo].ToString() != string.Empty)
                                                txtTrinhDo = dr[idTrinhDo].ToString();
                                            #endregion

                                            #region Định mức
                                            string txtDinhMuc = "";
                                            if (dr[idDinhMuc] != string.Empty)
                                                txtDinhMuc = dr[idDinhMuc].ToString().Replace(",", ".");
                                            #endregion

                                            #region Giảm trừ chức vụ chinh quyền
                                            string txtChucVuChinhQuyen = "";
                                            if (dr[idChucVuChinhQuyen] != string.Empty)
                                                txtChucVuChinhQuyen = dr[idChucVuChinhQuyen].ToString().Replace(",", ".");
                                            #endregion

                                            #region Giảm trừ kiêm nhiệm
                                            string txtKiemNhiem = "";
                                            if (dr[idKiemNhiem] != string.Empty)
                                                txtKiemNhiem = dr[idKiemNhiem].ToString().Replace(",", ".");
                                            #endregion

                                            #region Giảm trừ nuôi con nhỏ
                                            string txtNuoiConNho = "";
                                            if (dr[idNuoiConNho] != string.Empty)
                                                txtNuoiConNho = dr[idNuoiConNho].ToString().Replace(",", ".");
                                            #endregion

                                            #region Giảm trừ Cố vấn học tập
                                            string txtCoVanHocTap = "";
                                            if (dr[idCoVanHocTap] != string.Empty)
                                                txtCoVanHocTap = dr[idCoVanHocTap].ToString().Replace(",", ".");
                                            #endregion

                                            #region Giảm trừ Giảng viên mới
                                            string txtGiangVienMoi = "";
                                            if (dr[idGiangVienMoi] != string.Empty)
                                                txtGiangVienMoi = dr[idGiangVienMoi].ToString().Replace(",", ".");
                                            #endregion

                                            #region Giảm trừ NGhiên cứu sinh
                                            string txtNghienCuuSinh = "";
                                            if (dr[idHocNghienCuuSinh] != string.Empty)
                                                txtNghienCuuSinh = dr[idHocNghienCuuSinh].ToString().Replace(",", ".");
                                            #endregion

                                            #region Định mức thực hiện
                                            string txtDinhMucThucHien = "";
                                            if (dr[idDinhMucThucHien] != string.Empty)
                                                txtDinhMucThucHien = dr[idDinhMucThucHien].ToString().Replace(",", ".");
                                            #endregion

                                          
                                            //
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////                                     

                                            if (txtMaGiangVien != string.Empty)
                                            {
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaGiangVien);
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                                if (nhanVien != null)
                                                {
                                                    #region UFM
                                                    if (TruongConfig.MaTruong == "UFM")
                                                    {
                                                        #region Ghi Chú
                                                        string txtGhiChu = "";
                                                        if (dr[idGhiChu] != string.Empty)
                                                            txtGhiChu = dr[idGhiChu].ToString().Replace("'", "");
                                                        #endregion                                                      

                                                        sql += " Union All select N'" + txtMaGiangVien + "' as MaQuanLy"
                                                                + ", N'" + txtHoTen + "' as HoTen"
                                                                + ", N'" + txtChucDanh + "' as ChucDanh"
                                                                + ", N'" + txtChucVu + "' as ChucVu"
                                                                + ", N'" + txtTrinhDo + "' as TrinhDo"
                                                                + ", N'" + txtDinhMuc + "' as DinhMuc"
                                                                + ", N'" + txtChucVuChinhQuyen + "' as ChucVuChinhQuyen"
                                                                + ", N'" + txtKiemNhiem + "' as KiemNhiem"
                                                                + ", N'" + txtNuoiConNho + "' as NuoiConNho"
                                                                + ", N'" + txtCoVanHocTap + "' as CoVanHocTap"
                                                                + ", N'" + txtGiangVienMoi + "' as GiangVienMoi"
                                                                + ", N'" + txtNghienCuuSinh + "' as NghienCuuSinh"
                                                                + ", N'" + txtDinhMucThucHien + "' as DinhMucThucHien"
                                                                + ", N'" + txtGhiChu + "' as GhiChu";

                                                        sucessNumber++;
                                                    }
                                                    #endregion
                                                    #region Trường khác
                                                    else
                                                    {
                                                        sql += " Union All select N'" + txtMaGiangVien + "' as MaQuanLy"
                                                               + ", N'" + txtHoTen + "' as HoTen"
                                                               + ", N'" + txtChucDanh + "' as ChucDanh"
                                                               + ", N'" + txtChucVu + "' as ChucVu"
                                                               + ", N'" + txtTrinhDo + "' as TrinhDo"
                                                               + ", N'" + txtDinhMuc + "' as DinhMuc"
                                                               + ", N'" + txtChucVuChinhQuyen + "' as ChucVuChinhQuyen"
                                                               + ", N'" + txtKiemNhiem + "' as KiemNhiem"
                                                               + ", N'" + txtNuoiConNho + "' as NuoiConNho"
                                                               + ", N'" + txtCoVanHocTap + "' as CoVanHocTap"
                                                               + ", N'" + txtGiangVienMoi + "' as GiangVienMoi"
                                                               + ", N'" + txtNghienCuuSinh + "' as NghienCuuSinh"
                                                               + ", N'" + txtDinhMucThucHien + "' as DinhMucThucHien";
                                                        sucessNumber++;
                                                    }
                                                    #endregion                                                  
                                                }
                                                else
                                                {
                                                    erorrNumber++;
                                                    errorLog.AppendLine("- Mã giảng viên: " + txtMaGiangVien + " - " + txtHoTen + " Không tồn tại");
                                                }
                                            }

                                            #region Ghi File log
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {                                               
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////     
                                            if (sucessImport)
                                            {
                                                //uow.CommitChanges();////Lưu                                        
                                                //sucessNumber++;
                                            }
                                            else
                                            {
                                                //uow.RollbackTransaction(); ////trả lại dữ liệu ban đầu                                             
                                                sucessImport = true;
                                            }
                                            #endregion
                                        }
                                    }

                                    //hợp lệ cả file mới lưu

                                    if (erorrNumber == 0)
                                    {
                                        SqlParameter[] pImport = new SqlParameter[2];
                                        pImport[0] = new SqlParameter("@QuanLyGioChuan", OidQuanLy.Oid);
                                        pImport[1] = new SqlParameter("@string", sql.Substring(11));
                                        DataProvider.GetValueFromDatabase("spd_PMS_Import_DinhMucGioChuan_NhaNVien", CommandType.StoredProcedure, pImport);
                                    }                                                                 
                                }
                          }                          

                        }
                        #endregion
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


        public static void XuLyUEL(IObjectSpace obs, QuanLyGioChuan OidQuanLy)
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
                        if (TruongConfig.MaTruong == "UEL")
                        {
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:G]"))
                            {
                                const int idSTT = 0;
                                const int idMAGV = 1;
                                const int idHoTen = 2;
                                const int idDONVI = 3;
                                const int idDINHMUC = 4;
                                const int idDINHMUCSAUTRU = 5;
                                const int idGHICHU = 6;
                                int Stt = 0;
                                string sql = "";
                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    if (OidQuanLy != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            Stt++;
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            string STT = "";
                                            if (dr[idSTT].ToString() != string.Empty)
                                                STT = dr[idSTT].ToString().Replace(",", ".");
                                            string MAGV = "";
                                            if (dr[idMAGV].ToString() != string.Empty)
                                                MAGV = dr[idMAGV].ToString().Replace(",", ".");
                                            string HoTen = "";
                                            if (dr[idHoTen].ToString() != string.Empty)
                                                HoTen = dr[idHoTen].ToString().Replace(",", ".");
                                            string DONVI = "";
                                            if (dr[idDONVI].ToString() != string.Empty)
                                                DONVI = dr[idDONVI].ToString().Replace(",", ".");
                                            string DINHMUC = "";
                                            if (dr[idDINHMUC].ToString() != string.Empty)
                                                DINHMUC = dr[idDINHMUC].ToString().Replace(",", ".");
                                            string DINHMUCSAUTRU = "";
                                            if (dr[idDINHMUCSAUTRU].ToString() != string.Empty)
                                                DINHMUCSAUTRU = dr[idDINHMUCSAUTRU].ToString().Replace(",", ".");
                                            string GHICHU = "";
                                            if (dr[idGHICHU].ToString() != string.Empty)
                                                GHICHU = dr[idGHICHU].ToString().Replace(",", ".");
                                            #endregion
                                            #endregion
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Xử lý sql
                                            sql += " Union All select N'" + STT + "' as STT"
                                                           + ", N'" + MAGV + "' as MaGV"
                                                           + ", N'" + HoTen + "' as HoTen"
                                                           + ", N'" + DONVI + "' as DonVi"
                                                           + ", N'" + DINHMUC + "' as DinhMuc"
                                                           + ", N'" + DINHMUCSAUTRU + "' as DinhMucSauTru"
                                                           + ", N'" + GHICHU + "' as GhiChu";
                                            sucessNumber++;
                                        }
                                        #region Insert SQL
                                        object kq = null;
                                        SqlParameter[] pImport = new SqlParameter[3];
                                        pImport[0] = new SqlParameter("@OidQuanLy", OidQuanLy.Oid);
                                        pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                        pImport[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                                        kq = DataProvider.GetValueFromDatabase("spd_PMS_Import_DinhMucGioChuan", CommandType.StoredProcedure, pImport);
                                        if (kq != null)
                                            sucessNumber = Convert.ToInt32(kq.ToString());
                                        uow.CommitChanges();//Lưu
                                        #endregion
                                    }
                                }
                            }
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
        #endregion
    }
}