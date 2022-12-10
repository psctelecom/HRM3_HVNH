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
    public class DiNuocNgoai_ImportQuyetDinhDiNuocNgoai
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
                        ChiTietQuyetDinhDiNuocNgoai chiTietQuyetDinhDiNuocNgoai;
                        QuyetDinhDiNuocNgoai quyetDinhDiNuocNgoai;
                        XPCollection<QuyetDinhDiNuocNgoai> listQuyetDinh;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            listQuyetDinh = new XPCollection<QuyetDinhDiNuocNgoai>(uow);

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
                                    int ghiChuTG = 8;
                                    int nguonKinhPhi = 9;                                    
                                    int donViToChuc = 10;
                                    int diaDiem = 11;
                                    int lyDo = 12;
                                    int ghiChu = 13;
                                    int maQuanLy = 14;
                                    int tinhTrangHuongLuong = 15;
                                    int quyetDinhCu = 16;

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
                                        String ghiChuThoiGianText = item[nguonKinhPhi].ToString().FullTrim();
                                        String nguonKinhPhiText = item[ghiChuTG].ToString().FullTrim();
                                        String donViToChucText = item[donViToChuc].ToString().FullTrim();                                        
                                        String diaDiemText = item[diaDiem].ToString().FullTrim();
                                        String lyDoText = item[lyDo].ToString().FullTrim();
                                        String ghiChuText = item[ghiChu].ToString().FullTrim();
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();
                                        String tinhTrangHuongLuongText = item[tinhTrangHuongLuong].ToString().FullTrim();
                                        String quyetDinhCuText = item[quyetDinhCu].ToString().FullTrim();

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
                                                        chiTietQuyetDinhDiNuocNgoai = new ChiTietQuyetDinhDiNuocNgoai(uow);

                                                        chiTietQuyetDinhDiNuocNgoai.ThongTinNhanVien = nhanVien;
                                                        chiTietQuyetDinhDiNuocNgoai.BoPhan = nhanVien.BoPhan;

                                                        #region Tình trạng hưởng lương
                                                        if (!string.IsNullOrEmpty(tinhTrangHuongLuongText))
                                                        {
                                                            TinhTrang TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang =?", tinhTrangHuongLuongText));
                                                            if (TinhTrang != null)
                                                            {
                                                                chiTietQuyetDinhDiNuocNgoai.TinhTrangMoi = TinhTrang;
                                                            }
                                                            else
                                                                detailLog.AppendLine("Tình trạng hưởng lương không tồn tại.");
                                                        }
                                                        #endregion

                                                        listQuyetDinh[0].ListChiTietQuyetDinhDiNuocNgoai.Add(chiTietQuyetDinhDiNuocNgoai);
                                                    }
                                                    else //Tạo quyết định mới
                                                    {
                                                        quyetDinhDiNuocNgoai = new QuyetDinhDiNuocNgoai(uow);

                                                        #region Số quyết định
                                                        if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                        {
                                                            QuyetDinh.QuyetDinh quyetDinh = uow.FindObject<QuyetDinh.QuyetDinh>(CriteriaOperator.Parse("SoQuyetDinh =?", soQuyetDinhText));
                                                            if (quyetDinh == null)
                                                                quyetDinhDiNuocNgoai.SoQuyetDinh = soQuyetDinhText;
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
                                                                quyetDinhDiNuocNgoai.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
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
                                                                quyetDinhDiNuocNgoai.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
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
                                                            quyetDinhDiNuocNgoai.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                            quyetDinhDiNuocNgoai.TenCoQuan = coQuanRaQuyetDinhText;

                                                            if (!string.IsNullOrEmpty(nguoiKyText))
                                                            {
                                                                quyetDinhDiNuocNgoai.NguoiKy1 = nguoiKyText;
                                                            }

                                                        }
                                                        else
                                                        {
                                                            quyetDinhDiNuocNgoai.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                            quyetDinhDiNuocNgoai.TenCoQuan = HamDungChung.ThongTinTruong(uow).TenBoPhan;
                                                        }
                                                        #endregion

                                                        #region Quốc gia
                                                        if (!string.IsNullOrEmpty(quocGiaText))
                                                        {
                                                            QuocGia QuocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia =?", quocGiaText));
                                                            if (QuocGia != null)
                                                                quyetDinhDiNuocNgoai.QuocGia = QuocGia;
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
                                                                quyetDinhDiNuocNgoai.TuNgay = Convert.ToDateTime(tuNgayText);
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
                                                                quyetDinhDiNuocNgoai.DenNgay = Convert.ToDateTime(denNgayText);
                                                            }
                                                            catch
                                                            {
                                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                                            }
                                                        }
                                                        #endregion

                                                        #region Ghi chú thời gian
                                                        if (!string.IsNullOrEmpty(ghiChuThoiGianText))
                                                        {
                                                            quyetDinhDiNuocNgoai.GhiChuTG = ghiChuThoiGianText;
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
                                                            quyetDinhDiNuocNgoai.NguonKinhPhi = NguonKinhPhi;
                                                        }
                                                        #endregion

                                                        #region Đơn vị tổ chức
                                                        if (!string.IsNullOrEmpty(donViToChucText))
                                                        {
                                                            quyetDinhDiNuocNgoai.DonViToChuc = donViToChucText;
                                                        }
                                                        #endregion

                                                        #region Địa điểm
                                                        if (!string.IsNullOrEmpty(diaDiemText))
                                                        {
                                                            quyetDinhDiNuocNgoai.DiaDiem = diaDiemText;
                                                        }
                                                        #endregion

                                                        #region Lý do
                                                        if (!string.IsNullOrEmpty(lyDoText))
                                                        {
                                                            quyetDinhDiNuocNgoai.LyDo = lyDoText;
                                                        }
                                                        #endregion

                                                        #region Ghi chú
                                                        if (!string.IsNullOrEmpty(ghiChuText))
                                                        {
                                                            quyetDinhDiNuocNgoai.GhiChu = ghiChuText;
                                                        }
                                                        #endregion

                                                        #region Quyết định cũ
                                                        if (!string.IsNullOrEmpty(quyetDinhCuText))
                                                        {
                                                            quyetDinhDiNuocNgoai.QuyetDinhMoi = false;
                                                        }
                                                        #endregion

                                                        //Thêm chi tiết đào tạo
                                                        chiTietQuyetDinhDiNuocNgoai = new ChiTietQuyetDinhDiNuocNgoai(uow);

                                                        chiTietQuyetDinhDiNuocNgoai.QuyetDinhDiNuocNgoai = quyetDinhDiNuocNgoai;
                                                        chiTietQuyetDinhDiNuocNgoai.ThongTinNhanVien = nhanVien;
                                                        chiTietQuyetDinhDiNuocNgoai.BoPhan = nhanVien.BoPhan;

                                                        #region Tình trạng hưởng lương
                                                        if (!string.IsNullOrEmpty(tinhTrangHuongLuongText))
                                                        {
                                                            TinhTrang TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang =?", tinhTrangHuongLuongText));
                                                            if (TinhTrang != null)
                                                            {
                                                                chiTietQuyetDinhDiNuocNgoai.TinhTrangMoi = TinhTrang;
                                                            }
                                                            else
                                                                detailLog.AppendLine("Tình trạng hưởng lương không tồn tại.");
                                                        }
                                                        #endregion

                                                        listQuyetDinh.Add(quyetDinhDiNuocNgoai);
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
