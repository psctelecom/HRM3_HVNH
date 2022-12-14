using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chuyển xếp lương")]

    [Appearance("Hide_QNU", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_UFM", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='UFM'")]
    [Appearance("Hide_DNU", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DNU'")]
    [Appearance("Hide_UEL", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UEL'")]
    [Appearance("Hide_HVNH", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HVNH'")]
    [Appearance("Hide_VHU", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='VHU'")]
    [Appearance("Hide_HUFLIT", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HUFLIT'")]

    public class QuyetDinhChuyenXepLuong : QuyetDinh
    {
        private string _SoBanSao;
        private ChucVu _ChucVuNguoiKyBanSao;
        private ThongTinNhanVien _NguoiKyBanSao;
        private string _GhiChu;
        private DateTime _NgayPhatSinhBienDong;
        private DateTime _NgayHopHoiDongLuong;
        private bool _QuyetDinhMoi;
        //private string _LuuTru;

        [ModelDefault("Caption", "Số bản sao")]
        public string SoBanSao
        {
            get
            {
                return _SoBanSao;
            }
            set
            {
                SetPropertyValue("SoBanSao", ref _SoBanSao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ người ký bản sao")]
        [DataSourceProperty("ChucVuList")]
        public ChucVu ChucVuNguoiKyBanSao
        {
            get
            {
                return _ChucVuNguoiKyBanSao;
            }
            set
            {
                SetPropertyValue("ChucVuNguoiKyBanSao", ref _ChucVuNguoiKyBanSao, value);
                if (!IsLoading)
                {
                    UpdateNguoiKyBanSaoList();
                    NguoiKy = null;

                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Người ký bản sao")]
        [DataSourceProperty("NguoiKyBanSaoList", DataSourcePropertyIsNullMode.SelectAll)]
        public ThongTinNhanVien NguoiKyBanSao
        {
            get
            {
                return _NguoiKyBanSao;
            }
            set
            {
                SetPropertyValue("NguoiKyBanSao", ref _NguoiKyBanSao, value);
            }
        }

        [Browsable(false)]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Ngày phát sinh biến động")]
        public DateTime NgayPhatSinhBienDong
        {
            get
            {
                return _NgayPhatSinhBienDong;
            }
            set
            {
                SetPropertyValue("NgayPhatSinhBienDong", ref _NgayPhatSinhBienDong, value);
            }
        }

        [ModelDefault("Caption", "Ngày họp hội đồng lương")]
        public DateTime NgayHopHoiDongLuong
        {
            get
            {
                return _NgayHopHoiDongLuong;
            }
            set
            {
                SetPropertyValue("NgayHopHoiDongLuong", ref _NgayHopHoiDongLuong, value);
            }
        }

        [ModelDefault("Caption", "Quyết định còn hiệu lực")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
                if (!IsLoading)
                {
                    foreach (ChiTietQuyetDinhChuyenXepLuong item in this.ListChiTietQuyetDinhchuyenXepLuong)
                    {
                        //Đối với quyết định đã lập rồi giờ chỉ cập nhật lại quyết định có hiệu lực thì set tất cả các con
                        item.QuyetDinhMoi = true;
                    }
                }
            }
        }

        //Không sử dụng
        //[ModelDefault("Caption", "Lưu trữ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        //public string LuuTru
        //{
        //    get
        //    {
        //        return _LuuTru;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LuuTru", ref _LuuTru, value);
        //    }
        //}

        [Size(200)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhChuyenXepLuong-ListChiTietQuyetDinhchuyenXepLuong")]
        public XPCollection<ChiTietQuyetDinhChuyenXepLuong> ListChiTietQuyetDinhchuyenXepLuong
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhChuyenXepLuong>("ListChiTietQuyetDinhchuyenXepLuong");
            }
        }

        public QuyetDinhChuyenXepLuong(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuNguoiKyBanSaoList { get; set; }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NguoiKyBanSaoList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNangLuong;

            if (MaTruong.Equals("NEU"))
            {
                ChucVuNguoiKyBanSao = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Trưởng phòng"));
                NguoiKyBanSao = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("BoPhan.TenBoPhan like ? and ChucVu.TenChucVu like ? and TinhTrang.TenTinhTrang like ? and ThongTinTruong=?", "Phòng Tổ chức cán bộ", "Trưởng phòng", "Đang làm việc", ThongTinTruong.Oid));
                UpdateChucVuNguoiKyBanSaoList();
                UpdateNguoiKyBanSaoList();
            }            
        }
        private void UpdateChucVuNguoiKyBanSaoList()
        {
            if (ChucVuNguoiKyBanSaoList == null)
                ChucVuNguoiKyBanSaoList = new XPCollection<ChucVu>(Session);

            if (CoQuanRaQuyetDinh != CoQuanRaQuyetDinhEnum.TruongRaQuyetDinh)
                ChucVuNguoiKyBanSaoList.Criteria = CriteriaOperator.Parse("PhanLoai=2 or PhanLoai=1");
            else
                ChucVuNguoiKyBanSaoList.Criteria = CriteriaOperator.Parse("PhanLoai=2 or PhanLoai=0 or PhanLoai is null");
        }

        private void UpdateNguoiKyBanSaoList()
        {            
            NguoiKyBanSaoList = new XPCollection<ThongTinNhanVien>(Session);

            if (ChucVuNguoiKyBanSao != null)
                NguoiKyBanSaoList.Criteria = HamDungChung.GetNguoiKyTenCriteria(PhanLoaiNguoiKy, ChucVuNguoiKyBanSao);
        }

        private void UpdateGiayToList()
        {
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                if (ListChiTietQuyetDinhchuyenXepLuong.Count == 1)
                    GiayToList = ListChiTietQuyetDinhchuyenXepLuong[0].ThongTinNhanVien.ListGiayToHoSo;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateGiayToList();
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //luu giay to ho so
                if (GiayToHoSo != null)
                {
                    foreach (ChiTietQuyetDinhChuyenXepLuong item in ListChiTietQuyetDinhchuyenXepLuong)
                    {
                        if (item.GiayToHoSo != null)
                        {
                            //luu tru giay to ho so                            
                            item.GiayToHoSo.QuyetDinh = this;                           
                            item.GiayToHoSo.SoGiayTo = SoQuyetDinh;
                            item.GiayToHoSo.NgayLap = NgayQuyetDinh;
                            item.GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                            item.GiayToHoSo.TrichYeu = NoiDung;
                            item.GiayToHoSo.LuuTru = GiayToHoSo.LuuTru;
                            item.GiayToHoSo.DuongDanFile = GiayToHoSo.DuongDanFile;

                            CriteriaOperator filter = CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chuyển xếp lương");
                            DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(filter);
                            item.GiayToHoSo.GiayTo = giayTo;
                        }
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhchuyenXepLuong.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhchuyenXepLuong[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhchuyenXepLuong[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }
    }

}
