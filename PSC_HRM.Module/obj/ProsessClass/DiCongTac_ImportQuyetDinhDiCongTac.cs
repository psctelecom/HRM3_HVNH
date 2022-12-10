using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public class DiCongTac_ImportQuyetDinhDiCongTac
    {
        public static void XuLy(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:S]"))
                    {
                        ChiTietQuyetDinhDiCongTac chiTietQuyetDinhDiCongTac;
                        QuyetDinhDiCongTac quyetDinhDiCongTac;
                        XPCollection<QuyetDinhDiCongTac> listQuyetDinh;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            listQuyetDinh = new XPCollection<QuyetDinhDiCongTac>(uow);

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //
                                    int soQuyetDinh = 0;
                                    int ngayQuyetDinh = 1;
                                    int ngayHieuLuc = 2;
                                    int coQuanRaQuyetDinh = 3;
                                    int nguoiKy = 4;
                                    int quocGia = 5;
                                    int tuNgay = 6;
                                    int denNgay = 7;
                                    int tuNgayTT = 8;
                                    int denNgayTT = 9;
                                    int ghiChuTG = 10;
                                    int nguonKinhPhi = 11;                                    
                                    int donViToChuc = 12;
                                    int diaDiem = 13;
                                    int lyDo = 14;                                    
                                    int maQuanLy = 15;
                                    int viTriCongTac = 16;
                                 
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                        String coQuanRaQuyetDinhText = item[coQuanRaQuyetDinh].ToString().FullTrim();
                                        String nguoiKyText = item[nguoiKy].ToString().FullTrim();
                                        String quocGiaText = item[quocGia].ToString().FullTrim();
                                        String tuNgayText = item[tuNgay].ToString().FullTrim();
                                        String denNgayText = item[denNgay].ToString().FullTrim();
                                        String tuNgayTTText = item[tuNgayTT].ToString().FullTrim();
                                        String denNgayTTText = item[denNgayTT].ToString().FullTrim();
                                        String ghiChuThoiGianText = item[nguonKinhPhi].ToString().FullTrim();
                                        String nguonKinhPhiText = item[ghiChuTG].ToString().FullTrim();
                                        String donViToChucText = item[donViToChuc].ToString().FullTrim();                                        
                                        String diaDiemText = item[diaDiem].ToString().FullTrim();
                                        String lyDoText = item[lyDo].ToString().FullTrim();                                       
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();
                                        String viTriCongTacText = item[viTriCongTac].ToString().FullTrim();                                        

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=?", maQuanLyText, maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            #region Số quyết định
                                            if (!string.IsNullOrEmpty(soQuyetDinhText))
                                            {
                                                QuyetDinh.QuyetDinh QuyetDinh = uow.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                if (QuyetDinh == null)
                                                {
                                                    listQuyetDinh.Filter = CriteriaOperator.Parse("SoQuyetDinh = ?", soQuyetDinhText);

                                                    if (listQuyetDinh.Count > 0) //Đã tạo quyết định chỉ thêm chi tiết
                                                    {
                                                        chiTietQuyetDinhDiCongTac = new ChiTietQuyetDinhDiCongTac(uow);

                                                        chiTietQuyetDinhDiCongTac.ThongTinNhanVien = nhanVien;
                                                        chiTietQuyetDinhDiCongTac.BoPhan = nhanVien.BoPhan;

                                                        #region Vị trí công tác
                                                        if (!string.IsNullOrEmpty(viTriCongTacText))
                                                        {
                                                            ViTriCongTac _viTriCongTac = uow.FindObject<ViTriCongTac>(CriteriaOperator.Parse("TenViTriCongTac like ?", viTriCongTacText));
                                                            if (_viTriCongTac != null)
                                                            {
                                                                chiTietQuyetDinhDiCongTac.ViTriCongTac = _viTriCongTac;
                                                            }
                                                            else
                                                                detailLog.AppendLine("Vị trí công tác không tồn tại.");
                                                        }
                                                        #endregion

                                                        listQuyetDinh[0].ListChiTietQuyetDinhDiCongTac.Add(chiTietQuyetDinhDiCongTac);
                                                    }
                                                    else //Tạo quyết định mới
                                                    {
                                                        quyetDinhDiCongTac = new QuyetDinhDiCongTac(uow);

                                                        #region Số quyết định
                                                        if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                        {
                                                            QuyetDinh.QuyetDinh quyetDinh = uow.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                            if (quyetDinh == null)
                                                                quyetDinhDiCongTac.SoQuyetDinh = soQuyetDinhText;
                                                            else
                                                                detailLog.AppendLine("Số quyết định đã tồn tại: " + soQuyetDinhText);
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                        }
                                                        #endregion

                                                        #region Ngày quyết định
                                                        if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhDiCongTac.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Ngày quyết định chưa có dữ liệu");
                                                        }
                                                        #endregion

                                                        #region Ngày hiệu lực
                                                        if (!string.IsNullOrEmpty(ngayHieuLucText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhDiCongTac.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Ngày hiệu lực không hợp lệ: " + ngayHieuLucText);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Ngày hiệu lực chưa có dữ liệu");
                                                        }
                                                        #endregion

                                                        #region Cơ quan ra quyết định - Nguời ký
                                                        if (!string.IsNullOrEmpty(coQuanRaQuyetDinhText))
                                                        {
                                                            quyetDinhDiCongTac.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                            quyetDinhDiCongTac.TenCoQuan = coQuanRaQuyetDinhText;

                                                            if (!string.IsNullOrEmpty(nguoiKyText))
                                                            {
                                                                quyetDinhDiCongTac.NguoiKy1 = nguoiKyText;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            quyetDinhDiCongTac.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                            quyetDinhDiCongTac.TenCoQuan = HamDungChung.ThongTinTruong(uow).TenBoPhan;
                                                        }
                                                        #endregion

                                                        #region Quốc gia
                                                        if (!string.IsNullOrEmpty(quocGiaText))
                                                        {
                                                            QuocGia QuocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia =?", quocGiaText));
                                                            if (QuocGia != null)
                                                                quyetDinhDiCongTac.QuocGia = QuocGia;
                                                            else
                                                                detailLog.AppendLine("Quốc gia không hợp lệ: " + quocGiaText);
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Quốc gia không tìm thấy.");
                                                        }
                                                        #endregion

                                                        #region Từ ngày
                                                        if (!string.IsNullOrEmpty(tuNgayText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhDiCongTac.TuNgay = Convert.ToDateTime(tuNgayText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgayText);
                                                            }
                                                        }
                                                        #endregion

                                                        #region Đến ngày
                                                        if (!string.IsNullOrEmpty(denNgayText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhDiCongTac.DenNgay = Convert.ToDateTime(denNgayText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                                            }
                                                        }
                                                        #endregion

                                                        #region Từ ngày thực tế
                                                        if (!string.IsNullOrEmpty(tuNgayTTText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhDiCongTac.TuNgayTT = Convert.ToDateTime(tuNgayTTText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Từ ngày thực tế không hợp lệ: " + tuNgayTTText);
                                                            }
                                                        }
                                                        #endregion

                                                        #region Đến ngày thực tế
                                                        if (!string.IsNullOrEmpty(denNgayTTText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhDiCongTac.DenNgayTT = Convert.ToDateTime(denNgayTTText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày thực tế không hợp lệ: " + denNgayTTText);
                                                            }
                                                        }
                                                        #endregion

                                                        #region Ghi chú thời gian
                                                        if (!string.IsNullOrEmpty(ghiChuThoiGianText))
                                                        {
                                                            quyetDinhDiCongTac.GhiChuTG = ghiChuThoiGianText;
                                                        }
                                                        #endregion

                                                        #region Nguồn kinh phí
                                                        if (!string.IsNullOrEmpty(nguonKinhPhiText))
                                                        {
                                                            NguonKinhPhi NguonKinhPhi = uow.FindObject<NguonKinhPhi>(CriteriaOperator.Parse("TenNguonKinhPhi =?", nguonKinhPhiText));
                                                            if (NguonKinhPhi == null)
                                                            {
                                                                NguonKinhPhi = new NguonKinhPhi(uow);
                                                                NguonKinhPhi.MaQuanLy = HamDungChung.TaoChuVietTat(nguonKinhPhiText);
                                                                NguonKinhPhi.TenNguonKinhPhi = nguonKinhPhiText;
                                                            }
                                                            quyetDinhDiCongTac.NguonKinhPhi = NguonKinhPhi;
                                                        }
                                                        #endregion

                                                        #region Đơn vị tổ chức
                                                        if (!string.IsNullOrEmpty(donViToChucText))
                                                        {
                                                            quyetDinhDiCongTac.DonViToChuc = donViToChucText;
                                                        }
                                                        #endregion

                                                        #region Địa điểm
                                                        if (!string.IsNullOrEmpty(diaDiemText))
                                                        {
                                                            quyetDinhDiCongTac.DiaDiem = diaDiemText;
                                                        }
                                                        #endregion

                                                        #region Lý do
                                                        if (!string.IsNullOrEmpty(lyDoText))
                                                        {
                                                            quyetDinhDiCongTac.LyDo = lyDoText;
                                                        }
                                                        #endregion

                                                        //Thêm chi tiết đào tạo
                                                        chiTietQuyetDinhDiCongTac = new ChiTietQuyetDinhDiCongTac(uow);

                                                        chiTietQuyetDinhDiCongTac.QuyetDinhDiCongTac = quyetDinhDiCongTac;
                                                        chiTietQuyetDinhDiCongTac.ThongTinNhanVien = nhanVien;
                                                        chiTietQuyetDinhDiCongTac.BoPhan = nhanVien.BoPhan;

                                                        #region Vị trí công tác
                                                        if (!string.IsNullOrEmpty(viTriCongTacText))
                                                        {
                                                            ViTriCongTac _viTriCongTac = uow.FindObject<ViTriCongTac>(CriteriaOperator.Parse("TenViTriCongTac like ?", viTriCongTacText));
                                                            if (_viTriCongTac != null)
                                                            {
                                                                chiTietQuyetDinhDiCongTac.ViTriCongTac = _viTriCongTac;
                                                            }
                                                            else
                                                                detailLog.AppendLine("Vị trí công tác không tồn tại.");
                                                        }
                                                        #endregion

                                                        listQuyetDinh.Add(quyetDinhDiCongTac);
                                                    }

                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Số quyết định đã tồn tại");
                                                }

                                            }
                                            else
                                            {
                                                detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                            }
                                            #endregion


                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
                                            }
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã nhân sự (Số hiệu công chức) là: {0}", maQuanLy));
                                        }
                                    }
                                }
                            }

                            //
                            if (mainLog.Length > 0)
                            {
                                uow.RollbackTransaction();
                                if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                                {
                                    string tenFile = "Import_Log.txt";
                                    StreamWriter writer = new StreamWriter(tenFile);
                                    writer.WriteLine(mainLog.ToString());
                                    writer.Flush();
                                    writer.Close();
                                    writer.Dispose();
                                    HamDungChung.WriteLog(tenFile, mainLog.ToString());
                                    System.Diagnostics.Process.Start(tenFile);
                                }
                            }
                            else
                            {
                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                uow.CommitChanges();
                                //hoàn tất giao tác
                                DialogUtil.ShowSaveSuccessful("Import Thành Công!");
                            }

                        }
                    }
                }
            }
        }
    }
}
