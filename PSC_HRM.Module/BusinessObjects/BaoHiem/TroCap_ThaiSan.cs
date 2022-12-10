using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using System.Collections.Generic;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_TroCap")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Đề nghị hưởng chế độ thai sản")]
    [Appearance("TroCap_ThaiSan", TargetItems = "DenNgay", Enabled = false, Criteria = "LyDoNghi.MaQuanLy == '04'")]
    public class TroCap_ThaiSan : TroCap
    {
        // Fields...
        private LyDoNghi _LyDoNghi;
        private int _LuyKeTuDauNam;
        private int _SoNgayNghiTrongKy;
        private decimal _TienLuongTinhHuong;
        private DieuKienTinhHuong _DieuKienTinhHuong;

        [ImmediatePostData]
        [ModelDefault("Caption", "Lý do nghỉ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("PhanLoai=0")]
        public LyDoNghi LyDoNghi
        {
            get
            {
                return _LyDoNghi;
            }
            set
            {
                SetPropertyValue("LyDoNghi", ref _LyDoNghi, value);
                if (!IsLoading && value != null)
                {
                    TinhLuyKe();
                    UpdateDieuKienTinhHuong();
                    DieuKienTinhHuong = null;
                }
            }
        }

        [ModelDefault("Caption", "Điều kiện tính hưởng")]
        [DataSourceProperty("ListDieuKienTinhHuong")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DieuKienTinhHuong DieuKienTinhHuong
        {
            get
            {
                return _DieuKienTinhHuong;
            }
            set
            {
                SetPropertyValue("DieuKienTinhHuong", ref _DieuKienTinhHuong, value);
            }
        }

        [ModelDefault("Caption", "Tiền lương tính hưởng")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal TienLuongTinhHuong
        {
            get
            {
                return _TienLuongTinhHuong;
            }
            set
            {
                SetPropertyValue("TienLuongTinhHuong", ref _TienLuongTinhHuong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày nghỉ trong kỳ")]
        public int SoNgayNghiTrongKy
        {
            get
            {
                return _SoNgayNghiTrongKy;
            }
            set
            {
                SetPropertyValue("SoNgayNghiTrongKy", ref _SoNgayNghiTrongKy, value);
                if (!IsLoading)
                {
                    TinhLuyKe();
                    TinhSoTien();
                }
            }
        }

        [ModelDefault("Caption", "Lũy kế từ đầu năm")]
        public int LuyKeTuDauNam
        {
            get
            {
                return _LuyKeTuDauNam;
            }
            set
            {
                SetPropertyValue("LuyKeTuDauNam", ref _LuyKeTuDauNam, value);
            }
        }

        public TroCap_ThaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            LoaiTroCap = "Trợ cấp thai sản";
        }
        [Browsable(false)]
        public XPCollection<DieuKienTinhHuong> ListDieuKienTinhHuong { get; set; }

        private void UpdateDieuKienTinhHuong()
        {
            if (ListDieuKienTinhHuong == null)
                ListDieuKienTinhHuong = new XPCollection<DieuKienTinhHuong>(Session);

            CriteriaOperator filter;
            if (LyDoNghi.MaQuanLy == "04")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.KhamThai);
            else if (LyDoNghi.MaQuanLy == "05")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.SayThai);
            else if (LyDoNghi.MaQuanLy == "06")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.SinhCon);
            else if (LyDoNghi.MaQuanLy == "07")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.TranhThai);
            else
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.KhamThai);

            ListDieuKienTinhHuong.Criteria = filter;
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            TinhLuyKe();
        }

        private void TinhLuyKe()
        {
            DateTime current = TuNgay == DateTime.MinValue ? HamDungChung.GetServerTime() : TuNgay;
            DateTime dauNam = new DateTime(current.Year, 1, 1);

            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(CriteriaOperator.Parse("Oid!=?", Oid));
            go.Operands.Add(CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien));
            go.Operands.Add(new BetweenOperator("TuNgay", dauNam, current));

            object luyKe = Session.Evaluate<TroCap_ThaiSan>(CriteriaOperator.Parse("SUM(LuyKeTuDauNam)"), go);
            if (luyKe != null)
                LuyKeTuDauNam = (int)luyKe;
            else
                LuyKeTuDauNam = 0;
            LuyKeTuDauNam += SoNgayNghiTrongKy;
        }

        protected override void TinhMucLuongHuongBHXH()
        {
            //4 thông tin cần kiểm tra là hệ số lương, PCCV, TNVK, TNGD, PCK lấy từ diễn biến lương
            DateTime current = HamDungChung.GetServerTime();
            DateTime tuNgay = new DateTime(current.Year, current.Month, 1).AddMonths(-6);
            DateTime denNgay = tuNgay.AddMonths(7).AddDays(-1);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay between (?,?)", ThongTinNhanVien, tuNgay, denNgay);
            SortProperty sort = new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Descending);
            XPCollection<DienBienLuong> dbl = new XPCollection<DienBienLuong>(Session, filter, sort);
            DienBienLuong dienBienLuong;

            if (dbl.Count > 0)
            {
                //kiểm tra xem có diễn biến lương ở tháng đầu tiên trong 6 tháng không?
                //nếu có (sướng nhất) làm tiếp cái bên dưới (:D)
                //nếu không có phải tìm cái diễn biến lương gần nhất với cái tháng đầu tiên đó.
                dienBienLuong = dbl[0];
                if (dienBienLuong.TuNgay.Month != tuNgay.Month)
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay<?", tuNgay);
                    sort = new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Descending);
                    XPCollection<DienBienLuong> dbl1 = new XPCollection<DienBienLuong>(Session, filter, sort);
                    dbl1.TopReturnedObjects = 1;
                    if (dbl1.Count == 1)
                        dbl.Add(dbl1[0]);
                }

                List<decimal> tienLuong6Thang = new List<decimal>();
                int month = denNgay.Month;
                foreach (DienBienLuong item in dbl)
                {
                    if (item.TuNgay.Month != month)
                    {
                        for (int i = 0; i < month - item.TuNgay.Month; i++)
                        {
                            if (tienLuong6Thang.Count == 6)
                                break;
                            if (item.Huong85PhanTramLuong)
                                tienLuong6Thang.Add((item.HeSoLuong +
                                                    item.HSPCChucVu +
                                                    item.HSPCKhac +
                                                    item.VuotKhung * item.HeSoLuong / 100 +
                                                    item.ThamNien * item.HeSoLuong / 100) * 0.85m);
                            else
                                tienLuong6Thang.Add(item.HeSoLuong +
                                    item.HSPCChucVu +
                                    item.HSPCKhac +
                                    item.VuotKhung * item.HeSoLuong / 100 +
                                    item.ThamNien * item.HeSoLuong / 100);
                        }
                        month = item.TuNgay.Month;
                    }
                }

                decimal temp = 0;
                foreach (decimal item in tienLuong6Thang)
                {
                    temp += item;
                }

                if (temp > 0)
					TienLuongTinhHuong = temp / 6;
                else
                {
                    TienLuongTinhHuong = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;

                    if (ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong)
                        TienLuongTinhHuong *= 0.85m;
                }
            }
            else
            {
                //nếu không có thì tìm diễn biến lương cũ hơn
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay<?", ThongTinNhanVien, tuNgay);
                dbl = new XPCollection<DienBienLuong>(Session, filter, sort);
                dbl.TopReturnedObjects = 1;
                if (dbl.Count == 1)
                {
                    dienBienLuong = dbl[0];
                    if (dienBienLuong != null)
                    {
                        TienLuongTinhHuong = dienBienLuong.HeSoLuong +
                            dienBienLuong.HSPCChucVu +
                            dienBienLuong.HSPCKhac +
                            dienBienLuong.VuotKhung * dienBienLuong.HeSoLuong / 100 +
                            dienBienLuong.ThamNien * dienBienLuong.HeSoLuong / 100;
                        
                        if (dienBienLuong.Huong85PhanTramLuong)
                            TienLuongTinhHuong *= 0.85m;
                    }
                }
                else
                {
                    //nếu không có thì lấy thông tin lương hiện tại của nhân viên
                    TienLuongTinhHuong = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien +
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCVuotKhung;

                    if (ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong)
                        TienLuongTinhHuong *= 0.85m;
                }
            }
        }

        protected override void TinhSoNgayNghi()
        {
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
            {
                switch (DieuKienTinhHuong.MaQuanLy)
                {
                    case "06"://khám thai (bình thường) (không tính cuối tuần, lễ, tết)
                    case "07"://khám thai (nơi khám xa, mẹ có bệnh lý, thai bất thường) (không tính cuối tuần, lễ, tết)
                        SoNgayNghiTrongKy = TuNgay.TinhSoNgay(DenNgay, Session);
                        break;
                    case "08"://sẩy thai, nạo hút thai, thai chết lưu < 1 tháng (tính cả cuối tuần, lễ, tết)
                    case "09"://sẩy thai, nạo hút thai, thai chết lưu < 3 tháng (tính cả cuối tuần, lễ, tết)
                    case "10"://sẩy thai, nạo hút thai, thai chết lưu < 6 tháng (tính cả cuối tuần, lễ, tết)
                    case "11"://sẩy thai, nạo hút thai, thai chết lưu >= 6 tháng (tính cả cuối tuần, lễ, tết)
                    case "12"://sinh con (bình thường) (tính cả cuối tuần, lễ, tết)
                    case "13"://sinh con (độc hại, nặng nhọc) (tính cả cuối tuần, lễ, tết)
                    case "14"://sinh con (mẹ tàn tật) (tính cả cuối tuần, lễ, tết)
                    case "15"://sinh đôi (tính cả cuối tuần, lễ, tết)
                    case "16"://sinh ba (tính cả cuối tuần, lễ, tết)
                    case "17"://nhận con nuôi (tính cả cuối tuần, lễ, tết)
                    case "18"://đặt vòng tránh thai (tính cả cuối tuần, lễ, tết)
                    case "19"://triệt sản (tính cả cuối tuần, lễ, tết)
                        SoNgayNghiTrongKy = TuNgay.TinhSoNgay(DenNgay);
                        break;
                }
            }
        }

        protected override void TinhSoTien()
        {
            int soNgayNghiToiDa = 0;
            decimal luongCoBan;

            ThongTinTruong truong = HamDungChung.ThongTinTruong(Session);
            if (truong != null && truong.ThongTinChung != null)
                luongCoBan = truong.ThongTinChung.LuongCoBan;
            else
                luongCoBan = 1050000;

            switch (DieuKienTinhHuong.MaQuanLy)
            {
                case "06"://khám thai (bình thường)
                    soNgayNghiToiDa = 5;
                    break;
                case "07"://khám thai bất thường
                    soNgayNghiToiDa = 10;
                    break;
                case "08"://sẩy thai < 1 tháng
                    soNgayNghiToiDa = 10;
                    break;
                case "09"://sẩy thai < 3 tháng
                    soNgayNghiToiDa = 20;
                    break;
                case "10"://sẩy thai < 6 tháng
                    soNgayNghiToiDa = 40;
                    break;
                case "11"://sẩy thai >= 6 tháng
                    soNgayNghiToiDa = 60;
                    break;
                case "12"://sinh con bình thường
                    soNgayNghiToiDa = 120;
                    break;
                case "13"://sinh con (nặng nhọc, độc hại)
                    soNgayNghiToiDa = 150;
                    break;
                case "14"://sinh con (mẹ tàn tật)
                    soNgayNghiToiDa = 180;
                    break;
                case "15"://sinh đôi
                    soNgayNghiToiDa = 150;
                    break;
                case "16"://sinh ba
                    soNgayNghiToiDa = 180;
                    break;
                case "17"://nhận con nuôi
                    soNgayNghiToiDa = 120;
                    break;
                case "18"://đặt vòng tránh thai
                    soNgayNghiToiDa = 7;
                    break;
                case "19"://triệt sản
                    soNgayNghiToiDa = 15;
                    break;
            }

            //số ngày nghỉ chưa vượt quy định
            if (LuyKeTuDauNam <= soNgayNghiToiDa)
            {
                SoTien = (TienLuongTinhHuong * luongCoBan / 26) * SoNgayNghiTrongKy;
            }
            else//số ngày nghỉ vượt quy định
            {
                int t1 = LuyKeTuDauNam - SoNgayNghiTrongKy;
                int t2 = soNgayNghiToiDa - t1;
                if (t2 > 0)
                {
                    SoTien = (TienLuongTinhHuong * luongCoBan / 26) * t2;
                }
            }
        }
    }

}
