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
    public class BoiDuong_ImportQuyetDinhBoiDuong
    {
        public static void XuLy_BUH(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:R]"))
                    {
                        ChiTietBoiDuong chiTietBoiDuong;
                        QuyetDinhBoiDuong quyetDinhBoiDuong;
                        XPCollection<QuyetDinhBoiDuong> listQuyetDinh;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            listQuyetDinh = new XPCollection<QuyetDinhBoiDuong>(uow);

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //
                                    //
                                    int soQuyetDinh = 0;
                                    int ngayQuyetDinh = 1;
                                    int ngayHieuLuc = 2;
                                    int coQuanRaQuyetDinh = 3;
                                    int nguoiKy = 4;
                                    int quocGia = 5;
                                    int loaiHinhBoiDuong = 6;
                                    int loaiChungChi = 7;
                                    int chuongTrinhBoiDuong = 8;
                                    int donViToChuc = 9;
                                    int noiBoiDuong = 10;
                                    int noiDungBoiDuong = 11;
                                    int nguonKinhPhi = 12;
                                    int soTien = 13;
                                    int tuNgay = 14;
                                    int denNgay = 15;
                                    int maQuanLy = 16;
                                    int tinhTrangHuongLuong = 17;

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
                                        String loaiHinhBoiDuongText = item[loaiHinhBoiDuong].ToString().FullTrim();
                                        String loaiChungChiText = item[loaiChungChi].ToString().FullTrim();
                                        String chuongTrinhBoiDuongText = item[chuongTrinhBoiDuong].ToString().FullTrim();
                                        String donViToChucText = item[donViToChuc].ToString().FullTrim();
                                        String noiBoiDuongText = item[noiBoiDuong].ToString().FullTrim();
                                        String noiDungBoiDuongText = item[noiDungBoiDuong].ToString().FullTrim();
                                        String nguonKinhPhiText = item[nguonKinhPhi].ToString().FullTrim();
                                        String soTienText = item[soTien].ToString().FullTrim();
                                        String tuNgayText = item[tuNgay].ToString().FullTrim();
                                        String denNgayText = item[denNgay].ToString().FullTrim();
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();
                                        String tinhTrangHuongLuongText = item[tinhTrangHuongLuong].ToString().FullTrim();

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
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
                                                        chiTietBoiDuong = new ChiTietBoiDuong(uow);

                                                        chiTietBoiDuong.ThongTinNhanVien = nhanVien;
                                                        chiTietBoiDuong.BoPhan = nhanVien.BoPhan;

                                                        #region Tình trạng hưởng lương
                                                        if (!string.IsNullOrEmpty(tinhTrangHuongLuongText))
                                                        {
                                                            TinhTrang TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang =?", tinhTrangHuongLuongText));
                                                            if (TinhTrang != null)
                                                            {
                                                                chiTietBoiDuong.TinhTrang = TinhTrang;
                                                            }
                                                            else
                                                                detailLog.AppendLine("Tình trạng hưởng lương không tồn tại.");
                                                        }
                                                        #endregion

                                                        listQuyetDinh[0].ListChiTietBoiDuong.Add(chiTietBoiDuong);
                                                    }
                                                    else //Tạo quyết định mới
                                                    {
                                                        quyetDinhBoiDuong = new QuyetDinhBoiDuong(uow);

                                                        #region Số quyết định
                                                        if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                        {
                                                            QuyetDinh.QuyetDinh quyetDinh = uow.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                            if (quyetDinh == null)
                                                                quyetDinhBoiDuong.SoQuyetDinh = soQuyetDinhText;
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
                                                                quyetDinhBoiDuong.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
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
                                                                quyetDinhBoiDuong.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
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
                                                            quyetDinhBoiDuong.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                            quyetDinhBoiDuong.TenCoQuan = coQuanRaQuyetDinhText;

                                                            if (!string.IsNullOrEmpty(nguoiKyText))
                                                            {
                                                                quyetDinhBoiDuong.NguoiKy1 = nguoiKyText;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            quyetDinhBoiDuong.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                        }
                                                        #endregion

                                                        #region Quốc gia
                                                        if (!string.IsNullOrEmpty(quocGiaText))
                                                        {
                                                            QuocGia QuocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia =?", quocGiaText));
                                                            if (QuocGia != null)
                                                                quyetDinhBoiDuong.QuocGia = QuocGia;
                                                            else
                                                                detailLog.AppendLine("Quốc gia không hợp lệ: " + quocGiaText);
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Quốc gia không tìm thấy.");
                                                        }
                                                        #endregion

                                                        #region Loại hình bồi dưỡng
                                                        if (!string.IsNullOrEmpty(loaiHinhBoiDuongText))
                                                        {
                                                            LoaiHinhBoiDuong LoaiHinhBoiDuong = uow.FindObject<LoaiHinhBoiDuong>(CriteriaOperator.Parse("TenLoaiHinhBoiDuong =?", loaiHinhBoiDuongText));
                                                            if (LoaiHinhBoiDuong == null)
                                                            {
                                                                LoaiHinhBoiDuong = new LoaiHinhBoiDuong(uow);
                                                                LoaiHinhBoiDuong.MaQuanLy = HamDungChung.TaoChuVietTat(loaiHinhBoiDuongText);
                                                                LoaiHinhBoiDuong.TenLoaiHinhBoiDuong = loaiHinhBoiDuongText;
                                                            }
                                                            quyetDinhBoiDuong.LoaiHinhBoiDuong = LoaiHinhBoiDuong;
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Loại hình bồi dưỡng không tìm thấy.");
                                                        }
                                                        #endregion

                                                        #region Loại chứng chỉ
                                                        if (!string.IsNullOrEmpty(loaiChungChiText))
                                                        {
                                                            LoaiChungChi LoaiChungChi = uow.FindObject<LoaiChungChi>(CriteriaOperator.Parse("TenChungChi =?", loaiChungChiText));
                                                            if (LoaiChungChi == null)
                                                            {
                                                                LoaiChungChi = new LoaiChungChi(uow);
                                                                LoaiChungChi.MaQuanLy = HamDungChung.TaoChuVietTat(loaiChungChiText);
                                                                LoaiChungChi.TenChungChi = loaiChungChiText;
                                                            }
                                                            quyetDinhBoiDuong.ChungChi = LoaiChungChi;
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Loại chứng chỉ không tìm thấy.");
                                                        }
                                                        #endregion
                                                        
                                                        #region Chương trình bồi dưỡng
                                                        if (!string.IsNullOrEmpty(chuongTrinhBoiDuongText))
                                                        {
                                                            ChuongTrinhBoiDuong ChuongTrinhBoiDuong = uow.FindObject<ChuongTrinhBoiDuong>(CriteriaOperator.Parse("TenChuongTrinhBoiDuong =?", chuongTrinhBoiDuongText));
                                                            if (ChuongTrinhBoiDuong == null)
                                                            {
                                                                ChuongTrinhBoiDuong = new ChuongTrinhBoiDuong(uow);
                                                                ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong = chuongTrinhBoiDuongText;
                                                                if (!string.IsNullOrEmpty(noiBoiDuongText))
                                                                    ChuongTrinhBoiDuong.DiaDiem = noiBoiDuongText;
                                                                if (!string.IsNullOrEmpty(donViToChucText))
                                                                    ChuongTrinhBoiDuong.DonViToChuc = donViToChucText;
                                                            }
                                                            quyetDinhBoiDuong.ChuongTrinhBoiDuong = ChuongTrinhBoiDuong;
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine("Chương trình bồi dưỡng không tìm thấy.");
                                                        }
                                                        #endregion

                                                        #region Nội dung bồi dưỡng
                                                        if (!string.IsNullOrEmpty(noiDungBoiDuongText))
                                                        {
                                                            quyetDinhBoiDuong.NoiDungBoiDuong = noiDungBoiDuongText;
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
                                                            quyetDinhBoiDuong.NguonKinhPhi = NguonKinhPhi;
                                                        }
                                                        #endregion

                                                        #region Số tiền
                                                        if (!string.IsNullOrEmpty(soTienText))
                                                        {
                                                            quyetDinhBoiDuong.TruongHoTro = soTienText;
                                                        }
                                                        #endregion

                                                        #region Từ ngày
                                                        if (!string.IsNullOrEmpty(tuNgayText))
                                                        {
                                                            try
                                                            {
                                                                quyetDinhBoiDuong.TuNgay = Convert.ToDateTime(tuNgayText);
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
                                                                quyetDinhBoiDuong.DenNgay = Convert.ToDateTime(denNgayText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                                            }
                                                        }
                                                        #endregion

                                                        //Thêm chi tiết đào tạo
                                                        chiTietBoiDuong = new ChiTietBoiDuong(uow);

                                                        chiTietBoiDuong.QuyetDinhBoiDuong = quyetDinhBoiDuong;
                                                        chiTietBoiDuong.ThongTinNhanVien = nhanVien;
                                                        chiTietBoiDuong.BoPhan = nhanVien.BoPhan;

                                                        #region Tình trạng hưởng lương
                                                        if (!string.IsNullOrEmpty(tinhTrangHuongLuongText))
                                                        {
                                                            TinhTrang TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang =?", tinhTrangHuongLuongText));
                                                            if (TinhTrang != null)
                                                            {
                                                                chiTietBoiDuong.TinhTrang = TinhTrang;
                                                            }
                                                            else
                                                                detailLog.AppendLine("Tình trạng hưởng lương không tồn tại.");
                                                        }
                                                        #endregion

                                                        listQuyetDinh.Add(quyetDinhBoiDuong);
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
