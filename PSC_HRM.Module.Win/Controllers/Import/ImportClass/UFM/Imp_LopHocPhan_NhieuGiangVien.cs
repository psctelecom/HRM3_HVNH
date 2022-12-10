using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen;
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
    public class Imp_LopHocPhan_NhieuGiangVien
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, Guid OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A6:U]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaLopHocPhan = 0;
                            const int idTenHocPhan = 1;
                            const int idLop = 2;
                            const int idMaGiangVien = 3;
                            const int idTenGiangVien = 4;
                            const int idSoTinChi = 5;
                            const int idSoTietLT = 6;
                            const int idSoTietTH = 7;
                            const int idThu = 8;
                            const int idBuoi = 9;//Tiết
                            const int idPhongHoc = 10;
                            const int idTuan = 11;
                            const int idNgayBD = 12;
                            const int idNgayKT = 13;
                            const int idSoTietGiang = 14;
                            const int idDonViQuanLy = 15;
                            const int idBacDaoTao = 16;
                            const int idHeDaoTao = 17;
                            const int idGhiChu = 18;
                            const int idNgonNgu = 19;
                            const int idHocky = 20;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                int STT = 0;
                                string sql = "";
                                string sql_TimGV = "";
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (OidQuanLy != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        #region Khởi tạo
                                        STT++;
                                        #region Khai Báo
                                        string MaLopHocPhan = "";
                                        string TenHocPhan = "";
                                        string Lop = "";

                                        string MaGiangVien = "";
                                        string TenGiangVien = "";
                                        string SoTinChi = "";

                                        string SoTietLT = "";
                                        string SoTietTH = "";
                                        string Thu = "";
                                        string Buoi = "";

                                        string PhongHoc = "";
                                        string Tuan = "";
                                        string NgayBD = "";
                                        string NgayKT = "";
                                        string SoTietGiang = "";
                                        string DonViQuanLy = "";
                                        string TenBacDaoTao = "";
                                        string TenHeDaoTao = "";
                                        string GhiChu = "";

                                        string NgonNgu = "";
                                        string HocKy = "";

                                        BoPhan donViQuanLy = null;
                                        BacDaoTao bacDaoTao=null;
                                        HeDaoTao heDaoTao =null;
                                        #endregion
                                        #endregion
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //
                                        if (dr[idMaLopHocPhan] != string.Empty)
                                            MaLopHocPhan = dr[idMaLopHocPhan].ToString().Replace(",", ".");
                                        if (dr[idTenHocPhan] != string.Empty)
                                            TenHocPhan = dr[idTenHocPhan].ToString().Replace(",", ".");
                                        if (dr[idLop] != string.Empty)
                                            Lop = dr[idLop].ToString().Replace(",", ".");
                                        if (dr[idMaGiangVien] != string.Empty)
                                            MaGiangVien = dr[idMaGiangVien].ToString().Replace(",", ".");
                                        if (dr[idTenGiangVien] != string.Empty)
                                            TenGiangVien = dr[idTenGiangVien].ToString().Replace(",", ".");
                                        if (dr[idSoTinChi] != string.Empty)
                                            SoTinChi = dr[idSoTinChi].ToString().Replace(",", ".");
                                        if (dr[idSoTietLT] != string.Empty)
                                            SoTietLT = dr[idSoTietLT].ToString().Replace(",", ".");
                                        if (dr[idSoTietTH] != string.Empty)
                                            SoTietTH = dr[idSoTietTH].ToString().Replace(",", ".");
                                        if (dr[idThu] != string.Empty)
                                            Thu = dr[idThu].ToString().Replace(",", ".");
                                        if (dr[idBuoi] != string.Empty)
                                            Buoi = dr[idBuoi].ToString().Replace(",", ".");
                                        if (dr[idPhongHoc] != string.Empty)
                                            PhongHoc = dr[idPhongHoc].ToString().Replace(",", ".");
                                        if (dr[idTuan] != string.Empty)
                                            Tuan = dr[idTuan].ToString().Replace(",", ".");
                                        if (dr[idNgayBD] != string.Empty)
                                            NgayBD = dr[idNgayBD].ToString().Replace(",", ".");
                                        if (dr[idNgayKT] != string.Empty)
                                            NgayKT = dr[idNgayKT].ToString().Replace(",", ".");
                                        if (dr[idSoTietGiang] != string.Empty)
                                            SoTietGiang = dr[idSoTietGiang].ToString().Replace(",", ".");
                                        if (dr[idDonViQuanLy] != string.Empty)
                                            DonViQuanLy = dr[idDonViQuanLy].ToString().Replace(",", ".");
                                        if (dr[idBacDaoTao] != string.Empty)
                                            TenBacDaoTao = dr[idBacDaoTao].ToString().Replace(",", ".");
                                        if (dr[idHeDaoTao] != string.Empty)
                                            TenHeDaoTao = dr[idHeDaoTao].ToString().Replace(",", ".");
                                        if (dr[idGhiChu] != string.Empty)
                                            GhiChu = dr[idGhiChu].ToString().Replace(",", ".");
                                        if (dr[idNgonNgu] != string.Empty)
                                            NgonNgu = dr[idNgonNgu].ToString().Replace(",", ".");
                                        if (dr[idHocky] != string.Empty)
                                            HocKy = dr[idHocky].ToString().Replace(",", ".");
                                        //
                                        #endregion
                                        if (TenGiangVien != "" && TenHocPhan != "")
                                        {
                                            #region KIỂM TRA VÀ LẤY DỮ LIỆU
                                            sql_TimGV = "SELECT Oid "
                                                    + " FROM dbo.HoSo"
                                                    + " WHERE N'" + MaGiangVien + "' LIKE '%'+MaQuanLy+'%'"
                                                    + " AND  N'" + TenGiangVien + "' LIKE '%'+HoTen+'%'"
                                                    + " AND MaQuanLy<>''"
                                                    + " AND GCRecord IS NULL";
                                            object oidNhanVien = DataProvider.GetValueFromDatabase(sql_TimGV, CommandType.Text);
                                            if (oidNhanVien != null)
                                            {
                                                donViQuanLy = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan =?", DonViQuanLy));
                                                if (donViQuanLy != null)
                                                {
                                                    bacDaoTao = uow.FindObject<BacDaoTao>(CriteriaOperator.Parse("MaQuanLy =? or TenBacDaoTao =?", TenBacDaoTao, TenBacDaoTao));
                                                    if (bacDaoTao != null)
                                                    {
                                                        heDaoTao = uow.FindObject<HeDaoTao>(CriteriaOperator.Parse("MaQuanLy =? or TenHeDaoTao =?", TenHeDaoTao, TenHeDaoTao));
                                                        if (bacDaoTao != null)
                                                        {
                                                            sql += " Union All select N'" + oidNhanVien + "' as oidNhanVien"
                                                                  + ", N'" + MaLopHocPhan + "' as MaLopHocPhan"
                                                                  + ", N'" + TenHocPhan + "' as TenHocPhan"
                                                                  + ", N'" + Lop + "' as Lop"
                                                                  + ", N'" + SoTinChi + "' as SoTinChi"
                                                                  + ", N'" + SoTietLT + "' as SoTietLT"
                                                                  + ", N'" + SoTietTH + "' as SoTietTH"
                                                                  + ", N'" + Thu + "' as Thu"
                                                                  + ", N'" + Buoi + "' as Buoi"
                                                                  + ", N'" + PhongHoc + "' as PhongHoc"
                                                                  + ", N'" + Tuan + "' as Tuan"
                                                                  + ", N'" + NgayBD + "' as NgayBD"
                                                                  + ", N'" + NgayKT + "' as NgayKT"
                                                                  + ", N'" + SoTietGiang + "' as SoTietGiang"
                                                                  + ", N'" + donViQuanLy.Oid + "' as donViQuanLy"
                                                                  + ", N'" + bacDaoTao.Oid + "' as bacDaoTao"
                                                                  + ", N'" + heDaoTao.Oid + "' as heDaoTao"
                                                                  + ", N'" + GhiChu + "' as GhiChu" 
                                                                  + ", N'" + NgonNgu + "' as NgonNgu" 
                                                                  + ", N'" + HocKy + "' as HocKy";
                                                        }
                                                        else
                                                        {
                                                            erorrNumber++;
                                                            errorLog.AppendLine("- STT: " + " Hệ đào tạo : " + TenHeDaoTao + " không tồn tại.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        erorrNumber++;
                                                        errorLog.AppendLine("- STT: " + " Bậc đào tạo : " + TenBacDaoTao + " không tồn tại.");
                                                    }
                                                }
                                                else
                                                {
                                                    erorrNumber++;
                                                    errorLog.AppendLine("- STT: " + " Đơn vị : " + DonViQuanLy + " không tồn tại.");
                                                }
                                            }
                                            else
                                            {
                                                erorrNumber++;
                                                errorLog.AppendLine("- STT: " + " Mã: " + MaGiangVien + ", tên: " + TenGiangVien + " không tồn tại.");
                                            }
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        }
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


                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        //uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                        DialogUtil.ShowInfo("Số dòng không thành công " + erorrNumber + " !");
                                    }
                                    else
                                    {
                                        SqlParameter[] pImport = new SqlParameter[2];
                                        pImport[0] = new SqlParameter("@KhoiLuongGiangDay", OidQuanLy);
                                        pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                        DataProvider.GetValueFromDatabase("spd_PMS_Import_LopHocPhan_NhieuGiangVien", CommandType.StoredProcedure, pImport); 
                                        //uow.CommitChanges();//Lưu
                                    }
                                }
                            }
                        }
                        //
                        string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                        if (erorrNumber == 0)
                            DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + "\n Số dòng không thành công " + erorrNumber + " " + s + "!");

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
}
