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
    public class DaoTao_ImportQuyetDinhDaoTao
    {
        //public static void XuLy(IObjectSpace obs)
        //{
        //    using (OpenFileDialog dialog = new OpenFileDialog())
        //    {
        //        dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
        //        if (dialog.ShowDialog() == DialogResult.OK)
        //        {

        //            using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:I]"))
        //            {
        //                ChiTietDaoTao chiTietDaoTao;
        //                QuyetDinhDaoTao quyetDinhDaoTao;
        //                ThongTinNhanVien nhanVien;
        //                StringBuilder mainLog = new StringBuilder();
        //                StringBuilder detailLog;

        //                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
        //                {
        //                    uow.BeginTransaction();

        //                    using (DialogUtil.AutoWait())
        //                    {
        //                        if (dt != null && dt.Rows.Count > 0)
        //                        {
        //                            //
        //                            int idx_MaQuanLy = 0;
        //                            int idx_HoTen = 1;
        //                            int idx_TruongDaoTao = 2;
        //                            int idx_TrinhDoDaoTao = 3;
        //                            int idx_QuocGia = 4;
        //                            int idx_HinhThucDaoTao = 5;
        //                            int idx_ChuyenMonDaoTao = 6;
        //                            int idx_TuNgay = 7;
        //                            int idx_DenNgay = 8;

        //                            foreach (DataRow item in dt.Rows)
        //                            {
        //                                //Khởi tạo bộ nhớ đệm
        //                                detailLog = new StringBuilder();

        //                                String maQuanLy = item[idx_MaQuanLy].ToString().FullTrim();
        //                                String hoTen = item[idx_HoTen].ToString().FullTrim();
        //                                String truongDaoTao = item[idx_TruongDaoTao].ToString().FullTrim();
        //                                String trinhDoDaoTao = item[idx_TrinhDoDaoTao].ToString().FullTrim();
        //                                String quocGia = item[idx_QuocGia].ToString().FullTrim();
        //                                String hinhThucDaoTao = item[idx_HinhThucDaoTao].ToString().FullTrim();
        //                                String chuyenMonDaoTao = item[idx_ChuyenMonDaoTao].ToString().FullTrim();
        //                                String tuNgay = item[idx_TuNgay].ToString().FullTrim();
        //                                String denNgay = item[idx_DenNgay].ToString().FullTrim();

        //                                //Tìm nhân viên theo mã quản lý
        //                                nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLy));
        //                                if (nhanVien != null)
        //                                {
        //                                    //Lấy dữ liệu quyết định đào tạo
        //                                    quyetDinhDaoTao = new QuyetDinhDaoTao(uow);

        //                                    #region Trường đào tạo
        //                                    if (!string.IsNullOrEmpty(truongDaoTao))
        //                                    {
        //                                        TruongDaoTao TruongDaoTao = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao =?", truongDaoTao));
        //                                        if (TruongDaoTao != null)
        //                                            quyetDinhDaoTao.TruongDaoTao = TruongDaoTao;
        //                                        else
        //                                            detailLog.AppendLine("Trường đào tạo không hợp lệ: " + truongDaoTao);
        //                                    }
        //                                    else
        //                                    {
        //                                        detailLog.AppendLine("Trường đào tạo không tìm thấy.");
        //                                    }
        //                                    #endregion

        //                                    #region Trình độ đào tạo
        //                                    if (!string.IsNullOrEmpty(trinhDoDaoTao))
        //                                    {
        //                                        TrinhDoChuyenMon TrinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon =?", trinhDoDaoTao));
        //                                        if (TrinhDoChuyenMon != null)
        //                                            quyetDinhDaoTao.TrinhDoChuyenMon = TrinhDoChuyenMon;
        //                                        else
        //                                            detailLog.AppendLine("Trình độ chuyên môn không hợp lệ: " + trinhDoDaoTao);
        //                                    }
        //                                    else
        //                                    {
        //                                        detailLog.AppendLine("Trình độ chuyên môn không tìm thấy.");
        //                                    }
        //                                    #endregion

        //                                    #region Quốc gia
        //                                    if (!string.IsNullOrEmpty(quocGia))
        //                                    {
        //                                        QuocGia QuocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia =?", quocGia));
        //                                        if (QuocGia != null)
        //                                            quyetDinhDaoTao.QuocGia = QuocGia;
        //                                        else
        //                                            detailLog.AppendLine("Quốc gia không hợp lệ: " + quocGia);
        //                                    }
        //                                    else
        //                                    {
        //                                        detailLog.AppendLine("Quốc gia không tìm thấy.");
        //                                    }
        //                                    #endregion

        //                                    #region Hình thức đào tạo
        //                                    if (!string.IsNullOrEmpty(hinhThucDaoTao))
        //                                    {
        //                                        HinhThucDaoTao HinhThucDaoTao = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao =?", hinhThucDaoTao));
        //                                        if (HinhThucDaoTao != null)
        //                                            quyetDinhDaoTao.HinhThucDaoTao = HinhThucDaoTao;
        //                                        else
        //                                            detailLog.AppendLine("Hình thức đào tạo không hợp lệ: " + hinhThucDaoTao);
        //                                    }
        //                                    else
        //                                    {
        //                                        detailLog.AppendLine("Hình thức đào tạo không tìm thấy.");
        //                                    }
        //                                    #endregion

        //                                    #region Từ ngày
        //                                    if (!string.IsNullOrEmpty(tuNgay))
        //                                    {
        //                                        try
        //                                        {
        //                                            quyetDinhDaoTao.TuNgay = Convert.ToDateTime(tuNgay);
        //                                            quyetDinhDaoTao.NgayHieuLuc = Convert.ToDateTime(tuNgay);
        //                                            quyetDinhDaoTao.NgayQuyetDinh = Convert.ToDateTime(tuNgay);
        //                                        }
        //                                        catch
        //                                        {
        //                                            detailLog.AppendLine(" + Từ ngày không hợp lệ: " + tuNgay);
        //                                        }
        //                                    }
        //                                    #endregion

        //                                    #region Đến ngày
        //                                    if (!string.IsNullOrEmpty(denNgay))
        //                                    {
        //                                        try
        //                                        {
        //                                            quyetDinhDaoTao.DenNgay = Convert.ToDateTime(denNgay);
        //                                        }
        //                                        catch
        //                                        {
        //                                            detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgay);
        //                                        }
        //                                    }
        //                                    #endregion

        //                                    { //Lấy dữ liệu chi tiết đào tạo
        //                                        chiTietDaoTao = new ChiTietDaoTao(uow);

        //                                        chiTietDaoTao.QuyetDinhDaoTao = quyetDinhDaoTao;
        //                                        chiTietDaoTao.ThongTinNhanVien = nhanVien;
        //                                        chiTietDaoTao.BoPhan = nhanVien.BoPhan;

        //                                        #region Chuyên môn đào tạo
        //                                        if (!string.IsNullOrEmpty(chuyenMonDaoTao))
        //                                        {
        //                                            ChuyenMonDaoTao ChuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao =?", chuyenMonDaoTao));
        //                                            if (ChuyenMonDaoTao == null)
        //                                            {
        //                                                ChuyenMonDaoTao = new ChuyenMonDaoTao(uow);
        //                                                ChuyenMonDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(chuyenMonDaoTao);
        //                                                ChuyenMonDaoTao.TenChuyenMonDaoTao = chuyenMonDaoTao;
        //                                            }
        //                                            chiTietDaoTao.ChuyenMonDaoTao = ChuyenMonDaoTao;
        //                                        }
        //                                        else
        //                                        {
        //                                            detailLog.AppendLine("Chuyên môn đào tạo không tìm thấy.");
        //                                        }
        //                                        #endregion

        //                                        //Đưa thông tin bị lỗi vào blog
        //                                        if (detailLog.Length > 0)
        //                                        {
        //                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
        //                                            mainLog.AppendLine(detailLog.ToString());
        //                                        }
        //                                    }
        //                                    //End Chi tiết đào tạo
        //                                }
        //                                else
        //                                {
        //                                    mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã nhân sự (Số hiệu công chức) là: {0}", maQuanLy));
        //                                }
        //                            }
        //                        }
        //                    }

        //                    //
        //                    if (mainLog.Length > 0)
        //                    {
        //                        uow.RollbackTransaction();
        //                        if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
        //                        {
        //                            string tenFile = "Import_Log.txt";
        //                            StreamWriter writer = new StreamWriter(tenFile);
        //                            writer.WriteLine(mainLog.ToString());
        //                            writer.Flush();
        //                            writer.Close();
        //                            writer.Dispose();
        //                            HamDungChung.WriteLog(tenFile, mainLog.ToString());
        //                            System.Diagnostics.Process.Start(tenFile);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        //Nếu không có lỗi thì tiến hành lưu dữ liệu.
        //                        uow.CommitChanges();
        //                        //hoàn tất giao tác
        //                        DialogUtil.ShowSaveSuccessful("Import Thành Công!");
        //                    }

        //                }
        //            }
        //        }
        //    }
        //}

        public static void XuLy(IObjectSpace obs)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:Q]"))
                    {
                        QuyetDinhDaoTao quyetDinhDaoTao;
                        ChiTietDaoTao chiTietDaoTao;
                        XPCollection<QuyetDinhDaoTao> listQuyetDinh;
                        XPCollection<ChiTietDaoTao> listChiTietDaoTao;
                        ThongTinNhanVien thongTinNhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            listQuyetDinh = new XPCollection<QuyetDinhDaoTao>(uow);
                            listChiTietDaoTao = new XPCollection<ChiTietDaoTao>(uow);

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    int soQuyetDinh = 0;
                                    int ngayQuyetDinh = 1; 
                                    int ngayHieuLuc = 2;
                                    int quocGia = 2;
                                    int truongDaoTao = 3;
                                    int hinhThucDaoTao = 4;
                                    int trinhDoDaoTao = 5;
                                    int tuNgay = 6;
                                    int denNgay = 7;
                                    int nguonKinhPhi = 8;                                   
                                    int maQuanLy = 9;
                                    int chuyenMonDaoTao = 10;
                                    int tinhTrangHuongLuong = 11;                                    
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[ngayHieuLuc].ToString().FullTrim();
                                        String quocGiaText = item[quocGia].ToString().FullTrim();
                                        String truongDaoTaoText = item[truongDaoTao].ToString().FullTrim();
                                        String hinhThucDaoTaoText = item[hinhThucDaoTao].ToString().FullTrim();
                                        String trinhDoDaoTaoText = item[trinhDoDaoTao].ToString().FullTrim();
                                        String chuyenMonDaoTaoText = item[chuyenMonDaoTao].ToString().FullTrim();
                                        String tuNgayText = item[tuNgay].ToString().FullTrim();
                                        String denNgayText = item[denNgay].ToString().FullTrim();
                                        String nguonKinhPhiText = item[nguonKinhPhi].ToString().FullTrim();                                        
                                        String maQuanLyText = item[maQuanLy].ToString().FullTrim();                                        
                                        String tinhTrangHuongLuongText = item[tinhTrangHuongLuong].ToString().FullTrim();                                        
                                        DateTime tuNgayDate = DateTime.MinValue, denNgayDate = DateTime.MinValue;

                                        #region Từ ngày
                                        if (!string.IsNullOrEmpty(tuNgayText))
                                        {
                                            try
                                            {
                                                tuNgayDate = Convert.ToDateTime(tuNgayText);
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
                                                denNgayDate = Convert.ToDateTime(denNgayText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                            }
                                        }
                                        #endregion

                                        //Tìm nhân viên theo mã quản lý
                                        thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ? or SoHieuCongChuc = ?", maQuanLyText, maQuanLyText));
                                        if (thongTinNhanVien != null)
                                        {
                                            
                                            #region Tìm quyết định
                                            if (!string.IsNullOrEmpty(soQuyetDinhText))// && tuNgayDate != DateTime.MinValue && denNgayDate != DateTime.MinValue)
                                            {
                                                listQuyetDinh.Filter = CriteriaOperator.Parse("SoQuyetDinh = ? and TuNgay = ? and DenNgay = ?", soQuyetDinhText, tuNgayDate, denNgayDate);
                                            }
                                            #endregion
                                            if (listQuyetDinh.Count == 1)
                                            {
                                                listChiTietDaoTao.Filter = CriteriaOperator.Parse("QuyetDinhDaoTao = ? and ThongTinNhanVien.Oid = ?", listQuyetDinh[0].Oid, thongTinNhanVien.Oid);
                                                if (listChiTietDaoTao.Count > 1)
                                                {
                                                    detailLog.AppendLine("Số quyết định " + soQuyetDinhText + ", nhân viên " + maQuanLyText + "đã tồn tại.");
                                                }
                                                else
                                                {
                                                    chiTietDaoTao = new ChiTietDaoTao(uow);
                                                    chiTietDaoTao.QuyetDinhDaoTao = listQuyetDinh[0];
                                                    chiTietDaoTao.ThongTinNhanVien = thongTinNhanVien;
                                                    chiTietDaoTao.BoPhan = thongTinNhanVien.BoPhan;
                                                    
                                                    #region Chuyên môn đào tạo
                                                    if (!string.IsNullOrEmpty(chuyenMonDaoTaoText))
                                                    {
                                                        ChuyenMonDaoTao ChuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao =?", chuyenMonDaoTaoText));
                                                        if (ChuyenMonDaoTao == null)
                                                        {
                                                            ChuyenMonDaoTao = new ChuyenMonDaoTao(uow);
                                                            ChuyenMonDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(chuyenMonDaoTaoText);
                                                            ChuyenMonDaoTao.TenChuyenMonDaoTao = chuyenMonDaoTaoText;
                                                        }
                                                        chiTietDaoTao.ChuyenMonDaoTao = ChuyenMonDaoTao;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Chuyên môn đào tạo không tìm thấy.");
                                                    }
                                                    #endregion

                                                    #region Tình trạng hưởng lương
                                                    if (!string.IsNullOrEmpty(tinhTrangHuongLuongText))
                                                    {
                                                        TinhTrang TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang =?", tinhTrangHuongLuongText));
                                                        if (TinhTrang != null)
                                                        {
                                                            chiTietDaoTao.TinhTrangMoi = TinhTrang;
                                                        }
                                                        else
                                                            detailLog.AppendLine("Tình trạng hưởng lương không tồn tại: " + tinhTrangHuongLuongText);
                                                    }
                                                    #endregion

                                                    listQuyetDinh[0].ListChiTietDaoTao.Add(chiTietDaoTao);
                                                    listChiTietDaoTao.Add(chiTietDaoTao);
                                                }
                                            }
                                            else
                                            {
                                                quyetDinhDaoTao = new QuyetDinhDaoTao(uow);
                                                quyetDinhDaoTao.QuyetDinhMoi = false;

                                                #region Số quyết định
                                                if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                {
                                                    quyetDinhDaoTao.SoQuyetDinh = soQuyetDinhText;
                                                }
                                                #endregion

                                                #region Ngày quyết định
                                                if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhDaoTao.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Ngày quyết định không hợp lệ: " + ngayQuyetDinhText);
                                                    }
                                                }
                                                #endregion
                                                
                                                #region Quốc gia
                                                if (!string.IsNullOrEmpty(quocGiaText))
                                                {
                                                    QuocGia QuocGia = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia =?", quocGiaText));
                                                    if (QuocGia != null)
                                                        quyetDinhDaoTao.QuocGia = QuocGia;
                                                    else
                                                        detailLog.AppendLine("Quốc gia không hợp lệ: " + quocGiaText);
                                                }
                                                else
                                                {
                                                    //detailLog.AppendLine("Quốc gia không tìm thấy.");
                                                }
                                                #endregion

                                                #region Trường đào tạo
                                                if (!string.IsNullOrEmpty(truongDaoTaoText))
                                                {
                                                    TruongDaoTao TruongDaoTao = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao =?", truongDaoTaoText));
                                                    if (TruongDaoTao == null)
                                                    {
                                                        TruongDaoTao = new TruongDaoTao(uow);
                                                        TruongDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(truongDaoTaoText);
                                                        TruongDaoTao.TenTruongDaoTao = truongDaoTaoText;
                                                    }
                                                    quyetDinhDaoTao.TruongDaoTao = TruongDaoTao;
                                                }
                                                //else
                                                //{
                                                //    detailLog.AppendLine("Trường đào tạo không tìm thấy.");
                                                //}
                                                #endregion

                                                #region Hình thức đào tạo
                                                if (!string.IsNullOrEmpty(hinhThucDaoTaoText))
                                                {
                                                    HinhThucDaoTao HinhThucDaoTao = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao =?", hinhThucDaoTaoText));
                                                    if (HinhThucDaoTao != null)
                                                        quyetDinhDaoTao.HinhThucDaoTao = HinhThucDaoTao;
                                                    else
                                                        detailLog.AppendLine("Hình thức đào tạo không hợp lệ: " + hinhThucDaoTaoText);
                                                }
                                                else
                                                {
                                                    //detailLog.AppendLine("Hình thức đào tạo không tìm thấy.");
                                                }
                                                #endregion

                                                #region Trình độ đào tạo
                                                if (!string.IsNullOrEmpty(trinhDoDaoTaoText))
                                                {
                                                    TrinhDoChuyenMon TrinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon =?", trinhDoDaoTaoText));
                                                    if (TrinhDoChuyenMon != null)
                                                        quyetDinhDaoTao.TrinhDoChuyenMon = TrinhDoChuyenMon;
                                                    else
                                                        detailLog.AppendLine("Trình độ chuyên môn không hợp lệ: " + trinhDoDaoTaoText);
                                                }
                                                else
                                                {
                                                    //detailLog.AppendLine("Trình độ chuyên môn không tìm thấy.");
                                                }
                                                #endregion

                                                #region Từ ngày
                                                if (!string.IsNullOrEmpty(tuNgayText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhDaoTao.TuNgay = Convert.ToDateTime(tuNgayText);
                                                        quyetDinhDaoTao.NgayHieuLuc = Convert.ToDateTime(tuNgayText);
                                                        quyetDinhDaoTao.NgayPhatSinhBienDong = Convert.ToDateTime(tuNgayText);
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
                                                        quyetDinhDaoTao.DenNgay = Convert.ToDateTime(denNgayText);
                                                    }
                                                    catch
                                                    {
                                                        detailLog.AppendLine(" + Đến ngày không hợp lệ: " + denNgayText);
                                                    }
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
                                                    quyetDinhDaoTao.NguonKinhPhi = NguonKinhPhi;
                                                }
                                                #endregion

                                                //Thêm chi tiết đào tạo
                                                chiTietDaoTao = new ChiTietDaoTao(uow);

                                                chiTietDaoTao.QuyetDinhDaoTao = quyetDinhDaoTao;
                                                chiTietDaoTao.ThongTinNhanVien = thongTinNhanVien;
                                                chiTietDaoTao.BoPhan = thongTinNhanVien.BoPhan;

                                                #region Chuyên môn đào tạo
                                                if (!string.IsNullOrEmpty(chuyenMonDaoTaoText))
                                                {
                                                    ChuyenMonDaoTao ChuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao =?", chuyenMonDaoTaoText));
                                                    if (ChuyenMonDaoTao == null)
                                                    {
                                                        ChuyenMonDaoTao = new ChuyenMonDaoTao(uow);
                                                        ChuyenMonDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(chuyenMonDaoTaoText);
                                                        ChuyenMonDaoTao.TenChuyenMonDaoTao = chuyenMonDaoTaoText;
                                                    }
                                                    chiTietDaoTao.ChuyenMonDaoTao = ChuyenMonDaoTao;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Chuyên môn đào tạo không tìm thấy.");
                                                }
                                                #endregion

                                                #region Tình trạng hưởng lương
                                                if (!string.IsNullOrEmpty(tinhTrangHuongLuongText))
                                                {
                                                    TinhTrang TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", tinhTrangHuongLuongText));
                                                    if (TinhTrang != null)
                                                    {
                                                        chiTietDaoTao.TinhTrangMoi = TinhTrang;
                                                    }
                                                    else
                                                        detailLog.AppendLine("Tình trạng hưởng lương không tồn tại.");
                                                }
                                                #endregion

                                                listQuyetDinh.Add(quyetDinhDaoTao);
                                            }

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", thongTinNhanVien.HoTen));
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