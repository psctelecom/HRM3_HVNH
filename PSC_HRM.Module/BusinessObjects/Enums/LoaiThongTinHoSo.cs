using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiThongTinHoSoUTE : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Thông tin sức khỏe")]
        ThongTinSucKhoe = 1
    }
    public enum LoaiThongTinHoSoDLU : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Tình trạng")]
        TinhTrang = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chức vụ")]
        ChucVu = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Loại hợp đồng")]
        LoaiHopDong = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Loại loại nhân sự")]
        LoaiNhanSu = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Công việc hiện nay")]
        CongViecHienNay = 5,
    }
}
