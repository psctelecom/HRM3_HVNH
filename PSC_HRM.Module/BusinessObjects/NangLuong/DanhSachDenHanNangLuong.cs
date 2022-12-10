using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.QuyetDinh;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace PSC_HRM.Module.NangLuong
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách đến hạn nâng lương")]
    [Appearance("DanhSachDenHanNangLuong_Hide", TargetItems = "NangTruocHan;NangThuongXuyen", Visibility = ViewItemVisibility.Hide,Criteria = "MaTruong <> 'UEL'")]
    public class DanhSachDenHanNangLuong : BaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<DenHanNangLuong> DanhSachNhanVien { get; set; }


        [NonPersistent]
        [Browsable(false)]
        private string MaTruong { get; set; }

        public DanhSachDenHanNangLuong(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DanhSachNhanVien = new XPCollection<DenHanNangLuong>(Session, false);
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
            //
            MaTruong = TruongConfig.MaTruong;
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue &&
                TuNgay < DenNgay)
            {
                GroupOperator go = new GroupOperator() { OperatorType = GroupOperatorType.And };
                CriteriaOperator filter1 = CriteriaOperator.Parse("NhanVienThongTinLuong.MocNangLuongLanSau>=? and NhanVienThongTinLuong.MocNangLuongLanSau<=?",
                                                                   HamDungChung.SetTime(TuNgay, 0), HamDungChung.SetTime(DenNgay, 1));
                InOperator filter2 = new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session));
                CriteriaOperator filter3;

                if (TruongConfig.MaTruong.Equals("UEL") || TruongConfig.MaTruong.Equals("QNU"))
                {
                    filter3 = CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong=0");
                }
                else
                {


                    filter3 = CriteriaOperator.Parse("TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ? or TinhTrang.TenTinhTrang like ?",
                    "%có hưởng lương%", "%đang làm việc%", "%nghỉ bhxh%", "%thai sản%","%hưởng 40% lương%", "%công tác%lương%");
                }

                go.Operands.Add(filter1);
                go.Operands.Add(filter2);
                go.Operands.Add(filter3);

                XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, go);

                DanhSachNhanVien.Reload();
                DenHanNangLuong nangLuong;
                foreach (ThongTinNhanVien item in nvList)
                {
                    nangLuong = new DenHanNangLuong(Session)
                    {
                        ThongTinNhanVien = item,
                    };
                    nangLuong.Ho = item.Ho;
                    nangLuong.Ten = item.Ten;
                    //
                    if (item.NhanVienThongTinLuong.VuotKhung == 0)
                    {
                        int bac = 0;
                        if (nangLuong.BacLuongCu != null && int.TryParse(nangLuong.BacLuongCu.MaQuanLy, out bac))
                        {
                            //chi lay bac luong moi thoi
                            //bac luong cu chi danh de nhap du lieu cu
                            bac++;
                            BacLuong bacLuong = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and MaQuanLy=? and (BacLuongCu is null or !BacLuongCu)", nangLuong.NgachLuong.Oid, bac.ToString()));
                            if (bacLuong != null)
                            {
                                nangLuong.BacLuongMoi = bacLuong;
                                nangLuong.HeSoLuongMoi = bacLuong.HeSoLuong;
                                nangLuong.PhanTramVuotKhungMoi = 0;
                            }
                            else
                            {
                                nangLuong.BacLuongMoi = nangLuong.BacLuongCu;
                                nangLuong.HeSoLuongMoi = nangLuong.HeSoLuongCu;
                                nangLuong.PhanTramVuotKhungMoi = 5;
                            }
                        }
                    }
                    else
                    {
                        nangLuong.BacLuongMoi = nangLuong.BacLuongCu;
                        nangLuong.HeSoLuongMoi = nangLuong.HeSoLuongCu;
                        nangLuong.PhanTramVuotKhungMoi = item.NhanVienThongTinLuong.VuotKhung + 1;
                    }
                    if (nangLuong.PhanTramVuotKhungCu <= 0)
                    {
                        if (item.NhanVienThongTinLuong.NgachLuong != null)
                            nangLuong.MocNangLuongMoi = nangLuong.NgayHuongLuongMoi = item.NhanVienThongTinLuong.MocNangLuongDieuChinh == DateTime.MinValue ? item.NhanVienThongTinLuong.MocNangLuong.AddMonths(item.NhanVienThongTinLuong.NgachLuong.ThoiGianNangBac) : item.NhanVienThongTinLuong.MocNangLuongDieuChinh.AddMonths(item.NhanVienThongTinLuong.NgachLuong.ThoiGianNangBac);
                        else
                            nangLuong.MocNangLuongMoi = nangLuong.NgayHuongLuongMoi = nangLuong.MocNangLuongDieuChinh == DateTime.MinValue ? nangLuong.MocNangLuongCu.AddMonths(36) : nangLuong.MocNangLuongDieuChinh.AddMonths(36);
                    }
                    else
                        nangLuong.MocNangLuongMoi = nangLuong.NgayHuongLuongMoi = nangLuong.MocNangLuongCu.AddMonths(12);

                    nangLuong.PhanLoai = NangLuongEnum.ThuongXuyen;
                    nangLuong.MaQuanLy = item.MaQuanLy;

                    if (TruongConfig.MaTruong.Equals("IUH"))
                    {
                        DateTime ngayHetHanDaoTao = DateTime.MinValue;
                        //Tìm xem người đó có trong quyết định đi học hay không?
                        QuyetDinhDaoTao quyetDinhDaoTao = Session.FindObject<QuyetDinhDaoTao>(CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", item.Oid));
                        if (quyetDinhDaoTao != null)
                        {
                            QuyetDinhGiaHanDaoTao quyetDinhGiaHanDaoTao = Session.FindObject<QuyetDinhGiaHanDaoTao>(CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinhDaoTao.Oid=?", item.Oid, quyetDinhDaoTao.Oid));
                            if (quyetDinhGiaHanDaoTao != null)
                            {
                                ngayHetHanDaoTao = quyetDinhGiaHanDaoTao.DenNgay;
                            }
                            else
                            {
                                ngayHetHanDaoTao = quyetDinhDaoTao.DenNgay;
                            }
                        }

                        if (ngayHetHanDaoTao!= DateTime.MinValue && ngayHetHanDaoTao <= HamDungChung.GetServerTime())
                        {
                            //Đối với những người đi học nước ngoài nếu chưa nộp báo cáo thì ghi chú lại.
                            CriteriaOperator filter = CriteriaOperator.Parse("BoPhan=? and ThongTinNhanVien=?", item.BoPhan, item.Oid);
                            ChiTietDangKyNopBaoCao chiTietDangKyNopBaoCao = Session.FindObject<ChiTietDangKyNopBaoCao>(filter);
                            if (chiTietDangKyNopBaoCao != null)
                            {
                                nangLuong.GhiChu = "Đã nộp báo cáo.";
                            }
                            else
                            {
                                nangLuong.GhiChu = "Đến hạn nộp báo cáo nhưng chưa hộp.";
                            }
                        }
                    }
                    
                    DanhSachNhanVien.Add(nangLuong);
                }
            }
        }
    }
}
