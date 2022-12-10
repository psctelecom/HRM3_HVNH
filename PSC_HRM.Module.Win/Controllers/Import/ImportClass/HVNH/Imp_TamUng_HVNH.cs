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
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen;
using PSC_HRM.Module.PMS.NghiepVu.TamUngThuLao;
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
    public class Imp_TamUng_HVNH
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyGioGiang OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A3:F]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idSTT = 0;
                            const int idMaGiangVien= 1;
                            const int idHoTen = 2;
                            const int idTamUngTS = 3;
                            const int idTamUngBN = 4;
                            const int idTamUngPY = 5;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                NhanVien nhanvien = null;
                                NhanVien_GioGiang ct = null;
                                int STT = 0;
                                string sql = "";
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (OidQuanLy != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        #region Khởi tạo
                                        STT++;
                                        decimal TamUngTS = 0;
                                        decimal TamUngBN = 0;
                                        decimal TamUngPY = 0;
                                       
                                        #endregion
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //                                        

                                        #region idTamUngTS
                                        if (dr[idTamUngTS].ToString() != string.Empty)
                                        {                                          
                                            TamUngTS = Convert.ToDecimal(dr[idTamUngTS].ToString());
                                        }
                                        #endregion

                                        #region idTamUngBN
                                        if (dr[idTamUngBN].ToString() != string.Empty)
                                        {
                                            TamUngBN = Convert.ToDecimal(dr[idTamUngBN].ToString());
                                        }
                                        #endregion

                                        #region idTamUngPY
                                        if (dr[idTamUngPY].ToString() != string.Empty)
                                        {
                                            TamUngPY = Convert.ToDecimal(dr[idTamUngPY].ToString());
                                        }
                                        #endregion

                                        #region NhanVien
                                        if (dr[idMaGiangVien].ToString() != string.Empty)
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy = ?", dr[idMaGiangVien].ToString());
                                            nhanvien = uow.FindObject<NhanVien>(fNhanVien);
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có nhân viên.");
                                        }
                                        #endregion
                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                        #region Kiểm tra dữ liệu
                                        if (nhanvien != null)
                                        {
                                            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyGioGiang =? and NhanVien=?", OidQuanLy.Oid, nhanvien.Oid);
                                            XPCollection<NhanVien_GioGiang> dsChiTietKhoiLuongSauDaiHoc = new XPCollection<NhanVien_GioGiang>(uow, filter);

                                            if (dsChiTietKhoiLuongSauDaiHoc.Count == 0)
                                            {
                                                ct = new NhanVien_GioGiang(uow);
                                                ct.QuanLyGioGiang = uow.GetObjectByKey<QuanLyGioGiang>(OidQuanLy.Oid);
                                                ct.TamUng = TamUngTS;
                                                ct.TamUngBN = TamUngBN;
                                                ct.TamUngPY = TamUngPY;
                                                ct.NhanVien = nhanvien;                                        
                                            }     
                                            else
                                            {
                                                sql += " Union All select '" + nhanvien.Oid + "' as NhanVien"
                                                    + ", '" + OidQuanLy.Oid + "' as QuanLyGioGiang"
                                                    + ", " + TamUngTS.ToString().Replace(",", ".") + " as TamUngTS"
                                                    + ", " + TamUngBN.ToString().Replace(",", ".") + " as TamUngBN"
                                                    + ", " + TamUngPY.ToString().Replace(",", ".") + " as TamUngPY";
                                            }                                   
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + "- Nhân viên không tồn tại.");
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


                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    }
                                    else
                                    {
                                        uow.CommitChanges();//Lưu
                                        SqlParameter[] pImport = new SqlParameter[1];
                                        pImport[0] = new SqlParameter("@String", sql.Substring(11).ToString());
                                        DataProvider.ExecuteNonQuery("spd_PMS_Import_UpdateTamUng", CommandType.StoredProcedure, pImport);
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
        #endregion
    }
}
