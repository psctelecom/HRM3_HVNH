using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
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
    public class Imp_CacHoatDongKhac
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyHoatDongKhac OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:K]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaGiangVien = 0;
                            const int idHoTen = 1;
                            const int idHoatDong = 3;
                            const int idDienGiai = 4;
                            const int idSoThanhVien = 5;
                            const int idVaiTro = 6;
                            const int idDukien = 7;
                            const int idSoGioQuyDoi = 8;
                            const int idXacNhan = 9;
                            const int idNgayNhap = 10;
                            const int idSoTienThanhToan = 11;
                            const int idSoTienThue = 12;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                QuanLyHoatDongKhac qly = uow.GetObjectByKey<QuanLyHoatDongKhac>(OidQuanLy.Oid);

                                NhanVien nhanVien = null;
                                HoatDongKhac hoatDong = null;
                                int STT = 0;
                                string sql = "";
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //


                                        #region Mã nhân viên
                                        string txtMaNhanVien = dr[idMaGiangVien].ToString();
                                        #endregion

                                        #region HỌ tên nhân viên
                                        string txtHoTen = dr[idHoTen].ToString();
                                        #endregion

                                        #region Tên hoạt động
                                        string txtTenHoatDong = dr[idHoatDong].ToString();
                                        #endregion

                                        #region Diễn giải
                                        string txtDienGiai = dr[idDienGiai].ToString();
                                        #endregion

                                        #region Số thành viên
                                        string txtSoThanhVien = dr[idSoThanhVien].ToString();
                                        #endregion

                                        #region Vai trò
                                        string txtVaiTro = dr[idVaiTro].ToString();
                                        #endregion

                                        #region Dự kiến
                                        string txtDuKien = dr[idDukien].ToString();
                                        #endregion

                                        #region Số giờ
                                        string txtSoGioQuyDoi = dr[idSoGioQuyDoi].ToString();
                                        #endregion

                                        #region Xác nhận
                                        string txtXacNhan = dr[idXacNhan].ToString();
                                        #endregion

                                        #region Ngày nhập
                                        DateTime txtNgayNhap = Convert.ToDateTime(dr[idNgayNhap].ToString());
                                        #endregion

                                        sql += " Union All select N'" + txtMaNhanVien + "' as MaQuanLy"
                                            + ", N'" + txtHoTen + "' as HoTen"
                                            + ", N'" + txtTenHoatDong + "' as TenHoatDong"
                                            + ", N'" + txtDienGiai.Replace(",", ".") + "' as DienGiai"
                                            + ", N'" + txtSoThanhVien + "' as SoThanhVien"
                                            + ", N'" + txtVaiTro + "' as VaiTro"
                                            + ", N'" + txtDuKien + "' as DuKien"
                                            + ", N'" + txtSoGioQuyDoi.Replace(",", ".") + "' as SoGio"
                                            + ", N'" + txtXacNhan + "' as XacNhan"
                                            + ",  CONVERT(DATE,'" + txtNgayNhap.ToShortDateString() + "',103) as NgayNhap";
                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        //#region Kiểm tra dữ liệu
                                        if (!string.IsNullOrEmpty(txtMaNhanVien) && !string.IsNullOrEmpty(txtTenHoatDong))
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                            nhanVien = uow.FindObject<NhanVien>(fNhanVien);

                                            if (nhanVien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                            }
                                            if(txtDienGiai==string.Empty)
                                                errorLog.AppendLine("- STT: " + STT + " Không có thông tin tên đề tài.");
                                            //else
                                            //{
                                            //    #region  Tạo hoạt động
                                            //    //if (!string.IsNullOrEmpty(txtTenHoatDong))
                                            //    //{
                                            //    //    CriteriaOperator fHoatDong = CriteriaOperator.Parse("TenHoatDong =?", txtTenHoatDong);
                                            //    //    hoatDong = uow.FindObject<HoatDongKhac>(fHoatDong);
                                            //    //    if (hoatDong == null)
                                            //    //    {
                                            //    //        hoatDong = new HoatDongKhac(uow);
                                            //    //        hoatDong.MaQuanLy = HamDungChung.BoDauTiengViet(txtTenHoatDong);
                                            //    //        hoatDong.TenHoatDong = txtTenHoatDong;
                                            //    //        //uow.CommitChanges();
                                            //    //    }
                                            //    //}
                                            //    #endregion
                                            //    ChiTietHoatDongKhac ct = new ChiTietHoatDongKhac(uow);
                                            //    ct.NhanVien = nhanVien;
                                            //    ct.BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("Oid =?", nhanVien.BoPhan.Oid));
                                            //    ct.HoatDongKhac = hoatDong;
                                            //    ct.DienGiai = txtDienGiai;
                                            //    ct.SoThanhVien = Convert.ToInt32(txtSoThanhVien);
                                            //    if (txtVaiTro == "Chính")
                                            //        ct.VaiTro = VaiTroNCKHEnum.Chinh;
                                            //    else
                                            //        if (txtVaiTro == "Chủ trì đề tài, tác giả chính")
                                            //            ct.VaiTro = VaiTroNCKHEnum.TacGia;
                                            //        else
                                            //            if (txtVaiTro == "Thành viên")
                                            //                ct.VaiTro = VaiTroNCKHEnum.ThanhVien;
                                            //    if (txtDuKien == "0" || txtDuKien == "FALSE")
                                            //        ct.DuKien = false;
                                            //    else
                                            //        ct.DuKien = true;
                                            //    ct.SoGio = Convert.ToDecimal(txtSoGioQuyDoi);
                                            //    if (txtXacNhan == "0" || txtXacNhan == "FALSE")
                                            //        ct.XacNhan = false;
                                            //    else
                                            //        ct.XacNhan = true;

                                            //    ct.NgayNhap = txtNgayNhap;

                                            //    qly.listChiTiet.Add(ct);

                                            //    sucessNumber++;
                                            //}
                                        }
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
                                    //uow.CommitChanges();//Lưu
                                    uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                }
                                else
                                {
                                    //uow.CommitChanges();//Lưu

                                    //spd_PMS_Import_HoatDongKhac
                                    SqlParameter[] pQuyDoi = new SqlParameter[3];
                                    pQuyDoi[0] = new SqlParameter("@QuanLyHoatDongKhac", qly.Oid);
                                    pQuyDoi[1] = new SqlParameter("@LoaiHoatDong", LoaiHoatDongEnum.NghienCuuKhoaHoc.GetHashCode());
                                    pQuyDoi[2] = new SqlParameter("@txtString", sql.Substring(11));
                                    DataProvider.GetValueFromDatabase("spd_PMS_Import_HoatDongKhac", CommandType.StoredProcedure, pQuyDoi);
                                    
                                }
                            }
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số hoạt động không thành công " + erorrNumber + " " + s + "!");

                    //Mở file log lỗi lên
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
                }
            }
        }

        public static void XuLy_UFM(IObjectSpace obs, QuanLyHoatDongKhac OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:H]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaGiangVien = 0;
                            const int idHo = 1;
                            const int idTen = 2;
                            const int idHoTen = 3;
                            const int idMaGiangVien2 = 4;
                            const int idNgaySinh = 5;
                            const int idGioTinh = 6;
                            const int idKhoiLuong = 7;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();

                                QuanLyHoatDongKhac qly = uow.GetObjectByKey<QuanLyHoatDongKhac>(OidQuanLy.Oid);

                                NhanVien nhanVien = null;
                                int STT = 0;
                                string sql = "";
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //


                                        #region Mã nhân viên
                                        string txtMaNhanVien = dr[idMaGiangVien].ToString();
                                        #endregion

                                        #region HỌ tên nhân viên
                                        string txtHoTen = dr[idHoTen].ToString();
                                        #endregion
                                 
                                        #region Khối lượng thực hiện
                                        string txtKhoiLuong = dr[idKhoiLuong].ToString();
                                        #endregion
                                        if (txtMaNhanVien != string.Empty&&(txtKhoiLuong!=string.Empty||txtKhoiLuong!="0"))
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                            nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                            if (nhanVien != null)
                                            {
                                                sql += " Union All select '" + nhanVien.Oid + "' as NhanVienOid"
                                                   + ", N'" + txtHoTen + "' as HoTen"
                                                   + ", N'" + txtKhoiLuong.Replace(",", ".") + "' as KhoiLuong";
                                                sucessNumber++;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên " + txtHoTen + " - Mã: " + txtMaNhanVien);
                                            }
                                        }
                                        //
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
                                    //uow.CommitChanges();//Lưu
                                    uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                }
                                else
                                {
                                    //uow.CommitChanges();//Lưu

                                    //spd_PMS_Import_HoatDongKhac
                                    SqlParameter[] pQuyDoi = new SqlParameter[3];
                                    pQuyDoi[0] = new SqlParameter("@QuanLyHoatDongKhac", qly.Oid);
                                    pQuyDoi[1] = new SqlParameter("@LoaiHoatDong", LoaiHoatDongEnum.NghienCuuKhoaHoc.GetHashCode());
                                    pQuyDoi[2] = new SqlParameter("@txtString", sql.Substring(11));
                                    DataProvider.GetValueFromDatabase("spd_PMS_Import_HoatDongKhac", CommandType.StoredProcedure, pQuyDoi);

                                }
                            }
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số hoạt động không thành công " + erorrNumber + " " + s + "!");

                    //Mở file log lỗi lên
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
                }
            }
        }
        #endregion
    }
}