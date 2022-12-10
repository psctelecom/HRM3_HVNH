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
using PSC_HRM.Module.PMS.NghiepVu.SauDaiHoc;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.Import.ImportClass
{
    public class Imp_DaoTaoSauDaiHoc
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLySauDaiHoc OidQuanLy)
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
                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A10:AN]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idTT = 0;
                            const int idHo = 1;
                            const int idTen = 2;
                            const int idDonVi = 3;
                            const int idTenMonHoc = 4;
                            const int idTenChuyenNganh = 5;
                            const int idKhoa = 6;
                            const int idChuyenNganh1 = 7;
                            //const int idChuyenNganh1_SiSo = 8;
                            const int idChuyenNganh2 = 8;
                            //const int idChuyenNganh2_SiSo = 10;
                            const int idChuyenNganh3 = 9;
                            //const int idChuyenNganh3_SiSo = 12;
                            const int idChuyenNganh4 = 10;
                            //const int idChuyenNganh4_SiSo = 14;
                            const int idChuyenNganh5 = 11;
                            //const int idChuyenNganh5_SiSo = 16;
                            const int idChuyenNganh6 = 12;
                            //const int idChuyenNganh6_SiSo = 18;
                            const int idChuyenNganh7 = 13;
                            //const int idChuyenNganh7_SiSo = 20;
                            const int idChuyenNganh8 = 14;
                            //const int idChuyenNganh8_SiSo = 22;
                            const int idChuyenNganh9 = 15;
                            //const int idChuyenNganh9_SiSo = 24;
                            const int idChuyenNganh10 = 16;
                            //const int idChuyenNganh10_SiSo = 26;
                            const int idTongSoSV = 17;
                            const int idSoTinhChi = 18;
                            const int idLyThuyetGD = 19;
                            const int idHeSoGD = 20;
                            const int idHeSoLopDong = 21;
                            const int idHinhThucThi = 22;
                            const int idTongSoTietGiangDay = 23;
                            const int idRaDe = 24;
                            const int idChamBaiKtraGiuaKy = 25;
                            const int idSoHocVienDuThiThucTe = 26;
                            const int idChamBaiThiTuLuan = 27;
                            const int idChamBaiThiTieuLuan = 28;
                            const int idChamBaiThiVanDap = 29;
                            const int idSoCaCoiThi = 30;
                            const int idQDGioChuanCoiThi = 31;
                            const int idSoLuanVanHD = 32;
                            const int idSoGioHDLuanVan = 33;
                            const int idTongGio = 34;
                            const int idLopGhep_HocLai = 36;
                            const int idGhiChu = 37;
                            const int idCMND = 38;
                            const int idMaHRM = 39;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //

                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLySauDaiHoc qly = uow.GetObjectByKey<QuanLySauDaiHoc>(OidQuanLy.Oid);
                                NhanVien nhanvien;
                                HocPhan_SauDaiHoc tenmonhoc;
                                SiSoChuyenNganh chuyennganh1;
                                SiSoChuyenNganh chuyennganh2;
                                SiSoChuyenNganh chuyennganh3;
                                SiSoChuyenNganh chuyennganh4;
                                SiSoChuyenNganh chuyennganh5;
                                SiSoChuyenNganh chuyennganh6;
                                SiSoChuyenNganh chuyennganh7;
                                SiSoChuyenNganh chuyennganh8;
                                SiSoChuyenNganh chuyennganh9;
                                SiSoChuyenNganh chuyennganh10;
                                HinhThucThi hinhthuc;
                                ChiTietKhoiLuongSauDaiHoc ct;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        #region Khởi tạo
                                        nhanvien = null;
                                        tenmonhoc = null;
                                        chuyennganh1 = null;
                                        chuyennganh2 = null;
                                        chuyennganh3 = null;
                                        chuyennganh4 = null;
                                        chuyennganh5 = null;
                                        chuyennganh6 = null;
                                        chuyennganh7 = null;
                                        chuyennganh8 = null;
                                        chuyennganh9 = null;
                                        chuyennganh10 = null;
                                        hinhthuc = null;
                                        int TongSoHocVien = 0;
                                        decimal SoTinChi = 0;
                                        decimal SoTietLyThuyet = 0;
                                        decimal HeSo_GiangDay = 0;
                                        decimal HeSo_LopDong = 0;
                                        decimal TongGioGiangDay = 0;
                                        decimal GioQuyDoi_RaDe = 0;
                                        decimal GioQuyDoi_ChamBaiGiuaKy = 0;
                                        int SoHocVien_DuThi = 0;
                                        decimal GioQuyDoi_ChamTuLuan = 0;
                                        decimal GioQuyDoi_ChamTieuLuan = 0;
                                        decimal GioQuyDoi_VanDap = 0;
                                        int SoCaCoiThi = 0;
                                        decimal GioQuyDoi_CoiThi = 0;
                                        decimal SoLuanVanHuongDan = 0;
                                        decimal SoGioQuyDoiHuongDanLuanVan = 0;
                                        decimal TongGio = 0;
                                        int txtTT = 0;

                                        string tg = "";
                                        object oid = null;


                                        #endregion
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //
                                        #region Thu Tu
                                        string txtThuTu = dr[idTT].ToString();
                                        #endregion

                                        #region Họ
                                        string txtHo = dr[idHo].ToString();
                                        #endregion

                                        #region Tên
                                        string txtTen = dr[idTen].ToString();
                                        #endregion

                                        #region Đơn vị
                                        string txtDonVi = dr[idDonVi].ToString();
                                        #endregion

                                        #region Khóa
                                        string txtKhoa = dr[idKhoa].ToString();
                                        #endregion
                                        decimal SoTC = 0;
                                        if (dr[idSoTinhChi].ToString() != string.Empty)
                                            SoTC = Convert.ToDecimal(dr[idSoTinhChi].ToString());
                                        #region Môn học
                                        if (!string.IsNullOrEmpty(dr[idTenMonHoc].ToString()))
                                        {
                                            CriteriaOperator fMonHoc = CriteriaOperator.Parse("TenHocPhan =?", dr[idTenMonHoc].ToString());
                                            tenmonhoc = uow.FindObject<HocPhan_SauDaiHoc>(fMonHoc);
                                            if (tenmonhoc == null)
                                            {
                                                tenmonhoc = new HocPhan_SauDaiHoc(uow);
                                                tenmonhoc.TenHocPhan = dr[idTenMonHoc].ToString();
                                                tenmonhoc.SoTinChi = SoTC;
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Môn học không tồn tại.");
                                        }
                                        #endregion

                                        #region Chuyên ngành 1
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh1].ToString()))
                                        {
                                            tg = dr[idChuyenNganh1].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN1 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh1 = uow.FindObject<SiSoChuyenNganh>(fCN1);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 2
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh2].ToString()))
                                        {
                                            tg = dr[idChuyenNganh2].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN2 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh2 = uow.FindObject<SiSoChuyenNganh>(fCN2);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 3
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh3].ToString()))
                                        {
                                            tg = dr[idChuyenNganh3].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN3 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh3 = uow.FindObject<SiSoChuyenNganh>(fCN3);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 4
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh4].ToString()))
                                        {
                                            tg = dr[idChuyenNganh4].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN4 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh4 = uow.FindObject<SiSoChuyenNganh>(fCN4);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 5
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh5].ToString()))
                                        {
                                            tg = dr[idChuyenNganh5].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN5 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh5 = uow.FindObject<SiSoChuyenNganh>(fCN5);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 6
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh6].ToString()))
                                        {
                                            tg = dr[idChuyenNganh6].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN6 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh6 = uow.FindObject<SiSoChuyenNganh>(fCN6);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 7
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh7].ToString()))
                                        {
                                            tg = dr[idChuyenNganh7].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN7 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh7 = uow.FindObject<SiSoChuyenNganh>(fCN7);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 8
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh8].ToString()))
                                        {
                                            tg = dr[idChuyenNganh8].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN8 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh8 = uow.FindObject<SiSoChuyenNganh>(fCN8);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 9
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh9].ToString()))
                                        {
                                            tg = dr[idChuyenNganh9].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN9 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh9 = uow.FindObject<SiSoChuyenNganh>(fCN9);
                                            }
                                        }
                                        #endregion

                                        #region Chuyên ngành 10
                                        if (!string.IsNullOrEmpty(dr[idChuyenNganh10].ToString()))
                                        {
                                            tg = dr[idChuyenNganh10].ToString();
                                            oid = null;
                                            string sql = "SELECT Oid ";
                                            sql += " FROM dbo.SiSoChuyenNganh";
                                            sql += " WHERE [TenGhep] = N'" + tg.ToString() + "'";
                                            sql += " AND GCRecord IS NULL";
                                            oid = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                                            if (oid != null)
                                            {
                                                CriteriaOperator fCN10 = CriteriaOperator.Parse("Oid =?", oid.ToString());
                                                chuyennganh10 = uow.FindObject<SiSoChuyenNganh>(fCN10);
                                            }
                                        }
                                        #endregion

                                        #region Tổng số học viên
                                        if (!string.IsNullOrEmpty(dr[idTongSoSV].ToString()))
                                        {
                                            TongSoHocVien = Convert.ToInt32(dr[idTongSoSV].ToString());
                                        }
                                        #endregion

                                        #region Số tính chỉ
                                        if (!string.IsNullOrEmpty(dr[idSoTinhChi].ToString()))
                                        {
                                            SoTinChi = Convert.ToDecimal(dr[idSoTinhChi].ToString());
                                        }
                                        #endregion

                                        #region Số tiết lý thuyết
                                        if (!string.IsNullOrEmpty(dr[idLyThuyetGD].ToString()))
                                        {
                                            SoTietLyThuyet = Convert.ToDecimal(dr[idLyThuyetGD].ToString());
                                        }
                                        #endregion

                                        #region Hệ số giảng dạy
                                        if (!string.IsNullOrEmpty(dr[idHeSoGD].ToString()))
                                        {
                                            HeSo_GiangDay = Convert.ToDecimal(dr[idHeSoGD].ToString());
                                        }
                                        #endregion

                                        #region Hệ số lớp đông
                                        if (!string.IsNullOrEmpty(dr[idHeSoLopDong].ToString()))
                                        {
                                            HeSo_LopDong = Convert.ToDecimal(dr[idHeSoLopDong].ToString());
                                        }
                                        #endregion

                                        #region Hình thức thi
                                        if (!string.IsNullOrEmpty(dr[idHinhThucThi].ToString()))
                                        {
                                            CriteriaOperator fHinhThuc = CriteriaOperator.Parse("TenHinhThucThi =?", dr[idHinhThucThi].ToString());
                                            hinhthuc = uow.FindObject<HinhThucThi>(fHinhThuc);
                                        }
                                        //else
                                        //{
                                        //    errorLog.AppendLine("- STT: " + STT + " Chưa có hình thức thi.");
                                        //}
                                        #endregion

                                        #region Tổng giờ giảng dạy
                                        if (!string.IsNullOrEmpty(dr[idTongSoTietGiangDay].ToString()))
                                        {
                                            TongGioGiangDay = Convert.ToDecimal(dr[idTongSoTietGiangDay].ToString());
                                        }
                                        #endregion

                                        #region Giờ quy đổi ra đề
                                        if (!string.IsNullOrEmpty(dr[idRaDe].ToString()))
                                        {
                                            GioQuyDoi_RaDe = Convert.ToDecimal(dr[idRaDe].ToString());
                                        }
                                        #endregion

                                        #region Giờ quy đổi chấm bài giửa kỳ
                                        if (!string.IsNullOrEmpty(dr[idChamBaiKtraGiuaKy].ToString()))
                                        {
                                            GioQuyDoi_ChamBaiGiuaKy = Convert.ToDecimal(dr[idChamBaiKtraGiuaKy].ToString());
                                        }
                                        #endregion

                                        #region Số học viên dự thi thực tế
                                        if (!string.IsNullOrEmpty(dr[idSoHocVienDuThiThucTe].ToString()))
                                        {
                                            SoHocVien_DuThi = Convert.ToInt32(dr[idSoHocVienDuThiThucTe].ToString());
                                        }
                                        #endregion

                                        #region Giờ chấm bài tự luận
                                        if (!string.IsNullOrEmpty(dr[idChamBaiThiTuLuan].ToString()))
                                        {
                                            GioQuyDoi_ChamTuLuan = Convert.ToDecimal(dr[idChamBaiThiTuLuan].ToString());
                                        }
                                        #endregion

                                        #region Giờ chấm bài tiểu luận
                                        if (!string.IsNullOrEmpty(dr[idChamBaiThiTieuLuan].ToString()))
                                        {
                                            GioQuyDoi_ChamTieuLuan = Convert.ToDecimal(dr[idChamBaiThiTieuLuan].ToString());
                                        }
                                        #endregion

                                        #region Giờ quy đổi vấn đáp
                                        if (!string.IsNullOrEmpty(dr[idChamBaiThiVanDap].ToString()))
                                        {
                                            GioQuyDoi_VanDap = Convert.ToDecimal(dr[idChamBaiThiVanDap].ToString());
                                        }
                                        #endregion

                                        #region Số ca coi thi
                                        if (!string.IsNullOrEmpty(dr[idSoCaCoiThi].ToString()))
                                        {
                                            SoCaCoiThi = Convert.ToInt32(dr[idSoCaCoiThi].ToString());
                                        }
                                        #endregion

                                        #region Quy đổi giờ coi thi
                                        if (!string.IsNullOrEmpty(dr[idQDGioChuanCoiThi].ToString()))
                                        {
                                            GioQuyDoi_CoiThi = Convert.ToDecimal(dr[idQDGioChuanCoiThi].ToString());
                                        }
                                        #endregion

                                        #region Số luận văn hướng dẫn
                                        if (!string.IsNullOrEmpty(dr[idSoLuanVanHD].ToString()))
                                        {
                                            SoLuanVanHuongDan = Convert.ToDecimal(dr[idSoLuanVanHD].ToString());
                                        }
                                        #endregion

                                        #region Số giờ quy đổi luận văn hướng dẫn
                                        if (!string.IsNullOrEmpty(dr[idSoGioHDLuanVan].ToString()))
                                        {
                                            SoGioQuyDoiHuongDanLuanVan = Convert.ToDecimal(dr[idSoGioHDLuanVan].ToString());
                                        }
                                        #endregion

                                        #region Tổng giờ
                                        if (!string.IsNullOrEmpty(dr[idTongGio].ToString()))
                                        {
                                            TongGio = Convert.ToDecimal(dr[idTongGio].ToString());
                                        }
                                        #endregion

                                        #region Lớp ghép học lại
                                        string txtLopGep_HocLai = dr[idLopGhep_HocLai].ToString();
                                        #endregion

                                        #region Ghi chú
                                        string txtGhiChu = dr[idGhiChu].ToString();
                                        #endregion

                                        #region CMND
                                        string txtCMND = "";
                                        if (!string.IsNullOrEmpty(dr[idCMND].ToString()))
                                        {
                                            txtCMND = dr[idCMND].ToString();
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + "(" + dr[idTT].ToString() + ")" + " Chưa có CMND.");
                                        }
                                        #endregion
                                        //
                                        #region MaHRM
                                        string txtMaHRM = "";
                                        if (!string.IsNullOrEmpty(dr[idMaHRM].ToString()))
                                        {
                                            txtMaHRM = dr[idMaHRM].ToString();
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + "(" + dr[idTT].ToString() + ")" + txtHo + " " + txtTen + " Chưa có Mã HRM.");
                                        }
                                        #endregion
                                        //
                                        #endregion
                                        string note = "";
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if (!string.IsNullOrEmpty(txtCMND) && tenmonhoc != null && !string.IsNullOrEmpty(txtMaHRM))
                                        {

                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("CMND =? or MaQuanLy=?", txtCMND, txtMaHRM);
                                            nhanvien = uow.FindObject<NhanVien>(fNhanVien);
                                            if (nhanvien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                            }
                                            else
                                            {
                                                if (tenmonhoc != null)
                                                {
                                                    //CriteriaOperator filter = CriteriaOperator.Parse("NhanVien = ? and TenMonHoc=? and ChuyenNganh1 = ?", nhanvien.Oid, tenmonhoc.Oid, chuyennganh1.Oid);
                                                    //XPCollection<ChiTietKhoiLuongSauDaiHoc> dsChiTietKhoiLuongSauDaiHoc = new XPCollection<ChiTietKhoiLuongSauDaiHoc>(uow, filter);
                                                    //if (dsChiTietKhoiLuongSauDaiHoc.Count == 0)
                                                    {
                                                        ct = new ChiTietKhoiLuongSauDaiHoc(uow);
                                                        ct.QuanLySauDaiHoc = qly;
                                                        ct.NhanVien = uow.GetObjectByKey<NhanVien>(nhanvien.Oid);
                                                        ct.BoPhan = nhanvien.BoPhan;
                                                        ct.TenMonHoc = tenmonhoc;
                                                        ct.ChuyenNganh1 = chuyennganh1;
                                                        ct.ChuyenNganh2 = chuyennganh2;
                                                        ct.ChuyenNganh3 = chuyennganh3;
                                                        ct.ChuyenNganh4 = chuyennganh4;
                                                        ct.ChuyenNganh5 = chuyennganh5;
                                                        ct.ChuyenNganh6 = chuyennganh6;
                                                        ct.ChuyenNganh7 = chuyennganh7;
                                                        ct.ChuyenNganh8 = chuyennganh8;
                                                        ct.ChuyenNganh9 = chuyennganh9;
                                                        ct.ChuyenNganh10 = chuyennganh10;
                                                        ct.TongSoHocVien = TongSoHocVien;
                                                        ct.SoTinChi = SoTinChi;
                                                        ct.SoTietLyThuyet = SoTietLyThuyet;
                                                        ct.HeSo_GiangDay = HeSo_GiangDay;
                                                        ct.HeSo_LopDong = HeSo_LopDong;
                                                        ct.HinhThucThi = hinhthuc;
                                                        ct.TongGioGiangDay = TongGioGiangDay;
                                                        ct.GioQuyDoi_RaDe = GioQuyDoi_RaDe;
                                                        ct.GioQuyDoi_ChamBaiGiuaKy = GioQuyDoi_ChamBaiGiuaKy;
                                                        ct.SoHocVien_DuThi = SoHocVien_DuThi;
                                                        ct.GioQuyDoi_ChamTuLuan = GioQuyDoi_ChamTuLuan;
                                                        ct.GioQuyDoi_ChamTieuLuan = GioQuyDoi_ChamTieuLuan;
                                                        ct.GioQuyDoi_VanDap = GioQuyDoi_VanDap;
                                                        ct.SoCaCoiThi = SoCaCoiThi;
                                                        ct.GioQuyDoi_CoiThi = GioQuyDoi_CoiThi;
                                                        ct.SoLuanVanHuongDan = SoLuanVanHuongDan;
                                                        ct.SoGioQuyDoiHuongDanLuanVan = SoGioQuyDoiHuongDanLuanVan;
                                                        ct.TongGio = TongGio;
                                                        ct.GhiChu = txtGhiChu;
                                                    }
                                                }
                                                else
                                                {
                                                    note = "";
                                                    if (nhanvien != null)
                                                        note = nhanvien.HoTen;
                                                    else
                                                        note = "Không tìm thấy nhân viên - ";
                                                    if (chuyennganh1 != null)
                                                        note += chuyennganh1.TenNganh;
                                                    else
                                                        note += "Không tìm thấy chuyên ngành";

                                                    errorLog.AppendLine("- STT: " + STT + " " + note);
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
    }
        #endregion
}