using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình hợp đồng")]
    [Appearance("CauHinhHopDong", TargetItems = "SoBatDau;MauSoHopDongLienKet;MauSoHopDongThinhGiang;MauSoHopDongKhoan;MauSoHopDongLaoDong;MauSoHopDongLamViec;MauSoHopDongCoVanHocTap;MauSoHopDongChuNhiem;MauSoHopDongChamBaoCao;", Enabled = false, Criteria = "!TuDongTaoSoHopDong")]
    public class CauHinhHopDong : BaseObject
    {
        // Fields...
        private int _SoBatDau;
        private string _MauSoHopDongThanhLyThinhGiang;
        private string _MauSoHopDongThanhLyThinhGiangChatLuongCao;
        private string _MauSoHopDongThinhGiang;
        private string _MauSoHopDongKhoan;
        private string _MauSoHopDongLaoDong;
        private string _MauSoHopDongLamViec;
        private bool _TuDongTaoSoHopDong = true;
        private string _MauSoHopDongCoVanHocTap;
        private string _MauSoHopDongChamBaoCao;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số hợp đồng")]
        public bool TuDongTaoSoHopDong
        {
            get
            {
                return _TuDongTaoSoHopDong;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHopDong", ref _TuDongTaoSoHopDong, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public int SoBatDau
        {
            get
            {
                return _SoBatDau;
            }
            set
            {
                SetPropertyValue("SoBatDau", ref _SoBatDau, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng làm việc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongLamViec
        {
            get
            {
                return _MauSoHopDongLamViec;
            }
            set
            {
                SetPropertyValue("MauSoHopDongLamViec", ref _MauSoHopDongLamViec, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongLaoDong
        {
            get
            {
                return _MauSoHopDongLaoDong;
            }
            set
            {
                SetPropertyValue("MauSoHopDongLaoDong", ref _MauSoHopDongLaoDong, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng khoán")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongKhoan
        {
            get
            {
                return _MauSoHopDongKhoan;
            }
            set
            {
                SetPropertyValue("MauSoHopDongKhoan", ref _MauSoHopDongKhoan, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng thỉnh giảng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongThinhGiang
        {
            get
            {
                return _MauSoHopDongThinhGiang;
            }
            set
            {
                SetPropertyValue("MauSoHopDongThinhGiang", ref _MauSoHopDongThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng thanh lý thỉnh giảng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongThanhLyThinhGiang
        {
            get
            {
                return _MauSoHopDongThanhLyThinhGiang;
            }
            set
            {
                SetPropertyValue("MauSoHopDongThanhLyThinhGiang", ref _MauSoHopDongThanhLyThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số HĐ cố vấn học tập")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongCoVanHocTap
        {
            get
            {
                return _MauSoHopDongCoVanHocTap;
            }
            set
            {
                SetPropertyValue("MauSoHopDongCoVanHocTap", ref _MauSoHopDongCoVanHocTap, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hợp đồng thanh lý thỉnh giảng chất lượng cao")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongThanhLyThinhGiangChatLuongCao
        {
            get
            {
                return _MauSoHopDongThanhLyThinhGiangChatLuongCao;
            }
            set
            {
                SetPropertyValue("MauSoHopDongThanhLyThinhGiangChatLuongCao", ref _MauSoHopDongThanhLyThinhGiangChatLuongCao, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số HĐ chấm báo cáo")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDong")]
        public string MauSoHopDongChamBaoCao
        {
            get
            {
                return _MauSoHopDongChamBaoCao;
            }
            set
            {
                SetPropertyValue("MauSoHopDongChamBaoCao", ref _MauSoHopDongChamBaoCao, value);
            }
        }

        public CauHinhHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            SoBatDau = 1;
            MauSoHopDongChamBaoCao = "{00#}/HĐCBC-ĐHL-TCCB";
            MauSoHopDongCoVanHocTap = "{00#}/HĐCVHT-ĐHL-TCCB";
            MauSoHopDongKhoan = "{00#}/HĐK-ĐHL-TCCB";
            MauSoHopDongLamViec = "{00#}/HĐLV-ĐHL-TCCB";
            MauSoHopDongLaoDong = "{00#}/HĐLĐ-ĐHL-TCCB";
            MauSoHopDongThanhLyThinhGiang = "{00#}/HĐTLTG-ĐHL-TCCB";
            MauSoHopDongThanhLyThinhGiangChatLuongCao = "{00#}/HĐCN-ĐHL-TCCB";
            MauSoHopDongThinhGiang = "{00#}/HĐTG-ĐHL-TCCB";
        }
    }

}
