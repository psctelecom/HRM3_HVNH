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
using PSC_HRM.Module.ThuNhap.ThuLao;
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

namespace ERP.Module.Win.Controllers.Import.ImportClass.UEL
{
    public class Imp_TheoDoiTruTietChuan
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, BangThuLaoNhanVien OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:E]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            const int idMaGiangVien = 0;
                            const int idHoTen = 1;
                            const int idTietChuanTruocThanhToan = 2;
                            const int idTietChuanDaTruThanhToan = 3;
                            const int idTietChuanConLaiSauThanhToan = 4;         
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                BangThuLaoNhanVien qly = uow.GetObjectByKey<BangThuLaoNhanVien>(OidQuanLy.Oid);
                                NhanVien nhanVien = null;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly!=null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;                               
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region txtMaGiangVien
                                        string txtMaGiangVien = dr[idMaGiangVien].ToString();
                                        #endregion

                                        #region txtHoTen
                                        string txtHoTen = dr[idHoTen].ToString();
                                        #endregion

                                        #region txtTietChuanTruocThanhToan
                                        decimal txtTietChuanTruocThanhToan = 0;
                                        if (dr[idTietChuanTruocThanhToan].ToString() != string.Empty)
                                            txtTietChuanTruocThanhToan = Convert.ToDecimal(dr[idTietChuanTruocThanhToan].ToString());
                                        #endregion

                                        #region txtTietChuanDaTruThanhToan
                                        decimal txtTietChuanDaTruThanhToan = 0;
                                        if (dr[idTietChuanDaTruThanhToan].ToString() != string.Empty)
                                            txtTietChuanDaTruThanhToan = Convert.ToDecimal(dr[idTietChuanDaTruThanhToan].ToString());
                                        #endregion

                                        #region txtTietChuanConLaiSauThanhToan
                                        decimal txtTietChuanConLaiSauThanhToan = 0;
                                        if (dr[idTietChuanConLaiSauThanhToan].ToString() != string.Empty)
                                            txtTietChuanConLaiSauThanhToan = Convert.ToDecimal(dr[idTietChuanConLaiSauThanhToan].ToString());
                                        #endregion

                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if (!string.IsNullOrEmpty(txtMaGiangVien))
                                        {
                                            CriteriaOperator f = CriteriaOperator.Parse("MaQuanLy =?", txtMaGiangVien);
                                            nhanVien = uow.FindObject<NhanVien>(f);
                                            if (nhanVien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại cán bộ " + txtHoTen);
                                            }
                                            else
                                            {
                                                ChiTietTheoDoiTruTietChuan ctTheoDoi = new ChiTietTheoDoiTruTietChuan(uow);
                                                ctTheoDoi.NhanVien = nhanVien;
                                                ctTheoDoi.TietChuanTruocThanhToan = txtTietChuanTruocThanhToan;
                                                ctTheoDoi.TietChuanDaTruThanhToan = txtTietChuanDaTruThanhToan;
                                                ctTheoDoi.TietChuanConLaiSauThanhToan = txtTietChuanConLaiSauThanhToan;
                                                ctTheoDoi.BangThuLaoNhanVien = qly;
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