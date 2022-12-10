using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_TroCap")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Đề nghị hưởng trợ cấp nghỉ DS-PHSK sau tai nạn lao động")]
    [Appearance("TroCap_PHSKSauTNLD", TargetItems = "BenhVien", Enabled = false, Criteria = "NghiTapTrung=0")]
    public class TroCap_PHSKSauTNLD : TroCap
    {
        // Fields...
        private int _LuyKeTuDauNam;
        private int _NghiTapTrung;
        private int _NghiTaiGiaDinh;
        private BenhVien _BenhVien;
        private int _MucDoSuyGiamSLD;

        [ModelDefault("Caption", "Mức độ suy giảm SLĐ (%)")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleRange("", DefaultContexts.Save, 1, 100)]
        public int MucDoSuyGiamSLD
        {
            get
            {
                return _MucDoSuyGiamSLD;
            }
            set
            {
                SetPropertyValue("MucDoSuyGiamSLD", ref _MucDoSuyGiamSLD, value);
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
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "NghiTapTrung>0")]
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

        public TroCap_PHSKSauTNLD(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            LoaiTroCap = "Trợ cấp phục hồi sức khỏe sau tai nạn lao động";
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

            object luyKe = Session.Evaluate<TroCap_PHSKSauTNLD>(CriteriaOperator.Parse("SUM(LuyKeTuDauNam)"), go);
            if (luyKe != null)
                LuyKeTuDauNam = (int)luyKe;
            else
                LuyKeTuDauNam = 0;
            LuyKeTuDauNam += NghiTaiGiaDinh + NghiTapTrung;
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
            if (LuyKeTuDauNam <= soNgayNghiToiDa)
            {                
                if (NghiTaiGiaDinh > 0)
                    tongSoTien += luongCoBan * NghiTaiGiaDinh * 0.25m;
                if (NghiTapTrung > 0)
                    tongSoTien += luongCoBan * NghiTapTrung * 0.4m;
            }
            else
            {
                //uu tien tinh nghi tap trung, khi nao con thua thi chuyen sang tinh nghi tai gia dinh
                soNgayNghi = NghiTapTrung <= (soNgayNghiToiDa - (LuyKeTuDauNam - NghiTapTrung)) ? NghiTapTrung : (soNgayNghiToiDa - (LuyKeTuDauNam - NghiTapTrung));
                if (soNgayNghi > 0)
                    tongSoTien += 0.4m * luongCoBan * soNgayNghi;
                soNgayNghi = NghiTapTrung <= (soNgayNghiToiDa - (LuyKeTuDauNam - NghiTapTrung - NghiTaiGiaDinh)) ? NghiTapTrung : (soNgayNghiToiDa - (LuyKeTuDauNam - NghiTapTrung - NghiTaiGiaDinh));
                if (soNgayNghi > 0)
                    tongSoTien += 0.25m * luongCoBan * soNgayNghi;
            }

            SoTien = tongSoTien;
        }
    }

}
