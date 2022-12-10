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
    public class Imp_KhoiLuongGiangDay_HVNH
    {
        #region Nhập dữ liệu từ tập tin excel
        public static void XuLy(IObjectSpace obs, KhoiLuongGiangDay OidQuanLy)
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[KhoiLuongGiangDay$A2:Y]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaGianVien = 0;

                            const int idMaNhom = 5;
                            const int idMaLopHP = 6;
                            const int idMaMonHoc = 7;
                            const int idTenMonHoc=8;
                            const int idSiSo = 9;
                            const int idGhiChu = 10;

                            const int idSoTiet = 11;
                            const int idHeSoTinChi = 12;
                            const int idChamBai = 16;
                            const int idSoTinChi = 19;
                            const int idHeSoLopNgoaiGio = 20;
                            const int idBacDaoTao = 21;
                            const int idNgonNguGiangDay = 22;
                            const int idCoSoGiangDay = 23;
                            const int idTonGio = 24;

                  
                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                bool TiepTuc = true;
                                NhanVien nhanVien;       
                                ChiTietKhoiLuongGiangDay ct;
                                KhoiLuongGiangDay qly = uow.GetObjectByKey<KhoiLuongGiangDay>(OidQuanLy.Oid);
                                BacDaoTao bacDaotao;
                                CoSoGiangDay coSoGiangDay;
                                //CriteriaOperator fCauHinh = CriteriaOperator.Parse("NamHoc =?", OidQuanLy.NamHoc.Oid);
                                //CauHinhQuyDoiPMS cauhinh = uow.FindObject<CauHinhQuyDoiPMS>(fCauHinh);                             
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

                                        #region Mã Giảng viên
                                        string txtMaGiangVien = dr[idMaGianVien].ToString();
                                        #endregion

                                        #region Mã nhóm
                                        string txtMaNhom = dr[idMaNhom].ToString();
                                        #endregion

                                        #region MaLopHP
                                        string txtMaLopHP = dr[idMaLopHP].ToString();
                                        #endregion

                                        #region MaMonHoc
                                        string txtMaMonHoc = dr[idMaMonHoc].ToString();
                                        #endregion

                                        #region Tên môn học
                                        string txtTenMonHoc = dr[idTenMonHoc].ToString();
                                        #endregion

                                        #region SiSo
                                        int txtSiSo = Convert.ToInt32(dr[idSiSo].ToString());
                                        #endregion

                                        #region Ghi chú
                                        string txtGhiChu = dr[idGhiChu].ToString();
                                        #endregion

                                        #region SoTiet
                                        decimal txtSoTiet = Convert.ToDecimal(dr[idSoTiet].ToString());
                                        #endregion

                                        #region HeSoTinChi
                                        decimal txtHeSoTinChi = Convert.ToDecimal(dr[idHeSoTinChi].ToString());
                                        #endregion

                                        #region HeSoLopNgoaiGio
                                        string txtHeSoLopNgoaiGio = dr[idHeSoLopNgoaiGio].ToString();
                                        #endregion

                                        #region Chấm bài
                                        decimal txtChamBai = Convert.ToDecimal(dr[idChamBai].ToString());
                                        #endregion

                                        #region Bậc đào tạo
                                        string txtBacDaoTao = dr[idBacDaoTao].ToString();
                                        #endregion

                                        #region Ngôn ngữ
                                        string txtNgonNgu = dr[idNgonNguGiangDay].ToString();
                                        #endregion

                                        #region txtCoSoGiangDay
                                        string txtCoSoGiangDay = dr[idCoSoGiangDay].ToString();
                                        #endregion
                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if (!string.IsNullOrEmpty(txtMaGiangVien) )
                                        {
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("MaQuanLy =?", txtMaGiangVien);
                                            nhanVien = uow.FindObject<NhanVien>(fNhanVien);
                                            if (nhanVien == null)
                                            {
                                                errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                                TiepTuc = false;
                                            }
                                            else
                                            {
                                                if (nhanVien != null)
                                                {
                                                    bacDaotao = uow.FindObject<BacDaoTao>(CriteriaOperator.Parse("TenBacDaoTao=?", txtBacDaoTao));
                                                    if (bacDaotao == null)
                                                    {
                                                        errorLog.AppendLine("- STT: " + STT + " Không tồn tại bậc đào tạo.");
                                                        TiepTuc = false;
                                                    }
                                                    if (nhanVien.BoPhan != null && nhanVien.BoPhan.BoPhanCha != null)
                                                        txtCoSoGiangDay = nhanVien.BoPhan.BoPhanCha.TenBoPhan;
                                                    coSoGiangDay = uow.FindObject<CoSoGiangDay>(CriteriaOperator.Parse("TenCoSo=?", txtCoSoGiangDay));
                                                    if (coSoGiangDay == null)
                                                    {
                                                        errorLog.AppendLine("- STT: " + STT + " Không tồn tại cơ sở giảng dạy.");
                                                        TiepTuc = false;
                                                    }
                                                    if (TiepTuc)//Đủ thông tin
                                                    {
                                                        CriteriaOperator filter = CriteriaOperator.Parse("KhoiLuongGiangDay=? and NhanVien = ? and TenMonHoc=? and LopHocPhan = ? and STT = ?", qly.Oid, nhanVien.Oid, txtMaMonHoc, txtMaLopHP, STT.ToString());
                                                        XPCollection<ChiTietKhoiLuongGiangDay> dsChiTietKhoiLuongGiangDay = new XPCollection<ChiTietKhoiLuongGiangDay>(uow, filter);

                                                        if (dsChiTietKhoiLuongGiangDay.Count == 0)
                                                        {
                                                            ct = new ChiTietKhoiLuongGiangDay(uow);
                                                            ct.KhoiLuongGiangDay = qly;
                                                            if (qly.HocKy != null)
                                                                ct.HocKy = qly.HocKy.TenHocKy;
                                                            ct.NhanVien = nhanVien;
                                                            ct.LopHocPhan = txtMaLopHP;
                                                            ct.LopSinhVien = txtMaLopHP;
                                                            ct.MaNhom = txtMaNhom;
                                                            ct.MaMonHoc = txtMaMonHoc;
                                                            ct.TenMonHoc = txtTenMonHoc;
                                                            ct.SoLuongSV = txtSiSo;
                                                            ct.GhiChu = txtGhiChu;
                                                            ct.SoTietLyThuyet = txtSoTiet;
                                                            ct.HeSo_TinChi = txtHeSoTinChi;
                                                            ct.GioQuyDoiChamBaiTNTH = txtChamBai;
                                                            ct.BacDaoTao = bacDaotao;
                                                            if (txtNgonNgu == "Ngoại ngữ")
                                                                ct.NgonNguGiangDay = NgonNguEnum.ChuyenNgu;
                                                            else
                                                                ct.NgonNguGiangDay = NgonNguEnum.BinhThuong;

                                                            if (txtHeSoLopNgoaiGio == "Giờ hành chính")
                                                                ct.GioGiangDay = GioGiangDayEnum.GioHanhChinh;
                                                            else
                                                                if (txtHeSoLopNgoaiGio == "Ngoài giờ")
                                                                    ct.GioGiangDay = GioGiangDayEnum.NgoaiGio;
                                                            ct.CoSoGiangDay = coSoGiangDay;
                                                            //ct.HeSo_GiangDayNgoaiGio = txtHeSoLopNgoaiGio;
                                                        }
                                                    }
                                                    //sucessNumber++;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + "- Không tìm thấy mã nhân viên "+ txtMaGiangVien );
                                                }
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