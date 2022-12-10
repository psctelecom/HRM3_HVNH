using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.Import.ImportClass
{
    public class Imp_NghienCuuKhoaHoc
    {
        public static void XuLy(IObjectSpace obs, QuanLyNCKH OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[NCKH$A1:J]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////


                            const int idMaGV = 0;
                            const int idTenGV = 1;
                            const int idNgaySinh = 2;
                            const int idGioiTinh = 3;
                            const int idTenNCKH = 4;
                            const int idVaiTro = 5;
                            const int idSoLuongTV = 6;
                            const int idKhoiLuongThucHien = 7;
                            const int idKhoiLuongSauTruDM = 8;
                            const int idDinhMuc = 9;//Tiết

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                int STT = 0;
                                string sql = "";
                                string sql_TimGV = "";
                                //Duyệt qua tất cả các dòng trong file excel
                                if (OidQuanLy != null)
                                {
                                    var errorLog = new StringBuilder(); ;
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        string MaGV = "";
                                        string TenGV = "";
                                        string NgaySinh = "";

                                        string GioiTinh = "";
                                        string TenNCKH = "";
                                        string VaiTro = "";

                                        string SoLuongTV = "";
                                        string KhoiLuongThucHien = "";
                                        string KhoiLuongSauTruDM = "";
                                        string DinhMuc = "";

                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        //
                                        if (dr[idMaGV] != string.Empty)
                                            MaGV = dr[idMaGV].ToString().Replace(",", ".");
                                        if (dr[idTenGV] != string.Empty)
                                            TenGV = dr[idTenGV].ToString().Replace(",", ".");
                                        if (dr[idNgaySinh] != string.Empty)
                                            NgaySinh = dr[idNgaySinh].ToString().Replace(",", ".");
                                        if (dr[idGioiTinh] != string.Empty)
                                            GioiTinh = dr[idGioiTinh].ToString().Replace(",", ".");
                                        if (dr[idTenNCKH] != string.Empty)
                                            TenNCKH = dr[idTenNCKH].ToString().Replace(",", ".");
                                        if (dr[idVaiTro] != string.Empty)
                                            VaiTro = dr[idVaiTro].ToString().Replace(",", ".");
                                        if (dr[idSoLuongTV] != string.Empty)
                                            SoLuongTV = dr[idSoLuongTV].ToString().Replace(",", ".");
                                        if (dr[idKhoiLuongThucHien] != string.Empty)
                                            KhoiLuongThucHien = dr[idKhoiLuongThucHien].ToString().Replace(",", ".");
                                        if (dr[idKhoiLuongSauTruDM] != string.Empty)
                                            KhoiLuongSauTruDM = dr[idKhoiLuongSauTruDM].ToString().Replace(",", ".");
                                        if (dr[idDinhMuc] != string.Empty)
                                            DinhMuc = dr[idDinhMuc].ToString().Replace(",", ".");
                                        //
                                        if (MaGV != "" && (KhoiLuongThucHien != "" || KhoiLuongSauTruDM != ""))
                                        {
                                            sql_TimGV = "SELECT Oid "
                                                    + " FROM dbo.HoSo"
                                                    + " WHERE MaQuanLy = N'" + MaGV + "'"
                                                    + " AND MaQuanLy<>''"
                                                    + " AND GCRecord IS NULL";
                                            object oidNhanVien = DataProvider.GetValueFromDatabase(sql_TimGV, CommandType.Text);

                                            if (oidNhanVien != null)
                                            {

                                                sql += " Union All select N'" + oidNhanVien + "' as NhanVien"
                                                      + ", N'" + OidQuanLy.Oid + "' as QuanLyNCKH"
                                                      + ", N'" + TenNCKH + "' as TenDanhMucNCKH"
                                                      + ", N'" + VaiTro + "' as TenVaiTro"
                                                      + ", N'" + KhoiLuongThucHien + "' as SoTiet"
                                                      + ", N'" + KhoiLuongSauTruDM + "' as GioQuyDoiNCKH"
                                                      + ", N'" + DinhMuc + "' as DinhMuc";
                                                sucessNumber++;
                                            }

                                        }
                                        else
                                        {
                                            erorrNumber++;
                                            errorLog.AppendLine("- STT: " + " Mã: " + MaGV + ", tên: " + TenGV + " không tồn tại.");
                                        }
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                    }
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
                                        //uow.CommitChanges();////Lưu                                        
                                        //sucessNumber++;
                                    }
                                    else
                                    {
                                        uow.RollbackTransaction(); ////trả lại dữ liệu ban đầu
                                        erorrNumber++;
                                        sucessImport = true;
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
                                    pImport[0] = new SqlParameter("@Quanly", OidQuanLy.Oid);
                                    pImport[1] = new SqlParameter("@txtString", sql.Substring(11));
                                    DataProvider.GetValueFromDatabase("spd_PMS_Import_NghienCuuKhoaHoc", CommandType.StoredProcedure, pImport);
                                    //uow.CommitChanges();//Lưu
                                }
                            }
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    if (erorrNumber == 0)
                        DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + "\n Số dòng không thành công " + erorrNumber + " " + s + "!");

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
                }
            }
        }
    }
}

