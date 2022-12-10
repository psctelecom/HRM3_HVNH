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
    public class Imp_KhoaLuanTotNghiep
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

                        using (DataTable dt = DataProvider.GetDataTable(open.FileName, "[TOTNGHIEP$A6:AO]"))
                        {
                            /////////////////////////////KHỞI TẠO CÁC BIẾN LƯU DỮ LIỆU/////////////////////////////////////////////////////
                            /////////////////////////////KHỞI TẠO CÁC IDX/////////////////////////////////////////////////////

                            #region Khởi tạo các Idx

                            const int idMaNhanVien = 1;
                            const int idHoTen = 2;
                            const int id10KTXD = 3;
                            const int id20KTXD = 4;
                            const int id30KTXD = 5;
                            const int id40KTXD = 6;
                            const int id50KTXD = 7;
                            const int id60KTXD = 8;
                            const int id70KTXD = 9;
                            const int id80KTXD = 10;
                            const int id90KTXD = 11;
                            const int id100KTXD = 12;
                            const int idNganhKhac = 13;
                            const int idPhanBien = 14;
                            const int idChamBai = 15;
                            const int idHDTTTH = 16;
                            const int idChamBaiTTTH = 17;
                            const int idHDTTTN = 18;
                            const int idChamBaiTTTN = 19;
                            const int idQD10KTXD = 20;
                            const int idQD20KTXD = 21;
                            const int idQD30KTXD = 22;
                            const int idQD40KTXD = 23;
                            const int idQD50KTXD = 24;
                            const int idQD60KTXD = 25;
                            const int idQD70KTXD = 26;
                            const int idQD80KTXD = 27;
                            const int idQD90KTXD = 28;
                            const int idQD100KTXD = 29;
                            const int idQDNganhKhac = 30;
                            const int idQDPhanBien = 31;
                            const int idQDChamBai = 32;
                            const int idQDHDTTTH = 33;
                            const int idQDChamBaiTTTH = 34;
                            const int idQDHDTTTN = 35;
                            const int idQDChamBaiTTTN = 36;
                            const int idTongGio = 38;
                            const int idMaKhoa = 40;



                            #endregion

                            /////////////////////////////TIẾN HÀNH ĐỌC DỮ LIỆU TỪ EXCEL///////////////////////////////////////

                            using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                //
                                uow.BeginTransaction();
                                #region Khai báo
                                KhoiLuongGiangDay qly = uow.GetObjectByKey<KhoiLuongGiangDay>(OidQuanLy.Oid);
                                psc_UIS_GiangVien nhanVien = null;
                                KhoaLuanTotNghiep ct;
                                int STT = 0;
                                #endregion
                                //Duyệt qua tất cả các dòng trong file excel
                                if (qly != null)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        STT++;
                                        int KTXD10 = 0;
                                        int KTXD20 = 0;
                                        int KTXD30 = 0;
                                        int KTXD40 = 0;
                                        int KTXD50 = 0;
                                        int KTXD60 = 0;
                                        int KTXD70 = 0;
                                        int KTXD80 = 0;
                                        int KTXD90 = 0;
                                        int KTXD100 = 0;
                                        int NganhKhac = 0;
                                        int PhanBien = 0;
                                        int ChamBai = 0;
                                        int HDTTTH = 0;
                                        int ChamBaiTTTH = 0;
                                        int HDTTTN = 0;
                                        int ChamBaiTTTN = 0;
                                        decimal QD10KTXD = 0;
                                        decimal QD20KTXD = 0;
                                        decimal QD30KTXD = 0;
                                        decimal QD40KTXD = 0;
                                        decimal QD50KTXD = 0;
                                        decimal QD60KTXD = 0;
                                        decimal QD70KTXD = 0;
                                        decimal QD80KTXD = 0;
                                        decimal QD90KTXD = 0;
                                        decimal QD100KTXD = 0;
                                        decimal QDNganhKhac = 0;
                                        decimal QDPhanBien = 0;
                                        decimal QDChamBai = 0;
                                        decimal QDHDTTTH = 0;
                                        decimal QDChamBaiTTTH = 0;
                                        decimal QDHDTTTN = 0;
                                        decimal QDChamBaiTTTN = 0;
                                        decimal TongGio = 0;
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

                                        #region Hướng dẫn TTTH
                                        if (dr[idHDTTTH].ToString() != string.Empty)
                                        {
                                            HDTTTH = Convert.ToInt32(dr[idHDTTTH].ToString());
                                        }
                                        #endregion

                                        #region Hướng dẫn TTTN
                                        if (dr[idHDTTTN].ToString() != string.Empty)
                                        {
                                            HDTTTN = Convert.ToInt32(dr[idHDTTTN].ToString());
                                        }
                                        #endregion

                                        #region Chấm báo cáo TTTH
                                        if (dr[idChamBaiTTTH].ToString() != string.Empty)
                                        {
                                            ChamBaiTTTH = Convert.ToInt32(dr[idChamBaiTTTH].ToString());
                                        }
                                        #endregion

                                        #region Chấm chuyên để TTTN
                                        if (dr[idChamBaiTTTN].ToString() != string.Empty)
                                        {
                                            ChamBaiTTTN = Convert.ToInt32(dr[idChamBaiTTTN].ToString());
                                        }
                                        #endregion

                                        #region Ngành khác
                                        if (dr[idNganhKhac].ToString() != string.Empty)
                                        {
                                            NganhKhac = Convert.ToInt32(dr[idNganhKhac].ToString());
                                        }
                                        #endregion
                                        #region Phản biện
                                        if (dr[idPhanBien].ToString() != string.Empty)
                                        {
                                            PhanBien = Convert.ToInt32(dr[idPhanBien].ToString());
                                        }
                                        #endregion
                                        #region Chấm bài
                                        if (dr[idChamBai].ToString() != string.Empty)
                                        {
                                            ChamBai = Convert.ToInt32(dr[idChamBai].ToString());
                                        }
                                        #endregion

                                        #region SLSV KTXD
                                        if (dr[id10KTXD].ToString() != string.Empty)
                                        {
                                            KTXD10 = Convert.ToInt32(dr[id10KTXD].ToString());
                                        }
                                        if (dr[id20KTXD].ToString() != string.Empty)
                                        {
                                            KTXD20 = Convert.ToInt32(dr[id20KTXD].ToString());
                                        }
                                        if (dr[id30KTXD].ToString() != string.Empty)
                                        {
                                            KTXD30 = Convert.ToInt32(dr[id30KTXD].ToString());
                                        }
                                        if (dr[id40KTXD].ToString() != string.Empty)
                                        {
                                            KTXD40 = Convert.ToInt32(dr[id40KTXD].ToString());
                                        }
                                        if (dr[id50KTXD].ToString() != string.Empty)
                                        {
                                            KTXD50 = Convert.ToInt32(dr[id50KTXD].ToString());
                                        }
                                        if (dr[id60KTXD].ToString() != string.Empty)
                                        {
                                            KTXD60 = Convert.ToInt32(dr[id60KTXD].ToString());
                                        }
                                        if (dr[id70KTXD].ToString() != string.Empty)
                                        {
                                            KTXD70 = Convert.ToInt32(dr[id70KTXD].ToString());
                                        }
                                        if (dr[id80KTXD].ToString() != string.Empty)
                                        {
                                            KTXD80 = Convert.ToInt32(dr[id80KTXD].ToString());
                                        }
                                        if (dr[id90KTXD].ToString() != string.Empty)
                                        {
                                            KTXD90 = Convert.ToInt32(dr[id90KTXD].ToString());
                                        }
                                        if (dr[id100KTXD].ToString() != string.Empty)
                                        {
                                            KTXD100 = Convert.ToInt32(dr[id100KTXD].ToString());
                                        }
                                        #endregion

                                        #region QD SLSV KTXD
                                        if (dr[idQD10KTXD].ToString() != string.Empty)
                                        {
                                            QD10KTXD = Convert.ToDecimal(dr[idQD10KTXD].ToString());
                                        }
                                        if (dr[idQD20KTXD].ToString() != string.Empty)
                                        {
                                            QD20KTXD = Convert.ToDecimal(dr[idQD20KTXD].ToString());
                                        }
                                        if (dr[idQD30KTXD].ToString() != string.Empty)
                                        {
                                            QD30KTXD = Convert.ToDecimal(dr[idQD30KTXD].ToString());
                                        }
                                        if (dr[idQD40KTXD].ToString() != string.Empty)
                                        {
                                            QD40KTXD = Convert.ToDecimal(dr[idQD40KTXD].ToString());
                                        }
                                        if (dr[idQD50KTXD].ToString() != string.Empty)
                                        {
                                            QD50KTXD = Convert.ToDecimal(dr[idQD50KTXD].ToString());
                                        }
                                        if (dr[idQD60KTXD].ToString() != string.Empty)
                                        {
                                            QD60KTXD = Convert.ToDecimal(dr[idQD60KTXD].ToString());
                                        }
                                        if (dr[idQD70KTXD].ToString() != string.Empty)
                                        {
                                            QD70KTXD = Convert.ToDecimal(dr[idQD70KTXD].ToString());
                                        }
                                        if (dr[idQD80KTXD].ToString() != string.Empty)
                                        {
                                            QD80KTXD = Convert.ToDecimal(dr[idQD80KTXD].ToString());
                                        }
                                        if (dr[idQD90KTXD].ToString() != string.Empty)
                                        {
                                            QD90KTXD = Convert.ToDecimal(dr[idQD90KTXD].ToString());
                                        }
                                        if (dr[idQD100KTXD].ToString() != string.Empty)
                                        {
                                            QD100KTXD = Convert.ToDecimal(dr[idQD100KTXD].ToString());
                                        }
                                        #endregion

                                        #region QD Ngành khác
                                        if (dr[idQDNganhKhac].ToString() != string.Empty)
                                        {
                                            QDNganhKhac = Convert.ToDecimal(dr[idQDNganhKhac].ToString());
                                        }
                                        #endregion
                                        #region QD Phản biện
                                        if (dr[idQDPhanBien].ToString() != string.Empty)
                                        {
                                            QDPhanBien = Convert.ToDecimal(dr[idQDPhanBien].ToString());
                                        }
                                        #endregion
                                        #region QD Chấm bài
                                        if (dr[idQDChamBai].ToString() != string.Empty)
                                        {
                                            QDChamBai = Convert.ToDecimal(dr[idQDChamBai].ToString());
                                        }
                                        #endregion

                                        #region QĐ Hướng dẫn TTTH
                                        if (dr[idQDHDTTTH].ToString() != string.Empty)
                                        {
                                            QDHDTTTH = Convert.ToDecimal(dr[idQDHDTTTH].ToString());
                                        }
                                        #endregion

                                        #region QĐ Hướng dẫn TTTN
                                        if (dr[idQDHDTTTN].ToString() != string.Empty)
                                        {
                                            QDHDTTTN = Convert.ToDecimal(dr[idQDHDTTTN].ToString());
                                        }
                                        #endregion

                                        #region QĐ Chấm báo cáo TTTH
                                        if (dr[idQDChamBaiTTTH].ToString() != string.Empty)
                                        {
                                            QDChamBaiTTTH = Convert.ToDecimal(dr[idQDChamBaiTTTH].ToString());
                                        }
                                        #endregion

                                        #region QĐ Chấm chuyên để TTTN
                                        if (dr[idQDChamBaiTTTN].ToString() != string.Empty)
                                        {
                                            QDChamBaiTTTN = Convert.ToDecimal(dr[idQDChamBaiTTTN].ToString());
                                        }
                                        #endregion

                                        #region Tổng giờ
                                        if (dr[idTongGio].ToString() != string.Empty)
                                        {
                                            TongGio = Convert.ToDecimal(dr[idTongGio].ToString());
                                        }
                                        #endregion

                                        #region Mã bộ phận
                                        string txtMaBoPhan = dr[idMaKhoa].ToString();
                                        #endregion

                                        //
                                        #endregion
                                        //////////////////////////KIỂM TRA VÀ LẤY DỮ LIỆU//////////////////////////
                                        #region Kiểm tra dữ liệu
                                        if ((!string.IsNullOrEmpty(txtHoTen) || !string.IsNullOrEmpty(txtMaNhanVien)))
                                        {
                                            NhanVien NV = null;
                                            CriteriaOperator fNV;
                                            CriteriaOperator fNhanVien = CriteriaOperator.Parse("ProfessorID =?", txtMaNhanVien);
                                            nhanVien = uow.FindObject<psc_UIS_GiangVien>(fNhanVien);
                                            if (nhanVien == null)
                                            {
                                                fNV = CriteriaOperator.Parse("MaQuanLy =?", txtMaNhanVien);
                                                NV = uow.FindObject<NhanVien>(fNV);
                                                if (NV == null)
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + " Không tồn tại nhân viên.");
                                                }
                                            }
                                            else
                                            {
                                                if (nhanVien.OidNhanVien != Guid.Empty)
                                                {
                                                    fNV = CriteriaOperator.Parse("Oid =?", nhanVien.OidNhanVien);
                                                    NV = uow.FindObject<NhanVien>(fNV);
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine("- STT: " + STT + "- Nhân viên " + txtMaNhanVien + " - " + txtHoTen + " chưa kết nối HRM.");
                                                }
                                            }
                                            if (NV != null)
                                            {
                                                CriteriaOperator filter = CriteriaOperator.Parse("NhanVien = ? and KhoiLuongGiangDay = ?", NV.Oid, qly.Oid);
                                                XPCollection<KhoaLuanTotNghiep> dsKhoaLuanTotNhiep = new XPCollection<KhoaLuanTotNghiep>(uow, filter);
                                                if (dsKhoaLuanTotNhiep.Count == 0)
                                                {
                                                    ct = new KhoaLuanTotNghiep(uow);
                                                    ct.NhanVien = uow.GetObjectByKey<NhanVien>(NV.Oid);
                                                    ct.KhoiLuongGiangDay = qly;
                                                    ct.KhoaQuanLy = NV.BoPhan;
                                                    ct.KTXD10 = KTXD10;
                                                    ct.KTXD20 = KTXD20;
                                                    ct.KTXD30 = KTXD30;
                                                    ct.KTXD40 = KTXD40;
                                                    ct.KTXD50 = KTXD50;
                                                    ct.KTXD60 = KTXD60;
                                                    ct.KTXD70 = KTXD70;
                                                    ct.KTXD80 = KTXD80;
                                                    ct.KTXD90 = KTXD90;
                                                    ct.KTXD100 = KTXD100;
                                                    ct.NganhKhac = NganhKhac;
                                                    ct.PhanBien = PhanBien;
                                                    ct.ChamBai = ChamBai;
                                                    ct.SLSVHuongDanThucTapTongHop = HDTTTH;
                                                    ct.SLSVHuongDanThucTapTotNghiep = HDTTTN;
                                                    ct.SLSVChamBaoCaoThucTapTongHop = ChamBaiTTTH;
                                                    ct.SLSVChamChuyuenDeThucTapTotNghiep = ChamBaiTTTN;
                                                    //
                                                    ct.QD10KTXD = QD10KTXD;
                                                    ct.QD20KTXD = QD20KTXD;
                                                    ct.QD30KTXD = QD30KTXD;
                                                    ct.QD40KTXD = QD40KTXD;
                                                    ct.QD50KTXD = QD50KTXD;
                                                    ct.QD60KTXD = QD60KTXD;
                                                    ct.QD70KTXD = QD70KTXD;
                                                    ct.QD80KTXD = QD80KTXD;
                                                    ct.QD90KTXD = QD90KTXD;
                                                    ct.QD100KTXD = QD100KTXD;
                                                    ct.QDChamBai = QDChamBai;
                                                    ct.QDNganhKhac = QDNganhKhac;
                                                    ct.QDPhanBien = QDPhanBien;
                                                    ct.QDSVChamBaoCaoThucTapTongHop = QDChamBaiTTTH;
                                                    ct.QDSVChamChuyuenDeThucTapTotNghiep = QDChamBaiTTTN;
                                                    ct.QDSVHuongDanThucTapTongHop = QDHDTTTH;
                                                    ct.QDSVHuongDanThucTapTotNghiep = QDHDTTTN;
                                                    //
                                                    ct.TongGio = TongGio;
                                                }
                                                //sucessNumber++;
                                            }
                                            else
                                            {
                                                errorLog.AppendLine("- STT: " + STT + "- Nhân viên " + txtMaNhanVien + " - " + txtHoTen + " chưa kết nối HRM.");
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