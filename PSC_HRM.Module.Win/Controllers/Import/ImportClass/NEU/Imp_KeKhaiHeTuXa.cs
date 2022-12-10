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
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
using PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
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
    public class Imp_KeKhaiHeTuXa
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, KeKhai_CacHoatDong_ThoiKhoaBieu OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A2:H]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            const int idSTT = 0;
                            const int idMaLopHocPhan = 1;
                            const int idHoTen = 2;
                            const int idSoHieu = 3;
                            const int idTraLoiCauHoi = 4;
                            const int idChamDiemTraLoi = 5;
                            const int idSoBaiKiemTra = 6;
                            const int idSoBaiTieuLuan = 7;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                int STT = 0;
                                string sql = "";
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (OidQuanLy != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        int TraLoiCauHoi = 0;
                                        int ChamDiemTraLoi = 0;
                                        int SoBaiKiemTra = 0;
                                        int SoBaiTieuLuan = 0;
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region 
                                        string txtMaLopHocPhan = dr[idMaLopHocPhan].ToString();
                                        string txtSoHieu = dr[idSoHieu].ToString();
                                        #endregion                                                                           

                                        #region 
                                        if (dr[idTraLoiCauHoi].ToString() != string.Empty)
                                        {
                                            TraLoiCauHoi = Convert.ToInt32(dr[idTraLoiCauHoi].ToString());
                                        }
                                        if (dr[idChamDiemTraLoi].ToString() != string.Empty)
                                        {
                                            ChamDiemTraLoi = Convert.ToInt32(dr[idChamDiemTraLoi].ToString());
                                        }
                                        if (dr[idSoBaiKiemTra].ToString() != string.Empty)
                                        {
                                            SoBaiKiemTra = Convert.ToInt32(dr[idSoBaiKiemTra].ToString());
                                        }
                                        if (dr[idSoBaiTieuLuan].ToString() != string.Empty)
                                        {
                                            SoBaiTieuLuan = Convert.ToInt32(dr[idSoBaiTieuLuan].ToString());
                                        }
                                        #endregion    
                                        sql += " Union All select '" + txtMaLopHocPhan + "' as MaLopHocPhan"
                                                    + ", '" + txtSoHieu + "' as SoHieu"
                                                    + ", " + TraLoiCauHoi.ToString().Replace(",", ".") + " as TraLoiCauHoi"
                                                    + ", " + ChamDiemTraLoi.ToString().Replace(",", ".") + " as ChamDiemTraLoi"
                                                    + ", " + SoBaiKiemTra.ToString().Replace(",", ".") + " as SoBaiKiemTra"
                                                    + ", " + SoBaiTieuLuan.ToString().Replace(",", ".") + " as SoBaiTieuLuan";
                                    }
                                }

                                //hợp lệ cả file mới lưu
                                if (erorrNumber > 0)
                                {
                                    uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                }
                                else
                                {
                                    SqlParameter[] pImport = new SqlParameter[2];
                                    pImport[0] = new SqlParameter("@String", sql.Substring(11).ToString());
                                    pImport[1] = new SqlParameter("@KeKhai", OidQuanLy.Oid);
                                    DataProvider.ExecuteNonQuery("spd_PMS_Import_KeKhaiTKB_TuXa", CommandType.StoredProcedure, pImport);
                                }
                            }
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    //DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số dòng không thành công " + erorrNumber + " " + s + "!");

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
                    #endregion
                    #endregion
                }
            }
        }


         public static void XuLyNew(IObjectSpace obs, KeKhai_CacHoatDong_ThoiKhoaBieu OidQuanLy)
        {
            int sucessNumber = 0;
            int erorrNumber = 0;
            bool sucessImport = true;
            var mainLog = new StringBuilder();
            LoaiHoatDong _lhd = null; 
            BoPhan _bomonquanly = null;
            NhanVien _nv = null;
            ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu ct = null;
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet$A2:I]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            const int idxSTT = 0;
                            const int idxChiTiet = 1;
                            const int idxSoHieu = 2;
                            const int idxHoTen = 3;
                            const int idxSoLuong = 4;
                            const int idxLoaiHuongDan = 5;
                            const int idxBoMonQuanLy = 6;
                            const int idxTenMonHoc = 7;
                            const int idxLopMonHoc = 8;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                int dem = 0;
                                string sql = "";
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (OidQuanLy != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        dem++;                                      
                                        int SoLuong = 0;
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                                                          
                                        string _stt=dr[idxSTT].ToString();
                                        string _oidChiTiet=dr[idxChiTiet].ToString();
                                        string _sohieu=dr[idxSoHieu].ToString();
                                        string _HoTen=dr[idxHoTen].ToString();
                                        string _SoLuong=dr[idxSoLuong].ToString();
                                        string _LoaiHuongDan=dr[idxLoaiHuongDan].ToString();
                                        string _BoMonQuanLy=dr[idxBoMonQuanLy].ToString();
                                        string _TenMonHoc=dr[idxTenMonHoc].ToString();
                                        string _LopMonHoc=dr[idxLopMonHoc].ToString();
                                     
                                        #endregion                                                                           

                                        ////////////////////////// KIỂM TRA DỮ LIỆU và LẤY DỮ LIỆU /////////////////////////////
                                        if (sucessImport == true)
                                        {
                                            if (string.IsNullOrEmpty(_stt) || string.IsNullOrEmpty(_sohieu) || string.IsNullOrEmpty(_HoTen) || string.IsNullOrEmpty(_SoLuong) || string.IsNullOrEmpty(_BoMonQuanLy) || string.IsNullOrEmpty(_TenMonHoc) || string.IsNullOrEmpty(_LopMonHoc) || string.IsNullOrEmpty(_LoaiHuongDan))
                                            {
                                                sucessImport = false;
                                                errorLog.AppendLine(string.Format("-- Không được để trống các cột trong phần execl"));
                                            }                                       
                                        }
                                        if (sucessImport == true)
                                        {
                                            if (!IsNumber(_SoLuong))
                                            {
                                                sucessImport = false;
                                                errorLog.AppendLine(string.Format("-- Không thể chuyển kiểu dữ liệu chuỗi sang kiểu dữ liệu số "));
                                            }
                                        }

                                        if (sucessImport == true)
                                        {
                                            //if (!string.IsNullOrEmpty(_oidChiTiet))
                                            //{
                                            //    ct = uow.FindObject<ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu>(CriteriaOperator.Parse("Oid = ?", _oidChiTiet));
                                            //    if (ct == null)
                                            //    {
                                            //        sucessImport = false;
                                            //        errorLog.AppendLine(string.Format("--Không tìm thấy key"));
                                            //    }
                                            //}                                            

                                            _lhd = uow.FindObject<LoaiHoatDong>(CriteriaOperator.Parse("MaQuanLy = ? OR TenLoaiHoatDong = ?", _LoaiHuongDan, _LoaiHuongDan));
                                            if (_lhd == null)
                                            {
                                                sucessImport = false;
                                                errorLog.AppendLine(string.Format("--Không tìm thấy loại hoạt động"));
                                            }

                                            _bomonquanly = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ? OR TenBoPhan = ?", _BoMonQuanLy, _BoMonQuanLy));
                                            if (_lhd == null)
                                            {
                                                sucessImport = false;
                                                errorLog.AppendLine(string.Format("--Không tìm thấy bộ môn"));
                                            }

                                            _nv = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaThinhGiang = ?", _sohieu));
                                            if (_nv == null)
                                            {
                                                _nv = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc = ?", _sohieu));
                                            }
                                            if (_nv == null)
                                            {
                                                sucessImport = false;
                                                errorLog.AppendLine(string.Format("--Không tìm thấy số hiệu"));
                                            }                                                  
                                        }                                    

                                        if (sucessImport == true)
                                        {
                                            sql += " Union All select '" + _oidChiTiet + "' as oidChiTiet"
                                                + ", '" + _nv.Oid.ToString() + "' as NhanVien"
                                                   + ", " + _SoLuong.ToString().Replace(",", ".") + " as SoLuong"
                                                   + ", '" + _lhd.Oid.ToString() + "' as LoaiHuongDan"
                                                   + ", '" + _bomonquanly.Oid.ToString() + "' as BoMonQuanLy"
                                                   + ", N'" + _TenMonHoc + "' as TenMonHoc"
                                                   + ", N'" + _LopMonHoc + "' as LopMonHoc";

                                            sucessNumber++;
                                        }
                                        else
                                        {
                                            erorrNumber++;
                                            mainLog.AppendLine(string.Format("- STT : {0} không import vào phần mềm được", _stt));
                                            mainLog.AppendLine(errorLog.ToString());
                                            uow.RollbackTransaction();
                                            sucessImport = true;
                                        }
                                        sucessImport = true;                                 
                                    }

                                    SqlParameter[] pImport = new SqlParameter[2];
                                    pImport[0] = new SqlParameter("@String", sql.Substring(11).ToString());
                                    pImport[1] = new SqlParameter("@KeKhai", OidQuanLy.Oid);
                                    DataProvider.ExecuteNonQuery("spd_PMS_Import_HuongDanChuyenDeTuXa", CommandType.StoredProcedure, pImport);
                                }                                                                                                                         
                            }
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    //DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số dòng không thành công " + erorrNumber + " " + s + "!");

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

         public static bool IsNumber(string Number)
         {
             try
             {
                 decimal.Parse(Number);
                 return true;
             }
             catch
             {
                 return false;
             }
         }
    }
}