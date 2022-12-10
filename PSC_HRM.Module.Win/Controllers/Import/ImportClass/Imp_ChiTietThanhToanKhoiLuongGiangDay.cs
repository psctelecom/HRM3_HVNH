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
    public class Imp_ChiTietThanhToanKhoiLuongGiangDay
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

                        if (TruongConfig.MaTruong == "VHU")
                        {
                            #region Trường khác
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet$A1:S]"))
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các Idx

                                const int idMaNhanVien = 0;
                                const int idHoTen = 1;
                                const int idTenMonHoc = 7;
                                const int idLoai = 8;
                                const int idNhom = 9;
                                const int idMaLop = 10;
                                const int idSiSo = 11;
                                const int idSoTiet = 12;
                                const int idHSLopDong = 13;
                                const int idHSBacDaoTao = 14;
                                const int idHSNgonNgu = 15;
                                const int idHSNgoaiGio = 16;
                                const int idHSThucHanh = 17;
                                const int idSoTietQuyDoi = 18;

                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    string sql = "";
                                    #region Khai báo
                                    QuanLyHoatDongKhac qly = uow.GetObjectByKey<QuanLyHoatDongKhac>(OidQuanLy.Oid);

                                    ChiTietThanhToanKhoiLuongGiangDay ctThanhToan;
                                    NhanVien nhanVien = null;

                                    int STT = 0;
                                    #endregion
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
                                            string txtMaNhanVien = dr[idMaNhanVien].ToString();
                                            #endregion

                                            #region Họ tên
                                            string txtHoTen = dr[idHoTen].ToString();
                                            #endregion

                                            #region Tên môn học
                                            string txtTenMonHoc = dr[idTenMonHoc].ToString();
                                            #endregion

                                            #region Loại
                                            string txtLoai = dr[idLoai].ToString();
                                            #endregion

                                            #region Nhóm
                                            string txtNhom = dr[idNhom].ToString();
                                            #endregion

                                            #region Mã lớp
                                            string txtMaLop = dr[idMaLop].ToString();
                                            #endregion

                                            #region Sĩ số
                                            string txtSiSo = dr[idSiSo].ToString();
                                            #endregion

                                            #region Số tiết
                                            string txtSoTiet = dr[idSoTiet].ToString();
                                            #endregion

                                            #region HS lớp đông
                                            string txtHeSoLopDong = dr[idHSLopDong].ToString();
                                            #endregion

                                            #region HS bậc đào tạo
                                            string txtHSBacDaoTao = dr[idHSBacDaoTao].ToString();
                                            #endregion

                                            #region HS ngôn ngữ
                                            string txtHeSoNgonNgu = dr[idHSNgonNgu].ToString();
                                            #endregion

                                            #region HS ngoài giờ
                                            string txtHSNgoaiGio = dr[idHSNgoaiGio].ToString();
                                            #endregion

                                            #region HS thực hành
                                            string txtHSThucHanh = dr[idHSThucHanh].ToString();
                                            #endregion

                                            #region Tiết quy đổi
                                            string txtTietQuyDoi = dr[idSoTietQuyDoi].ToString();
                                            #endregion
                                            //
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                            if (txtMaNhanVien != string.Empty && txtTenMonHoc != string.Empty)
                                            {
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                                if (nhanVien != null)
                                                {
                                                    decimal ss = Convert.ToDecimal(txtSiSo.ToString());
                                                    int SiSo = Convert.ToInt32(ss);
                                                    sql += " Union All select N'" + txtMaNhanVien + "' as MaQuanLy"
                                                           + ", N'" + txtHoTen + "' as HoTen"
                                                           + ", N'" + txtLoai + "' as Loai"
                                                           + ", N'" + txtTenMonHoc.Replace(",", ".") + "' as TenMonHoc"
                                                           + ", N'" + txtNhom + "' as Nhom"
                                                           + ", N'" + txtMaLop + "' as MaLop"
                                                           + ", N'" + Convert.ToInt32(Convert.ToDecimal(txtSiSo.ToString())) + "' as SiSo"
                                                           + ", N'" + txtSoTiet.Replace(",", ".") + "' as SoTiet"
                                                           + ", N'" + txtHeSoLopDong.Replace(",", ".") + "' as HSLopDong"
                                                           + ", N'" + txtHSBacDaoTao.Replace(",", ".") + "' as HSBacDaoTao"
                                                           + ", N'" + txtHeSoNgonNgu.Replace(",", ".") + "' as HSNgonNgu"
                                                           + ", N'" + txtHSNgoaiGio.Replace(",", ".") + "' as HSNgoaiGio"
                                                           + ", N'" + txtHSThucHanh.Replace(",", ".") + "' as HSThucHanh"
                                                           + ", N'" + txtTietQuyDoi.Replace(",", ".") + "' as HSLopDong";
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + " " + txtHoTen + " Không tìm thấy thông tin nhân viên");
                                                }
                                            }
                                            else
                                            {
                                                if (txtMaNhanVien == string.Empty)
                                                    errorLog.AppendLine("- STT: " + STT + " " + txtHoTen + " Không có thông tin Mã nhân viên");
                                                else
                                                    errorLog.AppendLine("- STT: " + STT + " " + txtHoTen + " Không có thông tin tên môn học");
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
                                    if (erorrNumber > 0)
                                    {
                                        uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    }
                                    else
                                    {
                                        SqlParameter[] pImport = new SqlParameter[2];
                                        pImport[0] = new SqlParameter("@QuanLyHoatDongKhac", qly.Oid);
                                        pImport[1] = new SqlParameter("@txtString", sql.Substring(11));
                                        DataProvider.GetValueFromDatabase("spd_PMS_Import_KhoiLuongKhac", CommandType.StoredProcedure, pImport);
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (TruongConfig.MaTruong == "UEL")
                        {
                            #region Trường khác
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$D1:AA]"))
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các Idx
                                const int idMaNhanVien = 0;
                                const int idHoTen = 1;
                                const int idKhoanChi = 2;
                                const int idTenMonHoc = 3;
                                const int idLopHocPhan = 4;
                                const int idHeDaoTao = 5;
                                const int idNgonNguGiangDay = 6;
                                const int idSoTiet = 7;
                                const int idSoLuongSV = 8;

                                const int idHeSoChucDanh = 9;
                                const int idHeSoCoSo = 10;
                                const int idHeSoLopDong = 11;
                                const int idHeSoHeDaoTao = 12;
                                const int idTongHeSo = 13;
                                const int idChiPhiDiLai = 14;
                                const int idDonGiaTietChuan = 15;
                                const int idDonGiaQuyTheoNgonNgu = 16;
                                const int idDonGiaThuLao = 17;
                                const int idTongCong = 18;
                                const int idNoHKTruoc = 19;
                                const int idNoHKNay = 20;
                                const int idTongTienNo = 21;
                                const int idThueTNCNTamTru = 22;
                                const int idConLaiThanhToan = 23;

                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    string sql = "";
                                  
                                    QuanLyHoatDongKhac qly = uow.GetObjectByKey<QuanLyHoatDongKhac>(OidQuanLy.Oid);
                                    NhanVien nhanVien = null;                                  
                                    string maTruong = HamDungChung.ThongTinTruong(uow).MaQuanLy;

                                    int STT = 0;
                                    var errorLog = new StringBuilder();                                  
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (qly != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            decimal SoTiet = 0;
                                            int SoLuongSV = 0;

                                            decimal HeSoChucDanh = 0;
                                            decimal HeSoCoSo = 0;
                                            decimal HeSoLopDong = 0;
                                            decimal HeSoHeDaoTao = 0;
                                            decimal TongHeSo = 0;
                                            decimal ChiPhiDiLai = 0;
                                            decimal DonGia = 0;
                                            decimal DonGiaTheoNgonNgu = 0;
                                            decimal DonGiaThuLao = 0;                                            
                                            decimal TongCong = 0;
                                            decimal NoGioHKTruoc = 0;
                                            decimal NoGioHKNay = 0;
                                            decimal TongTienNo = 0;
                                            decimal ThueTNCNTamTru = 0;
                                            decimal ConLaiThanhToan = 0;

                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////

                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            //
                                            string txtMaNhanVien = dr[idMaNhanVien].ToString();
                                            string txtHoTen = dr[idHoTen].ToString();
                                            string txtKhoanChi = dr[idKhoanChi].ToString();
                                            string txtTenMonHoc = dr[idTenMonHoc].ToString();
                                            string txtLopHocPhan = dr[idLopHocPhan].ToString();
                                            string txtHeDaoTao = dr[idHeDaoTao].ToString();
                                            string txtNgonNgu = dr[idNgonNguGiangDay].ToString();
                                            if (!string.IsNullOrEmpty(dr[idSoTiet].ToString()))
                                            {
                                                SoTiet = Convert.ToDecimal(dr[idSoTiet].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region SoLuongSV
                                            if (!string.IsNullOrEmpty(dr[idSoLuongSV].ToString()))
                                            {
                                                SoLuongSV = Convert.ToInt32(dr[idSoLuongSV].ToString());
                                            }
                                            #endregion

                                            #region HS chức danh
                                            if (!string.IsNullOrEmpty(dr[idHeSoChucDanh].ToString()))
                                            {
                                                HeSoChucDanh = Convert.ToDecimal(dr[idHeSoChucDanh].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region HS cơ sở
                                            if (!string.IsNullOrEmpty(dr[idHeSoCoSo].ToString()))
                                            {
                                                HeSoCoSo = Convert.ToDecimal(dr[idHeSoCoSo].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region HS lớp đông
                                            if (!string.IsNullOrEmpty(dr[idHeSoLopDong].ToString()))
                                            {
                                                HeSoLopDong = Convert.ToDecimal(dr[idHeSoLopDong].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region HS hệ đào tạo
                                            if (!string.IsNullOrEmpty(dr[idHeSoHeDaoTao].ToString()))
                                            {
                                                HeSoHeDaoTao = Convert.ToDecimal(dr[idHeSoHeDaoTao].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region HS TongHeSo
                                            if (!string.IsNullOrEmpty(dr[idTongHeSo].ToString()))
                                            {
                                                TongHeSo = Convert.ToDecimal(dr[idTongHeSo].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Chi phí đi lại
                                            if (!string.IsNullOrEmpty(dr[idChiPhiDiLai].ToString()))
                                            {
                                                ChiPhiDiLai = Convert.ToDecimal(dr[idChiPhiDiLai].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Đơn giá tiết chuẩn
                                            if (!string.IsNullOrEmpty(dr[idDonGiaTietChuan].ToString()))
                                            {
                                                DonGia = Convert.ToDecimal(dr[idDonGiaTietChuan].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Đơn giá ngôn ngữ
                                            if (!string.IsNullOrEmpty(dr[idDonGiaQuyTheoNgonNgu].ToString()))
                                            {
                                                DonGiaTheoNgonNgu = Convert.ToDecimal(dr[idDonGiaQuyTheoNgonNgu].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Đơn giá thù lao
                                            if (!string.IsNullOrEmpty(dr[idDonGiaThuLao].ToString()))
                                            {
                                                DonGiaThuLao = Convert.ToDecimal(dr[idDonGiaThuLao].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Tổng cộng
                                            if (!string.IsNullOrEmpty(dr[idTongCong].ToString()))
                                            {
                                                TongCong = Convert.ToDecimal(dr[idTongCong].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Nợ giờ HK trước
                                            if (!string.IsNullOrEmpty(dr[idNoHKTruoc].ToString()))
                                            {
                                                NoGioHKTruoc = Convert.ToDecimal(dr[idNoHKTruoc].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Nợ giờ HK này
                                            if (!string.IsNullOrEmpty(dr[idNoHKNay].ToString()))
                                            {
                                                NoGioHKNay = Convert.ToDecimal(dr[idNoHKNay].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Tổng tiền nợ
                                            if (!string.IsNullOrEmpty(dr[idTongTienNo].ToString()))
                                            {
                                                TongTienNo = Convert.ToDecimal(dr[idTongTienNo].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Thuế TNCN
                                            if (!string.IsNullOrEmpty(dr[idThueTNCNTamTru].ToString()))
                                            {
                                                ThueTNCNTamTru = Convert.ToDecimal(dr[idThueTNCNTamTru].ToString().Replace(".", ","));
                                            }
                                            #endregion

                                            #region Còn lại
                                            if (!string.IsNullOrEmpty(dr[idConLaiThanhToan].ToString()))
                                            {
                                                ConLaiThanhToan = Convert.ToDecimal(dr[idConLaiThanhToan].ToString().Replace(".", ","));
                                            }

                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                            if (txtTenMonHoc == string.Empty)
                                            {
                                                txtTenMonHoc = txtKhoanChi;
                                            }
                                            if (txtMaNhanVien != string.Empty && txtHoTen != string.Empty && ((txtTenMonHoc != string.Empty || maTruong == "UEL")))
                                            {
                                                sucessImport = true;
                                                CriteriaOperator fNhanVien = null;
                                                fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);

                                                if (nhanVien == null)
                                                {
                                                    sucessImport = false;
                                                    errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên " + txtHoTen != "" ? txtHoTen : "");
                                                }
                                                else
                                                {
                                                    HeDaoTao _HeDaoTao = uow.FindObject<HeDaoTao>(CriteriaOperator.Parse("MaQuanLy =? or TenHeDaoTao=?", txtHeDaoTao, txtHeDaoTao));

                                                    if (_HeDaoTao == null && txtHeDaoTao!="")
                                                    {
                                                        sucessImport = false;
                                                        errorLog.AppendLine("- STT: " + STT + " Không tồn tại hệ đào tạo trong hệ thống: " + txtHeDaoTao);
                                                    }

                                                    NgonNguGiangDay _NgonNguGiangDay = uow.FindObject<NgonNguGiangDay>(CriteriaOperator.Parse("MaQuanLy =? or TenNgonNgu=?", txtNgonNgu, txtNgonNgu));

                                                    if (_NgonNguGiangDay == null && txtNgonNgu!="")
                                                    {
                                                        sucessImport = false;
                                                        errorLog.AppendLine("- STT: " + STT + " Không tồn tại ngôn ngữ giảng dạy trong hệ thống: " + txtNgonNgu);
                                                    }
                                                    Guid NgonNgu = _NgonNguGiangDay != null ? _NgonNguGiangDay.Oid : Guid.Empty;
                                                    Guid HeDaoTao = _HeDaoTao != null ? _HeDaoTao.Oid : Guid.Empty;

                                                    if (sucessImport = true)
                                                    {
                                                        sucessNumber++;
                                                        sql += " union all select N'" + txtMaNhanVien + "' as MaNhanVien,N'";
                                                        sql += txtHoTen + "' as HoTen,N'";
                                                        sql += nhanVien.Oid + "N' as NhanVien,N'";
                                                        sql += txtKhoanChi + "' as KhoanChi,N'";
                                                        sql += txtTenMonHoc + "' as TenMonHoc,N'";
                                                        sql += txtLopHocPhan + "' as LopHocPhan,N'";
                                                        sql += HeDaoTao.ToString() + "N' as HeDaoTao,'";
                                                        sql += NgonNgu.ToString() + "N' as NgonNgu,'";
                                                        sql += SoTiet.ToString() + "' as SoTiet,N'";
                                                        sql += SoLuongSV.ToString() + "' as SoLuongSV,N'";
                                                        sql += HeSoChucDanh.ToString().Replace(",", ".") + "' as HeSoChucDanh,N'";
                                                        sql += HeSoCoSo.ToString().Replace(",", ".") + "' as HeSoCoSo,N'";
                                                        sql += HeSoLopDong.ToString().Replace(",", ".") + "' as HeSoLopDong,N'";
                                                        sql += HeSoHeDaoTao.ToString().Replace(",", ".") + "' as HeSoHeDaoTao,N'";
                                                        sql += TongHeSo.ToString().Replace(",", ".") + "' as TongHeSo,N'";
                                                        sql += ChiPhiDiLai.ToString().Replace(",", ".") + "' as ChiPhiDiLai,N'";
                                                        sql += DonGia.ToString().Replace(",", ".") + "' as DonGia,N'";
                                                        sql += DonGiaTheoNgonNgu.ToString().Replace(",", ".") + "' as DonGiaTheoNgonNgu,N'";
                                                        sql += DonGiaThuLao.ToString().Replace(",", ".") + "' as DonGiaThuLao,N'";
                                                        sql += TongCong.ToString().Replace(",", ".") + "' as TongCong,'";
                                                        sql += NoGioHKTruoc.ToString().Replace(",", ".") + "' as NoGioHKTruoc,N'";
                                                        sql += NoGioHKNay.ToString().Replace(",", ".") + "' as NoGioHKNay,N'";
                                                        sql += TongTienNo.ToString().Replace(",", ".") + "' as TongTienNo,N'";
                                                        sql += ThueTNCNTamTru.ToString().Replace(",", ".") + "' as ThueTNCNTamTru,N'";
                                                        sql += ConLaiThanhToan.ToString().Replace(",", ".") + "' as ConLaiThanhToan";
                                                    }
                                                }
                                            }
                                        }
                                        ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////    
                                        //Đưa thông tin bị lỗi vào blog                                      
                                        #region Mở file log lỗi lên
                                        if (erorrNumber == 0)
                                        {
                                            SqlParameter[] pImport = new SqlParameter[2];
                                            pImport[0] = new SqlParameter("@QuanLyHoatDongKhac", qly.Oid);
                                            pImport[1] = new SqlParameter("@txtString", sql.Substring(11));
                                            DataProvider.GetValueFromDatabase("spd_PMS_Import_ChiTietThanhToanKhoiLuongGiangDay", CommandType.StoredProcedure, pImport);
                                        }                                       
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            #region Trường khác
                            using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$D1:AB]"))
                            {
                                /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                                /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                                #region Khởi tạo các Idx
                                const int idMaNhanVien = 0;
                                const int idHoTen = 1;
                                const int idKhoanChi = 2;
                                const int idTenMonHoc = 3;
                                const int idLopHocPhan = 4;
                                const int idCuNhanTn = 5;
                                const int idSoTiet = 6;
                                const int idSoLuongSV = 7;

                                const int idHeSoChucDanh = 8;
                                const int idHeSoCoSo = 9;
                                const int idHeSoLopDong = 10;
                                const int idHeSoKhac = 11;
                                const int idTongHeSo = 12;

                                const int idSoTietLyThuyet = 13;
                                const int idSoTietQuyDoi = 14;
                                const int idChiPhiDiLai = 15;
                                const int idDonGia = 16;
                                const int idThanhTien = 17;
                                const int idTongCong = 18;
                                const int idNoHKTruoc = 19;
                                const int idNoHKNay = 20;
                                const int idTongTienNo = 21;
                                const int idThueTNCNTamTru = 22;
                                const int idConLaiThanhToan = 23;
                                const int idCMND = 24;

                                #endregion

                                /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                                {
                                    //
                                    uow.BeginTransaction();
                                    string sql = "";
                                    #region Khai báo
                                    QuanLyHoatDongKhac qly = uow.GetObjectByKey<QuanLyHoatDongKhac>(OidQuanLy.Oid);

                                    ChiTietThanhToanKhoiLuongGiangDay ctThanhToan;
                                    NhanVien nhanVien = null;
                                    ThongTinNhanVien ttNhanVien = null;
                                    BacDaoTao bacDaoTao = null;

                                    string maTruong = HamDungChung.ThongTinTruong(uow).MaQuanLy;

                                    int STT = 0;
                                    #endregion
                                    //Duyệt qua tất cả các dòng trong file excel
                                    if (qly != null)
                                    {
                                        foreach (DataRow dr in dt.Rows)
                                        {
                                            decimal SoTiet = 0;
                                            int SoLuongSV = 0;

                                            decimal HeSoChucDanh = 0;
                                            decimal HeSoCoSo = 0;
                                            decimal HeSoLopDong = 0;
                                            decimal HeSoKhac = 0;
                                            decimal TongHeSo = 0;
                                            bool CuNhanTN = false;
                                            decimal SoTietLyThuyet = 0;
                                            decimal SoTietQuyDoi = 0;
                                            decimal ChiPhiDiLai = 0;
                                            decimal DonGia = 0;
                                            decimal ThanhTien = 0;
                                            decimal TongCong = 0;
                                            decimal NoGioHKTruoc = 0;
                                            decimal NoGioHKNay = 0;
                                            decimal TongTienNo = 0;
                                            decimal ThueTNCNTamTru = 0;
                                            decimal ConLaiThanhToan = 0;

                                            STT++;
                                            //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                            var errorLog = new StringBuilder();
                                            //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                            #region Đọc dữ liệu
                                            //
                                            #region Họ tên
                                            string txtHoTen = dr[idHoTen].ToString();
                                            #endregion

                                            #region Mã nhân viên
                                            string txtMaNhanVien = dr[idMaNhanVien].ToString();
                                            #endregion

                                            #region Khoản chi
                                            string txtKhoanChi = dr[idKhoanChi].ToString();
                                            #endregion

                                            #region Tên môn học
                                            string txtTenMonHoc = dr[idTenMonHoc].ToString();
                                            #endregion

                                            #region Tên lớp học phần
                                            string txtLopHocPhan = dr[idLopHocPhan].ToString();
                                            #endregion

                                            #region Cử nhân
                                            if (!string.IsNullOrEmpty(dr[idCuNhanTn].ToString()))
                                            {
                                                if (Convert.ToInt32(dr[idCuNhanTn]) == 1)
                                                    CuNhanTN = true;
                                                else
                                                    CuNhanTN = false;
                                            }
                                            #endregion

                                            #region Số tiết
                                            if (!string.IsNullOrEmpty(dr[idSoTiet].ToString()))
                                            {
                                                SoTiet = Convert.ToDecimal(dr[idSoTiet].ToString());
                                            }
                                            #endregion

                                            #region SoLuongSV
                                            if (!string.IsNullOrEmpty(dr[idSoLuongSV].ToString()))
                                            {
                                                SoLuongSV = Convert.ToInt32(dr[idSoLuongSV].ToString());
                                            }
                                            #endregion

                                            #region HS chức danh
                                            if (!string.IsNullOrEmpty(dr[idHeSoChucDanh].ToString()))
                                            {
                                                HeSoChucDanh = Convert.ToDecimal(dr[idHeSoChucDanh].ToString());
                                            }
                                            #endregion

                                            #region HS cơ sở
                                            if (!string.IsNullOrEmpty(dr[idHeSoCoSo].ToString()))
                                            {
                                                HeSoCoSo = Convert.ToDecimal(dr[idHeSoCoSo].ToString());
                                            }
                                            #endregion

                                            #region HS lớp đông
                                            if (!string.IsNullOrEmpty(dr[idHeSoLopDong].ToString()))
                                            {
                                                HeSoLopDong = Convert.ToDecimal(dr[idHeSoLopDong].ToString());
                                            }
                                            #endregion

                                            #region HS HeSoKhac
                                            if (!string.IsNullOrEmpty(dr[idHeSoKhac].ToString()))
                                            {
                                                HeSoKhac = Convert.ToDecimal(dr[idHeSoKhac].ToString());
                                            }
                                            #endregion

                                            #region HS TongHeSo
                                            if (!string.IsNullOrEmpty(dr[idTongHeSo].ToString()))
                                            {
                                                TongHeSo = Convert.ToDecimal(dr[idTongHeSo].ToString());
                                            }
                                            #endregion

                                            #region So tiết lý thuyết
                                            if (!string.IsNullOrEmpty(dr[idSoTietLyThuyet].ToString()))
                                            {
                                                SoTietLyThuyet = Convert.ToDecimal(dr[idSoTietLyThuyet].ToString());
                                            }
                                            #endregion

                                            #region So tiết quy đổi
                                            if (!string.IsNullOrEmpty(dr[idSoTietQuyDoi].ToString()))
                                            {
                                                SoTietQuyDoi = Convert.ToDecimal(dr[idSoTietQuyDoi].ToString());
                                            }
                                            #endregion

                                            #region Chi phí đi lại
                                            if (!string.IsNullOrEmpty(dr[idChiPhiDiLai].ToString()))
                                            {
                                                ChiPhiDiLai = Convert.ToDecimal(dr[idChiPhiDiLai].ToString());
                                            }
                                            #endregion

                                            #region Đơn giá
                                            if (!string.IsNullOrEmpty(dr[idDonGia].ToString()))
                                            {
                                                DonGia = Convert.ToDecimal(dr[idDonGia].ToString());
                                            }
                                            #endregion

                                            #region Thành tiền
                                            if (!string.IsNullOrEmpty(dr[idThanhTien].ToString()))
                                            {
                                                ThanhTien = Convert.ToDecimal(dr[idThanhTien].ToString());
                                            }
                                            #endregion

                                            #region Tổng cộng
                                            if (!string.IsNullOrEmpty(dr[idTongCong].ToString()))
                                            {
                                                TongCong = Convert.ToDecimal(dr[idTongCong].ToString());
                                            }
                                            #endregion

                                            #region Nợ giờ HK trước
                                            if (!string.IsNullOrEmpty(dr[idNoHKTruoc].ToString()))
                                            {
                                                NoGioHKTruoc = Convert.ToDecimal(dr[idNoHKTruoc].ToString());
                                            }
                                            #endregion

                                            #region Nợ giờ HK này
                                            if (!string.IsNullOrEmpty(dr[idNoHKNay].ToString()))
                                            {
                                                NoGioHKNay = Convert.ToDecimal(dr[idNoHKNay].ToString());
                                            }
                                            #endregion

                                            #region Tổng tiền nợ
                                            if (!string.IsNullOrEmpty(dr[idTongTienNo].ToString()))
                                            {
                                                TongTienNo = Convert.ToDecimal(dr[idTongTienNo].ToString());
                                            }
                                            #endregion

                                            #region Thuế TNCN
                                            if (!string.IsNullOrEmpty(dr[idThueTNCNTamTru].ToString()))
                                            {
                                                ThueTNCNTamTru = Convert.ToDecimal(dr[idThueTNCNTamTru].ToString());
                                            }
                                            #endregion

                                            #region Còn lại
                                            if (!string.IsNullOrEmpty(dr[idConLaiThanhToan].ToString()))
                                            {
                                                ConLaiThanhToan = Convert.ToDecimal(dr[idConLaiThanhToan].ToString());
                                            }
                                            #endregion

                                            #region CMND
                                            string txtCMND = dr[idCMND].ToString();
                                            #endregion


                                            //
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                            #region Kiểm tra dữ liệu
                                            if (txtTenMonHoc == string.Empty)
                                            {
                                                txtTenMonHoc = txtKhoanChi;
                                            }
                                            if (txtHoTen != string.Empty && ((txtTenMonHoc != string.Empty || maTruong == "QNU") || maTruong == "UEL"))
                                            {
                                                sql += " union all select N'" + txtMaNhanVien + "' as MaNhanVien,N'";
                                                sql += txtHoTen + "' as HoTen,N'";
                                                sql += txtKhoanChi + "' as KhoanChi,N'";
                                                sql += txtTenMonHoc + "' as TenMonHoc,N'";
                                                sql += txtLopHocPhan + "' as LopHocPhan,N'";
                                                sql += CuNhanTN == false ? "0" : "1" + "N' as CuNhanTN,'";
                                                sql += SoTiet.ToString() + "' as SoTiet,N'";
                                                sql += SoLuongSV.ToString() + "' as SoLuongSV,N'";
                                                sql += HeSoChucDanh.ToString().Replace(",", ".") + "' as HeSoChucDanh,N'";
                                                sql += HeSoCoSo.ToString().Replace(",", ".") + "' as HeSoCoSo,N'";
                                                sql += HeSoLopDong.ToString().Replace(",", ".") + "' as HeSoLopDong,N'";
                                                sql += HeSoKhac.ToString().Replace(",", ".") + "' as HeSoKhac,N'";
                                                sql += TongHeSo.ToString().Replace(",", ".") + "' as TongHeSo,N'";
                                                sql += SoTietLyThuyet.ToString().Replace(",", ".") + "' as SoTietLyThuyet,N'";
                                                sql += SoTietQuyDoi.ToString().Replace(",", ".") + "' as SoTietQuyDoi,N'";
                                                sql += ChiPhiDiLai.ToString().Replace(",", ".") + "' as ChiPhiDiLai,N'";
                                                sql += DonGia.ToString().Replace(",", ".") + "' as DonGia,N'";
                                                sql += ThanhTien.ToString().Replace(",", ".") + "' as ThanhTien,N'";
                                                sql += TongCong.ToString().Replace(",", ".") + "' as TongCong,'";
                                                sql += NoGioHKTruoc.ToString().Replace(",", ".") + "' as NoGioHKTruoc,N'";
                                                sql += NoGioHKNay.ToString().Replace(",", ".") + "' as NoGioHKNay,N'";
                                                sql += TongTienNo.ToString().Replace(",", ".") + "' as TongTienNo,N'";
                                                sql += ThueTNCNTamTru.ToString().Replace(",", ".") + "' as ThueTNCNTamTru,N'";
                                                sql += ConLaiThanhToan.ToString().Replace(",", ".") + "' as ConLaiThanhToan";
                                                sql += txtCMND.ToString() + "' as CMND";

                                                bool TonTai = false;
                                                CriteriaOperator fNhanVien = null;
                                                if (maTruong == "QNU")
                                                {
                                                    fNhanVien = CriteriaOperator.Parse("HoTen =? or CMND =?", txtHoTen, txtCMND);
                                                }
                                                else
                                                {
                                                    fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                                }
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                                if (nhanVien == null)
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên " + txtHoTen != "" ? txtHoTen : "");
                                                }
                                                else
                                                {
                                                    #region Tạo ChiTietThanhToanKhoiLuongGiangDay

                                                    if (qly != null)
                                                    {
                                                        ctThanhToan = new ChiTietThanhToanKhoiLuongGiangDay(uow);
                                                        ctThanhToan.BacDaoTao = bacDaoTao;
                                                        ctThanhToan.NhanVien = nhanVien;
                                                        ctThanhToan.BoPhan = nhanVien.BoPhan;
                                                        ctThanhToan.TenMonHoc = txtTenMonHoc;
                                                        ctThanhToan.KhoanChi = txtKhoanChi;
                                                        ctThanhToan.LopHocPhan = txtLopHocPhan;
                                                        ctThanhToan.CuNhanTN = CuNhanTN;
                                                        ctThanhToan.SoTiet = SoTiet;
                                                        ctThanhToan.SoLuongSV = SoLuongSV;

                                                        ctThanhToan.HeSo_ChucDanh = HeSoChucDanh;
                                                        ctThanhToan.HeSo_CoSo = HeSoCoSo;
                                                        ctThanhToan.HeSo_Khac = HeSoKhac;
                                                        ctThanhToan.TongHeSo = TongHeSo;

                                                        ctThanhToan.SoTietLyThuyet = SoTietLyThuyet;
                                                        ctThanhToan.SoTietQuyDoi = SoTietQuyDoi;
                                                        ctThanhToan.ChiPhiDiLai = ChiPhiDiLai;
                                                        ctThanhToan.DonGiaTietChuan = DonGia;
                                                        ctThanhToan.ThanhTien = ThanhTien;
                                                        ctThanhToan.TongTien = TongCong;

                                                        ctThanhToan.NoGioHKTruoc = NoGioHKTruoc;
                                                        ctThanhToan.NoGioHKNay = NoGioHKNay;
                                                        ctThanhToan.TongTienNo = TongTienNo;
                                                        ctThanhToan.ThueTNCNTamTru = ThueTNCNTamTru;
                                                        ctThanhToan.ConLaiThanhToan = ConLaiThanhToan;

                                                        qly.ListThanhToanKLGD.Add(ctThanhToan);
                                                    }
                                                    #endregion
                                                    sucessNumber++;
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
                                        uow.CommitChanges();//Lưu
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    //
                    string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + sucessNumber + " Số hoạt động không thành công " + erorrNumber + " " + s + "!");
                    #region Mở file log lỗi lên
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
                    #endregion

                }
            }
        }
    }
    #endregion
}