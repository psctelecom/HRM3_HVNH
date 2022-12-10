using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class ChiTietDanhSachThongTinThoiKhoaBieuGiangVien : BaseObject
    {
        private int _Tuan;
        private string _Thu;
        private string _KhoanTiet;
        private int _SoTiet;
        private string _Phong;
        private string _MaGV;
        private string _TenGV;
        private string _NgayDay;


        [ModelDefault("Caption", "Tuần")]
        public int Tuan
        {
            get { return _Tuan; }
            set { SetPropertyValue("Tuan", ref _Tuan, value); }
        }

        [ModelDefault("Caption", "Thứ")]
        public string Thu
        {
            get { return _Thu; }
            set { SetPropertyValue("Thu", ref _Thu, value); }
        }

        [ModelDefault("Caption", "Khoản tiết")]
        public string KhoanTiet
        {
            get { return _KhoanTiet; }
            set { SetPropertyValue("KhoanTiet", ref _KhoanTiet, value); }
        }

        [ModelDefault("Caption", "Số tiết")]
        public int SoTiet
        {
            get { return _SoTiet; }
            set { SetPropertyValue("SoTiet", ref _SoTiet, value); }
        }

        [ModelDefault("Caption", "Phòng")]
        public string Phong
        {
            get { return _Phong; }
            set { SetPropertyValue("Phong", ref _Phong, value); }
        }

        [ModelDefault("Caption", "Mã GV")]
        public string MaGV
        {
            get { return _MaGV; }
            set { SetPropertyValue("MaGV", ref _MaGV, value); }
        }

        [ModelDefault("Caption", "Tên GV")]
        public string TenGV
        {
            get { return _TenGV; }
            set { SetPropertyValue("TenGV", ref _TenGV, value); }
        }

        [ModelDefault("Caption", "Ngày dạy")]
        public string NgayDay
        {
            get { return _NgayDay; }
            set { SetPropertyValue("NgayDay", ref _NgayDay, value); }
        }

        public ChiTietDanhSachThongTinThoiKhoaBieuGiangVien(Session session)
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