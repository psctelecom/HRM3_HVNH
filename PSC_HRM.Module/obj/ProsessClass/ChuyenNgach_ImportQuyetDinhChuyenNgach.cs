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
    public class ChuyenNgach_ImportQuyetDinhChuyenNgach
    {
        public static void XuLy_BUH(IObjectSpace obs)
        {
            #region Import quyết định cá nhân
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Excel 2003 file (*.xls)|*.xls";
                dialog.Multiselect = false;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A2:T]"))
                    {
                        ChiTietQuyetDinhChuyenNgach chiTietQuyetDinhChuyenNgach;
                        QuyetDinhChuyenNgach quyetDinhChuyenNgach;
                        XPCollection<QuyetDinhChuyenNgach> listQuyetDinh;
                        XPCollection<ChiTietQuyetDinhChuyenNgach> listChiTiet;
                        ThongTinNhanVien nhanVien;
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();
                            listQuyetDinh = new XPCollection<QuyetDinhChuyenNgach>(uow);
                            listChiTiet = new XPCollection<ChiTietQuyetDinhChuyenNgach>(uow);

                            using (DialogUtil.AutoWait())
                            {
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    int idx_soQuyetDinh = 1;
                                    int idx_ngayQuyetDinh = 2;
                                    int idx_ngayHieuLuc = 3;
                                    int idx_coQuanRaQuyetDinh = 4;
                                    int idx_nguoiKy = 5;
                                    int idx_maQuanLy = 6;
                                    int idx_ngachLuongCu = 9;
                                    int idx_bacLuongCu = 10;
                                    int idx_ngayHuongLuongCu = 12;
                                    int idx_mocNangLuongCu = 13;
                                    int idx_ngachLuongMoi = 14;
                                    int idx_bacLuongMoi = 15;
                                    int idx_ngayHuongLuongMoi = 17;
                                    int idx_mocNangLuongMoi = 18;

                                    foreach (DataRow item in dt.Rows)
                                    {
                                        //Khởi tạo bộ nhớ đệm
                                        detailLog = new StringBuilder();

                                        String soQuyetDinhText = item[idx_soQuyetDinh].ToString().FullTrim();
                                        String ngayQuyetDinhText = item[idx_ngayQuyetDinh].ToString().FullTrim();
                                        String ngayHieuLucText = item[idx_ngayHieuLuc].ToString().FullTrim();
                                        String coQuanRaQuyetDinhText = item[idx_coQuanRaQuyetDinh].ToString().FullTrim();
                                        String nguoiKyText = item[idx_nguoiKy].ToString().FullTrim();
                                        String maQuanLyText = item[idx_maQuanLy].ToString().FullTrim();
                                        String ngachLuongCuText = item[idx_ngachLuongCu].ToString().FullTrim();
                                        String bacLuongCuText = item[idx_bacLuongCu].ToString().FullTrim();
                                        String ngayHuongLuongCuText = item[idx_ngayHuongLuongCu].ToString().FullTrim();
                                        String mocNangLuongCuText = item[idx_mocNangLuongCu].ToString().FullTrim();
                                        String ngachLuongMoiText = item[idx_ngachLuongMoi].ToString().FullTrim();
                                        String bacLuongMoiText = item[idx_bacLuongMoi].ToString().FullTrim();
                                        String ngayHuongLuongMoiText = item[idx_ngayHuongLuongMoi].ToString().FullTrim();
                                        String mocNangLuongMoiText = item[idx_mocNangLuongMoi].ToString().FullTrim();
                                        //Tìm nhân viên theo mã quản lý
                                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                        if (nhanVien != null)
                                        {
                                            DateTime ngayQuyetDinh = DateTime.MinValue;
                                            #region Ngày quyết định
                                            if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                            {
                                                try
                                                {
                                                    ngayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
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

                                            if (!string.IsNullOrEmpty(soQuyetDinhText) && ngayQuyetDinh != DateTime.MinValue)
                                            {
                                                listQuyetDinh.Filter = CriteriaOperator.Parse("SoQuyetDinh = ? and NgayQuyetDinh = ?", soQuyetDinhText, ngayQuyetDinh);
                                            }
                                            if (listQuyetDinh.Count == 1)
                                            {
                                                quyetDinhChuyenNgach = listQuyetDinh[0];
                                                listChiTiet.Filter = CriteriaOperator.Parse("QuyetDinhChuyenNgach.Oid = ? and ThongTinNhanVien.Oid =?", quyetDinhChuyenNgach.Oid, nhanVien.Oid);
                                                if (listChiTiet.Count == 0)
                                                {
                                                    #region Thêm chi tiết

                                                    chiTietQuyetDinhChuyenNgach = new ChiTietQuyetDinhChuyenNgach(uow);
                                                    chiTietQuyetDinhChuyenNgach.QuyetDinhChuyenNgach = quyetDinhChuyenNgach;
                                                
                                                    chiTietQuyetDinhChuyenNgach.ThongTinNhanVien = nhanVien;
                                                    chiTietQuyetDinhChuyenNgach.BoPhan = nhanVien.BoPhan;

                                                    #region Ngạch lương cũ
                                                    if (!item.IsNull(idx_ngachLuongCu) && !string.IsNullOrEmpty(item[idx_ngachLuongCu].ToString()))
                                                    {

                                                        NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", ngachLuongCuText));
                                                        if (ngachLuong != null)
                                                        {
                                                            chiTietQuyetDinhChuyenNgach.NgachLuongCu = ngachLuong;

                                                            //Phòng trường hợp bậc lương cũ không có trong excel
                                                            chiTietQuyetDinhChuyenNgach.BacLuongCu = nhanVien.NhanVienThongTinLuong.BacLuong;

                                                            //Bậc lương cũ - Hệ số lương cũ
                                                            if (!item.IsNull(idx_bacLuongCu) && !string.IsNullOrEmpty(item[idx_bacLuongCu].ToString()))
                                                            {
                                                                BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and TenBacLuong = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuongCu].ToString().Trim()));
                                                                if (bacLuong == null)
                                                                {
                                                                    bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuongCu].ToString().Trim()));
                                                                }
                                                                if (bacLuong != null)
                                                                {
                                                                    chiTietQuyetDinhChuyenNgach.BacLuongCu = bacLuong;
                                                                    chiTietQuyetDinhChuyenNgach.HeSoLuongCu = bacLuong.HeSoLuong;
                                                                }
                                                                else
                                                                {
                                                                    detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_bacLuongCu].ToString());
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Ngạch lương cũ không hợp lệ:" + item[idx_ngachLuongCu].ToString());
                                                        }
                                                    }
                                                    #endregion

                                                    #region Ngày hưởng lương cũ
                                                    if (!item.IsNull(idx_ngayHuongLuongCu) && !string.IsNullOrEmpty(item[idx_ngayHuongLuongCu].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime ngayHuongLuongCu = Convert.ToDateTime(item[idx_ngayHuongLuongCu].ToString().Trim());
                                                            if (ngayHuongLuongCu != null && ngayHuongLuongCu != DateTime.MinValue)
                                                                chiTietQuyetDinhChuyenNgach.NgayHuongLuongCu = ngayHuongLuongCu;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày hưởng lương cũ không hợp lệ:" + item[idx_ngayHuongLuongCu].ToString());
                                                        }
                                                    }
                                                    #endregion

                                                    #region Mốc nâng lương cũ
                                                    if (!item.IsNull(idx_mocNangLuongCu) && !string.IsNullOrEmpty(item[idx_mocNangLuongCu].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime mocNangLuongCu = Convert.ToDateTime(item[idx_mocNangLuongCu].ToString().Trim());
                                                            if (mocNangLuongCu != null && mocNangLuongCu != DateTime.MinValue)
                                                                chiTietQuyetDinhChuyenNgach.MocNangLuongCu = mocNangLuongCu;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Mốc nâng lương cũ không hợp lệ:" + item[idx_mocNangLuongCu].ToString());
                                                        }
                                                    }
                                                    #endregion

                                                    #region Ngạch lương mới
                                                    if (!item.IsNull(idx_ngachLuongMoi) && !string.IsNullOrEmpty(item[idx_ngachLuongMoi].ToString()))
                                                    {

                                                        NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_ngachLuongMoi].ToString().Trim()));
                                                        if (ngachLuong != null)
                                                        {
                                                            chiTietQuyetDinhChuyenNgach.NgachLuongMoi = ngachLuong;

                                                            //Bậc lương mới - Hệ số lương mới
                                                            if (!item.IsNull(idx_bacLuongMoi) && !string.IsNullOrEmpty(item[idx_bacLuongMoi].ToString()))
                                                            {
                                                                BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and TenBacLuong = ? and BacLuongCu = false", ngachLuong.Oid, item[idx_bacLuongMoi].ToString().Trim()));
                                                                if (bacLuong == null)
                                                                {
                                                                    bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuongMoi].ToString().Trim()));
                                                                } 
                                                                if (bacLuong != null)
                                                                {
                                                                    chiTietQuyetDinhChuyenNgach.BacLuongMoi = bacLuong;
                                                                    chiTietQuyetDinhChuyenNgach.HeSoLuongMoi = bacLuong.HeSoLuong;
                                                                }
                                                                else
                                                                {
                                                                    detailLog.AppendLine(" + Bậc lương mới không hợp lệ:" + item[idx_bacLuongMoi].ToString());
                                                                }
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Bậc lương mới không tìm thấy.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Ngạch lương mới không hợp lệ:" + item[idx_ngachLuongMoi].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương mới không tìm thấy.");
                                                    }
                                                    #endregion

                                                    #region Ngày hưởng lương mới
                                                    if (!item.IsNull(idx_ngayHuongLuongMoi) && !string.IsNullOrEmpty(item[idx_ngayHuongLuongMoi].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime ngayHuongLuongMoi = Convert.ToDateTime(item[idx_ngayHuongLuongMoi].ToString().Trim());
                                                            if (ngayHuongLuongMoi != null && ngayHuongLuongMoi != DateTime.MinValue)
                                                                chiTietQuyetDinhChuyenNgach.NgayHuongLuongMoi = ngayHuongLuongMoi;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Ngày hưởng lương mới không hợp lệ:" + item[idx_ngayHuongLuongMoi].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngày hưởng lương mới không tìm thấy.");
                                                    }
                                                    #endregion

                                                    #region Mốc nâng lương mới
                                                    if (!item.IsNull(idx_mocNangLuongMoi) && !string.IsNullOrEmpty(item[idx_mocNangLuongMoi].ToString()))
                                                    {
                                                        try
                                                        {
                                                            DateTime mocNangLuongMoi = Convert.ToDateTime(item[idx_mocNangLuongMoi].ToString().Trim());
                                                            if (mocNangLuongMoi != null && mocNangLuongMoi != DateTime.MinValue)
                                                                chiTietQuyetDinhChuyenNgach.MocNangLuongMoi = mocNangLuongMoi;
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            detailLog.AppendLine(" + Mốc nâng lương mới không hợp lệ:" + item[idx_mocNangLuongMoi].ToString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Mốc nâng lương mới không tìm thấy.");
                                                    }
                                                    #endregion

                                                    listChiTiet.Add(chiTietQuyetDinhChuyenNgach);
                                                    listQuyetDinh[0].ListChiTietQuyetDinhChuyenNgach.Add(chiTietQuyetDinhChuyenNgach);
                                                    #endregion
                                                }
                                            }
                                            else if (listQuyetDinh.Count == 0)
                                            {
                                                quyetDinhChuyenNgach = new QuyetDinhChuyenNgach(uow);

                                                #region Số quyết định
                                                if (!string.IsNullOrEmpty(soQuyetDinhText))
                                                {
                                                    quyetDinhChuyenNgach.SoQuyetDinh = soQuyetDinhText;
                                                }
                                                //else
                                                //{
                                                //    detailLog.AppendLine("Số quyết định chưa có dữ liệu");
                                                //}
                                                #endregion

                                                #region Ngày quyết định
                                                if (!string.IsNullOrEmpty(ngayQuyetDinhText))
                                                {
                                                    try
                                                    {
                                                        quyetDinhChuyenNgach.NgayQuyetDinh = Convert.ToDateTime(ngayQuyetDinhText);
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
                                                        quyetDinhChuyenNgach.NgayHieuLuc = Convert.ToDateTime(ngayHieuLucText);
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
                                                    quyetDinhChuyenNgach.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.CoQuanKhacRaQuyetDinh;
                                                    quyetDinhChuyenNgach.TenCoQuan = coQuanRaQuyetDinhText;

                                                    if (!string.IsNullOrEmpty(nguoiKyText))
                                                    {
                                                        quyetDinhChuyenNgach.NguoiKy1 = nguoiKyText;
                                                    }

                                                }
                                                //else
                                                //{
                                                //    quyetDinhChuyenNgach.CoQuanRaQuyetDinh = CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh;
                                                //}
                                                #endregion


                                                #region Thêm chi tiết
                                                //Thêm chi tiết
                                                chiTietQuyetDinhChuyenNgach = new ChiTietQuyetDinhChuyenNgach(uow);

                                                chiTietQuyetDinhChuyenNgach.QuyetDinhChuyenNgach = quyetDinhChuyenNgach;
                                                chiTietQuyetDinhChuyenNgach.ThongTinNhanVien = nhanVien;
                                                chiTietQuyetDinhChuyenNgach.BoPhan = nhanVien.BoPhan;


                                                #region Ngạch lương cũ
                                                if (!item.IsNull(idx_ngachLuongCu) && !string.IsNullOrEmpty(item[idx_ngachLuongCu].ToString()))
                                                {

                                                    NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_ngachLuongCu].ToString().Trim()));
                                                    if (ngachLuong != null)
                                                    {
                                                        chiTietQuyetDinhChuyenNgach.NgachLuongCu = ngachLuong;

                                                        //Phòng trường hợp bậc lương cũ không có trong excel
                                                        chiTietQuyetDinhChuyenNgach.BacLuongCu = nhanVien.NhanVienThongTinLuong.BacLuong;

                                                        //Bậc lương cũ - Hệ số lương cũ
                                                        if (!item.IsNull(idx_bacLuongCu) && !string.IsNullOrEmpty(item[idx_bacLuongCu].ToString()))
                                                        {
                                                            BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and TenBacLuong = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuongCu].ToString().Trim()));
                                                            if (bacLuong == null)
                                                            {
                                                                bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuongCu].ToString().Trim()));
                                                            }
                                                            if (bacLuong != null)
                                                            {
                                                                chiTietQuyetDinhChuyenNgach.BacLuongCu = bacLuong;
                                                                chiTietQuyetDinhChuyenNgach.HeSoLuongCu = bacLuong.HeSoLuong;
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Bậc lương cũ không hợp lệ:" + item[idx_bacLuongCu].ToString());
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương cũ không hợp lệ:" + item[idx_ngachLuongCu].ToString());
                                                    }
                                                }
                                                #endregion

                                                #region Ngày hưởng lương cũ
                                                if (!item.IsNull(idx_ngayHuongLuongCu) && !string.IsNullOrEmpty(item[idx_ngayHuongLuongCu].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime ngayHuongLuongCu = Convert.ToDateTime(item[idx_ngayHuongLuongCu].ToString().Trim());
                                                        if (ngayHuongLuongCu != null && ngayHuongLuongCu != DateTime.MinValue)
                                                            chiTietQuyetDinhChuyenNgach.NgayHuongLuongCu = ngayHuongLuongCu;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Ngày hưởng lương cũ không hợp lệ:" + item[idx_ngayHuongLuongCu].ToString());
                                                    }
                                                }
                                                #endregion

                                                #region Mốc nâng lương cũ
                                                if (!item.IsNull(idx_mocNangLuongCu) && !string.IsNullOrEmpty(item[idx_mocNangLuongCu].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime mocNangLuongCu = Convert.ToDateTime(item[idx_mocNangLuongCu].ToString().Trim());
                                                        if (mocNangLuongCu != null && mocNangLuongCu != DateTime.MinValue)
                                                            chiTietQuyetDinhChuyenNgach.MocNangLuongCu = mocNangLuongCu;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Mốc nâng lương cũ không hợp lệ:" + item[idx_mocNangLuongCu].ToString());
                                                    }
                                                }
                                                #endregion

                                                #region Ngạch lương mới
                                                if (!item.IsNull(idx_ngachLuongMoi) && !string.IsNullOrEmpty(item[idx_ngachLuongMoi].ToString()))
                                                {

                                                    NgachLuong ngachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", item[idx_ngachLuongMoi].ToString().Trim()));
                                                    if (ngachLuong != null)
                                                    {
                                                        chiTietQuyetDinhChuyenNgach.NgachLuongMoi = ngachLuong;

                                                        //Bậc lương mới - Hệ số lương mới
                                                        if (!item.IsNull(idx_bacLuongMoi) && !string.IsNullOrEmpty(item[idx_bacLuongMoi].ToString()))
                                                        {
                                                            BacLuong bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and TenBacLuong = ? and BacLuongCu = false", ngachLuong.Oid, item[idx_bacLuongMoi].ToString().Trim()));
                                                            if (bacLuong == null)
                                                            {
                                                                bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong like ? and MaQuanLy = ? and BacLuongCu = false", ngachLuong, item[idx_bacLuongMoi].ToString().Trim()));
                                                            }
                                                            if (bacLuong != null)
                                                            {
                                                                chiTietQuyetDinhChuyenNgach.BacLuongMoi = bacLuong;
                                                                chiTietQuyetDinhChuyenNgach.HeSoLuongMoi = bacLuong.HeSoLuong;
                                                            }
                                                            else
                                                            {
                                                                detailLog.AppendLine(" + Bậc lương mới không hợp lệ:" + item[idx_bacLuongMoi].ToString());
                                                            }
                                                        }
                                                        else
                                                        {
                                                            detailLog.AppendLine(" + Bậc lương mới không tìm thấy.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        detailLog.AppendLine(" + Ngạch lương mới không hợp lệ:" + item[idx_ngachLuongMoi].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngạch lương mới không tìm thấy.");
                                                }
                                                #endregion

                                                #region Ngày hưởng lương mới
                                                if (!item.IsNull(idx_ngayHuongLuongMoi) && !string.IsNullOrEmpty(item[idx_ngayHuongLuongMoi].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime ngayHuongLuongMoi = Convert.ToDateTime(item[idx_ngayHuongLuongMoi].ToString().Trim());
                                                        if (ngayHuongLuongMoi != null && ngayHuongLuongMoi != DateTime.MinValue)
                                                            chiTietQuyetDinhChuyenNgach.NgayHuongLuongMoi = ngayHuongLuongMoi;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Ngày hưởng lương mới không hợp lệ:" + item[idx_ngayHuongLuongMoi].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Ngày hưởng lương mới không tìm thấy.");
                                                }
                                                #endregion

                                                #region Mốc nâng lương mới
                                                if (!item.IsNull(idx_mocNangLuongMoi) && !string.IsNullOrEmpty(item[idx_mocNangLuongMoi].ToString()))
                                                {
                                                    try
                                                    {
                                                        DateTime mocNangLuongMoi = Convert.ToDateTime(item[idx_mocNangLuongMoi].ToString().Trim());
                                                        if (mocNangLuongMoi != null && mocNangLuongMoi != DateTime.MinValue)
                                                            chiTietQuyetDinhChuyenNgach.MocNangLuongMoi = mocNangLuongMoi;
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        detailLog.AppendLine(" + Mốc nâng lương mới không hợp lệ:" + item[idx_mocNangLuongMoi].ToString());
                                                    }
                                                }
                                                else
                                                {
                                                    detailLog.AppendLine(" + Mốc nâng lương mới không tìm thấy.");
                                                }
                                                #endregion

                                                listQuyetDinh.Add(quyetDinhChuyenNgach);
                                                listChiTiet.Add(chiTietQuyetDinhChuyenNgach);
                                                #endregion
                                            }
                                            else
                                            {
                                                detailLog.AppendLine(string.Format("Có nhiều quyết định trùng số {0} - ngày quyết định {1}.", soQuyetDinhText, ngayQuyetDinhText));
                                            }


                                            //Đưa thông tin bị lỗi vào blog
                                            if (detailLog.Length > 0)
                                            {
                                                mainLog.AppendLine(string.Format("- Không import cán bộ [{0}] vào được: ", nhanVien.HoTen));
                                                mainLog.AppendLine(detailLog.ToString());
                                            }
                                        }
                                        else
                                        {
                                            mainLog.AppendLine(string.Format("- Không có cán bộ nào có Mã nhân sự (Số hiệu công chức) là: {0}", maQuanLyText));
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

            #endregion
        }
    }
}
