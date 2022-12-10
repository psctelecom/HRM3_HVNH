using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;

namespace PSC_HRM.Module.NghiPhep
{
    [ImageName("BO_NangThamNien")]
    [ModelDefault("Caption", "Ứng trước ngày phép")]
    [Appearance("UngTruocNgayPhep", TargetItems = "*", Enabled = false, Criteria = "Khoa")]
    //[RuleCombinationOfPropertiesIsUnique("UngTruocNgayPhep", DefaultContexts.Save, "QuanLyNghiPhep;ThongTinNhanVien")]
    //Không dùng nữa 04/01/2017
    public class UngTruocNgayPhep : BaseObject
    {
        private bool _Khoa;
        private int _SoNgayPhep;
        private int _UngTruocNgayPhepCuaNam;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyNghiPhep _QuanLyNghiPhep;

        //[Browsable(false)]
        //[ImmediatePostData]
        //[ModelDefault("Caption", "Quản lý nghỉ phép")]
        //[Association("QuanLyNghiPhep-ListUngTruocNgayPhep")]
        //public QuanLyNghiPhep QuanLyNghiPhep
        //{
        //    get
        //    {
        //        return _QuanLyNghiPhep;
        //    }
        //    set
        //    {
        //        SetPropertyValue("QuanLyNghiPhep", ref _QuanLyNghiPhep, value);
        //        if (!IsLoading && value != null)
        //            UngTruocNgayPhepCuaNam = value.Nam + 1;
        //    }
        //}

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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                if (!IsLoading && UngTruocNgayPhepCuaNam > 0 && value != null)
                    SoNgayPhep = NghiPhepHelper.TinhSoNgayPhep(value, UngTruocNgayPhepCuaNam);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ứng trước ngày phép của năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int UngTruocNgayPhepCuaNam
        {
            get
            {
                return _UngTruocNgayPhepCuaNam;
            }
            set
            {
                SetPropertyValue("UngTruocNgayPhepCuaNam", ref _UngTruocNgayPhepCuaNam, value);
                if (!IsLoading && UngTruocNgayPhepCuaNam > 0 && ThongTinNhanVien != null)
                    SoNgayPhep = NghiPhepHelper.TinhSoNgayPhep(ThongTinNhanVien, UngTruocNgayPhepCuaNam);
            }
        }

        [ModelDefault("Caption", "Số ngày phép")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int SoNgayPhep
        {
            get
            {
                return _SoNgayPhep;
            }
            set
            {
                SetPropertyValue("SoNgayPhep", ref _SoNgayPhep, value);
            }
        }

        //sau khi lưu khong cho phép sửa
        [Browsable(false)]
        public bool Khoa
        {
            get
            {
                return _Khoa;
            }
            set
            {
                SetPropertyValue("Khoa", ref _Khoa, value);
            }
        }

        public UngTruocNgayPhep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        //protected override void OnSaving()
        //{
        //    base.OnSaving();

        //    if (!IsDeleted
        //        && QuanLyNghiPhep.Oid != Guid.Empty
        //        && !Khoa)
        //    {
        //        ThongTinNghiPhep nghiPhep = QuanLyNghiPhep.GetThongTinNghiPhep(ThongTinNhanVien);
        //        if (nghiPhep == null)
        //        {
        //            nghiPhep = new ThongTinNghiPhep(Session);
        //            nghiPhep.QuanLyNghiPhep = QuanLyNghiPhep;
        //            nghiPhep.BoPhan = BoPhan;
        //            nghiPhep.ThongTinNhanVien = ThongTinNhanVien;
        //        }

        //        //nghiPhep.TongSoNgayPhep += SoNgayPhep;
        //        nghiPhep.GhiChu = String.Format("Đã ứng trước {0} ngày phép năm {1:####}", SoNgayPhep, UngTruocNgayPhepCuaNam);
        //        Khoa = true;
        //    }
        //}

        //protected override void OnDeleting()
        //{
        //    ThongTinNghiPhep nghiPhep = QuanLyNghiPhep.GetThongTinNghiPhep(ThongTinNhanVien);
        //    if (nghiPhep != null)
        //    {
        //        //nghiPhep.TongSoNgayPhep -= SoNgayPhep;
        //    }

        //    base.OnDeleting();
        //}
    }

}
