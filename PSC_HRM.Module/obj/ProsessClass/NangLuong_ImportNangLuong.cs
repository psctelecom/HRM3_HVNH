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

namespace PSC_HRM.Module.Controllers
{
    public class NangLuong_ImportNangLuong
    {
        public static void XuLy(IObjectSpace obs, DeNghiNangLuong deNghiNangLuong)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {

                        ChiTietDeNghiNangLuong chiTietDeNghi;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (DialogUtil.AutoWait())
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    //
                                    int idx_SoQuyetDinh = 0;
                                    int idx_MaQuanLy = 1;
                                    int idx_NgachLuong = 4;
                                    int idx_BacLuongCu = 5;
                                    int idx_VuotKhungCu = 7;
                                    int idx_NgayHuongLuongCu = 8;
                                    int idx_MocNangLuongCu = 9;
                                    int idx_BacLuongMoi = 10;
                                    int idx_VuotKhungMoi = 12;
                                    int idx_NgayHuongLuongMoi = 13;
                                    int idx_MocNangLuongMoi = 14;
                                    int idx_NangLuongTruocHan = 15;
                                    int idx_NangLuongTruocNghiHuu = 16;

                                    //Tìm nhân viên theo mã quản lý
                                    nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? ", item[idx_MaQuanLy].ToString().Trim()));
                                    if (nhanVien != null)
                                    {
                                        chiTietDeNghi = obs.FindObject<ChiTietDeNghiNangLuong>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiNangLuong = ?", nhanVien.Oid, deNghiNangLuong.Oid));
                                        if (chiTietDeNghi == null)
                                        {
                                            chiTietDeNghi = new ChiTietDeNghiNangLuong(((XPObjectSpace)obs).Session);
                                            chiTietDeNghi.DeNghiNangLuong = obs.GetObjectByKey<DeNghiNangLuong>(deNghiNangLuong.Oid);
                                            chiTietDeNghi.BoPhan = nhanVien.BoPhan;
                                            chiTietDeNghi.ThongTinNhanVien = nhanVien;
                                        }

                                        #region Số quyết định
                                        if (!item.IsNull(idx_SoQuyetDinh) && !string.IsNullOrEmpty(item[idx_SoQuyetDinh].ToString()))
                                        {
                                            chiTietDeNghi.SoQuyetDinh = item[idx_SoQuyetDinh].ToString().Trim();
                                        }
                                        #endregion

                                        #region Ngạch lương
                                        if (!item.IsNull(idx_NgachLuong) && !string.IsNullOrEmpty(item[idx_NgachLuong].ToString()))
                                        {

                                            NgachLuong ngachLuong = obs.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuong].ToString().Trim()));
                                            if (ngachLuong != null)
                                            {
                                                //
                                                chiTietDeNghi.NgachLuong = ngachLuong;

                                                //Lấy thông tin cũ từ nhân viên thông tin lương vì khi set ngạch lương thì bậc lương cũ = null
                                                if (nhanVien.NhanVienThongTinLuong.BacLuong != null)
                                                    chiTietDeNghi.BacLuongCu = obs.GetObjectByKey<BacLuong>(nhanVien.NhanVienThongTinLuong.BacLuong.Oid);

                                                //Bậc lương cũ - Hệ số lương cũ
                                                if (!item.IsNull(idx_BacLuongCu) && !string.IsNullOrEmpty(item[idx_BacLuongCu].ToString()))
                                                {
                                                    BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? ", ngachLuong, item[idx_BacLuongCu].ToString().Trim()));
                                                    if (bacLuong != null)
                                                    {
                                                        chiTietDeNghi.BacLuongCu = bacLuong;
                                                        chiTietDeNghi.HeSoLuongCu = bacLuong.HeSoLuong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_BacLuongCu].ToString());
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_NgachLuong].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                        }
                                        #endregion

                                        #region % vượt khung cũ
                                        if (!item.IsNull(idx_VuotKhungCu) && !string.IsNullOrEmpty(item[idx_VuotKhungCu].ToString()))
                                        {
                                            try
                                            {
                                                chiTietDeNghi.VuotKhungCu = Convert.ToInt32(item[idx_VuotKhungCu].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + % vượt khung cũ không hợp lệ:" + item[idx_VuotKhungCu].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Ngày hưởng lương cũ
                                        if (!item.IsNull(idx_NgayHuongLuongCu) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongCu].ToString()))
                                        {
                                            try
                                            {
                                                DateTime ngayHuongLuongCu = Convert.ToDateTime(item[idx_NgayHuongLuongCu].ToString().Trim());
                                                if (ngayHuongLuongCu != null && ngayHuongLuongCu != DateTime.MinValue)
                                                    chiTietDeNghi.NgayHuongLuongCu = ngayHuongLuongCu;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Ngày hưởng lương cũ không hợp lệ:" + item[idx_NgayHuongLuongCu].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Mốc nâng lương cũ
                                        if (!item.IsNull(idx_MocNangLuongCu) && !string.IsNullOrEmpty(item[idx_MocNangLuongCu].ToString()))
                                        {
                                            try
                                            {
                                                DateTime mocNangLuongCu = Convert.ToDateTime(item[idx_MocNangLuongCu].ToString().Trim());
                                                if (mocNangLuongCu != null && mocNangLuongCu != DateTime.MinValue)
                                                    chiTietDeNghi.MocNangLuongCu = mocNangLuongCu;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Mốc nâng lương cũ không hợp lệ:" + item[idx_MocNangLuongCu].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Bậc lương mới - Hệ số lương mới
                                        if (!item.IsNull(idx_BacLuongMoi) && !string.IsNullOrEmpty(item[idx_BacLuongMoi].ToString()))
                                        {
                                            BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", chiTietDeNghi.NgachLuong, item[idx_BacLuongMoi].ToString().Trim()));
                                            if (bacLuong != null)
                                            {
                                                chiTietDeNghi.BacLuongMoi = bacLuong;
                                                chiTietDeNghi.HeSoLuongMoi = bacLuong.HeSoLuong;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Bậc lương mới không hợp lệ:" + item[idx_BacLuongMoi].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Bậc lương mới không tìm thấy.");
                                        }
                                        #endregion

                                        #region % vượt khung mới
                                        if (!item.IsNull(idx_VuotKhungMoi) && !string.IsNullOrEmpty(item[idx_VuotKhungMoi].ToString()))
                                        {
                                            try
                                            {
                                                chiTietDeNghi.VuotKhungMoi = Convert.ToInt32(item[idx_VuotKhungMoi].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + % vượt khung mới không hợp lệ:" + item[idx_VuotKhungMoi].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Ngày hưởng lương mới
                                        if (!item.IsNull(idx_NgayHuongLuongMoi) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongMoi].ToString()))
                                        {
                                            try
                                            {
                                                DateTime ngayHuongLuongMoi = Convert.ToDateTime(item[idx_NgayHuongLuongMoi].ToString().Trim());
                                                if (ngayHuongLuongMoi != null && ngayHuongLuongMoi != DateTime.MinValue)
                                                    chiTietDeNghi.NgayHuongLuongMoi = ngayHuongLuongMoi;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Ngày hưởng lương mới không hợp lệ:" + item[idx_NgayHuongLuongMoi].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng lương mới không tìm thấy.");
                                        }
                                        #endregion

                                        #region Mốc nâng lương mới
                                        if (!item.IsNull(idx_MocNangLuongMoi) && !string.IsNullOrEmpty(item[idx_MocNangLuongMoi].ToString()))
                                        {
                                            try
                                            {
                                                DateTime mocNangLuongMoi = Convert.ToDateTime(item[idx_MocNangLuongMoi].ToString().Trim());
                                                if (mocNangLuongMoi != null && mocNangLuongMoi != DateTime.MinValue)
                                                    chiTietDeNghi.MocNangLuongMoi = mocNangLuongMoi;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Mốc nâng lương mới không hợp lệ:" + item[idx_MocNangLuongMoi].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Mốc nâng lương mới không tìm thấy.");
                                        }
                                        #endregion

                                        #region Nâng lương trước hạn or Nâng lương trước khi nghỉ hưu
                                        if (!item.IsNull(idx_NangLuongTruocHan) && !string.IsNullOrEmpty(item[idx_NangLuongTruocHan].ToString()))
                                        {
                                            if (item[idx_NangLuongTruocHan].ToString().ToLower().Equals("x"))
                                                chiTietDeNghi.PhanLoai = NangLuongEnum.CoThanhTichXuatSac;
                                            else
                                                detailLog.AppendLine(" + Nâng lương trước hạn không hợp lệ:" + item[idx_NangLuongTruocHan].ToString());
                                        }
                                        else if (!item.IsNull(idx_NangLuongTruocNghiHuu) && !string.IsNullOrEmpty(item[idx_NangLuongTruocNghiHuu].ToString()))
                                        {
                                            if (item[idx_NangLuongTruocNghiHuu].ToString().ToLower().Equals("x"))
                                                chiTietDeNghi.PhanLoai = NangLuongEnum.TruocKhiNghiHuu;
                                            else
                                                detailLog.AppendLine(" + Nâng lương trước khi nghỉ hưu không hợp lệ:" + item[idx_NangLuongTruocNghiHuu].ToString());
                                        }
                                        else
                                        {
                                            chiTietDeNghi.PhanLoai = NangLuongEnum.ThuongXuyen;
                                        }
                                        #endregion
                                        
                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                        else
                                            deNghiNangLuong.ListChiTietDeNghiNangLuong.Add(chiTietDeNghi);
                                    }
                                    else
                                    {
                                        mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", item[idx_MaQuanLy].ToString().Trim()));
                                    }
                                }
                            }
                        }
                        //
                        if (mainLog.Length > 0)
                        {
                            //Tiến hành trả lại dữ liệu không import vào phần mền
                            deNghiNangLuong.ListChiTietDeNghiNangLuong.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin nâng lương. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = "Text files (*.txt)|*.txt";

                                    if (saveFile.ShowDialog() == DialogResult.OK)
                                    {
                                        HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                        }

                    }
                }
            }
        }

        public static void XuLy_NEU(IObjectSpace obs, DeNghiNangLuong deNghiNangLuong)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A1:Q]"))
                    {
                        ChiTietDeNghiNangLuong chiTietDeNghi;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (DialogUtil.AutoWait())
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    //
                                    int idx_SoHieuCongChuc = 0;
                                    int idx_NgachLuong = 3;
                                    int idx_BacLuongCu = 4;                                  
                                    int idx_VuotKhungCu = 6;                                   
                                    int idx_NgayHuongLuongCu = 7;
                                    int idx_MocNangLuongCu = 8;
                                    int idx_BacLuongMoi = 9;
                                    int idx_VuotKhungMoi = 11;                                   
                                    int idx_NgayHuongLuongMoi = 12;
                                    int idx_MocNangLuongMoi = 13;
                                    int idx_NangLuongTruocHan = 14;
                                    int idx_NangLuongTruocNghiHuu = 15;
                                    int idx_GhiChu = 16;

                                    //Tìm nhân viên theo mã quản lý
                                    nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("SoHieuCongChuc=? ", item[idx_SoHieuCongChuc].ToString().Trim()));
                                    if (nhanVien != null)
                                    {
                                        chiTietDeNghi = obs.FindObject<ChiTietDeNghiNangLuong>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiNangLuong = ?", nhanVien.Oid, deNghiNangLuong.Oid));
                                        if (chiTietDeNghi == null)
                                        {
                                            chiTietDeNghi = new ChiTietDeNghiNangLuong(((XPObjectSpace)obs).Session);
                                            chiTietDeNghi.DeNghiNangLuong = obs.GetObjectByKey<DeNghiNangLuong>(deNghiNangLuong.Oid);
                                            chiTietDeNghi.BoPhan = nhanVien.BoPhan;
                                            chiTietDeNghi.ThongTinNhanVien = nhanVien;
                                        }

                                        #region Ngạch lương
                                        if (!item.IsNull(idx_NgachLuong) && !string.IsNullOrEmpty(item[idx_NgachLuong].ToString()))
                                        {

                                            NgachLuong ngachLuong = obs.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuong].ToString().Trim()));
                                            if (ngachLuong != null)
                                            {
                                                //
                                                chiTietDeNghi.NgachLuong = ngachLuong;

                                                //Lấy thông tin cũ từ nhân viên thông tin lương vì khi set ngạch lương thì bậc lương cũ = null
                                                if (nhanVien.NhanVienThongTinLuong.BacLuong != null)
                                                    chiTietDeNghi.BacLuongCu = obs.GetObjectByKey<BacLuong>(nhanVien.NhanVienThongTinLuong.BacLuong.Oid);

                                                //Bậc lương cũ - Hệ số lương cũ
                                                if (!item.IsNull(idx_BacLuongCu) && !string.IsNullOrEmpty(item[idx_BacLuongCu].ToString()))
                                                {
                                                    BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and TenBacLuong like ? ", ngachLuong, item[idx_BacLuongCu].ToString().Trim()));
                                                    if (bacLuong != null)
                                                    {
                                                        chiTietDeNghi.BacLuongCu = bacLuong;
                                                        chiTietDeNghi.HeSoLuongCu = bacLuong.HeSoLuong;
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_BacLuongCu].ToString());
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_NgachLuong].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                        }
                                        #endregion

                                        #region % vượt khung cũ
                                        if (!item.IsNull(idx_VuotKhungCu) && !string.IsNullOrEmpty(item[idx_VuotKhungCu].ToString()))
                                        {
                                            try
                                            {
                                                chiTietDeNghi.VuotKhungCu = Convert.ToInt32(item[idx_VuotKhungCu].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + % vượt khung cũ không hợp lệ:" + item[idx_VuotKhungCu].ToString());
                                            }
                                        }
                                        #endregion                                       

                                        #region Ngày hưởng lương cũ
                                        if (!item.IsNull(idx_NgayHuongLuongCu) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongCu].ToString()))
                                        {
                                            try
                                            {
                                                DateTime ngayHuongLuongCu = Convert.ToDateTime(item[idx_NgayHuongLuongCu].ToString().Trim());
                                                if (ngayHuongLuongCu != null && ngayHuongLuongCu != DateTime.MinValue)
                                                {
                                                    chiTietDeNghi.NgayHuongLuongCu = ngayHuongLuongCu;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Ngày hưởng lương cũ không hợp lệ:" + item[idx_NgayHuongLuongCu].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Mốc nâng lương cũ
                                        if (!item.IsNull(idx_MocNangLuongCu) && !string.IsNullOrEmpty(item[idx_MocNangLuongCu].ToString()))
                                        {
                                            try
                                            {
                                                DateTime mocNangLuongCu = Convert.ToDateTime(item[idx_MocNangLuongCu].ToString().Trim());
                                                if (mocNangLuongCu != null && mocNangLuongCu != DateTime.MinValue)
                                                {
                                                    chiTietDeNghi.MocNangLuongCu = mocNangLuongCu;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Mốc nâng lương cũ không hợp lệ:" + item[idx_MocNangLuongCu].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Bậc lương mới - Hệ số lương mới
                                        if (!item.IsNull(idx_BacLuongMoi) && !string.IsNullOrEmpty(item[idx_BacLuongMoi].ToString()))
                                        {
                                            BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and TenBacLuong like ? and BacLuongCu = false", chiTietDeNghi.NgachLuong, item[idx_BacLuongMoi].ToString().Trim()));
                                            if (bacLuong != null)
                                            {
                                                chiTietDeNghi.BacLuongMoi = bacLuong;
                                                chiTietDeNghi.HeSoLuongMoi = bacLuong.HeSoLuong;
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Bậc lương mới không hợp lệ:" + item[idx_BacLuongMoi].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Bậc lương mới không tìm thấy.");
                                        }
                                        #endregion

                                        #region % vượt khung mới
                                        if (!item.IsNull(idx_VuotKhungMoi) && !string.IsNullOrEmpty(item[idx_VuotKhungMoi].ToString()))
                                        {
                                            try
                                            {
                                                chiTietDeNghi.VuotKhungMoi = Convert.ToInt32(item[idx_VuotKhungMoi].ToString().Trim());
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + % vượt khung mới không hợp lệ:" + item[idx_VuotKhungMoi].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Ngày hưởng lương mới
                                        if (!item.IsNull(idx_NgayHuongLuongMoi) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongMoi].ToString()))
                                        {
                                            try
                                            {
                                                DateTime ngayHuongLuongMoi = Convert.ToDateTime(item[idx_NgayHuongLuongMoi].ToString().Trim());
                                                if (ngayHuongLuongMoi != null && ngayHuongLuongMoi != DateTime.MinValue)
                                                {
                                                    chiTietDeNghi.NgayHuongLuongMoi = ngayHuongLuongMoi;
                                                    chiTietDeNghi.MocNangLuongMoi = ngayHuongLuongMoi;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Ngày hưởng lương mới không hợp lệ:" + item[idx_NgayHuongLuongMoi].ToString());
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Ngày hưởng lương mới không tìm thấy.");
                                        }
                                        #endregion

                                        #region Mốc nâng lương mới
                                        if (!item.IsNull(idx_MocNangLuongMoi) && !string.IsNullOrEmpty(item[idx_MocNangLuongMoi].ToString()))
                                        {
                                            try
                                            {
                                                DateTime mocNangLuongMoi = Convert.ToDateTime(item[idx_MocNangLuongMoi].ToString().Trim());
                                                if (mocNangLuongMoi != null && mocNangLuongMoi != DateTime.MinValue)
                                                {
                                                    chiTietDeNghi.MocNangLuongCu = mocNangLuongMoi;
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Mốc nâng lương mới không hợp lệ:" + item[idx_MocNangLuongCu].ToString());
                                            }
                                        }
                                        #endregion

                                        #region Nâng lương trước hạn or Nâng lương trước khi nghỉ hưu
                                        if (!item.IsNull(idx_NangLuongTruocHan) && !string.IsNullOrEmpty(item[idx_NangLuongTruocHan].ToString()))
                                        {
                                            if (item[idx_NangLuongTruocHan].ToString().ToLower().Equals("x"))
                                                chiTietDeNghi.PhanLoai = NangLuongEnum.CoThanhTichXuatSac;
                                            else
                                                detailLog.AppendLine(" + Nâng lương trước hạn không hợp lệ:" + item[idx_NangLuongTruocHan].ToString());
                                        }
                                        else if (!item.IsNull(idx_NangLuongTruocNghiHuu) && !string.IsNullOrEmpty(item[idx_NangLuongTruocNghiHuu].ToString()))
                                        {
                                            if (item[idx_NangLuongTruocNghiHuu].ToString().ToLower().Equals("x"))
                                                chiTietDeNghi.PhanLoai = NangLuongEnum.TruocKhiNghiHuu;
                                            else
                                                detailLog.AppendLine(" + Nâng lương trước khi nghỉ hưu không hợp lệ:" + item[idx_NangLuongTruocNghiHuu].ToString());
                                        }
                                        else
                                        {
                                            chiTietDeNghi.PhanLoai = NangLuongEnum.ThuongXuyen;
                                        }
                                        #endregion

                                        #region Ghi chú
                                        if (!string.IsNullOrEmpty(item[idx_GhiChu].ToString()))
                                        {
                                            chiTietDeNghi.GhiChu = item[idx_GhiChu].ToString();
                                        }
                                        #endregion

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}]-[{1}] vào được: ", nhanVien.SoHieuCongChuc, nhanVien.HoTen));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                        else
                                            deNghiNangLuong.ListChiTietDeNghiNangLuong.Add(chiTietDeNghi);
                                    }
                                    else
                                    {
                                        mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", item[idx_SoHieuCongChuc].ToString().Trim()));
                                    }
                                }
                            }
                        }
                        //
                        if (mainLog.Length > 0)
                        {
                            //Tiến hành trả lại dữ liệu không import vào phần mền
                            deNghiNangLuong.ListChiTietDeNghiNangLuong.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin nâng lương. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = "Text files (*.txt)|*.txt";

                                    if (saveFile.ShowDialog() == DialogResult.OK)
                                    {
                                        HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                        }

                    }
                }
            }
        }

        public static void XuLy_HBU(IObjectSpace obs, DeNghiNangLuong deNghiNangLuong)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:G]"))
                    {

                        ChiTietDeNghiNangLuong chiTietDeNghi;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (DialogUtil.AutoWait())
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                //
                                int idx_SoQuyetDinh = 0;
                                int idx_MaQuanLy = 1;
                                int idx_HoTen = 2;
                                int idx_NgayHuongLuongMoi = 3;
                                int idx_MucLuongMoi = 4;
                                int idx_ThuongHieuQuaTheoThangMoi = 5;


                                foreach (DataRow item in dt.Rows)
                                {
                                    //Khởi tạo bộ nhớ đệm
                                    detailLog = new StringBuilder();

                                    String soQuyetDinh = item[idx_SoQuyetDinh].ToString().FullTrim();
                                    String maQuanLy = item[idx_MaQuanLy].ToString().FullTrim();
                                    String hoTen = item[idx_HoTen].ToString().FullTrim();
                                    String ngayHuongLuongMoi = item[idx_NgayHuongLuongMoi].ToString().FullTrim();
                                    String mucLuongMoi = item[idx_MucLuongMoi].ToString().FullTrim();
                                    String thuongHieuQuaTheoThangMoi = item[idx_ThuongHieuQuaTheoThangMoi].ToString().FullTrim();


                                    //Tìm nhân viên theo mã quản lý
                                    nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? and HoTen=?", maQuanLy,hoTen));
                                    if (nhanVien != null)
                                    {
                                        chiTietDeNghi = obs.FindObject<ChiTietDeNghiNangLuong>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiNangLuong = ?", nhanVien.Oid, deNghiNangLuong.Oid));
                                        if (chiTietDeNghi == null)
                                        {
                                            chiTietDeNghi = new ChiTietDeNghiNangLuong(((XPObjectSpace)obs).Session);
                                            chiTietDeNghi.DeNghiNangLuong = obs.GetObjectByKey<DeNghiNangLuong>(deNghiNangLuong.Oid);
                                            chiTietDeNghi.BoPhan = nhanVien.BoPhan;
                                            chiTietDeNghi.ThongTinNhanVien = nhanVien;
                                        }
                                        
                                        #region Số quyết định
                                        if (!string.IsNullOrEmpty(soQuyetDinh))
                                        {
                                            chiTietDeNghi.SoQuyetDinh = soQuyetDinh;
                                        }
                                        #endregion

                                        #region Ngày hưởng lương mới
                                        {

                                            if (!string.IsNullOrEmpty(ngayHuongLuongMoi))
                                            {
                                                try
                                                {
                                                    chiTietDeNghi.NgayHuongLuongMoi = Convert.ToDateTime(ngayHuongLuongMoi);
                                                }
                                                catch
                                                {
                                                    detailLog.AppendLine(" + Ngày ngày điều chỉnh mức thu nhập mới không hợp lệ: " + ngayHuongLuongMoi);
                                                }
                                            }
                                        }
                                        #endregion

                                        #region Mức lương mới
                                        if (!string.IsNullOrEmpty(mucLuongMoi))
                                        {
                                            try
                                            {
                                                chiTietDeNghi.MucLuongMoi = Convert.ToInt32(mucLuongMoi);
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Mức lương mới không hợp lệ:" + mucLuongMoi);
                                            }
                                        }
                                        #endregion

                                        #region Thưởng hiệu quả theo tháng mới
                                        if (!string.IsNullOrEmpty(thuongHieuQuaTheoThangMoi))
                                        {
                                            try
                                            {
                                                chiTietDeNghi.ThuongHieuQuaTheoThangMoi = Convert.ToInt32(thuongHieuQuaTheoThangMoi);
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.AppendLine(" + Thưởng hiệu quả theo tháng mới không hợp lệ:" + thuongHieuQuaTheoThangMoi);
                                            }
                                        }
                                        #endregion

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                            mainLog.AppendLine(detailLog.ToString());
                                        }
                                        else
                                            deNghiNangLuong.ListChiTietDeNghiNangLuong.Add(chiTietDeNghi);
                                    }
                                    else
                                    {
                                        mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", maQuanLy));
                                    }
                                }
                            }
                        }
                        //
                        if (mainLog.Length > 0)
                        {
                            //Tiến hành trả lại dữ liệu không import vào phần mền
                            deNghiNangLuong.ListChiTietDeNghiNangLuong.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin nâng lương. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = "Text files (*.txt)|*.txt";

                                    if (saveFile.ShowDialog() == DialogResult.OK)
                                    {
                                        HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Xuất thông báo thành công
                            DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                        }

                    }
                }
            }
        }

        public static void XuLy_BUH(IObjectSpace obs, DeNghiNangLuong deNghiNangLuong)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {

                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet$]"))
                    {

                        ChiTietDeNghiNangLuong chiTietDeNghi;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        using (DialogUtil.AutoWait())
                        {
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();

                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        int idx_SoQuyetDinh = 1;
                                        int idx_NgayQuyetDinh = 2;
                                        int idx_MaQuanLy = 3;
                                        int idx_NgachLuong = 5;
                                        int idx_BacLuongCu = 6;
                                        int idx_VuotKhungCu = 8;
                                        int idx_NgayHuongLuongCu = 9;
                                        int idx_BacLuongMoi = 10;
                                        int idx_VuotKhungMoi = 12;
                                        int idx_NgayHuongLuongMoi = 13;
                                        int idx_NangLuongTruocHan = 14;
                                        int idx_NangLuongTruocNghiHuu = 15;
                                        int idx_SoThang = 16;

                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? ", item[idx_MaQuanLy].ToString().Trim()));
                                        if (nhanVien != null)
                                        {
                                            chiTietDeNghi = obs.FindObject<ChiTietDeNghiNangLuong>(CriteriaOperator.Parse("ThongTinNhanVien = ? and DeNghiNangLuong = ?", nhanVien.Oid, deNghiNangLuong.Oid));
                                            if (chiTietDeNghi == null)
                                            {
                                                chiTietDeNghi = new ChiTietDeNghiNangLuong(((XPObjectSpace)obs).Session);
                                                chiTietDeNghi.DeNghiNangLuong = obs.GetObjectByKey<DeNghiNangLuong>(deNghiNangLuong.Oid);
                                                chiTietDeNghi.BoPhan = nhanVien.BoPhan;
                                                chiTietDeNghi.ThongTinNhanVien = nhanVien;
                                            }

                                            #region Ngày quyết định
                                            if (!item.IsNull(idx_NgayQuyetDinh) && !string.IsNullOrEmpty(item[idx_NgayQuyetDinh].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime ngayQuyetDinh = Convert.ToDateTime(item[idx_NgayQuyetDinh].ToString().Trim());
                                                    if (ngayQuyetDinh != null && ngayQuyetDinh != DateTime.MinValue)
                                                    {
                                                        chiTietDeNghi.NgayQuyetDinh = ngayQuyetDinh;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Ngày quyết định:" + item[idx_NgayQuyetDinh].ToString());
                                                }
                                            }
                                            #endregion


                                            #region Số quyết định
                                            if (!item.IsNull(idx_SoQuyetDinh) && !string.IsNullOrEmpty(item[idx_SoQuyetDinh].ToString()))
                                            {
                                                chiTietDeNghi.SoQuyetDinh = item[idx_SoQuyetDinh].ToString().Trim();
                                            }
                                            #endregion

                                            #region Ngạch lương
                                            if (!item.IsNull(idx_NgachLuong) && !string.IsNullOrEmpty(item[idx_NgachLuong].ToString()))
                                            {

                                                NgachLuong ngachLuong = obs.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_NgachLuong].ToString().Trim()));
                                                if (ngachLuong != null)
                                                {
                                                    //
                                                    chiTietDeNghi.NgachLuong = ngachLuong;

                                                    //Lấy thông tin cũ từ nhân viên thông tin lương vì khi set ngạch lương thì bậc lương cũ = null
                                                    if (nhanVien.NhanVienThongTinLuong.BacLuong != null)
                                                        chiTietDeNghi.BacLuongCu = obs.GetObjectByKey<BacLuong>(nhanVien.NhanVienThongTinLuong.BacLuong.Oid);

                                                    //Bậc lương cũ - Hệ số lương cũ
                                                    if (!item.IsNull(idx_BacLuongCu) && !string.IsNullOrEmpty(item[idx_BacLuongCu].ToString()))
                                                    {
                                                        BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and TenBacLuong = ? and BacLuongCu = false", ngachLuong, item[idx_BacLuongCu].ToString().Trim()));
                                                        if (bacLuong == null)
                                                        {
                                                            bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", ngachLuong, item[idx_BacLuongCu].ToString().Trim()));
                                                        }
                                                        if (bacLuong != null)
                                                        {
                                                            chiTietDeNghi.BacLuongCu = bacLuong;
                                                            chiTietDeNghi.HeSoLuongCu = bacLuong.HeSoLuong;
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_BacLuongCu].ToString());
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngạch lương không hợp lệ:" + item[idx_NgachLuong].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Ngạch lương không tìm thấy.");
                                            }
                                            #endregion

                                            #region % vượt khung cũ
                                            if (!item.IsNull(idx_VuotKhungCu) && !string.IsNullOrEmpty(item[idx_VuotKhungCu].ToString()))
                                            {
                                                try
                                                {
                                                    chiTietDeNghi.VuotKhungCu = Convert.ToInt32(item[idx_VuotKhungCu].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + % vượt khung cũ không hợp lệ:" + item[idx_VuotKhungCu].ToString());
                                                }
                                            }
                                            #endregion

                                            #region Ngày hưởng lương cũ
                                            if (!item.IsNull(idx_NgayHuongLuongCu) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongCu].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime ngayHuongLuongCu = Convert.ToDateTime(item[idx_NgayHuongLuongCu].ToString().Trim());
                                                    if (ngayHuongLuongCu != null && ngayHuongLuongCu != DateTime.MinValue)
                                                    {
                                                        chiTietDeNghi.NgayHuongLuongCu = ngayHuongLuongCu;
                                                        chiTietDeNghi.MocNangLuongCu = ngayHuongLuongCu;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng lương cũ không hợp lệ:" + item[idx_NgayHuongLuongCu].ToString());
                                                }
                                            }
                                            #endregion

                                            #region Bậc lương mới - Hệ số lương mới
                                            if (!item.IsNull(idx_BacLuongMoi) && !string.IsNullOrEmpty(item[idx_BacLuongMoi].ToString()))
                                            {
                                                BacLuong bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and TenBacLuong = ? and BacLuongCu = false", chiTietDeNghi.NgachLuong, item[idx_BacLuongMoi].ToString().Trim()));
                                                if (bacLuong == null)
                                                {
                                                    bacLuong = obs.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", chiTietDeNghi.NgachLuong, item[idx_BacLuongMoi].ToString().Trim()));
                                                }
                                                if (bacLuong != null)
                                                {
                                                    chiTietDeNghi.BacLuongMoi = bacLuong;
                                                    chiTietDeNghi.HeSoLuongMoi = bacLuong.HeSoLuong;
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Bậc lương mới không hợp lệ:" + item[idx_BacLuongMoi].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Bậc lương mới không tìm thấy.");
                                            }
                                            #endregion

                                            #region % vượt khung mới
                                            if (!item.IsNull(idx_VuotKhungMoi) && !string.IsNullOrEmpty(item[idx_VuotKhungMoi].ToString()))
                                            {
                                                try
                                                {
                                                    chiTietDeNghi.VuotKhungMoi = Convert.ToInt32(item[idx_VuotKhungMoi].ToString().Trim());
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + % vượt khung mới không hợp lệ:" + item[idx_VuotKhungMoi].ToString());
                                                }
                                            }
                                            #endregion

                                            #region Ngày hưởng lương mới
                                            if (!item.IsNull(idx_NgayHuongLuongMoi) && !string.IsNullOrEmpty(item[idx_NgayHuongLuongMoi].ToString()))
                                            {
                                                try
                                                {
                                                    DateTime ngayHuongLuongMoi = Convert.ToDateTime(item[idx_NgayHuongLuongMoi].ToString().Trim());
                                                    if (ngayHuongLuongMoi != null && ngayHuongLuongMoi != DateTime.MinValue)
                                                    {
                                                        chiTietDeNghi.NgayHuongLuongMoi = ngayHuongLuongMoi;
                                                        chiTietDeNghi.MocNangLuongMoi = ngayHuongLuongMoi;
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng lương mới không hợp lệ:" + item[idx_NgayHuongLuongMoi].ToString());
                                                }
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(" + Ngày hưởng lương mới không tìm thấy.");
                                            }
                                            #endregion


                                            #region Nâng lương trước hạn or Nâng lương trước khi nghỉ hưu
                                            if (!item.IsNull(idx_NangLuongTruocHan) && !string.IsNullOrEmpty(item[idx_NangLuongTruocHan].ToString()))
                                            {
                                                if (item[idx_NangLuongTruocHan].ToString().ToLower().Equals("x"))
                                                    chiTietDeNghi.PhanLoai = NangLuongEnum.CoThanhTichXuatSac;
                                                else
                                                {
                                                    chiTietDeNghi.PhanLoai = NangLuongEnum.CoThanhTichXuatSac;
                                                    chiTietDeNghi.LyDo = item[idx_NangLuongTruocHan].ToString();

                                                    if (!item.IsNull(idx_SoThang) && !string.IsNullOrEmpty(item[idx_SoThang].ToString()))
                                                    {
                                                        try
                                                        {
                                                            chiTietDeNghi.SoThang = Convert.ToInt32(item[idx_SoThang].ToString().Trim());
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + số tháng nâng lương trước hạn không hợp lệ:" + item[idx_SoThang].ToString());
                                                        }
                                                    }

                                                }
                                            }
                                            else if (!item.IsNull(idx_NangLuongTruocNghiHuu) && !string.IsNullOrEmpty(item[idx_NangLuongTruocNghiHuu].ToString()))
                                            {
                                                DateTime ngayNghiHuu = Convert.ToDateTime(item[idx_NangLuongTruocNghiHuu].ToString().Trim());
                                                if (ngayNghiHuu != null && ngayNghiHuu != DateTime.MinValue)
                                                {
                                                    chiTietDeNghi.PhanLoai = NangLuongEnum.TruocKhiNghiHuu;
                                                    chiTietDeNghi.NgayNghiHuu = ngayNghiHuu;
                                                }
                                                else
                                                    detailLog.AppendLine(" + Nâng lương trước khi nghỉ hưu không hợp lệ:" + item[idx_NangLuongTruocNghiHuu].ToString());
                                            }
                                            else
                                            {
                                                chiTietDeNghi.PhanLoai = NangLuongEnum.ThuongXuyen;
                                            }
                                            #endregion

                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
                                            }
                                            else
                                                deNghiNangLuong.ListChiTietDeNghiNangLuong.Add(chiTietDeNghi);
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có mã nhân sự (Số hiệu công chức) là: {0}", item[idx_MaQuanLy].ToString().Trim()));
                                        }
                                    }
                                    if (mainLog.Length > 0)
                                    {
                                        //Tiến hành trả lại dữ liệu không import vào phần mền
                                        uow.RollbackTransaction();
                                        //uow.Reload(deNghiNangLuong);

                                        if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin nâng lương. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
                                        {
                                            using (SaveFileDialog saveFile = new SaveFileDialog())
                                            {
                                                saveFile.Filter = "Text files (*.txt)|*.txt";

                                                if (saveFile.ShowDialog() == DialogResult.OK)
                                                {
                                                    HamDungChung.WriteLog(saveFile.FileName, mainLog.ToString());
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //Nếu không có lỗi thì tiến hành lưu dữ liệu.
                                        uow.CommitChanges();
                                        //Xuất thông báo thành công
                                        DialogUtil.ShowInfo("Quá trình Import dữ liệu thành công.!!!");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
