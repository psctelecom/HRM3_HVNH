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
using PSC_HRM.Module.PMS.NghiepVu.GDTC_QP;
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
    public class Imp_GiaoDucTheChat_QuocPhong
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyGDTC_QP OidQuanLy)
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
                            const int idTT = 0;
                            const int idMaCB_UIS = 1;
                            const int idHoTen = 2;
                            const int idLopHocPhan = 3;
                            const int idTenHocPhan = 4;
                            const int idHocKy = 5;
                            const int idSoTC = 6;
                            const int idLyThuyet= 7;
                            const int idThaoLuan = 8;
                            const int idThucHanh = 9;
                            const int idSiSo = 10;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyGDTC_QP qly = uow.GetObjectByKey<QuanLyGDTC_QP>(OidQuanLy.Oid);
                                ChiTietThongKeGDQP_TC ct = null;
                                psc_UIS_GiangVien nhanVien;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly!=null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        int SiSo = 0;                                      
                                        decimal SoTC = 0;
                                        decimal LyThuyet = 0;
                                        decimal ThaoLuan = 0;
                                        decimal ThucHanh = 0;
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region Thứ tự
                                        string txtTT = dr[idTT].ToString();
                                        #endregion

                                        #region Mã cán bộ UIS
                                        string txtMaCB = dr[idMaCB_UIS].ToString();
                                        #endregion

                                        #region Họ và tên
                                        string txtHoTen = dr[idHoTen].ToString();
                                        #endregion

                                        #region Lớp học phần
                                        string txtLopHocPhan = dr[idLopHocPhan].ToString();
                                        #endregion

                                        #region Tên học phần
                                        string txtTenHocPhan = dr[idTenHocPhan].ToString();
                                        #endregion

                                        #region Học kỳ
                                        string txtHocKy = dr[idHocKy].ToString();
                                        #endregion

                                        #region Sỉ số
                                        if (dr[idSiSo].ToString() != string.Empty)
                                        {
                                            SiSo = Convert.ToInt32(dr[idSiSo].ToString());
                                        }
                                        #endregion

                                        #region Số Tính Chỉ
                                        if (dr[idSoTC].ToString() != string.Empty)
                                        {
                                            SoTC = Convert.ToDecimal(dr[idSoTC].ToString());
                                        }
                                        #endregion                                     

                                        #region Lý thuyết
                                        if (dr[idLyThuyet].ToString() != string.Empty)
                                        {
                                            LyThuyet = Convert.ToDecimal(dr[idLyThuyet].ToString());
                                        }
                                        #endregion                                     

                                        #region Thảo luận
                                        if (dr[idThaoLuan].ToString() != string.Empty)
                                        {
                                            ThaoLuan = Convert.ToDecimal(dr[idThaoLuan].ToString());
                                        }
                                        #endregion                                     

                                        #region Thực hành
                                        if (dr[idThucHanh].ToString() != string.Empty)
                                        {
                                            ThucHanh = Convert.ToDecimal(dr[idThucHanh].ToString());
                                        }
                                        #endregion  


                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if ((!string.IsNullOrEmpty(txtMaCB) || !string.IsNullOrEmpty(txtHoTen)))
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("ProfessorID =?", txtMaCB);
                                            nhanVien = uow.FindObject<psc_UIS_GiangVien>(fNhanVien);
                                            if (nhanVien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Vui lòng kiểm tra lại nhân viên " + txtHoTen + ".");
                                            }
                                            else
                                            {
                                                if (nhanVien.OidNhanVien != Guid.Empty)
                                                {
                                                    CriteriaOperator fNV = CriteriaOperator.Parse("Oid =?", nhanVien.OidNhanVien);
                                                    NhanVien NV = uow.FindObject<NhanVien>(fNV);

                                                    CriteriaOperator filter = CriteriaOperator.Parse("QuanLyGDTC_QP = ?", qly.Oid);
                                                    XPCollection<ChiTietThongKeGDQP_TC> dsChiTietThongKeGDQP_TC = new XPCollection<ChiTietThongKeGDQP_TC>(uow, filter);
                                                    if (dsChiTietThongKeGDQP_TC.Count == 0)
                                                    {
                                                        ct = new ChiTietThongKeGDQP_TC(uow);
                                                        ct.QuanLyGDTC_QP = uow.GetObjectByKey<QuanLyGDTC_QP>(qly.Oid);
                                                        ct.NhanVien = uow.GetObjectByKey<NhanVien>(NV.Oid);
                                                        ct.BoPhan = NV.BoPhan;
                                                        ct.SoCMND = NV.CMND;
                                                        ct.Email = NV.Email;
                                                        ct.LopHP = txtLopHocPhan;
                                                        ct.TenHP = txtTenHocPhan;
                                                        ct.SoTC = SoTC;
                                                        ct.SoTietLT = LyThuyet;
                                                        ct.SoTiet_TNTH = ThucHanh;
                                                        ct.ThaoLuan = ThaoLuan;
                                                        ct.SiSo = SiSo;
                                                        ct.HK = txtHocKy;
                                                        ct.KyTinhPMS = qly.KyTinhPMS;
                                                    }
                                                    //sucessNumber++;                                                                            
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + "- Nhân viên " + txtMaCB + " - " + txtHoTen + " chưa kết nối HRM.");
                                                }
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