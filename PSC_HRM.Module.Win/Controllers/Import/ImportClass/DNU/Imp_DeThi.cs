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
    public class Imp_DeThi
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyKhaoThi QuanLy)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();

            int STT = 0;
            DataTable dt;
            NhanVien _NhanVien;
            BoPhan _donVi;
            
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A8:K]");
                        string sql = "";
                        using (dt)
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idTenMonHoc = 1;
                            const int idHoTen = 2;
                            const int idMaGiangVien = 3;
                            const int idToKhoa = 4;
                            const int idDonVi = 5;
                            const int idSoBoDeThi = 6;
                            const int idDuyetDe = 7;
                            const int idTienRaDe = 8;
                            const int idTienDuyetDe = 9;
                            const int idKyNhan = 10;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var errorLog = new StringBuilder();
                                    STT++;
                                    #region Đọc dữ liệu


                                    #region HoTen
                                    string HoTen = "";
                                    if (dr[idHoTen].ToString() != string.Empty)
                                    {
                                        HoTen = dr[idHoTen].ToString().Trim();
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
                                    if (dr[idMaGiangVien].ToString() != string.Empty)
                                    {
                                        MaGiangVien = dr[idMaGiangVien].ToString().Trim();
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

                                    #region TenMonHoc
                                    string TenMonHoc = "";

                                    if (dr[idTenMonHoc].ToString() != string.Empty)
                                    {
                                        TenMonHoc = dr[idTenMonHoc].ToString();
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(STT.ToString() + " + Tên môn học không được rỗng.");

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

                                    #region ToKhoa
                                    string ToKhoa = "";
                                    if (dr[idToKhoa].ToString() != string.Empty)
                                    {
                                        ToKhoa = dr[idToKhoa].ToString().Trim();
                                    }

                                    #endregion

                                    #region DonVi
                                    string DonVi = "";
                                    if (dr[idDonVi].ToString() != string.Empty)
                                    {
                                        DonVi = dr[idDonVi].ToString().Trim();
                                        _donVi = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy =? or TenBoPhan =?", DonVi, DonVi));
                                        if (_donVi == null)
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

                                    #region SoBoDe
                                    string SoBoDe = "";
                                    if (dr[idSoBoDeThi].ToString() != string.Empty)
                                    {
                                        SoBoDe = dr[idSoBoDeThi].ToString().Trim();
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": Số bộ đề không được rỗng.");
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

                                    #region DuyetDe
                                    int DuyetDe = 0;
                                    if (dr[idDuyetDe].ToString() != string.Empty)
                                    {
                                        DuyetDe = 1;
                                    }
                                    #endregion

                                    #region SoTienRaDe
                                    string SoTienRaDe = "";
                                    if (dr[idTienRaDe].ToString() != string.Empty)
                                    {
                                        SoTienRaDe = dr[idTienRaDe].ToString().Trim();
                                    }

                                    #endregion

                                    #region SoTienDuyetDe
                                    string SoTienDuyetDe = "";
                                    if (dr[idTienDuyetDe].ToString() != string.Empty)
                                    {
                                        SoTienDuyetDe = dr[idTienDuyetDe].ToString().Trim();
                                    }

                                    #endregion
                                    #region KyNhan
                                    string KyNhan = "";
                                    if (dr[idKyNhan].ToString() != string.Empty)
                                    {
                                        KyNhan = dr[idKyNhan].ToString().Trim();
                                    }

                                    #endregion

                                    #endregion

                                    sql += " Union All Select '" + _NhanVien.Oid.ToString() + "' as NhanVien, "
                                        + " N'" + TenMonHoc + "' as TenMonHoc, "
                                        + " N'" + ToKhoa + "' as ToKhoa, "
                                        + " N'" + _donVi.Oid.ToString() + "' as DonVi, "
                                        + " N'" + SoBoDe + "' as SoBoDe, "
                                        + " N'" + DuyetDe.ToString() + "' as DuyetDe, "
                                        + " N'" + SoTienRaDe.Replace(",", ".") + "' as SoTienRaDe, "
                                        + " N'" + SoTienDuyetDe.Replace(",", ".") + "' as SoTienDuyetDe,"
                                        + " N'" + KyNhan.Replace(",", ".") + "' as KyNhan";
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
                                    SqlCommand cmd = new SqlCommand("spd_PMS_Import_LamDeThi", DataProvider.GetConnection());
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@string", sql.Substring(11));
                                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                                    cmd.Parameters.AddWithValue("@QuanLy", QuanLy.Oid);
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
