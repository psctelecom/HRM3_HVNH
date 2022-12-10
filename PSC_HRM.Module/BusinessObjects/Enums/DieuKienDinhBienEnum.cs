using System;

namespace PSC_HRM.Module
{
    public enum DieuKienDinhBienEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("Trình độ chuyên môn")]
        TrinhDoChuyenMon,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trình độ ngoại ngữ")]
        TrinhDoNgoaiNgu,
        [DevExpress.ExpressApp.DC.XafDisplayName("Trình độ tin học")]
        TrinhDoTinHoc,
        [DevExpress.ExpressApp.DC.XafDisplayName("Học hàm")]
        HocHam,
        [DevExpress.ExpressApp.DC.XafDisplayName("Lý luận chính trị")]
        LyLuanChinhTri,
        [DevExpress.ExpressApp.DC.XafDisplayName("Loại biên chế")]
        LoaiBienChe,
        [DevExpress.ExpressApp.DC.XafDisplayName("Dân tộc")]
        DanToc,
        [DevExpress.ExpressApp.DC.XafDisplayName("Tôn giáo")]
        TonGiao,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đảng viên")]
        DangVien,
        [DevExpress.ExpressApp.DC.XafDisplayName("Đoàn viên")]
        DoanVien,
        [DevExpress.ExpressApp.DC.XafDisplayName("Biên chế")]
        BienChe,
        [DevExpress.ExpressApp.DC.XafDisplayName("Số năm kinh nghiệm")]
        SoNamKinhNghiem
    }
}
