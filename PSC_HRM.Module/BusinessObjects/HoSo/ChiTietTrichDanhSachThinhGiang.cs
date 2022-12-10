using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.HoSo
{
    [NonPersistent]
    [ImageName("BO_Extract")]
    [ModelDefault("Caption", "Giảng viên thỉnh giảng")]
    public class ChiTietTrichDanhSachThinhGiang : BaseObject, IBoPhan, ISupportController
    {
        private GiangVienThinhGiang _ThinhGiang;

        //0
        [ModelDefault("Caption", "Mã đơn vị")]
        public string MaDonVi
        {
            get
            {
                return ThinhGiang.BoPhan.MaQuanLy;
            }
        }

        //1
        [ModelDefault("Caption", "Tên đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return ThinhGiang.BoPhan;
            }
        }

        //2
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return ThinhGiang.MaQuanLy;
            }
        }

        //3
        [ModelDefault("Caption", "Họ và tên")]
        public GiangVienThinhGiang ThinhGiang
        {
            get
            {
                return _ThinhGiang;
            }
            set
            {
                SetPropertyValue("ThinhGiang", ref _ThinhGiang, value);
            }
        }

        //10
        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return ThinhGiang.TenGoiKhac;
            }
        }

        //5
        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return ThinhGiang.GioiTinh;
            }
        }

        //6
        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return ThinhGiang.NgaySinh;
            }
        }



        [Aggregated]
        [ModelDefault("Caption", "Nơi sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiSinh
        {
            get
            {
                return ThinhGiang.NoiSinh;
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return ThinhGiang.NgayCap;
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return ThinhGiang.NoiCap;
            }
        }

        [ModelDefault("Caption", "Số hộ chiếu")]
        public string SoHoChieu
        {
            get
            {
                return ThinhGiang.SoHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày cấp hộ chiếu")]
        public DateTime NgayCapHoChieu
        {
            get
            {
                return ThinhGiang.NgayCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Nơi cấp hộ chiếu")]
        public string NoiCapHoChieu
        {
            get
            {
                return ThinhGiang.NoiCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return ThinhGiang.NgayHetHan;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quê quán")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi QueQuan
        {
            get
            {
                return ThinhGiang.QueQuan;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Địa chỉ thường trú")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi DiaChiThuongTru
        {
            get
            {
                return ThinhGiang.DiaChiThuongTru;
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Nơi ở hiện nay")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiOHienNay
        {
            get
            {
                return ThinhGiang.NoiOHienNay;
            }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get
            {
                return ThinhGiang.Email;
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return ThinhGiang.DienThoaiDiDong;
            }
        }

        [ModelDefault("Caption", "Điện thoại nhà riêng")]
        public string DienThoaiNhaRieng
        {
            get
            {
                return ThinhGiang.DienThoaiNhaRieng;
            }
        }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan
        {
            get
            {
                return ThinhGiang.TinhTrangHonNhan;
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return ThinhGiang.DanToc;
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return ThinhGiang.TonGiao;
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich
        {
            get
            {
                return ThinhGiang.QuocTich;
            }
        }

        [ModelDefault("Caption", "Thành phần xuất thân")]
        public ThanhPhanXuatThan ThanhPhanXuatThan
        {
            get
            {
                return ThinhGiang.ThanhPhanXuatThan;
            }
        }

        [ModelDefault("Caption", "Ưu tiên gia đình")]
        public UuTienGiaDinh UuTienGiaDinh
        {
            get
            {
                return ThinhGiang.UuTienGiaDinh;
            }
        }

        [ModelDefault("Caption", "Ưu tiên bản thân")]
        public UuTienBanThan UuTienBanThan
        {
            get
            {
                return ThinhGiang.UuTienBanThan;
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay
        {
            get
            {
                return ThinhGiang.CongViecHienNay;
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        public HopDong.HopDong HopDongHienTai
        {
            get
            {
                return ThinhGiang.HopDongHienTai;
            }
        }

        [ModelDefault("Caption", "Ngày vào ngành")]
        public DateTime NgayVaoNganhGiaoDuc
        {
            get
            {
                return ThinhGiang.NgayVaoNganhGiaoDuc;
            }
        }

        [ModelDefault("Caption", "Hình thức tuyển dụng")]
        public HinhThucTuyenDungEnum HinhThucTuyenDung
        {
            get
            {
                return ThinhGiang.HinhThucTuyenDung;
            }
        }

        [ModelDefault("Caption", "Ngày tuyển dụng")]
        public DateTime NgayTuyenDung
        {
            get
            {
                return ThinhGiang.NgayTuyenDung;
            }
        }

        [ModelDefault("Caption", "Đơn vị tuyển dụng")]
        public string DonViTuyenDung
        {
            get
            {
                return ThinhGiang.DonViTuyenDung;
            }
        }

        [ModelDefault("Caption", "Công việc tuyển dụng")]
        public string CongViecTuyenDung
        {
            get
            {
                return ThinhGiang.CongViecTuyenDung;
            }
        }

        [ModelDefault("Caption", "Công việc được giao")]
        public CongViec CongViecDuocGiao
        {
            get
            {
                return ThinhGiang.CongViecDuocGiao;
            }
        }

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return ThinhGiang.NgayVaoCoQuan;
            }
        }

        [ModelDefault("Caption", "Đơn vị công tác")]
        public string DonViCongTac
        {
            get
            {
                return ThinhGiang.DonViCongTac;
            }
        }

        [ModelDefault("Caption", "Tài liệu giảng dạy")]
        public string TaiLieuGiangDay
        {
            get
            {
                return ThinhGiang.TaiLieuGiangDay;
            }
        }

        //37
        private string _SoTaiKhoan;
        [ModelDefault("Caption", "Số tài khoản")]
        [ImmediatePostData()]
        public string SoTaiKhoan
        {
            get
            {
                foreach (TaiKhoanNganHang TK in ThinhGiang.ListTaiKhoanNganHang)
                {
                    if (TK.TaiKhoanChinh)
                    {
                        _SoTaiKhoan = TK.SoTaiKhoan;
                        _NganHang = TK.NganHang;
                        break;
                    }
                }
                return _SoTaiKhoan;
            }
        }

        //38
        private NganHang _NganHang;
        [ModelDefault("Caption", "Ngân hàng mở tài khoản")]
        public NganHang NganHang
        {
            get
            {
                return _NganHang;
            }
        }



        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoVanHoa;
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoChuyenMon;
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.ChuyenMonDaoTao;
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TruongDaoTao;
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.HinhThucDaoTao;
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.NamTotNghiep;
            }
        }

        [ModelDefault("Caption", "Hiện đang theo học")]
        public ChuongTrinhHoc ChuongTrinhHoc
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.ChuongTrinhHoc;
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoTinHoc;
            }
        }

        [ModelDefault("Caption", "Ngoại ngữ chính")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.NgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.TrinhDoNgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.HocHam;
            }
        }

        [ModelDefault("Caption", "Năm công nhận")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamCongNhanHocHam
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.NamCongNhanHocHam;
            }
        }

        [ModelDefault("Caption", "Danh hiệu được phong")]
        public DanhHieuDuocPhong DanhHieuCaoNhat
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.DanhHieuCaoNhat;
            }
        }

        [ModelDefault("Caption", "Ngày công nhận danh hiệu")]
        public DateTime NgayPhongDanhHieu
        {
            get
            {
                return ThinhGiang.NhanVienTrinhDo.NgayPhongDanhHieu;
            }
        }

        [ModelDefault("Caption", "Không cư trú")]
        public bool KhongCuTru
        {
            get
            {
                return ThinhGiang.NhanVienThongTinLuong.KhongCuTru;
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return ThinhGiang.NhanVienThongTinLuong.MaSoThue;
            }
        }

        [ModelDefault("Caption", "Cơ quan thuế")]
        public CoQuanThue CoQuanThue
        {
            get
            {
                return ThinhGiang.NhanVienThongTinLuong.CoQuanThue;
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return ThinhGiang.TinhTrang;
            }
        }

        //106
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return ThinhGiang.GhiChu;
            }
        }

        public ChiTietTrichDanhSachThinhGiang(Session session) : base(session) { }
    }
}
