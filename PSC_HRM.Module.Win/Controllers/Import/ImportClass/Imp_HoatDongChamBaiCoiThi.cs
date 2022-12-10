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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.Import.ImportClass
{
    public class Imp_HoatDongChamBaiCoiThi
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$D1:Q]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaNhanVien = 0;
                            const int idHoTen = 1;
                            const int idKhoanChi = 2;
                            const int idTenMonHoc = 3;
                            const int idLopHocPhan = 4;
                            const int idSoBaiQuaTrinh = 5;
                            const int idSoBaiGiuaKy = 6;
                            const int idSoBaiCuoiKy = 7;
                            const int idDonGiaQuaTrinh = 8;
                            const int idDonGiaGiuaKy = 9;
                            const int idDonGiaCuoiKy = 10;
                            const int idTongTien = 11;
                            const int idTongGio = 12;
                            const int idCMND = 13;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyHoatDongKhac qly = uow.GetObjectByKey<QuanLyHoatDongKhac>(OidQuanLy.Oid);
                                NhanVien nhanVien = null;
                                ChiTietThuLaoChamBaiCoiThi ct;                              
                                int SoBaiQuaTrinh = 0;
                                int SoBaiGiuaKy = 0;
                                int SoBaiCuoiKy = 0;
                                decimal DonGiaQuaTrinh = 0;
                                decimal DonGiaGiuaKy = 0;
                                decimal DonGiaCuoiKy = 0;
                                decimal TongTien = 0;
                                decimal TongGio = 0;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly!=null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
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
                                        string txtKhoaChi = dr[idKhoanChi].ToString();
                                        #endregion

                                        #region Tên môn học
                                        string txtTenMonHoc = dr[idTenMonHoc].ToString();
                                        #endregion

                                        #region Tên lớp học phần
                                        string txtLopHocPhan = dr[idLopHocPhan].ToString();
                                        #endregion

                                        #region Số bài quá trình
                                        if (!string.IsNullOrEmpty(dr[idSoBaiQuaTrinh].ToString()))
                                        {
                                            SoBaiQuaTrinh = Convert.ToInt32(dr[idSoBaiQuaTrinh].ToString());
                                        }
                                        #endregion

                                        #region Số bài giữa kỳ
                                        if (!string.IsNullOrEmpty(dr[idSoBaiGiuaKy].ToString()))
                                        {
                                            SoBaiGiuaKy = Convert.ToInt32(dr[idSoBaiGiuaKy].ToString());
                                        }
                                        #endregion

                                        #region Số bài cuối kỳ
                                        if (!string.IsNullOrEmpty(dr[idSoBaiCuoiKy].ToString()))
                                        {
                                            SoBaiCuoiKy = Convert.ToInt32(dr[idSoBaiCuoiKy].ToString());
                                        }                                       
                                        #endregion

                                        #region Đơn giá quá trình
                                        if (!string.IsNullOrEmpty(dr[idDonGiaQuaTrinh].ToString()))
                                        {
                                            DonGiaQuaTrinh = Convert.ToDecimal(dr[idDonGiaQuaTrinh].ToString());
                                        }                                        
                                        #endregion

                                        #region Đơn giá giữa kỳ
                                        if (!string.IsNullOrEmpty(dr[idDonGiaGiuaKy].ToString()))
                                        {
                                            DonGiaGiuaKy = Convert.ToDecimal(dr[idDonGiaGiuaKy].ToString());
                                        }                                      
                                        #endregion

                                        #region Đơn giá cuối kỳ
                                        if (!string.IsNullOrEmpty(dr[idDonGiaCuoiKy].ToString()))
                                        {
                                            DonGiaCuoiKy = Convert.ToDecimal(dr[idDonGiaCuoiKy].ToString());
                                        }                                     
                                        #endregion

                                        #region Thành tiền
                                        if (!string.IsNullOrEmpty(dr[idTongTien].ToString()))
                                        {
                                            TongTien = Convert.ToDecimal(dr[idTongTien].ToString());
                                        }
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
                                        if ((!string.IsNullOrEmpty(txtHoTen) || !string.IsNullOrEmpty(txtCMND)) && !string.IsNullOrEmpty(txtKhoaChi))
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                            nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                            if (nhanVien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                            }
                                            else
                                            {

                                                #region Tạo ChiTietThuLaoChamBaiCoiThi
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