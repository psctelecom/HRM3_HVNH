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
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.Controllers
{
    //internal static class StringEx
    //{
    //    internal static String FullTrim(this String source)
    //    {
    //        return source.Trim().Replace("  ", " ");
    //    }

    //    internal static String RemoveEmpty(this String source)
    //    {
    //        return source.Trim().Replace(" ", "");
    //    }
    //}

    class HoSo_ImportHoSo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="obs"></param>
        /// <param name="filename"></param>
        /// <param name="laoiDo"> 
        /// true: đổ tất cả dữ liệu trong file phải đúng chắc chắn vô hểt
        /// false: đổ từng dòng kiểm dữ liệu dòng nào ok là đỏ vào 
        /// </param>
        /// <returns></returns>
        public static bool Import(HoSo_Import obj, IObjectSpace obs, string filename, bool doTatCa)
        {
            bool oke = true;
            int soNhanVienImportLoi = 0;
            int soNhanVienImportThanhCong = 0;
            using (DataTable dt = DataProvider.GetDataTable(filename, "[ThongTinNhanVien$A5:DR]"))//hồng bàng //các trường khác DO // ThongTinNhanVien$A4:DR
            {
                #region tblHoSO
                const int sttIdx = 0;
                const int maQuanLyIdx = 1;
                const int hoIdx = 2;
                const int tenIdx = 3;
                const int tenGoiKhacIdx = 4;
                const int ngaySinhIdx = 5;
                const int noiSinhIdx = 6;
                const int gioiTinhIdx = 7;
                const int soCmndIdx = 8;
                const int ngayCapCmndIdx = 9;
                const int noiCapCmndIdx = 10;
                const int soHoChieuIdx = 11;
                const int ngayCapHoChieuIdx = 12;
                const int noiCapHoChieuIdx = 13;
                const int ngayHetHanIdx = 14;
                const int queQuanIdx = 15;
                const int diaChiThuongTruIdx = 16;
                const int noiOHienNayIdx = 17;
                const int emailIdx = 18;
                const int DTDDIdx = 19;
                const int dtNhaRiengIdx = 20;
                const int tinhTrangHonNhanIdx = 21;
                const int danTocIdx = 22;
                const int tonGiaoIdx = 23;
                const int quocTichIdx = 24;
                const int hinhThucTuyenDungIdx = 25;
                #endregion
                //end 25

                #region tblNhanVien
                const int chucDanhIdx = 26;
                const int boPhanIdx = 27; //khóa ngoại đến bảng bộ phận                 
                const int boPhanSuDungIdx = 28; // khóa ngoại đến bảng bộ phận                  
                const int thanhPhanXuatThanIdx = 29;
                const int uuTienGiaDinhIdx = 30;
                const int uuTienBanThanIdx = 31;
                const int congViecHienNayIdx = 32; // khóa ngoại đến bảng công việc                                           
                const int ngayVaoNganhGiaoDucIdx = 33;
                const int ngayTuyenDungIdx = 34;
                const int donViTuyenDungIdx = 35;
                const int congViecTuyenDungIdx = 36;
                const int congViecDuocGiaoIdx = 37; // khóa ngoại đến bảng công việc                  
                const int ngayVaoCoQuanIdx = 38; // ngày vào làm
                const int hopDongHienTaiIdx = 39;
                const int tinhTrangIdx = 40;  //khóa ngoại đến bảng tình trạng                  
                #endregion
                //end 40

                #region tblThongTinNhanVien
                const int ngayNghiHuuIdx = 41;
                const int chucVuIdx = 42;
                const int lanBoNhiemChucVuIdx = 43;
                const int chucVuKiemNhiemIdx = 44;
                const int ngayBoNhiemKiemNhiemIdx = 45;
                const int loaiLuongChinhIdx = 46;
                const int loaiNhanVienIdx = 47;
                const int loaiNhanSuIdx = 48;
                const int soHieuCongChucIdx = 49;
                const int ngayVaoBienCheIdx = 50;
                const int nhomMauIdx = 51;
                const int chieuCaoIdx = 52;
                const int canNangIdx = 53;
                const int tinhTrangSucKhoeIdx = 54;
                const int ngayTinhThamNienNhaGiaoIdx = 55;
                #endregion
                //end55

                #region thông tin lương
                const int ngachLuongIdx = 56;
                const int tenNgachIdx = 57;
                const int ngayBoNhiemNgachIdx = 58;
                const int ngayHuongLuongIdx = 59;
                const int bacLuongIdx = 60;
                const int heSoLuongIdx = 61;
                const int heSoPhuCapChucVuIdx = 62;
                const int heSoPhuCapUuDaiIdx = 63;
                const int heSoPhuCapKhuVucIdx = 64;
                const int heSoPhuCapTrachNhiemIdx = 65;
                const int heSoPhuCapThuNhapTangIdx = 66;
                const int heSoVuotKhungIdx = 67;
                const int heSoPhuCapThamNienIdx = 68;
                const int mocTinhLuongLanSauIdx = 69;
                const int huong85LuongIdx = 70;
                const int maSoThueIdx = 71;
                #endregion
                //end 71

                #region Bảo hiểm xã hội
                const int soBHXHIdx = 72;
                const int ngayThamGiaBHXHIdx = 73;
                const int soTheBHYTIdx = 74;
                const int tuNgayIdx = 75;
                const int denNgayIdx = 76;
                const int noiDangKyKCBIdx = 77;
                #endregion
                //end 77

                #region trình độ học tập nhân viên

                const int trinhDoVanHoaIdx = 78;

                #region trình độ Trung Cấp
                const int chuyenMonDaoTaoTCIdx = 79; //ngành học
                const int truongDaoTaoTCIdx = 80;
                const int hinhThucDaoTaoTCIdx = 81;
                const int ngayCongNhanTCIdx = 82;
                #endregion
                //end 82

                #region trình độ Cao Đẳng
                const int chuyenMonDaoTaoCDIdx = 83; //ngành học
                const int truongDaoTaoCDIdx = 84;
                const int hinhThucDaoTaoCDIdx = 85;
                const int ngayCongNhanCDIdx = 86;
                #endregion
                //end 86

                #region trình độ Đại Học
                const int chuyenMonDaoTaoDHIdx = 87; //ngành học
                const int truongDaoTaoDHIdx = 88;
                const int hinhThucDaoTaoDHIdx = 89;
                const int ngayCongNhanDHIdx = 90;
                #endregion
                //end 90

                #region trình độ Thạc Sỹ
                const int chuyenMonDaoTaoThSIdx = 91; //ngành học
                const int truongDaoTaoThSIdx = 92;
                const int ngayCongNhanThSIdx = 93;
                #endregion
                //end 82

                #region trình độ Tiến Sỹ
                const int chuyenMonDaoTaoTSIdx = 94; //ngành học
                const int truongDaoTaoTSIdx = 95;
                const int ngayCongNhanTSIdx = 96;
                #endregion
                //end 96

                const int trinhDoCaoNhatHienTaiIdx = 97; //không cần thêm vì chính là các trình độ đã thêm
                const int trinhDoTinHocIdx = 98;
                const int trinhDoNgoaiNguIdx = 99;
                const int capDoNgoaiNguIdx = 100;

                //học hàm
                const int hocHamIdx = 101;
                const int namCongNhanHocHamIdx = 102;

                //danh hiệu
                const int danhHieuIdx = 103;
                const int ngayPhongTangDanhHieuIdx = 104;

                #endregion
                //end 104

                #region trình độ chính trị
                const int trinhDoChinhTriIdx = 105;
                const int trinhDoQuanLyNhaNuocIdx = 106;
                #endregion
                //end 106

                #region đoàn
                const int ngayKetNapDoanTNIdx = 107;
                const int chucVuDoanTNIdx = 108;
                #endregion
                //end 108

                #region Công Đoàn
                const int ngayVaoCongDoanIdx = 109;
                const int chucVuCongDoanIdx = 110;
                const int ngayBoNhiemChucVuCongDoanIdx = 111;
                #endregion
                //end 111

                #region Đảng
                const int ngayVaoDangIdx = 112;
                const int ngayVaoDangChinhThucIdx = 113;
                const int chucVuDangIdx = 114;
                #endregion
                //end 114

                #region tài khoản trả lương
                const int nganHangTK1Idx = 115;
                const int soTaiKhoanTK1Idx = 116;
                const int nganHangTK2Idx = 117;
                const int soTaiKhoanTK2Idx = 118;
                #endregion
                //end 118

                #region bổ sung thêm
                const int soNguoiPhuThuocIdx = 119;
                const int heSoPhuCapPhucVuDaoTaoIdx = 120;
                #endregion
                //end 121

                using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    ThongTinNhanVien nhanVien = null;
                    QuocGia quocTich = null;
                    var mainLog = new StringBuilder();
                    //int nullRowCount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        var errorLog = new StringBuilder();
                        //
                        String sttText = dr[sttIdx].ToString();
                        String hoNhanVien = dr[hoIdx].ToString().FullTrim();
                        String tenNhanVien = dr[tenIdx].ToString().FullTrim();
                        String maQuanLy = dr[maQuanLyIdx].ToString().FullTrim();
                        String cmnd = dr[soCmndIdx].ToString().FullTrim();
                        //StringBuilder detailLog=null;
                        #region kiểm tra xem nhân viên đó đã có chưa
                        nhanVien = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy = ?", maQuanLy));
                        if (nhanVien != null)
                        {
                            //mainLog.AppendLine("- STT: " + sttText);
                            //mainLog.AppendLine(string.Format("- CMND :{0}, hovaten {1} {2} đã tồn tại trong hệ thống", cmnd, hoNhanVien, tenNhanVien));

                            //Thông báo lỗi
                            //oke = false;
                        }
                        #endregion

                       
                        else
                        {
                            #region tiến hành kiểm tra dữ liệu để import Nhân Viên

                            nhanVien = new ThongTinNhanVien(uow);

                            #region Hồ sơ cột 0 --> cột 25

                            #region Mã nhân viên cột 1
                            {
                                if (!string.IsNullOrEmpty(maQuanLy))
                                {
                                    if (TruongConfig.MaTruong.Equals("NEU"))
                                        nhanVien.SoHieuCongChuc = maQuanLy;
                                    else
                                        nhanVien.MaQuanLy = maQuanLy;

                                }
                                //tạo mã ngẫu nhiên nếu không có mã
                                else
                                {
                                    //if (string.IsNullOrWhiteSpace(maQuanLy))
                                    //    nhanVien.MaQuanLy = Guid.NewGuid().ToString();
                                    errorLog.AppendLine(" + Thiếu mã quản lý nhân viên.");
                                }
                            }
                            #endregion

                            #region Họ cột 2
                            {
                                if (!string.IsNullOrEmpty(hoNhanVien))
                                {
                                    nhanVien.Ho = hoNhanVien;
                                }
                                else
                                {
                                    errorLog.AppendLine(" + Thiếu thông tin họ và tên đệm.");
                                }
                            }
                            #endregion

                            #region Tên cột 3
                            {
                                if (!string.IsNullOrEmpty(tenNhanVien))
                                {
                                    nhanVien.Ten = tenNhanVien;
                                }
                                else
                                {
                                    errorLog.AppendLine(" + Thiếu thông tin tên nhân viên.");
                                    oke = false;
                                }
                            }
                            #endregion

                            #region Bí danh cột 4
                            {
                                String biDanh = dr[tenGoiKhacIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(biDanh))
                                {
                                    nhanVien.TenGoiKhac = biDanh;
                                }
                            }
                            #endregion

                            #region Ngày sinh cột 5
                            {
                                String ngayString = dr[ngaySinhIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NgaySinh = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày sinh không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Thiếu thông tin ngày sinh hoặc không đúng định dạng dd/MM/yyyy.");

                                    }
                                }
                                else
                                {
                                    //errorLog.AppendLine( " + Thiếu thông tin ngày sinh.");

                                }
                            }
                            #endregion

                            #region Nơi sinh cột 6
                            {
                                String noiSinh = dr[noiSinhIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(noiSinh))
                                {
                                    //Nơi sinh
                                    DiaChi diaChi = new DiaChi(uow);
                                    diaChi.SoNha = noiSinh;
                                    nhanVien.NoiSinh = diaChi;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin nơi sinh.");                                 
                                }
                            }
                            #endregion

                            #region Giới tính cột 7
                            {
                                String gioiTinh = dr[gioiTinhIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(gioiTinh))
                                {
                                    if (gioiTinh.ToLower() == "nam")
                                        nhanVien.GioiTinh = GioiTinhEnum.Nam;
                                    else if (gioiTinh.ToLower() == "nữ" || gioiTinh.ToLower() == "nu")
                                        nhanVien.GioiTinh = GioiTinhEnum.Nu;
                                    else
                                    {
                                        errorLog.AppendLine(" + Giới tính không hợp lệ: " + gioiTinh);
                                        oke = false;
                                    }
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin giới tính.");
                                    //oke = false;
                                }
                            }
                            #endregion

                            #region Số chứng minh nhân dân cột 8
                            {
                                if (!string.IsNullOrEmpty(cmnd))
                                {
                                    nhanVien.CMND = cmnd;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Số CMND chưa nhập.");
                                }
                            }
                            #endregion

                            #region Ngày cấp CMND cột 9
                            {
                                string ngayString = dr[ngayCapCmndIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NgayCap = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                        errorLog.AppendLine(" + Ngày cấp CMND chưa nhập hoặc không đúng định dạng dd/MM/yyyy.");
                                }
                            }
                            #endregion
                         

                            #region Hộ Chiếu cột 11
                            {
                                String hoChieu = dr[soHoChieuIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(hoChieu))
                                {
                                    nhanVien.SoHoChieu = hoChieu;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Số Hộ Chiếu chưa nhập.");
                                }
                            }
                            #endregion

                            #region Ngày cấp hộ chiếu cột 12
                            {
                                string ngayString = dr[ngayCapHoChieuIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NgayCap = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày cấp CMND không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                        errorLog.AppendLine(" + Ngày cấp CMND chưa nhập hoặc không đúng định dạng dd/MM/yyyy.");
                                }
                            }
                            #endregion

                            #region Nơi cấp hộ chiếu cột 13
                            {
                                String noiCaphoChieu = dr[noiCapHoChieuIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(noiCaphoChieu))
                                {//                                
                                    nhanVien.NoiCapHoChieu = noiCaphoChieu;

                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Nơi cấp Hộ Chiếu chưa nhập.");
                                }
                            }
                            #endregion

                            #region Ngày hết hạn hộ chiếu cột 14
                            {
                                string ngayString = dr[ngayHetHanIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NgayHetHan = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày hết hạn hộ chiếu không hợp lệ: " +
                                                                ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                        errorLog.AppendLine(
                                            " + Ngày cấp hết hạn chưa nhập hoặc không đúng định dạng dd/MM/yyyy.");
                                }
                            }
                            #endregion

                            #region Quê quán cột 15
                            {
                                String quenQuan = dr[queQuanIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(quenQuan))
                                {
                                    //Quê quán
                                    DiaChi diaChi = new DiaChi(uow);
                                    diaChi.SoNha = quenQuan;
                                    nhanVien.QueQuan = diaChi;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin quê quán.");
                                    //oke = false;
                                }
                            }
                            #endregion

                            #region Địa chỉ thường trú cột 16
                            {
                                String diaChiThuongTru = dr[diaChiThuongTruIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(diaChiThuongTru))
                                {
                                    //Địa chỉ thường trú
                                    DiaChi diaChi = new DiaChi(uow);
                                    diaChi.SoNha = diaChiThuongTru;
                                    nhanVien.DiaChiThuongTru = diaChi;
                                }
                            }
                            #endregion

                            #region Nơi ở hiện nay cột 17
                            {
                                String noiOHienNay = dr[noiOHienNayIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(noiOHienNay))
                                {
                                    //Nơi ở hiện nay
                                    DiaChi diaChi = new DiaChi(uow);
                                    diaChi.SoNha = noiOHienNay;
                                    nhanVien.NoiOHienNay = diaChi;
                                }
                            }
                            #endregion

                            #region Email cột 18
                            {
                                String email = dr[emailIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(email))
                                {
                                    nhanVien.Email = email;
                                }
                            }
                            #endregion

                            #region Điện thoại di động cột 19
                            {
                                String dienThoaiDiDongText = dr[DTDDIdx].ToString();
                                if (!string.IsNullOrEmpty(dienThoaiDiDongText))
                                {
                                    nhanVien.DienThoaiDiDong = dienThoaiDiDongText;
                                }
                            }
                            #endregion

                            #region Điện thoại nhà riêng cột 20
                            {
                                String dienThoaiNhaRiengText = dr[dtNhaRiengIdx].ToString();

                                if (!string.IsNullOrEmpty(dienThoaiNhaRiengText))
                                {
                                    nhanVien.DienThoaiNhaRieng = dienThoaiNhaRiengText;
                                }
                            }
                            #endregion

                            #region Tình trạng hôn nhân cột 21
                            {
                                String tinhTrangHonNhan = dr[tinhTrangHonNhanIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(tinhTrangHonNhan))
                                {
                                    TinhTrangHonNhan honNhan = uow.FindObject<TinhTrangHonNhan>(CriteriaOperator.Parse("TenTinhTrangHonNhan like ?", tinhTrangHonNhan));
                                    if (honNhan == null)
                                    {
                                        honNhan = new TinhTrangHonNhan(uow);
                                        honNhan.TenTinhTrangHonNhan = tinhTrangHonNhan;
                                        honNhan.MaQuanLy = Guid.NewGuid().ToString();
                                        honNhan.Save();
                                    }
                                    nhanVien.TinhTrangHonNhan = honNhan;

                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu tình trạng hôn nhân.");
                                }
                            }
                            #endregion

                            #region Dân tộc cột 22
                            {
                                String danTocText = dr[danTocIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(danTocText))
                                {
                                    DanToc danToc = null;
                                    danToc = uow.FindObject<DanToc>(CriteriaOperator.Parse("TenDanToc like ?", danTocText));
                                    if (danToc == null)
                                    {
                                        //tạo mới dân tộc
                                        danToc = new DanToc(uow);
                                        danToc.TenDanToc = danTocText;
                                        danToc.MaQuanLy = Guid.NewGuid().ToString();
                                        danToc.Save();
                                        //uow.CommitChanges();
                                    }
                                    nhanVien.DanToc = danToc;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin dân tộc.");
                                }
                            }
                            #endregion

                            #region Tôn giáo cột 23
                            {
                                String tonGiaoText = dr[tonGiaoIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(tonGiaoText))
                                {
                                    TonGiao tonGiao = null;
                                    tonGiao = uow.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", tonGiaoText));
                                    if (tonGiao == null)
                                    {
                                        //tạo mới tôn giáo
                                        tonGiao = new TonGiao(uow);
                                        tonGiao.TenTonGiao = tonGiaoText;
                                        tonGiao.MaQuanLy = Guid.NewGuid().ToString();
                                        tonGiao.Save();
                                        //uow.Save(tonGiao);
                                    }
                                    nhanVien.TonGiao = tonGiao;

                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin tôn giáo.");
                                }
                            }
                            #endregion

                            #region Quốc tịch cột 24
                            {
                                String text = dr[quocTichIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(text))
                                {
                                    quocTich = uow.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", text));
                                    if (quocTich == null)
                                    {
                                        quocTich = new QuocGia(uow);
                                        quocTich.TenQuocGia = text;
                                        quocTich.MaQuanLy = Guid.NewGuid().ToString();
                                        quocTich.Save();
                                    }
                                    nhanVien.QuocTich = quocTich;

                                }
                                else
                                {
                                    errorLog.AppendLine(" + Thiếu quốc tịch.");
                                }
                            }
                            #endregion

                            #region Nơi cấp CMND cột 10
                            {
                                var text = dr[noiCapCmndIdx].ToString()
                                        .Replace("C.A.", "")
                                        .Replace("C.A", "")
                                        .Replace("CA.", "")
                                        .Replace("ca.", "")
                                        .Replace("ca ", "")
                                        .Replace("CA ", "")
                                        .Replace("Công an tỉnh", "")
                                        .Replace("công an tỉnh", "")
                                        .Replace("Công an", "")
                                        .Replace("công an", "")
                                        .Replace("Tỉnh", "")
                                        .Replace("tỉnh", "")
                                        .FullTrim();
                                if (!string.IsNullOrEmpty(text))
                                {//

                                    var tinh = uow.FindObject<TinhThanh>(CriteriaOperator.Parse("TenTinhThanh like ?", text));
                                    if (tinh == null)
                                    {
                                        tinh = new TinhThanh(uow);
                                        tinh.TenTinhThanh = text;
                                        tinh.MaQuanLy = Guid.NewGuid().ToString();
                                        tinh.QuocGia = quocTich;
                                        tinh.Save();
                                    }

                                    nhanVien.NoiCap = tinh;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Nơi cấp CMND chưa nhập.");
                                }
                            }
                            #endregion

                            //hình thức tuyển dụng
                            #endregion

                            #region Nhân viên cột 26--> 40
                            #region Chức Danh cột 26
                            {
                                String chucDanhText = dr[chucDanhIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(chucDanhText))
                                {
                                    ChucDanh chucDanh = null;
                                    chucDanh = uow.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh like ?", chucDanhText));
                                    if (chucDanh == null)
                                    {
                                        //tạo mới chức danh
                                        chucDanh = new ChucDanh(uow);
                                        chucDanh.TenChucDanh = chucDanhText;
                                        chucDanh.MaQuanLy = chucDanhText;
                                        //uow.CommitChanges();
                                    }
                                    nhanVien.ChucDanh = chucDanh;
                                }
                            }
                            #endregion

                            #region Bộ phận cột 27
                            {
                                if (obj.TatCa == false)
                                {
                                    nhanVien.BoPhan = uow.GetObjectByKey<BoPhan>(obj.BoPhan.Oid);
                                }
                                else
                                {
                                    String tenBoPhan = dr[boPhanIdx].ToString().FullTrim();
                                    if (!string.IsNullOrEmpty(tenBoPhan))
                                    {
                                        BoPhan boPhan = null;
                                        boPhan = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", tenBoPhan));
                                        if (boPhan == null)
                                        {
                                            boPhan = new BoPhan(uow);
                                            boPhan.TenBoPhan = tenBoPhan;
                                        }
                                        nhanVien.BoPhan = boPhan;
                                    }
                                    else
                                    {
                                        //errorLog.AppendLine(" + Bộ phận không được để trống: " + tenBoPhan);
                                    }
                                }
                            }
                            #endregion

                            #region Bộ phận sử dụng cột 28 - tạm thời chưa sử dụng
                            {
                                //String tenBoPhanSD = dr[boPhanSuDungIdx].ToString().FullTrim();
                                //if (!string.IsNullOrEmpty(tenBoPhanSD))
                                //{
                                //    BoPhan boPhanSD = null;
                                //    boPhanSD = uow.FindObject<BoPhan>(CriteriaOperator.Parse("TenBoPhan Like ?", tenBoPhanSD));
                                //    if (boPhanSD == null)
                                //    {
                                //        boPhanSD = new BoPhan(uow);
                                //        boPhanSD.TenBoPhan = tenBoPhanSD;
                                //    }
                                //    nhanVien.BoPhanSuDung = boPhanSD;                                    
                                //}
                            }
                            #endregion

                            #region Thành phần xuất thân cột 29
                            {
                                String thanhPhanXuatThanText = dr[thanhPhanXuatThanIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(thanhPhanXuatThanText))
                                {
                                    ThanhPhanXuatThan tpXuatThan = null;
                                    tpXuatThan = uow.FindObject<ThanhPhanXuatThan>(CriteriaOperator.Parse("TenThanhPhanXuatThan Like ?", thanhPhanXuatThanText));
                                    if (tpXuatThan == null)
                                    {
                                        tpXuatThan = new ThanhPhanXuatThan(uow);
                                        tpXuatThan.TenThanhPhanXuatThan = thanhPhanXuatThanText;
                                    }
                                    nhanVien.ThanhPhanXuatThan = tpXuatThan;
                                }
                            }
                            #endregion

                            #region Ưu tiên gia đình cột 30
                            {
                                String uuTienGiaDinhText = dr[uuTienGiaDinhIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(uuTienGiaDinhText))
                                {
                                    UuTienGiaDinh uuTienGD = null;
                                    uuTienGD = uow.FindObject<UuTienGiaDinh>(CriteriaOperator.Parse("TenUuTienGiaDinh like ?", uuTienGiaDinhText));
                                    if (uuTienGD == null)
                                    {
                                        uuTienGD = new UuTienGiaDinh(uow);
                                        uuTienGD.TenUuTienGiaDinh = uuTienGiaDinhText;

                                    }
                                    nhanVien.UuTienGiaDinh = uuTienGD;
                                }
                            }
                            #endregion

                            #region Ưu tiên bản thân cột 31
                            {
                                String uuTienBanThanText = dr[uuTienBanThanIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(uuTienBanThanText))
                                {
                                    UuTienBanThan uuTienBT = null;
                                    uuTienBT = uow.FindObject<UuTienBanThan>(CriteriaOperator.Parse("TenUuTienBanThan like ?", uuTienBanThanText));
                                    if (uuTienBT == null)
                                    {
                                        uuTienBT = new UuTienBanThan(uow);
                                        uuTienBT.TenUuTienBanThan = uuTienBanThanText;

                                    }
                                    nhanVien.UuTienBanThan = uuTienBT;
                                }
                            }
                            #endregion

                            #region Công việc hiện nay cột 32
                            {
                                String tenCongViecHN = dr[congViecHienNayIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(tenCongViecHN))
                                {
                                    CongViec congViec = null;
                                    congViec = uow.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec Like ?", tenCongViecHN));
                                    if (congViec == null)
                                    {
                                        congViec = new CongViec(uow);
                                        congViec.TenCongViec = tenCongViecHN;
                                        //congViec.MaQuanLy = congViecDuocGiao; //Guid.NewGuid().ToString();                                        
                                    }
                                    nhanVien.CongViecHienNay = congViec;
                                }

                            }
                            #endregion

                            #region Ngày vào ngành giáo dục cột 33
                            {
                                string ngayString = dr[ngayVaoNganhGiaoDucIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngayVaoNganhGD = null;
                                    try
                                    {
                                        ngayVaoNganhGD = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex) { }
                                    if (ngayVaoNganhGD != null)
                                    {
                                        nhanVien.NgayVaoNganhGiaoDuc = ngayVaoNganhGD.Value;
                                    }
                                }
                            }
                            #endregion

                            #region Ngày tuyển dụng lần đầu cột 34
                            {
                                string ngayString = dr[ngayTuyenDungIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex) { }

                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NgayTuyenDung = ngay.Value;
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày tuyển dụng lần đầu không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày tuyển dụng lần đầu không đúng định dạng dd/MM/yyyy.");
                                    }
                                }
                            }
                            #endregion

                            #region Cơ quan tuyển dụng cột 35
                            {
                                String coQuanTuyenDungText = dr[donViTuyenDungIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(coQuanTuyenDungText))
                                {
                                    nhanVien.DonViTuyenDung = coQuanTuyenDungText;
                                }
                            }
                            #endregion

                            #region Công việc tuyển dụng cột 36
                            {
                                String congViecTuyenDungText = dr[congViecTuyenDungIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(congViecTuyenDungText))
                                {
                                    nhanVien.CongViecTuyenDung = congViecTuyenDungText;
                                }
                            }
                            #endregion

                            #region Công việc được giao cột 37
                            {
                                String congViecDuocGiaoText = dr[congViecDuocGiaoIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(congViecDuocGiaoText))
                                {
                                    CongViec congViec = null;
                                    congViec = uow.FindObject<CongViec>(CriteriaOperator.Parse("TenCongViec Like ?", congViecDuocGiaoText));
                                    if (congViec == null)
                                    {
                                        congViec = new CongViec(uow);
                                        congViec.TenCongViec = congViecDuocGiaoText;
                                        //congViec.MaQuanLy = congViecDuocGiao; //Guid.NewGuid().ToString();                                        
                                    }
                                    nhanVien.CongViecHienNay = congViec;
                                }

                            }
                            #endregion

                            #region Ngày vào cơ quan (vào trường) cột 38
                            {
                                string ngayString = dr[ngayVaoCoQuanIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex) { }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                        {
                                            nhanVien.NgayVaoCoQuan = ngay.Value;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày vào cơ quan không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày vào cơ quan không đúng định dạng dd/MM/yyyy.");
                                    }
                                }
                            }

                            #endregion

                            //39 tạm thời chưa sử dụng

                            #region Tình trạng nhân viên cột 40
                            {
                                String tinhTrangTex = dr[tinhTrangIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(tinhTrangTex))
                                {
                                    TinhTrang tinhTrang = null;
                                    tinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", tinhTrangTex));
                                    if (tinhTrang == null)
                                    {
                                        tinhTrang = new TinhTrang(uow);
                                        tinhTrang.TenTinhTrang = tinhTrangTex;

                                    }
                                    nhanVien.TinhTrang = tinhTrang;
                                }
                            }
                            #endregion
                            #endregion

                            #region Thông tin nhân viên cột 41 --> 55
                            #region Ngày nghỉ việc - ngày về hưu cột 41
                            {
                                string ngayString = dr[ngayNghiHuuIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    { }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                        {
                                            nhanVien.NgayVaoCoQuan = ngay.Value;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày nghỉ hưu không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày vào cơ quan không đúng định dạng dd/MM/yyyy.");
                                    }
                                }
                            }


                            #endregion

                            #region Chức vụ hiện tại cột 42
                            {
                                String chucVuHienTai = dr[chucVuIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(chucVuHienTai))
                                {
                                    ChucVu chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", chucVuHienTai));
                                    if (chucVu == null)
                                    {
                                        chucVu = new ChucVu(uow);
                                        chucVu.TenChucVu = chucVuHienTai;
                                        chucVu.MaQuanLy = Guid.NewGuid().ToString();
                                        chucVu.Save();
                                    }
                                    nhanVien.ChucVu = chucVu;
                                }
                            }
                            #endregion

                            #region Chức vụ kiêm nhiệm cột 44
                            //{
                            //    String chucVuKiemNhiem = dr[chucVuKiemNhiemIdx].ToString().FullTrim();
                            //    if (!string.IsNullOrEmpty(chucVuKiemNhiem))
                            //    {
                            //        ChucVu chucVu = uow.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", chucVuKiemNhiem));
                            //        if (chucVu == null)
                            //        {
                            //            chucVu = new ChucVu(uow);
                            //            chucVu.TenChucVu = chucVuKiemNhiem;
                            //            chucVu.MaQuanLy = Guid.NewGuid().ToString();
                            //            chucVu.Save();
                            //        }
                            //        //nhanVien.ChucVu = chucVu;
                            //        ChucVuKiemNhiem chucVuKN = new ChucVuKiemNhiem(uow);
                            //        chucVuKN.ThongTinNhanVien = nhanVien;
                            //        chucVuKN.ChucVu = chucVu;
                            //        chucVuKN.BoPhan = nhanVien.BoPhan;
                            //        chucVuKN.Save();
                            //    }
                            //}
                            #endregion

                            #region Loại nhân viên cột 47
                            {
                                string loaiNhanVienText = dr[loaiNhanVienIdx].ToString().Trim();
                                if (!string.IsNullOrEmpty(loaiNhanVienText))
                                {
                                    LoaiNhanVien loaiNhanVien = uow.FindObject<LoaiNhanVien>(CriteriaOperator.Parse("TenLoaiNhanVien like ?", loaiNhanVienText));
                                    if (loaiNhanVien == null)
                                    {
                                        loaiNhanVien = new LoaiNhanVien(uow);
                                        loaiNhanVien.TenLoaiNhanVien = loaiNhanVienText;
                                    }
                                    nhanVien.LoaiNhanVien = loaiNhanVien;
                                }
                            }
                            #endregion

                            #region Loại nhân sự cột 48
                            {
                                String loaiNhanSuText = dr[loaiNhanSuIdx].ToString().Trim();
                                if (!string.IsNullOrEmpty(loaiNhanSuText))
                                {
                                    LoaiNhanSu loaiNhanSu = uow.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu Like ?", loaiNhanSuText));
                                    if (loaiNhanSu == null)
                                    {
                                        loaiNhanSu = new LoaiNhanSu(uow);
                                        loaiNhanSu.TenLoaiNhanSu = loaiNhanSuText;
                                    }
                                    nhanVien.LoaiNhanSu = loaiNhanSu;
                                }
                            }
                            #endregion

                            #region Số hiệu công chức 49
                            {
                                String soHieucongChucText = dr[soHieuCongChucIdx].ToString().Trim();
                                if (!string.IsNullOrEmpty(soHieucongChucText))
                                {
                                    nhanVien.SoHieuCongChuc = soHieucongChucText;
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(maQuanLy))
                                    {
                                        nhanVien.SoHieuCongChuc = nhanVien.MaQuanLy;
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Thiếu mã quản lý nhân viên.");
                                    }
                                }
                            }
                            #endregion

                            #region Ngày vào biên chế cột 50
                            {
                                string ngayString = dr[ngayVaoBienCheIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    { }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                        {
                                            nhanVien.NgayVaoBienChe = ngay.Value;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày vào biên chế: " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày vào biên chế không đúng định dạng dd/MM/yyyy.");
                                    }
                                }
                            }
                            #endregion

                            #region Nhóm máu cột 51
                            {
                                String nhomMauText = dr[nhomMauIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(nhomMauText))
                                {
                                    NhomMau nhomMau = uow.FindObject<NhomMau>(CriteriaOperator.Parse("TenNhomMau like ?", nhomMauText));
                                    if (nhomMau == null)
                                    {
                                        nhomMau = new NhomMau(uow);
                                        nhomMau.TenNhomMau = nhomMauText;
                                        nhomMau.MaQuanLy = Guid.NewGuid().ToString();
                                    }
                                    nhanVien.NhomMau = nhomMau;
                                }
                            }
                            #endregion

                            #region Chiều cao cột 52
                            {
                                String chieuCaotext = dr[chieuCaoIdx].ToString();
                                Decimal chieuCao;
                                if (Decimal.TryParse(chieuCaotext, out chieuCao))
                                {
                                    nhanVien.ChieuCao = Convert.ToInt32(chieuCao * 100); ;
                                }
                            }
                            #endregion

                            #region Cân nặng cột 53
                            {
                                String CanNangtext = dr[canNangIdx].ToString();
                                Decimal canNang;
                                if (Decimal.TryParse(CanNangtext, out canNang))
                                {
                                    nhanVien.CanNang = Convert.ToInt32(canNang * 100); ;
                                }
                            }
                            #endregion

                            #region Tình trạng sức khỏe cột 54
                            {
                                String sucKhoeText = dr[tinhTrangSucKhoeIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(sucKhoeText))
                                {
                                    SucKhoe sucKhoe = uow.FindObject<SucKhoe>(CriteriaOperator.Parse("TenSucKhoe like ?", sucKhoeText));
                                    if (sucKhoe == null)
                                    {
                                        sucKhoe = new SucKhoe(uow);
                                        sucKhoe.TenSucKhoe = sucKhoeText;
                                        sucKhoe.MaQuanLy = Guid.NewGuid().ToString();
                                    }
                                    nhanVien.TinhTrangSucKhoe = sucKhoe;
                                }
                            }
                            #endregion

                            #region Ngày tính thâm niên nhà giáo
                            {
                                string ngayString = dr[ngayTinhThamNienNhaGiaoIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    { }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                        {
                                            nhanVien.NgayTinhThamNienNhaGiao = ngay.Value;
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày Tính Thâm Niên Nhà Giáo : " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" +  Ngày Tính Thâm Niên Nhà Giáo không đúng định dạng dd/MM/yyyy.");
                                    }
                                }
                            }
                            #endregion
                            #endregion

                            #region Nhân viên thông tin lương cột 56 --> 71 và cột 120 mới thêm cho QNU
                            #region Ngạch lương cột 56, tên ngạch lương cột 57, ngày bổ nhiệm ngạch cột 58, bậc lương cột 60, hệ số lương cột 61
                            {
                                String maNgachLuong = dr[ngachLuongIdx].ToString().RemoveEmpty();
                                String tenNgachLuong = dr[tenNgachIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(maNgachLuong))
                                {
                                    NgachLuong ngach = null;
                                    ngach = uow.FindObject<NgachLuong>(CriteriaOperator.Parse("MaQuanLy like ?", maNgachLuong));
                                    //kiểm tra và thêm mới nếu ko tồn tại                                    
                                    if (ngach == null)
                                    {
                                        ngach = new NgachLuong(uow);
                                        ngach.TenNgachLuong = tenNgachLuong;
                                        ngach.MaQuanLy = maNgachLuong;
                                    }

                                    if (ngach != null)
                                    {
                                        nhanVien.NhanVienThongTinLuong.NgachLuong = ngach;

                                        #region Ngày bổ nhiệm ngạch cột 58
                                        {
                                            string ngayString = dr[ngayBoNhiemNgachIdx].ToString().Trim();
                                            if (!string.IsNullOrWhiteSpace(ngayString))
                                            {
                                                DateTime? ngay = null;
                                                try
                                                {
                                                    ngay = Convert.ToDateTime(ngayString);
                                                }
                                                catch (Exception ex) { }

                                                if (ngay != null)
                                                {
                                                    if (ngay != DateTime.MinValue)
                                                        nhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = ngay.Value;
                                                    else
                                                        errorLog.AppendLine(" + Ngày bổ nhiệm ngạch không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                                }
                                                else
                                                {
                                                    errorLog.AppendLine(" + Ngàybổ nhiệm ngạch không đúng định dạng dd/MM/yyyy.");
                                                }
                                            }
                                        }
                                        #endregion

                                        String bacLuongText = dr[bacLuongIdx].ToString().FullTrim();
                                        if (!string.IsNullOrEmpty(bacLuongText))
                                        {
                                            BacLuong bacLuong = null;
                                            bacLuong = uow.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong = ? and MaQuanLy = ?", ngach.Oid.ToString(), bacLuongText));

                                            if (bacLuong == null)
                                            {
                                                String heSoLuongString = dr[heSoLuongIdx].ToString().Trim();
                                                decimal heSoLuong = 0;
                                                bool kq = decimal.TryParse(heSoLuongString, out heSoLuong);
                                                if (kq == false)
                                                    errorLog.AppendLine(" + Thiếu thông tin hệ số lương hoặc không đúng định dạng số: " + heSoLuongString);
                                                bacLuong = new BacLuong(uow);
                                                bacLuong.HeSoLuong = heSoLuong;
                                                bacLuong.NgachLuong = ngach;
                                                // bacLuong.TenBacLuong = bacLuongText;
                                                bacLuong.MaQuanLy = bacLuongText;
                                                //bacLuong.Save();
                                            }

                                            if (bacLuong != null)
                                            {
                                                nhanVien.NhanVienThongTinLuong.BacLuong = bacLuong;
                                                nhanVien.NhanVienThongTinLuong.HeSoLuong = bacLuong.HeSoLuong;
                                            }
                                            else
                                            {
                                                //errorLog.AppendLine(" + Bậc lương không hợp lệ: " + bacLuongText);
                                            }
                                        }
                                        else
                                        {
                                            //errorLog.AppendLine(" + Thiếu thông tin bậc lương.");
                                        }
                                    }
                                    else
                                    {
                                        //errorLog.AppendLine(" + Ngạch lương không hợp lệ: " + maNgachLuong);
                                    }
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin ngạch lương.");
                                }
                            }
                            #endregion

                            #region Ngày hưởng lương cột 59
                            {
                                string ngayString = dr[ngayHuongLuongIdx].ToString().Trim();
                                if (!string.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex) { }

                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NhanVienThongTinLuong.NgayHuongLuong = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày hưởng lương không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày hưởng lương không đúng định dạng dd/MM/yyyy.");
                                    }
                                }

                            }
                            #endregion

                            #region Hệ số phụ cấp chức vụ, Tên phụ cấp chức vụ cột 62
                            {
                                String hspcChucVuText = dr[heSoPhuCapChucVuIdx].ToString();
                                Decimal hspcChucVu = 0;
                                if (!string.IsNullOrEmpty(hspcChucVuText))
                                {
                                    try
                                    {
                                        hspcChucVu = Convert.ToDecimal(hspcChucVuText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Hệ số chức vụ không hợp lệ." + hspcChucVuText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.HSPCChucVu = hspcChucVu;
                                }


                            }
                            #endregion

                            #region Hệ số phụ cấp ưu đãi, Tên phụ cấp ưu đãi cột 63
                            {
                                String hspcUuDaiText = dr[heSoPhuCapUuDaiIdx].ToString();
                                Decimal hspcUuDai = 0;
                                if (!string.IsNullOrEmpty(hspcUuDaiText))
                                {
                                    try
                                    {
                                        hspcUuDai = Convert.ToDecimal(hspcUuDaiText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Hệ số phụ cấp trách nhiệm không hợp lệ." + hspcUuDaiText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.HSPCUuDai = hspcUuDai;
                                }

                            }
                            #endregion

                            #region Hệ số phụ cấp trách nhiệm, Tên phụ cấp trách nhiệm cột 65
                            {
                                String hspcTrachNhiemText = dr[heSoPhuCapTrachNhiemIdx].ToString();
                                Decimal hspcTrachNhiem = 0;
                                if (!string.IsNullOrEmpty(hspcTrachNhiemText))
                                {
                                    try
                                    {
                                        hspcTrachNhiem = Convert.ToDecimal(hspcTrachNhiemText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Hệ số phụ cấp trách nhiệm không hợp lệ." + hspcTrachNhiemText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = hspcTrachNhiem;
                                }
                            }
                            #endregion

                            #region Hệ số phụ cấp thu nhập tăng thêm cột 66
                            {
                                String hspcThuNhapTangText = dr[heSoPhuCapThuNhapTangIdx].ToString();
                                Decimal hspcThuNhapTang = 0;
                                if (!string.IsNullOrEmpty(hspcThuNhapTangText))
                                {
                                    try
                                    {
                                        hspcThuNhapTang = Convert.ToDecimal(hspcThuNhapTangText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Hệ số phụ cấp thu nhập tăng thêm không hợp lệ." + hspcThuNhapTangText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.HSLTangThem = hspcThuNhapTang;
                                }
                            }
                            #endregion

                            #region % phụ cấp thâm niên vượt khung cột 67
                            {
                                String text = dr[heSoVuotKhungIdx].ToString();
                                Int32 phanTramThamNienVuotKhung;
                                if (Int32.TryParse(text, out phanTramThamNienVuotKhung))
                                {

                                    nhanVien.NhanVienThongTinLuong.VuotKhung = phanTramThamNienVuotKhung;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin hoặc định dạng sai % thâm niên vượt khung.");
                                    nhanVien.NhanVienThongTinLuong.VuotKhung = 0;
                                }
                            }
                            #endregion

                            #region % phụ cấp thâm niên cột 68
                            {
                                String hspcThamNienText = dr[heSoPhuCapThamNienIdx].ToString();
                                Decimal hspcThamNien = 0;
                                if (!string.IsNullOrEmpty(hspcThamNienText))
                                {
                                    try
                                    {
                                        hspcThamNien = Convert.ToDecimal(hspcThamNienText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Hệ số phụ cấp trách nhiệm không hợp lệ." + hspcThamNienText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.HSPCThamNien = hspcThamNien;
                                }
                            }
                            #endregion

                            #region Mốc tính lương lần sau số 69
                            {
                                String ngayString = dr[mocTinhLuongLanSauIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    { }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NgaySinh = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày tính lương lần sau không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày tính lương lần sau không đúng định dạng dd/MM/yyyy.");

                                    }
                                }
                                else
                                {
                                    //errorLog.AppendLine( " + Thiếu thông tin ngày sinh.");
                                }
                            }
                            #endregion

                            #region Hưởng 85% lương - tập sự cột 70
                            {
                                String huong85Luong = dr[huong85LuongIdx].ToString().Trim();
                                if (!string.IsNullOrEmpty(huong85Luong))
                                {
                                    nhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = true;
                                }
                                else
                                {
                                    nhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = false;
                                }
                            }
                            #endregion

                            #region Mã số thuế cột 71
                            {
                                String maSoThue = dr[maSoThueIdx].ToString();
                                if (!string.IsNullOrEmpty(maSoThue))
                                {
                                    nhanVien.NhanVienThongTinLuong.MaSoThue = maSoThue;
                                }
                                else
                                {
                                    //errorLog.AppendLine(" + Thiếu thông tin hoặc định dạng sai % thâm niên vượt khung.");
                                }
                            }
                            #endregion

                            #region Hệ số phụ cấp phục vụ đào tạo cột 120
                            {
                                String hspcPhucVuDaoTaoText = dr[heSoPhuCapPhucVuDaoTaoIdx].ToString();
                                Decimal hspcPhucVuDaoTao = 0;
                                if (!string.IsNullOrEmpty(hspcPhucVuDaoTaoText))
                                {
                                    try
                                    {
                                        hspcPhucVuDaoTao = Convert.ToDecimal(hspcPhucVuDaoTaoText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Hệ số phụ cấp phục vụ đào tạo không hợp lệ." + hspcPhucVuDaoTaoText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.HSPCPhucVuDaoTao = hspcPhucVuDaoTao;
                                }
                            }
                            #endregion

                            #endregion

                            #region Thông tin bảo hiểm cột 72 --> 77
                            String soSoBHXHText = dr[soBHXHIdx].ToString().Trim();
                            String ngayThamDongBHXHText = dr[ngayThamGiaBHXHIdx].ToString().Trim();
                            String soTheBHYTText = dr[soTheBHYTIdx].ToString().Trim();
                            String tuNgayBHYTText = dr[tuNgayIdx].ToString().Trim();
                            String denNgayBHYTText = dr[denNgayIdx].ToString().Trim();
                            String noiDangKyKCBText = dr[noiDangKyKCBIdx].ToString().Trim();

                            if (!String.IsNullOrWhiteSpace(soSoBHXHText))
                            {
                                HoSoBaoHiem hoSoBaoHiem = null;
                                hoSoBaoHiem = uow.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien.MaQuanLy like ?", nhanVien.MaQuanLy));
                                //nếu nhân viên đó chưa có bảo hiểm thì thêm hồ sơ bảo hiêm
                                if (hoSoBaoHiem == null)
                                {
                                    hoSoBaoHiem = new HoSoBaoHiem(uow);
                                    hoSoBaoHiem.BoPhan = nhanVien.BoPhan;
                                    hoSoBaoHiem.ThongTinNhanVien = nhanVien;
                                    hoSoBaoHiem.SoSoBHXH = soSoBHXHText;

                                    #region ngày tham gia bảo hiểm xã hội
                                    if (!String.IsNullOrWhiteSpace(ngayThamDongBHXHText))
                                    {
                                        DateTime? ngay = null;
                                        try
                                        {
                                            ngay = Convert.ToDateTime(ngayThamDongBHXHText);
                                        }
                                        catch (Exception ex)
                                        {
                                        }

                                        if (ngay != null)
                                        {
                                            if (ngay != DateTime.MinValue)
                                                hoSoBaoHiem.NgayThamGiaBHXH = ngay.Value;
                                            else
                                                errorLog.AppendLine(" + Ngày tham gia bảo hiểm XH không hợp lệ: " +
                                                                    ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày tham gia bảo hiểm XH không đúng định dạng dd/MM/yyyy.");
                                        }
                                    }
                                    #endregion

                                    #region Thẻ bảo hiểm y tế
                                    hoSoBaoHiem.SoTheBHYT = soTheBHYTText;
                                    #endregion

                                    #region ngày bắt đầu thẻ bảo hiểm Y tế
                                    if (!String.IsNullOrWhiteSpace(tuNgayBHYTText))
                                    {
                                        DateTime? ngay = null;
                                        try
                                        {
                                            ngay = Convert.ToDateTime(tuNgayBHYTText);
                                        }
                                        catch (Exception ex)
                                        {
                                        }

                                        if (ngay != null)
                                        {
                                            if (ngay != DateTime.MinValue)
                                                hoSoBaoHiem.TuNgay = ngay.Value;
                                            else
                                                errorLog.AppendLine(" + Ngày tham gia bảo hiểm Y tế không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày tham gia bảo hiểm Y tế không đúng định dạng dd/MM/yyyy.");
                                        }
                                    }
                                    #endregion

                                    #region ngày kết thúc bảo hiểm Y tế
                                    if (!String.IsNullOrWhiteSpace(denNgayBHYTText))
                                    {
                                        DateTime? ngay = null;
                                        try
                                        {
                                            ngay = Convert.ToDateTime(denNgayBHYTText);
                                        }
                                        catch (Exception ex)
                                        {
                                        }

                                        if (ngay != null)
                                        {
                                            if (ngay != DateTime.MinValue)
                                                hoSoBaoHiem.DenNgay = ngay.Value;
                                            else
                                                errorLog.AppendLine(" + Ngày kết thúc tham gia bảo hiểm Y tế không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                        }
                                        else
                                        {
                                            errorLog.AppendLine(" + Ngày kết thúc bảo hiểm Y tế không đúng định dạng dd/MM/yyyy.");
                                        }
                                    }
                                    #endregion

                                    #region Nơi đăng ký khám chữa bệnh
                                    if (!String.IsNullOrWhiteSpace(noiDangKyKCBText))
                                    {
                                        BenhVien benhVien = null;
                                        benhVien = uow.FindObject<BenhVien>(CriteriaOperator.Parse("TenBenhVien Like ?", noiDangKyKCBText));
                                        if (benhVien == null)
                                        {
                                            benhVien = new BenhVien(uow);
                                            benhVien.TenBenhVien = noiDangKyKCBText;
                                        }
                                        hoSoBaoHiem.NoiDangKyKhamChuaBenh = benhVien;
                                    }
                                    #endregion
                                }
                            }

                            #endregion

                            #region Trình độ học tập cột 78 --> 104
                            #region Trình độ văn hóa cột 78
                            {
                                String trinhDoVanHoaText = dr[trinhDoVanHoaIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(trinhDoVanHoaText))
                                {
                                    TrinhDoVanHoa trinhDo = uow.FindObject<TrinhDoVanHoa>(CriteriaOperator.Parse("TenTrinhDoVanHoa Like ?", trinhDoVanHoaText));
                                    if (trinhDo == null)
                                    {
                                        trinhDo = new TrinhDoVanHoa(uow);
                                        trinhDo.TenTrinhDoVanHoa = trinhDoVanHoaText;
                                        trinhDo.MaQuanLy = trinhDoVanHoaText;
                                    }
                                    nhanVien.NhanVienTrinhDo.TrinhDoVanHoa = trinhDo;

                                }
                            }
                            #endregion

                            #region Trình độ trung cấp cột 79 --> 82
                            String chuyenMonDaoTaoTCText = dr[chuyenMonDaoTaoTCIdx].ToString().Trim();
                            String truongDaoTaoTCText = dr[truongDaoTaoTCIdx].ToString().Trim();
                            String hinhThucDaoTaoTCText = dr[hinhThucDaoTaoTCIdx].ToString().Trim();
                            String ngayTotNghiepTC = dr[ngayCongNhanTCIdx].ToString().Trim();

                            if (!String.IsNullOrEmpty(chuyenMonDaoTaoTCText))
                            {
                                TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ? or TenTrinhDoChuyenMon Like ?", "trung cấp", "trung học%"));
                                if (trinhDo != null)
                                {
                                    VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", nhanVien.MaQuanLy, trinhDo));
                                    if (bang == null)
                                    {
                                        bang = new VanBang(uow);
                                        bang.HoSo = nhanVien;
                                        bang.TrinhDoChuyenMon = trinhDo;
                                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                        ChuyenMonDaoTao chuyenMon = null;
                                        chuyenMon = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoTCText));
                                        if (chuyenMon == null)
                                        {
                                            chuyenMon = new ChuyenMonDaoTao(uow);
                                            chuyenMon.TenChuyenMonDaoTao = chuyenMonDaoTaoTCText;
                                            chuyenMon.MaQuanLy = chuyenMonDaoTaoTCText;

                                        }
                                        bang.ChuyenMonDaoTao = chuyenMon;
                                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = chuyenMon;
                                        //else
                                        //    errorLog.AppendLine(" + Chuyên môn đào tạo trình độ Trung học không hợp lệ: " + chuyenMonDaoTaoTCText);

                                        //trường
                                        if (!String.IsNullOrEmpty(truongDaoTaoTCText))
                                        {
                                            TruongDaoTao truong = null;
                                            truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", truongDaoTaoTCText));
                                            if (truong == null)
                                            {
                                                truong = new TruongDaoTao(uow);
                                                truong.TenTruongDaoTao = truongDaoTaoTCText;
                                                truong.MaQuanLy = truongDaoTaoTCText;
                                            }
                                            bang.TruongDaoTao = truong;
                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                            //else
                                            //    errorLog.AppendLine(" + Trường đào tạo trình độ Trung học không hợp lệ: " + truongDaoTaoTCText);
                                        }

                                        if (!String.IsNullOrEmpty(hinhThucDaoTaoTCText))
                                        {
                                            HinhThucDaoTao hinhThuc = null;
                                            hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", hinhThucDaoTaoTCText));
                                            if (hinhThuc == null)
                                            {
                                                hinhThuc = new HinhThucDaoTao(uow);
                                                hinhThuc.TenHinhThucDaoTao = hinhThucDaoTaoTCText;
                                                hinhThuc.MaQuanLy = hinhThucDaoTaoTCText;
                                            }
                                            bang.HinhThucDaoTao = hinhThuc;
                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                            //else
                                            //    errorLog.AppendLine(" + Hình thức đào tạo trình độ Trung học không hợp lệ: " + hinhThucDaoTaoTCText);
                                        }
                                        #region năm công nhận tốt nghiệp
                                        if (!String.IsNullOrWhiteSpace(ngayTotNghiepTC))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {
                                                if (ngayTotNghiepTC.Length <10)
                                                    ngayTotNghiepTC = "01/01/" + ngayTotNghiepTC;
                                                ngay = Convert.ToDateTime(ngayTotNghiepTC);
                                            }
                                            catch (Exception ex)
                                            {
                                            }

                                            if (ngay != null)
                                            {
                                                if (ngay != DateTime.MinValue)
                                                {
                                                    bang.NamTotNghiep = ngay.Value.Year;
                                                    nhanVien.NhanVienTrinhDo.NamTotNghiep = ngay.Value.Year;
                                                }
                                                else
                                                    errorLog.AppendLine(" + Năm Tốt Nghiệp Trung Cấp không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Năm tốt nghiệp Trung Cấp không đúng định dạng dd/MM/yyyy.");
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ Cao Đẳng cột 83 --> 86
                            String chuyenMonDaoTaoCDText = dr[chuyenMonDaoTaoCDIdx].ToString().Trim();
                            String truongDaoTaoCDText = dr[truongDaoTaoCDIdx].ToString().Trim();
                            String hinhThucDaoTaoCDText = dr[hinhThucDaoTaoCDIdx].ToString().Trim();
                            String ngayTotNghiepCD = dr[ngayCongNhanCDIdx].ToString().Trim();

                            if (!String.IsNullOrEmpty(chuyenMonDaoTaoCDText))
                            {
                                TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ?", "cao đẳng"));
                                if (trinhDo != null)
                                {
                                    VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", nhanVien.MaQuanLy, trinhDo));
                                    if (bang == null)
                                    {
                                        bang = new VanBang(uow);
                                        bang.HoSo = nhanVien;
                                        bang.TrinhDoChuyenMon = trinhDo;
                                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                        ChuyenMonDaoTao chuyenMon = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoCDText));
                                        if (chuyenMon == null)
                                        {
                                            chuyenMon = new ChuyenMonDaoTao(uow);
                                            chuyenMon.TenChuyenMonDaoTao = chuyenMonDaoTaoCDText;
                                            chuyenMon.MaQuanLy = chuyenMonDaoTaoCDText;
                                        }
                                        bang.ChuyenMonDaoTao = chuyenMon;
                                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = chuyenMon;
                                        //else
                                        //    errorLog.AppendLine(" + Chuyên môn đào tạo trình độ Cao đẳng không hợp lệ: " + chuyenMonDaoTaoCDText);

                                        //trường
                                        if (!String.IsNullOrEmpty(truongDaoTaoCDText))
                                        {
                                            TruongDaoTao truong = null;
                                            truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", truongDaoTaoCDText));
                                            if (truong == null)
                                            {
                                                truong = new TruongDaoTao(uow);
                                                truong.MaQuanLy = truongDaoTaoCDText;
                                                truong.TenTruongDaoTao = truongDaoTaoCDText;
                                            }
                                            bang.TruongDaoTao = truong;
                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                            //else
                                            //    errorLog.AppendLine(" + Trường đào tạo trình độ Cao Đẳng không hợp lệ: " + truongDaoTaoCDText);
                                        }

                                        if (!String.IsNullOrEmpty(hinhThucDaoTaoCDText))
                                        {
                                            HinhThucDaoTao hinhThuc = null;
                                            hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", hinhThucDaoTaoCDText));
                                            if (hinhThuc == null)
                                            {
                                                hinhThuc = new HinhThucDaoTao(uow);
                                                hinhThuc.MaQuanLy = hinhThucDaoTaoCDText;
                                                hinhThuc.TenHinhThucDaoTao = hinhThucDaoTaoCDText;

                                            }
                                            bang.HinhThucDaoTao = hinhThuc;
                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                            //else
                                            //    errorLog.AppendLine(" + Hình thức đào tạo trình độ Cao Đẳng không hợp lệ: " + hinhThucDaoTaoCDText);
                                        }
                                        #region năm công nhận tốt nghiệp
                                        if (!String.IsNullOrWhiteSpace(ngayTotNghiepCD))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {
                                                if (ngayTotNghiepCD.Length < 10)
                                                    ngayTotNghiepCD = "01/01/" + ngayTotNghiepCD;
                                                ngay = Convert.ToDateTime(ngayTotNghiepCD);
                                            }
                                            catch (Exception ex)
                                            {
                                            }

                                            if (ngay != null)
                                            {
                                                if (ngay != DateTime.MinValue)
                                                {
                                                    bang.NamTotNghiep = ngay.Value.Year;
                                                    nhanVien.NhanVienTrinhDo.NamTotNghiep = ngay.Value.Year;
                                                }
                                                else
                                                    errorLog.AppendLine(" + Năm Tốt Nghiệp Cao Đẳng không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Nắm tốt nghiệp Cao Đẳng không đúng định dạng dd/MM/yyyy.");
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ Đại học cột 87 --> 90
                            String chuyenMonDaoTaoDHText = dr[chuyenMonDaoTaoDHIdx].ToString().Trim();
                            String truongDaoTaoDHText = dr[truongDaoTaoDHIdx].ToString().Trim();
                            String hinhThucDaoTaoDHText = dr[hinhThucDaoTaoDHIdx].ToString().Trim();
                            String ngayTotNghiepDH = dr[ngayCongNhanDHIdx].ToString().Trim();

                            if (!String.IsNullOrEmpty(chuyenMonDaoTaoDHText))
                            {
                                TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ?", "đại học"));
                                if (trinhDo != null)
                                {
                                    VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", nhanVien.MaQuanLy, trinhDo));
                                    if (bang == null)
                                    {
                                        bang = new VanBang(uow);
                                        bang.HoSo = nhanVien;
                                        bang.TrinhDoChuyenMon = trinhDo;
                                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                        ChuyenMonDaoTao chuyenMon = null;
                                        chuyenMon = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoDHText));
                                        if (chuyenMon == null)
                                        {
                                            chuyenMon = new ChuyenMonDaoTao(uow);
                                            chuyenMon.TenChuyenMonDaoTao = chuyenMonDaoTaoDHText;
                                            chuyenMon.MaQuanLy = chuyenMonDaoTaoDHText;
                                        }
                                        bang.ChuyenMonDaoTao = chuyenMon;
                                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = chuyenMon;
                                        //else
                                        //    errorLog.AppendLine(" + Chuyên môn đào tạo trình độ Đại học không hợp lệ: " + chuyenMonDaoTaoDHText);

                                        //trường
                                        if (!String.IsNullOrEmpty(truongDaoTaoDHText))
                                        {
                                            TruongDaoTao truong = null;
                                            truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", truongDaoTaoDHText));
                                            if (truong == null)
                                            {
                                                truong = new TruongDaoTao(uow);
                                                truong.MaQuanLy = truongDaoTaoDHText;
                                                truong.TenTruongDaoTao = truongDaoTaoDHText;
                                            }
                                            bang.TruongDaoTao = truong;
                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                            //else
                                            //{
                                            //    errorLog.AppendLine(" + Trường đào tạo trình độ Đại học không hợp lệ: " + truongDaoTaoDHText); 
                                            //}
                                        }

                                        if (!String.IsNullOrEmpty(hinhThucDaoTaoDHText))
                                        {
                                            HinhThucDaoTao hinhThuc = null;
                                            hinhThuc = uow.FindObject<HinhThucDaoTao>(CriteriaOperator.Parse("TenHinhThucDaoTao like ?", hinhThucDaoTaoDHText));
                                            if (hinhThuc == null)
                                            {
                                                hinhThuc = new HinhThucDaoTao(uow);
                                                hinhThuc.MaQuanLy = hinhThucDaoTaoDHText;
                                                hinhThuc.TenHinhThucDaoTao = hinhThucDaoTaoDHText;
                                            }
                                            bang.HinhThucDaoTao = hinhThuc;
                                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = hinhThuc;
                                            //else
                                            //    errorLog.AppendLine(" + Hình thức đào tạo trình độ Đại học không hợp lệ: " + hinhThucDaoTaoDHText);
                                        }
                                        #region năm công nhận tốt nghiệp
                                        if (!String.IsNullOrWhiteSpace(ngayTotNghiepDH))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {

                                                if (ngayTotNghiepDH.Length < 10)
                                                    ngayTotNghiepDH = "01/01/" + ngayTotNghiepDH;
                                                ngay = Convert.ToDateTime(ngayTotNghiepDH);
                                            }
                                            catch (Exception ex)
                                            {
                                            }

                                            if (ngay != null)
                                            {
                                                if (ngay != DateTime.MinValue)
                                                {
                                                    bang.NamTotNghiep = ngay.Value.Year;
                                                    nhanVien.NhanVienTrinhDo.NamTotNghiep = ngay.Value.Year;
                                                }
                                                else
                                                    errorLog.AppendLine(" + Năm Tốt Nghiệp Đại học không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Nắm tốt nghiệp Đại học không đúng định dạng dd/MM/yyyy.");
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ Thạc Sĩ cột 91 --> 93
                            String chuyenMonDaoTaoThSText = dr[chuyenMonDaoTaoThSIdx].ToString().Trim();
                            String truongDaoTaoThSText = dr[truongDaoTaoThSIdx].ToString().Trim();
                            String ngayTotNghiepThS = dr[ngayCongNhanThSIdx].ToString().Trim();

                            if (!String.IsNullOrEmpty(chuyenMonDaoTaoThSText))
                            {
                                TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ?", "thạc%"));
                                if (trinhDo != null)
                                {
                                    VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", nhanVien.MaQuanLy, trinhDo));
                                    if (bang == null)
                                    {
                                        bang = new VanBang(uow);
                                        bang.HoSo = nhanVien;
                                        bang.TrinhDoChuyenMon = trinhDo;
                                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                        ChuyenMonDaoTao chuyenMon = null;
                                        chuyenMon = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoThSText));
                                        if (chuyenMon == null)
                                        {
                                            chuyenMon = new ChuyenMonDaoTao(uow);
                                            chuyenMon.TenChuyenMonDaoTao = chuyenMonDaoTaoThSText;
                                            chuyenMon.MaQuanLy = chuyenMonDaoTaoThSText;
                                        }
                                        bang.ChuyenMonDaoTao = chuyenMon;
                                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = chuyenMon;
                                        //else
                                        //    errorLog.AppendLine(" + Chuyên môn đào tạo trình độ Thạc Sĩ không hợp lệ: " + chuyenMonDaoTaoThSText);

                                        //trường
                                        if (!String.IsNullOrEmpty(truongDaoTaoThSText))
                                        {
                                            TruongDaoTao truong = null;
                                            truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", truongDaoTaoThSText));
                                            if (truong == null)
                                            {
                                                truong = new TruongDaoTao(uow);
                                                truong.MaQuanLy = truongDaoTaoThSText;
                                                truong.TenTruongDaoTao = truongDaoTaoThSText;

                                            }
                                            bang.TruongDaoTao = truong;
                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                            //else
                                            //    errorLog.AppendLine(" + Trường đào tạo trình độ Thạc Sĩ không hợp lệ: " + truongDaoTaoThSText);
                                        }

                                        #region năm công nhận tốt nghiệp
                                        if (!String.IsNullOrWhiteSpace(ngayTotNghiepThS))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {
                                                if (ngayTotNghiepThS.Length < 10)
                                                    ngayTotNghiepThS = "01/01/" + ngayTotNghiepThS;
                                                ngay = Convert.ToDateTime(ngayTotNghiepThS);
                                            }
                                            catch (Exception ex)
                                            {
                                            }

                                            if (ngay != null)
                                            {
                                                if (ngay != DateTime.MinValue)
                                                {
                                                    bang.NamTotNghiep = ngay.Value.Year;
                                                    nhanVien.NhanVienTrinhDo.NamTotNghiep = ngay.Value.Year;
                                                }
                                                else
                                                    errorLog.AppendLine(" + Năm Tốt Nghiệp Thạc Sĩ không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Nắm tốt nghiệp Thạc Sĩ không đúng định dạng dd/MM/yyyy.");
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ Tiến Sĩ cột 94 --> 96
                            String chuyenMonDaoTaoTSText = dr[chuyenMonDaoTaoTSIdx].ToString().Trim();
                            String truongDaoTaoTSText = dr[truongDaoTaoTSIdx].ToString().Trim();
                            String ngayTotNghiepTS = dr[ngayCongNhanTSIdx].ToString().Trim();

                            if (!String.IsNullOrEmpty(chuyenMonDaoTaoTSText))
                            {
                                TrinhDoChuyenMon trinhDo = uow.FindObject<TrinhDoChuyenMon>(CriteriaOperator.Parse("TenTrinhDoChuyenMon Like ?", "tiến%"));
                                if (trinhDo != null)
                                {
                                    VanBang bang = uow.FindObject<VanBang>(CriteriaOperator.Parse("HoSo.MaQuanLy like ? and TrinhDoChuyenMon = ?", nhanVien.MaQuanLy, trinhDo));
                                    if (bang == null)
                                    {
                                        bang = new VanBang(uow);
                                        bang.HoSo = nhanVien;
                                        bang.TrinhDoChuyenMon = trinhDo;
                                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = trinhDo;

                                        ChuyenMonDaoTao chuyenMon = uow.FindObject<ChuyenMonDaoTao>(CriteriaOperator.Parse("TenChuyenMonDaoTao like ?", chuyenMonDaoTaoTSText));
                                        if (chuyenMon == null)
                                        {
                                            chuyenMon = new ChuyenMonDaoTao(uow);
                                            chuyenMon.TenChuyenMonDaoTao = chuyenMonDaoTaoTSText;
                                            chuyenMon.MaQuanLy = chuyenMonDaoTaoTSText;
                                        }
                                        bang.ChuyenMonDaoTao = chuyenMon;
                                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = chuyenMon;
                                        //else
                                        //    errorLog.AppendLine(" + Chuyên môn đào tạo trình độ Tiến Sĩ không hợp lệ: " + chuyenMonDaoTaoTSText);

                                        //trường
                                        if (!String.IsNullOrEmpty(truongDaoTaoTSText))
                                        {
                                            TruongDaoTao truong = uow.FindObject<TruongDaoTao>(CriteriaOperator.Parse("TenTruongDaoTao like ?", truongDaoTaoTSText));
                                            if (truong == null)
                                            {
                                                truong = new TruongDaoTao(uow);
                                                truong.MaQuanLy = truongDaoTaoTSText;
                                                truong.TenTruongDaoTao = truongDaoTaoTSText;
                                            }
                                            bang.TruongDaoTao = truong;
                                            nhanVien.NhanVienTrinhDo.TruongDaoTao = truong;
                                            //else
                                            //    errorLog.AppendLine(" + Trường đào tạo trình độ Tiến Sĩ không hợp lệ: " + truongDaoTaoTSText);
                                        }

                                        #region năm công nhận tốt nghiệp
                                        if (!String.IsNullOrWhiteSpace(ngayTotNghiepTS))
                                        {
                                            DateTime? ngay = null;
                                            try
                                            {
                                                if (ngayTotNghiepTS.Length < 10)
                                                    ngayTotNghiepTS = "01/01/" + ngayTotNghiepTS;
                                                ngay = Convert.ToDateTime(ngayTotNghiepTS);
                                            }
                                            catch (Exception ex)
                                            {
                                            }

                                            if (ngay != null)
                                            {
                                                if (ngay != DateTime.MinValue)
                                                {
                                                    bang.NamTotNghiep = ngay.Value.Year;
                                                    nhanVien.NhanVienTrinhDo.NamTotNghiep = ngay.Value.Year;
                                                }
                                                else
                                                    errorLog.AppendLine(" + Năm Tốt Nghiệp Tiến Sĩ không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                            }
                                            else
                                            {
                                                errorLog.AppendLine(" + Nắm tốt nghiệp Tiến Sĩ không đúng định dạng dd/MM/yyyy.");
                                            }
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                            //trình độ hiện tại cao nhất 97 không cần vì là quá trình lưu từ 79-96

                            #region Trinh độ tin học cột 98
                            {
                                String trinhDoTinHocText = dr[trinhDoTinHocIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(trinhDoTinHocText))
                                {
                                    TrinhDoTinHoc trinhDoTinHoc = uow.FindObject<TrinhDoTinHoc>(CriteriaOperator.Parse("TenTrinhDoTinHoc like ?", trinhDoTinHocText));
                                    if (trinhDoTinHoc == null)
                                    {
                                        trinhDoTinHoc = new TrinhDoTinHoc(uow);
                                        trinhDoTinHoc.TenTrinhDoTinHoc = trinhDoTinHocText;
                                        trinhDoTinHoc.MaQuanLy = Guid.NewGuid().ToString();
                                        trinhDoTinHoc.Save();
                                    }
                                    nhanVien.NhanVienTrinhDo.TrinhDoTinHoc = trinhDoTinHoc;
                                }
                            }
                            #endregion

                            #region Trình độ ngoại ngữ cột ngoại ngữ 99 - cấp độ ngoại ngữ 100
                            {
                                String trinhDoNgoaiNguText = dr[trinhDoNgoaiNguIdx].ToString().FullTrim();
                                String capDoNgoaiNguText = dr[capDoNgoaiNguIdx].ToString().FullTrim();
                                if (!string.IsNullOrEmpty(trinhDoNgoaiNguText))
                                {
                                    TrinhDoNgoaiNgu trinhDoNgoaiNgu = uow.FindObject<TrinhDoNgoaiNgu>(CriteriaOperator.Parse("MaQuanLy like ?", trinhDoNgoaiNguText));
                                    if (trinhDoNgoaiNgu == null)
                                    {
                                        trinhDoNgoaiNgu = new TrinhDoNgoaiNgu(uow);
                                        trinhDoNgoaiNgu.TenTrinhDoNgoaiNgu = capDoNgoaiNguText;
                                        trinhDoNgoaiNgu.MaQuanLy = trinhDoNgoaiNguText;
                                        trinhDoNgoaiNgu.Save();
                                    }
                                    nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = trinhDoNgoaiNgu;
                                }
                            }
                            #endregion

                            #region Học hàm cột 101
                            {
                                String hocHamText = dr[hocHamIdx].ToString().Trim();
                                if (!String.IsNullOrEmpty(hocHamText))
                                {
                                    HocHam hocHam = null;
                                    hocHam = uow.FindObject<HocHam>(CriteriaOperator.Parse("TenHocHam like ?", hocHamText));
                                    if (hocHam == null)
                                    {
                                        hocHam = new HocHam(uow);
                                        hocHam.TenHocHam = hocHamText;
                                        hocHam.MaQuanLy = hocHamText;
                                    }
                                    nhanVien.NhanVienTrinhDo.HocHam = hocHam;
                                }
                            }
                            #endregion

                            #region Ngày công nhận học hàm cột 102
                            {
                                String ngayString = dr[namCongNhanHocHamIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NhanVienTrinhDo.NamCongNhanHocHam = ngay.Value.Year;
                                        else
                                            errorLog.AppendLine(" + Năm Công Nhận học hàm không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Năm công nhận học hàm không đúng định dạng dd/MM/yyyy.");

                                    }
                                }
                                else
                                {
                                    //errorLog.AppendLine( " + Thiếu thông tin ngày sinh.");

                                }
                            }
                            #endregion

                            #region Danh Hiệu được phong tặng cột 103
                            {
                                String danhHieuPhongTangText = dr[danhHieuIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(danhHieuPhongTangText))
                                {
                                    DanhHieuDuocPhong danhHieu = null;
                                    danhHieu = uow.FindObject<DanhHieuDuocPhong>(CriteriaOperator.Parse("TenDanhHieu like ?", danhHieuPhongTangText));
                                    if (danhHieu == null)
                                    {
                                        danhHieu = new DanhHieuDuocPhong(uow);
                                        danhHieu.TenDanhHieu = danhHieuPhongTangText;
                                        danhHieu.MaQuanLy = danhHieuPhongTangText;
                                    }
                                    nhanVien.NhanVienTrinhDo.DanhHieuCaoNhat = danhHieu;

                                }
                            }
                            #endregion

                            #region Ngày công nhận học hàm cột 104
                            {
                                String ngayString = dr[ngayPhongTangDanhHieuIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(ngayString))
                                {
                                    DateTime? ngay = null;
                                    try
                                    {
                                        ngay = Convert.ToDateTime(ngayString);
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                    if (ngay != null)
                                    {
                                        if (ngay != DateTime.MinValue)
                                            nhanVien.NhanVienTrinhDo.NgayPhongDanhHieu = ngay.Value;
                                        else
                                            errorLog.AppendLine(" + Ngày phong tặng không không hợp lệ: " + ngay.Value.ToString("dd/MM/yyyy"));
                                    }
                                    else
                                    {
                                        errorLog.AppendLine(" + Ngày phong tặng không đúng định dạng dd/MM/yyyy.");

                                    }
                                }
                            }
                            #endregion
                            #endregion

                            #region Trình độ chính trị cột 105 --> 106
                            {
                                #region Trình độ lý luận chính trị
                                {
                                    String lyLuanChinhTriText = dr[trinhDoChinhTriIdx].ToString().FullTrim();
                                    if (!string.IsNullOrEmpty(lyLuanChinhTriText))
                                    {
                                        LyLuanChinhTri lyLuan = uow.FindObject<LyLuanChinhTri>(CriteriaOperator.Parse("TenLyLuanChinhTri like ?", lyLuanChinhTriText));
                                        if (lyLuan == null)
                                        {
                                            lyLuan = new LyLuanChinhTri(uow);
                                            lyLuan.TenLyLuanChinhTri = lyLuanChinhTriText;
                                            lyLuan.MaQuanLy = Guid.NewGuid().ToString();
                                            //lyLuan.Save();
                                        }
                                        nhanVien.NhanVienTrinhDo.LyLuanChinhTri = lyLuan;
                                    }
                                }
                                #endregion

                                #region Trình độ quản lý nhà nước
                                {
                                    String trinhDoQuanLyNhaNuocText = dr[trinhDoQuanLyNhaNuocIdx].ToString().FullTrim();
                                    if (!string.IsNullOrEmpty(trinhDoQuanLyNhaNuocText))
                                    {
                                        QuanLyNhaNuoc quanLyNhaNuoc = uow.FindObject<QuanLyNhaNuoc>(CriteriaOperator.Parse("TenQuanLyNhaNuoc like ?", trinhDoQuanLyNhaNuocText));
                                        if (quanLyNhaNuoc == null)
                                        {
                                            quanLyNhaNuoc = new QuanLyNhaNuoc(uow);
                                            quanLyNhaNuoc.TenQuanLyNhaNuoc = trinhDoQuanLyNhaNuocText;
                                            quanLyNhaNuoc.MaQuanLy = Guid.NewGuid().ToString();
                                        }
                                        nhanVien.NhanVienTrinhDo.QuanLyNhaNuoc = quanLyNhaNuoc;
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region Ðoàn viên cột 107-108
                            {
                                String ngayVaoDoanText = dr[ngayKetNapDoanTNIdx].ToString().Trim();
                                String chucVuDoanText = dr[chucVuDoanTNIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(ngayVaoDoanText))
                                {
                                    using (XPCollection<ToChucDoan> dsToChucDoan = new XPCollection<ToChucDoan>(uow, CriteriaOperator.Parse(""), new SortProperty("MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Descending)))
                                    {
                                        dsToChucDoan.TopReturnedObjects = 1;
                                        if (dsToChucDoan.Count == 1)
                                        {
                                            DoanVien doanVien = uow.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien.MaQuanLy like ?", nhanVien.MaQuanLy));
                                            if (doanVien == null)
                                            {
                                                doanVien = new DoanVien(uow);
                                                doanVien.BoPhan = nhanVien.BoPhan;
                                                doanVien.ThongTinNhanVien = nhanVien;
                                                doanVien.ToChucDoan = dsToChucDoan[0];
                                                try
                                                {
                                                    doanVien.NgayKetNap = Convert.ToDateTime(ngayVaoDoanText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày kết nạp không hợp lệ - không đúng định dạng dd/MM/yyyy: " + ngayVaoDoanText);
                                                }

                                                //chức vụ đoàn
                                                if (!string.IsNullOrEmpty(chucVuDoanText))
                                                {
                                                    ChucVuDoan chucVuDoan = null;
                                                    chucVuDoan = uow.FindObject<ChucVuDoan>(CriteriaOperator.Parse("TenChucVuDoan like ?", chucVuDoanText));
                                                    if (chucVuDoan == null)
                                                    {
                                                        //detailLog.AppendLine(" + Chức vụ Đoàn không hợp lệ: " + dr[38].ToString());
                                                        chucVuDoan = new ChucVuDoan(uow);
                                                        chucVuDoan.TenChucVuDoan = chucVuDoanText;
                                                        chucVuDoan.MaQuanLy = chucVuDoanText;
                                                    }
                                                    doanVien.ChucVuDoan = chucVuDoan;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Công đoàn cột 109-->111
                            {
                                String ngayVaoCongDoanText = dr[ngayVaoCongDoanIdx].ToString().Trim();
                                String chucVuCongDoanText = dr[chucVuCongDoanIdx].ToString().Trim();
                                String ngayBoNhiemChucVuCongDoanText = dr[ngayBoNhiemChucVuCongDoanIdx].ToString().Trim();
                                if (!String.IsNullOrWhiteSpace(ngayVaoCongDoanText))
                                {
                                    using (XPCollection<ToChucDoanThe> dsDoanThe = new XPCollection<ToChucDoanThe>(uow, CriteriaOperator.Parse(""), new SortProperty("MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Descending)))
                                    {
                                        dsDoanThe.TopReturnedObjects = 1;
                                        if (dsDoanThe.Count == 1)
                                        {
                                            DoanThe doanThe = uow.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien.MaQuanLy like ?", nhanVien.MaQuanLy));
                                            if (doanThe == null)
                                            {
                                                doanThe = new DoanThe(uow);
                                                doanThe.BoPhan = nhanVien.BoPhan;
                                                doanThe.ThongTinNhanVien = nhanVien;
                                                doanThe.ToChucDoanThe = dsDoanThe[0];
                                                try
                                                {
                                                    doanThe.NgayVaoCongDoan = Convert.ToDateTime(ngayVaoCongDoanText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày kết nạp không hợp lệ - không đúng định dạng dd/MM/yyyy: " + ngayVaoCongDoanText);
                                                }

                                                if (!String.IsNullOrWhiteSpace(chucVuCongDoanText))
                                                {
                                                    ChucVuDoanThe chucVuCongDoan = null;
                                                    chucVuCongDoan = uow.FindObject<ChucVuDoanThe>(CriteriaOperator.Parse("TenChucVuDoanThe like ?", chucVuCongDoanText));
                                                    if (chucVuCongDoan == null)
                                                    {
                                                        //  detailLog.AppendLine(" + Chức vụ Đoàn thể không hợp lệ: " + dr[39].ToString());
                                                        chucVuCongDoan = new ChucVuDoanThe(uow);
                                                        chucVuCongDoan.TenChucVuDoanThe = chucVuCongDoanText;
                                                        chucVuCongDoan.MaQuanLy = chucVuCongDoanText;
                                                    }
                                                    doanThe.ChucVuDoanThe = chucVuCongDoan;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Đảng cột 112-->114
                            {
                                String ngayVaoDangText = dr[ngayVaoDangIdx].ToString().Trim();
                                String ngayVaoDangCTText = dr[ngayVaoDangChinhThucIdx].ToString().Trim();
                                String chucVuDangText = dr[chucVuDangIdx].ToString().Trim();
                                if (!string.IsNullOrEmpty(ngayVaoDangText))
                                {
                                    //Chú ý file excel sau này phải có tổ chức đảng
                                    using (XPCollection<ToChucDang> dsDang = new XPCollection<ToChucDang>(uow, CriteriaOperator.Parse(""), new SortProperty("MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Descending)))
                                    {
                                        dsDang.TopReturnedObjects = 1;
                                        if (dsDang.Count == 1)
                                        {
                                            DangVien dang = uow.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien.MaQuanLy like ?", nhanVien.MaQuanLy));
                                            if (dang == null)
                                            {
                                                dang = new DangVien(uow);
                                                dang.BoPhan = nhanVien.BoPhan;
                                                dang.ThongTinNhanVien = nhanVien;
                                                dang.ToChucDang = dsDang[0];

                                                try
                                                {
                                                    dang.NgayDuBi = Convert.ToDateTime(ngayVaoDangText);
                                                }
                                                catch (Exception ex)
                                                {
                                                    errorLog.AppendLine(" + Ngày dự bị không hợp lệ - không đúng định dạng dd/MM/yyyy: " + ngayVaoDangText);
                                                }

                                                if (!String.IsNullOrEmpty(ngayVaoDangCTText))
                                                {
                                                    try
                                                    {
                                                        dang.NgayVaoDangChinhThuc = Convert.ToDateTime(ngayVaoDangCTText);
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        errorLog.AppendLine(" + Ngày vào đảng không hợp lệ - không đúng định dạng dd/MM/yyyy " + ngayVaoDangCTText);
                                                    }
                                                }

                                                if (!String.IsNullOrEmpty(chucVuDangText))
                                                {
                                                    ChucVuDang chucVuDang = null;
                                                    chucVuDang = uow.FindObject<ChucVuDang>(CriteriaOperator.Parse("TenChucVuDang like ?", chucVuDangText));
                                                    if (chucVuDang == null)
                                                    {
                                                        chucVuDang = new ChucVuDang(uow);
                                                        chucVuDang.TenChucVuDang = chucVuDangText;
                                                        chucVuDang.MaQuanLy = chucVuDangText;
                                                    }
                                                    dang.ChucVuDang = chucVuDang;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Tài khoản ngân hàng cột 115 --> 118
                            {
                                #region Tài khoản 1
                                {
                                    String nganHang1Text = dr[nganHangTK1Idx].ToString().Trim();
                                    String soTaiKhoan1 = dr[soTaiKhoanTK1Idx].ToString().Trim();

                                    if (!String.IsNullOrWhiteSpace(soTaiKhoan1))
                                    {
                                        TaiKhoanNganHang taiKhoanNganHang1 = new TaiKhoanNganHang(uow);
                                        taiKhoanNganHang1.NhanVien = nhanVien;
                                        taiKhoanNganHang1.TaiKhoanChinh = true;
                                        taiKhoanNganHang1.SoTaiKhoan = soTaiKhoan1;
                                        if (!String.IsNullOrWhiteSpace(nganHang1Text))
                                        {
                                            NganHang nganHang1 = null;
                                            nganHang1 = uow.FindObject<NganHang>(CriteriaOperator.Parse("TenNganHang like ?", nganHang1Text));
                                            if (nganHang1 == null)
                                            {
                                                //detailLog.AppendLine(" + Ngân hàng không hợp lệ (Lương kỳ 1):" + dr[88].ToString());
                                                nganHang1 = new NganHang(uow);
                                                nganHang1.TenNganHang = nganHang1Text;

                                            }
                                            taiKhoanNganHang1.NganHang = nganHang1;
                                        }
                                    }
                                }
                                #endregion

                                #region Tài khoản 2
                                {
                                    String nganHang2Text = dr[nganHangTK2Idx].ToString().Trim();
                                    String soTaiKhoan2 = dr[soTaiKhoanTK2Idx].ToString().Trim();
                                    if (!String.IsNullOrWhiteSpace(soTaiKhoan2))
                                    {
                                        TaiKhoanNganHang taiKhoanNganHang2 = new TaiKhoanNganHang(uow);
                                        taiKhoanNganHang2.NhanVien = nhanVien;
                                        taiKhoanNganHang2.TaiKhoanChinh = true;
                                        taiKhoanNganHang2.SoTaiKhoan = soTaiKhoan2;
                                        if (!String.IsNullOrWhiteSpace(nganHang2Text))
                                        {
                                            NganHang nganHang2 = null;
                                            nganHang2 = uow.FindObject<NganHang>(CriteriaOperator.Parse("TenNganHang like ?", nganHang2Text));
                                            if (nganHang2 == null)
                                            {
                                                //detailLog.AppendLine(" + Ngân hàng không hợp lệ (Lương kỳ 1):" + dr[88].ToString());
                                                nganHang2 = new NganHang(uow);
                                                nganHang2.TenNganHang = nganHang2Text;

                                            }
                                            taiKhoanNganHang2.NganHang = nganHang2;
                                        }
                                    }
                                }
                                #endregion
                            }
                            #endregion

                            #region bổ sung cột cho hồng bàng
                            {
                                //số người phụ thuộc
                                String soNguoiPhuThuocText = dr[soNguoiPhuThuocIdx].ToString();
                                int soNguoiPhuThuoc = 0;                              
                                if (!string.IsNullOrEmpty(soNguoiPhuThuocText))
                                {
                                    try
                                    {
                                        soNguoiPhuThuoc = Convert.ToInt32(soNguoiPhuThuocText);
                                    }
                                    catch (Exception ex)
                                    {
                                        errorLog.AppendLine(" + Số người phụ thuộc không hợp lệ." + soNguoiPhuThuocText);
                                    }
                                    nhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc = soNguoiPhuThuoc;
                                }
                            }
                            #endregion

                            #region Ghi File log
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (errorLog.Length > 0)
                                {
                                    mainLog.AppendLine("- STT: " + sttText);
                                    mainLog.AppendLine(string.Format("- Cán bộ Mã: {0} Tên: {1} không import vào phần mềm được: ", nhanVien.MaQuanLy, nhanVien.HoTen));
                                    mainLog.AppendLine(errorLog.ToString());

                                    //Thông báo lỗi
                                    oke = false;
                                }
                            }
                            #endregion

                            #endregion
                        }//end else
                        if(!doTatCa && oke)
                        {
                             uow.CommitChanges();
                             if (TruongConfig.MaTruong == "UFM")//Hưng thêm vào - Import UFM k có mã
                             {
                                 string sql = "UPDATE dbo.HoSo"
                                        + " SET MaQuanLy='" + maQuanLy + "'"
                                        + " WHERE GCRecord IS NULL"
                                        + " AND Oid='" + nhanVien.Oid + "'";
                                 DataProvider.ExecuteNonQuery(sql, CommandType.Text);
                             }
                             soNhanVienImportThanhCong++;
                        }
                        else
                        {
                            uow.RollbackTransaction();
                            soNhanVienImportLoi++;
                            oke = true;
                        }        
                }//end For
                if(doTatCa)
                {
                     if (mainLog.Length > 0)
                        {
                            uow.RollbackTransaction();
                            if (DialogUtil.ShowYesNo("Import Thành Công " + soNhanVienImportThanhCong + " Nhân viên - Số nhân viên Import không thành công " + soNhanVienImportLoi + ". Bạn có muốn xuất thông tin nhân viên lỗi") == DialogResult.Yes)
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
                            //hoàn tất giao tác
                            //transaction.Complete();
                        }
                }
                else
                {
                    string s = (soNhanVienImportLoi > 0 ? "Mời bạn xem file log" : "");
                    DialogUtil.ShowInfo("Import Thành Công " + soNhanVienImportThanhCong + " Nhân viên - Số nhân viên Import không thành công " + soNhanVienImportLoi + " " + s + "!");
                    //mở file log lỗi lên
                    if (soNhanVienImportLoi > 0)
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
             }  //end using (var uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))          
        }//end  using (DataTable dt = DataProvider.GetDataTable(filename, "[ThongTinNhanVien$A4:DM]"))
        return oke;
    }//end hàm Import
    }//end class
}
