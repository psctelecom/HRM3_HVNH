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
    public class Imp_BoSungTaiKhoanNganHang
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:F]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idTT = 0;
                            const int idMaGV = 1;
                            const int idHoTen = 2;
                            const int idSoTK = 3;
                            const int idTenNganHang = 4;
                            const int idMaSothue = 5;

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

                                foreach (DataRow dr in dt.Rows)
                                {
                                    //Khởi Tạo
                                    STT++;
                                    nhanVien = null;
                                    //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                    var errorLog = new StringBuilder();
                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////
                                    #region 0. STT
                                    string txtSTT = dr[idTT].ToString().Trim();
                                    #endregion
                                    if (!string.IsNullOrEmpty(dr[idMaGV].ToString().Trim()))
                                    {
                                        #region 1. MaGV
                                        string txtMaGV = dr[idMaGV].ToString().Trim();
                                        #endregion
                                        #region 2. Họ tên
                                        string txtHoTen = dr[idHoTen].ToString().Trim();
                                        #endregion

                                        CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaGV);
                                        nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                        if(nhanVien != null)
                                        {
                                            #region 3. Số TK
                                            string txtTKNH = "";
                                            if (!string.IsNullOrEmpty(dr[idSoTK].ToString().Trim()))
                                              txtTKNH  = dr[idSoTK].ToString().Trim();
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Dữ liệu tài khoản ngân hàng sai.");
                                                erorrNumber++;
                                                continue;
                                            }
                                            #endregion
                                            #region 4. Tên ngân hàng
                                            //Nếu trong file Excel là số thì định dạng lại là chuỗi
                                            string txtTenNganHang = dr[idTenNganHang].ToString().Trim();
                                            #endregion
                                            #region 5. Mã số thuế
                                            //Nếu trong file Excel là số thì định dạng lại là chuỗi
                                            string txtMaSoThue = "";
                                            if (!string.IsNullOrEmpty(dr[idMaSothue].ToString().Trim()))
                                                txtMaSoThue = dr[idMaSothue].ToString().Trim();
                                            #endregion

                                            sql += " Union All select N'" + nhanVien.Oid + "' as NhanVien"
                                            + ", N'" + txtHoTen + "' as HoTen"
                                            + ", N'" + txtTKNH + "' as SoTKNH"
                                            + ", N'" + txtTenNganHang.Replace(",", ".") + "' as TenNganHang"
                                            + ", N'" + txtMaSoThue + "' as MaSoThue";
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                            erorrNumber++;
                                            continue;
                                        }
        
                                    }
                                    else
                                    {
                                        errorLog.AppendLine("- STT: " + STT + " Không tìm thấy mã GV.");
                                        erorrNumber++;
                                        continue;
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
                                        //uow.CommitChanges();////Lưu _ Cứ mỗi lần chạy vòng lặp thì Lưu 1 lần                                  
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
                                    uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                }
                                else
                                {
                                    //uow.CommitChanges();//Lưu _ Đúng hết rồi mới lưu 
                                    SqlParameter[] pQuyDoi = new SqlParameter[1];
                                    pQuyDoi[0] = new SqlParameter("@txtString", sql.Substring(11));
                                    DataProvider.ExecuteNonQuery("spd_PMS_Import_BoSungTaiKhoanNganHang", CommandType.StoredProcedure, pQuyDoi);
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
        #endregion
    }
}