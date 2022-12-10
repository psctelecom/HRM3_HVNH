using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class HoSo_TaoHoSoNhanVienGiangVienCoHuuController : ViewController
    {
        public HoSo_TaoHoSoNhanVienGiangVienCoHuuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSo_TaoHoSoNhanVienGiangVienCoHuuController_Activated(object sender, EventArgs e)
        {           
             simpleAction.Active["TruyCap"] = (HamDungChung.IsWriteGranted<ThongTinNhanVien>());
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            GiangVienThinhGiang giangVienThinhGiang = null;

            giangVienThinhGiang = View.CurrentObject as GiangVienThinhGiang;

            if (giangVienThinhGiang != null)
            {
                if (DialogUtil.ShowYesNo(String.Format("Bạn thật sự muốn tạo hồ sơ cơ hữu cho cán bộ {0} - {1}", giangVienThinhGiang.MaQuanLy, giangVienThinhGiang.HoTen)) == System.Windows.Forms.DialogResult.Yes)
                {
                    if (!String.IsNullOrEmpty(giangVienThinhGiang.MaQuanLy))
                        XuLy(obs, giangVienThinhGiang);
                    //else
                    //    DialogUtil.ShowError(String.Format("Nhập số CMND của thỉnh giảng [{0}] trước khi tạo cơ hữu.", giangVienThinhGiang.HoTen));
                }
            }
        }

        private static void XuLy(IObjectSpace obs, GiangVienThinhGiang giangVienThinhGiang)
        {
            using (DialogUtil.AutoWait())
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //
                    ThongTinNhanVien cohuu = uow.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=?", giangVienThinhGiang.MaQuanLy));
                    if (cohuu == null)
                    {
                        try
                        {
                            cohuu = new ThongTinNhanVien(uow);

                            #region Thông tin cơ bản
                            cohuu.KhoaHoSo = false;
                            cohuu.OidHoSoCha = giangVienThinhGiang.Oid;
                            cohuu.MaQuanLy = giangVienThinhGiang.MaQuanLy;
                            cohuu.Ho = giangVienThinhGiang.Ho;
                            cohuu.Ten = giangVienThinhGiang.Ten;
                            cohuu.TenGoiKhac = giangVienThinhGiang.TenGoiKhac;
                            cohuu.GioiTinh = giangVienThinhGiang.GioiTinh;
                            cohuu.NgaySinh = giangVienThinhGiang.NgaySinh;
                            cohuu.NoiSinh = giangVienThinhGiang.NoiSinh != null ? uow.GetObjectByKey<DiaChi>(giangVienThinhGiang.NoiSinh.Oid) : null;
                            cohuu.CMND = giangVienThinhGiang.CMND;
                            cohuu.SoHoChieu = giangVienThinhGiang.SoHoChieu;
                            cohuu.NgayCap = giangVienThinhGiang.NgayCap;
                            cohuu.NoiCap = giangVienThinhGiang.NoiCap != null ? uow.GetObjectByKey<TinhThanh>(giangVienThinhGiang.NoiCap.Oid) : null;
                            cohuu.QuocTich = giangVienThinhGiang.QuocTich != null ? uow.GetObjectByKey<QuocGia>(giangVienThinhGiang.QuocTich.Oid) : null;
                            cohuu.QueQuan = giangVienThinhGiang.QueQuan != null ? uow.GetObjectByKey<DiaChi>(giangVienThinhGiang.QueQuan.Oid) : null;
                            cohuu.DiaChiThuongTru = giangVienThinhGiang.DiaChiThuongTru != null ? uow.GetObjectByKey<DiaChi>(giangVienThinhGiang.DiaChiThuongTru.Oid) : null;
                            cohuu.NoiOHienNay = giangVienThinhGiang.NoiOHienNay != null ? uow.GetObjectByKey<DiaChi>(giangVienThinhGiang.NoiOHienNay.Oid) : null;
                            cohuu.Email = giangVienThinhGiang.Email;
                            cohuu.DienThoaiDiDong = giangVienThinhGiang.DienThoaiDiDong;
                            cohuu.DienThoaiNhaRieng = giangVienThinhGiang.DienThoaiNhaRieng;
                            cohuu.TinhTrangHonNhan = giangVienThinhGiang.TinhTrangHonNhan != null ? uow.GetObjectByKey<TinhTrangHonNhan>(giangVienThinhGiang.TinhTrangHonNhan.Oid) : null;
                            cohuu.DanToc = giangVienThinhGiang.DanToc != null ? uow.GetObjectByKey<DanToc>(giangVienThinhGiang.DanToc.Oid) : null;
                            cohuu.TonGiao = giangVienThinhGiang.TonGiao != null ? uow.GetObjectByKey<TonGiao>(giangVienThinhGiang.TonGiao.Oid) : null;
                            cohuu.HinhThucTuyenDung = giangVienThinhGiang.HinhThucTuyenDung;
                            cohuu.GhiChu = giangVienThinhGiang.GhiChu;
                            cohuu.NgayVaoCoQuan = giangVienThinhGiang.NgayVaoCoQuan;
                            cohuu.LoaiHoSo = LoaiHoSoEnum.NhanVien;
                            if (giangVienThinhGiang.TaiBoMon != null)
                            {
                                cohuu.BoPhan = uow.GetObjectByKey<BoPhan>(giangVienThinhGiang.TaiBoMon.Oid);
                            }
                            else
                            {
                                cohuu.BoPhan = uow.GetObjectByKey<BoPhan>(giangVienThinhGiang.BoPhan.Oid);
                            }
                            cohuu.LoaiNhanSu = uow.FindObject<LoaiNhanSu>(CriteriaOperator.Parse("TenLoaiNhanSu like ?", "Giảng viên"));

                            //                           
                            //thinhGiang.CongViecHienNay = thongTinNhanVien.CongViecHienNay != null ? uow.GetObjectByKey<CongViec>(thongTinNhanVien.CongViecHienNay.Oid) : null;
                            cohuu.NgayTuyenDung = giangVienThinhGiang.NgayTuyenDung;
                            cohuu.DonViTuyenDung = giangVienThinhGiang.DonViTuyenDung;
                            cohuu.TinhTrang = uow.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang làm việc"));
                            #endregion

                            #region Nhân viên thông tin lương
                            if (giangVienThinhGiang.NhanVienThongTinLuong != null)
                            {
                                cohuu.NhanVienThongTinLuong.MaSoThue = giangVienThinhGiang.NhanVienThongTinLuong.MaSoThue;
                                cohuu.NhanVienThongTinLuong.TinhThueTNCNMacDinh = true;
                            }
                            #endregion

                            #region Nhân viên trình độ
                            if (giangVienThinhGiang.NhanVienTrinhDo != null)
                            {
                                cohuu.NhanVienTrinhDo.TrinhDoVanHoa = giangVienThinhGiang.NhanVienTrinhDo.TrinhDoVanHoa != null ? uow.GetObjectByKey<TrinhDoVanHoa>(giangVienThinhGiang.NhanVienTrinhDo.TrinhDoVanHoa.Oid) : null;
                                cohuu.NhanVienTrinhDo.TrinhDoChuyenMon = giangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon != null ? uow.GetObjectByKey<TrinhDoChuyenMon>(giangVienThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon.Oid) : null;
                                cohuu.NhanVienTrinhDo.ChuyenMonDaoTao = giangVienThinhGiang.NhanVienTrinhDo.ChuyenMonDaoTao != null ? uow.GetObjectByKey<ChuyenMonDaoTao>(giangVienThinhGiang.NhanVienTrinhDo.ChuyenMonDaoTao.Oid) : null;
                                cohuu.NhanVienTrinhDo.TruongDaoTao = giangVienThinhGiang.NhanVienTrinhDo.TruongDaoTao != null ? uow.GetObjectByKey<TruongDaoTao>(giangVienThinhGiang.NhanVienTrinhDo.TruongDaoTao.Oid) : null;
                                cohuu.NhanVienTrinhDo.HinhThucDaoTao = giangVienThinhGiang.NhanVienTrinhDo.HinhThucDaoTao != null ? uow.GetObjectByKey<HinhThucDaoTao>(giangVienThinhGiang.NhanVienTrinhDo.HinhThucDaoTao.Oid) : null;
                                cohuu.NhanVienTrinhDo.NamTotNghiep = giangVienThinhGiang.NhanVienTrinhDo.NamTotNghiep;
                                cohuu.NhanVienTrinhDo.HocHam = giangVienThinhGiang.NhanVienTrinhDo.HocHam != null ? uow.GetObjectByKey<HocHam>(giangVienThinhGiang.NhanVienTrinhDo.HocHam.Oid) : null;
                                cohuu.NhanVienTrinhDo.TrinhDoTinHoc = giangVienThinhGiang.NhanVienTrinhDo.TrinhDoTinHoc != null ? uow.GetObjectByKey<TrinhDoTinHoc>(giangVienThinhGiang.NhanVienTrinhDo.TrinhDoTinHoc.Oid) : null;
                                cohuu.NhanVienTrinhDo.NgoaiNgu = giangVienThinhGiang.NhanVienTrinhDo.NgoaiNgu != null ? uow.GetObjectByKey<NgoaiNgu>(giangVienThinhGiang.NhanVienTrinhDo.NgoaiNgu.Oid) : null;
                                cohuu.NhanVienTrinhDo.TrinhDoNgoaiNgu = giangVienThinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu != null ? uow.GetObjectByKey<TrinhDoNgoaiNgu>(giangVienThinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu.Oid) : null;
                            }
                            #endregion

                            #region Tài khoản ngân hàng
                            foreach (TaiKhoanNganHang item in giangVienThinhGiang.ListTaiKhoanNganHang)
                            {
                                TaiKhoanNganHang taiKhoan = new TaiKhoanNganHang(uow);
                                taiKhoan.NhanVien = cohuu;
                                taiKhoan.SoTaiKhoan = item.SoTaiKhoan;
                                taiKhoan.TaiKhoanChinh = item.TaiKhoanChinh;
                                taiKhoan.NganHang = item.NganHang != null ? uow.GetObjectByKey<NganHang>(item.NganHang.Oid) : null;
                                //
                                cohuu.ListTaiKhoanNganHang.Add(taiKhoan);
                            }
                            #endregion

                            #region Văn bằng
                            using (XPCollection<VanBang> vanBangList = new DevExpress.Xpo.XPCollection<VanBang>(uow, CriteriaOperator.Parse("HoSo=?", giangVienThinhGiang.Oid)))
                            {
                                foreach (VanBang item in vanBangList)
                                {
                                    if (item.TrinhDoChuyenMon != null)
                                    {
                                        VanBang vanBang = new VanBang(uow);
                                        vanBang.HoSo = cohuu;
                                        vanBang.TrinhDoChuyenMon = item.TrinhDoChuyenMon != null ? uow.GetObjectByKey<TrinhDoChuyenMon>(item.TrinhDoChuyenMon.Oid) : null;
                                        vanBang.ChuyenMonDaoTao = item.ChuyenMonDaoTao != null ? uow.GetObjectByKey<ChuyenMonDaoTao>(item.ChuyenMonDaoTao.Oid) : null;
                                        vanBang.TruongDaoTao = item.TruongDaoTao != null ? uow.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid) : null;
                                        vanBang.HinhThucDaoTao = item.HinhThucDaoTao != null ? uow.GetObjectByKey<HinhThucDaoTao>(item.HinhThucDaoTao.Oid) : null;
                                        vanBang.DiemTrungBinh = item.DiemTrungBinh;
                                        vanBang.XepLoai = item.XepLoai;
                                        vanBang.NamTotNghiep = item.NamTotNghiep;
                                        cohuu.ListVanBang.Add(vanBang);
                                    }
                                }
                            }
                            #endregion

                            #region Chứng chỉ
                            using (XPCollection<ChungChi> chungChiList = new XPCollection<ChungChi>(uow, CriteriaOperator.Parse("HoSo=?", giangVienThinhGiang.Oid)))
                            {
                                foreach (ChungChi item in chungChiList)
                                {
                                    if (item.LoaiChungChi != null)
                                    {
                                        ChungChi chungChi = new ChungChi(uow);
                                        chungChi.HoSo = cohuu;
                                        chungChi.TenChungChi = item.TenChungChi;
                                        chungChi.XepLoai = item.XepLoai;
                                        chungChi.NoiCap = item.NoiCap;
                                        chungChi.NgayCap = item.NgayCap;
                                        chungChi.Diem = item.Diem;
                                        chungChi.LoaiChungChi = item.LoaiChungChi != null ? uow.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid) : null;
                                        chungChi.LoaiChungChi = item.LoaiChungChi != null ? uow.GetObjectByKey<LoaiChungChi>(item.LoaiChungChi.Oid) : null;
                                        cohuu.ListChungChi.Add(chungChi);
                                    }
                                }
                            }
                            #endregion

                            #region Trình độ ngoại ngữ khác
                            using (XPCollection<TrinhDoNgoaiNguKhac> trinhDoNgoaiNguKhacList = new XPCollection<TrinhDoNgoaiNguKhac>(uow, CriteriaOperator.Parse("HoSo=?", giangVienThinhGiang.Oid)))
                            {
                                foreach (TrinhDoNgoaiNguKhac item in trinhDoNgoaiNguKhacList)
                                {
                                    if (item.NgoaiNgu != null && item.TrinhDoNgoaiNgu != null)
                                    {
                                        TrinhDoNgoaiNguKhac trinhDoNgoaiNguKhac = new TrinhDoNgoaiNguKhac(uow);
                                        trinhDoNgoaiNguKhac.HoSo = cohuu;
                                        trinhDoNgoaiNguKhac.NgoaiNgu = item.NgoaiNgu != null ? uow.GetObjectByKey<NgoaiNgu>(item.NgoaiNgu.Oid) : null;
                                        trinhDoNgoaiNguKhac.TrinhDoNgoaiNgu = item.TrinhDoNgoaiNgu != null ? uow.GetObjectByKey<TrinhDoNgoaiNgu>(item.TrinhDoNgoaiNgu.Oid) : null;
                                        trinhDoNgoaiNguKhac.Diem = item.Diem;
                                        cohuu.ListNgoaiNgu.Add(trinhDoNgoaiNguKhac);
                                    }
                                }
                            }
                            #endregion       
                            //Tiến hành lưu dữ liệu
                            uow.CommitChanges();


                            string create = "UPDATE dbo.NhanVien SET TinhTrang = '6A9D2F11-0AEB-4C50-A23F-DA353B0207BE' WHERE Oid = '" + giangVienThinhGiang.Oid +"'";
                            DataProvider.ExecuteNonQuery(create, CommandType.Text);


                            //
                            DialogUtil.ShowInfo(String.Format("Đã tạo hồ sơ cơ hữu cho thỉnh giảng {0} thành công. Vui lòng điều chỉnh và nhập bổ sung thông tin cần thiết trong hồ sơ cơ hữu!", giangVienThinhGiang.HoTen));
                        }
                        catch (Exception ex)
                        {
                            //
                            uow.RollbackTransaction();
                            //
                            DialogUtil.ShowError(String.Format("Không thể tạo hồ sơ cơ hữu của thỉnh giảng [{0}]. Vì {1}", giangVienThinhGiang.HoTen, ex.Message));
                        }
                    }
                    else
                    {
                        DialogUtil.ShowError(String.Format("Hồ sơ của thỉnh giảng [{0}] đã tồn tại trong cơ hữu.", giangVienThinhGiang.HoTen));
                    }
                }
            }
        }

    }
}
