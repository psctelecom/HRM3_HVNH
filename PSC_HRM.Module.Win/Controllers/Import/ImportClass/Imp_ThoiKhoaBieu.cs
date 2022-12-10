using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Win.Controllers.Import.ImportClass
{
    public class Imp_ThoiKhoaBieu
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, QuanLyThoiKhoaBieu OidQuanLy, string _chuyennganhdaotao)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[Sheet1$A13:L]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idTT = 0;
                            const int idHocPhan = 1;
                            const int idSoTinChi = 2;
                            const int idTuNgay = 3;
                            const int idDenNgay = 4;
                            const int idBuoiSangThu = 5;
                            const int idBuoiChieuThu = 6;
                            const int idHinhThucThi = 7;
                            const int idPhongHoc = 8;
                            const int idGiangVienPhuTrach = 9;
                            const int idDonViCongTac = 10;
                            const int idGhiChu = 11;

                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                QuanLyThoiKhoaBieu _QuanLyTKB = uow.GetObjectByKey<QuanLyThoiKhoaBieu>(OidQuanLy.Oid);      
                                ThoiKhoaBieu _TKB;                            
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (_QuanLyTKB != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        _TKB = new ThoiKhoaBieu(uow); 
                                        //Khởi Tạo
                                        STT++;                                      
                                        //////////////////////////GÁN LẠI DỮ LIỆU BẰNG NULL//////////////////////////
                                        var errorLog = new StringBuilder();
                                        //////////////////////////ĐỌC DỮ LIỆU//////////////////////////////
                                        #region 0. Học Phần 
                                        string txtHocPhan = dr[idHocPhan].ToString().Trim();
                                        #endregion
                                        #region 1. Số Tín Chỉ
                                        decimal txtSoTinChi = 0 ;
                                        try
                                        {
                                            txtSoTinChi = Convert.ToDecimal(dr[idSoTinChi].ToString().Trim());
                                        }
                                        catch (Exception e)
                                        {
                                            errorLog.AppendLine(" + Số tín chỉ nhập vào là kiểu số.");
                                        }                                      
                                        #endregion
                                        #region 2. Từ Ngày
                                        DateTime txtTuNgay = DateTime.Now;
                                        try
                                        {                                          
                                            txtTuNgay = Convert.ToDateTime(dr[idTuNgay].ToString());
                                        }
                                        catch (Exception e)
                                        {
                                            errorLog.AppendLine(" + Số Từ Ngày vào là kiểu dd/MM/yyyy.");
                                        }                                            
                                        #endregion
                                        #region 3. Đến Ngày
                                        DateTime txtDenNgay = DateTime.Now;
                                        try
                                        {
                                            var i = dr[idDenNgay].ToString();
                                            txtDenNgay = Convert.ToDateTime(i.ToString());
                                        }
                                        catch (Exception e)
                                        {
                                            errorLog.AppendLine(" + Số Từ Ngày vào là kiểu dd/MM/yyyy.");
                                        }                                                      
                                        #endregion
                                        #region 4. Buổi Sáng Thứ 
                                        //Nếu trong file Excel là số thì định dạng lại là chuỗi
                                        string txtBuoiSangThu = "";
                                        if (!string.IsNullOrEmpty(dr[idBuoiSangThu].ToString()))
                                        {
                                            txtBuoiSangThu = dr[idBuoiSangThu].ToString().Trim();
                                        }                                    
                                        #endregion
                                        #region 5. Buổi Chiều Thứ
                                        string txtBuoiChieuThu = "";
                                        if (!string.IsNullOrEmpty(dr[idBuoiChieuThu].ToString()))
                                        {
                                            txtBuoiChieuThu = dr[idBuoiChieuThu].ToString().Trim();
                                        }     
                                       
                                        #endregion
                                        #region 6. Hình Thức Thi
                                        string txtHinhThucThi = dr[idHinhThucThi].ToString().Trim();
                                        #endregion
                                        #region 7. Phòng Học
                                        string txtPhongHoc = dr[idPhongHoc].ToString().Trim();
                                        #endregion
                                        #region 8. Giảng Viên Phụ Trách
                                        string txtNhanVien = dr[idGiangVienPhuTrach].ToString().Trim();
                                        #endregion
                                        #region 9. Đơn Vị Công Tác
                                        string txtBoPhan = dr[idDonViCongTac].ToString().Trim();
                                        #endregion
                                        #region 10. Ghi Chú
                                        string txtGhiChu = dr[idGhiChu].ToString().Trim();
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region 0. HocPhan 
                                        if (!string.IsNullOrEmpty(txtHocPhan))
                                        {
                                            HocPhan _hp = uow.FindObject<HocPhan>(CriteriaOperator.Parse("TenHocPhan like ?", txtHocPhan));
                                            //Nếu tồn tại HocPhan
                                            if (_hp == null)
                                            {
                                                _hp = new HocPhan(uow);
                                                _hp.TenHocPhan = txtHocPhan;          
                                            }
                                            _TKB.HocPhan = _hp;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin Học phần.");
                                        }
                                        #endregion
                                        #region 1. SoTinChi
                                        if (!string.IsNullOrEmpty(txtSoTinChi.ToString()))
                                        {
                                            _TKB.SoTinChi = txtSoTinChi;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin Số tín chỉ.");
                                        }
                                        #endregion
                                        #region 2. TuNgay
                                        _TKB.TuNgay = txtTuNgay;                                    
                                        #endregion
                                        #region 3. DenNgay
                                        _TKB.DenNgay = txtDenNgay;                                      
                                        #endregion
                                        #region 4. BuoiSangThu
                                        _TKB.BuoiSangThu = txtBuoiSangThu;
                                        #endregion
                                        #region 5. BuoiChieuThu
                                        _TKB.BuoiChieuThu = txtBuoiChieuThu;
                                        #endregion
                                        #region 6. HinhThucThi
                                        if (!string.IsNullOrEmpty(txtHinhThucThi))
                                        {
                                            HinhThucThi _hinhthucthi = uow.FindObject<HinhThucThi>(CriteriaOperator.Parse("TenHinhThucThi like ?", txtHinhThucThi));
                                            //Nếu tồn tại HocPhan
                                            if (_hinhthucthi == null)
                                            {
                                                _hinhthucthi = new HinhThucThi(uow);
                                                _hinhthucthi.TenHinhThucThi = txtHinhThucThi;
                                            }
                                            _TKB.TenHinhThucThi = _hinhthucthi;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin Hình Thức Thi.");
                                        }
                                        #endregion
                                        #region 7. PhongHoc
                                        _TKB.PhongHoc = txtPhongHoc;
                                        #endregion
                                        #region 8. NhanVien
                                        if (!string.IsNullOrEmpty(txtNhanVien))
                                        {
                                            NhanVien _nv = uow.FindObject<NhanVien>(CriteriaOperator.Parse("HoTen like ?", txtNhanVien));
                                            //Nếu tồn tại NhanVien
                                            if (_nv != null)
                                            {
                                                _TKB.GiangVien = _nv;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Không tồn tại Nhân Viên.");
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin Nhân Viên.");
                                        }
                                        #endregion
                                        #region 9. BoPhan(Đơn Vị Công Tác)
                                        if (!string.IsNullOrEmpty(txtBoPhan))
                                        {
                                            BoPhan _bp = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan like ?", txtBoPhan));
                                            //Nếu tồn tại HocPhan
                                            if (_bp != null)
                                            {
                                                _TKB.BoPhan = _bp;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Không tồn tại Bộ Phận.");
                                            }
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Thiếu thông tin Bộ phận.");
                                        }
                                        #endregion
                                        #region 10. GhiChu
                                        _TKB.GhiChu = txtGhiChu;
                                        #endregion
                                        #region 11. ChuyenNganhDaoTao
                                        ChuyenNganhDaoTao _CNDT = uow.FindObject<ChuyenNganhDaoTao>(CriteriaOperator.Parse("TenChuyenNganh like ?", _chuyennganhdaotao));
                                        
                                        _TKB.ChuyenNganhDaoTao = _CNDT;
                                        #endregion
                                        //Add _TKB vào ListQuanLyTKB
                                        _QuanLyTKB.ListThoiKhoaBieu.Add(_TKB);
                                      
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
                                            uow.CommitChanges();////Lưu _ Cứ mỗi lần chạy vòng lặp thì Lưu 1 lần                                  
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
                                        uow.CommitChanges();//Lưu _ Đúng hết rồi mới lưu 
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