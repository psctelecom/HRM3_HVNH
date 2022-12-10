using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu;
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
    public class Import_KeKhai_HDKhac_TKB
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, Guid KeKhai)
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
                        //Loại file
                        //LoaiOfficeEnum loaiOffice = LoaiOfficeEnum.Office2003;
                        //if (open.SafeFileName.Contains(".xlsx"))
                        //{ loaiOffice = LoaiOfficeEnum.Office2010; }
                        if (TruongConfig.MaTruong == "NEU")
                        {
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A5:V]"))
                            {
                                #region Khởi tạo các Idx

                                const int idMaDonVi = 1;
                                const int idKhoaVien = 2;
                                const int idBoMonQuanLy = 3;
                                const int idMaMon = 4;
                                const int idTenMonHoc = 5;
                                const int idMaLopHocPhan = 6;
                                const int idLopMonHoc = 7;
                                const int idHoTen = 8;
                                const int idMaGiangVien = 9;
                                const int idCMND = 10;

                                const int idBaiKT = 11;
                                const int idBaiThi = 12;
                                const int idBaiTapLon = 13;
                                const int idBaiTieuLuan = 14;
                                const int idDeAnMonHoc = 15;
                                const int idChuyenDeTN = 16;
                                
                                const int idKhac = 17;
                                const int idSlotHoc = 18;
                                const int idTraLoiCauHoi = 19;
                                const int idTruyCapLopHoc = 20;
                                const int idSoDeRaDe = 21;


                                #endregion
                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    int STT = 0;
                                    uow.BeginTransaction();
                                    string sql = "";
                                    #region Khai Báo
                                    string MaDonVi = "";
                                    string KhoaVien = "";
                                    string BoMonQuanLy = "";

                                    string HoTen = "";
                                    string MaQuanLy = "";
                                    string CMND = "";

                                    string MaMonHoc = "";
                                    string TenMonHoc = "";
                                    string MaLopHocPhan = "";
                                    string LopMonHoc = "";

                                    string BaiKT = "";
                                    string BaiThi = "";
                                    string BaiTapLon = ""; 
                                    string BaiTieuLuan = ""; 
                                    string DeAnMonHoc = ""; 
                                    string ChuyenDeTN = "";
                                    string Khac = "";
                                    string SlotHoc = "";
                                    string TraLoiCauHoi = "";
                                    string TruyCapLopHoc = "";
                                    string RaDe = "";

                                    NhanVien nhanVien = null; 
                                    BoPhan donVi = null;
                                    BoPhan khoaVien = null;
                                    BoPhan boMonQuanLy = null;
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (KeKhai != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();

                                            #region ĐỌC DỮ LIỆU
                                            if (dr[idMaDonVi] != string.Empty)
                                                MaDonVi = dr[idMaDonVi].ToString().Replace(",", ".");

                                            if (dr[idKhoaVien] != string.Empty)
                                                KhoaVien = dr[idKhoaVien].ToString().Replace(",", ".");

                                            if (dr[idBoMonQuanLy] != string.Empty)
                                                BoMonQuanLy = dr[idBoMonQuanLy].ToString().Replace(",", ".");

                                            if (dr[idHoTen] != string.Empty)
                                                HoTen = dr[idHoTen].ToString().Replace(",", ".");

                                            if (dr[idMaGiangVien] != string.Empty)
                                                MaQuanLy = dr[idMaGiangVien].ToString().Replace(",", ".");

                                            if (dr[idCMND] != string.Empty)
                                                CMND = dr[idCMND].ToString().Replace(",", ".");

                                            if (dr[idMaMon] != string.Empty)
                                                MaMonHoc = dr[idMaMon].ToString().Replace(",", ".");
                                            if (dr[idTenMonHoc] != string.Empty)
                                                TenMonHoc = dr[idTenMonHoc].ToString().Replace(",", ".");

                                            if (dr[idMaLopHocPhan] != string.Empty)
                                                MaLopHocPhan = dr[idMaLopHocPhan].ToString().Replace(",", ".");
                                            if (dr[idLopMonHoc] != string.Empty)
                                                LopMonHoc = dr[idLopMonHoc].ToString().Replace(",", ".");

                                            if (dr[idBaiKT] != string.Empty)
                                                BaiKT = dr[idBaiKT].ToString().Replace(",", "."); 
                                            if (dr[idBaiThi] != string.Empty)
                                                BaiThi = dr[idBaiThi].ToString().Replace(",", ".");
                                            if (dr[idBaiTapLon] != string.Empty)
                                                BaiTapLon = dr[idBaiTapLon].ToString().Replace(",", ".");
                                            if (dr[idBaiTieuLuan] != string.Empty)
                                                BaiTieuLuan = dr[idBaiTieuLuan].ToString().Replace(",", ".");
                                            if (dr[idDeAnMonHoc] != string.Empty)
                                                DeAnMonHoc = dr[idDeAnMonHoc].ToString().Replace(",", ".");
                                            if (dr[idChuyenDeTN] != string.Empty)
                                                ChuyenDeTN = dr[idChuyenDeTN].ToString().Replace(",", ".");

                                            if (dr[idKhac] != string.Empty)
                                                Khac = dr[idKhac].ToString().Replace(",", ".");
                                            if (dr[idSlotHoc] != string.Empty)
                                                SlotHoc = dr[idSlotHoc].ToString().Replace(",", ".");
                                            if (dr[idTraLoiCauHoi] != string.Empty)
                                                TraLoiCauHoi = dr[idTraLoiCauHoi].ToString().Replace(",", ".");
                                            if (dr[idTruyCapLopHoc] != string.Empty)
                                                TruyCapLopHoc = dr[idTruyCapLopHoc].ToString().Replace(",", ".");
                                            if (dr[idSoDeRaDe] != string.Empty)
                                                RaDe = dr[idSoDeRaDe].ToString().Replace(",", ".");
                                            #endregion

                                            #region KIỂM TRA VÀ LẤY DỮ LIỆU
                                            if (MaQuanLy != string.Empty || CMND != string.Empty)
                                            {
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =? or CMND =? or SoHoChieu=?", MaQuanLy, CMND);
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                                if (nhanVien != null)
                                                {
                                                    donVi = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaDonVi =?", MaDonVi));
                                                    if (donVi != null)
                                                    {
                                                        khoaVien = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan =?", KhoaVien));
                                                        if (khoaVien != null)
                                                        {
                                                            boMonQuanLy = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan =?", BoMonQuanLy));
                                                            if (boMonQuanLy != null)
                                                            {
                                                                sql += " Union All select N'" + donVi.Oid + "' as OidDonVi"
                                                                                            + ", N'" + khoaVien.Oid + "' as OidKhoaVien"
                                                                                            + ", N'" + boMonQuanLy.Oid + "' as OidBoMonQuanLy"
                                                                                            + ", N'" + nhanVien.Oid + "' as OidNhanVien"
                                                                                            + ", N'" + MaMonHoc + "' as MaMonHoc"
                                                                                            + ", N'" + TenMonHoc + "' as TenMonHoc"
                                                                                            + ", N'" + MaLopHocPhan + "' as MaLopHocPhan"
                                                                                            + ", N'" + LopMonHoc + "' as TenLopHocPhan"

                                                                                            + ", N'" + BaiKT + "' as BaiKT"
                                                                                            + ", N'" + BaiThi + "' as BaiThi"
                                                                                            + ", N'" + BaiTapLon + "' as BaiTapLon"
                                                                                            + ", N'" + BaiTieuLuan + "' as BaiTieuLuan"
                                                                                            + ", N'" + DeAnMonHoc + "' as DeAnMonHoc"
                                                                                            + ", N'" + ChuyenDeTN + "' as ChuyenDeTN"
                                                                                            + ", N'" + Khac + "' as Khac"
                                                                                            + ", N'" + SlotHoc + "' as SlotHoc"
                                                                                            + ", N'" + TraLoiCauHoi + "' as TraLoiCauHoi"
                                                                                            + ", N'" + TruyCapLopHoc + "' as TruyCapLopHoc"
                                                                                            + ", N'" + RaDe + "' as RaDe";
                                                            }
                                                            else
                                                            {
                                                                erorrNumber++;
                                                                errorLog.AppendLine("- STT: " + "Bộ môn  - " + BoMonQuanLy + " không tồn tại.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            erorrNumber++;
                                                            errorLog.AppendLine("- STT: " + "Khoa -Viện  - " + KhoaVien + " không tồn tại.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        erorrNumber++;
                                                        errorLog.AppendLine("- STT: " + "Mã đơn vị - " + MaDonVi + " không tồn tại.");
                                                    }
                                                }
                                                else
                                                {
                                                    erorrNumber++;
                                                    errorLog.AppendLine("- STT: " + " - " + HoTen + " Không tồn tại nhân viên.");
                                                }
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
                                    }

                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        //uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                        DialogUtil.ShowInfo("Số dòng không thành công " + erorrNumber + " !");
                                    }
                                    else
                                    {
                                        SqlParameter[] pImport = new SqlParameter[2];
                                        pImport[0] = new SqlParameter("@KeKhai", KeKhai);
                                        pImport[1] = new SqlParameter("@String", sql.Substring(11));
                                        DataProvider.GetValueFromDatabase("spd_PMS_Import_KeKhai_CacHDKhac_ThoiKhoaBieu", CommandType.StoredProcedure, pImport); 
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
        #endregion
}