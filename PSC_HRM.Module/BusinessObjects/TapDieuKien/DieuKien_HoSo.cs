using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.TapDieuKien
{
    [NonPersistent]
    [ModelDefault("Caption", "Sơ yếu lý lịch 1")]
    public class DieuKien_HoSo : BaseObject
    {
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy { get; set; }

        [ModelDefault("Caption", "Họ")]
        public string Ho { get; set; }

        [ModelDefault("Caption", "Tên")]
        public string Ten { get; set; }

        [ModelDefault("Caption", "Họ và tên")]
        public string HoTen { get; set; }

        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac { get; set; }

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh { get; set; }

        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [ModelDefault("Caption", "Nơi ở hiện nay")]
        public DiaChi NoiOHienNay { get; set; }

        [ModelDefault("Caption", "Sinh nhật")]
        public bool SinhNhat { get; set; }

        [ModelDefault("Caption", "Tuổi")]
        public int Tuoi { get; set; }

        [ModelDefault("Caption", "Thâm niên làm việc")]
        public int ThamNienLamViec { get; set; }

        [ModelDefault("Caption", "Số năm công tác tại trường")]
        public int SoNamCongTacTaiTruong { get; set; }

        [ModelDefault("Caption", "Số tháng công tác tại trường")]
        public int SoThangCongTacTaiTruong { get; set; }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich { get; set; }

        [ModelDefault("Caption", "CMND")]
        public string CMND { get; set; }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap { get; set; }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap { get; set; }

        [ModelDefault("Caption", "Email")]
        public string Email { get; set; }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong { get; set; }

        [ModelDefault("Caption", "ĐT nhà riêng")]
        public string DienThoaiNhaRieng { get; set; }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan { get; set; }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc { get; set; }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao { get; set; }

        [ModelDefault("Caption", "Thành phần gia đình")]
        public ThanhPhanXuatThan ThanhPhanXuatThan { get; set; }

        [ModelDefault("Caption", "Ưu tiên gia đình")]
        public UuTienGiaDinh UuTienGiaDinh { get; set; }

        [ModelDefault("Caption", "Ưu tiên của bản thân")]
        public UuTienBanThan UuTienBanThan { get; set; }

        [ModelDefault("Caption", "Hình thức tuyển dụng")]
        public HinhThucTuyenDungEnum HinhThucTuyenDung { get; set; }

        public DieuKien_HoSo(Session session) : base(session) { }
    }

}
