using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.NghiPhep
{
    [ModelDefault("Caption", "Quản lý nghỉ phép")]
    public class QuanLyNghiPhep : BaseObject
    {
        // Fields...
        private int _Nam;
        private DateTime _NgayBatDau;
        private DateTime _NgayKetThuc;

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                if (!IsLoading && value != null)
                {
                    NgayBatDau = new DateTime(value, 1, 1);
                }
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Thông tin nghỉ phép")]
        [Association("QuanLyNghiPhep-ListThongTinNghiPhep")]
        public XPCollection<ThongTinNghiPhep> ListThongTinNghiPhep
        {
            get
            {
                return GetCollection<ThongTinNghiPhep>("ListThongTinNghiPhep");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày bắt đầu")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBatDau
        {
            get
            {
                return _NgayBatDau;
            }
            set
            {
                SetPropertyValue("NgayBatDau", ref _NgayBatDau, value);
                if (!IsLoading && value != null && value != DateTime.MinValue)
                {
                    NgayKetThuc = NgayBatDau.AddYears(1).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayKetThuc
        {
            get
            {
                return _NgayKetThuc;
            }
            set
            {
                SetPropertyValue("NgayKetThuc", ref _NgayKetThuc, value);
            }
        }


        //[Aggregated]
        //[ModelDefault("Caption", "Ứng trước ngày phép")]
        //[Association("QuanLyNghiPhep-ListUngTruocNgayPhep")]
        //public XPCollection<UngTruocNgayPhep> ListUngTruocNgayPhep
        //{
        //    get
        //    {
        //        return GetCollection<UngTruocNgayPhep>("ListUngTruocNgayPhep");
        //    }
        //}

        public QuanLyNghiPhep(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Nam = HamDungChung.GetServerTime().Year;
        }

        public ThongTinNghiPhep GetThongTinNghiPhep(ThongTinNhanVien nhanVien)
        {
            foreach (ThongTinNghiPhep item in ListThongTinNghiPhep)
            {
                if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
                    return item;
            }
            return null;
        }

        //public UngTruocNgayPhep GetUngTrucNgayPhep(ThongTinNhanVien nhanVien)
        //{
        //    foreach (UngTruocNgayPhep item in ListUngTruocNgayPhep)
        //    {
        //        if (item.ThongTinNhanVien.Oid == nhanVien.Oid)
        //            return item;
        //    }
        //    return null;
        //}
    }

}
