using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Quá trình giảng dạy")]
    public class QuaTrinhGiangDay : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private int _Nam;
        private string _Lop;
        private string _KetQuaKhaoSatDauNam;
        private string _KetQuaKhaoSatCuoiNam;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
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

        [ModelDefault("Caption", "Năm")]
        [RuleRequiredField("",DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [ModelDefault("Caption", "Lớp dạy")]
        public string Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }

        [ModelDefault("Caption", "Kết quả khảo sát HS đầu năm")]
        public string KetQuaKhaoSatDauNam
        {
            get
            {
                return _KetQuaKhaoSatDauNam;
            }
            set
            {
                SetPropertyValue("KetQuaKhaoSatDauNam", ref _KetQuaKhaoSatDauNam, value);
            }
        }
        
        [ModelDefault("Caption", "Kết quả khảo sát HS cuối năm")]
        public string KetQuaKhaoSatCuoiNam
        {
            get
            {
                return _KetQuaKhaoSatCuoiNam;
            }
            set
            {
                SetPropertyValue("KetQuaKhaoSatCuoiNam", ref _KetQuaKhaoSatCuoiNam, value);
            }
        }

        public QuaTrinhGiangDay(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }
    }

}
