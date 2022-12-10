using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
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
    public class Imp_ChamBaiCoiThiRaDe
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyKhaoThi QuanLy)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();

            int STT = 0;
            string sql = ""; 
            DataTable dt;
            NhanVien _nhanVien;
            BoPhan _donVi;
            BoPhan _donViKeKhai;
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        //Loại file
                        //LoaiOfficeEnum loaiOffice = LoaiOfficeEnum.Office2003;
                        //if (open.SafeFileName.Contains(".xlsx"))
                        //{ loaiOffice = LoaiOfficeEnum.Office2010; }
                        dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A3:K]");
                        using (dt)
                        {
                            #region Import
                            if(TruongConfig.MaTruong=="DNU")
                            {
                                #region Khởi tạo các Idx
                                const int idDonVi = 1;
                                const int idTenDonVi = 2;
                                const int idMaGiangVien = 3;
                                const int idHoTen = 4;
                                const int idSoRaDe = 5;
                                const int idSoCoiThi = 6;
                                const int idSoChamBai = 7;
                                const int idTongGio = 8;
                                const int idDonViKeKhai = 9;
                                const int idTenDonViKeKhai = 10;

                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (QuanLy != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu- Kiểm tra dữ liệu

                                            #region DonVi
                                            string DonVi="";
                                            if (!string.IsNullOrEmpty(dr[idDonVi].ToString()))
                                                DonVi = dr[idDonVi].ToString();

                                            CriteriaOperator fDonVi = CriteriaOperator.Parse("MaQuanLy =? Or TenBoPhan =?", DonVi, DonVi);
                                            _donVi = uow.FindObject<BoPhan>(fDonVi);
                                            if (_donVi == null)
                                            {
                                                errorLog.Append("Đơn vị " + DonVi + " không hợp lệ");
                                                continue;
                                            }
                                            string TenDonVi = "";
                                            if (!string.IsNullOrEmpty(dr[idTenDonVi].ToString()))
                                                TenDonVi = dr[idTenDonVi].ToString();
                                            if (_donVi.TenBoPhan != TenDonVi)
                                            {
                                                errorLog.Append("Đơn vị " + DonVi + " và mã đơn vị không trùng khớp");
                                                continue;
                                            }
                                            #endregion

                                            #region MaGiangVien - HoTen
                                            string MaGiangVien = "";
                                            if (!string.IsNullOrEmpty(dr[idMaGiangVien].ToString()))
                                                MaGiangVien = dr[idMaGiangVien].ToString();
                                            string HoTen = "";
                                            if (!string.IsNullOrEmpty(dr[idHoTen].ToString()))
                                                HoTen = dr[idHoTen].ToString();

                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =? ", MaGiangVien);
                                            _nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                            if (_nhanVien == null)
                                            {
                                                errorLog.Append("Giảng viên " + HoTen + " không hợp lệ");
                                                continue;
                                            }
                                            if (_nhanVien.HoTen != HoTen)
                                            {
                                                errorLog.Append("Nhân viên " + DonVi + " và mã nhân viên không trùng khớp");
                                                continue;
                                            }
                                            #endregion


                                            #region SoRaDe
                                            string SoRaDe = "";
                                            if (!string.IsNullOrEmpty(dr[idSoRaDe].ToString()))
                                                SoRaDe = dr[idSoRaDe].ToString();
                                            #endregion

                                            #region SoChamBai
                                            string SoChamBai = "";
                                            if (!string.IsNullOrEmpty(dr[idSoChamBai].ToString()))
                                                SoChamBai = dr[idSoChamBai].ToString();
                                            #endregion

                                            #region SoCoiThi
                                            string SoCoiThi = "";
                                            if (!string.IsNullOrEmpty(dr[idSoCoiThi].ToString()))
                                                SoCoiThi = dr[idSoCoiThi].ToString();
                                            #endregion

                                            #region TongGio
                                            string TongGio = "";
                                            if (!string.IsNullOrEmpty(dr[idTongGio].ToString()))
                                                TongGio = dr[idTongGio].ToString();
                                            #endregion

                                            #region DonViKeKhai
                                            string DonViKeKhai = "";
                                            if (!string.IsNullOrEmpty(dr[idDonViKeKhai].ToString()))
                                                DonViKeKhai = dr[idDonViKeKhai].ToString();
                                            if (DonViKeKhai == "")
                                                DonViKeKhai = "06";
                                            CriteriaOperator fDonViKeKhai = CriteriaOperator.Parse("MaQuanLy =? Or TenBoPhan =?", DonViKeKhai, DonViKeKhai);
                                            _donViKeKhai = uow.FindObject<BoPhan>(fDonViKeKhai);
                                            if (_donViKeKhai == null)
                                            {
                                                errorLog.Append("Đơn vị (Kê khai) " + DonViKeKhai + " không hợp lệ");
                                                continue;
                                            }
                                            string TenDonViKeKhai = "";
                                            if (!string.IsNullOrEmpty(dr[idTenDonViKeKhai].ToString()))
                                                TenDonViKeKhai = dr[idTenDonViKeKhai].ToString();
                                            if (_donViKeKhai.TenBoPhan != TenDonViKeKhai)
                                            {
                                                errorLog.Append("Đơn vị " + DonVi + " và mã đơn vị (Kê khai) không trùng khớp");
                                                continue;
                                            }
                                            #endregion
                                            #endregion
                                            sql += " Union All select N'" + _donVi.Oid.ToString() + "' as DonVi"
                                                + ", N'" + _nhanVien.Oid.ToString() + "' as NhanVien"
                                                + ", N'" + HoTen + "' as HoTen"
                                                + ", N'" + SoRaDe.Replace(",", ".") + "' as SoRaDe"
                                                + ", N'" + SoChamBai.Replace(",", ".") + "' as SoChamBai"
                                                + ", N'" + SoCoiThi.Replace(",", ".") + "' as SoCoiThi"
                                                + ", N'" + TongGio.Replace(",", ".") + "' as TongGio"
                                                + ", N'" + _donViKeKhai.Oid.ToString() + "' as DonViKeKhai";
                                            //
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                            //#region Kiểm tra dữ liệu
                                         
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
                                                //uow.CommitChanges();////Lưu                                        
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
                                    }

                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    }
                                    else
                                    {
                                        #region Import
                                        SqlCommand cmd = new SqlCommand("spd_PMS_Import_QuanLyKhaoThi", DataProvider.GetConnection());
                                        cmd.CommandType = CommandType.StoredProcedure;
                                        cmd.CommandTimeout = 1800;
                                        cmd.Parameters.AddWithValue("@QuanLyKhaoThi", QuanLy.Oid);
                                        cmd.Parameters.AddWithValue("@NamHoc", QuanLy.NamHoc.Oid);
                                        cmd.Parameters.AddWithValue("@HocKy", QuanLy.HocKy != null ? QuanLy.HocKy.Oid : Guid.Empty);
                                        cmd.Parameters.AddWithValue("@ThongTinTruong", QuanLy.ThongTinTruong.Oid);
                                        cmd.Parameters.AddWithValue("@txtString", sql.Substring(11));
                                        cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName);
                                        cmd.ExecuteNonQuery();
                                        
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                        }
                    }

                    #region Xem log
                    //Mở file log lỗi lên
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số hoạt động không thành công " + erorrNumber + " " + s + "!");

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
    }
}