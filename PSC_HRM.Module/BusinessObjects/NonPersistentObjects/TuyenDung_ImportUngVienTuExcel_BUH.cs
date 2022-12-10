using System;

using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using System.Text;
using System.IO;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Import ứng viên từ file excel")]
    public class TuyenDung_ImportUngVienTuExcel_BUH : BaseObject
    {
        // Fields...
        bool oke;
        int Loi = 0;
        int ThanhCong = 0;

        public TuyenDung_ImportUngVienTuExcel_BUH(Session session) : base(session) { }

        public void XuLy(IObjectSpace obs, QuanLyTuyenDung qlTuyenDung)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.FileName = "";
                dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A4:AM]"))
                    {
                        if (dt != null)
                        {
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                var mainLog = new StringBuilder();                               
                                UngVien ungVien;   
                                DiaChi diaChi;   

                                int soThuTu = 0;                            
                                int soBaoDanh = 1;                               
                                int Ho = 2;
                                int Ten = 3;                               
                                int gioiTinh = 4;                   
                                int ngaySinh = 5;                               
                                int noiSinh = 6;
                                int CMND = 7;                               
                                int ngayCap = 8;                            
                                int noiCap = 9;
                                int queQuan = 10;
                                int diaChiThuongTru = 11;
                                int noiOHienNay = 12;
                                int email = 13;                           
                                int dienThoaiDiDong = 14;
                                int tinhTrangHonNhan = 15;
                                int danToc = 16;
                                int tonGiao = 17;
                                int quocTich = 18;
                                int viTriTuyenDung = 19;
                                int loaiTuyenDung = 20;
                                int boPhan = 21;
                                int hinhThucTuyenDung = 22;
                                int ngayDuTuyen = 23;
                                int sinhVienGiuLaiTruong = 24;
                                int coQuanCu = 25;
                                int ghiChu = 26;
                                int trinhDoChuyenMon = 27;
                                int chuyenNganhDaoTao = 28;
                                int truongDaoTao = 29;
                                int hinhThucDaoTao = 30;
                                int namTotNghiep = 31;                                
                                int xepLoai = 32;                                
                                int diemTrungBinh = 33;
                                int kinhNghiem = 34;
                                int trinhDoTinHoc = 35;
                                int ngoaiNguChinh = 36;
                                int trinhdoNgoaiNguChinh = 37;
                                int tenChungChiKhac = 38;

                                using (DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        String soThuTuText = item[soThuTu].ToString();
                                        String soBaoDanhText = item[soBaoDanh].ToString();
                                        String HoText = item[Ho].ToString().Trim();
                                        String TenText = item[Ten].ToString().Trim();
                                        String gioiTinhText = item[gioiTinh].ToString().Trim();
                                        String ngaySinhText = item[ngaySinh].ToString();
                                        String noiSinhText = item[noiSinh].ToString().Trim();
                                        String CMNDText = item[CMND].ToString().Trim();
                                        String ngayCapText = item[ngayCap].ToString();
                                        String noiCapText = item[noiCap].ToString().Trim();
                                        String queQuanText = item[queQuan].ToString();
                                        String diaChiThuongTruText = item[diaChiThuongTru].ToString();
                                        String noiOHienNayText = item[noiOHienNay].ToString();
                                        String emailText = item[email].ToString().Trim();
                                        String dienThoaiDiDongText = item[dienThoaiDiDong].ToString();
                                        String tinhTrangHonNhanText = item[tinhTrangHonNhan].ToString().Trim();
                                        String danTocText = item[danToc].ToString();
                                        String tonGiaoText = item[tonGiao].ToString().Trim();
                                        String quocTichText = item[quocTich].ToString().Trim();
                                        String viTriTuyenDungText = item[viTriTuyenDung].ToString();
                                        String loaiTuyenDungText = item[loaiTuyenDung].ToString();
                                        String boPhanText = item[boPhan].ToString();
                                        String hinhThucTuyenDungText = item[hinhThucTuyenDung].ToString();
                                        String ngayDuTuyenText = item[ngayDuTuyen].ToString();
                                        String sinhVienGiuLaiTruongText = item[sinhVienGiuLaiTruong].ToString();
                                        String coQuanCuText = item[coQuanCu].ToString();
                                        String ghiChuText = item[ghiChu].ToString();
                                        String trinhDoChuyenMonText = item[trinhDoChuyenMon].ToString().Trim();
                                        String chuyenNganhDaoTaoText = item[chuyenNganhDaoTao].ToString().Trim();
                                        String truongDaoTaoText = item[truongDaoTao].ToString().Trim();
                                        String hinhThucDaoTaoText = item[hinhThucDaoTao].ToString().Trim();
                                        String namTotNghiepText = item[namTotNghiep].ToString().Trim();
                                        String xepLoaiText = item[xepLoai].ToString();
                                        String diemTrungBinhText = item[diemTrungBinh].ToString();
                                        String kinhNghiemText = item[kinhNghiem].ToString();
                                        String trinhDoTinHocText = item[trinhDoTinHoc].ToString().Trim();
                                        String ngoaiNguChinhText = item[ngoaiNguChinh].ToString().Trim();
                                        String trinhdoNgoaiNguChinhText = item[trinhdoNgoaiNguChinh].ToString().Trim();
                                        String tenChungChiKhacText = item[tenChungChiKhac].ToString().Trim();

                                        oke = true;
                                        var errorLog = new StringBuilder();

                                        ungVien = Session.FindObject<UngVien>(CriteriaOperator.Parse("CMND_UngVien=?", CMNDText));
                                        if (ungVien != null)
                                        {
                                            errorLog.AppendLine(string.Format("+ CMND :{0} đã tồn tại trong hệ thống", CMNDText));
                                        }

                                        else
                                        {
                                            #region Kiểm tra dữ liệu import
                                            ungVien = new UngVien(uow);
                                            ungVien.QuanLyTuyenDung = uow.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);

                                            //nhu cầu tuyển dụng                                     
                                            //Bộ phận
                                            BoPhan bophan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy=?", boPhanText));
                                            if (bophan == null)
                                                errorLog.AppendLine(" + Sai thông tin bộ phận.");

                                            //Loại tuyển dụng
                                            LoaiTuyenDung loaituyendung = uow.FindObject<LoaiTuyenDung>(CriteriaOperator.Parse("TenLoaiTuyenDung=?", loaiTuyenDungText));
                                            
                                            //Vị trí tuyển dụng
                                            ViTriTuyenDung vitrituyendung = uow.FindObject<ViTriTuyenDung>(CriteriaOperator.Parse("TenViTriTuyenDung=? and LoaiTuyenDung=?", viTriTuyenDungText, loaituyendung.Oid));
                                            if (vitrituyendung == null)
                                                errorLog.AppendLine(" + Sai thông tin vị trí tuyển dụng.");

                                            if (bophan != null && vitrituyendung != null)
                                            {
                                                NhuCauTuyenDung nhucau = uow.FindObject<NhuCauTuyenDung>(CriteriaOperator.Parse(
                                                                                "ViTriTuyenDung=? and BoPhan=? and QuanLyTuyenDung=?"
                                                                                , vitrituyendung.Oid
                                                                                , bophan.Oid
                                                                                , qlTuyenDung.Oid));
                                                if (nhucau != null)
                                                {
                                                    ungVien.NhuCauTuyenDung = nhucau;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Sai thông tin nhu cầu tuyển dụng.");
                                                }
                                            }

                                            //Số báo danh
                                            if (!string.IsNullOrEmpty(soBaoDanhText))
                                                ungVien.SoBaoDanh = soBaoDanhText;
                                            else
                                                errorLog.AppendLine(" + Thiếu Số báo danh.");

                                            //họ
                                            if (!string.IsNullOrEmpty(HoText))
                                                ungVien.Ho = HamDungChung.VietHoaChuDau(HoText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                            else
                                                errorLog.AppendLine(" + Thiếu họ.");

                                            //tên
                                            if (!string.IsNullOrEmpty(TenText))
                                                ungVien.Ten = HamDungChung.VietHoaChuDau(new string[] { TenText });
                                            else
                                                errorLog.AppendLine(" + Thiếu tên.");

                                            //giới tính
                                            if (!string.IsNullOrEmpty(gioiTinhText) &&
                                                gioiTinhText.ToLower() == "nam")
                                                ungVien.GioiTinh = GioiTinhEnum.Nam;
                                            else
                                                ungVien.GioiTinh = GioiTinhEnum.Nu;

                                            //ngày sinh
                                            if (!string.IsNullOrEmpty(ngaySinhText))
                                            {
                                                try
                                                {
                                                    DateTime NgaySinh = Convert.ToDateTime(ngaySinhText);
                                                    if (NgaySinh != null && NgaySinh != DateTime.MinValue)
                                                        ungVien.NgaySinh = Convert.ToDateTime(ngaySinhText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngaySinhText);
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu thông tin ngày sinh");
                                            }


                                            //nơi sinh
                                            if (!string.IsNullOrEmpty(noiSinhText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = noiSinhText;
                                                ungVien.NoiSinh = diaChi;
                                            }

                                            //CMND
                                            if (!string.IsNullOrEmpty(CMNDText))
                                                ungVien.CMND_UngVien = CMNDText;
                                            else
                                                errorLog.AppendLine(" + Thiếu cmnd.");

                                            //ngày cấp                                
                                            if (!string.IsNullOrEmpty(ngayCapText))
                                            {
                                                try
                                                {
                                                    DateTime NgayCap = Convert.ToDateTime(ngayCapText);
                                                    if (NgayCap != null && NgayCap != DateTime.MinValue)
                                                        ungVien.NgayCap = Convert.ToDateTime(ngayCapText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày cấp không hợp lệ: " + ngayCapText);
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu thông tin ngày cấp");
                                            }


                                            //nơi cấp
                                            if (!string.IsNullOrEmpty(noiCapText))
                                            {
                                                TinhThanh tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh Like ?", noiCapText));
                                                if (tinhThanh == null)
                                                {
                                                    tinhThanh = new TinhThanh(uow);
                                                    tinhThanh.TenTinhThanh = noiCapText;
                                                    tinhThanh.MaQuanLy = HamDungChung.TaoChuVietTat(noiCapText);
                                                }
                                                ungVien.NoiCap = tinhThanh;
                                            }

                                            //quê quán
                                            if (!string.IsNullOrEmpty(queQuanText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = queQuanText;
                                                ungVien.QueQuan = diaChi;
                                            }

                                            //địa chỉ thường trú
                                            if (!string.IsNullOrEmpty(diaChiThuongTruText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = diaChiThuongTruText;
                                                ungVien.DiaChiThuongTru = diaChi;
                                            }

                                            //nơi ở hiện nay
                                            if (!string.IsNullOrEmpty(noiOHienNayText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = noiOHienNayText;
                                                ungVien.NoiOHienNay = diaChi;
                                            }

                                            //Email
                                            if (!string.IsNullOrEmpty(emailText))
                                                ungVien.Email = emailText;

                                            //điện thoại di động
                                            if (!string.IsNullOrEmpty(dienThoaiDiDongText))
                                                ungVien.DienThoaiDiDong = dienThoaiDiDongText;

                                            //tình trạng hôn nhân
                                            if (!string.IsNullOrEmpty(tinhTrangHonNhanText))
                                            {
                                                TinhTrangHonNhan TinhTrangHonNhan = uow.FindObject<TinhTrangHonNhan>(CriteriaOperator.Parse("TenTinhTrangHonNhan Like ?", tinhTrangHonNhanText));
                                                if (TinhTrangHonNhan == null)
                                                {
                                                    TinhTrangHonNhan = new TinhTrangHonNhan(uow);
                                                    TinhTrangHonNhan.TenTinhTrangHonNhan = tinhTrangHonNhanText;
                                                    TinhTrangHonNhan.MaQuanLy = HamDungChung.TaoChuVietTat(tinhTrangHonNhanText);
                                                }
                                                ungVien.TinhTrangHonNhan = TinhTrangHonNhan;
                                            }

                                            //dân tộc
                                            if (!string.IsNullOrEmpty(danTocText))
                                            {
                                                DanToc DanToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc Like ?", danTocText));
                                                if (DanToc == null)
                                                {
                                                    DanToc = new DanToc(uow);
                                                    DanToc.TenDanToc = danTocText;
                                                    DanToc.MaQuanLy = HamDungChung.TaoChuVietTat(danTocText);
                                                }
                                                ungVien.DanToc = DanToc;
                                            }

                                            //tôn giáo
                                            if (!string.IsNullOrEmpty(tonGiaoText))
                                            {
                                                TonGiao TonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao Like ?", tonGiaoText));
                                                if (TonGiao == null)
                                                {
                                                    TonGiao = new TonGiao(uow);
                                                    TonGiao.TenTonGiao = tonGiaoText;
                                                    TonGiao.MaQuanLy = HamDungChung.TaoChuVietTat(tonGiaoText);
                                                }
                                                ungVien.TonGiao = TonGiao;
                                            }

                                            //quốc tịch
                                            if (!string.IsNullOrEmpty(quocTichText))
                                            {
                                                QuocGia QuocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia Like ?", quocTichText));
                                                if (QuocTich == null)
                                                {
                                                    QuocTich = new QuocGia(uow);
                                                    QuocTich.TenQuocGia = quocTichText;
                                                    QuocTich.MaQuanLy = HamDungChung.TaoChuVietTat(quocTichText);
                                                }
                                                ungVien.QuocTich = QuocTich;
                                            }


                                            //hình thức tuyển dụng
                                            if (!string.IsNullOrEmpty(hinhThucTuyenDungText)
                                                && hinhThucTuyenDungText.ToLower() == "thi tuyển")
                                                ungVien.HinhThucTuyenDung = HinhThucTuyenDungEnum.ThiTuyen;
                                            else
                                                ungVien.HinhThucTuyenDung = HinhThucTuyenDungEnum.XetTuyen;

                                            //ngày dự tuyển
                                            if (!string.IsNullOrEmpty(ngayDuTuyenText))
                                            {
                                                try
                                                {
                                                    DateTime NgayDuTuyen = Convert.ToDateTime(ngayDuTuyenText);
                                                    if (NgayDuTuyen != null && NgayDuTuyen != DateTime.MinValue)
                                                        ungVien.NgayDuTuyen = Convert.ToDateTime(ngayDuTuyenText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày dự tuyển không hợp lệ: " + ngayDuTuyenText);
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu thông tin ngày dự tuyển");
                                            }


                                            //giữ lại trường
                                            if (!string.IsNullOrEmpty(sinhVienGiuLaiTruongText)
                                                && sinhVienGiuLaiTruongText.ToLower() == "true")
                                                ungVien.SinhVienGiuLaiTruong = true;
                                            else
                                                ungVien.SinhVienGiuLaiTruong = false;

                                            //cơ quan cũ
                                            if (!string.IsNullOrEmpty(coQuanCuText))
                                                ungVien.CoQuanCu = coQuanCuText;

                                            //ghi chú
                                            if (!string.IsNullOrEmpty(ghiChuText))
                                                ungVien.GhiChu = ghiChuText;

                                            //trình độ chuyên môn 
                                            if (!string.IsNullOrEmpty(trinhDoChuyenMonText))
                                            {
                                                TrinhDoChuyenMon TrinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("MaQuanLy Like ?", trinhDoChuyenMonText));
                                                if (TrinhDoChuyenMon == null)
                                                {
                                                    TrinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                                    TrinhDoChuyenMon.TenTrinhDoChuyenMon = trinhDoChuyenMonText;
                                                    TrinhDoChuyenMon.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoChuyenMonText);

                                                }
                                                ungVien.TrinhDoChuyenMon = TrinhDoChuyenMon;

                                                HoSo.VanBang vanBang = new HoSo.VanBang(uow);
                                                vanBang.TrinhDoChuyenMon = TrinhDoChuyenMon;

                                                //chuyên ngành đào tạo
                                                if (!string.IsNullOrEmpty(chuyenNganhDaoTaoText))
                                                {
                                                    ChuyenMonDaoTao chuyenMonDaoTao = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao Like ?", chuyenNganhDaoTaoText));
                                                    if (chuyenMonDaoTao == null)
                                                    {
                                                        chuyenMonDaoTao = new ChuyenMonDaoTao(uow);
                                                        chuyenMonDaoTao.TenChuyenMonDaoTao = chuyenNganhDaoTaoText;
                                                        chuyenMonDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(chuyenNganhDaoTaoText);

                                                    }
                                                    ungVien.ChuyenMonDaoTao = chuyenMonDaoTao;

                                                    vanBang.ChuyenMonDaoTao = chuyenMonDaoTao;
                                                }

                                                //trường đào tạo
                                                if (!string.IsNullOrEmpty(truongDaoTaoText))
                                                {
                                                    TruongDaoTao TruongDaoTao = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao Like ?", truongDaoTaoText));
                                                    if (TruongDaoTao == null)
                                                    {
                                                        TruongDaoTao = new TruongDaoTao(uow);
                                                        TruongDaoTao.TenTruongDaoTao = truongDaoTaoText;
                                                        TruongDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(truongDaoTaoText);

                                                    }
                                                    ungVien.TruongDaoTao = TruongDaoTao;

                                                    vanBang.TruongDaoTao = TruongDaoTao;
                                                }

                                                //hình thức đào tạo
                                                if (!string.IsNullOrEmpty(hinhThucDaoTaoText))
                                                {
                                                    HinhThucDaoTao HinhThucDaoTao = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao Like ?", hinhThucDaoTaoText));
                                                    if (HinhThucDaoTao == null)
                                                    {
                                                        HinhThucDaoTao = new HinhThucDaoTao(uow);
                                                        HinhThucDaoTao.TenHinhThucDaoTao = hinhThucDaoTaoText;
                                                        HinhThucDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(hinhThucDaoTaoText);
                                                    }
                                                    ungVien.HinhThucDaoTao = HinhThucDaoTao;

                                                    vanBang.HinhThucDaoTao = HinhThucDaoTao;
                                                }

                                                //năm tốt nghiệp
                                                if (!string.IsNullOrEmpty(namTotNghiepText))
                                                {
                                                    int NamTotNghiep = Convert.ToInt32(namTotNghiepText);
                                                    if (NamTotNghiep != null)
                                                    {
                                                        ungVien.NamTotNghiep = NamTotNghiep;

                                                        vanBang.NamTotNghiep = NamTotNghiep;
                                                    }
                                                    else
                                                        errorLog.AppendLine(" + Năm tốt nghiệp không hợp lệ: " + namTotNghiepText);
                                                }

                                              
                                                //xếp loại
                                                if (!string.IsNullOrEmpty(xepLoaiText))
                                                {
                                                    switch (xepLoaiText.ToLower())
                                                    {
                                                        case "xuất sắc":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.XuatSac;
                                                            break;
                                                        case "giỏi":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.Gioi;
                                                            break;
                                                        case "khá":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.Kha;
                                                            break;
                                                        case "trung bình":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.TrungBinh;
                                                            break;
                                                        case "trung bình khá":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.TrungBinhKha;
                                                            break;
                                                        default:
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.KhongChon;
                                                            break;
                                                    }
                                                    //điểm trung bình
                                                    if (!string.IsNullOrEmpty(diemTrungBinhText))
                                                    {
                                                        try
                                                        {
                                                            vanBang.DiemTrungBinh = Convert.ToDecimal(diemTrungBinhText);
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            errorLog.AppendLine(" + Điểm trung bình không hợp lệ: " + diemTrungBinhText);
                                                        }
                                                    }
                                                    ungVien.ListVanBang.Add(vanBang);
                                                }
                                            }

                                            //kinh nghiệm
                                            if (!string.IsNullOrEmpty(kinhNghiemText))
                                                ungVien.KinhNghiem = kinhNghiemText;

                                            //trình độ tin học
                                            if (!string.IsNullOrEmpty(trinhDoTinHocText))
                                            {
                                                TrinhDoTinHoc TrinhDoTinHoc = uow.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("MaQuanLy Like ?", trinhDoTinHocText));
                                                if (TrinhDoTinHoc == null)
                                                {
                                                    TrinhDoTinHoc = new TrinhDoTinHoc(uow);
                                                    TrinhDoTinHoc.TenTrinhDoTinHoc = trinhDoTinHocText;
                                                    TrinhDoTinHoc.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoTinHocText);
                                                }
                                                ungVien.TrinhDoTinHoc = TrinhDoTinHoc;
                                            }

                                            //ngoại ngữ chính
                                            if (!string.IsNullOrEmpty(ngoaiNguChinhText))
                                            {
                                                NgoaiNgu NgoaiNguChinh = uow.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu Like ?", ngoaiNguChinhText));
                                                if (NgoaiNguChinh == null)
                                                {
                                                    NgoaiNguChinh = new NgoaiNgu(uow);
                                                    NgoaiNguChinh.TenNgoaiNgu = ngoaiNguChinhText;
                                                    NgoaiNguChinh.MaQuanLy = HamDungChung.TaoChuVietTat(ngoaiNguChinhText);
                                                }
                                                ungVien.NgoaiNgu = NgoaiNguChinh;
                                            }
                                            else
                                            {
                                                NgoaiNgu NgoaiNguChinh = uow.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu Like ?", "Tiếng Anh"));
                                                if (ngoaiNguChinh == null)
                                                {
                                                    NgoaiNguChinh = new NgoaiNgu(uow);
                                                    NgoaiNguChinh.TenNgoaiNgu = ngoaiNguChinhText;
                                                    NgoaiNguChinh.MaQuanLy = HamDungChung.TaoChuVietTat(ngoaiNguChinhText);
                                                }
                                                ungVien.NgoaiNgu = NgoaiNguChinh;
                                            }


                                            //trình độ ngoại ngữ chính
                                            if (!string.IsNullOrEmpty(trinhdoNgoaiNguChinhText))
                                            {
                                                TrinhDoNgoaiNgu trinhDoNgoaiNgu = uow.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("MaQuanLy Like ?", trinhdoNgoaiNguChinhText));
                                                if (trinhDoNgoaiNgu == null)
                                                {
                                                    trinhDoNgoaiNgu = new TrinhDoNgoaiNgu(uow);
                                                    trinhDoNgoaiNgu.TenTrinhDoNgoaiNgu = trinhdoNgoaiNguChinhText;
                                                    trinhDoNgoaiNgu.MaQuanLy = HamDungChung.TaoChuVietTat(trinhdoNgoaiNguChinhText);
                                                }
                                                ungVien.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                            }

                                            //chứng chỉ khác
                                            if (!string.IsNullOrEmpty(tenChungChiKhacText))
                                            {
                                                LoaiChungChi loaiChungChi = uow.FindObject<LoaiChungChi>(CriteriaOperator.Parse("TenChungChi Like ?", "Chứng chỉ"));
                                                if (loaiChungChi == null)
                                                {
                                                    loaiChungChi = new LoaiChungChi(uow);
                                                    loaiChungChi.TenChungChi = "Chứng chỉ";
                                                    loaiChungChi.MaQuanLy = "CC";
                                                }

                                                HoSo.ChungChi chungChi = uow.FindObject<HoSo.ChungChi>(CriteriaOperator.Parse("LoaiChungChi like ? and TenChungChi Like ?", "Chứng chỉ", tenChungChiKhacText));
                                                if (chungChi == null)
                                                {
                                                    chungChi = new HoSo.ChungChi(uow);
                                                    chungChi.LoaiChungChi = loaiChungChi;
                                                    chungChi.TenChungChi = tenChungChiKhacText;
                                                }
                                                ungVien.ListChungChi.Add(chungChi);
                                            }
                                            #endregion
                                        }

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên:" + HoText + " " + TenText);
                                                mainLog.AppendLine(errorLog.ToString());
                                                oke = false;
                                            }
                                        }
                                        #endregion

                                        if (oke == false)
                                        {
                                            Loi++;
                                        }
                                        else
                                        {
                                            ThanhCong++;
                                        }

                                    }//end foreach

                                }
                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import Thành Công " + ThanhCong + " ứng viên - Số ứng viên Import không thành công " + Loi + ". Bạn có muốn xuất thông tin ứng viên lỗi") == DialogResult.Yes)
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
                                    //transaction.Complete();
                                    DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả ứng viên !");
                                }
                            }
                        }
                    }
                }
            }
        }

        public void XuLy_LUH(IObjectSpace obs, QuanLyTuyenDung qlTuyenDung)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.FileName = "";
                dialog.Filter = "Excel 1997-2003 files (*.xls)|*.xls";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (DataTable dt = DataProvider.GetDataTable(dialog.FileName, "[Sheet1$A5:AM]"))
                    {
                        if (dt != null)
                        {
                            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                            {
                                uow.BeginTransaction();
                                var mainLog = new StringBuilder();
                                UngVien ungVien;
                                DiaChi diaChi;

                                int soThuTu = 0;
                                int soBaoDanh = 1;
                                int Ho = 2;
                                int Ten = 3;
                                int gioiTinh = 4;
                                int chieuCao = 5;
                                int ngaySinh = 6;
                                int noiSinh = 7;
                                int CMND = 8;
                                int ngayCap = 9;
                                int noiCap = 10;
                                int queQuan = 11;
                                int diaChiThuongTru = 12;
                                int noiOHienNay = 13;
                                int dienThoaiDiDong = 14;
                                int email = 15;
                                int tinhTrangHonNhan = 16;
                                int danToc = 17;
                                int tonGiao = 18;
                                int quocTich = 19;
                                int viTriTuyenDung = 20;
                                int loaiTuyenDung = 21;
                                int boPhan = 22;
                                int hinhThucTuyenDung = 23;
                                int ngayDuTuyen = 24;
                                int ngheNghiepHienTai = 25;
                                int dangKyBaiGiang = 26;
                                int noiDaoTaoCuNhan = 27;
                                int namTotNghiep = 28;
                                int xepLoai = 29;
                                int noiDaoTaoSauDH = 30;
                                int bacDaoTao = 31;
                                int chuongTrinhDaoTaoSauDH = 32;
                                int trinhDoTinHoc = 33;
                                int ngoaiNguChinh = 34;
                                int trinhDoNgoaiNgu = 35;
                                int diemNgoaiNgu = 36;
                                int chungChi = 37;
                                int ghiChu = 38;

                                using (DialogUtil.AutoWait())
                                {
                                    foreach (DataRow item in dt.Rows)
                                    {
                                        String soThuTuText = item[soThuTu].ToString();
                                        String soBaoDanhText = item[soBaoDanh].ToString();
                                        String HoText = item[Ho].ToString().Trim();
                                        String TenText = item[Ten].ToString().Trim();
                                        String gioiTinhText = item[gioiTinh].ToString().Trim();
                                        String chieuCaoText = item[chieuCao].ToString().Trim();
                                        String ngaySinhText = item[ngaySinh].ToString();
                                        String noiSinhText = item[noiSinh].ToString().Trim();
                                        String CMNDText = item[CMND].ToString().Trim();
                                        String ngayCapText = item[ngayCap].ToString();
                                        String noiCapText = item[noiCap].ToString().Trim();
                                        String queQuanText = item[queQuan].ToString();
                                        String diaChiThuongTruText = item[diaChiThuongTru].ToString();
                                        String noiOHienNayText = item[noiOHienNay].ToString();
                                        String dienThoaiDiDongText = item[dienThoaiDiDong].ToString();
                                        String emailText = item[email].ToString().Trim();
                                        String tinhTrangHonNhanText = item[tinhTrangHonNhan].ToString().Trim();
                                        String danTocText = item[danToc].ToString();
                                        String tonGiaoText = item[tonGiao].ToString().Trim();
                                        String quocTichText = item[quocTich].ToString().Trim();
                                        String viTriTuyenDungText = item[viTriTuyenDung].ToString();
                                        String loaiTuyenDungText = item[loaiTuyenDung].ToString();
                                        String boPhanText = item[boPhan].ToString();
                                        String hinhThucTuyenDungText = item[hinhThucTuyenDung].ToString();
                                        String ngayDuTuyenText = item[ngayDuTuyen].ToString();
                                        String ngheNghiepHienTaiText = item[ngheNghiepHienTai].ToString();
                                        String dangKyBaiGiangText = item[dangKyBaiGiang].ToString();
                                        String noiDaoTaoCNText = item[noiDaoTaoCuNhan].ToString();
                                        String namTotNghiepText = item[namTotNghiep].ToString().Trim();
                                        String xepLoaiText = item[xepLoai].ToString().Trim();
                                        String noiDaoTaoSauDHText = item[noiDaoTaoSauDH].ToString().Trim();
                                        String bacDaoTaoText = item[bacDaoTao].ToString().Trim();
                                        String chuongTrinhDaoTaoSauDHText = item[chuongTrinhDaoTaoSauDH].ToString().Trim();
                                        String trinhDoTinHocText = item[trinhDoTinHoc].ToString();
                                        String ngoaiNguChinhText = item[ngoaiNguChinh].ToString();
                                        String trinhDoNgoaiNguText = item[trinhDoNgoaiNgu].ToString();
                                        String diemNgoaiNguText = item[diemNgoaiNgu].ToString().Trim();
                                        String chungChiText = item[chungChi].ToString().Trim();
                                        String ghiChuText = item[ghiChu].ToString().Trim();

                                        oke = true;
                                        var errorLog = new StringBuilder();

                                        ungVien = Session.FindObject<UngVien>(CriteriaOperator.Parse("CMND_UngVien=?", CMNDText));
                                        if (ungVien != null)
                                        {
                                            errorLog.AppendLine(string.Format("+ CMND :{0} đã tồn tại trong hệ thống", CMNDText));
                                        }

                                        else
                                        {
                                            #region Kiểm tra dữ liệu import
                                            ungVien = new UngVien(uow);
                                            ungVien.QuanLyTuyenDung = uow.GetObjectByKey<QuanLyTuyenDung>(qlTuyenDung.Oid);

                                            //nhu cầu tuyển dụng                                     
                                            //Bộ phận
                                            BoPhan bophan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("MaQuanLy=?", boPhanText));
                                            if (bophan == null)
                                                errorLog.AppendLine(" + Sai thông tin bộ phận.");

                                            //Loại tuyển dụng
                                            LoaiTuyenDung loaituyendung = uow.FindObject<LoaiTuyenDung>(CriteriaOperator.Parse("TenLoaiTuyenDung=?", loaiTuyenDungText));
                                            //Vị trí tuyển dụng
                                            ViTriTuyenDung vitrituyendung = new ViTriTuyenDung(uow);
                                            vitrituyendung = uow.FindObject<ViTriTuyenDung>(CriteriaOperator.Parse("TenViTriTuyenDung=? and LoaiTuyenDung=?", viTriTuyenDungText, loaituyendung.Oid));
                                            if (vitrituyendung == null)
                                                errorLog.AppendLine(" + Sai thông tin vị trí tuyển dụng.");

                                            if (bophan != null && vitrituyendung != null)
                                            {
                                                NhuCauTuyenDung nhucau = uow.FindObject<NhuCauTuyenDung>(CriteriaOperator.Parse(
                                                                                "ViTriTuyenDung=? and BoPhan=? and QuanLyTuyenDung=?"
                                                                                , vitrituyendung.Oid
                                                                                , bophan.Oid
                                                                                , qlTuyenDung.Oid));
                                                if (nhucau != null)
                                                {
                                                    ungVien.NhuCauTuyenDung = nhucau;
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Sai thông tin nhu cầu tuyển dụng.");
                                                }
                                            }

                                            //Số báo danh
                                            if (!string.IsNullOrEmpty(soBaoDanhText))
                                                ungVien.SoBaoDanh = soBaoDanhText;
                                            else
                                                errorLog.AppendLine(" + Thiếu Số báo danh.");

                                            //họ
                                            if (!string.IsNullOrEmpty(HoText))
                                                ungVien.Ho = HamDungChung.VietHoaChuDau(HoText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                                            else
                                                errorLog.AppendLine(" + Thiếu họ.");

                                            //tên
                                            if (!string.IsNullOrEmpty(TenText))
                                                ungVien.Ten = HamDungChung.VietHoaChuDau(new string[] { TenText });
                                            else
                                                errorLog.AppendLine(" + Thiếu tên.");

                                            //giới tính
                                            if (!string.IsNullOrEmpty(gioiTinhText) &&
                                                gioiTinhText.ToLower() == "nam")
                                                ungVien.GioiTinh = GioiTinhEnum.Nam;
                                            else
                                                ungVien.GioiTinh = GioiTinhEnum.Nu;

                                            //chiều cao
                                            //if (!string.IsNullOrEmpty(chieuCaoText))
                                                //ungVien.ChieuCao = Convert.ToInt32(chieuCaoText);
                                            //else
                                               // errorLog.AppendLine(" + Thiếu chiều cao.");

                                            //ngày sinh
                                            if (!string.IsNullOrEmpty(ngaySinhText))
                                            {
                                                try
                                                {
                                                    DateTime NgaySinh = Convert.ToDateTime(ngaySinhText);
                                                    if (NgaySinh != null && NgaySinh != DateTime.MinValue)
                                                        ungVien.NgaySinh = Convert.ToDateTime(ngaySinhText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngaySinhText);
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu thông tin ngày sinh");
                                            }  

                                            //nơi sinh
                                            if (!string.IsNullOrEmpty(noiSinhText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = noiSinhText;
                                                ungVien.NoiSinh = diaChi;
                                            }

                                            //CMND
                                            if (!string.IsNullOrEmpty(CMNDText))
                                                ungVien.CMND_UngVien = CMNDText;
                                            else
                                                errorLog.AppendLine(" + Thiếu cmnd.");

                                            //ngày cấp                                
                                            if (!string.IsNullOrEmpty(ngayCapText))
                                            {
                                                try
                                                {
                                                    DateTime NgayCap = Convert.ToDateTime(ngayCapText);
                                                    if (NgayCap != null && NgayCap != DateTime.MinValue)
                                                        ungVien.NgayCap = Convert.ToDateTime(ngayCapText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày cấp không hợp lệ: " + ngayCapText);
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu thông tin ngày cấp");
                                            }


                                            //nơi cấp
                                            if (!string.IsNullOrEmpty(noiCapText))
                                            {
                                                TinhThanh tinhThanh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh Like ?", noiCapText));
                                                if (tinhThanh == null)
                                                {
                                                    tinhThanh = new TinhThanh(uow);
                                                    tinhThanh.TenTinhThanh = noiCapText;
                                                    tinhThanh.MaQuanLy = HamDungChung.TaoChuVietTat(noiCapText);
                                                }
                                                ungVien.NoiCap = tinhThanh;
                                            }

                                            //quê quán
                                            if (!string.IsNullOrEmpty(queQuanText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = queQuanText;
                                                ungVien.QueQuan = diaChi;
                                            }

                                            //địa chỉ thường trú
                                            if (!string.IsNullOrEmpty(diaChiThuongTruText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = diaChiThuongTruText;
                                                ungVien.DiaChiThuongTru = diaChi;
                                            }

                                            //nơi ở hiện nay
                                            if (!string.IsNullOrEmpty(noiOHienNayText))
                                            {
                                                diaChi = new DiaChi(uow);
                                                diaChi.SoNha = noiOHienNayText;
                                                ungVien.NoiOHienNay = diaChi;
                                            }

                                            //Email
                                            if (!string.IsNullOrEmpty(emailText))
                                                ungVien.Email = emailText;

                                            //điện thoại di động
                                            if (!string.IsNullOrEmpty(dienThoaiDiDongText))
                                                ungVien.DienThoaiDiDong = dienThoaiDiDongText;

                                            //tình trạng hôn nhân
                                            if (!string.IsNullOrEmpty(tinhTrangHonNhanText))
                                            {
                                                TinhTrangHonNhan TinhTrangHonNhan = uow.FindObject<TinhTrangHonNhan>(CriteriaOperator.Parse("TenTinhTrangHonNhan Like ?", tinhTrangHonNhanText));
                                                if (TinhTrangHonNhan == null)
                                                {
                                                    TinhTrangHonNhan = new TinhTrangHonNhan(uow);
                                                    TinhTrangHonNhan.TenTinhTrangHonNhan = tinhTrangHonNhanText;
                                                    TinhTrangHonNhan.MaQuanLy = HamDungChung.TaoChuVietTat(tinhTrangHonNhanText);
                                                }
                                                ungVien.TinhTrangHonNhan = TinhTrangHonNhan;
                                            }

                                            //dân tộc
                                            if (!string.IsNullOrEmpty(danTocText))
                                            {
                                                DanToc DanToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc Like ?", danTocText));
                                                if (DanToc == null)
                                                {
                                                    DanToc = new DanToc(uow);
                                                    DanToc.TenDanToc = danTocText;
                                                    DanToc.MaQuanLy = HamDungChung.TaoChuVietTat(danTocText);
                                                }
                                                ungVien.DanToc = DanToc;
                                            }

                                            //tôn giáo
                                            if (!string.IsNullOrEmpty(tonGiaoText))
                                            {
                                                TonGiao TonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao Like ?", tonGiaoText));
                                                if (TonGiao == null)
                                                {
                                                    TonGiao = new TonGiao(uow);
                                                    TonGiao.TenTonGiao = tonGiaoText;
                                                    TonGiao.MaQuanLy = HamDungChung.TaoChuVietTat(tonGiaoText);
                                                }
                                                ungVien.TonGiao = TonGiao;
                                            }

                                            //quốc tịch
                                            if (!string.IsNullOrEmpty(quocTichText))
                                            {
                                                QuocGia QuocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia Like ?", quocTichText));
                                                if (quocTich == null)
                                                {
                                                    QuocTich = new QuocGia(uow);
                                                    QuocTich.TenQuocGia = quocTichText;
                                                    QuocTich.MaQuanLy = HamDungChung.TaoChuVietTat(quocTichText);
                                                }
                                                ungVien.QuocTich = QuocTich;
                                            }


                                            //hình thức tuyển dụng
                                            if (!string.IsNullOrEmpty(hinhThucTuyenDungText)
                                                && hinhThucTuyenDungText.ToLower() == "thi tuyển")
                                                ungVien.HinhThucTuyenDung = HinhThucTuyenDungEnum.ThiTuyen;
                                            else
                                                ungVien.HinhThucTuyenDung = HinhThucTuyenDungEnum.XetTuyen;

                                            //ngày dự tuyển
                                            if (!string.IsNullOrEmpty(ngayDuTuyenText))
                                            {
                                                try
                                                {
                                                    DateTime NgayDuTuyen = Convert.ToDateTime(ngayDuTuyenText);
                                                    if (NgayDuTuyen != null && NgayDuTuyen != DateTime.MinValue)
                                                        ungVien.NgayDuTuyen = Convert.ToDateTime(ngayDuTuyenText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày dự tuyển không hợp lệ: " + ngayDuTuyenText);
                                                }
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Thiếu thông tin ngày dự tuyển");
                                            }


                                            //Nghề nghiệp hiện tại
                                            if (!string.IsNullOrEmpty(ngheNghiepHienTaiText))                                              
                                                ungVien.KinhNghiem = ngheNghiepHienTaiText;
                                                                                  

                                            HoSo.VanBang vanBang = new HoSo.VanBang(uow);
                                            //trình độ chuyên môn cử nhân
                                            if (!string.IsNullOrEmpty(noiDaoTaoCNText) || !string.IsNullOrEmpty(namTotNghiepText) || !string.IsNullOrEmpty(xepLoaiText))
                                            {
                                                TrinhDoChuyenMon TrinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("MaQuanLy = ?", "CN"));
                                                if (TrinhDoChuyenMon == null)
                                                {
                                                    TrinhDoChuyenMon = new TrinhDoChuyenMon(uow);
                                                    TrinhDoChuyenMon.TenTrinhDoChuyenMon = "Cử nhân";
                                                    TrinhDoChuyenMon.MaQuanLy = "CN";                                                 
                                                }
                                                vanBang.TrinhDoChuyenMon = TrinhDoChuyenMon;                                             
                                                ungVien.TrinhDoChuyenMon = TrinhDoChuyenMon;                 

                                                //Nơi đào tạo
                                                if (!string.IsNullOrEmpty(noiDaoTaoCNText))
                                                {
                                                    TruongDaoTao TruongDaoTao = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao Like ?", noiDaoTaoSauDHText));
                                                    if (TruongDaoTao == null)
                                                    {
                                                        TruongDaoTao = new TruongDaoTao(uow);
                                                        TruongDaoTao.TenTruongDaoTao = noiDaoTaoSauDHText;
                                                        //TruongDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(noiDaoTaoSauDHText);
                                                    }
                                                    vanBang.TruongDaoTao = TruongDaoTao;
                                                    ungVien.TruongDaoTao = TruongDaoTao;                                                    
                                                }                                              

                                                //năm tốt nghiệp
                                                if (!string.IsNullOrEmpty(namTotNghiepText))
                                                {
                                                    int NamTotNghiep = Convert.ToInt32(namTotNghiepText);
                                                    ungVien.NamTotNghiep = NamTotNghiep;
                                                    vanBang.NamTotNghiep = NamTotNghiep;
                                                }
                                                 
                                                //xếp loại
                                                if (!string.IsNullOrEmpty(xepLoaiText))
                                                {
                                                    switch (xepLoaiText.ToLower())
                                                    {
                                                        case "xuất sắc":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.XuatSac;
                                                            break;
                                                        case "giỏi":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.Gioi;
                                                            break;
                                                        case "khá":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.Kha;
                                                            break;
                                                        case "trung bình":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.TrungBinh;
                                                            break;
                                                        case "trung bình khá":
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.TrungBinhKha;
                                                            break;
                                                        default:
                                                            vanBang.XepLoai = XepLoaiChungChiEnum.KhongChon;
                                                            break;
                                                    }                                                 
                                                    ungVien.ListVanBang.Add(vanBang);
                                                }
                                            }
                                           
                                            //trình độ chuyên sau đại học
                                            if (!string.IsNullOrEmpty(bacDaoTaoText) || !string.IsNullOrEmpty(chuongTrinhDaoTaoSauDHText))
                                            {
                                                if (chuongTrinhDaoTaoSauDHText == "Hoàn thành")
                                                {
                                                    TrinhDoChuyenMon TrinhDoChuyenMon = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("MaQuanLy = ?", bacDaoTaoText));
                                                    if (TrinhDoChuyenMon != null)
                                                    {
                                                        vanBang.TrinhDoChuyenMon = TrinhDoChuyenMon;
                                                        ungVien.TrinhDoChuyenMon = TrinhDoChuyenMon;
                                                    }                                                  

                                                    //Nơi đào tạo
                                                    if (!string.IsNullOrEmpty(noiDaoTaoSauDHText))
                                                    {
                                                        TruongDaoTao TruongDaoTao = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao Like ?", noiDaoTaoSauDHText));
                                                        if (TruongDaoTao == null)
                                                        {
                                                            TruongDaoTao = new TruongDaoTao(uow);
                                                            TruongDaoTao.TenTruongDaoTao = noiDaoTaoSauDHText;
                                                            //TruongDaoTao.MaQuanLy = HamDungChung.TaoChuVietTat(noiDaoTaoSauDHText);
                                                        }
                                                        vanBang.TruongDaoTao = TruongDaoTao;
                                                        ungVien.TruongDaoTao = TruongDaoTao;
                                                    }
                                                    ungVien.ListVanBang.Add(vanBang);
                                                }
                                                else
                                                {
                                                    //Chương trình học
                                                    if (!string.IsNullOrEmpty(chuongTrinhDaoTaoSauDHText))
                                                    {
                                                        ChuongTrinhHoc ChuongTrinhHoc = uow.FindObject<ChuongTrinhHoc>(CriteriaOperator.Parse("TenChuongTrinhHoc Like ?", chuongTrinhDaoTaoSauDHText));
                                                        if (ChuongTrinhHoc == null)
                                                        {
                                                            ChuongTrinhHoc = new ChuongTrinhHoc(uow);
                                                            ChuongTrinhHoc.TenChuongTrinhHoc = chuongTrinhDaoTaoSauDHText;
                                                            ChuongTrinhHoc.MaQuanLy = HamDungChung.TaoChuVietTat(chuongTrinhDaoTaoSauDHText);
                                                        }
                                                        ungVien.ChuongTrinhHoc = ChuongTrinhHoc;                                                       
                                                    }
                                                }
                                            }                                         
                                         
                                            //trình độ tin học
                                            if (!string.IsNullOrEmpty(trinhDoTinHocText))
                                            {
                                                TrinhDoTinHoc TrinhDoTinHoc = uow.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("MaQuanLy Like ?", trinhDoTinHocText));
                                                if (TrinhDoTinHoc == null)
                                                {
                                                    TrinhDoTinHoc = new TrinhDoTinHoc(uow);
                                                    TrinhDoTinHoc.TenTrinhDoTinHoc = trinhDoTinHocText;
                                                    TrinhDoTinHoc.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoTinHocText);
                                                }
                                                ungVien.TrinhDoTinHoc = TrinhDoTinHoc;
                                            }

                                            //ngoại ngữ chính
                                            if (!string.IsNullOrEmpty(ngoaiNguChinhText))
                                            {
                                                NgoaiNgu NgoaiNguChinh = uow.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu Like ?", ngoaiNguChinhText));
                                                if (NgoaiNguChinh == null)
                                                {
                                                    NgoaiNguChinh = new NgoaiNgu(uow);
                                                    NgoaiNguChinh.TenNgoaiNgu = ngoaiNguChinhText;
                                                    NgoaiNguChinh.MaQuanLy = HamDungChung.TaoChuVietTat(ngoaiNguChinhText);
                                                }
                                                ungVien.NgoaiNgu = NgoaiNguChinh;
                                            }
                                            else
                                            {
                                                NgoaiNgu NgoaiNguChinh = uow.FindObject<NgoaiNgu>(CriteriaOperator.Parse("TenNgoaiNgu Like ?", "Tiếng Anh"));
                                                if (NgoaiNguChinh == null)
                                                {
                                                    NgoaiNguChinh = new NgoaiNgu(uow);
                                                    NgoaiNguChinh.TenNgoaiNgu = ngoaiNguChinhText;
                                                    NgoaiNguChinh.MaQuanLy = HamDungChung.TaoChuVietTat(ngoaiNguChinhText);
                                                }
                                                ungVien.NgoaiNgu = NgoaiNguChinh;
                                            }


                                            //trình độ ngoại ngữ chính
                                            if (!string.IsNullOrEmpty(trinhDoNgoaiNguText))
                                            {
                                                TrinhDoNgoaiNgu TrinhDoNgoaiNgu = uow.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("MaQuanLy Like ?", trinhDoNgoaiNguText));
                                                if (TrinhDoNgoaiNgu == null)
                                                {
                                                    TrinhDoNgoaiNgu = new TrinhDoNgoaiNgu(uow);
                                                    TrinhDoNgoaiNgu.TenTrinhDoNgoaiNgu = trinhDoNgoaiNguText;
                                                    TrinhDoNgoaiNgu.MaQuanLy = HamDungChung.TaoChuVietTat(trinhDoNgoaiNguText);
                                                }
                                                ungVien.TrinhDoNgoaiNgu = TrinhDoNgoaiNgu;
                                            }

                                            //chứng chỉ khác
                                            if (!string.IsNullOrEmpty(chungChiText))
                                            {
                                                LoaiChungChi loaiChungChi = uow.FindObject<LoaiChungChi>(CriteriaOperator.Parse("TenChungChi = ?", "Chứng chỉ"));
                                                if (loaiChungChi == null)
                                                {
                                                    loaiChungChi = new LoaiChungChi(uow);
                                                    loaiChungChi.TenChungChi = "Chứng chỉ";
                                                    loaiChungChi.MaQuanLy = "CC";
                                                }

                                                HoSo.ChungChi ChungChi = uow.FindObject<HoSo.ChungChi>(CriteriaOperator.Parse("LoaiChungChi = ? and TenChungChi Like ?", "Chứng chỉ", chungChiText));
                                                if (ChungChi == null)
                                                {
                                                    ChungChi = new HoSo.ChungChi(uow);
                                                    ChungChi.LoaiChungChi = loaiChungChi;
                                                    ChungChi.TenChungChi = chungChiText;
                                                }
                                                ungVien.ListChungChi.Add(ChungChi);
                                            }
                                            #endregion
                                        }

                                        #region Ghi File log
                                        {
                                            //Đưa thông tin bị lỗi vào blog
                                            if (errorLog.Length > 0)
                                            {
                                                mainLog.AppendLine("- STT: " + soThuTuText + " - Họ Tên:" + HoText + " " + TenText);
                                                mainLog.AppendLine(errorLog.ToString());
                                                oke = false;
                                            }
                                        }
                                        #endregion

                                        if (oke == false)
                                        {
                                            Loi++;
                                        }
                                        else
                                        {
                                            ThanhCong++;
                                        }

                                    }//end foreach

                                }
                                if (mainLog.Length > 0)
                                {
                                    uow.RollbackTransaction();
                                    if (DialogUtil.ShowYesNo("Import Thành Công " + ThanhCong + " ứng viên - Số ứng viên Import không thành công " + Loi + ". Bạn có muốn xuất thông tin ứng viên lỗi") == DialogResult.Yes)
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
                                    //transaction.Complete();
                                    DialogUtil.ShowSaveSuccessful("Import Thành Công tất cả ứng viên !");
                                }
                            }
                        }
                    }
                }
            }
        }
    }


}
