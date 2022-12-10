using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NghiPhep
{
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Thông tin nghỉ phép")]
    [RuleCombinationOfPropertiesIsUnique("ThongTinNghiPhep", DefaultContexts.Save, "QuanLyNghiPhep;ThongTinNhanVien")]
    public class ThongTinNghiPhep : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyNghiPhep _QuanLyNghiPhep;
        private string _GhiChu;

        private decimal _SoNgayPhepCoBan;
        private decimal _SoNgayPhepCongThem;
        //private decimal _SoNgayPhepNamTruoc;

        //private decimal _PhepNamTruocConLai;
        //private decimal _PhepNamNayConLai;

        private decimal _SoNgayPhepDaNghi;
        private decimal _SoNgayPhepConLai;

        private bool _TruNgayDiDuong;

        //private decimal _TongSoNgayPhep;
        
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý nghỉ phép")]
        [Association("QuanLyNghiPhep-ListThongTinNghiPhep")]
        public QuanLyNghiPhep QuanLyNghiPhep
        {
            get
            {
                return _QuanLyNghiPhep;
            }
            set
            {
                SetPropertyValue("QuanLyNghiPhep", ref _QuanLyNghiPhep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
            }
        }

        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "Số ngày phép cơ bản")]
        public decimal SoNgayPhepCoBan
        {
            get
            {
                return _SoNgayPhepCoBan;
            }
            set
            {
                SetPropertyValue("SoNgayPhepCoBan", ref _SoNgayPhepCoBan, value);
                
            }
        }

        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "Số ngày phép cộng thêm")]
        public decimal SoNgayPhepCongThem
        {
            get
            {
                return _SoNgayPhepCongThem;
            }
            set
            {
                SetPropertyValue("SoNgayPhepCongThem", ref _SoNgayPhepCongThem, value);
            }
        }

        //[ModelDefault("DisplayFormat", "N1")]
        //[ModelDefault("EditMask", "N1")]
        //[ModelDefault("Caption", "Số ngày phép năm trước")]
        //public decimal SoNgayPhepNamTruoc
        //{
        //    get
        //    {
        //        return _SoNgayPhepNamTruoc;
        //    }
        //    set
        //    {
        //        SetPropertyValue("SoNgayPhepNamTruoc", ref _SoNgayPhepNamTruoc, value);
        //    }
        //}

        //[ModelDefault("DisplayFormat", "N1")]
        //[ModelDefault("EditMask", "N1")]
        //[ModelDefault("Caption", "Phép năm trước còn lại")]
        //public decimal PhepNamTruocConLai
        //{
        //    get
        //    {
        //        return _PhepNamTruocConLai;
        //    }
        //    set
        //    {
        //        SetPropertyValue("PhepNamTruocConLai", ref _PhepNamTruocConLai, value);
        //    }
        //}

        //[ModelDefault("DisplayFormat", "N1")]
        //[ModelDefault("EditMask", "N1")]
        //[ModelDefault("Caption", "Phép năm nay còn lại")]
        //public decimal PhepNamNayConLai
        //{
        //    get
        //    {
        //        return _PhepNamNayConLai;
        //    }
        //    set
        //    {
        //        SetPropertyValue("PhepNamNayConLai", ref _PhepNamNayConLai, value);
        //    }
        //}
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "Số ngày phép đã nghỉ")]
        [ImmediatePostData]
        public decimal SoNgayPhepDaNghi
        {
            get
            {
                return _SoNgayPhepDaNghi;
            }
            set
            {
                SetPropertyValue("SoNgayPhepDaNghi", ref _SoNgayPhepDaNghi, value);
                if (!IsLoading && value != null)
                {
                    SoNgayPhepConLai = SoNgayPhepCoBan + SoNgayPhepCongThem - SoNgayPhepDaNghi;
                }
                
            }
        }
        [ImmediatePostData]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("Caption", "Số ngày phép còn lại")]
        [ModelDefault("AllowEdit", "False")]
        public decimal SoNgayPhepConLai
        {
            get
            {
                return _SoNgayPhepConLai;
            }
            set
            {
                SetPropertyValue("SoNgayPhepConLai", ref _SoNgayPhepConLai, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Trừ ngày đi đường")]
        public bool TruNgayDiDuong
        {
            get
            {
                return _TruNgayDiDuong;
            }
            set
            {
                SetPropertyValue("TruNgayDiDuong", ref _TruNgayDiDuong, value);
            }
        }

        /*
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết nghỉ phép")]
        [Association("ThongTinNghiPhep-ListChiTietNghiPhep")]
        public XPCollection<ChiTietNghiPhep> ListChiTietNghiPhep
        {
            get
            {
                return GetCollection<ChiTietNghiPhep>("ListChiTietNghiPhep");
            }
        }
        */

        
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết thông tin nghỉ phép")]
        [Association("ThongTinNghiPhep-ListChiTietThongTinNghiPhep")]
        [ImmediatePostData]
        public XPCollection<ChiTietThongTinNghiPhep> ListChiTietThongTinNghiPhep
        {
            get
            {
                return GetCollection<ChiTietThongTinNghiPhep>("ListChiTietThongTinNghiPhep");
            }
        }
        
        
        public ThongTinNghiPhep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            //if (ListChiTietThongTinNghiPhep != null)
            //    foreach (ChiTietThongTinNghiPhep item in ListChiTietThongTinNghiPhep)
            //    {
            //        this.SoNgayPhepDaNghi -= item.SoNgay;
            //    }
        }

        //Nguyen
        //protected override void OnSaving()
        //{
        //    base.OnSaving();
        //    //this.SoNgayPhepDaNghi = 0;
        //    if (!IsDeleted && ListChiTietThongTinNghiPhep != null)
        //        foreach (ChiTietThongTinNghiPhep item in ListChiTietThongTinNghiPhep)
        //        {
        //            this.SoNgayPhepDaNghi = item.SoNgay;
        //        }
        //}
        //protected override void OnDeleted()
        //{ 

        //    base.OnDeleted();
        //    //this.SoNgayPhepDaNghi = 0;
        //    if (!IsSaving && ListChiTietThongTinNghiPhep != null)
        //        foreach (ChiTietThongTinNghiPhep item in ListChiTietThongTinNghiPhep)
        //        {
        //            this.SoNgayPhepDaNghi += item.SoNgay;
        //        }
        //    //CapNhatSoNgayNghi();
        //}

        //Nguyen
        //private void CapNhatSoNgayDaNghi()
        //{
        //    //ThongTinNghiPhep.SoNgayPhepDaNghi = 0;
        //    foreach (ChiTietThongTinNghiPhep item in ListChiTietThongTinNghiPhep)
        //    {
        //        SoNgayPhepDaNghi += item.SoNgay;
        //    }
        //}

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
