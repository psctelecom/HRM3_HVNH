using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.Xpo.DB;


namespace PSC_HRM.Module.QuyetDinh
{
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Chi tiết công nhận hết hạn tập sự")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhCongNhanHetHanTapSu;ThongTinNhanVien")]
    //[Appearance("Hide_IUH", TargetItems = "TaiBoMonPhu;CapUuDai", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    public class ChiTietCongNhanHetHanTapSu : TruongBaseObject, IBoPhan
    {
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ThongTinNhanVien _ThongTinNhanVien;
        private QuyetDinhCongNhanHetHanTapSu _QuyetDinhCongNhanHetHanTapSu;
        private DateTime _NgayHetHanTapSu;
        private NgachLuong _NgachLuong;
        private BacLuong _BacLuong;
        private decimal _HeSoLuong;
        private DateTime _MocNangLuong;
        private DateTime _NgayHuongLuong;
        private GiayTo.GiayToHoSo _GiayToHoSo;

        //BUH
        private int _PhuCapUuDai;
        private BoPhan _TaiBoMon;

        //QNU
        private DateTime _NgayBoNhiemNgach;
        
        public ChiTietCongNhanHetHanTapSu(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định công nhận hết hạn tập sự")]
        [Association("QuyetDinhCongNhanHetHanTapSu-ListChiTietCongNhanHetHanTapSu")]
        public QuyetDinhCongNhanHetHanTapSu QuyetDinhCongNhanHetHanTapSu
        {
            get
            {
                return _QuyetDinhCongNhanHetHanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhCongNhanHetHanTapSu", ref _QuyetDinhCongNhanHetHanTapSu, value);
                //if (!IsLoading && value != null && GiayToHoSo != null)
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
                    if (ThongTinNhanVien == null || !MaTruong.Equals("BUH"))
                    {
                        ThongTinNhanVien = null;
                        UpdateNVList();
                        BoPhanText = value.TenBoPhan;
                    }
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
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList")]
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
                    NgachLuong = value.NhanVienThongTinLuong.NgachLuong;
                    BacLuong = value.NhanVienThongTinLuong.BacLuong;
                    HeSoLuong = value.NhanVienThongTinLuong.HeSoLuong;

                    //
                    if (QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu == null)
                    {
                        ChiTietQuyetDinhHuongDanTapSu chiTiet = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("ThongTinNhanVien = ? and BoPhan = ?", value.Oid, BoPhan.Oid));
                        if(chiTiet != null && chiTiet.QuyetDinhHuongDanTapSu.QuyetDinhMoi)
                        {
                            
                            QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu = Session.GetObjectByKey<QuyetDinhHuongDanTapSu>(chiTiet.QuyetDinhHuongDanTapSu.Oid);
                            //
                            NgayHetHanTapSu = chiTiet.DenNgay;
                        }
                    }
                }
            }
        }

        [ModelDefault("Caption", "Ngày bổ nhiệm ngạch")]
        public DateTime NgayBoNhiemNgach
        {
            get
            {
                return _NgayBoNhiemNgach;
            }
            set
            {
                SetPropertyValue("NgayBoNhiemNgach", ref _NgayBoNhiemNgach, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết hạn tập sự")]
        public DateTime NgayHetHanTapSu
        {
            get
            {
                return _NgayHetHanTapSu;
            }
            set
            {
                SetPropertyValue("NgayHetHanTapSu", ref _NgayHetHanTapSu, value);
                if (!IsLoading)
                {
                    MocNangLuong = value;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngạch lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                {
                    BacLuong = null;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bậc lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("NgachLuong.ListBacLuong")]
        public BacLuong BacLuong
        {
            get
            {
                return _BacLuong;
            }
            set
            {
                SetPropertyValue("BacLuong", ref _BacLuong, value);
                if (!IsLoading && value != null)
                {
                    HeSoLuong = value.HeSoLuong;
                }
            }
        }

        [ModelDefault("Caption", "Hệ số lương")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal HeSoLuong
        {
            get
            {
                return _HeSoLuong;
            }
            set
            {
                SetPropertyValue("HeSoLuong", ref _HeSoLuong, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime MocNangLuong
        {
            get
            {
                return _MocNangLuong;
            }
            set
            {
                SetPropertyValue("MocNangLuong", ref _MocNangLuong, value);
            }
        }
        [ModelDefault("Caption", "Ngày hưởng lương")]
       // [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayHuongLuong
        {
            get
            {
                return _NgayHuongLuong;
            }
            set
            {
                SetPropertyValue("NgayHuongLuong", ref _NgayHuongLuong, value);
            }
        }
        [ModelDefault("Caption", "% PC ưu đãi")]
        public int PhuCapUuDai
        {
            get
            {
                return _PhuCapUuDai;
            }
            set
            {
                SetPropertyValue("PhuCapUuDai", ref _PhuCapUuDai, value);
            }
        }

        [ModelDefault("Caption", "Giảng dạy tại")]
        public BoPhan TaiBoMon
        {
            get
            {
                return _TaiBoMon;
            }
            set
            {
                SetPropertyValue("TaiBoMon", ref _TaiBoMon, value);
            }
        }

        //[Aggregated]
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
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            GiayToHoSo = new GiayToHoSo(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định công nhận hết hạn tập sự"));
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "%Bản gốc%"));
            UpdateNVList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (QuyetDinhCongNhanHetHanTapSu != null
                && !IsLoading
                && !QuyetDinhCongNhanHetHanTapSu.IsDirty)
                QuyetDinhCongNhanHetHanTapSu.IsDirty = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNVList();
            UpdateGiayToList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 
        }

        private void UpdateNVList()
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


        protected override void OnSaving()
        {
            base.OnSaving();
            if (!IsDeleted && Oid != Guid.Empty)
            {
                CriteriaOperator filter;
                SortProperty sort;
                ChiTietQuyetDinhHuongDanTapSu tapSu = null;
                if (QuyetDinhCongNhanHetHanTapSu.QuyetDinhMoi)
                {
                    //update huong 100% luong
                    ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = false;
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = MocNangLuong;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayBoNhiemNgach = NgayBoNhiemNgach;
                    if (MaTruong.Equals("BUH"))
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai = PhuCapUuDai;
                        if (TaiBoMon != null)
                        { ThongTinNhanVien.TaiBoMon = TaiBoMon; }
                    }

                    //update HSPCTrachNhiem can bo huong dan
                    if (QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu != null)
                        tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu, ThongTinNhanVien));
                    if (tapSu != null)
                    {
                        tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                    }

                    //tăng mức đóng
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                        ThongTinNhanVien);
                    HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                    if (hoSoBaoHiem != null)
                    {
                        DateTime tuNgay = new DateTime(QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc.Year, QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc.Month, 1);
                        DateTime denNgay = tuNgay.AddMonths(1).AddDays(-1);
                        filter = CriteriaOperator.Parse("ThoiGian>=? and ThoiGian<=? and !KhoaSo",
                            HamDungChung.SetTime(tuNgay, 2), HamDungChung.SetTime(denNgay, 3));
                        QuanLyBienDong quanLyBienDong = Session.FindObject<QuanLyBienDong>(filter);
                        if (quanLyBienDong == null)
                        {
                            quanLyBienDong = new QuanLyBienDong(Session);
                            quanLyBienDong.ThoiGian = QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc;
                            filter = CriteriaOperator.Parse("ThoiGian>=? and ThoiGian<=?",
                                HamDungChung.SetTime(tuNgay, 0), HamDungChung.SetTime(denNgay, 1));
                            object count = Session.Evaluate<QuanLyBienDong>(CriteriaOperator.Parse("COUNT()"), filter);
                            if (count != null)
                                quanLyBienDong.Dot = (int)count + 1;
                        }

                        filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                            ThongTinNhanVien, QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc);
                        BienDong_ThayDoiLuong bienDong = Session.FindObject<BienDong_ThayDoiLuong>(filter);
                        if (bienDong == null)
                        {
                            bienDong = new BienDong_ThayDoiLuong(Session);
                            bienDong.QuanLyBienDong = quanLyBienDong;
                            bienDong.BoPhan = BoPhan;
                            bienDong.ThongTinNhanVien = ThongTinNhanVien;
                            bienDong.TuNgay = QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc;
                        }
                        bienDong.TienLuongCu = Math.Round(ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong * 85 / 100, 3, MidpointRounding.AwayFromZero);
                        bienDong.TienLuongMoi = ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong;
                        bienDong.GhiChu = "QĐ số " + QuyetDinhCongNhanHetHanTapSu.SoQuyetDinh;
                    }
                }


                //Diễn biến lương
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay<?",
                    ThongTinNhanVien.Oid, QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc);
                sort = new SortProperty("TuNgay", SortingDirection.Descending);
                using (XPCollection<DienBienLuong> dblList = new XPCollection<DienBienLuong>(Session, filter, sort))
                {
                    dblList.TopReturnedObjects = 1;
                    if (dblList.Count == 1)
                    {
                        dblList[0].DenNgay = QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc.AddDays(-1);
                    }
                }

                filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    QuyetDinhCongNhanHetHanTapSu, ThongTinNhanVien.Oid);
                DienBienLuong dienBienLuong = Session.FindObject<DienBienLuong>(filter);
                if (dienBienLuong == null)
                {
                    dienBienLuong = new DienBienLuong(Session);
                    dienBienLuong.ThongTinNhanVien = ThongTinNhanVien;
                    dienBienLuong.TuNgay = QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc;
                    dienBienLuong.QuyetDinh = QuyetDinhCongNhanHetHanTapSu;
                }
                dienBienLuong.Huong85PhanTramLuong = false;
                dienBienLuong.LyDo = "Hoàn thành tập sự";
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien.Oid);
                if (QuyetDinhCongNhanHetHanTapSu.QuyetDinhMoi)
                {
                    //update huong 85% luong
                    ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong = true;
                    ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong = NgayHetHanTapSu;
                    if (MaTruong.Equals("BUH"))
                    {
                        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapUuDai = 0;
                        ThongTinNhanVien.TaiBoMon = null;
                    }

                    ChiTietQuyetDinhHuongDanTapSu tapSu = null;
                    if (QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu != null)
                        tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu = ? and ThongTinNhanVien = ?", QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu, ThongTinNhanVien));
                    if (tapSu != null)
                    {
                        tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = QuyetDinhCongNhanHetHanTapSu.QuyetDinhHuongDanTapSu.HSPCTrachNhiem;
                    }

                }

                //Xóa diễn biến lương
                filter = CriteriaOperator.Parse("QuyetDinh=? and ThongTinNhanVien=?",
                    QuyetDinhCongNhanHetHanTapSu, ThongTinNhanVien);
                DienBienLuong dienBienLuong = Session.FindObject<DienBienLuong>(filter);
                if (dienBienLuong != null)
                {
                    Session.Delete(dienBienLuong);
                    Session.Save(dienBienLuong);
                }

                //xóa biến động
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? and TuNgay=?",
                    ThongTinNhanVien, QuyetDinhCongNhanHetHanTapSu.NgayHieuLuc);
                BienDong_ThayDoiLuong bienDong = Session.FindObject<BienDong_ThayDoiLuong>(filter);
                if (bienDong != null)
                {
                    Session.Delete(bienDong);
                    Session.Save(bienDong);
                }

                //xoa giay to ho so
                filter = CriteriaOperator.Parse("HoSo=? and QuyetDinh=?",
                    ThongTinNhanVien, QuyetDinhCongNhanHetHanTapSu.Oid);
                GiayToHoSo giayToHoSo = Session.FindObject<GiayToHoSo>(filter);
                if (giayToHoSo != null)
                {
                    Session.Delete(giayToHoSo);
                    Session.Save(giayToHoSo);
                }
            }
            base.OnDeleting();
        }
    }

}
