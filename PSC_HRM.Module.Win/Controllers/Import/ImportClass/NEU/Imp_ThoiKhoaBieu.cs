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
    public class Imp_ThoiKhoaBieu
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, Guid TKB, Guid BaoPhan, Guid BacDaoTao, Guid HeDaoTao)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            string sql = "";
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
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A5:W]"))
                            {
                                #region Khởi tạo các Idx
                                const int idMaDonVi = 1;
                                const int idBoMonQuanLy = 2;
                                const int idMaMonHoc = 3;
                                const int idTenMonHoc = 4;


                                const int idMaLopHocPhan = 5;
                                const int idTenLopHocPhan = 6;

                                const int idLoaiHocPhan = 7;
                                const int idBacDaoTao = 8;
                                const int idHeDaoTao = 9;

                                const int idSoTinChi = 10;
                                const int idSoTietDungLop = 11;
                                const int idSoTietHeThong = 12;

                                const int idSoSVDK = 13;

                                const int idThoiGianDiaDiem = 14;
                                const int idCoSoGiangDay = 15;
                                const int idPhongHoc = 16;
                                const int idNgoaiGio = 17;
                                const int idTiengAnh = 18;

                                const int idKhoaDaoTao = 19;

                                const int idHoTenGiangVien = 20;
                                const int idMaQuanLy = 21;
                                const int idGhiChu = 22;

                                #endregion
                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    int STT = 0;
                                    uow.BeginTransaction();
                                    #region Khai Báo

                                    string MaDonVi = "";
                                    string BoMonQuanLy = "";
                                    string MaMonHoc = "";
                                    string TenMonHoc = "";

                                    string MaLopHocPhan = "";
                                    string TenLopHocPhan = "";
                                    string LoaiHocPhan = "";
                                    string TenBacDaoTao = "";
                                    string TenHeDaoTao = "";

                                    string SoTinChi = "";
                                    string SoTietDungLop = "";
                                    string SoTietHeThong = "";

                                    string SoSinhVienDK = "";
                                    string ThoiGianDiaDiem = "";
                                    string CoSoGiangDay = "";
                                    string PhongHoc = "";
                                    string ApDungHeSoNgoaiGio = "";
                                    string ApDungHeSoTiengNuocNgoai = "";
                                    string KhoaDaoTao = "";

                                    string HoTenGiangVien = "";
                                    string MaQuanLy = "";
                                    string GhiChu = "";
                                    ThongTinNhanVien ttNhanVien = null;
                                    NhanVien nhanVien = null;
                                    BoPhan boMonQuanLy = null;
                                    string OidNhanVien = "";
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (TKB != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();

                                            #region ĐỌC DỮ LIỆU
                                            #region Thông tin môn học
                                            if (dr[idMaDonVi] != string.Empty)
                                                MaDonVi = dr[idMaDonVi].ToString().Replace(",", ".");
                                            if (dr[idBoMonQuanLy] != string.Empty)
                                                BoMonQuanLy = dr[idBoMonQuanLy].ToString().Replace(",", ".");
                                            if (dr[idMaMonHoc] != string.Empty)
                                                MaMonHoc = dr[idMaMonHoc].ToString().Replace(",", ".");
                                            if (dr[idTenMonHoc] != string.Empty)
                                                TenMonHoc = dr[idTenMonHoc].ToString().Replace(",", ".");


                                            if (dr[idMaLopHocPhan] != string.Empty)
                                                MaLopHocPhan = dr[idMaLopHocPhan].ToString().Replace(",", ".");
                                            if (dr[idTenLopHocPhan] != string.Empty)
                                                TenLopHocPhan = dr[idTenLopHocPhan].ToString().Replace(",", ".");
                                            if (dr[idLoaiHocPhan] != string.Empty)
                                                LoaiHocPhan = dr[idLoaiHocPhan].ToString().Replace(",", ".");

                                            if (dr[idBacDaoTao] != string.Empty)
                                                TenBacDaoTao = dr[idBacDaoTao].ToString().Replace(",", ".");

                                            if (dr[idHeDaoTao] != string.Empty)
                                                TenHeDaoTao = dr[idHeDaoTao].ToString().Replace(",", ".");


                                            if (dr[idSoTinChi] != string.Empty)
                                                SoTinChi = dr[idSoTinChi].ToString().Replace(",", ".");
                                            #endregion

                                            #region Hệ số - Tiết
                                            if (dr[idSoTietDungLop] != string.Empty)
                                                SoTietDungLop = dr[idSoTietDungLop].ToString().Replace(",", ".");
                                            if (dr[idSoTietHeThong] != string.Empty)
                                                SoTietHeThong = dr[idSoTietHeThong].ToString().Replace(",", ".");

                                            if (dr[idSoSVDK] != string.Empty)
                                                SoSinhVienDK = dr[idSoSVDK].ToString().Replace(",", ".");

                                            if (dr[idThoiGianDiaDiem] != string.Empty)
                                                ThoiGianDiaDiem = dr[idThoiGianDiaDiem].ToString().Replace(",", ".");
                                            if (dr[idCoSoGiangDay] != string.Empty)
                                                CoSoGiangDay = dr[idCoSoGiangDay].ToString().Replace(",", ".");
                                            if (dr[idPhongHoc] != string.Empty)
                                                PhongHoc = dr[idPhongHoc].ToString().Replace(",", ".");

                                            if (dr[idNgoaiGio] != string.Empty)
                                                ApDungHeSoNgoaiGio = dr[idNgoaiGio].ToString().Replace(",", ".");

                                            if (dr[idTiengAnh] != string.Empty)
                                                ApDungHeSoTiengNuocNgoai = dr[idTiengAnh].ToString().Replace(",", ".");
                                            #endregion

                                            if (dr[idKhoaDaoTao] != string.Empty)
                                                KhoaDaoTao = dr[idKhoaDaoTao].ToString().Replace(",", ".");

                                            #region Giảng viên
                                            if (dr[idHoTenGiangVien] != string.Empty)
                                                HoTenGiangVien = dr[idHoTenGiangVien].ToString().Replace(",", ".");
                                            if (dr[idMaQuanLy] != string.Empty)
                                                MaQuanLy = dr[idMaQuanLy].ToString().Replace(",", ".");

                                            if (dr[idGhiChu] != string.Empty)
                                                GhiChu = dr[idGhiChu].ToString().Replace(",", ".");
                                            #endregion

                                            #endregion
                                            #region KIỂM TRA VÀ LẤY DỮ LIỆU
                                           // if (MaQuanLy != string.Empty)
                                            {
                                                if (MaQuanLy == "" || MaQuanLy == string.Empty)
                                                {
                                                    OidNhanVien = Guid.Empty.ToString();
                                                }

                                                else
                                                {
                                                    CriteriaOperator fttNhaNVien = CriteriaOperator.Parse("MaQuanLy =? or SoHieuCongChuc =?", MaQuanLy, MaQuanLy);
                                                    ttNhanVien = uow.FindObject<ThongTinNhanVien>(fttNhaNVien);
                                                    if (ttNhanVien != null)
                                                        OidNhanVien = ttNhanVien.Oid.ToString();
                                                    if (ttNhanVien == null)
                                                    {
                                                        if(MaQuanLy == "01.003.TG0089")
                                                        {

                                                        }
                                                        CriteriaOperator fNhaNVien = CriteriaOperator.Parse("MaThinhGiang =?", MaQuanLy);
                                                        nhanVien = uow.FindObject<GiangVienThinhGiang>(fNhaNVien);
                                                        if (nhanVien != null)
                                                            OidNhanVien = nhanVien.Oid.ToString();
                                                    }
                                                }
                                                if (OidNhanVien != null || OidNhanVien != string.Empty || OidNhanVien != "")
                                                {
                                                    boMonQuanLy = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaDonVi =? or MaQuanLy =?", MaDonVi, MaDonVi));

                                                    if (boMonQuanLy != null)
                                                    {
                                                        sql += " Union All select N'" + boMonQuanLy.Oid + "' as OidBoMonQuanLy"

                                                                      + ", N'" + MaMonHoc + "' as MaMonHoc"
                                                                      + ", N'" + TenMonHoc + "' as TenMonHoc"
                                                                      + ", N'" + MaLopHocPhan + "' as MaLopHocPhan"
                                                                      + ", N'" + TenLopHocPhan + "' as TenLopHocPhan"
                                                                      + ", N'" + LoaiHocPhan + "' as LoaiHocPhan"
                                                                      + ", N'" + TenBacDaoTao + "' as TenBacDaoTao"
                                                                      + ", N'" + TenHeDaoTao + "' as TenHeDaoTao"
                                                                      + ", N'" + SoTinChi + "' as SoTinChi"

                                                                      + ", N'" + SoTietDungLop + "' as SoTietDungLop"
                                                                      + ", N'" + SoTietHeThong + "' as SoTietHeThong"

                                                                      + ", N'" + SoSinhVienDK + "' as SoSinhVienDK"
                                                                      + ", N'" + ThoiGianDiaDiem + "' as ThoiGianDiaDiem"
                                                                      + ", N'" + CoSoGiangDay + "' as CoSoGiangDay"

                                                                      + ", N'" + PhongHoc + "' as PhongHoc"

                                                                      + ", N'" + ApDungHeSoNgoaiGio + "' as ApDungHeSoNgoaiGio"
                                                                      + ", N'" + ApDungHeSoTiengNuocNgoai + "' as ApDungHeSoTiengNuocNgoai"

                                                                      + ", N'" + KhoaDaoTao + "' as KhoaDaoTao"

                                                                      + ", N'" + OidNhanVien + "' as OidNhanVien"

                                                                      + ", N'" + GhiChu + "' as GhiChu";

                                                    }
                                                    else
                                                    {
                                                        erorrNumber++;
                                                        errorLog.AppendLine("- STT: " + "Mã đơn vị/bộ môn - " + MaDonVi + " không tồn tại.");
                                                    }
                                                }
                                                else
                                                {
                                                    erorrNumber++;
                                                    errorLog.AppendLine("- STT: " + " - " + HoTenGiangVien + " Không tồn tại nhân viên.");
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

                                        #region Mở file log lỗi lên
                                        string tenFile = "Import_Log.txt";
                                        StreamWriter writer = new StreamWriter(tenFile);
                                        writer.WriteLine(mainLog.ToString());
                                        writer.Flush();
                                        writer.Close();
                                        writer.Dispose();
                                        HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                        Process.Start(tenFile);

                                        #endregion
                                    }
                                    else
                                    {
                                        SqlParameter[] pImport = new SqlParameter[5];
                                        pImport[0] = new SqlParameter("@ThoiKhoaBieu", TKB);
                                        pImport[1] = new SqlParameter("@BacDaoTao", BacDaoTao);
                                        pImport[2] = new SqlParameter("@HeDaoTao", HeDaoTao);
                                        pImport[3] = new SqlParameter("@String", sql.Substring(11));
                                        pImport[4] = new SqlParameter("@User", HamDungChung.CurrentUser().UserName);
                                        DataProvider.GetValueFromDatabase("spd_PMS_Import_ThoiKhoaBieu", CommandType.StoredProcedure, pImport); uow.CommitChanges();//Lưu

                                        DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " dòng dữ liệu");
                                    }
                                }
                            }
                        }
                    }
                    //
                }
            }
        }
    }
        #endregion
}