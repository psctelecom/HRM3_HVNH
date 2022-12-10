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
    public class Imp_CongTacPhiBoiDuongThuongXuyen
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A1:AL]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaDV = 0;
                            const int idDiaDiemDay = 1;
                            const int idMaNganh = 2;
                            const int idTenNganh = 2;
                            const int idKhoa = 4;
                            const int idLop = 5;
                            const int idHocky = 6;
                            const int idSiSo = 7;
                            const int idThuTu1 = 8;
                            const int idTenHocPhan = 9;
                            const int idSoDVHT = 10;
                            const int idSoTietLT = 11;
                            const int idSoTietTH = 12;
                            const int idSoTietTL = 13;
                            const int idGhiChu = 14;
                            const int idMaChucDanh = 15;
                            const int idHoVaTen = 16;
                            const int idCanBoGiangDay = 17;
                            const int idSDT = 18;
                            const int idDonVi = 19;
                            const int idTuNgay = 20;
                            const int idDauNoi1 = 21;
                            const int idDenNgay = 22;
                            const int idThuTu2 = 23;
                            const int idHo = 24;
                            const int idTen = 25;
                            const int idDonGiaTauXe = 26;
                            const int idHeSoTauXe = 27;
                            const int idQuyDoiTauXe = 28;
                            const int idSoNgayLuuTru = 29;
                            const int idTienLuuTru = 30;
                            const int idTongTien= 31;
                            const int idTuNgayGDD = 32;
                            const int idDauNoi2 = 33;
                            const int idDenNgayGDD = 34;
                            const int idGhiChuTong = 35;
                            const int idCMND = 36;
                            const int idMaHRM = 37;
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
                                ChiTietThuLaoCongTacPhi_BDTX ct = null;
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
                                        decimal SoDVHT = 0;
                                        int SoTietLyThuyet = 0;
                                        int SoTietThucHanh = 0;
                                        int SoTietThaoLuan = 0;
                                        decimal DinhMucTienTauXe = 0;
                                        decimal HeSoTauXe = 0;
                                        decimal QuyDoiTienTauXe = 0;
                                        decimal SoNgayLuuTru = 0;
                                        decimal SoTienLuuTru = 0;
                                        decimal TongTien = 0;
                                        decimal DonGiaLuuTru = 150000;
                                        DateTime TuNgay = DateTime.MinValue;
                                        DateTime DenNgay = DateTime.MinValue;
                                        DateTime TuNgayGDD = DateTime.MinValue;
                                        DateTime DenNgayGDD = DateTime.MinValue;
                                        #endregion
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //      
                                        #region hoc kỳ nơi dạy
                                        string txtHKNoiDay = dr[idHocky].ToString();
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
                                            CriteriaOperator fNganhHoc = CriteriaOperator.Parse("TenNganh =?", dr[idTenNganh].ToString());
                                            NganhHoc = uow.FindObject<ChuyenNganhDaoTao>(fNganhHoc);
                                            if(NganhHoc == null)
                                            {
                                                NganhHoc = new ChuyenNganhDaoTao(uow);
                                                NganhHoc.TenChuyenNganh = dr[idTenNganh].ToString();
                                                NganhHoc.MaQuanLy = dr[idMaNganh].ToString();
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

                                        #region Sỉ số
                                        if (dr[idSiSo].ToString() != string.Empty)
                                        {
                                            SiSo = Convert.ToInt32(dr[idSiSo].ToString());
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

                                        #region Số DVHT - Tính chỉ
                                        if (dr[idSoDVHT].ToString() != string.Empty)
                                        {
                                            SoDVHT = Convert.ToDecimal(dr[idSoDVHT].ToString());
                                        }
                                        #endregion

                                        #region Số tiết lý thuyết
                                        if (dr[idSoTietLT].ToString() != string.Empty)
                                        {
                                            SoTietLyThuyet = Convert.ToInt32(dr[idSoTietLT].ToString());
                                        }
                                        #endregion

                                        #region Số tiết thực hành
                                        if (dr[idSoTietTH].ToString() != string.Empty)
                                        {
                                            SoTietThucHanh = Convert.ToInt32(dr[idSoTietTH].ToString());
                                        }
                                        #endregion

                                        #region Số tiết thảo luận
                                        if (dr[idSoTietTL].ToString() != string.Empty)
                                        {
                                            SoTietThaoLuan = Convert.ToInt32(dr[idSoTietTL].ToString());
                                        }
                                        #endregion

                                        #region Định mức tiền xe
                                        if (dr[idDonGiaTauXe].ToString() != string.Empty)
                                        {
                                            DinhMucTienTauXe = Convert.ToDecimal(dr[idDonGiaTauXe].ToString());
                                        }
                                        #endregion

                                        #region Hệ số tiền xe
                                        if (dr[idHeSoTauXe].ToString() != string.Empty)
                                        {
                                            HeSoTauXe = Convert.ToDecimal(dr[idHeSoTauXe].ToString());
                                        }
                                        #endregion

                                        #region Quy đổi tiền xe
                                        if (dr[idQuyDoiTauXe].ToString() != string.Empty)
                                        {
                                            QuyDoiTienTauXe = Convert.ToDecimal(dr[idQuyDoiTauXe].ToString());
                                        }
                                        #endregion

                                        #region Số ngày lưu trú
                                        if (dr[idSoNgayLuuTru].ToString() != string.Empty)
                                        {
                                            SoNgayLuuTru = Convert.ToDecimal(dr[idSoNgayLuuTru].ToString());
                                        }
                                        #endregion

                                        #region Số tiền lưu trú
                                        if (dr[idTienLuuTru].ToString() != string.Empty)
                                        {
                                            SoTienLuuTru = Convert.ToDecimal(dr[idTienLuuTru].ToString());
                                        }
                                        #endregion

                                        #region Tổng tiền
                                        if (dr[idTongTien].ToString() != string.Empty)
                                        {
                                            TongTien = Convert.ToDecimal(dr[idTongTien].ToString());
                                        }
                                        #endregion

                                        #region NhanVien
                                        if (dr[idCMND].ToString() != string.Empty)
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("CMND =? or MaQuanLy = ?", dr[idCMND].ToString(), dr[idMaHRM].ToString());
                                            nhanvien = uow.FindObject<NhanVien>(fNhanVien);
                                        }
                                        else
                                        {
                                            errorLog.AppendLine("- STT: " + STT + " Chưa có nhân viên.");
                                        }
                                        #endregion

                                        #region Từ Ngày
                                        if (dr[idTuNgay].ToString() != string.Empty)
                                        {
                                            TuNgay = Convert.ToDateTime(dr[idTuNgay].ToString());
                                        }
                                        #endregion

                                        #region Đến Ngày
                                        if (dr[idDenNgay].ToString() != string.Empty)
                                        {
                                            DenNgay = Convert.ToDateTime(dr[idDenNgay].ToString());
                                        }
                                        #endregion

                                        #region Từ Ngày GDD
                                        if (dr[idTuNgayGDD].ToString() != string.Empty)
                                        {
                                            TuNgayGDD = Convert.ToDateTime(dr[idTuNgayGDD].ToString());
                                        }
                                        #endregion

                                        #region Đến Ngày GDD
                                        if (dr[idDenNgayGDD].ToString() != string.Empty)
                                        {
                                            DenNgayGDD = Convert.ToDateTime(dr[idDenNgayGDD].ToString());
                                        }
                                        #endregion

                                        #region Tổng tiền
                                        if (dr[idTongTien].ToString() != string.Empty)
                                        {
                                            TongTien = Convert.ToDecimal(dr[idTongTien].ToString());
                                        }
                                        #endregion

                                        #region Ghi chú tổng
                                        string txtGhoChuTong = dr[idGhiChuTong].ToString();
                                        #endregion                                      

                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if (nhanvien != null)
                                        {
                                            ct = new ChiTietThuLaoCongTacPhi_BDTX(uow);
                                            ct.QuanLyBoiDuongThuongXuyen = qly;
                                            ct.NoiGiangDay = hesocoso;
                                            ct.HocKy = hocky;
                                            ct.NganhHocDT = NganhHoc;
                                            ct.Khoa = txtKhoa;
                                            ct.Lop = txtLop;
                                            ct.HocKyNoiDay = txtHKNoiDay;
                                            ct.SiSo = SiSo;
                                            ct.HocPhan = hocphan;
                                            ct.GhiChuHeSo = txtGhiChu;
                                            ct.NhanVien = nhanvien;
                                            ct.SoTietLyThuyet = SoTietLyThuyet;
                                            ct.SoTietThaoLuan = SoTietThaoLuan;
                                            ct.SoTietThucHanh = SoTietThucHanh;
                                            ct.SoDVHT_TinhChi = SoDVHT;
                                            ct.TuNgayGDD = TuNgayGDD;
                                            ct.TuNgay = TuNgay;
                                            ct.DenNgay = DenNgay;
                                            ct.DenNgayGDD = DenNgayGDD;
                                            ct.DinhMucTienXe = DinhMucTienTauXe;
                                            ct.HeSoTienXe = HeSoTauXe;
                                            ct.QuyDoiTienXe = QuyDoiTienTauXe;
                                            ct.SoNgayLuuTru = SoNgayLuuTru;
                                            ct.DonGiaLuuTru = DonGiaLuuTru;
                                            ct.TongTienLuuTru = SoTienLuuTru;
                                            ct.GhiChuTong = txtGhoChuTong;
                                            ct.TuNgayGDD = TuNgayGDD;
                                            ct.DenNgayGDD = DenNgayGDD;                                         
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
