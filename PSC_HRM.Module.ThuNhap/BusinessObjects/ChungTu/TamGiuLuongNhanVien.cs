using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_ChuyenKhoan")]
    [ModelDefault("Caption", "Danh sách tạm giữ lương nhân viên")]
    [NonPersistent]
    public class TamGiuLuongNhanVien : BaseObject
    {
        private DateTime _TuThang;
        private DateTime _DenThang;

        [ModelDefault("Caption", "Từ tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime TuThang
        {
            get
            {
                return _TuThang;
            }
            set
            {
                SetPropertyValue("TuThang", ref _TuThang, value);
            }
        }
        [ModelDefault("Caption", "Đến tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        public DateTime DenThang
        {
            get
            {
                return _DenThang;
            }
            set
            {
                SetPropertyValue("DenThang", ref _DenThang, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietTamGiuLuongNhanVien> ListChiTietTamGiuLuongNhanVien { get; set; }

        public TamGiuLuongNhanVien(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            TuThang = HamDungChung.GetServerTime().Date;
            DenThang = TuThang;
        }
       
    }
}
