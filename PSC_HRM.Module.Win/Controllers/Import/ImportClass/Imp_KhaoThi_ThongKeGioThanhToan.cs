using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
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
    public class Imp_KhaoThi_ThongKeGioThanhToan
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyKhaoThi OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$D1:Q]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaNhanVien = 0;
                            const int idHoTen = 1;
                            const int idDonVi = 2;
                            const int idSoLuongBaiChamGiuaKy = 3;
                            const int idSoLuongDeThi = 4;
                            const int idSoCaCoiThi = 5;
                            const int idSoLuongBaiChamThiTuLuan = 6;
                            const int idSoLuongBaiChamVDTHTin = 7;
                            const int idSoLuongBaiChamGDQPTC = 8;
                            const int idSoLuongBaiChamTieuLuan = 9;
                            const int idTongGio = 10;
                            const int idGhiChu = 11;
                            const int idCMND = 12;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyKhaoThi qly = uow.GetObjectByKey<QuanLyKhaoThi>(OidQuanLy.Oid);
                                NhanVien nhanVien = null;
                                ChiTietChamBaiCoiThi ct;
                                int stt = 0;
                                int SLBaiChamGK = 0;
                                int SLDeThi = 0;
                                int SLCaCoiThi = 0;
                                int SLChamThiTuLuan = 0;
                                int SLChamVDTHTin = 0;
                                int SLChamGDQPTC = 0;
                                int SLChamTieuLuan = 0;
                                decimal TongGio = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly!=null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        stt++;
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

                                        #region Đơn vị
                                        string txtDonVi = dr[idDonVi].ToString();
                                        #endregion

                                        #region SL bài chấm giữa kỳ
                                        if (!string.IsNullOrEmpty(dr[idSoLuongBaiChamGiuaKy].ToString()))
                                            SLBaiChamGK = Convert.ToInt32(dr[idSoLuongBaiChamGiuaKy].ToString());
                                        #endregion

                                        #region Số lượng đề thi
                                        if (!string.IsNullOrEmpty(dr[idSoLuongDeThi].ToString()))
                                            SLDeThi = Convert.ToInt32(dr[idSoLuongDeThi].ToString());
                                        #endregion

                                        #region Số ca coi thi
                                        if (!string.IsNullOrEmpty(dr[idSoCaCoiThi].ToString()))
                                            SLCaCoiThi = Convert.ToInt32(dr[idSoCaCoiThi].ToString());
                                        #endregion

                                        #region SL bài chấm thi tự luận
                                        if (!string.IsNullOrEmpty(dr[idSoLuongBaiChamThiTuLuan].ToString()))
                                            SLChamThiTuLuan = Convert.ToInt32(dr[idSoLuongBaiChamThiTuLuan].ToString());
                                        #endregion

                                        #region SL bài chấm VĐ, TH Tin
                                        if (!string.IsNullOrEmpty(dr[idSoLuongBaiChamVDTHTin].ToString()))
                                            SLChamVDTHTin = Convert.ToInt32(dr[idSoLuongBaiChamVDTHTin].ToString());
                                        #endregion

                                        #region SL bài chấm GDTC-QP
                                        if (!string.IsNullOrEmpty(dr[idSoLuongBaiChamGDQPTC].ToString()))
                                            SLChamGDQPTC = Convert.ToInt32(dr[idSoLuongBaiChamGDQPTC].ToString());
                                        #endregion

                                        #region SL bài chấm Tiểu luận
                                        if (!string.IsNullOrEmpty(dr[idSoLuongBaiChamTieuLuan].ToString()))
                                            SLChamTieuLuan = Convert.ToInt32(dr[idSoLuongBaiChamTieuLuan].ToString());
                                        #endregion

                                        #region Tổng giờ
                                        if (!string.IsNullOrEmpty(dr[idTongGio].ToString()))
                                        {
                                            TongGio = Convert.ToDecimal(dr[idTongGio].ToString());
                                        }
                                        #endregion

                                        #region CMND
                                        string txtCMND = dr[idCMND].ToString();
                                        #endregion
                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if ((!string.IsNullOrEmpty(txtHoTen) || !string.IsNullOrEmpty(txtCMND)) || !string.IsNullOrEmpty(txtMaNhanVien))
                                        {
                                            if (!string.IsNullOrEmpty(txtMaNhanVien))
                                            {
                                                psc_UIS_GiangVien uisGiangVien = uow.FindObject<psc_UIS_GiangVien>(CriteriaOperator.Parse("ProfessorID =?", txtMaNhanVien));
                                                if(uisGiangVien!=null)
                                                {
                                                    nhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("Oid =?", uisGiangVien.OidNhanVien));                                                    
                                                }
                                            }
                                            if (nhanVien == null)
                                            {
                                                CriteriaOperator fNhanVien = CriteriaOperator.Parse("HoTen =? or CMND =?", txtHoTen, txtCMND);
                                                nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                            }
                                            if (nhanVien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + stt + " Không tồn tại nhân viên.");
                                            }
                                            else
                                            {

                                                #region Tạo ChiTietChamBaiCoiThi

                                                CriteriaOperator fChamBaiCoiThi = CriteriaOperator.Parse("QuanLyHoatDongKhac =? and KhoanChi =? and TenMonHoc =? and LopHocPhan =?  and SoBaiQuaTrinh =? and SoBaiGiuaKy =? and SoBaiCuoiKy =? and DonGiaQuaTrinh =? and DonGiaGiuaKy =? and DonGiaCuoiKy =? and TongTien =?", qly.Oid,
                                                txtKhoaChi, txtTenMonHoc, txtLopHocPhan, SoBaiQuaTrinh, SoBaiGiuaKy, SoBaiCuoiKy, DonGiaQuaTrinh, DonGiaGiuaKy, DonGiaCuoiKy, TongTien);
                                                XPCollection<ChiTietThuLaoChamBaiCoiThi> dschambaicoithi = new XPCollection<ChiTietThuLaoChamBaiCoiThi>(uow, fChamBaiCoiThi);
                                                if (dschambaicoithi.Count == 0)
                                                {
                                                    ct = new ChiTietThuLaoChamBaiCoiThi(uow);
                                                    ct.KhoanChi = txtKhoaChi;
                                                    ct.TenMonHoc = txtTenMonHoc;
                                                    ct.LopHocPhan = txtLopHocPhan;
                                                    ct.DonGiaQuaTrinh = DonGiaQuaTrinh;
                                                    ct.DonGiaGiuaKy = DonGiaGiuaKy;
                                                    ct.DonGiaCuoiKy = DonGiaCuoiKy;
                                                    ct.SoBaiQuaTrinh = SoBaiQuaTrinh;
                                                    ct.SoBaiGiuaKy = SoBaiGiuaKy;
                                                    ct.SoBaiCuoiKy = SoBaiCuoiKy;
                                                    ct.TongTien = TongTien;
                                                    ct.TongTienThueTNCN = TongTien;
                                                    ct.NhanVien = nhanVien;
                                                    ct.BoPhan = nhanVien.BoPhan;
                                                    ct.TongGio = TongGio;
                                                    ct.CMND = txtCMND;
                                                    qly.ListChamBaiCoiThi.Add(ct);
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