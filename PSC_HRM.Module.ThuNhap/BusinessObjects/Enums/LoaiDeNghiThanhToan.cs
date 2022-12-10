using System;
using DevExpress.Xpo;

namespace PSC_HRM.Module
{
    public enum LoaiDeNghiThanhToan : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Chi nộp thuế thu nhập cá nhân")]
        ChiNopThueThuNhapCaNhan = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chi nộp bảo hiểm")]
        ChiNopBaoHiem = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chi tiền công đoàn")]
        ChiTienCongDoan = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chi tiền lương tiền mặt")]
        ChiTienLuongTienMat = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("Chi tiền lương chuyển khoản")]
        ChiTienLuongChuyenKhoan = 4,
    }
}
