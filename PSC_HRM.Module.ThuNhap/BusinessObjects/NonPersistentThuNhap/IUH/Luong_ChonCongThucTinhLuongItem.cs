using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.ThuNhap.Luong;

namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn công thức tính lương")]
    public class Luong_ChonCongThucTinhLuongItem : BaseObject
    {
        // Fields...
        private CongThucTinhLuong _CongThucTinhLuong;
        private bool _Chon;

        [ModelDefault("Caption", "Chọn")]
        [ModelDefault("AllowEdit", "True")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Công thức tính lương")]
        [ModelDefault("AllowEdit", "True")]
        public CongThucTinhLuong CongThucTinhLuong
        {
            get
            {
                return _CongThucTinhLuong;
            }
            set
            {
                SetPropertyValue("CongThucTinhLuong", ref _CongThucTinhLuong, value);
            }
        }

        public Luong_ChonCongThucTinhLuongItem(Session session)
            : base(session)
        { }
    }

}
