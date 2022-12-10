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
    public class HoSo_ImportNhanVienSoNoiVu
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
            using (DataTable dt = DataProvider.GetDataTable(fileName, "[Sheet1$A9:AL]"))
            {
                StringBuilder mainLog = new StringBuilder();
                StringBuilder detailLog;

                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    ThongTinNhanVien thongTinNhanVien = null;

                    int soThuTu = 0;
                    int maQuanLy = 1;
                    int hoTen = 2;
                    int ngaySinh = 3;
                    int gioiTinh = 4;
                    int cMND = 5;
                    int ngayCap = 6;
                    int soBHXH = 7;
                    int chucVu = 8;
                    int pLTheoLinhVuc = 9;
                    int loai = 10;
                    int maBoPhan = 11;
                    int tenBoPhan = 12;
                    int nhomNgach = 13;
                    int maNgach = 14;
                    int bacLuong = 15;
                    int heSoLuong = 16;
                    int pCChucVu = 17;
                    int pCThamNienVuotKhung = 18;
                    int cLBaoLuu = 19;
                    int mocNangLuongLanSau = 20;
                    //
                    int trinhDoVanHoa = 21;
                    int trinhDoChuyenMon = 22;
                    int chuyenNganhDaoTao = 23;
                    int quanLyNhaNuoc = 24;
                    int lyLuanChinhTri = 25;
                    int trinhDoNgoaiNgu = 26;
                    int trinhDoTinHoc = 27;
                    //
                    int danToc = 28;
                    int tonGiao = 29;
                    int dangVien = 30;
                    //
                    int noiSinh = 31;
                    int queQuan = 32;
                    int diaChiHoKhau = 33;
                    int ngayVaoCoQuan = 34;
                    int ghiChu = 35;
                    int ngayVaoDangDuBi = 36;
                    int ngayVaoDangChinhThuc = 37;

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            //Khởi tạo bộ nhớ đệm
                            detailLog = new StringBuilder();

                            String soThuTuText = dr[soThuTu].ToString().Trim();
                            String maQuanLyText = dr[maQuanLy].ToString().Trim();
                            String hoTenText = dr[hoTen].ToString().Trim();
                            String ngaySinhText = dr[ngaySinh].ToString().Trim();
                            String gioiTinhText = dr[gioiTinh].ToString().Trim();
                            String cMNDText = dr[cMND].ToString().Trim();
                            String ngayCapText = dr[ngayCap].ToString().Trim();
                            String chucVuText = dr[chucVu].ToString().Trim();
                            String maBoPhanText = dr[maBoPhan].ToString().Trim();
                            String tenBoPhanText = dr[tenBoPhan].ToString().Trim();
                            String loaiNhanSuText = dr[loai].ToString().Trim();
                            String nhomNgachText = FullTrim(dr[nhomNgach].ToString().Trim());
                            String maNgachText = FullTrim(dr[maNgach].ToString().Trim());
                            String bacLuongText = FullTrim(dr[bacLuong].ToString().Trim());
                            String heSoLuongText = FullTrim(dr[heSoLuong].ToString().Trim());
                            String pCChucVuText = FullTrim(dr[pCChucVu].ToString().Trim());
                            String pCThamNienVuotKhungText = FullTrim(dr[pCThamNienVuotKhung].ToString().Trim());
                            String cLBaoLuuText = FullTrim(dr[cLBaoLuu].ToString().Trim());
                            String mocNangLuongLanSauText = FullTrim(dr[mocNangLuongLanSau].ToString().Trim());
                            //
                            String trinhDoVanHoaText = FullTrim(dr[trinhDoVanHoa].ToString().Trim());
                            String trinhDoChuyenMonText = FullTrim(dr[trinhDoChuyenMon].ToString().Trim());
                            String chuyenNganhDaoTaoText = FullTrim(dr[chuyenNganhDaoTao].ToString().Trim());
                            String quanLyNhaNuocText = FullTrim(dr[quanLyNhaNuoc].ToString().Trim());
                            String lyLuanChinhTriText = FullTrim(dr[lyLuanChinhTri].ToString().Trim());
                            String trinhDoNgoaiNguText = FullTrim(dr[trinhDoNgoaiNgu].ToString().Trim());
                            String trinhDoTinHocText = FullTrim(dr[trinhDoTinHoc].ToString().Trim());
                            //
                            String danTocText = FullTrim(dr[danToc].ToString().Trim());
                            String tonGiaoText = FullTrim(dr[tonGiao].ToString().Trim());
                            String dangVienText = FullTrim(dr[dangVien].ToString().Trim());
                            //
                            String noiSinhText = FullTrim(dr[noiSinh].ToString().Trim());
                            String queQuanText = FullTrim(dr[queQuan].ToString().Trim());
                            String diaChiHoKhauText = dr[diaChiHoKhau].ToString().Trim();
                            String ngayVaoCoQuanText = dr[ngayVaoCoQuan].ToString().Trim();
                            String ghiChuText = FullTrim(dr[ghiChu].ToString().Trim());
                            String ngayVaoDangDuBiText = FullTrim(dr[ngayVaoDangDuBi].ToString().Trim());
                            String ngayVaoDangChinhThucText = FullTrim(dr[ngayVaoDangChinhThuc].ToString().Trim());


                            #region Thông tin giảng viên thỉnh giảng
                            {
                                //Tìm giảng viên theo mã quản lý          
                                thongTinNhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", maQuanLyText));
                                if (thongTinNhanVien == null)
                                {
                                    thongTinNhanVien = new ThongTinNhanVien(uow);

                                    #region Mã quản lý
                                    {
                                        if (!string.IsNullOrEmpty(maQuanLyText))
                                        {
                                            thongTinNhanVien.MaQuanLy = maQuanLyText;
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
                                        if (!string.IsNullOrEmpty(hoTenText))
                                        {
                                            thongTinNhanVien.Ho = XuLyHo(hoTenText);
                                            thongTinNhanVien.Ten = XuLyTen(hoTenText);
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                                " + Thiếu thông tin họ tên");
                                        }
                                    }
                                    #endregion

                                    #region Ngày sinh
                                    {
                                        if (!string.IsNullOrEmpty(ngaySinhText))
                                        {
                                            try
                                            {
                                                thongTinNhanVien.NgaySinh = Convert.ToDateTime(ngaySinhText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngaySinhText);
                                            }
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(" + Thiếu thông tin ngày sinh hoặc không đúng định dạng dd/MM/yyyy.");
                                        }
                                    }
                                    #endregion

                                    #region Giới tính
                                    {
                                        if (!string.IsNullOrEmpty(gioiTinhText))
                                        {
                                            if (gioiTinhText.ToLower() == "nam")
                                                thongTinNhanVien.GioiTinh = GioiTinhEnum.Nam;
                                            else if (gioiTinhText.ToLower() == "nữ" || gioiTinhText.ToLower() == "nu")
                                                thongTinNhanVien.GioiTinh = GioiTinhEnum.Nu;
                                            else
                                            {
                                                detailLog.AppendLine(" + Giới tính không hợp lệ: " + gioiTinh);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Số CMND
                                    {
                                        if (!string.IsNullOrWhiteSpace(cMNDText))
                                        {
                                            thongTinNhanVien.CMND = cMNDText;
                                        }
                                    }
                                    #endregion

                                    #region Ngày cấp CMND
                                    {
                                        if (!string.IsNullOrEmpty(ngayCapText))
                                        {
                                            try
                                            {
                                                thongTinNhanVien.NgayCap = Convert.ToDateTime(ngayCapText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + ngayCapText);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Chức vụ - Loại nhân sự
                                    {
                                        if (!string.IsNullOrEmpty(chucVuText))
                                        {
                                            ChucVu ChucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu = ?", chucVuText));
                                            if (ChucVu == null)
                                            {
                                                ChucVu = new ChucVu(uow);
                                                ChucVu.MaQuanLy = chucVuText;
                                                ChucVu.TenChucVu = chucVuText;
                                            }
                                            thongTinNhanVien.ChucVu = ChucVu;                                           
                                        }
                                    }
                                    #endregion

                                    #region Loại nhân sự
                                    LoaiNhanSu LoaiNhanSu;
                                    if (!string.IsNullOrEmpty(loaiNhanSuText))
                                    {
                                        LoaiNhanSu = uow.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu = ?", loaiNhanSuText));
                                        if (LoaiNhanSu != null)
                                        {
                                            thongTinNhanVien.LoaiNhanSu = LoaiNhanSu;
                                        }
                                    }
                                    #endregion

                                    #region Mã đơn vị
                                    {
                                        BoPhan BoPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy = ?", maBoPhanText));
                                        if (BoPhan != null)
                                        {
                                            thongTinNhanVien.BoPhan = BoPhan;
                                        }
                                        else
                                        {
                                            detailLog.AppendLine(
                                              " + Sai thông tin bộ phận");
                                        }
                                    }
                                    #endregion

                                    //
                                    NhanVienThongTinLuong NhanVienThongTinLuong = new NhanVienThongTinLuong(uow);
                                    thongTinNhanVien.NhanVienThongTinLuong = NhanVienThongTinLuong;

                                    #region Nhóm ngạch - Ngạch lương - Bậc lương - HSL
                                    {
                                        if (!string.IsNullOrEmpty(nhomNgachText))
                                        {
                                            NhomNgachLuong NhomNgachLuong = uow.FindObject<NhomNgachLuong>(CriteriaOperator.Parse("MaQuanLy = ?", nhomNgachText));
                                            if (NhomNgachLuong == null)
                                            {
                                                NhomNgachLuong = new NhomNgachLuong(uow);
                                                NhomNgachLuong.TenNhomNgachLuong = nhomNgachText;
                                                NhomNgachLuong.MaQuanLy = nhomNgachText;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(maNgachText))
                                        {
                                            NgachLuong NgachLuong = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy = ?", maNgachText));
                                            if (NgachLuong != null)
                                            {
                                                NhanVienThongTinLuong.NgachLuong = NgachLuong;
                                                if (!string.IsNullOrEmpty(bacLuongText))
                                                {
                                                    BacLuong BacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("MaQuanLy = ? and NgachLuong =?", bacLuongText, NgachLuong.Oid));
                                                    if (BacLuong != null)
                                                    {
                                                        NhanVienThongTinLuong.BacLuong = BacLuong;
                                                        NhanVienThongTinLuong.HeSoLuong = BacLuong.HeSoLuong;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Phụ cấp chức vụ
                                    {
                                        if (!string.IsNullOrEmpty(pCChucVuText))
                                        {
                                            try
                                            {
                                                NhanVienThongTinLuong.HSPCChucVu = Convert.ToDecimal(pCChucVuText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + PC chức vụ không hợp lệ: " + pCChucVuText);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Phụ cấp thâm niên vượt khung
                                    {
                                        if (!string.IsNullOrEmpty(pCThamNienVuotKhungText))
                                        {
                                            try
                                            {
                                                NhanVienThongTinLuong.VuotKhung = Convert.ToInt32(pCThamNienVuotKhungText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + PC thâm niên vượt khung không hợp lệ: " + pCThamNienVuotKhungText);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Chênh lệch bảo lưu
                                    {
                                        if (!string.IsNullOrEmpty(cLBaoLuuText))
                                        {
                                            try
                                            {
                                                NhanVienThongTinLuong.ChenhLechBaoLuuLuong = Convert.ToDecimal(cLBaoLuuText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Chênh lệch bảo lưu không hợp lệ: " + cLBaoLuuText);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Mốc nâng lương lần sau
                                    {
                                        if (!string.IsNullOrEmpty(mocNangLuongLanSauText))
                                        {
                                            try
                                            {
                                                NhanVienThongTinLuong.MocNangLuongLanSau = Convert.ToDateTime(mocNangLuongLanSauText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Mốc nâng lương lần sau không hợp lệ: " + mocNangLuongLanSauText);
                                            }
                                        }
                                    }
                                    #endregion

                                    //
                                    NhanVienTrinhDo NhanVienTrinhDo = new NhanVienTrinhDo(uow);
                                    thongTinNhanVien.NhanVienTrinhDo = NhanVienTrinhDo;

                                    #region Nhân viên trình độ
                                    {
                                        TrinhDoVanHoa TrinhDoVanHoa = null;
                                        TrinhDoChuyenMon TrinhDoChuyenMon = null;
                                        ChuyenMonDaoTao chuyenMonDaoTao = null;
                                        QuanLyNhaNuoc QuanLyNhaNuoc = null;
                                        LyLuanChinhTri LyLuanChinhTri = null;
                                        TrinhDoNgoaiNgu TrinhDoNgoaiNgu = null;
                                        TrinhDoTinHoc TrinhDoTinHoc = null;

                                        if (!string.IsNullOrEmpty(trinhDoVanHoaText))
                                        {
                                            TrinhDoVanHoa = uow.FindObject<TrinhDoVanHoa>(CriteriaOperator.Parse("TenTrinhDoVanHoa Like ?", trinhDoVanHoaText));
                                            if (TrinhDoVanHoa == null)
                                            {
                                                TrinhDoVanHoa = new TrinhDoVanHoa(uow);
                                                TrinhDoVanHoa.TenTrinhDoVanHoa = trinhDoVanHoaText;
                                                TrinhDoVanHoa.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoVanHoaText);
                                            }
                                            NhanVienTrinhDo.TrinhDoVanHoa = TrinhDoVanHoa;
                                        }

                                        if (!string.IsNullOrEmpty(trinhDoChuyenMonText))
                                        {
                                            TrinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ?", trinhDoChuyenMonText));
                                            if (TrinhDoChuyenMon == null)
                                            {
                                                TrinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                                TrinhDoChuyenMon.TenTrinhDoChuyenMon = trinhDoVanHoaText;
                                                TrinhDoChuyenMon.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoChuyenMonText);
                                            }
                                            NhanVienTrinhDo.TrinhDoChuyenMon = TrinhDoChuyenMon;
                                        }

                                        if (!string.IsNullOrEmpty(chuyenNganhDaoTaoText))
                                        {
                                            chuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao Like ?", chuyenNganhDaoTaoText));
                                            if (chuyenMonDaoTao == null)
                                            {
                                                chuyenMonDaoTao = new ChuyenMonDaoTao(uow);
                                                chuyenMonDaoTao.TenChuyenMonDaoTao = chuyenNganhDaoTaoText;
                                                chuyenMonDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(chuyenNganhDaoTaoText);
                                            }
                                            NhanVienTrinhDo.ChuyenMonDaoTao = chuyenMonDaoTao;
                                        }

                                        if (!string.IsNullOrEmpty(lyLuanChinhTriText))
                                        {
                                            LyLuanChinhTri = uow.FindObject<LyLuanChinhTri>(CriteriaOperator.Parse("TenLyLuanChinhTri Like ?", lyLuanChinhTriText));
                                            if (LyLuanChinhTri == null)
                                            {
                                                LyLuanChinhTri = new LyLuanChinhTri(uow);
                                                LyLuanChinhTri.TenLyLuanChinhTri = lyLuanChinhTriText;
                                                LyLuanChinhTri.MaQuanLy = HamDungChung.TaoChuVietTat(lyLuanChinhTriText);
                                            }
                                            NhanVienTrinhDo.LyLuanChinhTri = LyLuanChinhTri;
                                        }

                                        if (!string.IsNullOrEmpty(quanLyNhaNuocText))
                                        {
                                            QuanLyNhaNuoc = uow.FindObject<QuanLyNhaNuoc>(CriteriaOperator.Parse("TenQuanLyNhaNuoc Like ?", quanLyNhaNuocText));
                                            if (QuanLyNhaNuoc == null)
                                            {
                                                QuanLyNhaNuoc = new QuanLyNhaNuoc(uow);
                                                QuanLyNhaNuoc.TenQuanLyNhaNuoc = quanLyNhaNuocText;
                                                QuanLyNhaNuoc.MaQuanLy = HamDungChung.TaoChuVietTat(quanLyNhaNuocText);
                                            }
                                            NhanVienTrinhDo.QuanLyNhaNuoc = QuanLyNhaNuoc;
                                        }

                                        if (!string.IsNullOrEmpty(trinhDoTinHocText))
                                        {
                                            TrinhDoTinHoc = uow.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("TenTrinhDoTinHoc Like ?", trinhDoTinHocText));
                                            if (TrinhDoTinHoc == null)
                                            {
                                                TrinhDoTinHoc = new TrinhDoTinHoc(uow);
                                                TrinhDoTinHoc.TenTrinhDoTinHoc = trinhDoTinHocText;
                                                TrinhDoTinHoc.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoTinHocText);
                                            }
                                            NhanVienTrinhDo.TrinhDoTinHoc = TrinhDoTinHoc;
                                        }

                                        if (!string.IsNullOrEmpty(trinhDoNgoaiNguText))
                                        {
                                            TrinhDoNgoaiNgu = uow.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("TenTrinhDoNgoaiNgu Like ?", trinhDoNgoaiNguText));
                                            if (TrinhDoNgoaiNgu == null)
                                            {
                                                TrinhDoNgoaiNgu = new TrinhDoNgoaiNgu(uow);
                                                TrinhDoNgoaiNgu.TenTrinhDoNgoaiNgu = trinhDoNgoaiNguText;
                                                TrinhDoNgoaiNgu.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoNgoaiNguText);
                                            }
                                            NhanVienTrinhDo.TrinhDoNgoaiNgu = TrinhDoNgoaiNgu;
                                        }
                                    }
                                    #endregion

                                    #region dân tộc
                                    if (!string.IsNullOrEmpty(danTocText))
                                    {
                                        DanToc DanToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc Like ?", danTocText));
                                        if (DanToc == null)
                                        {
                                            DanToc = new DanToc(uow);
                                            DanToc.TenDanToc = danTocText;
                                            DanToc.MaQuanLy = HamDungChung.TaoChuVietTat(danTocText);
                                        }
                                        thongTinNhanVien.DanToc = DanToc;
                                    }
                                    #endregion

                                    #region tôn giáo
                                    if (!string.IsNullOrEmpty(tonGiaoText))
                                    {
                                        TonGiao TonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao Like ?", tonGiaoText));
                                        if (TonGiao == null)
                                        {
                                            TonGiao = new TonGiao(uow);
                                            TonGiao.TenTonGiao = tonGiaoText;
                                            TonGiao.MaQuanLy = HamDungChung.TaoChuVietTat(tonGiaoText);
                                        }
                                        thongTinNhanVien.TonGiao = TonGiao;
                                    }
                                    #endregion

                                    #region Đảng viên
                                    if (!string.IsNullOrEmpty(dangVienText))
                                    {
                                        thongTinNhanVien.LaDangVien = true;
                                    }
                                    #endregion

                                    #region Nơi sinh
                                    {
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
                                            thongTinNhanVien.NoiSinh = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region quê quán
                                    if (!string.IsNullOrEmpty(queQuanText))
                                    {
                                        DiaChi diaChi;
                                        diaChi = new DiaChi(uow);
                                        diaChi.SoNha = queQuanText;
                                        thongTinNhanVien.QueQuan = diaChi;
                                    }
                                    #endregion

                                    #region Thường trú
                                    {
                                        if (!string.IsNullOrEmpty(diaChiHoKhauText))
                                        {
                                            DiaChi diaChi = null;
                                            diaChi = uow.FindObject<DiaChi>(CriteriaOperator.Parse("SoNha like ?", diaChiHoKhauText));
                                            if (diaChi == null)
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = diaChiHoKhauText;
                                                diaChi.Save();
                                            }
                                            thongTinNhanVien.DiaChiThuongTru = diaChi;
                                        }
                                    }
                                    #endregion

                                    #region Ngày vào cơ quan
                                    {
                                        if (!string.IsNullOrEmpty(ngayVaoCoQuanText))
                                        {
                                            try
                                            {
                                                thongTinNhanVien.NgayVaoCoQuan = Convert.ToDateTime(ngayVaoCoQuanText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày vào cơ quan không hợp lệ: " + ngayVaoCoQuanText);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Ghi chú
                                    {
                                        if (!string.IsNullOrEmpty(ghiChuText))
                                        {
                                            thongTinNhanVien.GhiChu = ghiChuText;
                                        }
                                    }
                                    #endregion

                                    #region Ngày vào Đảng dự bị
                                    {
                                        if (!string.IsNullOrEmpty(ngayVaoDangDuBiText))
                                        {
                                            try
                                            {
                                                thongTinNhanVien.NgayVaoDangDuBi = Convert.ToDateTime(ngayVaoDangDuBiText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày vào đảng dự bị không hợp lệ: " + ngayVaoDangDuBiText);
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Ngày vào Đảng chính thức
                                    {
                                        if (!string.IsNullOrEmpty(ngayVaoDangChinhThucText))
                                        {
                                            try
                                            {
                                                thongTinNhanVien.NgayVaoDangChinhThuc = Convert.ToDateTime(ngayVaoDangChinhThucText);
                                            }
                                            catch
                                            {
                                                detailLog.AppendLine(" + Ngày vào đảng chính thức không hợp lệ: " + ngayVaoDangChinhThucText);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    detailLog.AppendLine("+ Mã quản lý đã tồn tại trong hệ thống");
                                }

                                #region Ghi File log
                                {
                                    //Đưa thông tin bị lỗi vào blog
                                    if (detailLog.Length > 0)
                                    {
                                        mainLog.AppendLine(string.Format("- Giảng viên thỉnh giảng: {0} - {1} không import vào phần mềm được: ", maQuanLyText,hoTenText));
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
