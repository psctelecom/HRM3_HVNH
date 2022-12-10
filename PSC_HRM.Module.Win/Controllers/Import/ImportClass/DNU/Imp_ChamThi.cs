using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportClass.DNU
{
    public class Imp_ChamThi
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyKhaoThi OidQuanLy)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();

            int STT = 0;
            DataTable dt;
            NhanVien _NhanVien;
            BoPhan _ToKhoa;
            BoPhan _BoPhan = null;
            //

            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A7:I]");
                        string sql = "";
                        using (dt)
                        { 
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////
                            #region Khởi tạo các Idx
                            const int idSTT = 0;
                            const int idMon = 1;
                            const int idMaNV = 2;
                            const int idHoTenNV = 3;
                            const int idToKhoa = 4;
                            const int idSoBai = 5;
                            const int idTienChamBai = 6;
                            const int idThanhTien = 7;
                            const int idKyNhan = 8;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                //Duyệt qua tất cả các dòng trong file excel
                                
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                    STT++;
                                    var errorLog = new StringBuilder();
                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                    //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////

                                    #region Đọc dữ liệu
                                        #region HoTen
                                        string HoTen = "";
                                        if (dr[idHoTenNV].ToString() != string.Empty)
                                        {
                                            HoTen = dr[idHoTenNV].ToString().Trim();
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(STT.ToString() + " + Họ tên không được rỗng.");

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

                                        #region MaGiangVien
                                        string MaGiangVien = "";
                                        if (dr[idMaNV].ToString() != string.Empty)
                                        {
                                            MaGiangVien = dr[idMaNV].ToString().Trim();
                                            _NhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaQuanLy =?", MaGiangVien));
                                            _BoPhan = _NhanVien.BoPhan;
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

                                        #region Tổ/BoMon
                                        string ToKhoa = "";
                                        if (dr[idToKhoa].ToString() != string.Empty)
                                        {
                                            ToKhoa = dr[idToKhoa].ToString().Trim();
                                            _ToKhoa = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy =? or TenBoPhan =?", ToKhoa, ToKhoa));
                                            if (_ToKhoa == null)
                                            {
                                                errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": đơn vị không tìm thấy trên hệ thống.");
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
                                            errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": Đơn vị không được rỗng.");
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
     
                                        #region Tên môn học: Môn Chấm
                                        string TenMonHoc = "";
                                        if (dr[idMon].ToString() != string.Empty)
                                        {
                                            TenMonHoc = dr[idMon].ToString();
                                        }
                                        #endregion
                                        #region Số bài chấm thi
                                        string SoBaiCham = "";
                                        if (dr[idSoBai].ToString() != string.Empty)
                                        {
                                            SoBaiCham = dr[idSoBai].ToString();
                                        }
                                        #endregion
                                        #region Don Gia
                                        string DonGia = "";
                                        if (dr[idTienChamBai].ToString() != string.Empty)
                                        {
                                            DonGia = dr[idTienChamBai].ToString().Trim();
                                        }

                                        #endregion

                                        #region Don Gia
                                        string ThanhTien = "";
                                        if (dr[idThanhTien].ToString() != string.Empty)
                                        {
                                            ThanhTien = dr[idThanhTien].ToString().Trim();
                                        }

                                        #endregion
                                        
                                    #endregion
                                    sql += " Union All select N'" + _ToKhoa.Oid.ToString() + "' as BoPhan_To"
                                        + ", N'" + _NhanVien.Oid.ToString() + "' as NhanVien"
                                        //+ ", N'" + _BoPhan.Oid.ToString() + "' as BoPhanNhanVien"
                                        + ", N'" + HoTen + "' as HoTen"
                                        + ", N'" + SoBaiCham.Replace(",", ".") + "' as SoBaiCham"
                                        + ", N'" + OidQuanLy.DonGiaChamThi + "' as DonGia"
                                        + ", N'" + TenMonHoc + "' as TenMonHoc"
                                        + ", N'" + DonGia + "' as DonGiaFile"
                                        + ", N'" + ThanhTien + "' as ThanhTien";
                                    sucessNumber++;
                                    sucessImport = true;           
                                    
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
                                    SqlCommand cmd = new SqlCommand("spd_PMS_Import_QuanLyChamThi", DataProvider.GetConnection());
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@txtString", sql.Substring(11));
                                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                                    cmd.Parameters.AddWithValue("@QuanLyKhaoThi", OidQuanLy.Oid);
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
  