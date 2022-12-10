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
using PSC_HRM.Module.PMS.NghiepVu.KeKhaiSauGiang;
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
    public class Imp_KheKhaiSauGiang_HVNH
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyKeKhaiSauGiang OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A3:AB]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idSTT = 0;
                            const int idMaGiangVien= 1;
                            const int idHoTen = 2;
                            const int idChamThiHetHocPhan = 3;
                            const int idChamThiHetHocPhanVanDap = 4;
                            const int idChamThiHetHocPhanTieuLuan = 5;
                            const int idChamThiTotNghiep = 6;
                            const int idChamThiThucTapNgheHeCLC= 7;
                            const int idChamCDTN = 8;
                            const int idChamBaoVeKLTN = 9;
                            const int idChamBaoVeKLTNCLCTV = 10;
                            const int idChamBaoVeKLTNCLCTA= 11;
                            const int idHDHeCLCThamQuangThucTe = 12;
                            const int idHDChuyenDeTotNghiepCuoiKhoa = 13;
                            const int idHDDeTaiLuanVanHocVienCaoHoc = 14;
                            const int idHDChuyenDeTN = 15;
                            const int idHDThucTeNgheNghiepCLC = 16;
                            const int idHDKhoaLuatTotNghiep = 17;
                            const int idHDKhoaLuatTotNghiepCLCTV = 18;
                            const int idHDKhoaLuatTotNghiepCLCTA = 19;
                            const int idGiaiDapThacMatHeVHVL = 20;
                            const int idHeThongHoaVaOnThiCuoiKhoa = 21;
                            const int idRaDeThiTotNghiep = 22;
                            const int idRaDeThiHetHocPhanDaoTaoSDH = 23;
                            const int idPhuDaoSinhVienNuocNgoai = 24;
                            const int idGhiChu = 25;
                            const int idBacDaoTao = 26;
                            const int idHeDaoTao = 27;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                NhanVien nhanvien = null;
                                BacDaoTao bacdaotao = null;
                                HeDaoTao hedaotao = null;
                                ChiTietKeKhaiSauGiang ct = null;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (OidQuanLy != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        #region Khởi tạo                                     
                                        int ChamThiHetHocPhan = 0;
                                        int ChamThiHetHocPhanVanDap = 0;
                                        int ChamThiHetHocPhanTieuLuan = 0;
                                        int ChamThiTotNghiep = 0;
                                        int ChamThiThucTapNgheHeCLC = 0;
                                        int ChamCDTN = 0;
                                        int ChamBaoVeKLTN = 0;
                                        int ChamBaoVeKLTNCLCTV = 0;
                                        int ChamBaoVeKLTNCLCTA = 0;
                                        int HDThucTeNgheNghiepCLC = 0;
                                        int HDHeCLCThamQuangThucTe = 0;
                                        int HDChuyenDeTotNghiepCuoiKhoa = 0;
                                        int HDDeTaiLuanVanHocVienCaoHoc = 0;
                                        int HDChuyenDeTN = 0;
                                        int HDKhoaLuatTotNghiep = 0;
                                        int HDKhoaLuatTotNghiepCLCTV = 0;
                                        int HDKhoaLuatTotNghiepCLCTA = 0;
                                        int GiaiDapThacMatHeVHVL = 0;
                                        int HeThongHoaVaOnThiCuoiKhoa = 0;
                                        int SoanBoDeThiChoMotHocPhan = 0;
                                        int BoSungNganHangCauHoiDeThi = 0;
                                        int RaDeThiTotNghiep = 0;
                                        int RaDeThiHetHocPhanDaoTaoSDH = 0;
                                        int PhuTrachCThi = 0;
                                        int PhuDaoSinhVienNuocNgoai = 0;
                                        string GhiChu = "";
                                        #endregion
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //                                        

                                        if (dr[idMaGiangVien].ToString() != string.Empty || dr[idBacDaoTao].ToString() != string.Empty || dr[idHeDaoTao].ToString() != string.Empty)
                                        {
                                            STT++;
                                            #region Chấm thi hết học phần
                                            //TỰ LUẬN
                                            if (dr[idChamThiHetHocPhan].ToString() != string.Empty)
                                            {
                                                ChamThiHetHocPhan = Convert.ToInt32(dr[idChamThiHetHocPhan].ToString());
                                            }
                                            if (dr[idChamThiHetHocPhanVanDap].ToString() != string.Empty)
                                            {
                                                ChamThiHetHocPhanVanDap = Convert.ToInt32(dr[idChamThiHetHocPhanVanDap].ToString());
                                            }
                                            if (dr[idChamThiHetHocPhanTieuLuan].ToString() != string.Empty)
                                            {
                                                ChamThiHetHocPhanTieuLuan = Convert.ToInt32(dr[idChamThiHetHocPhanTieuLuan].ToString());
                                            }
                                            #endregion

                                            #region Chấm thi tốt nghiệp
                                            if (dr[idChamThiTotNghiep].ToString() != string.Empty)
                                            {
                                                ChamThiTotNghiep = Convert.ToInt32(dr[idChamThiTotNghiep].ToString());
                                            }
                                            #endregion

                                            #region Chấm thi thực tập nghề chất lượng cao
                                            if (dr[idChamThiThucTapNgheHeCLC].ToString() != string.Empty)
                                            {
                                                ChamThiThucTapNgheHeCLC = Convert.ToInt32(dr[idChamThiThucTapNgheHeCLC].ToString());
                                            }
                                            #endregion

                                            #region Chấm chuyên đề TN
                                            if (dr[idChamCDTN].ToString() != string.Empty)
                                            {
                                                ChamCDTN = Convert.ToInt32(dr[idChamCDTN].ToString());
                                            }
                                            #endregion

                                            #region Chấm bảo vệ KLTN
                                            //ĐẠI TRÀ
                                            if (dr[idChamBaoVeKLTN].ToString() != string.Empty)
                                            {
                                                ChamBaoVeKLTN = Convert.ToInt32(dr[idChamBaoVeKLTN].ToString());
                                            }
                                            if (dr[idChamBaoVeKLTNCLCTV].ToString() != string.Empty)
                                            {
                                                ChamBaoVeKLTNCLCTV = Convert.ToInt32(dr[idChamBaoVeKLTNCLCTV].ToString());
                                            }
                                            if (dr[idChamBaoVeKLTNCLCTA].ToString() != string.Empty)
                                            {
                                                ChamBaoVeKLTNCLCTA = Convert.ToInt32(dr[idChamBaoVeKLTNCLCTA].ToString());
                                            }
                                            #endregion

                                            #region HD THỰC TẾ NGHỀ NGHIỆP CLC
                                            if (dr[idHDThucTeNgheNghiepCLC].ToString() != string.Empty)
                                            {
                                                HDThucTeNgheNghiepCLC = Convert.ToInt32(dr[idHDThucTeNgheNghiepCLC].ToString());
                                            }
                                            #endregion

                                            #region HD hệ CLC tham quan thực tế
                                            if (dr[idHDHeCLCThamQuangThucTe].ToString() != string.Empty)
                                            {
                                                HDHeCLCThamQuangThucTe = Convert.ToInt32(dr[idHDHeCLCThamQuangThucTe].ToString());
                                            }
                                            #endregion

                                            #region HD chuyên đề tốt nghiệp cuối khóa
                                            if (dr[idHDChuyenDeTotNghiepCuoiKhoa].ToString() != string.Empty)
                                            {
                                                HDChuyenDeTotNghiepCuoiKhoa = Convert.ToInt32(dr[idHDChuyenDeTotNghiepCuoiKhoa].ToString());
                                            }
                                            #endregion

                                            #region HD đề tài luận văn học viên cao học
                                            if (dr[idHDDeTaiLuanVanHocVienCaoHoc].ToString() != string.Empty)
                                            {
                                                HDDeTaiLuanVanHocVienCaoHoc = Convert.ToInt32(dr[idHDDeTaiLuanVanHocVienCaoHoc].ToString());
                                            }
                                            #endregion

                                            #region HD chuyên đề TN
                                            if (dr[idHDChuyenDeTN].ToString() != string.Empty)
                                            {
                                                HDChuyenDeTN = Convert.ToInt32(dr[idHDChuyenDeTN].ToString());
                                            }
                                            #endregion

                                            #region HD khoa luận tốt nghiệp
                                            //ĐẠI TRÀ
                                            if (dr[idHDKhoaLuatTotNghiep].ToString() != string.Empty)
                                            {
                                                HDKhoaLuatTotNghiep = Convert.ToInt32(dr[idHDKhoaLuatTotNghiep].ToString());
                                            }
                                            if (dr[idHDKhoaLuatTotNghiepCLCTV].ToString() != string.Empty)
                                            {
                                                HDKhoaLuatTotNghiepCLCTV = Convert.ToInt32(dr[idHDKhoaLuatTotNghiepCLCTV].ToString());
                                            }
                                            if (dr[idHDKhoaLuatTotNghiepCLCTA].ToString() != string.Empty)
                                            {
                                                HDKhoaLuatTotNghiepCLCTA = Convert.ToInt32(dr[idHDKhoaLuatTotNghiepCLCTA].ToString());
                                            }
                                            #endregion

                                            #region GiaiDapThacMatVHVL
                                            if (dr[idGiaiDapThacMatHeVHVL].ToString() != string.Empty)
                                            {
                                                GiaiDapThacMatHeVHVL = Convert.ToInt32(dr[idGiaiDapThacMatHeVHVL].ToString());
                                            }
                                            #endregion

                                            #region HeThongHoaVaOnThiCuoiKhoa
                                            if (dr[idHeThongHoaVaOnThiCuoiKhoa].ToString() != string.Empty)
                                            {
                                                HeThongHoaVaOnThiCuoiKhoa = Convert.ToInt32(dr[idHeThongHoaVaOnThiCuoiKhoa].ToString());
                                            }
                                            #endregion

                                            #region RaDeThiTotNghiep
                                            if (dr[idRaDeThiTotNghiep].ToString() != string.Empty)
                                            {
                                                RaDeThiTotNghiep = Convert.ToInt32(dr[idRaDeThiTotNghiep].ToString());
                                            }
                                            #endregion

                                            #region RaDeThiHetHocPhanDaoTaoSDH
                                            if (dr[idRaDeThiHetHocPhanDaoTaoSDH].ToString() != string.Empty)
                                            {
                                                RaDeThiHetHocPhanDaoTaoSDH = Convert.ToInt32(dr[idRaDeThiHetHocPhanDaoTaoSDH].ToString());
                                            }
                                            #endregion

                                            #region PhuDaoSinhVienNuocNgoai
                                            if (dr[idPhuDaoSinhVienNuocNgoai].ToString() != string.Empty)
                                            {
                                                PhuDaoSinhVienNuocNgoai = Convert.ToInt32(dr[idPhuDaoSinhVienNuocNgoai].ToString());
                                            }
                                            #endregion

                                            #region GhiChu
                                            if (dr[idGhiChu].ToString() != string.Empty)
                                            {
                                                GhiChu = dr[idGhiChu].ToString();
                                            }
                                            #endregion

                                            #region NhanVien
                                            if (dr[idMaGiangVien].ToString() != string.Empty)
                                            {
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy = ?", dr[idMaGiangVien].ToString());
                                                nhanvien = uow.FindObject<NhanVien>(fNhanVien);
                                                if (nhanvien == null)
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + "- Nhân viên không tồn tại.");
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Chưa có nhân viên.");
                                            }
                                            #endregion

                                            #region BacDaoTao
                                            if (dr[idBacDaoTao].ToString() != string.Empty)
                                            {
                                                CriteriaOperator fBacDaoTao = CriteriaOperator.Parse("MaQuanLy = ?", dr[idBacDaoTao].ToString());
                                                bacdaotao = uow.FindObject<BacDaoTao>(fBacDaoTao);
                                                if (bacdaotao == null)
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + " Bậc đào tạo sai.");
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Bậc đào tạo sai.");
                                            }
                                            #endregion

                                            #region HeDaoTao
                                            if (dr[idHeDaoTao].ToString() != string.Empty)
                                            {
                                                CriteriaOperator fHeDaoTao = CriteriaOperator.Parse("MaQuanLy = ?", dr[idHeDaoTao].ToString());
                                                hedaotao = uow.FindObject<HeDaoTao>(fHeDaoTao);
                                                if (bacdaotao == null)
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + " Loại hình đào tạo sai.");
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Loại hình đào tạo sai.");
                                            }
                                            #endregion
                                            //
                                            #endregion
                                            //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////

                                            #region Kiểm tra dữ liệu
                                            if (nhanvien != null && bacdaotao != null && hedaotao != null)
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("QuanLyKeKhaiSauGiang =? and NhanVien=? and BacDaoTao = ? and HeDaoTao = ?", OidQuanLy.Oid, nhanvien.Oid, bacdaotao.Oid, hedaotao.Oid);
                                                XPCollection<ChiTietKeKhaiSauGiang> dsChiTietKhoiLuongSauDaiHoc = new XPCollection<ChiTietKeKhaiSauGiang>(uow, filter);

                                                if (dsChiTietKhoiLuongSauDaiHoc.Count == 0)
                                                {
                                                    ct = new ChiTietKeKhaiSauGiang(uow);
                                                    ct.QuanLyKeKhaiSauGiang = uow.GetObjectByKey<QuanLyKeKhaiSauGiang>(OidQuanLy.Oid);
                                                    ct.SLChamThiHetHocPhan = ChamThiHetHocPhan;
                                                    ct.SLChamThiHetHocPhanVanDap = ChamThiHetHocPhanVanDap;//
                                                    ct.SLChamThiHetHocPhanTieuLuan = ChamThiHetHocPhanTieuLuan;//
                                                    ct.SLBoSungNganHangCauHoi = BoSungNganHangCauHoiDeThi;
                                                    ct.SLCaCoiThi = PhuTrachCThi;
                                                    ct.SLCDTN = ChamCDTN;
                                                    ct.SLHDCDTN = HDChuyenDeTN;
                                                    ct.SLChamThiTN = ChamThiTotNghiep;
                                                    ct.SLGiaiDapThacMac = GiaiDapThacMatHeVHVL;
                                                    ct.SLHDDeTaiLuanVan = HDDeTaiLuanVanHocVienCaoHoc;
                                                    ct.SLHDSVThamQuanThucTe = HDHeCLCThamQuangThucTe;
                                                    ct.SLHDVietCDTN = HDChuyenDeTotNghiepCuoiKhoa;
                                                    ct.SLHeThongHoa_OnThi = HeThongHoaVaOnThiCuoiKhoa;
                                                    ct.NhanVien = nhanvien;
                                                    ct.SLKhoaLuanTN = ChamBaoVeKLTN;
                                                    ct.SLKhoaLuanTNCLCTV = ChamBaoVeKLTNCLCTV;//
                                                    ct.SLKhoaLuanTNCLCTA = ChamBaoVeKLTNCLCTA;//
                                                    ct.SLHDKhoaLuanTN = HDKhoaLuatTotNghiep;
                                                    ct.SLHDKhoaLuanTNCLCTV = HDKhoaLuatTotNghiepCLCTV;//
                                                    ct.SLHDKhoaLuanTNCLCTA = HDKhoaLuatTotNghiepCLCTA;//
                                                    ct.SLRaDeThiHetHocPhan = RaDeThiHetHocPhanDaoTaoSDH;
                                                    ct.SLRaDeTotNghiep = RaDeThiTotNghiep;
                                                    ct.SLSoanDeThi = SoanBoDeThiChoMotHocPhan;
                                                    ct.SLThucTapNgheCLC = ChamThiThucTapNgheHeCLC;
                                                    ct.SLPhuDaoSinhVienNuocNgoai = PhuDaoSinhVienNuocNgoai;
                                                    ct.GhiChu = GhiChu;
                                                    ct.BacDaoTao = bacdaotao;
                                                    ct.HeDaoTao = hedaotao;
                                                    ct.SLHDThucTeNgeNghiepCLC = HDThucTeNgheNghiepCLC;//

                                                    sucessNumber++;
                                                    uow.CommitChanges();

                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + "- Dữ liệu sai.");
                                            }
                                            #endregion
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


                                    //hợp lệ cả file mới lưu
                                    if (erorrNumber > 0)
                                    {
                                        uow.RollbackTransaction(); //trả lại dữ liệu ban đầu
                                    }
                                    else
                                    {
                                        //uow.CommitChanges();//Lưu
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
