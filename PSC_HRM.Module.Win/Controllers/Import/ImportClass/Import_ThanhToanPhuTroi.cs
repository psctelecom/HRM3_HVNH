using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PSC_HRM.Module.HoSo;
using System.Data.SqlClient;


namespace PSC_HRM.Module.Win.Controllers.Import.ImportClass
{
    class Import_ThanhToanPhuTroi
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanlyThanhToan OidQuanLy)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();

            int STT = 0;
            //
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Filter = "Excel file (*.xls)|*.xls;*.xlsx";
                //
                if (open.ShowDialog() == DialogResult.OK)
                {
                    using (DialogUtil.AutoWait())
                    {
                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:H]"))
                        {
                            /////////////////////////////KIÊM3 TRA NẾU BẢNG RỖNG THÌ KHÔNG CẦN IMPORT TIẾP/////////////////////////////////
                            if (dt.Rows.Count > 0)
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX////////////////////////////////////////////////////
                                #region Khởi tạo các Idx
                                const int idMaQuanLy = 0;
                                const int idHoTen = 1;
                                const int idHSLuong = 2;
                                const int idHSChucVu = 3;
                                const int idHSThamNienVK = 4;
                                const int idHSThamNienNghe_PhanTram = 5;
                                const int idHSThamNienNghe_So = 6;
                                const int idPhanTramPhuCap = 7;
                                #endregion
                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////
                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    #region Khai báo                                   
                                    QuanlyThanhToan _QuanLyTT = uow.GetObjectByKey<QuanlyThanhToan>(OidQuanLy.Oid);
                                    NhanVien _nv = null;
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (_QuanLyTT != null)
                                    {
                                        //Đọc từng dòng trong bảng Import 
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////
                                            string txtHoTen = "";
                                            string txtMaQuanLy = "";
                                            decimal txtHSLuong = 0;
                                            decimal txtHSChucVu = 0;
                                            decimal txtHSThamNienVK = 0;
                                            decimal txtHSThamNienNghe_PhanTram = 0;
                                            decimal txtHSThamNienNghe_So = 0;
                                            decimal txtHSPhanTramPhuCap = 0;
                                            _nv = null;
                                                                                      
                                            #region 0. Ktra dữ liệu
                                            txtHoTen = dr[idHoTen].ToString().Trim();
                                            if (!string.IsNullOrEmpty(dr[idMaQuanLy].ToString().Trim()))                                           
                                            {   
                                                #region 1. Mã quản lý
                                                txtMaQuanLy = dr[idMaQuanLy].ToString().Trim();
                                                _nv = uow.FindObject<NhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", txtMaQuanLy));
                                                if (_nv == null)
                                                {
                                                    errorLog.Append("Không tồn tại NV : "+txtHoTen);
                                                    erorrNumber++;
                                                    continue;
                                                }
                                                #endregion
                                                // Cho vào đây vì muốn import vào phải có mã nhân viên(điều kiện tiên quyết).
                                                #region 2. Hệ Số Lương
                                                if (!string.IsNullOrEmpty(dr[idHSLuong].ToString().Trim()))
                                                    txtHSLuong = Convert.ToDecimal(dr[idHSLuong].ToString().Trim());
                                                #endregion

                                                #region 3. Hệ Số Chức Vụ
                                                if (!string.IsNullOrEmpty(dr[idHSChucVu].ToString().Trim()))
                                                    txtHSChucVu = Convert.ToDecimal(dr[idHSChucVu].ToString().Trim());
                                                #endregion

                                                #region 4. Hệ số ThamNienVK
                                                if (!string.IsNullOrEmpty(dr[idHSThamNienVK].ToString().Trim()))
                                                    txtHSThamNienVK = Convert.ToDecimal(dr[idHSThamNienVK].ToString().Trim());
                                                else if (txtMaQuanLy != "")
                                                {
                                                    errorLog.Append("Vui lòng kiểm tra dòng: " + STT + " hệ số thâm niên(hệ số phải là số nguyên)");
                                                    erorrNumber++;
                                                }
                                                #endregion

                                                #region 5. Hệ số ThamNienNghe_PhanTram
                                                if (!string.IsNullOrEmpty(dr[idHSThamNienNghe_PhanTram].ToString().Trim()))
                                                    txtHSThamNienNghe_PhanTram = Convert.ToDecimal(dr[idHSThamNienNghe_PhanTram].ToString().Trim());
                                                #endregion

                                                #region 6. Hệ số ThamNienNghe_So
                                                if (!string.IsNullOrEmpty(dr[idHSThamNienNghe_So].ToString().Trim()))
                                                    txtHSThamNienNghe_So = Convert.ToDecimal(dr[idHSThamNienNghe_So].ToString().Trim());
                                                #endregion

                                                #region 7. Hệ Số PhanTramPhuCap
                                                if (!string.IsNullOrEmpty(dr[idPhanTramPhuCap].ToString().Trim()))
                                                    txtHSPhanTramPhuCap = Convert.ToDecimal(dr[idPhanTramPhuCap].ToString().Trim());
                                                else if (txtMaQuanLy != "")
                                                {
                                                    errorLog.Append("Vui lòng kiểm tra dòng: " + STT + " hệ số phụ cấp ưu đãi(hệ số phải là số nguyên)");
                                                    erorrNumber++;
                                                }
                                                #endregion
                                            }  
                                            #endregion
                                         
                                            if (_nv != null)
                                            {                                             
                                                //Cập nhật trực tiếp vào bảng NhanVienThongTinLuong
                                                _nv.NhanVienThongTinLuong.HeSoLuong = txtHSLuong;
                                                _nv.NhanVienThongTinLuong.HSPCChucVu = txtHSChucVu;
                                                _nv.NhanVienThongTinLuong.VuotKhung = Convert.ToInt32(txtHSThamNienVK);
                                                _nv.NhanVienThongTinLuong.ThamNien = txtHSThamNienNghe_PhanTram;
                                                _nv.NhanVienThongTinLuong.HSPCThamNien = txtHSThamNienNghe_So;
                                                _nv.NhanVienThongTinLuong.PhuCapUuDai = Convert.ToInt32(txtHSPhanTramPhuCap);
                                                sucessNumber++;
                                            }
                                           
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
                                                //sucessNumber++;
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
                                        uow.CommitChanges();//Lưu _ Đúng hết rồi mới lưu 
                                    }
                                }
                            }
                        }
                        #region Xem log
                        //Mở file log lỗi lên
                        string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                        DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số Import không thành công " + erorrNumber + " " + s + "!");

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
}

