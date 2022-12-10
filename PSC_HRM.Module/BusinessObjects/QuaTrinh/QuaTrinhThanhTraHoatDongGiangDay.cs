using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình thanh tra giảng dạy")]    
    public class QuaTrinhThanhTraHoatDongGiangDay : BaseObject
    {
        private int _NamThanhTra;
        private string _NoiDung;
        private string _HoTen;
        private XepLoaiChuyenMon _XepLoaiSuPham;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _NhanXet;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Năm thanh tra")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int NamThanhTra
        {
            get
            {
                return _NamThanhTra;
            }
            set
            {
                SetPropertyValue("NamThanhTra", ref _NamThanhTra, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Nội dung thanh tra")]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
            }
        }

        [ModelDefault("Caption", "Thanh tra viên")]
        public string HoTen
        {
            get
            {
                return _HoTen;
            }
            set
            {
                SetPropertyValue("HoTen", ref _HoTen, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại sư phạm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public XepLoaiChuyenMon XepLoaiSuPham
        {
            get
            {
                return _XepLoaiSuPham;
            }
            set
            {
                SetPropertyValue("XepLoaiSuPham", ref _XepLoaiSuPham, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Nhận xét")]
        public string NhanXet
        {
            get
            {
                return _NhanXet;
            }
            set
            {
                SetPropertyValue("NhanXet", ref _NhanXet, value);
            }
        }

        public QuaTrinhThanhTraHoatDongGiangDay(Session session) :
            base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }
    }

}
