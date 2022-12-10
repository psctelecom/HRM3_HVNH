using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.PMS.NghiepVu.NEU.VHVL
{
    [ModelDefault("Caption","Địa điểm giảng dạy liên kết")]
    public class DiaDiemGiangDayLienKet : BaseObject
    {
       
        private string _MaQuanLy;
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { _MaQuanLy = value; }
        }
        private string _TenDiaDiem;
        [ModelDefault("Caption", "Tên địa điểm")]
        public string TenDiaDiem
        {
            get { return _TenDiaDiem; }
            set { _TenDiaDiem = value; }
        }

        private decimal _DonGia;
        [ModelDefault("Caption", "Đơn giá")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { _DonGia = value; }
        }
        public DiaDiemGiangDayLienKet(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}