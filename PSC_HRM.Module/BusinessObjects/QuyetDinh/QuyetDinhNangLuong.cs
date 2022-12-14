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
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng lương")]

    [Appearance("Hide_QNU", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'QNU'")]
    [Appearance("Hide_UFM", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='UFM'")]
    [Appearance("Hide_DNU", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DNU'")]
    [Appearance("Hide_UEL", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UEL'")]
    [Appearance("Hide_HVNH", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HVNH'")]
    [Appearance("Hide_VHU", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='VHU'")]
    [Appearance("Hide_HUFLIT", TargetItems = "SoBanSao;ChucVuNguoiKyBanSao;NguoiKyBanSao;NgayHopHoiDongLuong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HUFLIT'")]

    public class QuyetDinhNangLuong : QuyetDinh
    {
        private string _SoBanSao;
        private ChucVu _ChucVuNguoiKyBanSao;
        private ThongTinNhanVien _NguoiKyBanSao;
        private string _GhiChu;
        private DeNghiNangLuong _DeNghiNangLuong;
        private DateTime _NgayPhatSinhBienDong;
        private DateTime _NgayHopHoiDongLuong;
        private bool _QuyetDinhMoi;
        private bool _Import;
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

        [ModelDefault("Caption", "Đề nghị nâng lương")]
        public DeNghiNangLuong DeNghiNangLuong
        {
            get
            {
                return _DeNghiNangLuong;
            }
            set
            {
                SetPropertyValue("DeNghiNangLuong", ref _DeNghiNangLuong, value);
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
                    foreach (ChiTietQuyetDinhNangLuong item in this.ListChiTietQuyetDinhNangLuong)
                    {
                        //Đối với quyết định đã lập rồi giờ chỉ cập nhật lại quyết định có hiệu lực thì set tất cả các con
                        item.QuyetDinhMoi = true;
                    }
                }
            }
        }


        [ModelDefault("Caption", "Import")]
        [NonPersistent]
        [Browsable(false)]
        public bool Imporrt
        {
            get
            {
                return _Import;
            }
            set
            {
                SetPropertyValue("Imporrt", ref _Import, value);
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
        [Association("QuyetDinhNangLuong-ListChiTietQuyetDinhNangLuong")]
        public XPCollection<ChiTietQuyetDinhNangLuong> ListChiTietQuyetDinhNangLuong
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangLuong>("ListChiTietQuyetDinhNangLuong");
            }
        }

        public QuyetDinhNangLuong(Session session) : base(session) { }

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

        public void CreateListChiTietQuyetDinhNangLuong(HoSo_NhanVienItem item)
        {
            ChiTietQuyetDinhNangLuong chiTiet = new ChiTietQuyetDinhNangLuong(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            chiTiet.MocNangLuongCu = item.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh != null ? item.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh : item.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong;
            chiTiet.NgayHuongLuongCu = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;

            this.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
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
                if (ListChiTietQuyetDinhNangLuong.Count == 1)
                    GiayToList = ListChiTietQuyetDinhNangLuong[0].ThongTinNhanVien.ListGiayToHoSo;
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
                    foreach (ChiTietQuyetDinhNangLuong item in ListChiTietQuyetDinhNangLuong)
                    {
                        if(Imporrt)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", item.ThongTinNhanVien.Oid);
                            HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);

                            if (QuyetDinhMoi)
                            {
                                //quản lý biến động (chi cập nhật khi có thông tin lương bị thay đổi)
                                //tăng mức đóng
                                if (hoSoBaoHiem != null &&
                                    NgayPhatSinhBienDong != DateTime.MinValue)
                                    BienDongHelper.CreateBienDongThayDoiLuong(Session, this,
                                        item.BoPhan, item.ThongTinNhanVien,
                                        NgayPhatSinhBienDong,
                                        item.HeSoLuongMoi, item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu,
                                        item.VuotKhungMoi, item.ThongTinNhanVien.NhanVienThongTinLuong.ThamNien,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                                        item.ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong);

                                //cập nhật thông tin vào hồ sơ
                                if (item.NgayHuongLuongMoi <= HamDungChung.GetServerTime())
                                {
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = item.NgachLuong;
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = item.BacLuongMoi;
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = item.HeSoLuongMoi;
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = item.VuotKhungMoi;
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = item.NgayHuongLuongMoi;
                                    item.ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = item.MocNangLuongMoi;
                                }
                            }

                            //update dien bien luong
                            if (item.NgayHuongLuongMoi != DateTime.MinValue && ((item.NgachLuong != null && item.BacLuongCu != null)))
                            {
                                filter = CriteriaOperator.Parse("ThongTinNhanVien=?", item.ThongTinNhanVien.Oid);

                                ChiTietQuyetDinhNangLuong chiTiet = Session.FindObject<ChiTietQuyetDinhNangLuong>(filter);
                                if (chiTiet != null)
                                {
                                    filter = CriteriaOperator.Parse("QuyetDinh = ?",
                                        chiTiet.QuyetDinhNangLuong.Oid);
                                    DienBienLuong updateDienBienLuong = Session.FindObject<DienBienLuong>(filter);
                                    if (updateDienBienLuong != null)
                                        updateDienBienLuong.DenNgay = item.NgayHuongLuongMoi.AddDays(-1);
                                }
                            }

                            //tạo mới diễn biến lương
                            if (item.NgayHuongLuongMoi != DateTime.MinValue)
                            {
                                QuaTrinhHelper.CreateDienBienLuong(Session, this, item.ThongTinNhanVien, item.NgayHuongLuongMoi, item.Oid);

                                //Bảo hiểm xã hội
                                if (hoSoBaoHiem != null)
                                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, this, hoSoBaoHiem, item.NgayHuongLuongMoi);
                            }

                        }

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

                            CriteriaOperator filter = CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nâng lương");
                            DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(filter);
                            item.GiayToHoSo.GiayTo = giayTo;
                        }
                    }
                }

                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListChiTietQuyetDinhNangLuong.Count == 1)
                {
                    BoPhanText = ListChiTietQuyetDinhNangLuong[0].BoPhan.TenBoPhan;
                    NhanVienText = ListChiTietQuyetDinhNangLuong[0].ThongTinNhanVien.HoTen;
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
