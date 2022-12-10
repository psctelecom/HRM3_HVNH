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
    public class Import_HeSoChucDanhNhanVien
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A2:F]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idSTT = 0;
                            const int idMaGV = 1;
                            const int idTenGV = 2;
                            const int idHeSoChucDanh = 3;
                            const int idGhiChu = 4;
                            const int idGhiChuNguoiDung = 5;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                int STT = 0;
                                NhanVien _NhanVien = null;
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

                                    #region HeSoChucDanh
                                    decimal HeSoChucDanh = 0;
                                    if (dr[idHeSoChucDanh].ToString() != string.Empty)
                                    {
                                        HeSoChucDanh = Convert.ToDecimal(dr[idHeSoChucDanh].ToString().Trim().Replace(".", ","));
                                    }
                          
                                    #endregion

                                    #region GhiChu
                                    string GhiChu = "";
                                    if (dr[idGhiChu].ToString() != string.Empty)
                                    {
                                        GhiChu = dr[idGhiChu].ToString().Trim();
                                    }

                                    #endregion
                                    #region GhiChuNguoiDung
                                    string GhiChuNguoiDung = "";
                                    if (dr[idGhiChuNguoiDung].ToString() != string.Empty)
                                    {
                                        GhiChuNguoiDung = dr[idGhiChuNguoiDung].ToString().Trim();
                                    }

                                    #endregion

                                    #endregion

                                    sql += " Union All Select '" + _NhanVien.Oid.ToString() + "' as NhanVien, "
                                        + " N'" + _Quanly + "' as OidQuanLy, "
                                        + " N'" + HeSoChucDanh.ToString().Replace(",", ".") + "' as HeSoChucDanh, "
                                        + " N'" + GhiChu + "' as GhiChu, "
                                        + " N'" + GhiChuNguoiDung + "' as GhiChuNguoiDung";
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
                                    SqlCommand cmd = new SqlCommand("spd_PMS_HeSo_HeSoChucDanh", DataProvider.GetConnection());
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