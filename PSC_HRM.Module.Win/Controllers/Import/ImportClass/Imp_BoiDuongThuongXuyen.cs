using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen;
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
    public class Imp_BoiDuongThuongXuyen
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyBoiDuongThuongXuyen OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A2:AS]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idHKTamUng = 0;
                            const int idMaDonViDay = 1;
                            const int idDiaDiemDay = 2;
                            const int idMaNganh = 3;
                            const int idTenNganh = 4;
                            const int idKhoa = 5;
                            const int idLop = 6;
                            const int idHKTruong = 7;
                            const int idSiSoLop = 8;
                            const int idSoNhom = 9;
                            const int idTT = 10;
                            const int idTenHocPhan = 11;
                            const int idSoDVHT = 12;
                            const int idSoTietLyThuyet = 13;
                            const int idSoTietThucHanh = 14;
                            const int idSoTietThaoLuan = 15;
                            const int idGhiChu = 16;
                            const int idChucDanh = 17;
                            const int idHoTen = 18;
                            const int idCanBoGiangDay = 19;
                            const int idSDT = 20;
                            const int idBoPhanNhanVien = 21;
                            const int idThuTuDong = 22;
                            const int idHo = 23;
                            const int idTen = 24;
                            const int idSoGioTamUng = 25;
                            const int idPhuongThucDaoTao = 26;
                            const int idHinhThucThi = 27;
                            const int idSoBaiThucHanh = 28;
                            const int idHeSoLopDongK1 = 29;
                            const int idHeSoDaoTaoK2 = 30;
                            const int idHeSoGDK5 = 31;
                            const int idTongGioLyThuyetA2 = 32;
                            const int idHeSoThucHanh = 33;
                            const int idQuyDoiGioThucHanh = 34;
                            const int idBaiTapLop = 35;
                            const int idDoAn_ThucTap_ThucHanh = 36;
                            const int idThucTapTotNghiep = 37;
                            const int idRade = 38;
                            const int idChamBaiThi = 39;
                            const int idTongGioKhacA1 = 40;
                            const int idTongGio = 41;
                            const int idCMND = 42;
                            const int idCMaHRM = 43;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyBoiDuongThuongXuyen qly = uow.GetObjectByKey<QuanLyBoiDuongThuongXuyen>(OidQuanLy.Oid);
                                DonViDaoTaoThuongXuyen hesocoso = null;
                                HocKy hocky = null;
                                ChuyenNganhDaoTao NganhHoc = null;
                                HocPhan_BoiDuongThuongXuyen hocphan = null;
                                NhanVien nhanvien = null;
                                ChiTietThuLaoBoiDuongThuongXuyen ct = null;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        #region Khởi tạo
                                        STT++;
                                        int SiSo = 0;
                                        int SoNhom = 0;
                                        decimal SoGioTamUng = 0;
                                        int SoBaiThucHanh = 0;
                                        decimal HeSoLopDongK1 = 0;
                                        decimal HeSoTinhChiK2 = 0;
                                        decimal HeSoGDK5 = 0;
                                        decimal TongGioLyThuyetA2 = 0;
                                        decimal HeSoThucHanh = 0;
                                        decimal QuyDoiTietThucHanh = 0;
                                        decimal QuyDoiBTL = 0;
                                        decimal QuyDoiDA_TT_TH = 0;
                                        decimal ThucTap_ThiNghiem = 0;
                                        decimal RaDe = 0;
                                        decimal ChamBaiThiThucHanh = 0;
                                        decimal TongGioKhacA1 = 0;
                                        decimal TongGio = 0;
                                        decimal SoDVHT_TinhChi = 0;
                                        int SoTietLyThuyet = 0;
                                        int SoTietThucHanh = 0;
                                        int SoTietThaoLuan = 0;
                                        #endregion
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region Học kỳ tạm ứng
                                        string txtHocKyTamUng = dr[idHKTamUng].ToString();
                                        string txtTenHocKy = "";
                                        if (txtHocKyTamUng == "I")
                                            txtTenHocKy = "Học kỳ 1";
                                        else if (txtHocKyTamUng == "II")
                                            txtTenHocKy = "Học kỳ 2";
                                        if (txtTenHocKy != "")
                                        {
                                            CriteriaOperator fHocKy = CriteriaOperator.Parse("TenHocKy =? and NamHoc = ?", txtTenHocKy, qly.NamHoc.Oid);
                                            hocky = uow.FindObject<HocKy>(fHocKy);
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có học kỳ tạm ứng.");
                                        }
                                        #endregion

                                        #region Nơi giảng dạy
                                        if (dr[idDiaDiemDay].ToString() != string.Empty)
                                        {
                                            CriteriaOperator fHeSoCS = CriteriaOperator.Parse("TenDonVi =?", dr[idDiaDiemDay].ToString());
                                            hesocoso = uow.FindObject<DonViDaoTaoThuongXuyen>(fHeSoCS);
                                            CriteriaOperator fCoSo;
                                            HeSoCoSo coso;
                                            if(hesocoso == null)
                                            {
                                                hesocoso = new DonViDaoTaoThuongXuyen(uow);
                                                hesocoso.TenDonVi = dr[idDiaDiemDay].ToString();
                                                if(dr[idDiaDiemDay].ToString() != "Trường ĐH Quy Nhơn")
                                                {
                                                    fCoSo = CriteriaOperator.Parse("TenCoSo =?", "Ngoài trường");                                               
                                                }
                                                else
                                                {
                                                    fCoSo = CriteriaOperator.Parse("TenCoSo =?", "Trong trường");
                                                }
                                                coso = uow.FindObject<HeSoCoSo>(fCoSo);
                                                hesocoso.HeSoCoSo = coso;

                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có địa điểm giảng dạy.");
                                        }
                                        #endregion

                                        #region Ngành học
                                        if (dr[idTenNganh].ToString() != string.Empty)
                                        {
                                            CriteriaOperator fNganhHoc = CriteriaOperator.Parse("TenChuyenNganh =?", dr[idTenNganh].ToString());
                                            NganhHoc = uow.FindObject<ChuyenNganhDaoTao>(fNganhHoc);
                                            if(NganhHoc == null)
                                            {
                                                NganhHoc = new ChuyenNganhDaoTao(uow);
                                                NganhHoc.TenChuyenNganh = dr[idTenNganh].ToString();
                                                NganhHoc.MaQuanLy = dr[idTenNganh].ToString();
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có ngành học.");
                                        }
                                        #endregion

                                        #region Khóa
                                        string txtKhoa = dr[idKhoa].ToString();
                                        #endregion

                                        #region Lớp
                                        string txtLop = dr[idLop].ToString();
                                        #endregion

                                        #region hoc kỳ nơi dạy
                                        string txtHKNoiDay = dr[idHKTruong].ToString();
                                        #endregion

                                        #region Sỉ số
                                        if (dr[idSiSoLop].ToString() != string.Empty)
                                        {
                                            SiSo = Convert.ToInt32(dr[idSiSoLop].ToString());
                                        }
                                        #endregion

                                        #region Số nhóm
                                        if (dr[idSoNhom].ToString() != string.Empty)
                                        {
                                            var sn = dr[idSoNhom].ToString();
                                            if (sn != " ")
                                                SoNhom = Convert.ToInt32(dr[idSoNhom].ToString());
                                        }
                                        #endregion

                                        #region HocPhan
                                        if (dr[idTenHocPhan].ToString() != string.Empty)
                                        {
                                            CriteriaOperator fHocPhan = CriteriaOperator.Parse("TenHocPhan =?", dr[idTenHocPhan].ToString());
                                            hocphan = uow.FindObject<HocPhan_BoiDuongThuongXuyen>(fHocPhan);
                                            if (hocphan == null)
                                            {
                                                hocphan = new HocPhan_BoiDuongThuongXuyen(uow);
                                                hocphan.TenHocPhan = dr[idTenHocPhan].ToString();
                                                hocphan.MaQuanLy = dr[idTenHocPhan].ToString();
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có học phần.");
                                        }


                                        #endregion

                                        #region Ghi chú
                                        string txtGhiChu = dr[idGhiChu].ToString();
                                        #endregion

                                        #region Số giờ tạm ứng
                                        if (dr[idSoGioTamUng].ToString() != string.Empty)
                                        {
                                            SoGioTamUng = Convert.ToDecimal(dr[idSoGioTamUng].ToString());
                                        }
                                        #endregion

                                        #region Phương thức đào tạo
                                        string txtPhuongThucDaoTao = dr[idPhuongThucDaoTao].ToString();
                                        #endregion

                                        #region Hình thức thi
                                        string txtHinhThucThi = dr[idHinhThucThi].ToString();
                                        #endregion

                                        #region Số DVHT - Tính chỉ
                                        if (dr[idSoDVHT].ToString() != string.Empty)
                                        {
                                            SoDVHT_TinhChi = Convert.ToDecimal(dr[idSoDVHT].ToString());
                                        }
                                        #endregion

                                        #region Số tiết lý thuyết
                                        if (dr[idSoTietLyThuyet].ToString() != string.Empty)
                                        {
                                            SoTietLyThuyet = Convert.ToInt32(dr[idSoTietLyThuyet].ToString());
                                        }
                                        #endregion

                                        #region Số tiết thực hành
                                        if (dr[idSoTietThucHanh].ToString() != string.Empty)
                                        {
                                            SoTietThucHanh = Convert.ToInt32(dr[idSoTietThucHanh].ToString());
                                        }
                                        #endregion

                                        #region Số tiết thảo luận
                                        if (dr[idSoTietThaoLuan].ToString() != string.Empty)
                                        {
                                            SoTietThaoLuan = Convert.ToInt32(dr[idSoTietThaoLuan].ToString());
                                        }
                                        #endregion

                                        #region Số bài thực hành
                                        if (dr[idSoBaiThucHanh].ToString() != string.Empty)
                                        {
                                            SoBaiThucHanh = Convert.ToInt32(dr[idSoBaiThucHanh].ToString());
                                        }
                                        #endregion

                                        #region Hệ số lớp đông K1
                                        if (dr[idHeSoLopDongK1].ToString() != string.Empty)
                                        {
                                            HeSoLopDongK1 = Convert.ToDecimal(dr[idHeSoLopDongK1].ToString());
                                        }
                                        #endregion

                                        #region Hệ số tính chỉ K2
                                        if (dr[idHeSoDaoTaoK2].ToString() != string.Empty)
                                        {
                                            HeSoTinhChiK2 = Convert.ToDecimal(dr[idHeSoDaoTaoK2].ToString());
                                        }
                                        #endregion

                                        #region Hệ số giáo dục K5
                                        if (dr[idHeSoGDK5].ToString() != string.Empty)
                                        {
                                            HeSoGDK5 = Convert.ToDecimal(dr[idHeSoGDK5].ToString());
                                        }
                                        #endregion

                                        #region Tổng giờ lý thuyết
                                        if (dr[idTongGioLyThuyetA2].ToString() != string.Empty)
                                        {
                                            TongGioLyThuyetA2 = Convert.ToDecimal(dr[idTongGioLyThuyetA2].ToString());
                                        }
                                        #endregion

                                        #region Hệ số Thực Hành
                                        if (dr[idHeSoThucHanh].ToString() != string.Empty)
                                        {
                                            HeSoThucHanh = Convert.ToDecimal(dr[idHeSoThucHanh].ToString());
                                        }
                                        #endregion

                                        #region Quy đổi hệ số Thực Hành
                                        if (dr[idQuyDoiGioThucHanh].ToString() != string.Empty)
                                        {
                                            QuyDoiTietThucHanh = Convert.ToDecimal(dr[idQuyDoiGioThucHanh].ToString());
                                        }
                                        #endregion

                                        #region Quy đổi BTL
                                        if (dr[idBaiTapLop].ToString() != string.Empty)
                                        {
                                            QuyDoiBTL = Convert.ToDecimal(dr[idBaiTapLop].ToString());
                                        }
                                        #endregion

                                        #region Quy đổi ĐA, TT, TH
                                        if (dr[idDoAn_ThucTap_ThucHanh].ToString() != string.Empty)
                                        {
                                            QuyDoiDA_TT_TH = Convert.ToDecimal(dr[idDoAn_ThucTap_ThucHanh].ToString());
                                        }
                                        #endregion

                                        #region Thực tập thí nghiệm
                                        if (dr[idThucTapTotNghiep].ToString() != string.Empty)
                                        {
                                            ThucTap_ThiNghiem = Convert.ToDecimal(dr[idThucTapTotNghiep].ToString());
                                        }
                                        #endregion

                                        #region Ra đề
                                        if (dr[idRade].ToString() != string.Empty)
                                        {
                                            RaDe = Convert.ToDecimal(dr[idRade].ToString());
                                        }
                                        #endregion

                                        #region Chấm bài thi thực hành
                                        if (dr[idChamBaiThi].ToString() != string.Empty)
                                        {
                                            ChamBaiThiThucHanh = Convert.ToDecimal(dr[idChamBaiThi].ToString());
                                        }
                                        #endregion

                                        #region Tổng giờ khác A1
                                        if (dr[idTongGioKhacA1].ToString() != string.Empty)
                                        {
                                            TongGioKhacA1 = Convert.ToDecimal(dr[idTongGioKhacA1].ToString());
                                        }
                                        #endregion

                                        #region Tổng giờ
                                        if (dr[idTongGio].ToString() != string.Empty)
                                        {
                                            TongGio = Convert.ToDecimal(dr[idTongGio].ToString());
                                        }
                                        #endregion

                                        #region NhanVien
                                        if (dr[idCMND].ToString() != string.Empty || dr[idCMaHRM].ToString() != string.Empty)
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("CMND =? or MaQuanLy = ?", dr[idCMND].ToString(), dr[idCMaHRM].ToString());
                                            nhanvien = uow.FindObject<NhanVien>(fNhanVien);
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có nhân viên.");
                                        }
                                        #endregion
                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if (nhanvien != null)
                                        {
                                            ct = new ChiTietThuLaoBoiDuongThuongXuyen(uow);
                                            ct.QuanLyBoiDuongThuongXuyen = qly;
                                            ct.NoiGiangDay = hesocoso;
                                            ct.HocKy = hocky;
                                            ct.NganhHocDT = NganhHoc;
                                            ct.Khoa = txtKhoa;
                                            ct.Lop = txtLop;
                                            ct.HocKyNoiDay = txtHKNoiDay;
                                            ct.SiSo = SiSo;
                                            ct.SoNhom = SoNhom;
                                            ct.HocPhan = hocphan;
                                            ct.GhiChu = txtGhiChu;
                                            ct.NhanVien = nhanvien;
                                            ct.SoGioTamUng = SoGioTamUng;
                                            ct.SoTietLyThuyet = SoTietLyThuyet;
                                            ct.SoTietThaoLuan = SoTietThaoLuan;
                                            ct.SoTietThucHanh = SoTietThucHanh;
                                            ct.SoDVHT_TinhChi = SoDVHT_TinhChi;
                                            ct.PhuongThucDaoTao = txtPhuongThucDaoTao;
                                            ct.HinhThucThi = txtHinhThucThi;
                                            ct.SoBaiThucHanh = SoBaiThucHanh;
                                            ct.HeSoGDK5 = HeSoGDK5;
                                            ct.TongGioLyThuyetA2 = TongGioLyThuyetA2;
                                            ct.HeSoThucHanh = HeSoThucHanh;
                                            ct.QuyDoiTietThucHanh = QuyDoiTietThucHanh;
                                            ct.QuyDoiBTL = QuyDoiBTL;
                                            ct.QuyDoiDA_TT_TH = QuyDoiDA_TT_TH;
                                            ct.ThucTap_ThiNghiem = ThucTap_ThiNghiem;
                                            ct.RaDe = RaDe;
                                            ct.HeSoLopDongK1 = HeSoLopDongK1;
                                            ct.HeSoTinhChiK2 = HeSoTinhChiK2;
                                            ct.ChamBaiThiThucHanh = ChamBaiThiThucHanh;
                                            ct.TongGioKhacA1 = TongGioKhacA1;
                                            ct.TongGio = TongGio;
                                            //sucessNumber++;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + "- Nhân viên không tồn tại.");
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
}
