using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Tham gia lực lượng vũ trang")]
    public class ThamGiaLucLuongVuTrang : BaseObject
    {
        private int _STT;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _NgayNhapNgu;
        private string _NgayXuatNgu;
        private QuanHam _QuanHam;
        private string _NoiDung;

        [Browsable(false)]
        [ImmediatePostData]
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
                if (!IsLoading && value != null)
                {
                    object obj = Session.Evaluate<ThamGiaLucLuongVuTrang>(CriteriaOperator.Parse("COUNT()"), CriteriaOperator.Parse("ThongTinNhanVien=?", value));
                    if (obj != null)
                        STT = (int)obj + 1;
                    else
                        STT = 1;
                }
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Ngày nhập ngũ")]
        public string NgayNhapNgu
        {
            get
            {
                return _NgayNhapNgu;
            }
            set
            {
                SetPropertyValue("NgayNhapNgu", ref _NgayNhapNgu, value);
            }
        }

        [ModelDefault("Caption", "Ngày xuất ngũ")]
        public string NgayXuatNgu
        {
            get
            {
                return _NgayXuatNgu;
            }
            set
            {
                SetPropertyValue("NgayXuatNgu", ref _NgayXuatNgu, value);
            }
        }

        [ModelDefault("Caption", "Quân hàm")]
        public QuanHam QuanHam
        {
            get
            {
                return _QuanHam;
            }
            set
            {
                SetPropertyValue("QuanHam", ref _QuanHam, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Nội dung")]
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

        public ThamGiaLucLuongVuTrang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }
    }

}
