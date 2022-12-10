using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;

namespace PSC_HRM.Module.PMS.NghiepVu.NEU.VHVL
{
    [ModelDefault("Caption","Khối lượng đào tạo từ xa")]
    public class DanhMucDonGiaCongTacPhi_NEU : BaseObject
    {
        private int _MucDo;
        [ModelDefault("Caption", "Mức độ")]
        public int MucDo
        {
            get { return _MucDo; }
            set { _MucDo = value; }
        }
        private DiaDiemGiangDayLienKet _DiaDiem;
        [ModelDefault("Caption", "Địa điểm")]       
        public DiaDiemGiangDayLienKet DiaDiem
        {
            get { return _DiaDiem; }
            set { SetPropertyValue("DiaDiem", ref _DiaDiem, value); }
        }
        private decimal _DinhMucMotLan;
        [ModelDefault("Caption", "Định mức/lần")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DinhMucMotLan
        {
            get { return _DinhMucMotLan; }
            set { _DinhMucMotLan = value; }
        }

        public DanhMucDonGiaCongTacPhi_NEU(Session session)
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