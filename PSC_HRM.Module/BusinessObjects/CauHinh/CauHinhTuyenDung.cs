using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.CauHinh
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình tuyển dụng")]
    [Appearance("CauHinhTuyenDung", TargetItems = "SoBatDau;MauSoBaoDanhGiangVien;MaSoBaoDanhChuyenVien", Visibility = ViewItemVisibility.Hide, Criteria = "!TuDongTaoSoBaoDanh")]
    public class CauHinhTuyenDung : BaseObject
    {
        // Fields...
        private string _MaSoBaoDanhChuyenVien;
        private int _SoBatDau;
        private string _MauSoBaoDanhGiangVien;
        private bool _TuDongTaoSoBaoDanh = true;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số báo danh")]
        public bool TuDongTaoSoBaoDanh
        {
            get
            {
                return _TuDongTaoSoBaoDanh;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoBaoDanh", ref _TuDongTaoSoBaoDanh, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBaoDanh")]
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

        [ModelDefault("Caption", "Mẫu số báo danh giảng viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBaoDanh")]
        public string MauSoBaoDanhGiangVien
        {
            get
            {
                return _MauSoBaoDanhGiangVien;
            }
            set
            {
                SetPropertyValue("MauSoBaoDanhGiangVien", ref _MauSoBaoDanhGiangVien, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số báo danh nhân viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoBaoDanh")]
        public string MaSoBaoDanhChuyenVien
        {
            get
            {
                return _MaSoBaoDanhChuyenVien;
            }
            set
            {
                SetPropertyValue("MaSoBaoDanhChuyenVien", ref _MaSoBaoDanhChuyenVien, value);
            }
        }

        public CauHinhTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuDongTaoSoBaoDanh = true;
            SoBatDau = 1;
            MauSoBaoDanhGiangVien = "GV {0#}";
            MaSoBaoDanhChuyenVien = "CV {0#}";
        }
    }

}
