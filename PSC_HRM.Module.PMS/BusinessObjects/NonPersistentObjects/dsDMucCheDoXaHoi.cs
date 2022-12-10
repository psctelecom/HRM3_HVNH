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
    [ModelDefault("Caption", "Danh sách NV")]
    public class dsDMucCheDoXaHoi : BaseObject
    {
        private bool _Chon;
        private Guid _OidNV;
        //
        private string _MaQuanLy;
        private string _HoTen;
        private DateTime _NgaySinh;
        private int _TuoiTheoThang;
        private string _TenBoPhan;
        private string _TenTinhTrang;

        [ModelDefault("Caption", "Tình trạng")]
        public string TenTinhTrang
        {
            get { return _TenTinhTrang; }
            set { _TenTinhTrang = value; }
        }

        [ModelDefault("Caption", "Đơn vị")]

        public string TenBoPhan
        {
            get { return _TenBoPhan; }
            set { _TenBoPhan = value; }
        }


        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }
        [ModelDefault("Caption", "Oid NV")]
        [Browsable(false)]
        public Guid OidNV
        {
            get { return _OidNV; }
            set { SetPropertyValue("OidNV", ref _OidNV, value); }
        }
        [ModelDefault("Caption", "Mã Quản Lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }
        [ModelDefault("Caption", "Họ tên")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }
        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get { return _NgaySinh; }
            set { SetPropertyValue("NgaySinh", ref _NgaySinh, value); }
        }
        [ModelDefault("Caption", "Tuổi theo tháng")]
        [ModelDefault("DisplayFormat", "N")]
        [ModelDefault("EditMask", "N")]
        public int TuoiTheoThang
        {
            get { return _TuoiTheoThang; }
            set { SetPropertyValue("TuoiTheoThang", ref _TuoiTheoThang, value); }
        }

        public dsDMucCheDoXaHoi(Session session)
            : base(session)
        { }
    }
}
