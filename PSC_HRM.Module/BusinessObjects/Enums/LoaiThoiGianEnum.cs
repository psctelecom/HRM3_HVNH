using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    //Trung: Đưa enum Trong_NgoaiNuocEnum ra ngoài để sử dụng bên report
    //Trung: Thêm enum cho người dùng chọn thời gian đào tạo, gia hạn 
    //là ngày hoặc tháng

    public enum LoaiThoiGianEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo ngày")]
        TheoNgay = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Theo tháng")]
        TheoThang = 1
    }
}
