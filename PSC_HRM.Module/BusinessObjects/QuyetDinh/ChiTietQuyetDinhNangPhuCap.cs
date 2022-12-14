using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định phụ cấp")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangPhuCap;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangPhuCap : TruongBaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhNangPhuCap _QuyetDinhNangPhuCap;
        private GiayToHoSo _GiayToHoSo;
        private bool _QuyetDinhMoi;

        //HBU
        private decimal _PhuCapTienXangCu;
        private decimal _PhuCapTienXangTruocDieuChinh;
        private decimal _PhuCapTienXangMoi;
        private decimal _PhuCapDienThoaiCu;
        private decimal _PhuCapDienThoaiTruocDieuChinh;
        private decimal _PhuCapDienThoaiMoi;
        private decimal _PhuCapTrachNhiemCongViecCu;
        private decimal _PhuCapTrachNhiemCongViecTruocDieuChinh;
        private decimal _PhuCapTrachNhiemCongViecMoi;
        private DateTime _NgayHuongPhuCapMoi;
        private DateTime _NgayHuongPhuCapCu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng phụ cấp")]
        [Association("QuyetDinhNangPhuCap-ListChiTietQuyetDinhNangPhuCap")]
        public QuyetDinhNangPhuCap QuyetDinhNangPhuCap
        {
            get
            {
                return _QuyetDinhNangPhuCap;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangPhuCap", ref _QuyetDinhNangPhuCap, value);
                //if (!IsLoading && value != null)
                //{
                //    GiayToHoSo.SoGiayTo = value.SoQuyetDinh;
                //    GiayToHoSo.NgayBanHanh = value.NgayHieuLuc;
                //    GiayToHoSo.LuuTru = value.LuuTru;
                //    GiayToHoSo.TrichYeu = value.NoiDung;
                //}
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                    BoPhanText = value.TenBoPhan;

                }
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }
        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                    AfterNhanVienChanged();
                }
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền xăng cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTienXangCu
        {
            get
            {
                return _PhuCapTienXangCu;
            }
            set
            {
                SetPropertyValue("PhuCapTienXangCu", ref _PhuCapTienXangCu, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp tiền xăng mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTienXangMoi
        {
            get
            {
                return _PhuCapTienXangMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTienXangMoi", ref _PhuCapTienXangMoi, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTienXangTruocDieuChinh
        {
            get
            {
                return _PhuCapTienXangTruocDieuChinh;
            }
            set
            {
                SetPropertyValue("PhuCapTienXangTruocDieuChinh", ref _PhuCapTienXangTruocDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp điện thoại cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapDienThoaiCu
        {
            get
            {
                return _PhuCapDienThoaiCu;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoaiCu", ref _PhuCapDienThoaiCu, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp điện thoại mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapDienThoaiMoi
        {
            get
            {
                return _PhuCapDienThoaiMoi;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoaiMoi", ref _PhuCapDienThoaiMoi, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapDienThoaiTruocDieuChinh
        {
            get
            {
                return _PhuCapDienThoaiTruocDieuChinh;
            }
            set
            {
                SetPropertyValue("PhuCapDienThoaiTruocDieuChinh", ref _PhuCapDienThoaiTruocDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp trách nhiệm công việc cũ")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTrachNhiemCongViecCu
        {
            get
            {
                return _PhuCapTrachNhiemCongViecCu;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCongViecCu", ref _PhuCapTrachNhiemCongViecCu, value);
            }
        }

        [ModelDefault("Caption", "Phụ cấp trách nhiệm công việc mới")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTrachNhiemCongViecMoi
        {
            get
            {
                return _PhuCapTrachNhiemCongViecMoi;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCongViecMoi", ref _PhuCapTrachNhiemCongViecMoi, value);
            }
        }

        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal PhuCapTrachNhiemCongViecTruocDieuChinh
        {
            get
            {
                return _PhuCapTrachNhiemCongViecTruocDieuChinh;
            }
            set
            {
                SetPropertyValue("PhuCapTrachNhiemCongViecTruocDieuChinh", ref _PhuCapTrachNhiemCongViecTruocDieuChinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng phụ cấp cũ")]
        public DateTime NgayHuongPhuCapCu
        {
            get
            {
                return _NgayHuongPhuCapCu;
            }
            set
            {
                SetPropertyValue("NgayHuongPhuCapCu", ref _NgayHuongPhuCapCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng phụ cấp mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongPhuCapMoi
        {
            get
            {
                return _NgayHuongPhuCapMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongPhuCapMoi", ref _NgayHuongPhuCapMoi, value);
            }
        }

        [Aggregated]
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

        public ChiTietQuyetDinhNangPhuCap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //MaTruong = TruongConfig.MaTruong;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nâng phụ cấp"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhNangPhuCap != null
                && !IsLoading
                && !QuyetDinhNangPhuCap.IsDirty)
                QuyetDinhNangPhuCap.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 

        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void AfterNhanVienChanged()
        {
            PhuCapDienThoaiCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai;
            PhuCapTienXangCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang;
            PhuCapTrachNhiemCongViecCu = ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiemCongViec;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted 
                && Oid != Guid.Empty)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhNangPhuCap.QuyetDinhMoi)
                {
                    //cập nhật thông tin vào hồ sơ
                    if (NgayHuongPhuCapMoi <= HamDungChung.GetServerTime())
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXangMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoaiMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiemCongViec = PhuCapTrachNhiemCongViecMoi;
                    }
                }

                //tạo mới diễn biến lương
                if (NgayHuongPhuCapMoi != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhNangPhuCap, ThongTinNhanVien, NgayHuongPhuCapMoi, this);
                }
            }
        }

        protected override void OnDeleting()
        {
            if (NgayHuongPhuCapMoi != DateTime.MinValue)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHuongPhuCapMoi>?",
                    ThongTinNhanVien, NgayHuongPhuCapMoi);
                if (QuyetDinhNangPhuCap.QuyetDinhMoi)
                {
                    SortProperty sort = new SortProperty("QuyetDinhNangPhuCap.NgayHieuLuc", SortingDirection.Descending);
                    using (XPCollection<ChiTietQuyetDinhNangPhuCap> qdList = new XPCollection<ChiTietQuyetDinhNangPhuCap>(Session, filter, sort))
                    {
                        qdList.TopReturnedObjects = 1;
                        if (qdList.Count == 0 ||
                            (qdList.Count == 1 && qdList[0].QuyetDinhNangPhuCap == QuyetDinhNangPhuCap))
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTienXang = PhuCapTienXangCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoaiCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.PhuCapTrachNhiemCongViec = PhuCapTrachNhiemCongViecCu;
                        }
                    }
                }
                //Xóa diễn biến lương
                QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhNangPhuCap, ThongTinNhanVien));

                //xóa quá trình bhxh
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, NgayHuongPhuCapMoi));
            }
            //xóa biến động
            if (QuyetDinhNangPhuCap.NgayPhatSinhBienDong != DateTime.MinValue)
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, QuyetDinhNangPhuCap.NgayPhatSinhBienDong);

            //xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }
            base.OnDeleting();
        }
    }

}
