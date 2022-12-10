using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{
    [ModelDefault("Caption", "Hệ số địa điểm ngoài giờ")]
    [DefaultProperty("Caption")]
    public class HeSoDiaDiemNgoaiGio : BaseObject
    {
        private decimal _HeSo;
        private QuanLyHeSo _QuanLyHeSo;
        private DayOfWeek? _Thu;
        private int _TuTiet;
        private int _DenTiet;
        private TinhThanh _TinhThanh;
        private GioGiangDayEnum _GioGiangDay;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoDiaDiemNgoaiGio")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Hệ số")]
        public decimal HeSo
        {
            get { return _HeSo; }
            set
            {
                SetPropertyValue("HeSo", ref _HeSo, value);
            }
        }

        [ModelDefault("Caption", "Từ tiết")]
        public int TuTiet
        {
            get { return _TuTiet; }
            set { SetPropertyValue("TuTiet", ref _TuTiet, value); }
        }


        [ModelDefault("Caption", "Đến tiết")]
        public int DenTiet
        {
            get { return _DenTiet; }
            set { SetPropertyValue("DenTiet", ref _DenTiet, value); }
        }

        [ModelDefault("Caption", "Thứ")]
        public DayOfWeek? Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }

        [ModelDefault("Caption", "Tỉnh thành")]
        public TinhThanh TinhThanh
        {
            get { return _TinhThanh; }
            set { SetPropertyValue("TinhThanh", ref _TinhThanh, value); }
        }
        [ModelDefault("Caption", "Loại giờ giảng dạy")]
        public GioGiangDayEnum GioGiangDay
        {
            get { return _GioGiangDay; }
            set { SetPropertyValue("GioGiangDay", ref _GioGiangDay, value); }
        }

        public HeSoDiaDiemNgoaiGio(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
