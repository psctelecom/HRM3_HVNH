using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{
    [ModelDefault("Caption", "Nhân viên - vai trò - khảo thí")]

    [DefaultProperty("ThongTin")]

    public class NhanVien_VaoTroKhaoThi : BaseObject
    {
        private NhanVien _NhanVien;
        private VaiTro_KhaoThi _VaiTro;

        [ModelDefault("Caption", "Nhân viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Vai trò")]
        [ImmediatePostData]
        public VaiTro_KhaoThi VaiTro
        {
            get { return _VaiTro; }
            set { SetPropertyValue("VaiTro", ref _VaiTro, value); }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1}", NhanVien != null ? NhanVien.HoTen : "", VaiTro != null ? " - " + VaiTro.TenVaiTro : "");
            }
        }
        public NhanVien_VaoTroKhaoThi(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
    }

}