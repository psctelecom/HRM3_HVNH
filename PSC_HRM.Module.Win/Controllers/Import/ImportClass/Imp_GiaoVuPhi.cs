using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu;
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
    public class Imp_GiaoVuPhi
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyPhiGiaoVu OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A8:K]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            const int idMaHocPhan = 1;
                            const int idMaLopHocPhan = 2;
                            const int idTenHocPhan = 3;
                            const int idSiSo = 4;
                            const int idHeSoGiaoVuPhi = 5;
                            const int idHeSoHocKyHe = 6;
                            const int idGiaoViPhiHe = 7;
                            const int idGhiChu = 8;
                            const int idMaKhoa = 9;
                            const int idTenKhoa = 10;                          
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyPhiGiaoVu qly = uow.GetObjectByKey<QuanLyPhiGiaoVu>(OidQuanLy.Oid);
                                BoPhan bophan = null;
                                ChiTietPhiGiaoVu ct = null;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly!=null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        int SiSo = 0;                                      
                                        decimal HeSoGiaoVuPhi = 0;
                                        decimal HeSoHocKyHe = 0;
                                        decimal GiaoViPhiHe = 0;                                     
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region Mã học phần
                                        string txtMaHocPhan = dr[idMaHocPhan].ToString();
                                        #endregion

                                        #region Tên học phần
                                        string txtTenHocPhan = dr[idTenHocPhan].ToString();
                                        #endregion

                                        #region Mã lớp học phần
                                        string txtMaLopHocPhan = dr[idMaLopHocPhan].ToString();
                                        #endregion

                                        #region Ghi chú
                                        string txtGhiChu = dr[idGhiChu].ToString();
                                        #endregion

                                        #region Sỉ số
                                        if (dr[idSiSo].ToString() != string.Empty)
                                        {
                                            SiSo = Convert.ToInt32(dr[idSiSo].ToString());
                                        }
                                        #endregion

                                        #region Hệ số giáo vụ phí
                                        if (dr[idHeSoGiaoVuPhi].ToString() != string.Empty)
                                        {
                                            HeSoGiaoVuPhi = Convert.ToDecimal(dr[idHeSoGiaoVuPhi].ToString());
                                        }
                                        #endregion                                     

                                        #region Hệ số học kỳ hè
                                        if (dr[idHeSoHocKyHe].ToString() != string.Empty)
                                        {
                                            HeSoHocKyHe = Convert.ToDecimal(dr[idHeSoHocKyHe].ToString());
                                        }
                                        #endregion                                     

                                        #region Giáo vụ phí học kỳ hè
                                        if (dr[idGiaoViPhiHe].ToString() != string.Empty)
                                        {
                                            GiaoViPhiHe = Convert.ToDecimal(dr[idGiaoViPhiHe].ToString());
                                        }
                                        #endregion                                     

                                        #region Mã bộ phận
                                        string txtMaBoPhan = dr[idMaKhoa].ToString();
                                        #endregion

                                        #region Tên bộ phận
                                        string txtTenBoPhan = dr[idTenKhoa].ToString();
                                        #endregion

                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if ((!string.IsNullOrEmpty(txtMaBoPhan) || !string.IsNullOrEmpty(txtTenBoPhan)))
                                        {
                                            CriteriaOperator fBoPhan = CriteriaOperator.Parse("MaQuanLy_UIS =? Or TenBoPhan =?", txtMaBoPhan, txtTenBoPhan);
                                            bophan = uow.FindObject<BoPhan>(fBoPhan);
                                            if (bophan == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại bộ phận.");
                                            }
                                            else
                                            {                                           
                                                CriteriaOperator filter = CriteriaOperator.Parse("QuanLyPhiGiaoVu = ?", qly.Oid);
                                                XPCollection<ChiTietPhiGiaoVu> dsChiTietPhiGiaoVup = new XPCollection<ChiTietPhiGiaoVu>(uow, filter);
                                                if (dsChiTietPhiGiaoVup.Count == 0)
                                                {
                                                    ct = new ChiTietPhiGiaoVu(uow);
                                                    ct.BoPhan = bophan;
                                                    ct.GhiChu = txtGhiChu;
                                                    ct.GiaoVuPhi_HocKyHe = GiaoViPhiHe;
                                                    ct.MaHocPhan = txtMaHocPhan;
                                                    ct.TenHocPhan = txtTenHocPhan;
                                                    ct.MaLopHocPhan = txtMaLopHocPhan;
                                                    ct.SiSo = SiSo;
                                                    ct.HeSo_GiaoVu = HeSoGiaoVuPhi;
                                                    ct.HeSo_HocKyHe = HeSoHocKyHe;
                                                    ct.QuanLyPhiGiaoVu = uow.GetObjectByKey<QuanLyPhiGiaoVu>(qly.Oid);
                                                }
                                                //sucessNumber++;                                                                            
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
                                    uow.CommitChanges();//Lưu
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
        #endregion
    }
}