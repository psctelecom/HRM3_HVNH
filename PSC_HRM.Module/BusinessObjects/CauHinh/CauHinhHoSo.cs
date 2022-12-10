using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình hồ sơ")]
    public class CauHinhHoSo : TruongBaseObject
    {
        // Fields...
        private bool _KhongHienNhanVienKhiChonTruong;
        private decimal _HSPCTrachNhiem;
        private decimal _TyLeHeSoKiemNhiem2;
        private decimal _TyLeHeSoKiemNhiem1;
        private int _SoBatDauMaGiangVienThinhGiang;
        private int _SoBatDauSoHieuCongChuc;
        private int _SoBatDauSoHoSo;
        private int _SoBatDauMaNhanVien;
        private decimal _QuaSinhNhat;
        private string _MauMaGiangVienThinhGiang;
        private bool _TuDongTaoMaGiangVienThinhGiang;
        private string _MauSoHieuCongChuc;
        private string _MauSoHoSo;
        private string _MauMaNhanVien;
        private bool _TuDongTaoSoHieuCongChuc;
        private bool _TuDongTaoSoHoSo;
        private bool _TuDongTaoMaNhanVien;
        private bool _TuDongTaoHoSoBaoHiem;

        [ModelDefault("Caption", "Không hiện nhân viên khi chọn trường")]
        public bool KhongHienNhanVienKhiChonTruong
        {
            get
            {
                return _KhongHienNhanVienKhiChonTruong;
            }
            set
            {
                SetPropertyValue("KhongHienNhanVienKhiChonTruong", ref _KhongHienNhanVienKhiChonTruong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã nhân viên")]
        public bool TuDongTaoMaNhanVien
        {
            get
            {
                return _TuDongTaoMaNhanVien;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaNhanVien", ref _TuDongTaoMaNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu mã nhân viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaNhanVien")]
        public int SoBatDauMaNhanVien
        {
            get
            {
                return _SoBatDauMaNhanVien;
            }
            set
            {
                SetPropertyValue("SoBatDauMaNhanVien", ref _SoBatDauMaNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã nhân viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaNhanVien")]
        public string MauMaNhanVien
        {
            get
            {
                return _MauMaNhanVien;
            }
            set
            {
                SetPropertyValue("MauMaNhanVien", ref _MauMaNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số hồ sơ")]
        public bool TuDongTaoSoHoSo
        {
            get
            {
                return _TuDongTaoSoHoSo;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHoSo", ref _TuDongTaoSoHoSo, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu số hồ sơ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHoSo")]
        public int SoBatDauSoHoSo
        {
            get
            {
                return _SoBatDauSoHoSo;
            }
            set
            {
                SetPropertyValue("SoBatDauSoHoSo", ref _SoBatDauSoHoSo, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hồ sơ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHoSo")]
        public string MauSoHoSo
        {
            get
            {
                return _MauSoHoSo;
            }
            set
            {
                SetPropertyValue("MauSoHoSo", ref _MauSoHoSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số hiệu công chức")]
        public bool TuDongTaoSoHieuCongChuc
        {
            get
            {
                return _TuDongTaoSoHieuCongChuc;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHieuCongChuc", ref _TuDongTaoSoHieuCongChuc, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu số hiệu công chức")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHieuCongChuc")]
        public int SoBatDauSoHieuCongChuc
        {
            get
            {
                return _SoBatDauSoHieuCongChuc;
            }
            set
            {
                SetPropertyValue("SoBatDauSoHieuCongChuc", ref _SoBatDauSoHieuCongChuc, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số hiệu công chức")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHieuCongChuc")]
        public string MauSoHieuCongChuc
        {
            get
            {
                return _MauSoHieuCongChuc;
            }
            set
            {
                SetPropertyValue("MauSoHieuCongChuc", ref _MauSoHieuCongChuc, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã giảng viên thỉnh giảng")]
        public bool TuDongTaoMaGiangVienThinhGiang
        {
            get
            {
                return _TuDongTaoMaGiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaGiangVienThinhGiang", ref _TuDongTaoMaGiangVienThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu mã giảng viên thỉnh giảng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaGiangVienThinhGiang")]
        public int SoBatDauMaGiangVienThinhGiang
        {
            get
            {
                return _SoBatDauMaGiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("SoBatDauMaGiangVienThinhGiang", ref _SoBatDauMaGiangVienThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã giảng viên thỉnh giảng")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaGiangVienThinhGiang")]
        public string MauMaGiangVienThinhGiang
        {
            get
            {
                return _MauMaGiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("MauMaGiangVienThinhGiang", ref _MauMaGiangVienThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Tự động tạo hồ sơ bảo hiểm")]
        public bool TuDongTaoHoSoBaoHiem
        {
            get
            {
                return _TuDongTaoHoSoBaoHiem;
            }
            set
            {
                SetPropertyValue("TuDongTaoHoSoBaoHiem", ref _TuDongTaoHoSoBaoHiem, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Quà sinh nhật")]
        public decimal QuaSinhNhat
        {
            get
            {
                return _QuaSinhNhat;
            }
            set
            {
                SetPropertyValue("QuaSinhNhat", ref _QuaSinhNhat, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "% tỷ lệ hệ số kiêm nhiệm 1")]
        public decimal TyLeHeSoKiemNhiem1
        {
            get
            {
                return _TyLeHeSoKiemNhiem1;
            }
            set
            {
                SetPropertyValue("TyLeHeSoKiemNhiem1", ref _TyLeHeSoKiemNhiem1, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "% tỷ lệ hệ số kiêm nhiệm 2")]
        public decimal TyLeHeSoKiemNhiem2
        {
            get
            {
                return _TyLeHeSoKiemNhiem2;
            }
            set
            {
                SetPropertyValue("TyLeHeSoKiemNhiem2", ref _TyLeHeSoKiemNhiem2, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "HSPC trách nhiệm")]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return _HSPCTrachNhiem;
            }
            set
            {
                SetPropertyValue("HSPCTrachNhiem", ref _HSPCTrachNhiem, value);
            }
        }

        public CauHinhHoSo(Session session) : base(session) { }
        protected override void OnLoaded()
        {
            base.OnLoaded();

            MaTruong = TruongConfig.MaTruong;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuDongTaoMaNhanVien = true;
            TuDongTaoSoHieuCongChuc = true;
            TuDongTaoSoHoSo = true;
            TuDongTaoHoSoBaoHiem = true;
            SoBatDauMaGiangVienThinhGiang = 1;
            SoBatDauMaNhanVien = 1;
            SoBatDauSoHoSo = 1;
            SoBatDauSoHieuCongChuc = 1;
            QuaSinhNhat = 500000m;
            MauMaNhanVien = "NV{00#}";
            MauMaGiangVienThinhGiang = "TG{00#}";
            MauSoHieuCongChuc = "{00#}";
            MauSoHoSo = "{00#}";
            TyLeHeSoKiemNhiem1 = 25;
            TyLeHeSoKiemNhiem2 = 10;
            HSPCTrachNhiem = 0.3m;
        }
    }

}
