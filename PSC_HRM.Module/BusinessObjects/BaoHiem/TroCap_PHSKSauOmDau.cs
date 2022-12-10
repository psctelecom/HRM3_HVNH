using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    //trường hợp con ốm:
    // - lý do nghỉ: con ốm
    // - điều kiện tính hưởng: con dưới 3 tuổi; con từ 3 đến 7 tuổi
    [ImageName("BO_TroCap")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Đề nghị hưởng trợ cấp nghỉ DS-PHSK sau ốm đau")]
    [Appearance("TroCap_PHSKSauOmDau", TargetItems = "BenhVien", Enabled = false, Criteria = "NghiTapTrung=0")]
    public class TroCap_PHSKSauOmDau : TroCap
    {
        // Fields...
        private BenhVien _BenhVien;
        private int _NghiTapTrung;
        private int _NghiTaiGiaDinh;
        private int _LuyKeDauNam;
        private DieuKienTinhHuong _DieuKienTinhHuong;

        [ModelDefault("Caption", "Điều kiện tính hưởng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("PhanLoai=7")]
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

        [ModelDefault("Caption", "Lũy kế từ đầu năm")]
        public int LuyKeDauNam
        {
            get
            {
                return _LuyKeDauNam;
            }
            set
            {
                SetPropertyValue("LuyKeDauNam", ref _LuyKeDauNam, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nghỉ tại gia đình")]
        public int NghiTaiGiaDinh
        {
            get
            {
                return _NghiTaiGiaDinh;
            }
            set
            {
                SetPropertyValue("NghiTaiGiaDinh", ref _NghiTaiGiaDinh, value);
                if (!IsLoading)
                {
                    TinhLuyKe();
                    TinhSoTien();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nghỉ tập trung")]
        public int NghiTapTrung
        {
            get
            {
                return _NghiTapTrung;
            }
            set
            {
                SetPropertyValue("NghiTapTrung", ref _NghiTapTrung, value);
                if (!IsLoading)
                {
                    TinhLuyKe();
                    TinhSoTien();
                }
            }
        }

        [ModelDefault("Caption", "Bệnh viện")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria="NghiTapTrung>0")]
        public BenhVien BenhVien
        {
            get
            {
                return _BenhVien;
            }
            set
            {
                SetPropertyValue("BenhVien", ref _BenhVien, value);
            }
        }

        public TroCap_PHSKSauOmDau(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiTroCap = "Trợ cấp phục hồi sức khỏe sau ốm đau";
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

            object luyKe = Session.Evaluate<TroCap_PHSKSauOmDau>(CriteriaOperator.Parse("SUM(LuyKeDauNam)"), go);
            if (luyKe != null)
                LuyKeDauNam = (int)luyKe;
            else
                LuyKeDauNam = 0;
            LuyKeDauNam += NghiTaiGiaDinh + NghiTapTrung;
        }

        protected override void TinhSoNgayNghi()
        {
            NghiTaiGiaDinh = TuNgay.TinhSoNgay(DenNgay);
        }

        protected override void TinhSoTien()
        {
            decimal luongCoBan;
            decimal tongSoTien = 0;
            const int soNgayNghiToiDa = 10;
            int soNgayNghi;

            if (QuanLyTroCap.ThongTinTruong != null && QuanLyTroCap.ThongTinTruong.ThongTinChung != null)
                luongCoBan = QuanLyTroCap.ThongTinTruong.ThongTinChung.LuongCoBan;
            else
            {
                ThongTinTruong truong = HamDungChung.ThongTinTruong(Session);
                if (truong != null && truong.ThongTinChung != null)
                    luongCoBan = truong.ThongTinChung.LuongCoBan;
                else
                    luongCoBan = 1050000; //luong co ban moi nhat 1.050.000 NVD
            }

            //hiện tại luật cho phép nghỉ từ 5->10 ngày nên chỉ tính trợ cấp trong khoảng thời gian này
            if (LuyKeDauNam <= soNgayNghiToiDa)
            {
                if (NghiTaiGiaDinh > 0)
                    tongSoTien += 0.25m * luongCoBan * NghiTaiGiaDinh;
                if (NghiTapTrung > 0)
                    tongSoTien += 0.4m * luongCoBan * NghiTapTrung;
            }
            else
            {
                //uu tien tinh nghi tap trung, khi nao con thua thi chuyen sang tinh nghi tai gia dinh
                soNgayNghi = NghiTapTrung <= (soNgayNghiToiDa - (LuyKeDauNam - NghiTapTrung)) ? NghiTapTrung : (soNgayNghiToiDa - (LuyKeDauNam - NghiTapTrung));
                if (soNgayNghi > 0)
                    tongSoTien += 0.4m * luongCoBan * soNgayNghi;
                soNgayNghi = NghiTapTrung <= (soNgayNghiToiDa - (LuyKeDauNam - NghiTapTrung - NghiTaiGiaDinh)) ? NghiTapTrung : (soNgayNghiToiDa - (LuyKeDauNam - NghiTapTrung - NghiTaiGiaDinh));
                if (soNgayNghi > 0)
                    tongSoTien += 0.25m * luongCoBan * soNgayNghi;
            }

            SoTien = tongSoTien;
        }
    }

}
