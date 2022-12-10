using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.DanhGia
{
    [ImageName("BO_DanhGiaCanBo")]
    [ModelDefault("Caption", "Xếp loại lao động")]
    public class XepLoaiLaoDong : BaseObject
    {
        private QuanLyXepLoaiLaoDong _QuanLyXepLoaiLaoDong;
        private DateTime _ThangNam;

        [ModelDefault("Caption", "Quản lý xếp loại lao động")]
        [Association("QuanLyXepLoaiLaoDong-ListXepLoaiLaoDong")]
        public QuanLyXepLoaiLaoDong QuanLyXepLoaiLaoDong
        {
            get
            {
                return _QuanLyXepLoaiLaoDong;
            }
            set
            {
                SetPropertyValue("QuanLyXepLoaiLaoDong", ref _QuanLyXepLoaiLaoDong, value);
            }
        }

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ModelDefault("EditMask", "MM/yyyy")]
        public DateTime ThangNam
        {
            get
            {
                return _ThangNam;
            }
            set
            {
                SetPropertyValue("ThangNam", ref _ThangNam, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Xếp loại lần 1")]
        [Association("XepLoaiLaoDong-ListXepLoaiLan1")]
        public XPCollection<XepLoaiLan1> ListXepLoaiLan1
        {
            get
            {
                return GetCollection<XepLoaiLan1>("ListXepLoaiLan1");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Xếp loại lần 2")]
        [Association("XepLoaiLaoDong-ListXepLoaiLan2")]
        public XPCollection<XepLoaiLan2> ListXepLoaiLan2
        {
            get
            {
                return GetCollection<XepLoaiLan2>("ListXepLoaiLan2");
            }
        }

        public XepLoaiLaoDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThangNam = HamDungChung.GetServerTime();
        }
    }

}
