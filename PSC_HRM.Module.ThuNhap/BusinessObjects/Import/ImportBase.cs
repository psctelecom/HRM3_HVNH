using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.ThuNhap.Import
{
    [NonPersistent]
    [ImageName("Act_Import1")]
    public abstract class ImportBase : BaseObject
    {
        /// <summary>
        /// contain other data if exists
        /// </summary>
        public object Data { get; set; }

        public ImportBase(Session session) : base(session) { }

        public abstract void XuLy(IObjectSpace obs, object obj);

        /// <summary>
        /// Get nhan vien by ma quan ly
        /// </summary>
        /// <param name="session"></param>
        /// <param name="maQuanLy"></param>
        /// <returns></returns>
        protected ThongTinNhanVien GetNhanVien(Session session, string maQuanLy, string hoTen)
        {
            ThongTinNhanVien nhanVien = null;
            if (TruongConfig.MaTruong.Equals("UTE") || TruongConfig.MaTruong.Equals("QNU"))
            {
                nhanVien = session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ListTaiKhoanNganHang[SoTaiKhoan=?]", maQuanLy));
            }
            else
            {
                nhanVien = session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("MaQuanLy=? or SoHieuCongChuc=? or CMND = ?", maQuanLy, maQuanLy, maQuanLy));
            }
            return nhanVien;
        }
    }

}
