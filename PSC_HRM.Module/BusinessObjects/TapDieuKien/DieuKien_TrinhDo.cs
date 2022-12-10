using System;

using DevExpress.Xpo;

using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Trình độ nhân viên")]
    public class DieuKien_TrinhDo : BaseObject
    {
        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa { get; set; }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon { get; set; }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenMonDaoTao ChuyenMonDaoTao { get; set; }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao { get; set; }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao { get; set; }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep { get; set; }

        [ModelDefault("Caption", "Đang theo học")]
        public ChuongTrinhHoc ChuongTrinhHoc { get; set; }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc { get; set; }

        [ModelDefault("Caption", "Lý luận chính trị")]
        public LyLuanChinhTri LyLuanChinhTri { get; set; }

        [ModelDefault("Caption", "Quản lý giáo dục")]
        public QuanLyGiaoDuc QuanLyGiaoDuc { get; set; }

        [ModelDefault("Caption", "Quản lý nhà nước")]
        public QuanLyNhaNuoc QuanLyNhaNuoc { get; set; }

        [ModelDefault("Caption", "Quản lý kinh tế")]
        public QuanLyKinhTe QuanLyKinhTe { get; set; }

        [ModelDefault("Caption", "Ngoại ngữ")]
        public NgoaiNgu NgoaiNgu { get; set; }

        [ModelDefault("Caption", "Trình độ ngoại ngữ")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu { get; set; }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam { get; set; }

        [ModelDefault("Caption", "Năm công nhận")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamCongNhanHocHam { get; set; }

        [ModelDefault("Caption", "Danh hiệu được phong")]
        public DanhHieuDuocPhong DanhHieuCaoNhat { get; set; }

        [ModelDefault("Caption", "Ngày phong danh hiệu")]
        public DateTime NgayPhongDanhHieu { get; set; }

        public DieuKien_TrinhDo(Session session) : base(session) { }
    }

}
