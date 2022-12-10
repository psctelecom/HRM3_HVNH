using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.GioChuan
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenNhomMonHoc")]
    [ModelDefault("Caption", "Định mức chức vụ")]
    [Appearance("SoGioChuan", TargetItems = "SoGioChuan", BackColor = "Aquamarine", FontColor = "Red")]

    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyGioChuan;ChucVu;SoGiangVienToiThieu;SoSVToiThieu;GhiChu", "Chức vụ đã tồn tại")]
    [Appearance("Hide_DNU", TargetItems = "ChiTinhGioChuan"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyGioChuan.ThongTinTruong.TenVietTat <> 'DNU'")]
    public class DinhMucChucVu : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;

        private ChucVu _ChucVu;
        private ChucVuDoan _ChucVuDoan;

        private ChucVuDang _ChucVuDang;

        private ChucVuDoanThe _ChucVuDoanThe;

        private decimal _DinhMuc;
        private decimal _SoGioChuan;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;
        private int _SoGiangVienToiThieu;
        private int _SoSVToiThieu;
        private string _GhiChu;
        private bool _ChiTinhGioChuan;


        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucChucVu")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }
        [ModelDefault("Caption", "Chức vụ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
                //if (!IsLoading)
                //    Check();
            }
        }

        [ModelDefault("Caption", "Chức vụ Đảng")]
        [ImmediatePostData]
        public ChucVuDang ChucVuDang
        {
            get { return _ChucVuDang; }
            set
            {
                _ChucVuDang = value;
                //if (!IsLoading)
                //    Check();
            }
        }
        [ModelDefault("Caption", "Chức vụ Đoàn")]
        [ImmediatePostData]
        public ChucVuDoan ChucVuDoan
        {
            get { return _ChucVuDoan; }
            set
            {
                _ChucVuDoan = value;
                //if (!IsLoading)
                //    Check();
            }
        }
        [ModelDefault("Caption", "Chức vụ Đoàn thể")]
        [ImmediatePostData]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get { return _ChucVuDoanThe; }
            set
            {
                _ChucVuDoanThe = value;
                //if (!IsLoading)
                //    Check();
            }
        }
        [ModelDefault("Caption", "Định mức (%)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ImmediatePostData]
        public decimal DinhMuc
        {
            get { return _DinhMuc; }
            set
            {
                SetPropertyValue("DinhMuc", ref _DinhMuc, value);
                if (!IsLoading)
                {
                    PSC_HRM.Module.CauHinh.CauHinhChung cauHinh = HamDungChung.CauHinhChung;
                    if (cauHinh != null)
                    {
                        SoGioChuan = cauHinh.SoGioChuan * DinhMuc / 100;
                        //SoGioDinhMuc_NCHK = cauHinh.SoGioChuan_NCHK * DinhMuc / 100;
                        //SoGioDinhMuc_Khac = cauHinh.SoGioChuan_Khac * DinhMuc / 100;
                        SoGioDinhMuc_NCHK = cauHinh.SoGioChuan_NCHK;
                        SoGioDinhMuc_Khac = cauHinh.SoGioChuan_Khac;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Số giờ chuẩn")]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }
        [ModelDefault("Caption", "Số giờ chuẩn (NCKH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCHK
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCHK", ref _SoGioDinhMuc_NCKH, value); }
        }
        [ModelDefault("Caption", "Số giờ chuẩn(Khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Số giảng viên (Tối thiểu)")]
        public int SoGiangVienToiThieu
        {
            get { return _SoGiangVienToiThieu; }
            set { SetPropertyValue("SoGiangVienToiThieu", ref _SoGiangVienToiThieu, value); }
        }

        [ModelDefault("Caption", "Số sinh viên (Tối thiểu)")]
        public int SoSVToiThieu
        {
            get { return _SoSVToiThieu; }
            set { SetPropertyValue("SoSVToiThieu", ref _SoSVToiThieu, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
                //if (!IsLoading)
                //    Check();
            }
        }
        [ModelDefault("Caption", "Chỉ tính giờ chuẩn")]
        public bool ChiTinhGioChuan
        {
            get { return _ChiTinhGioChuan; }
            set
            {
                SetPropertyValue("ChiTinhGioChuan", ref _ChiTinhGioChuan, value);
            }
        }


        //private bool _KT;
        //[NonPersistent]
        //[Browsable(false)]
        //[RuleFromBoolProperty("DinhMucChucVu.KT", DefaultContexts.Save, "Chức vụ hoặc ghi chú không được rỗng!", SkipNullOrEmptyValues = false, UsedProperties = "QuanLyGioChuan")]
        //public bool KT
        //{
        //    get
        //    {
        //        return !_KT;
        //    }
        //    set
        //    {
        //        SetPropertyValue("KT", ref _KT, value);
        //    }
        //}
        //void Check()
        //{
        //    #region Store
        //    {
        //        if (ChucVu == null && ChucVuDang == null && ChucVuDoan == null && ChucVuDoanThe == null && GhiChu == string.Empty)
        //        {
        //            KT = true;//không dc Save
        //        }
        //        else
        //            KT = false;//Dc save                     
        //        #endregion
        //    }
        //}


        public DinhMucChucVu(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //KT = true;
            DinhMuc = 0;
            SoGiangVienToiThieu = 0;
            SoSVToiThieu = 0;
            PSC_HRM.Module.CauHinh.CauHinhChung cauHinh = HamDungChung.CauHinhChung;
            if (cauHinh != null)
            {
                SoGioChuan = cauHinh.SoGioChuan * DinhMuc / 100;
                SoGioDinhMuc_NCHK = cauHinh.SoGioChuan_NCHK;
                SoGioDinhMuc_Khac = cauHinh.SoGioChuan_Khac;
            }
        }

        //protected override void OnSaving()
        //{
        //    Check();
        //}
    }
}