using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.NangNgach;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định nâng ngạch lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangNgach;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangNgach : BaseObject
    {
        private DateTime _NgayBoNhiemNgachMoi;
        private DateTime _NgayBoNhiemNgachCu;
        private DateTime _NgayHuongLuongMoi;
        private DateTime _NgayHuongLuongCu;
        private NgachLuong _NgachLuongCu;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private DateTime _MocNangLuongCu;
        private NgachLuong _NgachLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private DateTime _MocNangLuongMoi;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhNangNgach _QuyetDinhNangNgach;
        private GiayToHoSo _GiayToHoSo;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng ngạch")]
        [Association("QuyetDinhNangNgach-ListChiTietQuyetDinhNangNgach")]
        public QuyetDinhNangNgach QuyetDinhNangNgach
        {
            get
            {
                return _QuyetDinhNangNgach;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangNgach", ref _QuyetDinhNangNgach, value);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương cũ")]
        public NgachLuong NgachLuongCu
        {
            get
            {
                return _NgachLuongCu;
            }
            set
            {
                SetPropertyValue("NgachLuongCu", ref _NgachLuongCu, value);
                if (!IsLoading)
                {
                    BacLuongCu = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [DataSourceProperty("NgachLuongCu.ListBacLuong")]
        public BacLuong BacLuongCu
        {
            get
            {
                return _BacLuongCu;
            }
            set
            {
                SetPropertyValue("BacLuongCu", ref _BacLuongCu, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuongCu = value.HeSoLuong;
                    TinhMocNangLuong();
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương cũ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongCu
        {
            get
            {
                return _HeSoLuongCu;
            }
            set
            {
                SetPropertyValue("HeSoLuongCu", ref _HeSoLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch cũ")]
        public DateTime NgayBoNhiemNgachCu
        {
            get
            {
                return _NgayBoNhiemNgachCu;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachCu", ref _NgayBoNhiemNgachCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        public DateTime NgayHuongLuongCu
        {
            get
            {
                return _NgayHuongLuongCu;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongCu", ref _NgayHuongLuongCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        public DateTime MocNangLuongCu
        {
            get
            {
                return _MocNangLuongCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongCu", ref _MocNangLuongCu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgachLuong NgachLuongMoi
        {
            get
            {
                return _NgachLuongMoi;
            }
            set
            {
                SetPropertyValue("NgachLuongMoi", ref _NgachLuongMoi, value);
                if (!IsLoading)
                {
                    BacLuongMoi = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương mới")]
        [DataSourceProperty("NgachLuongMoi.ListBacLuong")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BacLuong BacLuongMoi
        {
            get
            {
                return _BacLuongMoi;
            }
            set
            {
                SetPropertyValue("BacLuongMoi", ref _BacLuongMoi, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuongMoi = value.HeSoLuong;
                    TinhMocNangLuong();
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương mới")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoLuongMoi
        {
            get
            {
                return _HeSoLuongMoi;
            }
            set
            {
                SetPropertyValue("HeSoLuongMoi", ref _HeSoLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayBoNhiemNgachMoi
        {
            get
            {
                return _NgayBoNhiemNgachMoi;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgachMoi", ref _NgayBoNhiemNgachMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongLuongMoi
        {
            get
            {
                return _NgayHuongLuongMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongLuongMoi", ref _NgayHuongLuongMoi, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime MocNangLuongMoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
            }
        }

        //[Aggregated]
        //[Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        //[ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
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

        public ChiTietQuyetDinhNangNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nâng ngạch"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (QuyetDinhNangNgach != null
                && !IsLoading
                && !QuyetDinhNangNgach.IsDirty)
                QuyetDinhNangNgach.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            UpdateGiayToList();
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

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }
        private void UpdateGiayToList()
        {
            //if (GiayToList == null)
            //    GiayToList = new XPCollection<GiayToHoSo>(Session);

            if (ThongTinNhanVien != null)
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    GiayToList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", ThongTinNhanVien.Oid, "%Quyết định%");
        }

        private void AfterNhanVienChanged()
        {
            NgachLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            HeSoLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            NgayBoNhiemNgachCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            if (NgachLuongCu != null)
                MocNangLuongMoi = MocNangLuongCu.AddMonths(NgachLuongCu.ThoiGianNangBac);
            else
                MocNangLuongMoi = MocNangLuongCu;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted
                && Oid != Guid.Empty && Session is NestedUnitOfWork)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);

                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhNangNgach.QuyetDinhMoi)
                {
                    //chỉ phát sinh biến động khi nhân viên tham gia bảo hiểm xã hội
                    //tăng mức đóng
                    if (hoSoBaoHiem != null &&
                        QuyetDinhNangNgach.NgayPhatSinhBienDong != DateTime.MinValue)
                        BienDongHelper.CreateBienDongThayDoiLuong(Session, QuyetDinhNangNgach, 
                            BoPhan, ThongTinNhanVien,
                            QuyetDinhNangNgach.NgayPhatSinhBienDong, 
                            HeSoLuongMoi, ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu,
                            ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung, ThongTinNhanVien.NhanVienThongTinLuong.ThamNien,
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac, ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong);

                    //cập nhật thông tin hồ sơ
                    ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongMoi;
                    string strChucDanh = NgachLuongMoi.TenNgachLuong;
                    ChucDanh chucDanh = Session.FindObject<ChucDanh>(CriteriaOperator.Parse("TenChucDanh =?", strChucDanh));
                    if (chucDanh != null)
                    {
                        ThongTinNhanVien.ChucDanh = chucDanh;
                    }
                    ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuongMoi;              
                }

                //update dien bien luong
                if (NgayHuongLuongMoi != DateTime.MinValue && 
                    NgachLuongCu != null &&
                    BacLuongCu != null)
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=? and NgachLuong=? and BacLuongMoi=?",
                        ThongTinNhanVien.Oid, NgachLuongCu.Oid, BacLuongCu.Oid);
                    ChiTietQuyetDinhNangLuong chiTiet = Session.FindObject<ChiTietQuyetDinhNangLuong>(filter);
                    if (chiTiet != null)
                    {
                        filter = CriteriaOperator.Parse("QuyetDinh=?",
                            chiTiet.QuyetDinhNangLuong.Oid);
                        DienBienLuong updateDienBienLuong = Session.FindObject<DienBienLuong>(filter);
                        if (updateDienBienLuong != null)
                            updateDienBienLuong.DenNgay = NgayHuongLuongMoi.AddDays(-1);
                    }
                }

                //tạo diễn biến lương mới
                QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhNangNgach, ThongTinNhanVien, NgayHuongLuongMoi,this);

                //Bảo hiểm xã hội
                if (hoSoBaoHiem != null && 
                    NgayHuongLuongMoi != DateTime.MinValue)
                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, QuyetDinhNangNgach, hoSoBaoHiem, NgayHuongLuongMoi);
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null &&
                NgayHuongLuongMoi != DateTime.MinValue)
            {
                CriteriaOperator filter;
                if (QuyetDinhNangNgach.QuyetDinhMoi)
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay>?",
                        ThongTinNhanVien, NgayHuongLuongMoi);
                    SortProperty sort = new SortProperty("TuNgay", SortingDirection.Descending);
                    using (XPCollection<DienBienLuong> dblList = new XPCollection<DienBienLuong>(Session, filter, sort))
                    {
                        dblList.TopReturnedObjects = 1;
                        //Quyết định còn hiệu lực nhất
                        if (dblList.Count == 0 ||
                            (dblList.Count == 1 && dblList[0].QuyetDinh == QuyetDinhNangNgach))
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgachCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuongCu;

                        }
                    }
                }

                //xóa diễn biến lương
                QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhNangNgach, ThongTinNhanVien));

                //xóa quá trình bhxh
                if (NgayHuongLuongMoi != DateTime.MinValue)
                    QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, NgayHuongLuongMoi));

                //xóa biến động
                if (QuyetDinhNangNgach.NgayPhatSinhBienDong != DateTime.MinValue)
                    BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, QuyetDinhNangNgach.NgayPhatSinhBienDong);
            }

            //xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }

        private void TinhMocNangLuong()
        {
            //tính mốc nâng lương ở đây
            //nếu mới 1 - cũ 1 >= cũ 2 - cũ 1
            //  ngày hiệu lực
            //  mốc nâng lương cũ     
            if (BacLuongCu != null && BacLuongMoi != null && QuyetDinhNangNgach != null)
            {
                int bac;
                if (int.TryParse(BacLuongCu.MaQuanLy, out bac))
                {
                    bac++;
                    BacLuong bacLuong = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and MaQuanLy=?",
                        BacLuongCu.NgachLuong.Oid, Convert.ToString(bac)));
                    if (bacLuong != null)
                    {
                        if (BacLuongMoi.HeSoLuong - BacLuongCu.HeSoLuong >= bacLuong.HeSoLuong - BacLuongCu.HeSoLuong)
                            MocNangLuongMoi = QuyetDinhNangNgach.NgayHieuLuc;
                        else
                            MocNangLuongMoi = MocNangLuongCu;
                    }
                }
            }

        }
    }

}
