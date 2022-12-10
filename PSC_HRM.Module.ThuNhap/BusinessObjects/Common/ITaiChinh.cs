using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.ThuNhap.Luong;
using DevExpress.Xpo;

namespace PSC_HRM.Module.ThuNhap
{
    public interface ITaiChinh
    {
        void XuLy(IObjectSpace obs, BaseObject obj, XPCollection<CongThucTinhLuong> congThucTinhLuongList);
    }
}
