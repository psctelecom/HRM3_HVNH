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
using PSC_HRM.Module.ChuyenNgach;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.Controllers
{
    public class ThongTinLuong_ImportThongTinLuong
    {
        public static bool XuLy(IObjectSpace obs, BangChotThongTinTinhLuong bangChot)
        {
            bool _oke = false;
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    //
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$]"))
                    {
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                //Khởi tạo bộ nhớ đệm
                                detailLog = new StringBuilder();
                                //
                                int idx_SoTaiKhoan = 0;
                                int idx_TinhTrang = 2;
                                int idx_Huong85PhanTram = 3;
                                int idx_HSPCVuotKhung = 4;
                                int idx_HSPCThamNienHanhChinh = 5;
                                int idx_HSPCKhoiHanhChinh = 6;
                                int idx_HSPCThamNien = 7;
                                int idx_HSPCDocHai = 8;
                                int idx_HSPCChucVu = 9;
                                int idx_HSPCUuDai = 10;
                                int idx_HSPCKhac = 11;
                                int idx_HSPCTangThem = 12;
                                int idx_TiLeTangThem = 13;
                                int idx_HSPCTrachNhiem1 = 14;
                                int idx_HSPCTrachNhiem2 = 15;
                                int idx_HSPCTrachNhiem3 = 16;
                                int idx_HSPCTrachNhiem4 = 17;
                                int idx_HSPCTrachNhiem5 = 18;

                                //Tìm nhân viên theo mã quản lý
                                ThongTinNhanVien nhanVien = obs.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ListTaiKhoanNganHang[SoTaiKhoan = ?]", item[idx_SoTaiKhoan].ToString().Trim()));
                                if (nhanVien != null)
                                {
                                    ThongTinTinhLuong thongTinTinhLuong = obs.FindObject<ThongTinTinhLuong>(CriteriaOperator.Parse("ThongTinNhanVien = ? and BangChotThongTinTinhLuong = ?", nhanVien.Oid, bangChot.Oid));
                                    if (thongTinTinhLuong == null)
                                    {
                                        thongTinTinhLuong = new ThongTinTinhLuong(((XPObjectSpace)obs).Session);
                                        thongTinTinhLuong.BangChotThongTinTinhLuong = obs.GetObjectByKey<BangChotThongTinTinhLuong>(bangChot.Oid);
                                        thongTinTinhLuong.BoPhan = nhanVien.BoPhan;
                                        thongTinTinhLuong.ThongTinNhanVien = nhanVien;
                                        thongTinTinhLuong.TinhLuong = true;
                                    }

                                    #region Tình trạng
                                    if (!item.IsNull(idx_TinhTrang) && !string.IsNullOrEmpty(item[idx_TinhTrang].ToString()))
                                    {
                                        TinhTrang tinhTrang = obs.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang = ?", item[idx_TinhTrang].ToString().Trim()));
                                        if (thongTinTinhLuong != null)
                                        {
                                            thongTinTinhLuong.TinhTrang = tinhTrang;
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Tình trạng không hợp lệ:" + item[idx_TinhTrang].ToString());
                                        }
                                    }
                                    else
                                    {
                                        detailLog.AppendLine(" + Không tìm thấy tình trạng.");
                                    }
                                    #endregion

                                    #region Hưởng 85% lương
                                    if (!item.IsNull(idx_Huong85PhanTram) && !string.IsNullOrEmpty(item[idx_Huong85PhanTram].ToString()))
                                    {
                                        if (item[idx_Huong85PhanTram].ToString().Trim().Equals("x"))
                                        {
                                            thongTinTinhLuong.Huong85PhanTramLuong = true;
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Hưởng 85% lương không hợp lệ:" + item[idx_Huong85PhanTram].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Hệ số phụ cấp vượt khung
                                    if (!item.IsNull(idx_HSPCVuotKhung) && !string.IsNullOrEmpty(item[idx_HSPCVuotKhung].ToString()))
                                    {
                                        try
                                        {
                                            decimal hSPCVuotKhung = Convert.ToDecimal(item[idx_HSPCVuotKhung].ToString().Trim());
                                            if (hSPCVuotKhung > 0)
                                                thongTinTinhLuong.HSPCVuotKhung = hSPCVuotKhung;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Hệ số PC vượt khung không hợp lệ:" + item[idx_HSPCVuotKhung].ToString());
                                        }
                                    }
                                    #endregion#endregion
                                    
                                    #region Hệ số phụ cấp thâm niên
                                    if (!item.IsNull(idx_HSPCThamNien) && !string.IsNullOrEmpty(item[idx_HSPCThamNien].ToString()))
                                    {
                                        try
                                        {
                                            decimal hSPCThamNien = Convert.ToDecimal(item[idx_HSPCThamNien].ToString().Trim());
                                            if (hSPCThamNien > 0)
                                                thongTinTinhLuong.HSPCThamNien = hSPCThamNien;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Hệ số PC thâm niên không hợp lệ:" + item[idx_HSPCThamNien].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Hệ số phụ cấp độc hại
                                    if (!item.IsNull(idx_HSPCDocHai) && !string.IsNullOrEmpty(item[idx_HSPCDocHai].ToString()))
                                    {
                                        try
                                        {
                                            decimal hSPCDocHai = Convert.ToDecimal(item[idx_HSPCDocHai].ToString().Trim());
                                            if (hSPCDocHai > 0)
                                                thongTinTinhLuong.HSPCDocHai = hSPCDocHai;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Hệ số PC độc hại không hợp lệ:" + item[idx_HSPCDocHai].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Hệ số phụ cấp chức vụ
                                    if (!item.IsNull(idx_HSPCChucVu) && !string.IsNullOrEmpty(item[idx_HSPCChucVu].ToString()))
                                    {
                                        try
                                        {
                                            decimal hSPCChucVu = Convert.ToDecimal(item[idx_HSPCChucVu].ToString().Trim());
                                            if (hSPCChucVu > 0)
                                                thongTinTinhLuong.HSPCChucVu = hSPCChucVu;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Hệ số PC chức vụ không hợp lệ:" + item[idx_HSPCChucVu].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Hệ số phụ cấp ưu đãi
                                    if (!item.IsNull(idx_HSPCUuDai) && !string.IsNullOrEmpty(item[idx_HSPCUuDai].ToString()))
                                    {
                                        try
                                        {
                                            decimal hSPCUuDai = Convert.ToDecimal(item[idx_HSPCUuDai].ToString().Trim());
                                            if (hSPCUuDai > 0)
                                                thongTinTinhLuong.HSPCUuDai = hSPCUuDai;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Hệ số PC ưu đãi không hợp lệ:" + item[idx_HSPCUuDai].ToString());
                                        }
                                    }
                                    #endregion

                                    #region Hệ số phụ cấp khác
                                    if (!item.IsNull(idx_HSPCKhac) && !string.IsNullOrEmpty(item[idx_HSPCKhac].ToString()))
                                    {
                                        try
                                        {
                                            decimal hSPCKhac = Convert.ToDecimal(item[idx_HSPCKhac].ToString().Trim());
                                            if (hSPCKhac > 0)
                                                thongTinTinhLuong.HSPCKhac = hSPCKhac;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Hệ số PC khác không hợp lệ:" + item[idx_HSPCKhac].ToString());
                                        }
                                    }
                                    #endregion

                                    
                                    #region Tỉ lệ tăng thêm
                                    if (!item.IsNull(idx_TiLeTangThem) && !string.IsNullOrEmpty(item[idx_TiLeTangThem].ToString()))
                                    {
                                        try
                                        {
                                            int tiLeTangThem = Convert.ToInt32(item[idx_TiLeTangThem].ToString().Trim());
                                            if (tiLeTangThem > 0)
                                                thongTinTinhLuong.TiLeTangThem = tiLeTangThem;
                                        }
                                        catch (Exception ex)
                                        {
                                            detailLog.AppendLine(" + Tỉ lệ tăng thêm không hợp lệ:" + item[idx_HSPCTangThem].ToString());
                                        }
                                    }
                                    #endregion                                    n

                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Không import cán bộ [{0} - {1}] vào được: ", nhanVien.HoTen,item[idx_SoTaiKhoan].ToString()));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                    else
                                        bangChot.ListThongTinTinhLuong.Add(thongTinTinhLuong);
                                }
                                else
                                {
                                    mainLog.AppendLine(string.Format("- Không có cán bộ nào có số tài khoản là: {0}", item[idx_SoTaiKhoan].ToString()));
                                }
                            }
                        }

                        if (mainLog.Length > 0)
                        {
                            //Tiến hành trả lại dữ liệu không import vào phần mền
                            bangChot.ListThongTinTinhLuong.Reload();

                            if (DialogUtil.ShowYesNo("Không thể tiếp tục vì sai thông tin chuyển ngạch. Bạn có muốn xuất dữ liệu bị sai?") == DialogResult.Yes)
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

                            //Xuất thông báo lỗi
                            _oke = false;
                        }
                        else
                        {
                            //Xuất thông báo thành công
                            _oke = true;
                        }

                    }
                }
            }
            return _oke;
        }
    }

}
