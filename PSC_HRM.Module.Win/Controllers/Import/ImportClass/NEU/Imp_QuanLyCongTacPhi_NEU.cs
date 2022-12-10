using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportClass.NEU
{
    public class Imp_QuanLyCongTacPhi_NEU
    {
        public static int XuLy(IObjectSpace obs, QuanLyCongTacPhi QuanLyCongTacPhi)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();
            string KQ = "";
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                string InsertSql = "";
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
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:G]"))
                            {
                                #region Khởi tạo các Idx

                                const int INDEX_SoThuTu = 0;
                                const int INDEX_MaGiangVien = 1;
                                const int INDEX_TenGiangVien = 2;
                                const int INDEX_MaLopHocPhan = 3;
                                const int INDEX_TenLopHocPhan = 4;
                                const int INDEX_SoTien = 5;
                                const int INDEX_GhiChu = 6;


                                Hashtable hashNhanVien = new Hashtable();

                                #endregion
                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    #region Khai Báo

                                    string STT = "";
                                    string MaGiangVien = "";
                                    string TenGiangVien = "";
                                    string MaLopHocPhan = "";
                                    string TenLopHocPhan = "";
                                    string SoTien = "";
                                    string GhiChu = "";


                                    var errorLog = new StringBuilder();
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT = dr[INDEX_SoThuTu].ToString();
                                        MaGiangVien = dr[INDEX_MaGiangVien].ToString();
                                        TenGiangVien = dr[INDEX_TenGiangVien].ToString();
                                        MaLopHocPhan = dr[INDEX_MaLopHocPhan].ToString();
                                        TenLopHocPhan = dr[INDEX_TenLopHocPhan].ToString();
                                        SoTien = dr[INDEX_SoTien].ToString();
                                        GhiChu = dr[INDEX_GhiChu].ToString();

                                        if (string.IsNullOrEmpty(MaGiangVien) || string.IsNullOrEmpty(TenGiangVien)
                                            || string.IsNullOrEmpty(MaLopHocPhan) || string.IsNullOrEmpty(TenLopHocPhan) || string.IsNullOrEmpty(SoTien))
                                        {
                                            erorrNumber++;
                                            errorLog.AppendLine("- STT: " + STT + " Không được để trống các dòng dữ liệu");
                                            continue;
                                        }
                                        if (string.IsNullOrEmpty(MaGiangVien) == false)
                                        {
                                            if (hashNhanVien[MaGiangVien] == null)
                                            {
                                                NhanVien nv = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", MaGiangVien));
                                                if (nv == null)
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + " Không tìm thấy mã nhân viên : " + MaGiangVien);
                                                    erorrNumber++;
                                                    continue;
                                                }
                                                else
                                                {
                                                    hashNhanVien.Add(MaGiangVien, nv.Oid);
                                                }
                                            }
                                        }
                                        if (string.IsNullOrEmpty(SoTien) == false)
                                        {
                                            if (IsMoney(SoTien) == false)
                                            {
                                                erorrNumber++;
                                                errorLog.AppendLine("- STT: " + STT + " Không thể định dạng được số tiền : " + SoTien);
                                                continue;
                                            }
                                        }
                                        if(string.IsNullOrEmpty(MaLopHocPhan) == false && string.IsNullOrEmpty(TenLopHocPhan) == false )
                                        {
                                            ChiTietCongTacPhi_NEU chi_tiet = uow.FindObject<ChiTietCongTacPhi_NEU>(CriteriaOperator.Parse("NhanVien = ? AND LopHocPhan = ? AND MaLopHocPhan = ?", hashNhanVien[MaGiangVien].ToString(),TenLopHocPhan,MaLopHocPhan));
                                            if(chi_tiet == null)
                                            {
                                                InsertSql +=   "UNION ALL \n" +
                                                               "SELECT '" + hashNhanVien[MaGiangVien].ToString() + "' as NhanVien," +
                                                               "N'" + MaLopHocPhan + "' as LopHocPhan," +
                                                               "N'" + TenLopHocPhan + " 'as TenHocPhan," +
                                                               "" + SoTien.Replace(",", ".") + " As SoTien," +
                                                               "N'" + GhiChu + "' As GhiChu \n";
                                                sucessNumber++;

                                            }
                                            else
                                            {
                                                sucessNumber++;
                                                chi_tiet.ThanhTien = decimal.Parse(SoTien);
                                            }
                                            uow.CommitChanges();
                                        }

                                    }

                                    //Đưa thông tin bị lỗi vào blog

                                    ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////     
                                }
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
                            SqlParameter[] pImport = new SqlParameter[3];
                            pImport[0] = new SqlParameter("@QuanLyCongTacPhi",QuanLyCongTacPhi.Oid);
                            pImport[1] = new SqlParameter("@StringInsert", (InsertSql.Length > 10) ? InsertSql.Substring(10) : "");
                            pImport[2] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                            pImport[2].Direction = ParameterDirection.Output;
                            DataProvider.GetValueFromDatabase("spd_PMS_Import_QuanLyCongTacPhi", CommandType.StoredProcedure, pImport);
                            KQ = pImport[2].Value.ToString();
                        }
                    }
                }
            }

            ////
            string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
            DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số dòng không thành công " + erorrNumber + " " + s + "!");

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
            if (KQ == "SUCCESS")
                return 201;
            else
                return 400;
        }
        private static bool IsMoney(string Money)
        {
            try
            {
                decimal.Parse(Money);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}