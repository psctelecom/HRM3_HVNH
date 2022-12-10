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
    public class Imp_KeKhaiSauGiangDay
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
                        //Loại file
                        //LoaiOfficeEnum loaiOffice = LoaiOfficeEnum.Office2003;
                        //if (open.SafeFileName.Contains(".xlsx"))
                        //{ loaiOffice = LoaiOfficeEnum.Office2010; }

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A4:V]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx
                            const int idSTT = 0;
                            const int idMaGiangVien = 1;
                            const int idHo = 2;
                            const int idTen = 3;
                            const int idTong = 4;
                            const int idChamThiHetHocPhan = 5;
                            const int idChamThiTotNghiep = 6;
                            const int idChamThiThucTapCLC = 7;
                            const int idChamCDTN = 8;
                            const int idChamBaoVeKLTN = 9;
                            const int idHDCLCThamQuanThucTe = 10;
                            const int idHDVietCDTNSVCuoiKhoa = 11;
                            const int idHDDeTaiLuanVanCaoHoc = 12;
                            const int idHDCDTN = 13;
                            const int idHDKLTN = 14;
                            const int idGiaiDapHeVLVH = 15;
                            const int idHeThongOnThiCuoiKhoa = 16;
                            const int idSoanDeThi1HocPhan = 17;
                            const int idBoSungNganHangCauHoi = 18;
                            const int idRaDeThiTotNghiep = 19;
                            const int idRaDeHetHocPhanSDH = 20;
                            const int idPhuTrachCaThi = 21;
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyKeKhaiSauGiang qly = uow.GetObjectByKey<QuanLyKeKhaiSauGiang>(OidQuanLy.Oid);
                                BoPhan bophan = null;
                                ChiTietKeKhaiSauGiang ct = null;
                                NhanVien nhanvien = null;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        decimal Tong = 0;
                                        decimal ChamThiHetHocPhan = 0;
                                        decimal ChamThiTotNghiep = 0;
                                        decimal ChamThiThucTapCLC = 0;
                                        decimal ChamCDTN = 0;
                                        decimal ChamBaoVeKLTN = 0;
                                        decimal HDCLCThamQuanThucTe = 0;
                                        decimal HDVietCDTNSVCuoiKhoa = 0;
                                        decimal HDDeTaiLuanVanCaoHoc = 0;
                                        decimal HDCDTN = 0;
                                        decimal HDKLTN = 0;
                                        decimal GiaiDapHeVLVH = 0;
                                        decimal HeThongOnThiCuoiKhoa = 0;
                                        decimal SoanDeThi1HocPhan = 0;
                                        decimal BoSungNganHangCauHoi = 0;
                                        decimal RaDeThiTotNghiep = 0;
                                        decimal RaDeHetHocPhanSDH = 0;
                                        decimal PhuTrachCaThi = 0;
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////////////
                                        #region Đọc dữ liệu
                                        //

                                        #region Mã giảng viên
                                        string txtMaGiangVien = dr[idMaGiangVien].ToString();
                                        #endregion                                     

                                        #region Số thứ tự
                                        string txtSTT = dr[idSTT].ToString();
                                        #endregion   

                                        #region Họ
                                        string txtHo = dr[idHo].ToString();
                                        #endregion  

                                        #region Tên
                                        string txtTen = dr[idTen].ToString();
                                        #endregion  

                                        #region Tổng
                                        if (dr[idTong].ToString() != string.Empty)
                                        {
                                            Tong = Convert.ToDecimal(dr[idTong].ToString());
                                        }
                                        #endregion

                                        #region Chấm thi hết(viết) học phần
                                        if (dr[idChamThiHetHocPhan].ToString() != string.Empty)
                                        {
                                            ChamThiHetHocPhan = Convert.ToDecimal(dr[idChamThiHetHocPhan].ToString());
                                        }
                                        #endregion

                                        #region Chấm thi tốt nghiệp
                                        if (dr[idChamThiTotNghiep].ToString() != string.Empty)
                                        {
                                            ChamThiTotNghiep = Convert.ToDecimal(dr[idChamThiTotNghiep].ToString());
                                        }
                                        #endregion

                                        #region Chấm học phần thực tập nghề nghiệp với sinh viên hệ CLC
                                        if (dr[idChamThiThucTapCLC].ToString() != string.Empty)
                                        {
                                            ChamThiThucTapCLC = Convert.ToDecimal(dr[idChamThiThucTapCLC].ToString());
                                        }
                                        #endregion

                                        #region Chấm CĐTN
                                        if (dr[idChamCDTN].ToString() != string.Empty)
                                        {
                                            ChamCDTN = Convert.ToDecimal(dr[idChamCDTN].ToString());
                                        }
                                        #endregion                                

                                        #region Chấm bảo vệ KLTN
                                        if (dr[idChamBaoVeKLTN].ToString() != string.Empty)
                                        {
                                            ChamBaoVeKLTN = Convert.ToDecimal(dr[idChamBaoVeKLTN].ToString());
                                        }
                                        #endregion                                

                                        #region Hướng dẫn sinh viên hệ CLC tham quan thực tế 
                                        if (dr[idHDCLCThamQuanThucTe].ToString() != string.Empty)
                                        {
                                            HDCLCThamQuanThucTe = Convert.ToDecimal(dr[idHDCLCThamQuanThucTe].ToString());
                                        }
                                        #endregion    

                                        #region Hướng dẫn viết CĐTN cho sinh viên cuối khóa (buổi)
                                        if (dr[idHDVietCDTNSVCuoiKhoa].ToString() != string.Empty)
                                        {
                                            HDVietCDTNSVCuoiKhoa = Convert.ToDecimal(dr[idHDVietCDTNSVCuoiKhoa].ToString());
                                        }
                                        #endregion    

                                        #region Hướng dẫn đề tài luận văn cho học viện cao học (buổi)
                                        if (dr[idHDDeTaiLuanVanCaoHoc].ToString() != string.Empty)
                                        {
                                            HDDeTaiLuanVanCaoHoc = Convert.ToDecimal(dr[idHDDeTaiLuanVanCaoHoc].ToString());
                                        }
                                        #endregion    

                                        #region Hướng dẫn CĐTN (quyển)
                                        if (dr[idHDCDTN].ToString() != string.Empty)
                                        {
                                            HDCDTN = Convert.ToDecimal(dr[idHDCDTN].ToString());
                                        }
                                        #endregion  

                                        #region Hướng dẫn KLTN (quyển)
                                        if (dr[idHDKLTN].ToString() != string.Empty)
                                        {
                                            HDKLTN = Convert.ToDecimal(dr[idHDKLTN].ToString());
                                        }
                                        #endregion  

                                        #region Giải đáp thắc mắc cho sinh viên hệ VLVH
                                        if (dr[idGiaiDapHeVLVH].ToString() != string.Empty)
                                        {
                                            GiaiDapHeVLVH = Convert.ToDecimal(dr[idGiaiDapHeVLVH].ToString());
                                        }
                                        #endregion 

                                        #region Hệ thống hóa và ôn thi cuối khóa
                                        if (dr[idHeThongOnThiCuoiKhoa].ToString() != string.Empty)
                                        {
                                            HeThongOnThiCuoiKhoa = Convert.ToDecimal(dr[idHeThongOnThiCuoiKhoa].ToString());
                                        }
                                        #endregion 

                                        #region Soạn bộ đề thi cho 01 học phần
                                        if (dr[idSoanDeThi1HocPhan].ToString() != string.Empty)
                                        {
                                            SoanDeThi1HocPhan = Convert.ToDecimal(dr[idSoanDeThi1HocPhan].ToString());
                                        }
                                        #endregion 

                                        #region Bổ sung ngân hàng câu hỏi, đề thi
                                        if (dr[idBoSungNganHangCauHoi].ToString() != string.Empty)
                                        {
                                            BoSungNganHangCauHoi = Convert.ToDecimal(dr[idBoSungNganHangCauHoi].ToString());
                                        }
                                        #endregion 

                                        #region Ra đề thi tốt nghiệp
                                        if (dr[idRaDeThiTotNghiep].ToString() != string.Empty)
                                        {
                                            RaDeThiTotNghiep = Convert.ToDecimal(dr[idRaDeThiTotNghiep].ToString());
                                        }
                                        #endregion 

                                        #region Ra đề thi hết học phần đào tạo SĐH
                                        if (dr[idRaDeHetHocPhanSDH].ToString() != string.Empty)
                                        {
                                            RaDeHetHocPhanSDH = Convert.ToDecimal(dr[idRaDeHetHocPhanSDH].ToString());
                                        }
                                        #endregion 

                                        #region Phụ trách ca  thi
                                        if (dr[idPhuTrachCaThi].ToString() != string.Empty)
                                        {
                                            PhuTrachCaThi = Convert.ToDecimal(dr[idPhuTrachCaThi].ToString());
                                        }
                                        #endregion

                                        #region NhanVien
                                        if (txtMaGiangVien != string.Empty && txtHo != string.Empty && txtTen != string.Empty)
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaGiangVien);
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
                                            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyKeKhaiSauGiang = ?", qly.Oid);
                                            XPCollection<ChiTietKeKhaiSauGiang> dsChiTietPhiGiaoVup = new XPCollection<ChiTietKeKhaiSauGiang>(uow, filter);
                                            if (dsChiTietPhiGiaoVup.Count == 0)
                                            {
                                                ct = new ChiTietKeKhaiSauGiang(uow);
                                                ct.QuanLyKeKhaiSauGiang = uow.GetObjectByKey<QuanLyKeKhaiSauGiang>(qly.Oid);
                                                ct.NhanVien = uow.GetObjectByKey<NhanVien>(nhanvien.Oid);
                                                ct.TongGio = Tong;
                                                ct.ChamThiHetHocPhan = ChamThiHetHocPhan;
                                                ct.ChamThiTotNghiep = ChamThiTotNghiep;
                                                ct.ChamThiThucTapCLC = ChamThiThucTapCLC;
                                                ct.ChamCDTN = ChamCDTN;
                                                ct.ChamBaoVeKLTN = ChamBaoVeKLTN;
                                                ct.HDCLCThamQuanThucTe = HDCLCThamQuanThucTe;
                                                ct.HDVietCDTNSVCuoiKhoa = HDVietCDTNSVCuoiKhoa;
                                                ct.HDDeTaiLuanVanCaoHoc = HDDeTaiLuanVanCaoHoc;
                                                ct.HDCDTN = HDCDTN;
                                                ct.HDKLTN = HDKLTN;
                                                ct.GiaiDapHeVLVH = GiaiDapHeVLVH;
                                                ct.HeThongOnThiCuoiKhoa = HeThongOnThiCuoiKhoa;
                                                ct.SoanDeThi1HocPhan = SoanDeThi1HocPhan;
                                                ct.BoSungNganHangCauHoi = BoSungNganHangCauHoi;
                                                ct.RaDeThiTotNghiep = RaDeThiTotNghiep;
                                                ct.RaDeHetHocPhanSDH = RaDeHetHocPhanSDH;
                                                ct.PhuTrachCaThi = PhuTrachCaThi;
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