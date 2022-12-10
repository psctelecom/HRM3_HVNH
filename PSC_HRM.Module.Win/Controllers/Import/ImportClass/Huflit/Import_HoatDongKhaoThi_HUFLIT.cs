using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
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

namespace PSC_HRM.Module.Win.Controllers.Import.ImportClass
{
    class Import_HoatDongKhaoThi_HUFLIT
    {
        public static void XuLy(IObjectSpace obs, QuanLyKhaoThi _Quanly)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:M]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int ID_STT = 0;
                            const int ID_MaGV = 1;
                            const int ID_VaiTroCoiThi = 2;
                            const int ID_MaLopHP = 3;
                            const int ID_MaMon = 4;
                            const int ID_TenMon = 5;
                            const int ID_NgaySinh = 6;
                            const int ID_GioBatDau = 7;
                            const int ID_PhongThi = 8;
                            const int ID_MaHinhThucThi = 9;
                            const int ID_ThoiGianThi = 10;
                            const int ID_SoBai = 11;
                            const int ID_LoaiKhaoThi = 12;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                int STT = 0;
                                NhanVien _NhanVien = null;
                                HinhThucThi _HinhThucThi = null;
                                string sql = "";



                                //Duyệt qua tất cả các dòng trong file excel
                                foreach (DataRow dr in dt.Rows)
                                {
                                    var errorLog = new StringBuilder();
                                    STT++;


                                    string STRING_STT = dr[ID_STT].ToString();
                                    string STRING_MaGV = dr[ID_MaGV].ToString();
                                    string STRING_VaiTroCoiThi = dr[ID_VaiTroCoiThi].ToString();
                                    string STRING_MaLopHp = dr[ID_MaLopHP].ToString();
                                    string STRING_MaMon = dr[ID_MaMon].ToString();
                                    string STRING_TenMon = dr[ID_TenMon].ToString();
                                    string STRING_NgaySinh = dr[ID_NgaySinh].ToString();
                                    string STRING_GioBatDau = dr[ID_GioBatDau].ToString();
                                    string STRING_PhongThi = dr[ID_PhongThi].ToString();
                                    string STRING_MaHinhThucThi = dr[ID_MaHinhThucThi].ToString();
                                    string STRING_ThoiGianThi = dr[ID_ThoiGianThi].ToString();
                                    string STRING_SoBai = dr[ID_SoBai].ToString();
                                    string STRING_LoaiKhaoThi = dr[ID_LoaiKhaoThi].ToString();



                                    string MaGiangVien = "";
                                    string HoTen = "";

                                    if (string.IsNullOrEmpty(STRING_STT) || string.IsNullOrEmpty(STRING_MaGV) || string.IsNullOrEmpty(STRING_VaiTroCoiThi) || string.IsNullOrEmpty(STRING_MaLopHp)
                                       || string.IsNullOrEmpty(STRING_MaMon) || string.IsNullOrEmpty(STRING_TenMon) || string.IsNullOrEmpty(STRING_NgaySinh)
                                       || string.IsNullOrEmpty(STRING_GioBatDau) || string.IsNullOrEmpty(STRING_PhongThi) || string.IsNullOrEmpty(STRING_MaHinhThucThi)
                                       || string.IsNullOrEmpty(STRING_ThoiGianThi) || string.IsNullOrEmpty(STRING_SoBai) || string.IsNullOrEmpty(STRING_LoaiKhaoThi))
                                    {
                                        errorLog.AppendLine("Không được để trống các cột trong file excel");
                                        erorrNumber++;
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        continue;
                                    }
                                    if (!string.IsNullOrEmpty(STRING_MaGV))
                                    {
                                        _NhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaQuanLy =?", STRING_MaGV));
                                        if (_NhanVien == null)
                                        {
                                            errorLog.AppendLine(HoTen + " - " + MaGiangVien + ": không tìm thấy trên hệ thống.");
                                            erorrNumber++;
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(STT.ToString() + " + Mã giảng viên không được rỗng.");
                                        erorrNumber++;
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        continue;
                                    }

                                    if (!string.IsNullOrEmpty(STRING_MaHinhThucThi))
                                    {
                                        _HinhThucThi = uow.FindObject<HinhThucThi>(CriteriaOperator.Parse("MaQuanLy =?", STRING_MaHinhThucThi));
                                        if (_HinhThucThi == null)
                                        {
                                            errorLog.AppendLine("Không tìm thấy hình thức thi trong hệ thống.");
                                            erorrNumber++;
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(STT.ToString() + " + Hình thức thi không được để trống");
                                        erorrNumber++;
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        continue;
                                    }

                                    if (!string.IsNullOrEmpty(STRING_ThoiGianThi) || !string.IsNullOrEmpty(STRING_LoaiKhaoThi))
                                    {
                                        if (IsNumber(STRING_ThoiGianThi) == false)
                                        {
                                            errorLog.AppendLine("Không thể đọc được thời gian thi vì bạn đã nhập sai kiểu.");
                                            erorrNumber++;
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            continue;
                                        }
                                        if(IsLoaiKhaoThi(STRING_LoaiKhaoThi) == false)
                                        {
                                            errorLog.AppendLine("Không thể đọc được loại khảo thí vì bạn đã nhập sai kiểu.");
                                            erorrNumber++;
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + STT);
                                                mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                                mainLog.AppendLine(errorLog.ToString());
                                                sucessImport = false;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(STT.ToString() + " + Không được bỏ các cột trong phần mềm để trống");
                                        erorrNumber++;
                                        //Đưa thông tin bị lỗi vào blog
                                        if (errorLog.Length > 0)
                                        {
                                            mainLog.AppendLine("- STT: " + STT);
                                            mainLog.AppendLine(string.Format("- STT: {0} không import vào phần mềm được: ", STT));
                                            mainLog.AppendLine(errorLog.ToString());
                                            sucessImport = false;
                                        }
                                        continue;
                                    }



                                    sql += " Union All Select '" + STRING_MaGV + "' as NhanVien, \n"
                                        + " N'" + STRING_VaiTroCoiThi + "' as VaiTroCoiThi, \n"
                                        + " N'" + STRING_MaLopHp +"' as MaLopHocPhan, \n"
                                        + " N'" + STRING_MaMon + "' as MaMon, \n"
                                        + " N'" + STRING_TenMon + "' as TenMon, \n"
                                        + " N'" + STRING_NgaySinh + "' as NgayThi, \n"
                                        + " N'" + STRING_GioBatDau + "' as GioBatDau, \n"
                                        + " N'" + STRING_PhongThi + "' as PhongThi, \n"
                                        + " N'" + STRING_MaHinhThucThi + "' as MaHinhThucThi, \n"
                                        + " " + STRING_ThoiGianThi + " as ThoiGianThi, \n"
                                        + " " + STRING_SoBai + " as SoBai, \n"
                                        + " " + STRING_LoaiKhaoThi + " as LoaiKhaoThi \n";
                                    sucessNumber++;
                                }


                                //hợp lệ cả file mới lưu
                                if (erorrNumber > 0)
                                {
                                    //uow.RollbackTransaction(); //trả lại dữ liệu ban đầu

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
                                }
                                else
                                {
                                    uow.CommitChanges();//Lưu _ Đúng hết rồi mới lưu 
                                    SqlCommand cmd = new SqlCommand("spd_PMS_Import_HoatDongKhaoThi", DataProvider.GetConnection());
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@string", sql.Substring(11));
                                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                                    cmd.Parameters.AddWithValue("@QuanLyKhaoThi", _Quanly.Oid);
                                    cmd.ExecuteNonQuery();
                                    DialogUtil.ShowInfo("Số dòng thành công: " + sucessNumber + "!");
                                }
                            }
                        }

                    }
                }
            }
        }
        public static bool IsNumber(string Number)
        {
            try
            {
                int temp = int.Parse(Number);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsLoaiKhaoThi(string LoaiKhaoThi)
        {
            try
            {
                int temp = int.Parse(LoaiKhaoThi);
                if(temp != 0 && temp != 1 )
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
