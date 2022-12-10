using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    public class HoSo_ImportGiangVienThinhGiang
    {
        //Xử lý 2 khoảng trắng liền nhau
        static String FullTrim(String chuoi)
        {
            string s = "  ";
            if (chuoi.Contains(s))
            {
                return FullTrim(chuoi.Replace(s, " "));
            }
            else
                return chuoi;
        }

        static string XuLyHo(string hoTen)
        {           
            string ten = string.Empty;
            string ho = string.Empty;
            ten = XuLyTen(hoTen);
            if (ten.Length > 0)
            {
                ho = hoTen.Replace(ten, "").Trim();
            }
            else
            {
                MessageBox.Show(hoTen);
            }
            return ho;
        }

        static string XuLyTen(string hoTen)
        {
            string ten = string.Empty;
            string[] words = hoTen.Split(' ');
            ten = words[words.Length - 1];
            return ten;
        }

        public static void XuLy(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AT]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;
                string maQuanLy;

                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //ThongTinNhanVien thongTinGiangVien;

                    GiangVienThinhGiang giangVienThinhGiang = null;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Khởi tạo bộ nhớ đệm
                            detailLog = new StringBuilder();

                            int idx_MaQuanLy = 0;
                            int idx_HoTen = 1;
                            int idx_MaDonVi = 2;
                            int idx_HocHam = 3;
                            int idx_TrinhDoChuyenMon = 4;
                            int idx_TinhTrang = 5;
                            int idx_NgaySinh = 6;
                            int idx_GioiTinh = 7;
                            int idx_NoiSinh = 8;
                            int idx_SoCMND = 9;
                            int idx_NgayCap = 10;
                            int idx_NoiCap = 11;
                            int idx_TamTru = 12;
                            int idx_ThuongTru = 13;
                            int idx_Email = 14;
                            int idx_DienThoai = 15;
                            int idx_DienThoaiDD = 16;
                            int idx_SoTaiKhoan = 17;
                            int idx_NganHang = 18;
                            int idx_MaSoThue = 19;

                            #region Thông tin giảng viên thỉnh giảng
                            {
                                //Tìm giảng viên theo mã quản lý                        

                                giangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaThinhGiang=?", dr[idx_MaQuanLy].ToString().Trim()));
                                if (giangVienThinhGiang == null)
                                {
                                    giangVienThinhGiang = new GiangVienThinhGiang(uow);

                                    #region Mã quản lý
                                    {
                                        String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                        if (!string.IsNullOrEmpty(maQuanLyText))
                                        {
                                            if (TruongConfig.MaTruong.Equals("NEU"))
                                                giangVienThinhGiang.MaThinhGiang = maQuanLyText;
                                            else
                                                giangVienThinhGiang.MaQuanLy = maQuanLyText;
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                                " + Thiếu thông tin mã quản lý");
                                        }

                                    }
                                    #endregion

                                    #region Họ tên
                                    {
                                        String hoTenText = dr[idx_HoTen].ToString();
                                        if (!string.IsNullOrEmpty(hoTenText))
                                        {
                                            giangVienThinhGiang.Ho = XuLyHo(hoTenText);
                                            giangVienThinhGiang.Ten = XuLyTen(hoTenText);
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                                " + Thiếu thông tin họ tên");
                                        }
                                    }
                                    #endregion

                                    #region Mã đơn vị
                                    {
                                        String maDonViText = dr[idx_MaDonVi].ToString().Trim();
                                        if (!string.IsNullOrEmpty(maDonViText))
                                        {
                                            BoPhan boPhan = null;
                                            boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ? or TenBoPhan like ?", maDonViText));
                                            if (boPhan != null)
                                            {
                                                giangVienThinhGiang.TaiBoMon = boPhan;                                                
                                            }                                           
                                        }
                                        giangVienThinhGiang.BoPhan = HamDungChung.ThongTinTruong(uow);
                                        //else
                                        //{
                                        //    detailLog.AppendLine(
                                        //        " + Thiếu thông tin mã đơn vị");
                                        //}
                                    }
                                    #endregion                                    

                                    #region Học hàm
                                    {                                       
                                        String hocHamText = dr[idx_HocHam].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(hocHamText))
                                        {
                                            HocHam hocHam = null;
                                            hocHam = uow.FindObject<HocHam>(CriteriaOperator.Parse("TenHocHam like ?", hocHamText));
                                            if (hocHam == null)
                                            {
                                                hocHam = new HocHam(uow);
                                                hocHam.TenHocHam = hocHamText;
                                                hocHam.MaQuanLy = Guid.NewGuid().ToString();                                              
                                            }
                                            giangVienThinhGiang.NhanVienTrinhDo.HocHam = hocHam;                                           
                                        }
                                    }
                                    #endregion

                                    #region Trình độ chuyên môn
                                    {
                                        String trinhDoChuyenMonText = dr[idx_TrinhDoChuyenMon].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(trinhDoChuyenMonText))
                                        {
                                            TrinhDoChuyenMon trinhDoChuyenMon = null;
                                            trinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", trinhDoChuyenMonText));
                                            if (trinhDoChuyenMon == null)
                                            {
                                                //tạo mới trình độ chuyên môn
                                                trinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                                trinhDoChuyenMon.TenTrinhDoChuyenMon = trinhDoChuyenMonText;
                                                trinhDoChuyenMon.MaQuanLy = Guid.NewGuid().ToString();
                                                trinhDoChuyenMon.Save();
                                            }
                                            VanBang vanBang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", giangVienThinhGiang.MaQuanLy, trinhDoChuyenMon));
                                            if (vanBang == null)
                                            {
                                                vanBang = new VanBang(uow);
                                                vanBang.HoSo = giangVienThinhGiang;
                                                vanBang.TrinhDoChuyenMon = trinhDoChuyenMon;
                                            }
                                            giangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDoChuyenMon;
                                            
                                        }
                                    }
                                    #endregion

                                    #region Tình trạng
                                    {
                                        String tinhTrangText = dr[idx_TinhTrang].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(tinhTrangText))
                                        {
                                            TinhTrang tinhTrang = null;
                                            tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", tinhTrangText));
                                            if (tinhTrang == null)
                                            {
                                                tinhTrang = new TinhTrang(uow);
                                                tinhTrang.TenTinhTrang = tinhTrangText;
                                                tinhTrang.MaQuanLy = Guid.NewGuid().ToString();
                                                tinhTrang.Save();
                                            }
                                            giangVienThinhGiang.TinhTrang = tinhTrang;
                                        }
                                    }
                                    #endregion

                                    #region Ngày sinh
                                    {
                                        String ngaySinhText = dr[idx_NgaySinh].ToString().Trim();
                                        if (!string.IsNullOrEmpty(ngaySinhText))
                                        {
                                            try
                                            {
                                                giangVienThinhGiang.NgaySinh = Convert.ToDateTime(ngaySinhText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngaySinhText);
                                            }
                                        }
                                        else
                                        {
                                            giangVienThinhGiang.NgaySinh = Convert.ToDateTime("01/01/1960");
                                            //detailLog.AppendLine(" + Thiếu thông tin ngày sinh hoặc không đúng định dạng dd/MM/yyyy.");
                                        }
                                    }
                                    #endregion

                                    #region Giới tính
                                    {
                                        String gioiTinh = dr[idx_GioiTinh].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(gioiTinh))
                                        {
                                            if (gioiTinh.ToLower() == "nam")
                                                giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nam;
                                            else if (gioiTinh.ToLower() == "nữ" || gioiTinh.ToLower() == "nu")
                                                giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                detailLog.AppendLine(" + Giới tính không hợp lệ: " + gioiTinh);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Nơi sinh
                                    {
                                        String noiSinhText = dr[idx_NoiSinh].ToString().FullTrim();
                                        DiaChi diaChi = null;
                                        if (!string.IsNullOrEmpty(noiSinhText))
                                        {
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", noiSinhText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = noiSinhText;
                                                diaChi.Save();
                                            }
                                            giangVienThinhGiang.NoiSinh = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Số CMND
                                    {
                                        String soCMND = dr[idx_SoCMND].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(soCMND))
                                        {
                                            giangVienThinhGiang.CMND = soCMND;
                                        }
                                    }
                                    #endregion

                                    #region Ngày cấp CMND
                                    {
                                        String ngayCapText = dr[idx_NgayCap].ToString().Trim();
                                        if (!string.IsNullOrEmpty(ngayCapText))
                                        {
                                            try
                                            {
                                                giangVienThinhGiang.NgayCap = Convert.ToDateTime(ngayCapText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + ngayCapText);
                                            }
                                        }
                                        //else
                                        //{
                                        //    //giangVienThinhGiang.NgayCap = Convert.ToDateTime("01/01/1960");
                                        //    detailLog.AppendLine(" + Thiếu thông tin cấp CMND hoặc không đúng định dạng dd/MM/yyyy.");
                                        //}


                                    }
                                    #endregion

                                    #region Nơi cấp CMND
                                    {
                                        String noiCapText = dr[idx_NoiCap].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(noiCapText))
                                        {
                                            TinhThanh tinhThanh = null;
                                            tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", noiCapText));
                                            if (tinhThanh == null)
                                            {
                                                //tạo mới tỉnh thành
                                                tinhThanh = new TinhThanh(uow);
                                                tinhThanh.TenTinhThanh = noiCapText;
                                                tinhThanh.MaQuanLy = Guid.NewGuid().ToString();
                                                tinhThanh.Save();
                                                //uow.Save(tinhThanh);
                                            }
                                            giangVienThinhGiang.NoiCap = tinhThanh;
                                        }
                                    }
                                    #endregion

                                    #region Thường trú
                                    {
                                        String thuongTruText = dr[idx_ThuongTru].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(thuongTruText))
                                        {
                                            DiaChi diaChi = null;
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", thuongTruText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = thuongTruText;
                                                diaChi.Save();
                                            }
                                            giangVienThinhGiang.DiaChiThuongTru = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Tạm trú - Nơi ở hiện nay
                                    {
                                        String tamTruText = dr[idx_TamTru].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(tamTruText))
                                        {
                                            DiaChi diaChi = null;
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", tamTruText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = tamTruText;
                                                diaChi.Save();
                                            }
                                            giangVienThinhGiang.NoiOHienNay = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Số điện thoại
                                    {
                                        String soDienThoaiText = dr[idx_DienThoai].ToString().Trim();
                                        if (!string.IsNullOrEmpty(soDienThoaiText))
                                        {
                                            giangVienThinhGiang.DienThoaiNhaRieng = soDienThoaiText;
                                        }
                                    }
                                    #endregion

                                    #region Số điện thoại di động
                                    {
                                        String soDienThoaiDDText = dr[idx_DienThoaiDD].ToString().Trim();
                                        if (!string.IsNullOrEmpty(soDienThoaiDDText))
                                        {
                                            giangVienThinhGiang.DienThoaiDiDong = soDienThoaiDDText;
                                        }
                                    }
                                    #endregion

                                    #region Email
                                    {
                                        String emailText = dr[idx_Email].ToString().Trim();
                                        if (!string.IsNullOrEmpty(emailText))
                                        {
                                            giangVienThinhGiang.Email = emailText;
                                        }
                                    }
                                    #endregion

                                    #region Số tài khoản - Ngân hàng
                                    String soTaiKhoanText = dr[idx_SoTaiKhoan].ToString().Trim();
                                    String nganHangText = dr[idx_NganHang].ToString().Trim();
                                    if (!string.IsNullOrWhiteSpace(soTaiKhoanText))
                                    {
                                        NganHang nganHang = null;
                                        if (!string.IsNullOrWhiteSpace(soTaiKhoanText))
                                        {
                                            nganHang = uow.FindObject<NganHang>(CriteriaOperator.Parse("TenNganHang like ?", nganHangText));
                                            if (nganHang == null)
                                            {
                                                nganHang = new NganHang(uow);
                                                nganHang.TenNganHang = nganHangText;
                                                nganHang.Save();
                                            }
                                        }

                                        TaiKhoanNganHang tknh = null;
                                        tknh = uow.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("SoTaiKhoan = ?", soTaiKhoanText));
                                        if (tknh == null)
                                        {
                                            tknh = new TaiKhoanNganHang(uow);
                                            tknh.SoTaiKhoan = soTaiKhoanText;
                                            if (nganHang != null)
                                                tknh.NganHang = nganHang;
                                            tknh.TaiKhoanChinh = true;
                                        }
                                        tknh.NhanVien = giangVienThinhGiang;
                                        tknh.Save();
                                    }
                                    #endregion

                                    #region Mã số thuế
                                    {
                                        String maSoThue = dr[idx_MaSoThue].ToString().Trim();
                                        if (!string.IsNullOrEmpty(maSoThue))
                                        {
                                            giangVienThinhGiang.NhanVienThongTinLuong.MaSoThue = maSoThue;
                                        }
                                    }
                                    #endregion

                                }
                                #region Ghi File log
                                {
                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Giảng viên thỉnh giảng: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                }

                                #endregion
                            }
                        }
                            #endregion

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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
                            obs.Refresh();
                        }
                    }
                }

            }            
            
        }

        public static void XuLy_DLU(IObjectSpace obs, string fileName)
        {
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A2:AV]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //ThongTinNhanVien thongTinGiangVien;

                    GiangVienThinhGiang giangVienThinhGiang = null;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Khởi tạo bộ nhớ đệm
                            detailLog = new StringBuilder();

                            int idx_MaQuanLy = 0;
                            int idx_HoTen = 1;
                            int idx_MaDonVi = 2;
                            int idx_HocHam = 3;
                            int idx_TrinhDoChuyenMon = 4;
                            int idx_TinhTrang = 5;
                            int idx_NgaySinh = 6;
                            int idx_GioiTinh = 7;
                            int idx_NoiSinh = 8;
                            int idx_SoCMND = 9;
                            int idx_NgayCap = 10;
                            int idx_NoiCap = 11;
                            int idx_TamTru = 12;
                            int idx_ThuongTru = 13;
                            int idx_Email = 14;
                            int idx_DienThoai = 15;
                            int idx_DienThoaiDD = 16;
                            int idx_SoTaiKhoan = 17;
                            int idx_QuocTich = 18;
                            int idx_DonViCongTac = 19;
                            int idx_CongViecHienTai = 20;
                            int idx_ChuyenMonDaoTao = 21;

                            #region Thông tin giảng viên thỉnh giảng
                            {
                                //Tìm giảng viên theo mã quản lý                        

                                giangVienThinhGiang = uow.FindObject<GiangVienThinhGiang>(CriteriaOperator.Parse("MaQuanLy=?", dr[idx_MaQuanLy].ToString().Trim()));
                                if (giangVienThinhGiang == null)
                                {
                                    giangVienThinhGiang = new GiangVienThinhGiang(uow);

                                    #region Mã quản lý
                                    {
                                        String maQuanLyText = dr[idx_MaQuanLy].ToString();
                                        if (!string.IsNullOrEmpty(maQuanLyText))
                                        {
                                            giangVienThinhGiang.MaQuanLy = maQuanLyText;
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                                " + Thiếu thông tin mã quản lý");
                                        }

                                    }
                                    #endregion

                                    #region Họ tên
                                    {
                                        String hoTenText = dr[idx_HoTen].ToString();
                                        if (!string.IsNullOrWhiteSpace(hoTenText))
                                        {
                                            giangVienThinhGiang.Ho = XuLyHo(hoTenText);
                                            giangVienThinhGiang.Ten = XuLyTen(hoTenText);
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                                " + Thiếu thông tin họ tên");
                                        }
                                    }
                                    #endregion

                                    #region Mã đơn vị
                                    {
                                        BoPhan boPhan = null;
                                        boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ?", "DLU"));
                                        if (boPhan != null)
                                        {
                                            giangVienThinhGiang.BoPhan = boPhan;
                                        }

                                    }
                                    #endregion

                                    #region Khoa bộ môn
                                    {
                                        String khoaBoMon = dr[idx_MaDonVi].ToString().Trim();
                                        if (!string.IsNullOrEmpty(khoaBoMon))
                                        {
                                            BoPhan boPhan = null;
                                            boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ?", khoaBoMon));
                                            if (boPhan != null)
                                            {
                                                giangVienThinhGiang.TaiBoMon = boPhan;
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                                " + Thiếu thông tin mã đơn vị khoa/bộ môn");
                                        }
                                    }
                                    #endregion

                                    #region Học hàm - Trình độ chuyên môn
                                    {
                                        NhanVienTrinhDo nhanVienTrinhDo = null;
                                        HocHam hocHam = null;
                                        TrinhDoChuyenMon trinhDoChuyenMon = null;
                                        ChuyenMonDaoTao chuyenMonDaoTao = null;

                                        String hocHamText = dr[idx_HocHam].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(hocHamText))
                                        {                                                                               
                                            hocHam = uow.FindObject<HocHam>(CriteriaOperator.Parse("TenHocHam like ?", hocHamText));
                                            if (hocHam == null)
                                            {
                                                hocHam = new HocHam(uow);
                                                hocHam.TenHocHam = hocHamText;
                                                hocHam.MaQuanLy = Guid.NewGuid().ToString();
                                                hocHam.Save();
                                            }
                                        }

                                        String trinhDoChuyenMonText = dr[idx_TrinhDoChuyenMon].ToString().Trim();
                                        if (!string.IsNullOrWhiteSpace(trinhDoChuyenMonText))
                                        {
                                            trinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon like ?", trinhDoChuyenMonText));
                                            if (trinhDoChuyenMon == null)
                                            {
                                                trinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                                trinhDoChuyenMon.TenTrinhDoChuyenMon = trinhDoChuyenMonText;
                                                trinhDoChuyenMon.Save();
                                            }

                                            String chuyenMonDaoTaoText = dr[idx_ChuyenMonDaoTao].ToString().Trim();
                                            if (!string.IsNullOrWhiteSpace(chuyenMonDaoTaoText))
                                            {
                                                chuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoText));
                                                if (chuyenMonDaoTao == null)
                                                {
                                                    chuyenMonDaoTao = new ChuyenMonDaoTao(uow);
                                                    chuyenMonDaoTao.TenChuyenMonDaoTao = chuyenMonDaoTaoText;
                                                    chuyenMonDaoTao.Save();
                                                }
                                            }

                                            VanBang vanBang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", giangVienThinhGiang.MaQuanLy, trinhDoChuyenMon));
                                            if (vanBang == null)
                                            {
                                                vanBang = new VanBang(uow);
                                                vanBang.HoSo = giangVienThinhGiang;
                                                vanBang.TrinhDoChuyenMon = trinhDoChuyenMon;
                                                vanBang.ChuyenMonDaoTao = chuyenMonDaoTao;
                                            }

                                            nhanVienTrinhDo = new NhanVienTrinhDo(uow);
                                            nhanVienTrinhDo.HocHam = hocHam;
                                            nhanVienTrinhDo.TrinhDoChuyenMon = trinhDoChuyenMon;
                                            nhanVienTrinhDo.ChuyenMonDaoTao = chuyenMonDaoTao;
                                            nhanVienTrinhDo.Save();
                                            giangVienThinhGiang.NhanVienTrinhDo = nhanVienTrinhDo;  

                                        }                           
                                    }
                                    #endregion                                   

                                    #region Tình trạng
                                    {
                                        String tinhTrangText = dr[idx_TinhTrang].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(tinhTrangText))
                                        {
                                            TinhTrang tinhTrang = null;
                                            tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", tinhTrangText));
                                            if (tinhTrang == null)
                                            {
                                                tinhTrang = new TinhTrang(uow);
                                                tinhTrang.TenTinhTrang = tinhTrangText;
                                                tinhTrang.MaQuanLy = Guid.NewGuid().ToString();
                                                tinhTrang.Save();
                                            }
                                            giangVienThinhGiang.TinhTrang = tinhTrang;
                                        }
                                    }
                                    #endregion

                                    #region Ngày sinh
                                    {
                                        String ngaySinhText = dr[idx_NgaySinh].ToString().Trim();
                                        if (!string.IsNullOrEmpty(ngaySinhText))
                                        {
                                            try
                                            {
                                                giangVienThinhGiang.NgaySinh = Convert.ToDateTime(ngaySinhText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngaySinhText);
                                            }
                                        }
                                        else
                                        {
                                            giangVienThinhGiang.NgaySinh = Convert.ToDateTime("01/01/1960");
                                            //detailLog.AppendLine(" + Thiếu thông tin ngày sinh hoặc không đúng định dạng dd/MM/yyyy.");
                                        }
                                    }
                                    #endregion

                                    #region Giới tính
                                    {
                                        String gioiTinh = dr[idx_GioiTinh].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(gioiTinh))
                                        {
                                            if (gioiTinh.ToLower() == "nam")
                                                giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nam;
                                            else if (gioiTinh.ToLower() == "nữ" || gioiTinh.ToLower() == "nu")
                                                giangVienThinhGiang.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                detailLog.AppendLine(" + Giới tính không hợp lệ: " + gioiTinh);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Nơi sinh
                                    {
                                        String noiSinhText = dr[idx_NoiSinh].ToString().FullTrim();
                                        DiaChi diaChi = null;
                                        if (!string.IsNullOrEmpty(noiSinhText))
                                        {
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", noiSinhText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = noiSinhText;
                                                diaChi.Save();
                                            }
                                            giangVienThinhGiang.NoiSinh = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Ngày cấp CMND
                                    {
                                        String ngayCapText = dr[idx_NgayCap].ToString().Trim();
                                        if (!string.IsNullOrEmpty(ngayCapText))
                                        {
                                            try
                                            {
                                                giangVienThinhGiang.NgayCap = Convert.ToDateTime(ngayCapText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + ngayCapText);
                                            }
                                        }
                                        //else
                                        //{
                                        //    //giangVienThinhGiang.NgayCap = Convert.ToDateTime("01/01/1960");
                                        //    detailLog.AppendLine(" + Thiếu thông tin cấp CMND hoặc không đúng định dạng dd/MM/yyyy.");
                                        //}


                                    }
                                    #endregion

                                    #region Nơi cấp CMND
                                    {
                                        String noiCapText = dr[idx_NoiCap].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(noiCapText))
                                        {
                                            TinhThanh tinhThanh = null;
                                            tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", noiCapText));
                                            if (tinhThanh == null)
                                            {
                                                //tạo mới tỉnh thành
                                                tinhThanh = new TinhThanh(uow);
                                                tinhThanh.TenTinhThanh = noiCapText;
                                                tinhThanh.MaQuanLy = Guid.NewGuid().ToString();
                                                tinhThanh.Save();
                                                //uow.Save(tinhThanh);
                                            }
                                            giangVienThinhGiang.NoiCap = tinhThanh;
                                        }
                                    }
                                    #endregion

                                    #region Thường trú
                                    {
                                        String thuongTruText = dr[idx_ThuongTru].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(thuongTruText))
                                        {
                                            DiaChi diaChi = null;
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", thuongTruText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = thuongTruText;
                                                diaChi.Save();
                                            }
                                            giangVienThinhGiang.DiaChiThuongTru = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Tạm trú - Nơi ở hiện nay
                                    {
                                        String tamTruText = dr[idx_TamTru].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(tamTruText))
                                        {
                                            DiaChi diaChi = null;
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", tamTruText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = tamTruText;
                                                diaChi.Save();
                                            }
                                            giangVienThinhGiang.NoiOHienNay = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Số điện thoại
                                    {
                                        String soDienThoaiText = dr[idx_DienThoai].ToString().Trim();
                                        if (!string.IsNullOrEmpty(soDienThoaiText))
                                        {
                                            giangVienThinhGiang.DienThoaiNhaRieng = soDienThoaiText;
                                        }
                                    }
                                    #endregion

                                    #region Số điện thoại di động
                                    {
                                        String soDienThoaiDDText = dr[idx_DienThoaiDD].ToString().Trim();
                                        if (!string.IsNullOrEmpty(soDienThoaiDDText))
                                        {
                                            giangVienThinhGiang.DienThoaiDiDong = soDienThoaiDDText;
                                        }
                                    }
                                    #endregion

                                    #region Email
                                    {
                                        String emailText = dr[idx_Email].ToString().Trim();
                                        if (!string.IsNullOrEmpty(emailText))
                                        {
                                            giangVienThinhGiang.Email = emailText;
                                        }
                                    }
                                    #endregion

                                    #region Số tài khoản
                                    String soTaiKhoanText = dr[idx_SoTaiKhoan].ToString().Trim();

                                    if (!string.IsNullOrWhiteSpace(soTaiKhoanText))
                                    {
                                        TaiKhoanNganHang tknh = null;
                                        tknh = uow.FindObject<TaiKhoanNganHang>(CriteriaOperator.Parse("SoTaiKhoan = ?", soTaiKhoanText));
                                        if (tknh == null)
                                        {
                                            tknh = new TaiKhoanNganHang(uow);
                                            tknh.SoTaiKhoan = soTaiKhoanText;
                                            tknh.TaiKhoanChinh = true;
                                        }
                                        tknh.NhanVien = giangVienThinhGiang;
                                        tknh.Save();
                                    }
                                    #endregion

                                    #region Quốc tịch
                                    String quocTichText = dr[idx_QuocTich].ToString().Trim();
                                    if (!string.IsNullOrWhiteSpace(quocTichText))
                                    {
                                        QuocGia quocTich = null;
                                        quocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", quocTichText));
                                        if (quocTich == null)
                                        {
                                            quocTich = new QuocGia(uow);
                                            quocTich.TenQuocGia = quocTichText;
                                            quocTich.Save();
                                        }
                                        giangVienThinhGiang.QuocTich = quocTich;
                                    }
                                    #endregion

                                    #region Đơn vị công tác
                                    {
                                        String donViCongTacText = dr[idx_DonViCongTac].ToString().Trim();
                                        if (!string.IsNullOrWhiteSpace(donViCongTacText))
                                        {
                                            giangVienThinhGiang.DonViCongTac = donViCongTacText;
                                        }
                                    }
                                    #endregion

                                    #region Công việc hiện tại
                                    {
                                        String congViecHienTaiText = dr[idx_CongViecHienTai].ToString().Trim();
                                        if (!string.IsNullOrWhiteSpace(congViecHienTaiText))
                                        {
                                            CongViec congViecHienTai = null;
                                            congViecHienTai = uow.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec like ?", congViecHienTaiText));
                                            if (congViecHienTai == null)
                                            {
                                                congViecHienTai = new CongViec(uow);
                                                congViecHienTai.TenCongViec = congViecHienTaiText;
                                                congViecHienTai.Save();
                                            }
                                            giangVienThinhGiang.CongViecHienNay = congViecHienTai;
                                        }
                                    }
                                    #endregion

                                }
                                #region Ghi File log
                                {
                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Giảng viên thỉnh giảng: {0} - {1} không import vào phần mềm được: ", dr[idx_MaQuanLy].ToString().Trim(), dr[idx_HoTen].ToString().FullTrim()));
                                        mainLog.AppendLine(detailLog.ToString());
                                    }
                                }

                                #endregion
                            }
                        }
                            #endregion

                        if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import không thành công. Bạn có muốn xuất thông tin lỗi?") == DialogResult.Yes)
                            {
                                using (SaveFileDialog saveFile = new SaveFileDialog())
                                {
                                    saveFile.Filter = @"Text files (*.txt)|*.txt";

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
                            obs.Refresh();
                        }
                    }
                }

            }

        }       
    }
}
