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
    public class Import_UpdateVanBang
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A2:O]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idTT = 0;
                            const int idHoTen = 1;
                            const int idMaGiangVien = 2;
                            const int idTrinhDoChuyenMon = 3;
                            const int idChuyenMonDaoTao = 4;
                            const int idTruongDaoTao = 5;
                            const int idQuocGia = 6;
                            const int idHinhThucDaoTao = 7;
                            const int idNamTotNghiep = 8;
                            const int idDiemTrungBinh = 9;
                            const int idXepLoai = 10;
                            const int idNgayCapBang = 11;
                            const int idNgayApDung = 12;
                            const int idSoNamThamNien = 13;
                            const int idGhiChu = 14;

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

                                    #region TrinhDoChuyenmon
                                    string TrinhDoChuyenmon = "";
                                    if (dr[idTrinhDoChuyenMon].ToString() != string.Empty)
                                    {
                                        TrinhDoChuyenmon = dr[idTrinhDoChuyenMon].ToString().Trim();
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": Trình độ không được rỗng.");
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

                                    #region ChuyenMonDaoTao
                                    string ChuyenMonDaoTao = "";
                                    if (dr[idChuyenMonDaoTao].ToString() != string.Empty)
                                    {
                                        ChuyenMonDaoTao = dr[idChuyenMonDaoTao].ToString().Trim();
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": Chuyên môn đào tạo không được rỗng.");
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

                                    #region TruongDaoTao
                                    string TruongDaoTao = "";
                                    if (dr[idTruongDaoTao].ToString() != string.Empty)
                                    {
                                        TruongDaoTao = dr[idTruongDaoTao].ToString().Trim();
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": Trường đào tạo không được rỗng.");
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

                                    #region QuocGia
                                    string QuocGia = "Việt Nam";
                                    if (dr[idQuocGia].ToString() != string.Empty)
                                    {
                                        QuocGia = dr[idQuocGia].ToString().Trim();
                                    }
                                    #endregion

                                    #region HinhThucDaoTao
                                    string HinhThucDaoTao = "";
                                    if (dr[idHinhThucDaoTao].ToString() != string.Empty)
                                    {
                                        HinhThucDaoTao = dr[idHinhThucDaoTao].ToString().Trim();
                                    }
                                    //else
                                    //{
                                    //    errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": Hình thức đào tạo không được rỗng.");
                                    //    erorrNumber++;
                                    //    #region Ghi File log
                                    //    //Đưa thông tin bị lỗi vào blog
                                    //    if (errorLog.Length > 0)
                                    //    {
                                    //        mainLog.AppendLine("- STT: " + STT);
                                    //        mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                    //        mainLog.AppendLine(errorLog.ToString());
                                    //        sucessImport = false;
                                    //    }
                                    //    #endregion
                                    //    continue;
                                    //}
                                    #endregion

                                    #region NamTotNghiep
                                    string NamTotNghiep = "";
                                    if (dr[idNamTotNghiep].ToString() != string.Empty)
                                    {
                                        NamTotNghiep = dr[idNamTotNghiep].ToString().Trim();
                                    }
                                    //else
                                    //{
                                    //    errorLog.AppendLine(STT.ToString() + " + Năm tốt nghiệp không được rỗng.");
                                    //    continue;
                                    //}
                                    #endregion

                                    #region DiemTrungBinh
                                    string DiemTrungBinh = "";
                                    if (dr[idDiemTrungBinh].ToString() != string.Empty)
                                    {
                                        DiemTrungBinh = dr[idDiemTrungBinh].ToString().Trim();
                                    }
                                    //else
                                    //{
                                    //    errorLog.AppendLine(STT.ToString() + " + Năm tốt nghiệp không được rỗng.");
                                    //    continue;
                                    //}
                                    #endregion

                                    #region XepLoai
                                    string XepLoai = "";
                                    if (dr[idXepLoai].ToString() != string.Empty)
                                    {
                                        XepLoai = dr[idXepLoai].ToString().Trim();
                                    }
                                    //else
                                    //{
                                    //    errorLog.AppendLine(STT.ToString() + " + Năm tốt nghiệp không được rỗng.");
                                    //    continue;
                                    //}
                                    #endregion

                                    #region NgayCapBang
                                    DateTime NgayCapBang = DateTime.MinValue;
                                    //try
                                    //{
                                    if (dr[idNgayCapBang].ToString().Trim() != string.Empty)
                                        NgayCapBang = Convert.ToDateTime(dr[idNgayCapBang].ToString().Trim());
                                       
                                    //}
                                    //catch (Exception)
                                    //{
                                        //errorLog.AppendLine("Định dạng ngày không hợp lệ! (dd/MM/yyyy)");
                                        //erorrNumber++;
                                        //#region Ghi File log
                                        ////Đưa thông tin bị lỗi vào blog
                                        //if (errorLog.Length > 0)
                                        //{
                                        //    mainLog.AppendLine("- STT: " + STT);
                                        //    mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                        //    mainLog.AppendLine(errorLog.ToString());
                                        //    sucessImport = false;
                                        //}
                                        //#endregion
                                        //continue;
                                    //}
                                    #endregion

                                    #region NgayApDung
                                    DateTime NgayApDung = DateTime.MinValue;
                                    //try
                                    //{
                                    if (dr[idNgayApDung].ToString().Trim() != string.Empty)
                                        NgayApDung = Convert.ToDateTime(dr[idNgayApDung].ToString().Trim());
                                    //}
                                    //catch (Exception)
                                    //{
                                    //    errorLog.AppendLine("Định dạng ngày không hợp lệ! (dd/MM/yyyy)");
                                    //    erorrNumber++;
                                    //    #region Ghi File log
                                    //    //Đưa thông tin bị lỗi vào blog
                                    //    if (errorLog.Length > 0)
                                    //    {
                                    //        mainLog.AppendLine("- STT: " + STT);
                                    //        mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                    //        mainLog.AppendLine(errorLog.ToString());
                                    //        sucessImport = false;
                                    //    }
                                    //    #endregion
                                    //    continue;
                                    //}
                                    #endregion

                                    #region SoNamThamNien
                                    int SoNamThamNien = 0;
                                    //try
                                    //{
                                    if (dr[idSoNamThamNien].ToString().Trim() != string.Empty)
                                        SoNamThamNien = Convert.ToInt32(dr[idSoNamThamNien].ToString().Trim());
                                    //}
                                    //catch (Exception)
                                    //{

                                    //}
                                    #endregion

                                    #region GhiChu
                                    string GhiChu = "";
                                    if (dr[idGhiChu].ToString() != string.Empty)
                                    {
                                        GhiChu = dr[idGhiChu].ToString().Trim();
                                    }
                                    //else
                                    //{
                                    //    errorLog.AppendLine(STT.ToString() + " + Năm tốt nghiệp không được rỗng.");
                                    //    continue;
                                    //}
                                    #endregion

                                    #endregion

                                    sql += " Union All Select '" + _NhanVien.Oid.ToString() + "' as NhanVien, "
                                        + " N'" + TrinhDoChuyenmon + "' as TrinhDoChuyenmon, "
                                        + " N'" + ChuyenMonDaoTao + "' as ChuyenMonDaoTao, "
                                        + " N'" + TruongDaoTao + "' as TruongDaoTao, "
                                        + " N'" + QuocGia + "' as QuocGia, "
                                        + " N'" + HinhThucDaoTao + "' as HinhThucDaoTao, "
                                        + " N'" + NamTotNghiep + "' as NamTotNghiep, "
                                        + " N'" + DiemTrungBinh.Replace(",", ".") + "' as DiemTrungBinh, "
                                        + " N'" + XepLoai + "' as XepLoai, "
                                        + " N'" + NgayCapBang.ToShortDateString() + "' as NgayCapBang, "
                                        + " N'" + NgayApDung.ToShortDateString() + "' as NgayApDung, "
                                        + " N'" + SoNamThamNien + "' as SoNamThamNien, "
                                        + " N'" + GhiChu + "' as GhiChu";
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
                                    SqlCommand cmd = new SqlCommand("spd_HoSo_VanBang_CapNhatVanBang", DataProvider.GetConnection());
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