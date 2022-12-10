using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using System.Data;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using System.Text;
using PSC_HRM.Module.ThuNhap.TruyLuong;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.ThuNhapTangThem;
using PSC_HRM.Module.BaoMat;
using System.IO;
using PSC_HRM.Module.DanhMuc;


namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    [ModelDefault("Caption", "Import truy lĩnh - New")]
    public class ImportTruyLuongNew : ImportBase
    {
        public ImportTruyLuongNew(Session session)
            : base(session)
        { }

        /*
        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                //import
                using (OpenFileDialog dialog = new OpenFileDialog())
                {

                    dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";
                    dialog.Multiselect = false;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:Z]"))
                        {
                            BangTruyLuongNew bangTruyLuong = obj as BangTruyLuongNew;

                            uow.BeginTransaction();

                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;

                            //var mainLog = new StringBuilder();
                            //var errorLog = new StringBuilder();

                            ThongTinNhanVien thongTinNhanVien;
                            TruyLuongNhanVienNew truyLuong = null;
                            ChiTietTruyLuongNew chiTiet = null;
                            XPCollection<TruyLuongNhanVienNew> listTruyLuong = new XPCollection<TruyLuongNhanVienNew>(uow);
                            XPCollection<ChiTietTruyLuongNew> listChiTiet = new XPCollection<ChiTietTruyLuongNew>(uow);

                            const int idx_stt = 0;
                            const int idx_Thang = 1;
                            const int idx_Nam = 2;
                            const int idx_MaQuanLy = 3;
                            const int idx_HoTen = 4;
                            const int idx_BoPhan = 5;

                            const int idx_HSL_Cu = 6;
                            const int idx_HSL_Moi = 7;
                            const int idx_HSCV_Cu = 8;
                            const int idx_HSCV_Moi = 9;
                            const int idx_VuotKhung_Cu = 10;
                            const int idx_VuotKhung_Moi = 11;
                            const int idx_ThamNien_Cu = 12;
                            const int idx_ThamNien_Moi = 13;

                            const int idx_PhuCapUuDai_Cu = 14;
                            const int idx_PhuCapUuDai_Moi = 15;
                            const int idx_HSPCChuyenMon_Cu = 16;
                            const int idx_HSPCChuyenMon_Moi = 17;
                            const int idx_MucLuongCoSo_Cu = 18;
                            const int idx_MucLuongCoSo_Moi = 19;
                            const int idx_ChenhLechLuong_Cu = 20;
                            const int idx_ChenhLechLuong_Moi = 21;

                            const int idx_MaChiTiet = 22;
                            const int idx_CongTru = 23;
                            const int idx_MucLuong_Cu = 24;
                            const int idx_MucLuong_Moi = 25;

                            using (DialogUtil.AutoWait())
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    String text_stt = item[idx_stt].ToString().Trim();
                                    String text_Thang = item[idx_Thang].ToString().Trim();
                                    String text_Nam = item[idx_Nam].ToString().Trim();
                                    String text_MaQuanLy = item[idx_MaQuanLy].ToString().Trim();
                                    String text_HoTen = item[idx_HoTen].ToString().Trim();
                                    String text_BoPhan = item[idx_BoPhan].ToString().Trim();

                                    String text_HSL_Cu = item[idx_HSL_Cu].ToString().Trim();
                                    String text_HSL_Moi = item[idx_HSL_Moi].ToString().Trim();
                                    String text_HSCV_Cu = item[idx_HSCV_Cu].ToString().Trim();
                                    String text_HSCV_Moi = item[idx_HSCV_Moi].ToString().Trim();
                                    String text_VuotKhung_Cu = item[idx_VuotKhung_Cu].ToString().Trim();
                                    String text_VuotKhung_Moi = item[idx_VuotKhung_Moi].ToString().Trim();
                                    String text_ThamNien_Cu = item[idx_ThamNien_Cu].ToString().Trim();
                                    String text_ThamNien_Moi = item[idx_ThamNien_Moi].ToString().Trim();

                                    String text_PhuCapUuDai_Cu = item[idx_PhuCapUuDai_Cu].ToString().Trim();
                                    String text_PhuCapUuDai_Moi = item[idx_PhuCapUuDai_Moi].ToString().Trim();
                                    String text_HSPCChuyenMon_Cu = item[idx_HSPCChuyenMon_Cu].ToString().Trim();
                                    String text_HSPCChuyenMon_Moi = item[idx_HSPCChuyenMon_Moi].ToString().Trim();
                                    String text_MucLuongCoSo_Cu = item[idx_MucLuongCoSo_Cu].ToString().Trim();
                                    String text_MucLuongCoSo_Moi = item[idx_MucLuongCoSo_Moi].ToString().Trim();
                                    String text_ChenhLechLuong_Cu = item[idx_ChenhLechLuong_Cu].ToString().Trim();
                                    String text_ChenhLechLuong_Moi = item[idx_ChenhLechLuong_Moi].ToString().Trim();

                                    String text_MaChiTiet = item[idx_MaChiTiet].ToString().Trim();
                                    String text_CongTru = item[idx_CongTru].ToString().Trim();
                                    String text_MucLuong_Cu = item[idx_MucLuong_Cu].ToString().Trim();
                                    String text_MucLuong_Moi = item[idx_MucLuong_Moi].ToString().Trim();

                                    thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ? && HoTen = ?", text_MaQuanLy, text_HoTen));
                                    KyTinhLuong kyTinhLuong = uow.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang = ? && Nam = ?", text_Thang, text_Nam));
                                    if (thongTinNhanVien == null || kyTinhLuong == null)
                                    {
                                        if (thongTinNhanVien == null)
                                        { detailLog.AppendLine(string.Format("+ Mã quản lý: {0}, {1} chưa tồn tại trong hệ thống", text_MaQuanLy, text_HoTen)); }
                                        if (kyTinhLuong == null)
                                        { detailLog.AppendLine(string.Format("+ Kỳ tính lương: {0}/{1} chưa tồn tại trong hệ thống", text_Thang, text_Nam)); }
                                    }
                                    else
                                    {

                                        listTruyLuong.Filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid = ? && KyTinhLuong.Oid = ?", thongTinNhanVien.Oid, kyTinhLuong.Oid);
                                        if (listTruyLuong.Count == 1)
                                        {
                                            truyLuong = listTruyLuong[0];
                                            listChiTiet.Filter = CriteriaOperator.Parse("TruyLuongNhanVien.ThongTinNhanVien.Oid = ? && TruyLuongNhanVien.KyTinhLuong.Oid = ? && MaChiTiet like ?", truyLuong.Oid, kyTinhLuong.Oid, text_MaChiTiet);
                                            if (listChiTiet.Count > 0)
                                            {
                                                detailLog.AppendLine(string.Format("+ Mã quản lý: {0}, {1} - Mã chi tiết: {2} đã tồn tại", text_MaQuanLy, text_HoTen, text_MaChiTiet));
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(text_MaChiTiet)
                                                    && !string.IsNullOrEmpty(text_MucLuong_Cu)
                                                    && !string.IsNullOrEmpty(text_MucLuong_Moi))
                                                {
                                                    #region Tạo mới ChiTietTruyLuongNew
                                                    chiTiet = new ChiTietTruyLuongNew(uow);
                                                    chiTiet.TruyLuongNhanVien = truyLuong;
                                                    #region--Mã chi tiết--
                                                    if (!string.IsNullOrEmpty(text_MaChiTiet))
                                                    {
                                                        chiTiet.MaChiTiet = text_MaChiTiet;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Mã chi tiết không có");
                                                    }
                                                    #endregion

                                                    #region Cộng/trừ
                                                    if (text_CongTru.Equals("Trừ"))
                                                    { chiTiet.CongTru = CongTruEnum.Tru; }
                                                    else
                                                    { chiTiet.CongTru = CongTruEnum.Cong; }
                                                    #endregion

                                                    #region--Mức lương cũ--
                                                    if (!string.IsNullOrEmpty(text_MucLuong_Cu))
                                                    {
                                                        try
                                                        {
                                                            chiTiet.MucLuongCu = Convert.ToDecimal(text_MucLuong_Cu);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine("+ Sai thông tin Mức lương cũ");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Mức lương cũ không có");
                                                    }
                                                    #endregion

                                                    #region--Mức lương mới--
                                                    if (!string.IsNullOrEmpty(text_MucLuong_Moi))
                                                    {
                                                        try
                                                        {
                                                            chiTiet.MucLuongMoi = Convert.ToDecimal(text_MucLuong_Moi);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine("+ Sai thông tin Mức lương mới");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Mức lương mới không có");
                                                    }
                                                    #endregion
                                                    //----Số tiền tự tính = Mức lương mới - Mức lương cũ
                                                    #endregion
                                                    listChiTiet.Add(chiTiet);
                                                }
                                                else if (!string.IsNullOrEmpty(text_MaChiTiet)
                                                    || !string.IsNullOrEmpty(text_MucLuong_Cu)
                                                    || !string.IsNullOrEmpty(text_MucLuong_Moi))
                                                {
                                                    if (string.IsNullOrEmpty(text_MaChiTiet))
                                                        detailLog.AppendLine("Mã chi tiết không có");
                                                    if (string.IsNullOrEmpty(text_MucLuong_Cu))
                                                        detailLog.AppendLine("Mức lương cũ không có");
                                                    if (string.IsNullOrEmpty(text_MucLuong_Moi))
                                                        detailLog.AppendLine("Mức lương mới không có");
                                                }
                                                //trường hợp trống hết số tiền: bỏ qua, chỉ lấy hệ số
                                            }
                                        }
                                        else
                                        {
                                            #region Tạo mới TruyLuongNhanVienNew
                                            truyLuong = new TruyLuongNhanVienNew(uow);
                                            truyLuong.BangTruyLuong = uow.FindObject<BangTruyLuongNew>(CriteriaOperator.Parse("Oid = ?", bangTruyLuong.Oid));

                                            BoPhan boPhan;
                                            boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", text_BoPhan));
                                            if (boPhan != null)
                                                truyLuong.BoPhan = boPhan;

                                            truyLuong.ThongTinNhanVien = thongTinNhanVien;
                                            truyLuong.KyTinhLuong = kyTinhLuong;
                                            //Chọn Nhân viên + Kỳ tính lương => tự động lấy hệ số

                                            #region--HSL cũ--
                                            if (!string.IsNullOrEmpty(text_HSL_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.HeSoLuongCu = Convert.ToDecimal(text_HSL_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSL cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSL cũ không có");
                                            //}
                                            #endregion

                                            #region--HSL Mới--
                                            if (!string.IsNullOrEmpty(text_HSL_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.HeSoLuongMoi = Convert.ToDecimal(text_HSL_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSL mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSL mới không có");
                                            //}
                                            #endregion

                                            #region--HSCV cũ--
                                            if (!string.IsNullOrEmpty(text_HSCV_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.HSPCChucVuCu = Convert.ToDecimal(text_HSCV_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSCV cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSCV cũ không có");
                                            //}
                                            #endregion

                                            #region--HSCV Mới--
                                            if (!string.IsNullOrEmpty(text_HSCV_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.HSPCChucVuMoi = Convert.ToDecimal(text_HSCV_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSCV mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSCV mới không có");
                                            //}
                                            #endregion

                                            #region--VuotKhung cũ--
                                            if (!string.IsNullOrEmpty(text_VuotKhung_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.VuotKhungCu = Convert.ToInt32(text_VuotKhung_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin VuotKhung cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("VuotKhung cũ không có");
                                            //}
                                            #endregion

                                            #region--VuotKhung Mới--
                                            if (!string.IsNullOrEmpty(text_VuotKhung_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.VuotKhungMoi = Convert.ToInt32(text_VuotKhung_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin VuotKhung mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("VuotKhung mới không có");
                                            //}
                                            #endregion

                                            #region--ThamNien cũ--
                                            if (!string.IsNullOrEmpty(text_ThamNien_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.ThamNienCu = Convert.ToDecimal(text_ThamNien_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin ThamNien cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("ThamNien cũ không có");
                                            //}
                                            #endregion

                                            #region--ThamNien Mới--
                                            if (!string.IsNullOrEmpty(text_ThamNien_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.ThamNienMoi = Convert.ToDecimal(text_ThamNien_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin ThamNien mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("ThamNien mới không có");
                                            //}
                                            #endregion

                                            #region--PhuCapUuDai cũ--
                                            if (!string.IsNullOrEmpty(text_PhuCapUuDai_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.PhuCapUuDaiCu = Convert.ToInt32(text_PhuCapUuDai_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin PhuCapUuDai cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("PhuCapUuDai cũ không có");
                                            //}
                                            #endregion

                                            #region--PhuCapUuDai Mới--
                                            if (!string.IsNullOrEmpty(text_PhuCapUuDai_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.PhuCapUuDaiMoi = Convert.ToInt32(text_PhuCapUuDai_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin PhuCapUuDai mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("PhuCapUuDai mới không có");
                                            //}
                                            #endregion

                                            #region--HSPCChuyenMon cũ--
                                            if (!string.IsNullOrEmpty(text_HSPCChuyenMon_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.HSPCChuyenMonCu = Convert.ToDecimal(text_HSPCChuyenMon_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSPCChuyenMon cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSPCChuyenMon cũ không có");
                                            //}
                                            #endregion

                                            #region--HSPCChuyenMon Mới--
                                            if (!string.IsNullOrEmpty(text_HSPCChuyenMon_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.HSPCChuyenMonMoi = Convert.ToDecimal(text_HSPCChuyenMon_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSPCChuyenMon mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSPCChuyenMon mới không có");
                                            //}
                                            #endregion

                                            #region--MucLuongCoSo cũ--
                                            if (!string.IsNullOrEmpty(text_MucLuongCoSo_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.MucLuongCoSoCu = Convert.ToDecimal(text_MucLuongCoSo_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin MucLuongCoSo cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("MucLuongCoSo cũ không có");
                                            //}
                                            #endregion

                                            #region--MucLuongCoSo Mới--
                                            if (!string.IsNullOrEmpty(text_MucLuongCoSo_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.MucLuongCoSoMoi = Convert.ToDecimal(text_MucLuongCoSo_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin MucLuongCoSo mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("MucLuongCoSo mới không có");
                                            //}
                                            #endregion

                                            #region--ChenhLechLuongCoBan cũ--
                                            if (!string.IsNullOrEmpty(text_ChenhLechLuong_Cu))
                                            {
                                                try
                                                {
                                                    truyLuong.ChenhLechLuongCoBanCu = Convert.ToDecimal(text_ChenhLechLuong_Cu);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin ChenhLechLuongCoBan cũ");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("ChenhLechLuongCoBan cũ không có");
                                            //}
                                            #endregion

                                            #region--ChenhLechLuongCoBan Mới--
                                            if (!string.IsNullOrEmpty(text_ChenhLechLuong_Moi))
                                            {
                                                try
                                                {
                                                    truyLuong.ChenhLechLuongCoBanMoi = Convert.ToDecimal(text_ChenhLechLuong_Moi);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin ChenhLechLuongCoBan mới");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("ChenhLechLuongCoBan mới không có");
                                            //}
                                            #endregion

                                            #region Tạo mới ChiTietTruyLuongNew
                                            if (!string.IsNullOrEmpty(text_MaChiTiet)
                                            && !string.IsNullOrEmpty(text_MucLuong_Cu)
                                            && !string.IsNullOrEmpty(text_MucLuong_Moi))
                                            {
                                                #region Tạo mới ChiTietTruyLuongNew
                                                chiTiet = new ChiTietTruyLuongNew(uow);
                                                chiTiet.TruyLuongNhanVien = truyLuong;
                                                #region--Mã chi tiết--
                                                if (!string.IsNullOrEmpty(text_MaChiTiet))
                                                {
                                                    chiTiet.MaChiTiet = text_MaChiTiet;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Mã chi tiết không có");
                                                }
                                                #endregion

                                                #region Cộng/trừ
                                                if (text_CongTru.Equals("Trừ"))
                                                { chiTiet.CongTru = CongTruEnum.Tru; }
                                                else
                                                { chiTiet.CongTru = CongTruEnum.Cong; }
                                                #endregion

                                                #region--Mức lương cũ--
                                                if (!string.IsNullOrEmpty(text_MucLuong_Cu))
                                                {
                                                    try
                                                    {
                                                        chiTiet.MucLuongCu = Convert.ToDecimal(text_MucLuong_Cu);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine("+ Sai thông tin Mức lương cũ");
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Mức lương cũ không có");
                                                }
                                                #endregion

                                                #region--Mức lương mới--
                                                if (!string.IsNullOrEmpty(text_MucLuong_Moi))
                                                {
                                                    try
                                                    {
                                                        chiTiet.MucLuongMoi = Convert.ToDecimal(text_MucLuong_Moi);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine("+ Sai thông tin Mức lương mới");
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Mức lương mới không có");
                                                }
                                                #endregion
                                                //----Số tiền tự tính = Mức lương mới - Mức lương cũ
                                                #endregion
                                                listChiTiet.Add(chiTiet);
                                            }
                                            else if (!string.IsNullOrEmpty(text_MaChiTiet)
                                                || !string.IsNullOrEmpty(text_MucLuong_Cu)
                                                || !string.IsNullOrEmpty(text_MucLuong_Moi))
                                            {
                                                if (string.IsNullOrEmpty(text_MaChiTiet))
                                                    detailLog.AppendLine("Mã chi tiết không có");
                                                if (string.IsNullOrEmpty(text_MucLuong_Cu))
                                                    detailLog.AppendLine("Mức lương cũ không có");
                                                if (string.IsNullOrEmpty(text_MucLuong_Moi))
                                                    detailLog.AppendLine("Mức lương mới không có");
                                            }
                                            //trường hợp trống hết số tiền: bỏ qua, chỉ lấy hệ số
                                            #endregion
                                            #endregion
                                            listTruyLuong.Add(truyLuong);
                                        }
                                    }

                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine("- STT: " + text_stt + " - Họ Tên:" + text_HoTen);
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                }

                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
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
        */

        public override void XuLy(IObjectSpace obs, object obj)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                //import
                using (OpenFileDialog dialog = new OpenFileDialog())
                {

                    dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";
                    dialog.Multiselect = false;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:Z]"))
                        {
                            BangTruyLuongNew bangTruyLuong = obj as BangTruyLuongNew;

                            uow.BeginTransaction();

                            StringBuilder mainLog = new StringBuilder();
                            StringBuilder detailLog;

                            //var mainLog = new StringBuilder();
                            //var errorLog = new StringBuilder();

                            ThongTinNhanVien thongTinNhanVien;
                            TruyLuongNhanVienNew truyLuong = null;
                            ChiTietTruyLuongNew chiTiet = null;
                            XPCollection<TruyLuongNhanVienNew> listTruyLuong = new XPCollection<TruyLuongNhanVienNew>(uow);
                            XPCollection<ChiTietTruyLuongNew> listChiTiet = new XPCollection<ChiTietTruyLuongNew>(uow);

                            const int idx_stt = 0;
                            const int idx_Thang = 1;
                            const int idx_Nam = 2;
                            const int idx_MaQuanLy = 3;
                            const int idx_HoTen = 4;
                            const int idx_BoPhan = 5;

                            const int idx_HSL_ChenhLech = 6;                       
                            const int idx_HSCV_ChenhLech = 7;                      
                            const int idx_HSThamNien_ChenhLech = 8;                          

                            const int idx_MaChiTiet = 9;
                            const int idx_CongTru = 10;
                            const int idx_SoTien = 11;
                            const int idx_SoTienChiuThue = 12;

                            using (DialogUtil.AutoWait())
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    String text_stt = item[idx_stt].ToString().Trim();
                                    String text_Thang = item[idx_Thang].ToString().Trim();
                                    String text_Nam = item[idx_Nam].ToString().Trim();
                                    String text_MaQuanLy = item[idx_MaQuanLy].ToString().Trim();
                                    String text_HoTen = item[idx_HoTen].ToString().Trim();
                                    String text_BoPhan = item[idx_BoPhan].ToString().Trim();

                                    String text_HSL_ChenhLech = item[idx_HSL_ChenhLech].ToString().Trim();
                                    String text_HSCV_ChenhLech = item[idx_HSCV_ChenhLech].ToString().Trim();
                                    String text_HSThamNien_ChenhLech = item[idx_HSThamNien_ChenhLech].ToString().Trim();                                  

                                    String text_MaChiTiet = item[idx_MaChiTiet].ToString().Trim();
                                    String text_CongTru = item[idx_CongTru].ToString().Trim();
                                    String text_SoTien = item[idx_SoTien].ToString().Trim();
                                    String text_SoTienChiuThue = item[idx_SoTienChiuThue].ToString().Trim();

                                    thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ? && HoTen = ?", text_MaQuanLy, text_HoTen));
                                    KyTinhLuong kyTinhLuong = uow.FindObject<KyTinhLuong>(CriteriaOperator.Parse("Thang = ? && Nam = ?", text_Thang, text_Nam));
                                    if (thongTinNhanVien == null || kyTinhLuong == null)
                                    {
                                        if (thongTinNhanVien == null)
                                        { detailLog.AppendLine(string.Format("+ Mã quản lý: {0}, {1} chưa tồn tại trong hệ thống", text_MaQuanLy, text_HoTen)); }
                                        if (kyTinhLuong == null)
                                        { detailLog.AppendLine(string.Format("+ Kỳ tính lương: {0}/{1} chưa tồn tại trong hệ thống", text_Thang, text_Nam)); }
                                    }
                                    else
                                    {

                                        listTruyLuong.Filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid = ? && KyTinhLuong.Oid = ?", thongTinNhanVien.Oid, kyTinhLuong.Oid);
                                        if (listTruyLuong.Count == 1)
                                        {
                                            truyLuong = listTruyLuong[0];
                                            listChiTiet.Filter = CriteriaOperator.Parse("TruyLuongNhanVien.ThongTinNhanVien.Oid = ? && TruyLuongNhanVien.KyTinhLuong.Oid = ? && MaChiTiet like ?", truyLuong.Oid, kyTinhLuong.Oid, text_MaChiTiet);
                                            if (listChiTiet.Count > 0)
                                            {
                                                detailLog.AppendLine(string.Format("+ Mã quản lý: {0}, {1} - Mã chi tiết: {2} đã tồn tại", text_MaQuanLy, text_HoTen, text_MaChiTiet));
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(text_MaChiTiet)
                                                    && !string.IsNullOrEmpty(text_SoTien))
                                                {
                                                    #region Tạo mới ChiTietTruyLuongNew
                                                    chiTiet = new ChiTietTruyLuongNew(uow);
                                                    chiTiet.TruyLuongNhanVien = truyLuong;
                                                    #region--Mã chi tiết--
                                                    if (!string.IsNullOrEmpty(text_MaChiTiet))
                                                    {
                                                        chiTiet.MaChiTiet = text_MaChiTiet;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Mã chi tiết không có");
                                                    }
                                                    #endregion

                                                    #region Cộng/trừ
                                                    if (text_CongTru.Equals("Trừ"))
                                                    { chiTiet.CongTru = CongTruEnum.Tru; }
                                                    else
                                                    { chiTiet.CongTru = CongTruEnum.Cong; }
                                                    #endregion

                                                    #region--Số tiền--
                                                    if (!string.IsNullOrEmpty(text_SoTien))
                                                    {
                                                        try
                                                        {
                                                            chiTiet.SoTien = Convert.ToDecimal(text_SoTien);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine("+ Sai thông tin Số tiền");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Số tiền không có");
                                                    }
                                                    #endregion

                                                    #region--Số tiền chịu thuế--
                                                    if (!string.IsNullOrEmpty(text_SoTienChiuThue))
                                                    {
                                                        try
                                                        {
                                                            chiTiet.MucLuongMoi = Convert.ToDecimal(text_SoTienChiuThue);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine("+ Sai thông tin Số tiền chịu thuế");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine("Số tiền chịu thuế không có");
                                                    }
                                                    #endregion
                                                    //----Số tiền tự tính = Mức lương mới - Mức lương cũ
                                                    #endregion
                                                    listChiTiet.Add(chiTiet);
                                                }
                                                else if (!string.IsNullOrEmpty(text_MaChiTiet)
                                                    || !string.IsNullOrEmpty(text_SoTien))
                                                {
                                                    if (string.IsNullOrEmpty(text_MaChiTiet))
                                                        detailLog.AppendLine("Mã chi tiết không có");                                                   
                                                    if (string.IsNullOrEmpty(text_SoTien))
                                                        detailLog.AppendLine("Số tiền không có");
                                                }
                                                //trường hợp trống hết số tiền: bỏ qua, chỉ lấy hệ số
                                            }
                                        }
                                        else
                                        {
                                            #region Tạo mới TruyLuongNhanVienNew
                                            truyLuong = new TruyLuongNhanVienNew(uow);
                                            truyLuong.BangTruyLuong = uow.FindObject<BangTruyLuongNew>(CriteriaOperator.Parse("Oid = ?", bangTruyLuong.Oid));

                                            BoPhan boPhan;
                                            boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", text_BoPhan));
                                            if (boPhan != null)
                                                truyLuong.BoPhan = boPhan;

                                            truyLuong.ThongTinNhanVien = thongTinNhanVien;
                                            truyLuong.KyTinhLuong = kyTinhLuong;
                                            //Chọn Nhân viên + Kỳ tính lương => tự động lấy hệ số

                                            #region--HSL chênh lệch--
                                            if (!string.IsNullOrEmpty(text_HSL_ChenhLech))
                                            {
                                                try
                                                {
                                                    truyLuong.HeSoLuongChenhLech = Convert.ToDecimal(text_HSL_ChenhLech);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSL chênh lệch");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSL chênh lệch không có");
                                            //}
                                            #endregion                                       

                                            #region--HSCV chênh lệch--
                                            if (!string.IsNullOrEmpty(text_HSCV_ChenhLech))
                                            {
                                                try
                                                {
                                                    truyLuong.HSPCChucVuChenhLenh = Convert.ToDecimal(text_HSCV_ChenhLech);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSCV chênh lệch");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("HSCV mới không có");
                                            //}
                                            #endregion                                      

                                            #region--Hệ số ThamNien chênh lệch--
                                            if (!string.IsNullOrEmpty(text_HSThamNien_ChenhLech))
                                            {
                                                try
                                                {
                                                    truyLuong.HSPCThamNienChenhLech = Convert.ToDecimal(text_HSThamNien_ChenhLech);
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine("+ Sai thông tin HSThamNien chênh lệch");
                                                }
                                            }
                                            //else
                                            //{
                                            //    detailLog.AppendLine("ThamNien cũ không có");
                                            //}
                                            #endregion
                                         
                                            #region Tạo mới ChiTietTruyLuongNew
                                            if (!string.IsNullOrEmpty(text_MaChiTiet)
                                            && !string.IsNullOrEmpty(text_SoTien))
                                            {
                                                #region Tạo mới ChiTietTruyLuongNew
                                                chiTiet = new ChiTietTruyLuongNew(uow);
                                                chiTiet.TruyLuongNhanVien = truyLuong;
                                                #region--Mã chi tiết--
                                                if (!string.IsNullOrEmpty(text_MaChiTiet))
                                                {
                                                    chiTiet.MaChiTiet = text_MaChiTiet;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Mã chi tiết không có");
                                                }
                                                #endregion

                                                #region Cộng/trừ
                                                if (text_CongTru.Equals("Trừ"))
                                                { chiTiet.CongTru = CongTruEnum.Tru; }
                                                else
                                                { chiTiet.CongTru = CongTruEnum.Cong; }
                                                #endregion

                                                #region--Số tiền--
                                                if (!string.IsNullOrEmpty(text_SoTien))
                                                {
                                                    try
                                                    {
                                                        chiTiet.SoTien = Convert.ToDecimal(text_SoTien);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine("+ Sai thông tin Số tiền");
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine("Số tiền không có");
                                                }
                                                #endregion
                                               
                                                //----Số tiền tự tính = Mức lương mới - Mức lương cũ
                                                #endregion
                                                listChiTiet.Add(chiTiet);
                                            }
                                            else if (!string.IsNullOrEmpty(text_MaChiTiet)
                                                || !string.IsNullOrEmpty(text_SoTien))
                                            {
                                                if (string.IsNullOrEmpty(text_MaChiTiet))
                                                    detailLog.AppendLine("Mã chi tiết không có");
                                                if (string.IsNullOrEmpty(text_SoTien))
                                                    detailLog.AppendLine("Số tiền không có");                                                
                                            }
                                            //trường hợp trống hết số tiền: bỏ qua, chỉ lấy hệ số
                                            #endregion
                                            #endregion
                                            listTruyLuong.Add(truyLuong);
                                        }
                                    }

                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine("- STT: " + text_stt + " - Họ Tên:" + text_HoTen);
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                }

                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi") == DialogResult.Yes)
                                    {
                                        string tenFile = "Import_Log.txt";
                                        //FileStream fileStream = File.Open(tenFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
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
}
