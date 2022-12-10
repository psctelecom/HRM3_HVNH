using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_TroCap")]
    [ModelDefault("Caption", "Đề nghị hưởng chế độ ốm đau")]
    [DefaultProperty("ThongTinNhanVien")]
    public class TroCap_OmDau : TroCap
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
        [DataSourceCriteria("PhanLoai=1")]
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

        //tính số tiền ở đây
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

        public TroCap_OmDau(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            LoaiTroCap = "Trợ cấp ốm đau";
        }
		
        [Browsable(false)]
        public XPCollection<DieuKienTinhHuong> ListDieuKienTinhHuong { get; set; }

        private void UpdateDieuKienTinhHuong()
        {
            if (ListDieuKienTinhHuong == null)
                ListDieuKienTinhHuong = new XPCollection<DieuKienTinhHuong>(Session);

            CriteriaOperator filter;
            if (LyDoNghi.MaQuanLy == "01")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.BanThanOmNganNgay);
            else if (LyDoNghi.MaQuanLy == "02")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.BanThanOmDaiNgay);
            else if (LyDoNghi.MaQuanLy == "03")
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.ConOm);
            else
                filter = CriteriaOperator.Parse("PhanLoai=?", (int)DieuKienTinhHuongEnum.BanThanOmNganNgay);

            ListDieuKienTinhHuong.Criteria = filter;
        }

        protected override void AfterThongTinNhanVienChanged()
        {
            TinhLuyKe();
        }

        private void TinhLuyKe()
        {
            DateTime current = TuNgay == DateTime.MinValue ? HamDungChung.GetServerTime() : TuNgay;
            DateTime dauNam = current.SetTime(SetTimeEnum.StartYear);
            current = current.SetTime(SetTimeEnum.EndDay);

            CriteriaOperator co = CriteriaOperator.Parse("Oid!=? and ThongTinNhanVien=? and TuNgay>=? and DenNgay<=? and LyDoNghi=?",
                Oid, ThongTinNhanVien.Oid, dauNam, current, LyDoNghi.Oid);

            object luyKe = Session.Evaluate<TroCap_OmDau>(CriteriaOperator.Parse("SUM(SoNgayNghiTrongKy)"), co);
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
            DateTime tuNgay = new DateTime(current.Year, current.Month, 1).AddMonths(-1);
            DateTime denNgay = tuNgay.AddMonths(1).AddDays(-1);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay between (?,?)", ThongTinNhanVien, tuNgay, denNgay);
            SortProperty sort = new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Descending);
            XPCollection<DienBienLuong> dbl = new XPCollection<DienBienLuong>(Session, filter, sort);
            dbl.TopReturnedObjects = 1;
            DienBienLuong dienBienLuong;

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
                //lấy diễn biến lương ở trước đó nữa
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
            if (TuNgay != DateTime.MinValue && 
                DenNgay != DateTime.MinValue &&
                DieuKienTinhHuong != null)
            {
                switch (DieuKienTinhHuong.MaQuanLy)
                {
                    case "01"://bình thường (không tính cuối tuần, lễ, tết)
                    case "02"://nặng nhọc, độc hại (không tính cuối tuần, lễ, tết)
                    case "04"://con ốm < 3 tuổi (không tính cuối tuần, lễ, tết)
                    case "05"://con ốm dưới 7 tuổi (không tính cuối tuần, lễ, tết)
                        SoNgayNghiTrongKy = TuNgay.TinhSoNgay(DenNgay, Session);
                        break;
                    case "03"://mắc bệnh nan y (tính cả cuối tuần, lễ, tết)
                        SoNgayNghiTrongKy = TuNgay.TinhSoNgay(DenNgay);
                        break;
                }
            }
        }

        protected override void TinhSoTien()
        {
            int soNgayNghiToiDa = 0;
            decimal mucHuong = 0;
            decimal luongCoBan;
            ThongTinTruong truong = HamDungChung.ThongTinTruong(Session);
            if (truong != null && truong.ThongTinChung != null)
                luongCoBan = truong.ThongTinChung.LuongCoBan;
            else
                luongCoBan = 1150000;

            switch (DieuKienTinhHuong.MaQuanLy)
            {
                case "01"://bình thường
                    if (ThoiGianDongBaoHiem >= 360)//30 năm
                        soNgayNghiToiDa = 60;
                    else if (ThoiGianDongBaoHiem >= 180)//15 năm
                        soNgayNghiToiDa = 40;
                    else
                        soNgayNghiToiDa = 30;

                    mucHuong = 0.75m;
                    break;
                case "02"://nặng nhọc, độc hại
                    if (ThoiGianDongBaoHiem >= 360)
                        soNgayNghiToiDa = 70;
                    else if (ThoiGianDongBaoHiem >= 180)
                        soNgayNghiToiDa = 50;
                    else
                        soNgayNghiToiDa = 40;

                    mucHuong = 0.75m;
                    break;
                case "03"://mắc bệnh nan y
                    soNgayNghiToiDa = 180;
                    if (LuyKeTuDauNam < soNgayNghiToiDa)
                        mucHuong = 0.75m;
                    else
                    {
                        if (ThoiGianDongBaoHiem >= 360)
                            mucHuong = 0.65m;
                        else if (ThoiGianDongBaoHiem >= 180)
                            mucHuong = 0.55m;
                        else
                            mucHuong = 0.45m;
                    }
                    break;
                case "04"://con ốm < 3 tuổi
                    soNgayNghiToiDa = 20;
                    mucHuong = 0.75m;
                    break;
                case "05"://con ốm dưới 7 tuổi
                    soNgayNghiToiDa = 15;
                    mucHuong = 0.75m;
                    break;
            }

            //số ngày nghỉ chưa vượt quy định
            if (LuyKeTuDauNam <= soNgayNghiToiDa)
            {
                SoTien = (TienLuongTinhHuong * luongCoBan / 26) * mucHuong * SoNgayNghiTrongKy;
            }
            else//số ngày nghỉ vượt quy định
            {
                int t1 = LuyKeTuDauNam - SoNgayNghiTrongKy;
                int t2 = soNgayNghiToiDa - t1;
                if (t2 > 0)
                {
                    if (DieuKienTinhHuong.MaQuanLy == "03")//ốm dài ngày > 180 ngày
                    {
                        SoTien = (TienLuongTinhHuong * luongCoBan / 26) * 0.75m * t2;//số ngày chưa vượt quy định
                        SoTien += (TienLuongTinhHuong * luongCoBan / 26) * mucHuong * (SoNgayNghiTrongKy - t2);//số ngày đã vượt quy định

                        //nếu số tiền < lương cơ bản thì tính bằng lương cơ bản
                        if (SoTien < luongCoBan)
                            SoTien = luongCoBan;
                    }
                    else//ốm đau bình thường: chỉ được hưởng chế độ những ngày chưa vượt quy định
                    {
                        SoTien = (TienLuongTinhHuong * luongCoBan / 26) * 0.75m * t2;
                    }
                }
                else
                {
                    if (DieuKienTinhHuong.MaQuanLy == "03")
                    {
                        SoTien = (TienLuongTinhHuong * luongCoBan / 26) * mucHuong * SoNgayNghiTrongKy;//số ngày đã vượt quy định
                        
                        //nếu số tiền < lương cơ bản thì tính bằng lương cơ bản
                        if (SoTien < luongCoBan)
                            SoTien = luongCoBan;
                    }
                }
            }
        }
    }

}
