using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportClass
{
    public class Import_HoatDongKhac_HUFLIT
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, Guid _Quanly)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A2:I]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idSTT = 0;
                            const int idMaDV = 1;
                            const int idTenDV = 2;
                            const int idMaGV = 3;
                            const int idTenGV = 4;
                            const int idTenHoatDong = 5;
                            const int idDienGiai = 6;
                            const int idSoTiet= 7;
                            const int idDanhMuc = 8;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                int STT = 0;
                                NhanVien _NhanVien = null;
                                BoPhan _BoPhan = null;
                                string sql = "";
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var errorLog = new StringBuilder();
                                    STT++;
                                    #region Đọc dữ liệu                                

                                    #region GiangVien
                                    string MaGiangVien = "";
                                    string HoTen = "";

                                    if (dr[idTenGV].ToString() != string.Empty)
                                    {
                                        HoTen = dr[idTenGV].ToString().Trim();
                                    }
                                    if (dr[idMaGV].ToString() != string.Empty)
                                    {
                                        MaGiangVien = dr[idMaGV].ToString().Trim();
                                        _NhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaQuanLy =?", MaGiangVien));
                                        if (_NhanVien == null)
                                        {
                                            errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": không tìm thấy trên hệ thống.");
                                            erorrNumber++;
                                            #region Ghi File log
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            #endregion
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(STT.ToString() + " + Mã giảng viên không được rỗng.");
                                        erorrNumber++;
                                        #region Ghi File log
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        #endregion
                                        continue;
                                    }
                                    #endregion

                                    #region DonVi
                                    string MaDV = "";
                                    string TenDV = "";

                                    if (dr[idTenDV].ToString() != string.Empty)
                                    {
                                        TenDV = dr[idTenDV].ToString().Trim();
                                    }
                                    if (dr[idMaDV].ToString() != string.Empty)
                                    {
                                        MaDV = dr[idMaDV].ToString().Trim();
                                        _BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy =?", MaDV));
                                        if (_BoPhan == null)
                                        {
                                            errorLog.AppendLine(MaDV + " - " + TenDV + ": không tìm thấy trên hệ thống.");
                                            erorrNumber++;
                                            #region Ghi File log
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            #endregion
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(STT.ToString() + " + Mã đơn vị không được rỗng.");
                                        erorrNumber++;
                                        #region Ghi File log
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        #endregion
                                        continue;
                                    }
                                    #endregion

                                    #region TenHoatDong
                                    string TenHoatDong = "";
                                    if (dr[idTenHoatDong].ToString() != string.Empty)
                                    {
                                        TenHoatDong = dr[idTenHoatDong].ToString().Trim();
                                    }
                                    #endregion

                                    #region DienGiai
                                    string DienGiai = "";
                                    if (dr[idDienGiai].ToString() != string.Empty)
                                    {
                                        DienGiai = dr[idDienGiai].ToString().Trim();
                                    }
                                    #endregion

                                    #region SoTiet
                                    decimal SoTiet = 0;
                                    if (dr[idSoTiet].ToString() != string.Empty)
                                    {
                                        SoTiet = Convert.ToDecimal(dr[idSoTiet].ToString().Trim().Replace(".", ","));
                                    }

                                    #endregion

                                    #region DanhMuc
                                    int DanhMuc = 1;
                                    if (dr[idDanhMuc].ToString() != string.Empty)
                                    {
                                        DanhMuc = Convert.ToInt32(dr[idDanhMuc].ToString().Trim());
                                    }

                                    #endregion

                                    #endregion

                                    sql += " Union All Select '" + _NhanVien.Oid.ToString() + "' as NhanVien, "
                                        + " N'" + _Quanly + "' as OidQuanLy, "
                                        + " N'" + SoTiet.ToString().Replace(",", ".") + "' as SoTiet, "
                                        + " N'" + _BoPhan.Oid.ToString() + "' as BoPhan, "
                                        + " N'" + TenHoatDong + "' as TenHoatDong"
                                        + " N'" + DienGiai + "' as DienGiai"
                                        + " N'" + DanhMuc.ToString() + "' as DanhMuc"
                                        
                                        ;
                                    sucessNumber++;
                                }


                                //hợp lệ cả file mới lưu
                                if (erorrNumber > 0)
                                {
                                    //uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    #region Mở file log lỗi lên

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
                                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                                    DialogUtil.ShowInfo("Số dòng không thành công " + erorrNumber + " " + s + "!");
                                    #endregion
                                }
                                else
                                {
                                    //uow.CommitChanges();//Lưu _ Đúng hết rồi mới lưu 
                                    SqlCommand cmd = new SqlCommand("spd_Import_HoatDongKhac_Huflit", DataProvider.GetConnection());
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@string", sql.Substring(11));
                                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                                    cmd.ExecuteNonQuery();
                                    DialogUtil.ShowInfo("Số dòng thành công: " + sucessNumber + "!");
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