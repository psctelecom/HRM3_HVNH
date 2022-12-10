using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách định mức chức vụ")]
    public class dsDinhMucChucVu : BaseObject
    {
        private bool _Chon;
        private Guid _OidQLyGioChuan;
        private Guid _OidDinhMucChucVu;
        //
        private string _ChucVu;
        private decimal _DinhMuc;
        private decimal _SoGioChuan;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;
        private int _SoGiangVienToiThieu;
        private int _SoSVToiThieu;

        //

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Oid QLyGioChuan")]
        [Browsable(false)]
        public Guid OidQLyGioChuan
        {
            get { return _OidQLyGioChuan; }
            set { SetPropertyValue("OidQLyGioChuan", ref _OidQLyGioChuan, value); }
        }

        [ModelDefault("Caption", "Oid DinhMucChucVu")]
        [Browsable(false)]
        public Guid OidDinhMucChucVu
        {
            get { return _OidDinhMucChucVu; }
            set { SetPropertyValue("OidDinhMucChucVu", ref _OidDinhMucChucVu, value); }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public string ChucVu
        {
            get { return _ChucVu; }
            set { SetPropertyValue("ChucVu", ref _ChucVu, value); }
        }

        [ModelDefault("Caption", "Định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DinhMuc
        {
            get { return _DinhMuc; }
            set { SetPropertyValue("DinhMuc", ref _DinhMuc, value); }
        }

        [ModelDefault("Caption", "Số giờ chuẩn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }

        [ModelDefault("Caption", "Số giờ định mức(NCKH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCKH
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCKH", ref _SoGioDinhMuc_NCKH, value); }
        }

        [ModelDefault("Caption", "Số giờ định mức(khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Số GV tối thiểu")]
        [ModelDefault("DisplayFormat","n")]
        [ModelDefault("EditMask", "n")]
        public int SoGiangVienToiThieu
        {
            get { return _SoGiangVienToiThieu; }
            set
            {
                SetPropertyValue("SoGiangVienToiThieu", ref _SoGiangVienToiThieu, value);
            }
        }

        [ModelDefault("Caption", "Số SV tối thiểu")]
        [ModelDefault("DisplayFormat", "n")]
        [ModelDefault("EditMask", "n")]
        public int SoSVToiThieu
        {
            get { return _SoSVToiThieu; }
            set
            {
                SetPropertyValue("SoSVToiThieu", ref _SoSVToiThieu, value);
            }
        }


        public dsDinhMucChucVu(Session session)
            : base(session)
        { }
    }
}
