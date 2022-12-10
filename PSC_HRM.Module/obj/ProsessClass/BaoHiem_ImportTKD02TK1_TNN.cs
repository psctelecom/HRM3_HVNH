using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.HoSo;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.ChamCong;
using System.Text;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module;
using System.Collections.Generic;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.CauHinh;
using System.IO;

namespace PSC_HRM.Module.Controllers
{
    public class BaoHiem_ImportTKD02TK1_TNN
    {
        public static bool XuLy(IObjectSpace obs, object obj)
        {
            bool oke = false;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    TKD02TK1_TNN TKD02TK1_TNN = obj as TKD02TK1_TNN;
                                    //
                                    int IDLaoDong = 0;
                                    int hoTen = 1;
                                    int maSoBHXH = 2;
                                    int loai = 3;
                                    int PA = 4;
                                    int CMND = 5;
                                    int dinhDangNgaySinh = 6;
                                    int ngaySinh = 7;
                                    int gioiTinh = 8;
                                    int coGiamChet = 9;
                                    int ngayChet = 10;
                                    int chucVu = 11;
                                    int tienLuong = 12;
                                    int phuCapCV = 13;
                                    int phuCapTNVK = 14;
                                    int phuCapTNNghe = 15;
                                    int phuCapLuong = 16;
                                    int phuCapBoSung = 17;
                                    int tuThang = 18;
                                    int denThang = 19;
                                    int ghiChu = 20;
                                    int tyLeDong = 21;
                                    int tinhLai = 22;
                                    int daCoSo = 23;
                                    int mucHuongBHYT = 24;
                                    int maPhongBan = 25;
                                    int noiLamViec = 26;
                                    int maVungSinhSong = 27;
                                    int maVungLuongToiThieu = 28;
                                    int VTVL_NQL_TuNgay = 29;
                                    int VTVL_NQL_DenNgay = 30;
                                    int VTVL_CMKTBC_TuNgay = 31;
                                    int VTVL_CMKTBC_DenNgay = 32;
                                    int VTVL_CMKTBT_TuNgay = 33;
                                    int VTVL_CMKTBT_DenNgay = 34;
                                    int VTVL_Khac_TuNgay = 35;
                                    int VTVL_Khac_DenNgay = 36;
                                    int NNDH_TuNgay = 37;
                                    int NNDH_DenNgay = 38;
                                    int HDLD_TuNgay = 39;
                                    int HDLD_XDTH_TuNgay = 40;
                                    int HDLD_XDTH_DenNgay = 41;
                                    int HDLD_Khac_TuNgay = 42;
                                    int HDLD_Khac_DenNgay = 43;
                                    int ADD_TK1_TS = 44;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String IDLaoDongText = item[IDLaoDong].ToString().FullTrim();
                                        String hoTenText = item[hoTen].ToString().FullTrim();
                                        String maSoBHXHText = item[maSoBHXH].ToString().FullTrim();
                                        String loaiText = item[loai].ToString().FullTrim();
                                        String PAText = item[PA].ToString().FullTrim();
                                        String CMNDText = item[CMND].ToString().FullTrim();
                                        String dinhDangNgaySinhText = item[dinhDangNgaySinh].ToString().FullTrim();
                                        String ngaySinhText = item[ngaySinh].ToString().FullTrim();
                                        String gioiTinhText = item[gioiTinh].ToString().FullTrim();
                                        String coGiamChetText = item[coGiamChet].ToString().FullTrim();
                                        String ngayChetText = item[ngayChet].ToString().FullTrim();
                                        String chucVuText = item[chucVu].ToString().FullTrim();
                                        String tienLuongText = item[tienLuong].ToString().FullTrim();
                                        String phuCapCVText = item[phuCapCV].ToString().FullTrim();
                                        String phuCapTNVKText = item[phuCapTNVK].ToString().FullTrim();
                                        String phuCapTNNgheText = item[phuCapTNNghe].ToString().FullTrim();
                                        String phuCapLuongText = item[phuCapLuong].ToString().FullTrim();
                                        String phuCapBoSungText = item[phuCapBoSung].ToString().FullTrim();
                                        String tuThangText = item[tuThang].ToString().FullTrim();
                                        String denThangText = item[denThang].ToString().FullTrim();
                                        String ghiChuText = item[ghiChu].ToString().FullTrim();
                                        String tyLeDongText = item[tyLeDong].ToString().FullTrim();
                                        String tinhLaiText = item[tinhLai].ToString().FullTrim();
                                        String daCoSoText = item[daCoSo].ToString().FullTrim();
                                        String mucHuongBHYTText = item[mucHuongBHYT].ToString().FullTrim();
                                        String maPhongBanText = item[maPhongBan].ToString().FullTrim();
                                        String noiLamViecText = item[noiLamViec].ToString().FullTrim();
                                        String maVungSinhSongText = item[maVungSinhSong].ToString().FullTrim();
                                        String maVungLuongToiThieuText = item[maVungLuongToiThieu].ToString().FullTrim();
                                        String VTVL_NQL_TuNgayText = item[VTVL_NQL_TuNgay].ToString().FullTrim();
                                        String VTVL_NQL_DenNgayText = item[VTVL_NQL_DenNgay].ToString().FullTrim();
                                        String VTVL_CMKTBC_TuNgayText = item[VTVL_CMKTBC_TuNgay].ToString().FullTrim();
                                        String VTVL_CMKTBC_DenNgayText = item[VTVL_CMKTBC_DenNgay].ToString().FullTrim();
                                        String VTVL_CMKTBT_TuNgayText = item[VTVL_CMKTBT_TuNgay].ToString().FullTrim();
                                        String VTVL_CMKTBT_DenNgayText = item[VTVL_CMKTBT_DenNgay].ToString().FullTrim();
                                        String VTVL_Khac_TuNgayText = item[VTVL_Khac_TuNgay].ToString().FullTrim();
                                        String VTVL_Khac_DenNgayText = item[VTVL_Khac_DenNgay].ToString().FullTrim();
                                        String NNDH_TuNgayText = item[NNDH_TuNgay].ToString().FullTrim();
                                        String NNDH_DenNgayText = item[NNDH_DenNgay].ToString().FullTrim();
                                        String HDLD_TuNgayText = item[HDLD_TuNgay].ToString().FullTrim();
                                        String HDLD_XDTH_TuNgayText = item[HDLD_XDTH_TuNgay].ToString().FullTrim();
                                        String HDLD_XDTH_DenNgayText = item[HDLD_XDTH_DenNgay].ToString().FullTrim();
                                        String HDLD_Khac_TuNgayText = item[HDLD_Khac_TuNgay].ToString().FullTrim();
                                        String HDLD_Khac_DenNgayText = item[HDLD_Khac_DenNgay].ToString().FullTrim();
                                        String ADD_TK1_TSText = item[ADD_TK1_TS].ToString().FullTrim();

                                        ChiTietTKD02TK1_TNN ChiTietTKD02TK1_TNN = uow.FindObject<ChiTietTKD02TK1_TNN>(CriteriaOperator.Parse("TKD02TK1_TNN=?", TKD02TK1_TNN.Oid));
                                        if (ChiTietTKD02TK1_TNN == null)
                                        {
                                            ChiTietTKD02TK1_TNN = new ChiTietTKD02TK1_TNN(uow);
                                            ChiTietTKD02TK1_TNN.TKD02TK1_TNN = uow.GetObjectByKey<TKD02TK1_TNN>(TKD02TK1_TNN.Oid);
                                        }

                                        #region ID lao động
                                        try
                                        {
                                            ChiTietTKD02TK1_TNN.IDLaoDong = IDLaoDongText;
                                        }
                                        catch
                                        {
                                            detailLog.AppendLine(" + ID lao động không hợp lệ: " + hoTenText);
                                        }
                                        #endregion

                                        #region Họ tên
                                        if (!string.IsNullOrEmpty(hoTenText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.HoTen = hoTenText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Họ tên không hợp lệ: " + hoTenText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Họ tên chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Số BHXH
                                        //if (!string.IsNullOrEmpty(maSoBHXHText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.MaSoBHXH = maSoBHXHText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Số BHXH không hợp lệ: " + maSoBHXHText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Số BHXH chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Loại khai báo
                                        if (!string.IsNullOrEmpty(loaiText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.Loai = loaiText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Loại khai báo không hợp lệ: " + loaiText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Loại khai báo chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Phương án
                                        if (!string.IsNullOrEmpty(PAText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.PA = PAText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phương án không hợp lệ: " + PAText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Phương án chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region CMND
                                        //if (!string.IsNullOrEmpty(CMNDText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.CMND = CMNDText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + CMND không hợp lệ: " + CMNDText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("CMND chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Định dạng ngày sinh
                                        if (!string.IsNullOrEmpty(dinhDangNgaySinhText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.DinhDangNgaySinh = dinhDangNgaySinhText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Định dạng ngày sinh không hợp lệ: " + dinhDangNgaySinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Định dạng ngày sinh chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Ngày sinh
                                        if (!string.IsNullOrEmpty(ngaySinhText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.NgaySinh = ngaySinhText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngaySinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Ngày sinh chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Giới tính
                                        if (!string.IsNullOrEmpty(gioiTinhText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.GioiTinh = gioiTinhText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Giới tính không hợp lệ: " + gioiTinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Giới tính chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Giảm do chết
                                        //if (!string.IsNullOrEmpty(coGiamChetText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.CoGiamChet = coGiamChetText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Giảm do chết không hợp lệ: " + coGiamChetText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Giảm do chết chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày chết
                                        //if (!string.IsNullOrEmpty(ngayChetText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.NgayChet = ngayChetText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày chết không hợp lệ: " + ngayChetText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày chết chưa có dữ liệu");
                                        //}
                                        #endregion


                                        #region Chức vụ
                                        if (!string.IsNullOrEmpty(chucVuText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.ChucVu = chucVuText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Chức vụ không hợp lệ: " + chucVuText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Chức vụ chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Tiền lương
                                        if (!string.IsNullOrEmpty(tienLuongText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.TienLuong = tienLuongText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Tiền lương không hợp lệ: " + tienLuongText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Tiền lương chưa có dữ liệu");
                                        }
                                        #endregion
                                        
                                        #region Phụ cấp CV
                                        //if (!string.IsNullOrEmpty(phuCapCVText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.PhuCapCV = phuCapCVText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phụ cấp CV không hợp lệ: " + phuCapCVText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Phụ cấp CV chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Phụ cấp TNVK
                                        if (!string.IsNullOrEmpty(phuCapTNVKText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.PhuCapTNVK = phuCapTNVKText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phụ cấp TNVK không hợp lệ: " + phuCapTNVKText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Phụ cấp TNVK chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Phụ cấp TNNghe
                                        if (!string.IsNullOrEmpty(phuCapTNNgheText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.PhuCapTNNghe = phuCapTNNgheText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phụ cấp TNNghe không hợp lệ: " + phuCapTNNgheText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Phụ cấp TNNghe chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Phụ cấp lương
                                        if (!string.IsNullOrEmpty(phuCapLuongText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.PhuCapLuong = phuCapLuongText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phụ cấp lương không hợp lệ: " + phuCapLuongText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Phụ cấp lương chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Phụ cấp bổ sung
                                        if (!string.IsNullOrEmpty(phuCapBoSungText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.PhuCapBoSung = phuCapBoSungText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phụ cấp bổ sung không hợp lệ: " + phuCapBoSungText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Phụ cấp bổ sung chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Từ tháng
                                        if (!string.IsNullOrEmpty(tuThangText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.TuThang = tuThangText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Từ tháng không hợp lệ: " + tuThangText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Từ tháng chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Đến tháng
                                        if (!string.IsNullOrEmpty(denThangText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.DenThang = denThangText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Đến tháng không hợp lệ: " + denThangText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Đến tháng chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Ghi chú
                                        if (!string.IsNullOrEmpty(ghiChuText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.GhiChu = ghiChuText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ghi chú không hợp lệ: " + ghiChuText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Ghi chú chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Tỷ lệ đóng
                                        if (!string.IsNullOrEmpty(tyLeDongText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.TyLeDong = tyLeDongText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Tỷ lệ đóng không hợp lệ: " + tyLeDongText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Tỷ lệ đóng chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Tính lãi
                                        //if (!string.IsNullOrEmpty(tinhLaiText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.TinhLai = tinhLaiText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Tính lãi không hợp lệ: " + tinhLaiText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Tính lãi chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Đã có sổ
                                        //if (!string.IsNullOrEmpty(daCoSoText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.DaCoSo = daCoSoText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Đã có sổ không hợp lệ: " + daCoSoText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Đã có sổ chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Mức hưởng BHYT
                                        //if (!string.IsNullOrEmpty(mucHuongBHYTText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.MucHuongBHYT = mucHuongBHYTText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Mức hưởng BHYT không hợp lệ: " + mucHuongBHYTText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Mức hưởng BHYT chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Phòng ban
                                        //if (!string.IsNullOrEmpty(maPhongBanText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.MaPhongBan = maPhongBanText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Phòng ban không hợp lệ: " + maPhongBanText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Phòng ban chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Nơi làm việc
                                        if (!string.IsNullOrEmpty(noiLamViecText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.NoiLamViec = noiLamViecText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Nơi làm việc không hợp lệ: " + noiLamViecText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Nơi làm việc chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Mã vùng sinh sống
                                        //if (!string.IsNullOrEmpty(maVungSinhSongText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.MaVungSinhSong = maVungSinhSongText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Mã vùng sinh sống không hợp lệ: " + maVungSinhSongText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Mã vùng sinh sống chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Mã vùng lương tối thiểu
                                        if (!string.IsNullOrEmpty(maVungLuongToiThieuText))
                                        {
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.MaVungLuongToiThieu = maVungLuongToiThieuText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Mã vùng lương tối thiểu không hợp lệ: " + maVungLuongToiThieuText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine("Mã vùng lương tối thiểu chưa có dữ liệu");
                                        }
                                        #endregion

                                        #region Ngày bắt đầu vị trí quản lý
                                        //if (!string.IsNullOrEmpty(VTVL_NQL_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_NQL_TuNgay = VTVL_NQL_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu vị trí quản lý không hợp lệ: " + VTVL_NQL_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu vị trí quản lý chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc vị trí quản lý
                                        //if (!string.IsNullOrEmpty(VTVL_NQL_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_NQL_DenNgay = VTVL_NQL_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc vị trí quản lý không hợp lệ: " + VTVL_NQL_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc vị trí quản lý chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc cao
                                        //if (!string.IsNullOrEmpty(VTVL_CMKTBC_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_CMKTBC_TuNgay = VTVL_CMKTBC_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc cao không hợp lệ: " + VTVL_CMKTBC_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc cao chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc vị trí chuyên môn kĩ thuật bậc cao
                                        //if (!string.IsNullOrEmpty(VTVL_CMKTBC_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_CMKTBC_DenNgay = VTVL_CMKTBC_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc vị trí chuyên môn kĩ thuật bậc cao không hợp lệ: " + VTVL_CMKTBC_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc vị trí chuyên môn kĩ thuật bậc cao chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc trung
                                        //if (!string.IsNullOrEmpty(VTVL_CMKTBT_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_CMKTBT_TuNgay = VTVL_CMKTBT_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc trung không hợp lệ: " + VTVL_CMKTBT_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu vị trí chuyên môn kĩ thuật bậc trung chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc vị trí chuyên môn kĩ thuật bậc trung
                                        //if (!string.IsNullOrEmpty(VTVL_CMKTBT_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_CMKTBT_DenNgay = VTVL_CMKTBT_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc vị trí chuyên môn kĩ thuật bậc trung không hợp lệ: " + VTVL_CMKTBT_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc vị trí chuyên môn kĩ thuật bậc trung chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu vị trí khác
                                        //if (!string.IsNullOrEmpty(VTVL_Khac_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_Khac_TuNgay = VTVL_Khac_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu vị trí khác không hợp lệ: " + VTVL_Khac_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu vị trí khác chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc vị trí khác
                                        //if (!string.IsNullOrEmpty(VTVL_Khac_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.VTVL_Khac_DenNgay = VTVL_Khac_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc vị trí khác không hợp lệ: " + VTVL_Khac_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc vị trí khác chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu ngành nghê độc hại
                                        //if (!string.IsNullOrEmpty(NNDH_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.NNDH_TuNgay = NNDH_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu ngành nghê độc hại không hợp lệ: " + NNDH_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu ngành nghê độc hại chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc ngành nghê độc hại
                                        //if (!string.IsNullOrEmpty(NNDH_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.NNDH_DenNgay = NNDH_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc ngành nghê độc hại không hợp lệ: " + NNDH_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc ngành nghê độc hại chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu HĐLĐ không xác định thời hạn
                                        //if (!string.IsNullOrEmpty(HDLD_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.HDLD_TuNgay = HDLD_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu HĐLĐ không xác định thời hạn không hợp lệ: " + HDLD_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu HĐLĐ không xác định thời hạn chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu HĐLĐ xác định thời hạn
                                        //if (!string.IsNullOrEmpty(HDLD_XDTH_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.HDLD_XDTH_TuNgay = HDLD_XDTH_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu HĐLĐ xác định thời hạn không hợp lệ: " + HDLD_XDTH_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu HĐLĐ xác định thời hạn chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc HĐLĐ xác định thời hạn
                                        //if (!string.IsNullOrEmpty(HDLD_XDTH_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.HDLD_XDTH_DenNgay = HDLD_XDTH_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc HĐLĐ xác định thời hạn không hợp lệ: " + HDLD_XDTH_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc HĐLĐ xác định thời hạn chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày bắt đầu HĐLĐ khác
                                        //if (!string.IsNullOrEmpty(HDLD_Khac_TuNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.HDLD_Khac_TuNgay = HDLD_Khac_TuNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày bắt đầu HĐLĐ khác không hợp lệ: " + HDLD_Khac_TuNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày bắt đầu HĐLĐ khác chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Ngày kết thúc HĐLĐ khác
                                        //if (!string.IsNullOrEmpty(HDLD_Khac_DenNgayText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.HDLD_Khac_DenNgay = HDLD_Khac_DenNgayText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày kết thúc HĐLĐ khác không hợp lệ: " + HDLD_Khac_DenNgayText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Ngày kết thúc HĐLĐ khác chưa có dữ liệu");
                                        //}
                                        #endregion

                                        #region Bổ sung TK1
                                        //if (!string.IsNullOrEmpty(ADD_TK1_TSText))
                                        //{
                                            try
                                            {
                                                ChiTietTKD02TK1_TNN.ADD_TK1_TS = ADD_TK1_TSText;
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Bổ sung TK1 không hợp lệ: " + ADD_TK1_TSText);
                                            }
                                        //}
                                        //else
                                        //{
                                        //    detailLog.AppendLine("Bổ sung TK1 chưa có dữ liệu");
                                        //}
                                        #endregion

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", hoTen));
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

                                //Xuất thông báo lỗi
                                oke = false;
                            }
                            else
                            {
                                //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                uow.CommitChanges();
                                //Xuất thông báo thành công
                                oke = true;
                            }

                        }
                    }
                }
            }
            return oke;
        }
    }
}

