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
    public class HoSo_ImportQuanHeGiaDinh
    {
        public static void XuLy_BUH(IObjectSpace obs,ThongTinNhanVien thongTinNhanVien)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:O]"))
                    {
                        QuanHeGiaDinh quanHe;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    //
                                    //
                                    int hoTen = 0;
                                    int gioiTinh = 1;
                                    int namSinh = 2;
                                    int loaiQuanHe = 3;
                                    int queQuan = 4;
                                    int danToc = 5;
                                    int tonGiao = 6;
                                    int quocTich = 7;
                                    int noiOHienNay = 8;
                                    int trongNgoaiNuoc = 9;
                                    int nuocCuTru = 10;
                                    int namDiCu = 11;
                                    int ngheNghiep = 12;
                                    int noiLamViec = 13;
                                    int tinhTrang = 14;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String hoTenText = item[hoTen].ToString().FullTrim();
                                        String gioiTinhText = item[gioiTinh].ToString().FullTrim();
                                        String namSinhText = item[namSinh].ToString().FullTrim();
                                        String loaiQuanHeText = item[loaiQuanHe].ToString().FullTrim();
                                        String queQuanText = item[queQuan].ToString().FullTrim();
                                        String danTocText = item[danToc].ToString().FullTrim();
                                        String tonGiaoText = item[tonGiao].ToString().FullTrim();
                                        String quocTichText = item[quocTich].ToString().FullTrim();
                                        String noiOHienNayText = item[noiOHienNay].ToString().FullTrim();
                                        String trongNgoaiNuocText = item[trongNgoaiNuoc].ToString().FullTrim();
                                        String nuocCuTruText = item[nuocCuTru].ToString().FullTrim();
                                        String namDiCuText = item[namDiCu].ToString().FullTrim();
                                        String ngheNghiepText = item[ngheNghiep].ToString().FullTrim();
                                        String noiLamViecText = item[noiLamViec].ToString().FullTrim();
                                        String tinhTrangText = item[tinhTrang].ToString().FullTrim();

                                        quanHe = new QuanHeGiaDinh(uow);
                                        quanHe.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(thongTinNhanVien.Oid);

                                        #region Họ tên
                                        if (!string.IsNullOrEmpty(hoTenText))
                                        {
                                            quanHe.HoTenNguoiThan = hoTenText;
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Họ tên chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Giới tính
                                        if (!string.IsNullOrEmpty(gioiTinhText))
                                        {
                                            if (gioiTinhText.ToLower() == "nam")
                                                quanHe.GioiTinh = GioiTinhEnum.Nam;
                                            else if (gioiTinhText.ToLower() == "nữ" || gioiTinhText.ToLower() == "nu")
                                                quanHe.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                detailLog.AppendLine(" + Giới tính không hợp lệ: " + gioiTinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Giới tính chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Năm sinh
                                        if (!string.IsNullOrEmpty(namSinhText))
                                        {
                                            try
                                            {
                                                quanHe.NgaySinh = Convert.ToInt32(namSinhText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + namSinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Ngày sinh chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Loại quan hệ
                                        if (!string.IsNullOrEmpty(loaiQuanHeText))
                                        {
                                            QuanHe QuanHe = uow.FindObject<QuanHe>(CriteriaOperator.Parse("TenQuanHe =?", loaiQuanHeText));
                                            if (QuanHe != null)
                                                quanHe.QuanHe = QuanHe;
                                            else
                                                detailLog.AppendLine("Loại quan hệ không hợp lệ: " + loaiQuanHeText);
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Loại quan hệ không tìm thấy.");
                                        }
                                        #endregion

                                        #region Quê quán
                                        if (!string.IsNullOrEmpty(queQuanText))
                                        {
                                            TinhThanh TinhThanh;
                                            TinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh Like ?", queQuanText));
                                            if (TinhThanh != null)
                                                quanHe.QueQuan = TinhThanh;
                                            else
                                                detailLog.AppendLine("Tỉnh thành không hợp lệ: " + queQuanText);
                                        }
                                        #endregion

                                        #region Dân tộc
                                        if (!string.IsNullOrEmpty(danTocText))
                                        {
                                            DanToc DanToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc Like ?", danTocText));
                                            if (DanToc == null)
                                            {
                                                DanToc = new DanToc(uow);
                                                DanToc.TenDanToc = danTocText;
                                                DanToc.MaQuanLy = HamDungChung.TaoChuVietTat(danTocText);
                                            }
                                            quanHe.DanToc = DanToc;
                                        }
                                        #endregion

                                        #region Tôn giáo
                                        if (!string.IsNullOrEmpty(tonGiaoText))
                                        {
                                            TonGiao TonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao Like ?", tonGiaoText));
                                            if (TonGiao == null)
                                            {
                                                TonGiao = new TonGiao(uow);
                                                TonGiao.TenTonGiao = tonGiaoText;
                                                TonGiao.MaQuanLy = HamDungChung.TaoChuVietTat(tonGiaoText);
                                            }
                                            quanHe.TonGiao = TonGiao;
                                        }
                                        #endregion              

                                        #region Quốc tịch
                                        if (!string.IsNullOrEmpty(quocTichText))
                                        {
                                            QuocGia QuocTich = null;
                                            QuocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia Like ?", quocTichText));
                                            if (QuocTich == null)
                                            {
                                                QuocTich = new QuocGia(uow);
                                                QuocTich.TenQuocGia = quocTichText;
                                                QuocTich.MaQuanLy = HamDungChung.TaoChuVietTat(quocTichText);
                                            }
                                            quanHe.QuocTich = QuocTich;
                                        }
                                        #endregion

                                        #region Nơi ở hiện nay
                                        {
                                            if (!string.IsNullOrEmpty(noiOHienNayText))
                                            {
                                                quanHe.NoiOHienNay = noiOHienNayText;
                                            }
                                        }
                                        #endregion

                                        #region Trong ngoài nước
                                        if (!string.IsNullOrEmpty(trongNgoaiNuocText))
                                        {
                                            if (trongNgoaiNuocText.ToLower() == "trong nước")
                                                quanHe.PhanLoai = Trong_NgoaiNuocEnum.TrongNuoc;
                                            else if (trongNgoaiNuocText.ToLower() == "ngoài nước")
                                                quanHe.PhanLoai = Trong_NgoaiNuocEnum.NgoaiNuoc;
                                            else
                                            {
                                                detailLog.AppendLine(" + Trong/ Ngoài nước không hợp lệ: " + trongNgoaiNuocText);
                                            }
                                        }
                                        else
                                        {
                                            quanHe.PhanLoai = Trong_NgoaiNuocEnum.TrongNuoc;
                                        }
                                        #endregion           

                                        #region Nước cư trú
                                        if (!string.IsNullOrEmpty(nuocCuTruText))
                                        {
                                            QuocGia NuocCuTru = null;
                                            NuocCuTru = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia Like ?", nuocCuTruText));
                                            if (NuocCuTru == null)
                                            {
                                                NuocCuTru = new QuocGia(uow);
                                                NuocCuTru.TenQuocGia = nuocCuTruText;
                                                NuocCuTru.MaQuanLy = HamDungChung.TaoChuVietTat(nuocCuTruText);
                                            }
                                            quanHe.NuocCuTru = NuocCuTru;
                                        }
                                        
                                        #endregion

                                        #region Năm di cư
                                        if (!string.IsNullOrEmpty(namDiCuText))
                                        {
                                            try
                                            {
                                                quanHe.NamDiCu = Convert.ToInt32(namDiCuText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Năm di cư không hợp lệ: " + namDiCuText);
                                            }
                                        }
                                        #endregion

                                        #region Nghề nghiệp
                                        if (!string.IsNullOrEmpty(ngheNghiepText))
                                        {
                                            quanHe.NgheNghiepHienTai = ngheNghiepText;
                                        }
                                        #endregion

                                        #region Nơi làm việc
                                        if (!string.IsNullOrEmpty(noiLamViecText))
                                        {
                                            quanHe.NoiLamViec = noiLamViecText;
                                        }
                                        #endregion

                                        #region Tình trạng
                                        if (!string.IsNullOrEmpty(tinhTrangText))
                                        {
                                            if (tinhTrangText.ToLower() == "còn sống")
                                                quanHe.TinhTrang = TinhTrangEnum.ConSong;
                                            else if (tinhTrangText.ToLower() == "đã mất")
                                                quanHe.TinhTrang = TinhTrangEnum.DaMat;
                                            else
                                            {
                                                detailLog.AppendLine(" + Tình trạng không hợp lệ: " + tinhTrangText);
                                            }
                                        }
                                        else
                                        {
                                            quanHe.TinhTrang = TinhTrangEnum.ConSong;
                                        }
                                        #endregion

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import người thân [{0}] vào được: ", quanHe.HoTenNguoiThan));
                                            mainLog.AppendLine(detailLog.ToString());
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
