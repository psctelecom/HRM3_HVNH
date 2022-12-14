using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.NangLuong
{
    [NonPersistent]
    [ImageName("BO_NangLuong")]
    [ModelDefault("Caption", "Danh sách nâng lương sớm")]
    public class DanhSachNangLuongSom : BaseObject
    {
        private bool _ChonTatCa;
        private DateTime _TinhDenNgay;

        [ImmediatePostData]
        [ModelDefault("Caption", "Chọn tất cả")]
        public bool ChonTatCa
        {
            get
            {
                return _ChonTatCa;
            }
            set
            {
                SetPropertyValue("ChonTatCa", ref _ChonTatCa, value);
                if (!IsLoading)
                {
                    foreach (NangLuongSom item in DanhSachNhanVien)
                    {
                        if (item.Chon != ChonTatCa)
                            item.Chon = ChonTatCa;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Tính đến ngày")]
        [ImmediatePostData]
        public DateTime TinhDenNgay
        {
            get
            {
                return _TinhDenNgay;
            }
            set
            {
                SetPropertyValue("TinhDenNgay", ref _TinhDenNgay, value);
            }
        }

        [ModelDefault("Caption", "Lưu ý")]
        public string LuuY
        {
            get
            {
                object obj = Session.Evaluate<ThongTinNhanVien>(CriteriaOperator.Parse("COUNT()"),
                    CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong=?", "False"));
                if (obj != null)
                {
                    int soLuong = (int)obj;
                    int toiDa = (int)Math.Floor(soLuong * 10m / 100);
                    return String.Format("Chỉ được xét nâng lương cho tối đa {0} cán bộ. (10% tổng số cán bộ)", toiDa);
                }
                return "Chỉ được lấy 10% trên tổng số cán bộ để xét nâng lương sớm";
            }
        }

        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<NangLuongSom> DanhSachNhanVien { get; set; }

        public DanhSachNangLuongSom(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            DanhSachNhanVien = new XPCollection<NangLuongSom>(Session, false);
            TinhDenNgay = HamDungChung.GetServerTime();
        }

        public void Refresh()
        {
            GroupOperator go = new GroupOperator() { OperatorType = GroupOperatorType.And };
            CriteriaOperator filter1 = CriteriaOperator.Parse("NhanVienThongTinLuong.MocNangLuong is not null and NhanVienThongTinLuong.MocNangLuongLanSau>? and (NhanVienThongTinLuong.NgachLuong.MaQuanLy like ? or NhanVienThongTinLuong.NgachLuong.MaQuanLy like ? or NhanVienThongTinLuong.NgachLuong.MaQuanLy like ?  or NhanVienThongTinLuong.NgachLuong.MaQuanLy like ?) and NhanVienThongTinLuong.VuotKhung=0",
                TinhDenNgay, "15%", "01%", "13%", "V.%");
            InOperator filter2 = new InOperator("BoPhan.Oid", HamDungChung.GetCriteriaBoPhan());
            CriteriaOperator filter3 = CriteriaOperator.Parse("TinhTrang.KhongConCongTacTaiTruong=?", "False");
            go.Operands.Add(filter1);
            go.Operands.Add(filter2);
            go.Operands.Add(filter3);

            XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, go);
            DanhSachNhanVien.Reload();
            NangLuongSom nangLuong;
            int soThang, demKhenThuong = 0;
            string danhHieu, danhHieu1;

            foreach (ThongTinNhanVien item in nvList)
            {
                //điều kiện 1: Lần nâng lương trước cách thời điểm hiện tại >= 2 năm
                soThang = HamDungChung.TinhSoThang(item.NhanVienThongTinLuong.MocNangLuong, TinhDenNgay);
                if (soThang >= 24)
                {
                    nangLuong = new NangLuongSom(Session);
                    nangLuong.ThongTinNhanVien = item;
                    if (item.NgayVaoCoQuan != DateTime.MinValue)
                    {
                        DateTime temp = new DateTime(TinhDenNgay.Year, 12, 31);
                        nangLuong.SoNamCongTac = HamDungChung.TinhSoNam(item.NgayVaoCoQuan, temp);

                        //tròn tháng, tròn ngày
                        if (item.NgayVaoCoQuan.Month != 1 || (item.NgayVaoCoQuan.Month == 1 && item.NgayVaoCoQuan.Day != 1))
                            nangLuong.SoNamCongTac--;
                    }

                    //tính vượt khung
                    int bac = 0;
                    if (nangLuong.BacLuongCu != null && int.TryParse(nangLuong.BacLuongCu.TenBacLuong, out bac))
                    {
                        //chi lay bac luong moi thoi
                        //bac luong cu chi danh de nhap du lieu cu
                        bac++;
                        BacLuong bacLuong = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and TenBacLuong=? and (BacLuongCu is null or !BacLuongCu)", nangLuong.NgachLuong.Oid, bac.ToString()));
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

                    //điều kiện 2: được khen thưởng
                    //tìm khen thưởng từ mốc nâng lương tới thời điểm hiện tại
                    #region
                    filter1 = CriteriaOperator.Parse("ThongTinNhanVien=? and QuyetDinhKhenThuong.NgayHieuLuc>=? and QuyetDinhKhenThuong.NgayHieuLuc<=?",
                        item.Oid, item.NhanVienThongTinLuong.MocNangLuong, TinhDenNgay);
                    SortProperty sort = new SortProperty("QuyetDinhKhenThuong.DanhHieuKhenThuong.MaQuanLy", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    XPCollection<ChiTietKhenThuongNhanVien> khenThuongList = new XPCollection<ChiTietKhenThuongNhanVien>(Session, filter1, sort);
                    if (khenThuongList.Count > 0)
                    {

                        //tạo ghi chú
                        danhHieu = khenThuongList[0].QuyetDinhKhenThuong.DanhHieuKhenThuong != null ?
                            khenThuongList[0].QuyetDinhKhenThuong.DanhHieuKhenThuong.TenDanhHieu : "";
                        foreach (ChiTietKhenThuongNhanVien ktItem in khenThuongList)
                        {
                            danhHieu1 = ktItem.QuyetDinhKhenThuong.DanhHieuKhenThuong != null ?
                                ktItem.QuyetDinhKhenThuong.DanhHieuKhenThuong.TenDanhHieu :"";
                            if (danhHieu == danhHieu1)
                            {
                                demKhenThuong++;
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(danhHieu))
                                {
                                    nangLuong.GhiChu += String.Format("{0}({1} lần); ", danhHieu, demKhenThuong);
                                    demKhenThuong = 1;
                                    danhHieu = danhHieu1;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(danhHieu))
                            nangLuong.GhiChu += String.Format("{0}({1} lần)", danhHieu, demKhenThuong);
                    }
                    #endregion

                    DanhSachNhanVien.Add(nangLuong);
                }
            }
        }
    }

}
