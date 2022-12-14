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
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định nâng lương")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangLuong;ThongTinNhanVien")]
    
    [Appearance("Hide_NangLuongTruocNghiHuu", TargetItems = "NgayNghiHuu", Visibility = ViewItemVisibility.Hide, Criteria = "!NangLuongTruocKhiNghiHuu")]
    [Appearance("Hide_NangLuongTruocHan", TargetItems = "SoThang;LyDo;", Visibility = ViewItemVisibility.Hide, Criteria = "!NangLuongTruocHan")]

    public class ChiTietQuyetDinhNangLuong : TruongBaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private QuyetDinhNangLuong _QuyetDinhNangLuong;
        private GiayToHoSo _GiayToHoSo;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuongCu;
        private decimal _HeSoLuongCu;
        private int _VuotKhungCu;
        private DateTime _NgayHuongLuongCu;
        private DateTime _MocNangLuongCu;
        private DateTime _NgayHuongLuongMoi;
        private BacLuong _BacLuongMoi;
        private decimal _HeSoLuongMoi;
        private int _VuotKhungMoi;
        private DateTime _MocNangLuongMoi;
        private bool _NangLuongTruocHan;
        private bool _NangLuongTruocKhiNghiHuu;
        private bool _QuyetDinhMoi;
        private DateTime _NgayNghiHuu;
        private string _LyDo;
        private int _SoThang;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng lương")]
        [Association("QuyetDinhNangLuong-ListChiTietQuyetDinhNangLuong")]
        public QuyetDinhNangLuong QuyetDinhNangLuong
        {
            get
            {
                return _QuyetDinhNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangLuong", ref _QuyetDinhNangLuong, value);
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
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
                if (!IsLoading)
                    BacLuongCu = null;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương cũ")]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
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
                    HeSoLuongCu = value.HeSoLuong;
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

        [ModelDefault("Caption", "% vượt khung cũ")]
        public int VuotKhungCu
        {
            get
            {
                return _VuotKhungCu;
            }
            set
            {
                SetPropertyValue("VuotKhungCu", ref _VuotKhungCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương cũ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL'")]
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

        [ModelDefault("Caption", "Ngày hưởng lương cũ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL'")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương mới")]
        [DataSourceProperty("NgachLuong.ListBacLuong", DataSourcePropertyIsNullMode.SelectNothing)]
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
                    HeSoLuongMoi = value.HeSoLuong;
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

        [ModelDefault("Caption", "% vượt khung mới")]
        public int VuotKhungMoi
        {
            get
            {
                return _VuotKhungMoi;
            }
            set
            {
                SetPropertyValue("VuotKhungMoi", ref _VuotKhungMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mốc nâng lương mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL'")]
        public DateTime MocNangLuongMoi
        {
            get
            {
                return _MocNangLuongMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongMoi", ref _MocNangLuongMoi, value);
                if (!IsLoading && value != DateTime.MinValue)
                    NgayHuongLuongMoi = value;
            }
        }

        [ModelDefault("Caption", "Ngày hưởng lương mới")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong != 'UEL'")]
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

        [ModelDefault("Caption", "Nâng lương trước hạn")]
        public bool NangLuongTruocHan
        {
            get
            {
                return _NangLuongTruocHan;
            }
            set
            {
                SetPropertyValue("NangLuongTruocHan", ref _NangLuongTruocHan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Nâng lương trước khi nghỉ hưu")]
        public bool NangLuongTruocKhiNghiHuu
        {
            get
            {
                return _NangLuongTruocKhiNghiHuu;
            }
            set
            {
                SetPropertyValue("NangLuongTruocKhiNghiHuu", ref _NangLuongTruocKhiNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Ngày nghỉ hưu")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "NangLuongTruocKhiNghiHuu = True")]
        public DateTime NgayNghiHuu
        {
            get
            {
                return _NgayNghiHuu;
            }
            set
            {
                SetPropertyValue("NgayNghiHuu", ref _NgayNghiHuu, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [ModelDefault("Caption", "Số tháng")]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
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

        public ChiTietQuyetDinhNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //MaTruong = TruongConfig.MaTruong;
            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nâng lương"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNhanVienList();
            
        }
        
        
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (TruongConfig.MaTruong.Equals("NEU"))
            {
                if (QuyetDinhNangLuong != null
                    && !IsLoading
                    && !QuyetDinhNangLuong.IsDirty)
                    QuyetDinhNangLuong.IsDirty = true;
            }
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
            NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;
            BacLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.BacLuong;
            HeSoLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
            VuotKhungCu = ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung;
            MocNangLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh == DateTime.MinValue ? ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong : ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh;
            NgayHuongLuongCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong;
            MocNangLuongMoi = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;
            NgayHuongLuongMoi = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;           

            if (VuotKhungCu > 0)
            {
                BacLuongMoi = BacLuongCu;
                HeSoLuongMoi = HeSoLuongCu;
                VuotKhungMoi = VuotKhungCu + 1;
            }
            else
            {
                if (NgachLuong != null)
                {
                    if (NgachLuong.TotKhung != null &&
                        BacLuongCu != null)
                    {
                        if (NgachLuong.TotKhung == BacLuongCu)
                        {
                            BacLuongMoi = BacLuongCu;
                            HeSoLuongMoi = HeSoLuongCu;
                            VuotKhungMoi = 5;
                        }
                        else
                        {
                            int bac = 0;
                            if (int.TryParse(BacLuongCu.MaQuanLy, out bac))
                            {
                                //chi lay bac luong moi thoi
                                //bac luong cu chi danh de nhap du lieu cu
                                bac++;
                                BacLuong bacLuong = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and MaQuanLy=? and (BacLuongCu is null or !BacLuongCu)",
                                    NgachLuong.Oid, bac.ToString()));
                                if (bacLuong != null)
                                {
                                    BacLuongMoi = bacLuong;
                                    HeSoLuongMoi = bacLuong.HeSoLuong;
                                    VuotKhungMoi = 0;
                                }
                            }
                        }
                    }
                    else
                    {
                        int bac = 0;
                        if (BacLuongCu != null && int.TryParse(BacLuongCu.MaQuanLy, out bac))
                        {
                            //chi lay bac luong moi thoi
                            //bac luong cu chi danh de nhap du lieu cu
                            bac++;
                            BacLuong bacLuong = Session.FindObject<BacLuong>(CriteriaOperator.Parse("NgachLuong=? and MaQuanLy=? and (BacLuongCu is null or !BacLuongCu)", NgachLuong.Oid, bac.ToString()));
                            if (bacLuong != null)
                            {
                                BacLuongMoi = bacLuong;
                                HeSoLuongMoi = bacLuong.HeSoLuong;
                                VuotKhungMoi = 0;
                            }
                            else
                            {
                                BacLuongMoi = BacLuongCu;
                                HeSoLuongMoi = HeSoLuongCu;
                                VuotKhungMoi = 5;
                            }
                        }
                    }
                }
            }
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
                if (QuyetDinhNangLuong.QuyetDinhMoi)
                {
                    //quản lý biến động (chi cập nhật khi có thông tin lương bị thay đổi)
                    //tăng mức đóng
                    if (hoSoBaoHiem != null &&
                        QuyetDinhNangLuong.NgayPhatSinhBienDong != DateTime.MinValue)
                        BienDongHelper.CreateBienDongThayDoiLuong(Session, QuyetDinhNangLuong,
                            BoPhan, ThongTinNhanVien,
                            QuyetDinhNangLuong.NgayPhatSinhBienDong,
                            HeSoLuongMoi, ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu,
                            VuotKhungMoi, ThongTinNhanVien.NhanVienThongTinLuong.ThamNien,
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                            ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong);

                    //cập nhật thông tin vào hồ sơ
                    if (NgayHuongLuongMoi <= HamDungChung.GetServerTime())
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuong;
                        ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = VuotKhungMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuongMoi;
                    }
                }


                //update dien bien luong
                if (NgayHuongLuongMoi != DateTime.MinValue && ((NgachLuong != null && BacLuongCu != null)))
                {
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);

                    ChiTietQuyetDinhNangLuong chiTiet = Session.FindObject<ChiTietQuyetDinhNangLuong>(filter);
                    if (chiTiet != null)
                    {
                        filter = CriteriaOperator.Parse("QuyetDinh = ?",
                            chiTiet.QuyetDinhNangLuong.Oid);
                        DienBienLuong updateDienBienLuong = Session.FindObject<DienBienLuong>(filter);
                        if (updateDienBienLuong != null)
                            updateDienBienLuong.DenNgay = NgayHuongLuongMoi.AddDays(-1);
                    }
                }

                //tạo mới diễn biến lương
                if (NgayHuongLuongMoi != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateDienBienLuong(Session, QuyetDinhNangLuong, ThongTinNhanVien, NgayHuongLuongMoi,this);

                    //Bảo hiểm xã hội
                    if (hoSoBaoHiem != null)
                        QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, QuyetDinhNangLuong, hoSoBaoHiem, NgayHuongLuongMoi);
                }
            }
        }

        protected override void OnDeleting()
        {
            if (NgayHuongLuongMoi != DateTime.MinValue)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and NgayHuongLuongMoi>?",
                    ThongTinNhanVien, NgayHuongLuongMoi);

                if (QuyetDinhNangLuong.QuyetDinhMoi)
                {
                    SortProperty sort = new SortProperty("QuyetDinhNangLuong.NgayHieuLuc", SortingDirection.Descending);
                    using (XPCollection<ChiTietQuyetDinhNangLuong> qdList = new XPCollection<ChiTietQuyetDinhNangLuong>(Session, filter, sort))
                    {
                        qdList.TopReturnedObjects = 1;
                        if (qdList.Count == 0 ||
                            (qdList.Count == 1 && qdList[0].QuyetDinhNangLuong == QuyetDinhNangLuong))
                        {
                            ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong = NgachLuong;
                            ThongTinNhanVien.NhanVienThongTinLuong.BacLuong = BacLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong = HeSoLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung = VuotKhungCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongLuong = NgayHuongLuongCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuongCu;
                        }
                    }
                }


                //Xóa diễn biến lương
                QuaTrinhHelper.DeleteQuaTrinh<DienBienLuong>(Session, CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?", QuyetDinhNangLuong, ThongTinNhanVien));

                //xóa quá trình bhxh
                QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=? and TuNam=?", ThongTinNhanVien, Convert.ToDateTime(NgayHuongLuongMoi.ToString("01/MM/yyyy"))));
            }
            //xóa biến động
            if (QuyetDinhNangLuong.NgayPhatSinhBienDong != DateTime.MinValue)
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, QuyetDinhNangLuong.NgayPhatSinhBienDong);

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
