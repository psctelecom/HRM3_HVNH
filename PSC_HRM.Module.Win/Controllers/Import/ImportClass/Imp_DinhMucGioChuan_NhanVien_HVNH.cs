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
    public class Imp_DinhMucGioChuan_NhanVien_HVNH
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
                        if (TruongConfig.MaTruong == "HVNH")
                        {
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[DinhMucGioChuan$A4:G]"))
                            {
                                const int idSTT = 0;
                                const int idMaGV = 1;
                                const int idHoTen = 2;
                                const int idDinhMuc = 3;
                                const int idGiamTruDinhMuc = 4;
                                const int idDinhMucSau = 5;
                                const int idLyDoGiamTru = 6;
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
                                            string MaGV = "";
                                            if (dr[idMaGV].ToString() != string.Empty)
                                                MaGV = dr[idMaGV].ToString();
                                            string DinhMuc = "";
                                            if (dr[idDinhMuc].ToString() != string.Empty)
                                                DinhMuc = dr[idDinhMuc].ToString().Replace(",", ".");
                                            string GiamTruDinhMuc = "";
                                            if (dr[idGiamTruDinhMuc].ToString() != string.Empty)
                                                GiamTruDinhMuc = dr[idGiamTruDinhMuc].ToString().Replace(",", ".");
                                            string DinhMucSau = "";
                                            if (dr[idDinhMucSau].ToString() != string.Empty)
                                                DinhMucSau = dr[idDinhMucSau].ToString().Replace(",", ".");
                                            string LyDoGiamTru = "";
                                            if (dr[idLyDoGiamTru].ToString() != string.Empty)
                                                LyDoGiamTru = dr[idLyDoGiamTru].ToString();
                                            #endregion
                                            #endregion
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Xử lý sql
                                            sql += " Union All select N'" + MaGV + "' as MaGV"
                                                           + ", N'" + DinhMuc + "' as DinhMuc"
                                                           + ", N'" + GiamTruDinhMuc + "' as GiamTruDinhMuc"
                                                           + ", N'" + DinhMucSau + "' as DinhMucSau"
                                                           + ", N'" + LyDoGiamTru + "' as LyDoGiamTru";
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
                                        //uow.CommitChanges();//Lưu
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

        public static void XuLyGioTruKhac(IObjectSpace obs, QuanLyGioChuan OidQuanLy)
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
                        if (TruongConfig.MaTruong == "HVNH")
                        {
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[DinhMucGioChuan$A2:E]"))
                            {
                                const int idSTT = 0;
                                const int idMaGV = 1;
                                const int idHoTen = 2;
                                const int idDonVi = 3;
                                const int idSoGo = 4;
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
                                            string MaGV = "";
                                            if (dr[idMaGV].ToString() != string.Empty)
                                                MaGV = dr[idMaGV].ToString();
                                            string SoGo = "";
                                            if (dr[idSoGo].ToString() != string.Empty)
                                                SoGo = dr[idSoGo].ToString().Replace(",", ".");
                                            #endregion

                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            sql += " Union All select N'" + MaGV + "' as MaGV"
                                                           + ", N'" + SoGo + "' as SoGo";
                                            sucessNumber++;
                                        }
                                        object kq = null;
                                        SqlParameter[] pImport = new SqlParameter[3];
                                        pImport[0] = new SqlParameter("@OidQuanLy", OidQuanLy.Oid);
                                        pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                        pImport[2] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                                        kq = DataProvider.GetValueFromDatabase("spd_PMS_Import_GioTruKhac", CommandType.StoredProcedure, pImport);
                                        if (kq != null)
                                            sucessNumber = Convert.ToInt32(kq.ToString());
                                        //uow.CommitChanges();//Lưu
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
    }
}