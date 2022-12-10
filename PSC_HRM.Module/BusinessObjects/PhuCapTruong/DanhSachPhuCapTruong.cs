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

namespace PSC_HRM.Module.PhuCapTruong
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách phụ cấp trường")]
    public class DanhSachPhuCapTruong : BaseObject
    {
        private bool _HienThiTatCa;
        private bool _ChonTatCa;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

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
                    AfterCheck();
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Hiển thị tất cả cán bộ đang hưởng phụ cấp (trường)")]
        public bool HienThiTatCa
        {
            get
            {
                return _HienThiTatCa;
            }
            set
            {
                SetPropertyValue("HienThiTatCa", ref _HienThiTatCa, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<PhuCapTruong> ListTrachNhiem { get; set; }

        public DanhSachPhuCapTruong(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();


            ListTrachNhiem = new XPCollection<PhuCapTruong>(Session, false);
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
        }
        private void AfterCheck()
        {
            foreach (PhuCapTruong item in ListTrachNhiem)
            {
                if (item.Chon != ChonTatCa)
                    item.Chon = ChonTatCa;
            }
        }

        public bool IsExist(ThongTinNhanVien nhanVien)
        {
            foreach (PhuCapTruong item in ListTrachNhiem)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Load danh sách phụ cấp
        /// </summary>
        public void LoadDuLieu()
        {
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang like ?", "%Đang làm việc%"));
            if (!HienThiTatCa)
            {
                go.Operands.Add(CriteriaOperator.Parse("NhanVienThongTinLuong.DuocHuongHSPCChuyenVien"));
                go.Operands.Add(CriteriaOperator.Parse("NgayVaoCoQuan is not null"));
                //kiểm tra hợp đồng khoán
                //go.Operands.Add(CriteriaOperator.Parse("HopDongLaoDong.LoaiDieuKhoanHopDong!=1"));
                //tạm thời bỏ dòng này, sau này ổn sẽ chạy lại
                //go.Operands.Add(CriteriaOperator.Parse("(HopDongLaoDongList[DenNgay>=?] or HopDongLaoDongList[DenNgay>=? and DenNgay<=?])", DenNgay, TuNgay, DenNgay));
            }
            else
            {
                go.Operands.Add(CriteriaOperator.Parse("NhanVienThongTinLuong.DuocHuongHSPCChuyenVien"));
            }

            XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(Session, go);

            ListTrachNhiem.Reload();
            PhuCapTruong trachNhiem;
            int nam, thangHSPCTrachNhiem, thangHSPCThamNien, thang1;
            CriteriaOperator filter;
            foreach (ThongTinNhanVien item in nvList)
            {
                if (HienThiTatCa)
                {
                    if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                        thangHSPCTrachNhiem = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.TinhSoThang(DenNgay);
                    else
                        thangHSPCTrachNhiem = item.NgayVaoCoQuan.TinhSoThang(DenNgay);
                    thangHSPCThamNien = item.NgayVaoCoQuan.TinhSoThang(DenNgay);
                    nam = thangHSPCThamNien / 12;
                    thang1 = thangHSPCThamNien % 12;

                    if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue &&
                        item.NhanVienThongTinLuong.HSPCTrachNhiem > 0)
                        filter = CriteriaOperator.Parse("TuThang<=? and DenThang>=? and HSPCTrachNhiem>?",
                            thangHSPCTrachNhiem, thangHSPCTrachNhiem, item.NhanVienThongTinLuong.HSPCTrachNhiem);
                    else
                        filter = CriteriaOperator.Parse("TuThang<=? and DenThang>=?",
                            thangHSPCTrachNhiem, thangHSPCTrachNhiem);
                    HeSoPhuCapTrachNhiem HSPCTrachNhiem = Session.FindObject<HeSoPhuCapTrachNhiem>(filter);
                    HeSoPhuCapThamNien HSPCThamNien = Session.FindObject<HeSoPhuCapThamNien>(CriteriaOperator.Parse("TuNam<=? and DenNam>?", nam, nam));
                    trachNhiem = new PhuCapTruong(Session);
                    trachNhiem.BoPhan = item.BoPhan;
                    trachNhiem.ThongTinNhanVien = item;
                    trachNhiem.NgayVaoCoQuan = item.NgayVaoCoQuan;
                    trachNhiem.ThoiGianCongTac = ThoiGianCongTac(nam, thang1);
                    trachNhiem.HSPCLanhDao = item.NhanVienThongTinLuong.HSPCLanhDao;
                    trachNhiem.HSPCKiemNhiem = item.NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;
                    trachNhiem.HSPCChuyenMon = item.NhanVienThongTinLuong.HSPCChuyenMon;
                    trachNhiem.HSPCTrachNhiemCu = item.NhanVienThongTinLuong.HSPCTrachNhiem;
                    trachNhiem.HSPCThamNienCu = item.NhanVienThongTinLuong.HSPCThamNienTrongTruong;
                    //trachNhiem.NgayHuongHSPCThamNien = item.NhanVienThongTinLuong.NgayHuongHSPCThamNien;
                    //trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem;
                    trachNhiem.HSPCKhac = item.NhanVienThongTinLuong.HSPCKhac;

                    //tham nien
                    if (HSPCThamNien != null && trachNhiem.HSPCThamNienCu != HSPCThamNien.HSPCThamNien)
                    {
                        trachNhiem.HSPCThamNien = HSPCThamNien.HSPCThamNien;
                        trachNhiem.NgayHuongHSPCThamNien = item.NgayVaoCoQuan.AddYears(nam);
                    }
                    else
                    {
                        trachNhiem.HSPCThamNien = trachNhiem.HSPCThamNienCu;
                        trachNhiem.NgayHuongHSPCThamNien = item.NhanVienThongTinLuong.NgayHuongHSPCThamNien;
                    }

                    //trach nhiem
                    if (HSPCTrachNhiem != null && trachNhiem.HSPCTrachNhiemCu != HSPCTrachNhiem.HSPCTrachNhiem)
                    {
                        trachNhiem.HSPCTrachNhiem = HSPCTrachNhiem.HSPCTrachNhiem;
                        if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem == DateTime.MinValue &&
                                item.NgayVaoCoQuan != DateTime.MinValue)
                            trachNhiem.NgayHuong = item.NgayVaoCoQuan.AddMonths(HSPCTrachNhiem.TuThang);
                        else if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                            trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.AddMonths(HSPCTrachNhiem.TuThang);

                        //CriteriaOperator filter = CriteriaOperator.Parse("HSPCTrachNhiem<?", HSPCTrachNhiem.HSPCTrachNhiem);
                        //SortProperty sort = new SortProperty("HSPCTrachNhiem", DevExpress.Xpo.DB.SortingDirection.Descending);
                        //using (XPCollection<HeSoPhuCapTrachNhiem> pcTrachNhiemList = new XPCollection<HeSoPhuCapTrachNhiem>(Session, filter, sort))
                        //{
                        //    pcTrachNhiemList.TopReturnedObjects = 1;

                        //    if (pcTrachNhiemList.Count == 0 &&
                        //        item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem == DateTime.MinValue &&
                        //        item.NgayVaoCoQuan != DateTime.MinValue)
                        //        trachNhiem.NgayHuong = item.NgayVaoCoQuan.AddMonths(HSPCTrachNhiem.TuThang);
                        //    else if (pcTrachNhiemList.Count == 1 && item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                        //        trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.AddMonths(HSPCTrachNhiem.TuThang - pcTrachNhiemList[0].TuThang);
                        //}
                    }
                    else
                    {
                        trachNhiem.HSPCTrachNhiem = trachNhiem.HSPCTrachNhiemCu;
                        trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem;
                    }

                    //chuyen mon nếu mới vào ngành giáo dục được 6 tháng
                    //object hsTrachNhiem = Session.Evaluate<HeSoPhuCapTrachNhiem>(CriteriaOperator.Parse("Min(HSPCTrachNhiem)"),
                    //    CriteriaOperator.Parse(""));
                    //if (hsTrachNhiem != null)
                    //{
                    HeSoChuyenMon hsChuyenMon = Session.FindObject<HeSoChuyenMon>(CriteriaOperator.Parse("TrinhDoChuyenMon=?", item.NhanVienTrinhDo.TrinhDoChuyenMon));
                    if (hsChuyenMon != null && hsChuyenMon.HSPCChuyenMon > trachNhiem.HSPCChuyenMon)
                    {
                        trachNhiem.HSPCChuyenMon = hsChuyenMon.HSPCChuyenMon;
                        trachNhiem.NgayHuongHSPCChuyenMon = trachNhiem.NgayHuong;
                    }
                    else
                        trachNhiem.NgayHuongHSPCChuyenMon = item.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon;

                    ListTrachNhiem.Add(trachNhiem);
                }
                else
                {
                    if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                        thangHSPCTrachNhiem = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.TinhSoThang(DenNgay);
                    else
                        thangHSPCTrachNhiem = item.NgayVaoCoQuan.TinhSoThang(DenNgay);
                    thangHSPCThamNien = item.NgayVaoCoQuan.TinhSoThang(DenNgay);
                    nam = thangHSPCThamNien / 12;
                    thang1 = thangHSPCThamNien % 12;

                    if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue &&
                        item.NhanVienThongTinLuong.HSPCTrachNhiem > 0)
                        filter = CriteriaOperator.Parse("TuThang<=? and DenThang>=? and HSPCTrachNhiem>?",
                            thangHSPCTrachNhiem, thangHSPCTrachNhiem, item.NhanVienThongTinLuong.HSPCTrachNhiem);
                    else
                        filter = CriteriaOperator.Parse("TuThang<=? and DenThang>=?",
                            thangHSPCTrachNhiem, thangHSPCTrachNhiem);
                    HeSoPhuCapTrachNhiem HSPCTrachNhiem = Session.FindObject<HeSoPhuCapTrachNhiem>(filter);
                    HeSoPhuCapThamNien HSPCThamNien = Session.FindObject<HeSoPhuCapThamNien>(CriteriaOperator.Parse("TuNam<=? and DenNam>?", nam, nam));
                    trachNhiem = new PhuCapTruong(Session);
                    trachNhiem.BoPhan = item.BoPhan;
                    trachNhiem.ThongTinNhanVien = item;
                    trachNhiem.NgayVaoCoQuan = item.NgayVaoCoQuan;
                    trachNhiem.ThoiGianCongTac = ThoiGianCongTac(nam, thang1);
                    trachNhiem.HSPCLanhDao = item.NhanVienThongTinLuong.HSPCLanhDao;
                    trachNhiem.HSPCKiemNhiem = item.NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;
                    trachNhiem.HSPCChuyenMon = item.NhanVienThongTinLuong.HSPCChuyenMon;
                    trachNhiem.HSPCTrachNhiemCu = item.NhanVienThongTinLuong.HSPCTrachNhiem;
                    trachNhiem.HSPCThamNienCu = item.NhanVienThongTinLuong.HSPCThamNienTrongTruong;
                    //trachNhiem.NgayHuongHSPCThamNien = item.NhanVienThongTinLuong.NgayHuongHSPCThamNien;
                    //trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem;
                    trachNhiem.HSPCKhac = item.NhanVienThongTinLuong.HSPCKhac;

                    if (HSPCThamNien != null && trachNhiem.HSPCThamNienCu != HSPCThamNien.HSPCThamNien)
                    {
                        trachNhiem.HSPCThamNien = HSPCThamNien.HSPCThamNien;
                        trachNhiem.NgayHuongHSPCThamNien = item.NgayVaoCoQuan.AddYears(nam);
                    }
                    else
                        trachNhiem.HSPCThamNien = trachNhiem.HSPCThamNienCu;
                    if (HSPCTrachNhiem != null && trachNhiem.HSPCTrachNhiemCu != HSPCTrachNhiem.HSPCTrachNhiem)
                    {
                        trachNhiem.HSPCTrachNhiem = HSPCTrachNhiem.HSPCTrachNhiem;
                        if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem == DateTime.MinValue &&
                                item.NgayVaoCoQuan != DateTime.MinValue)
                            trachNhiem.NgayHuong = item.NgayVaoCoQuan.AddMonths(HSPCTrachNhiem.TuThang);
                        else if (item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                            trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.AddMonths(HSPCTrachNhiem.TuThang);
                        //filter = CriteriaOperator.Parse("HSPCTrachNhiem<?", HSPCTrachNhiem.HSPCTrachNhiem);
                        //SortProperty sort = new SortProperty("HSPCTrachNhiem", DevExpress.Xpo.DB.SortingDirection.Descending);
                        //using (XPCollection<HeSoPhuCapTrachNhiem> pcTrachNhiemList = new XPCollection<HeSoPhuCapTrachNhiem>(Session, filter, sort))
                        //{
                        //    pcTrachNhiemList.TopReturnedObjects = 1;

                        //    if (pcTrachNhiemList.Count == 0 &&
                        //        item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem == DateTime.MinValue &&
                        //        item.NgayVaoCoQuan != DateTime.MinValue)
                        //        trachNhiem.NgayHuong = item.NgayVaoCoQuan.AddMonths(HSPCTrachNhiem.TuThang);
                        //    else if (pcTrachNhiemList.Count == 1 && item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                        //        trachNhiem.NgayHuong = item.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.AddMonths(HSPCTrachNhiem.TuThang - pcTrachNhiemList[0].TuThang);
                        //}
                    }
                    else
                        trachNhiem.HSPCTrachNhiem = trachNhiem.HSPCTrachNhiemCu;

                    ////chuyen mon nếu mới vào ngành giáo dục được 6 tháng
                    //object hsTrachNhiem = Session.Evaluate<HeSoPhuCapTrachNhiem>(CriteriaOperator.Parse("Min(HSPCTrachNhiem)"),
                    //    CriteriaOperator.Parse(""));
                    //if (hsTrachNhiem != null)
                    //{
                    HeSoChuyenMon hsChuyenMon = Session.FindObject<HeSoChuyenMon>(CriteriaOperator.Parse("TrinhDoChuyenMon=?", item.NhanVienTrinhDo.TrinhDoChuyenMon));
                    if (hsChuyenMon != null && hsChuyenMon.HSPCChuyenMon > trachNhiem.HSPCChuyenMon)
                    {
                        trachNhiem.HSPCChuyenMon = hsChuyenMon.HSPCChuyenMon;
                        trachNhiem.NgayHuongHSPCChuyenMon = trachNhiem.NgayHuong;
                    }
                    else
                        trachNhiem.NgayHuongHSPCChuyenMon = item.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon;

                    //chỉ lấy những người có hspc trách nhiệm < 2
                    if ((HSPCThamNien != null && HSPCThamNien.HSPCThamNien != trachNhiem.HSPCThamNienCu) ||
                        (HSPCTrachNhiem != null && HSPCTrachNhiem.HSPCTrachNhiem != trachNhiem.HSPCTrachNhiemCu) ||
                        (trachNhiem.NgayHuongHSPCChuyenMon >= TuNgay && trachNhiem.NgayHuongHSPCChuyenMon <= DenNgay && trachNhiem.HSPCThamNien >= trachNhiem.HSPCThamNienCu && trachNhiem.HSPCTrachNhiem >= trachNhiem.HSPCTrachNhiemCu))
                        ListTrachNhiem.Add(trachNhiem);
                }

            }
        }

        /// <summary>
        /// Cập nhật thông tin phụ cấp
        /// </summary>
        public void XuLy()
        {
            foreach (PhuCapTruong item in ListTrachNhiem)
            {
                if (item.Chon)
                {
                    if (item.NgayHuong != DateTime.MinValue)
                        item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem = item.NgayHuong;
                    if (item.NgayHuongHSPCThamNien != DateTime.MinValue)
                        item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCThamNien = item.NgayHuongHSPCThamNien;
                    if (item.NgayHuongHSPCChuyenMon != DateTime.MinValue)
                        item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon = item.NgayHuongHSPCChuyenMon;
                    item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = item.HSPCTrachNhiem;
                    item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong = item.HSPCThamNien;
                    item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon = item.HSPCChuyenMon;
                    item.ThongTinNhanVien.Save();
                }
            }
        }

        public string ThoiGianCongTac(int nam, int thang)
        {
            if (nam > 0 && thang == 0)
                return nam + " năm";
            if (nam == 0 && thang > 0)
                return thang + " tháng";
            return string.Format("{0} năm {1} tháng", nam, thang);
        }
        
    }

}
