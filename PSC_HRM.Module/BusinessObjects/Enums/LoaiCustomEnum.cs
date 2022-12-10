using System;

namespace PSC_HRM.Module
{
    public enum LoaiCustomEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên đang công tác tại trường")]
        ConCongTacTaiTruong = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên không còn công tác tại trường")]
        KhongConCongTacTaiTruong = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Nhân viên công nhật")]
        NhanVienCongNhat =2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tổ chức đảng")]
        ToChucDang = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên thỉnh giảng")]
        GiangVienThinhGiang = 4,
        [DevExpress.ExpressApp.DC.XafDisplayName("Giảng viên thỉnh giảng nghỉ việc")]
        GiangVienThinhGiangNghiViec = 5
    }
}
