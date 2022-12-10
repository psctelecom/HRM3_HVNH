using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;


namespace PSC_HRM.Module.HoSo
{
    [NonPersistent]
    [ImageName("BO_Extract")]
    [ModelDefault("Caption", "Thông tin cán bộ CH-TG")]
    public class
        ChiTietTrichDanhSachNhanVienCH_TG : BaseObject, IBoPhan, ISupportController
    {
        private NhanVien _NhanVien;

        //0
        [ModelDefault("Caption", "Mã đơn vị")]
        public string MaDonVi
        {
            get
            {
                return NhanVien.BoPhan.MaQuanLy;
            }
        }

        //1
        [ModelDefault("Caption", "Tên đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return NhanVien.BoPhan;
            }
        }

        //1
        [ModelDefault("Caption", "Tại Khoa/Bộ môn")]
        public BoPhan TaiBoMon
        {
            get
            {
                //return NhanVien.TaiBoMon;
                BoPhan _TaiBoMon = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _TaiBoMon = nhanvien.TaiBoMon;
                }
                return _TaiBoMon;
            }
        }

        //2
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get
            {
                return NhanVien.MaQuanLy;
            }
        }

        //3
        [ModelDefault("Caption", "Số hồ sơ")]
        public string SoHoSo
        {
            get
            {
                //return NhanVien.SoHoSo;
                string _SoHoSo = "";
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _SoHoSo = nhanvien.SoHoSo;
                }
                return _SoHoSo;
            }
        }

        //4
        [ModelDefault("Caption", "Số hiệu công chức")]
        public string SoHieuCongChuc
        {
            get
            {
                //return NhanVien.SoHieuCongChuc;
                string _SoHieuCongChuc = "";
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _SoHieuCongChuc = nhanvien.SoHieuCongChuc;
                }
                return _SoHieuCongChuc;
            }
        }
       
        [ModelDefault("Caption", "Họ")]
        public string Ho { get; set; }

        [ModelDefault("Caption", "Tên")]
        public string Ten { get; set; }

        //3
        [ModelDefault("Caption", "Họ và tên")]
        public NhanVien NhanVien
        {
            get
            {
                if (_NhanVien != null)
                {
                    Ho = _NhanVien.Ho;
                    Ten = _NhanVien.Ten;
                }
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (value != null)
                {
                    Ho = _NhanVien.Ho;
                    Ten = _NhanVien.Ten;
                }
            }
        }

        //10
        [ModelDefault("Caption", "Tên gọi khác")]
        public string TenGoiKhac
        {
            get
            {
                return NhanVien.TenGoiKhac;
            }
        }

        //5
        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return NhanVien.GioiTinh;
            }
        }

        //6
        [ModelDefault("Caption", "Ngày sinh")]
        public DateTime NgaySinh
        {
            get
            {
                return NhanVien.NgaySinh;
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
                return NhanVien.NoiSinh;
            }
        }

        [ModelDefault("Caption", "Số CMND")]
        public string CMND
        {
            get
            {
                return NhanVien.CMND;
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return NhanVien.NgayCap;
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public TinhThanh NoiCap
        {
            get
            {
                return NhanVien.NoiCap;
            }
        }

        [ModelDefault("Caption", "Số hộ chiếu")]
        public string SoHoChieu
        {
            get
            {
                return NhanVien.SoHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày cấp hộ chiếu")]
        public DateTime NgayCapHoChieu
        {
            get
            {
                return NhanVien.NgayCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Nơi cấp hộ chiếu")]
        public string NoiCapHoChieu
        {
            get
            {
                return NhanVien.NoiCapHoChieu;
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn")]
        public DateTime NgayHetHan
        {
            get
            {
                return NhanVien.NgayHetHan;
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
                return NhanVien.QueQuan;
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
                return NhanVien.DiaChiThuongTru;
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
                return NhanVien.NoiOHienNay;
            }
        }

        [ModelDefault("Caption", "Email")]
        public string Email
        {
            get
            {
                return NhanVien.Email;
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return NhanVien.DienThoaiDiDong;
            }
        }

        [ModelDefault("Caption", "Điện thoại nhà riêng")]
        public string DienThoaiNhaRieng
        {
            get
            {
                return NhanVien.DienThoaiNhaRieng;
            }
        }

        [ModelDefault("Caption", "Tình trạng hôn nhân")]
        public TinhTrangHonNhan TinhTrangHonNhan
        {
            get
            {
                return NhanVien.TinhTrangHonNhan;
            }
        }

        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return NhanVien.DanToc;
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return NhanVien.TonGiao;
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich
        {
            get
            {
                return NhanVien.QuocTich;
            }
        }

        [ModelDefault("Caption", "Hình thức tuyển dụng")]
        public HinhThucTuyenDungEnum HinhThucTuyenDung
        {
            get
            {
                return NhanVien.HinhThucTuyenDung;
            }
        }

        [ModelDefault("Caption", "Thành phần xuất thân")]
        public ThanhPhanXuatThan ThanhPhanXuatThan
        {
            get
            {
                return NhanVien.ThanhPhanXuatThan;
            }
        }

        [ModelDefault("Caption", "Ưu tiên gia đình")]
        public UuTienGiaDinh UuTienGiaDinh
        {
            get
            {
                return NhanVien.UuTienGiaDinh;
            }
        }

        [ModelDefault("Caption", "Ưu tiên bản thân")]
        public UuTienBanThan UuTienBanThan
        {
            get
            {
                return NhanVien.UuTienBanThan;
            }
        }

        [ModelDefault("Caption", "Công việc hiện nay")]
        public CongViec CongViecHienNay
        {
            get
            {
                return NhanVien.CongViecHienNay;
            }
        }

        [ModelDefault("Caption", "Hợp đồng hiện tại")]
        public HopDong.HopDong HopDongHienTai
        {
            get
            {
                return NhanVien.HopDongHienTai;
            }
        }

        [ModelDefault("Caption", "Ngày vào ngành")]
        public DateTime NgayVaoNganhGiaoDuc
        {
            get
            {
                return NhanVien.NgayVaoNganhGiaoDuc;
            }
        }

        [ModelDefault("Caption", "Ngày tuyển dụng")]
        public DateTime NgayTuyenDung
        {
            get
            {
                return NhanVien.NgayTuyenDung;
            }
        }

        [ModelDefault("Caption", "Đơn vị tuyển dụng")]
        public string DonViTuyenDung
        {
            get
            {
                return NhanVien.DonViTuyenDung;
            }
        }

        [ModelDefault("Caption", "Công việc tuyển dụng")]
        public string CongViecTuyenDung
        {
            get
            {
                return NhanVien.CongViecTuyenDung;
            }
        }

        [ModelDefault("Caption", "Công việc được giao")]
        public CongViec CongViecDuocGiao
        {
            get
            {
                return NhanVien.CongViecDuocGiao;
            }
        }

        [ModelDefault("Caption", "Ngày vào cơ quan")]
        public DateTime NgayVaoCoQuan
        {
            get
            {
                return NhanVien.NgayVaoCoQuan;
            }
        }

        [ModelDefault("Caption", "Biên chế")]
        public bool BienChe
        {
            get
            {
                //return NhanVien.BienChe;
                bool _BienChe = false;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _BienChe = nhanvien.BienChe;
                }
                return _BienChe;
            }
        }

        [ModelDefault("Caption", "Ngày vào biên chế")]
        public DateTime NgayVaoBienChe
        {
            get
            {
                //return NhanVien.NgayVaoBienChe;
                DateTime _NgayVaoBienChe = DateTime.MinValue;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _NgayVaoBienChe = nhanvien.NgayVaoBienChe;
                }
                return _NgayVaoBienChe;
            }
        }

        [ModelDefault("Caption", "Ngày sẽ nghỉ hưu")]
        public DateTime NgayNghiHuu
        {
            get
            {
                //return NhanVien.NgayNghiHuu;
                DateTime _NgayNghiHuu = DateTime.MinValue;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _NgayNghiHuu = nhanvien.NgayNghiHuu;
                }
                return _NgayNghiHuu;
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get
            {
                return NhanVien.ChucDanh;
            }
        }

        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                //return NhanVien.ChucVu;
                ChucVu _ChucVu = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _ChucVu = nhanvien.ChucVu;
                }
                return _ChucVu;
            }
        }

        [ModelDefault("Caption", "Lần bổ nhiệm chức vụ")]
        public int LanBoNhiemChucVu
        {
            get
            {
                //return NhanVien.LanBoNhiemChucVu;
                int _LanBoNhiemChucVu = 0;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _LanBoNhiemChucVu = nhanvien.LanBoNhiemChucVu;
                }
                return _LanBoNhiemChucVu;
            }
        }

        [ModelDefault("Caption", "Chức vụ kiêm nhiệm")]
        public ChucVu ChucVuKiemNhiem
        {
            get
            {
                //return NhanVien.ChucVuKiemNhiem;
                ChucVu _ChucVuKiemNhiem = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _ChucVuKiemNhiem = nhanvien.ChucVuKiemNhiem;
                }
                return _ChucVuKiemNhiem;
            }
        }

        [ModelDefault("Caption", "Loại cán bộ")]
        public LoaiNhanVien LoaiNhanVien
        {
            get
            {
                //return NhanVien.LoaiNhanVien;
                LoaiNhanVien _LoaiNhanVien = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _LoaiNhanVien = nhanvien.LoaiNhanVien;
                }
                return _LoaiNhanVien;
            }
        }

        [ModelDefault("Caption", "Loại nhân sự")]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                //return NhanVien.LoaiNhanSu;
                LoaiNhanSu _LoaiNhanSu = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _LoaiNhanSu = nhanvien.LoaiNhanSu;
                }
                return _LoaiNhanSu;
            }
        }

        [ModelDefault("Caption", "Điện thoại cơ quan")]
        public string DienThoaiCoQuan
        {
            get
            {
                //return NhanVien.DienThoaiCoQuan;
                string _DienThoaiCoQuan = "";
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _DienThoaiCoQuan = nhanvien.DienThoaiCoQuan;
                }
                return _DienThoaiCoQuan;
            }
        }

        [ModelDefault("Caption", "Chức vụ cao nhất")]
        public ChucVu ChucVuCoQuanCaoNhat
        {
            get
            {
                //return NhanVien.ChucVuCoQuanCaoNhat;
                ChucVu _ChucVuCoQuanCaoNhat = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _ChucVuCoQuanCaoNhat = nhanvien.ChucVuCoQuanCaoNhat;
                }
                return _ChucVuCoQuanCaoNhat;
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm")]
        public DateTime NgayBoNhiem
        {
            get
            {
                //return NhanVien.NgayBoNhiem;
                DateTime _NgayBoNhiem = DateTime.MinValue;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _NgayBoNhiem = nhanvien.NgayBoNhiem;
                }
                return _NgayBoNhiem;
            }
        }

        [ModelDefault("Caption", "Ngày tính thâm niên nhà giáo")]
        public DateTime NgayTinhThamNienNhaGiao
        {
            get
            {
                //return NhanVien.NgayTinhThamNienNhaGiao;
                DateTime _NgayTinhThamNienNhaGiao = DateTime.MinValue;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _NgayTinhThamNienNhaGiao = nhanvien.NgayTinhThamNienNhaGiao;
                }
                return _NgayTinhThamNienNhaGiao;
            }
        }

        [ModelDefault("Caption", "Nhóm máu")]
        public NhomMau NhomMau
        {
            get
            {
                //return NhanVien.NhomMau;
                NhomMau _NhomMau = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _NhomMau = nhanvien.NhomMau;
                }
                return _NhomMau;
            }
        }

        [ModelDefault("Caption", "Chiều cao (Cm)")]
        public int ChieuCao
        {
            get
            {
                //return NhanVien.ChieuCao;
                int _ChieuCao = 0;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _ChieuCao = nhanvien.ChieuCao;
                }
                return _ChieuCao;
            }
        }

        [ModelDefault("Caption", "Cân nặng (Kg)")]
        public int CanNang
        {
            get
            {
                //return NhanVien.CanNang;
                int _CanNang = 0;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _CanNang = nhanvien.CanNang;
                }
                return _CanNang;
            }
        }

        [ModelDefault("Caption", "Tình trạng sức khỏe")]
        public SucKhoe TinhTrangSucKhoe
        {
            get
            {
                SucKhoe _TinhTrangSucKhoe = null;
                ThongTinNhanVien nhanvien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", NhanVien.Oid));
                if (nhanvien != null)
                {
                    _TinhTrangSucKhoe = nhanvien.TinhTrangSucKhoe;
                }
                return _TinhTrangSucKhoe;
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
                foreach (TaiKhoanNganHang TK in NhanVien.ListTaiKhoanNganHang)
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

        //39
        private DateTime _NgayBatDauDongBHXH;
        [ModelDefault("Caption", "Ngày bắt đầu đóng BHXH")]
        public DateTime NgayBatDauDongBHXH
        {
            get
            {
                return _NgayBatDauDongBHXH;
            }
        }

        //40
        private string _SoBaoHiemXH;
        [ModelDefault("Caption", "Số BHXH")]
        public string SoBaoHiemXH
        {
            get
            {
                HoSoBaoHiem hoSo = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", NhanVien));
                if (hoSo != null)
                {
                    _SoBaoHiemXH = hoSo.SoSoBHXH;
                    _NgayBatDauDongBHXH = hoSo.NgayThamGiaBHXH;
                }
                return _SoBaoHiemXH;
            }
        }

        //57
        private string _DoanVienTNCSHCM;
        [ModelDefault("Caption", "Đoàn viên TNCS HCM")]
        public string DoanVienTNCSHCM
        {
            get
            {
                DoanVien doanVien = Session.FindObject<DoanVien>(CriteriaOperator.Parse("ThongTinNhanVien=?", NhanVien.Oid));
                if (doanVien != null)
                    _DoanVienTNCSHCM = "x";
                return _DoanVienTNCSHCM;
            }
        }

        //58
        [ModelDefault("Caption", "Chức vụ đoàn thể")]
        public ChucVuDoanThe ChucVuDoanThe
        {
            get
            {
                DoanThe doanThe = Session.FindObject<DoanThe>(CriteriaOperator.Parse("ThongTinNhanVien=?", NhanVien.Oid));
                if (doanThe != null)
                    return doanThe.ChucVuDoanThe;
                return null;
            }
        }

        //59
        DangVien dangVien;
        [ModelDefault("Caption", "Ngày vào Đảng")]
        public DateTime NgayVaoDuBiDang
        {
            get
            {
                dangVien = Session.FindObject<DangVien>(CriteriaOperator.Parse("ThongTinNhanVien=?", NhanVien.Oid));
                if (dangVien != null)
                    return dangVien.NgayDuBi;
                return DateTime.MinValue;
            }
        }

        //60
        [ModelDefault("Caption", "Ngày vào Đảng chính thức")]
        public DateTime NgayVaoDangChinhThuc
        {
            get
            {
                if (dangVien != null)
                    return dangVien.NgayVaoDangChinhThuc;
                return DateTime.MinValue;
            }
        }

        //61
        [ModelDefault("Caption", "Chức vụ Đảng")]
        public ChucVuDang ChucVuDang
        {
            get
            {
                if (dangVien != null)
                    return dangVien.ChucVuDang;
                return null;
            }
        }





        [ModelDefault("Caption", "Trình độ văn hóa")]
        public TrinhDoVanHoa TrinhDoVanHoa
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoVanHoa;
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoChuyenMon;
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.ChuyenMonDaoTao;
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TruongDaoTao;
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.HinhThucDaoTao;
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.NamTotNghiep;
            }
        }

        [ModelDefault("Caption", "Hiện đang theo học")]
        public ChuongTrinhHoc ChuongTrinhHoc
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.ChuongTrinhHoc;
            }
        }

        [ModelDefault("Caption", "Trình độ tin học")]
        public TrinhDoTinHoc TrinhDoTinHoc
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoTinHoc;
            }
        }

        [ModelDefault("Caption", "Ngoại ngữ chính")]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.NgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ chính")]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu;
            }
        }

        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.HocHam;
            }
        }

        [ModelDefault("Caption", "Năm công nhận")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamCongNhanHocHam
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.NamCongNhanHocHam;
            }
        }

        [ModelDefault("Caption", "Danh hiệu được phong")]
        public DanhHieuDuocPhong DanhHieuCaoNhat
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.DanhHieuCaoNhat;
            }
        }

        [ModelDefault("Caption", "Ngày công nhận danh hiệu")]
        public DateTime NgayPhongDanhHieu
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.NgayPhongDanhHieu;
            }
        }

        [ModelDefault("Caption", "Lý luận chính trị")]
        public LyLuanChinhTri LyLuanChinhTri
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.LyLuanChinhTri;
            }
        }

        [ModelDefault("Caption", "Quản lý giáo dục")]
        public QuanLyGiaoDuc QuanLyGiaoDuc
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.QuanLyGiaoDuc;
            }
        }

        [ModelDefault("Caption", "Quản lý Nhà nước")]
        public QuanLyNhaNuoc QuanLyNhaNuoc
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.QuanLyNhaNuoc;
            }
        }

        [ModelDefault("Caption", "Quản lý kinh tế")]
        public QuanLyKinhTe QuanLyKinhTe
        {
            get
            {
                return NhanVien.NhanVienTrinhDo.QuanLyKinhTe;
            }
        }




        [ModelDefault("Caption", "Không cư trú")]
        public bool KhongCuTru
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.KhongCuTru;
            }
        }

        [ModelDefault("Caption", "Số người phụ thuộc")]
        public int SoNguoiPhuThuoc
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.SoNguoiPhuThuoc;
            }
        }

        [ModelDefault("Caption", "Số tháng giảm trừ")]
        public int SoThangGiamTru
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.SoThangGiamTru;
            }
        }

        [ModelDefault("Caption", "Mã số thuế")]
        public string MaSoThue
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.MaSoThue;
            }
        }

        [ModelDefault("Caption", "Cơ quan thuế")]
        public CoQuanThue CoQuanThue
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.CoQuanThue;
            }
        }

        [ModelDefault("Caption", "Phân loại lương")]
        public ThongTinLuongEnum PhanLoai
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhanLoai;
            }
        }

        [ModelDefault("Caption", "Mã ngạch lương")]
        public string MaNgachLuong
        {
            get
            {
                    string MaNgachLuong = string.Empty;
                    MaNgachLuong = NhanVien.NhanVienThongTinLuong.NgachLuong != null ? NhanVien.NhanVienThongTinLuong.NgachLuong.MaQuanLy: string.Empty;
                return MaNgachLuong;
            }
        }

        [ModelDefault("Caption", "Tên ngạch lương")]
        public string TenNgachLuong
        {
            get
            {
                    string TenNgachLuong = string.Empty;
                    TenNgachLuong = NhanVien.NhanVienThongTinLuong.NgachLuong != null ? NhanVien.NhanVienThongTinLuong.NgachLuong.TenNgachLuong : string.Empty;
                return MaNgachLuong;
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương")]
        public DateTime NgayHuongLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            }
        }

        [ModelDefault("Caption", "Bậc lương")]
        public BacLuong BacLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.BacLuong;
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HeSoLuong;
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        public DateTime MocNangLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.MocNangLuong;
            }
        }

        [ModelDefault("Caption", "Hưởng 85%")]
        public bool Huong85PhanTramLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong;
            }
        }

        [ModelDefault("Caption", "Lương khoán")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal LuongKhoan
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.LuongKhoan;
            }
        }

        [ModelDefault("Caption", "HSPC chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCChucVu
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCChucVu;
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HSPCCV")]
        public DateTime NgayHuongHSPCChucVu
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu;
            }
        }

        [ModelDefault("Caption", "HSPC chức vụ bảo lưu")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCChucVuBaoLuu
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu;
            }
        }

        [ModelDefault("Caption", "HSPC độc hại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCDocHai
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCDocHai;
            }
        }

        [ModelDefault("Caption", "HSPC trách nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCTrachNhiem
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCTrachNhiem;
            }
        }

        [ModelDefault("Caption", "HSPC kiêm nhiệm")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCKiemNhiem
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCKiemNhiem;
            }
        }

        [ModelDefault("Caption", "HSPC ưu đãi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCUuDai
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCUuDai;
            }
        }

        [ModelDefault("Caption", "HSPC khác")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCKhac
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCKhac;
            }
        }

        [ModelDefault("Caption", "% PC ưu đãi")]
        public int PhuCapUuDai
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapUuDai;
            }
        }

        [ModelDefault("Caption", "Chênh lệch lương")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal ChenhLechBaoLuuLuong
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.ChenhLechBaoLuuLuong;
            }
        }

        [ModelDefault("Caption", "% vượt khung")]
        public int VuotKhung
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.VuotKhung;
            }
        }

        [ModelDefault("Caption", "HSPC vượt khung")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCVuotKhung
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCVuotKhung;
            }
        }

        [ImmediatePostData]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "% thâm niên")]
        public decimal ThamNien
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.ThamNien;
            }
        }

        [ModelDefault("Caption", "HSPC Thâm niên")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HSPCThamNien
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCThamNien;
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên")]
        public DateTime NgayHuongThamNien
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.NgayHuongThamNien;
            }
        }

        [ModelDefault("Caption", "Phụ cấp thu hút")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int PhuCapThuHut
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapThuHut;
            }
        }

        [ModelDefault("Caption", "HSPC CV Đảng")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal HSPCChucVuDang
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCChucVuDang;
            }
        }

        [ModelDefault("Caption", "HSPC CV Đoàn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal HSPCChucVuDoan
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCChucVuDoan;
            }
        }

        [ModelDefault("Caption", "Phụ cấp ưu đãi (CH)")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapUuDaiCoHuu
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCChucVuCongDoan;
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền ăn")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapTienAn
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapTienAn;
            }
        }

        [ModelDefault("Caption", "Phụ cấp điện thoại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal PhuCapDienThoai
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
            }
        }

        [ModelDefault("Caption", "HSPC học vị")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCHocVi
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.HSPCChuyenMon;
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrang TinhTrang
        {
            get
            {
                return NhanVien.TinhTrang;
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phương thức tính thuế")]
        public int PhuongThucTinhThue
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.PhuongThucTinhThue;
            }
        }

        [ModelDefault("Caption", "Tính thuế TNCN mặc định")]
        public bool TinhThueTNCNMacDinh
        {
            get
            {
                return NhanVien.NhanVienThongTinLuong.TinhThueTNCNMacDinh;
            }
        }

        //106
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return NhanVien.GhiChu;
            }
        }

        public ChiTietTrichDanhSachNhanVienCH_TG(Session session) : base(session) { }
    }
}
