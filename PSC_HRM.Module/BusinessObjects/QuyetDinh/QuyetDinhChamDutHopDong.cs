using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chấm dứt hợp đồng")]
    [Appearance("QuyetDinhChamDutHopDong", TargetItems = "DonViCu;BoPhanMoi", Enabled = false, Criteria = "PhanLoai=1")]

    //[Appearance("Hide_BUH", TargetItems = "NgayPhatSinhBienDong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    ////[Appearance("Hide_IUH", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    ////[Appearance("Hide_UTE", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    ////[Appearance("Hide_LUH", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    //[Appearance("Hide_DLU", TargetItems = "NgayPhatSinhBienDong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    ////[Appearance("Hide_HBU", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]

    public class QuyetDinhChamDutHopDong : QuyetDinhCaNhan
    {
        // Fields...
        private DateTime _NgayPhatSinhBienDong;
        private DateTime _TuNgay;
        private HopDong.HopDong _HopDong;
        private NamHoc _NamHoc;
        private TinhTrang _TinhTrang;
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]        
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
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

        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hợp đồng")]
        [DataSourceProperty("ThongTinNhanVien.ListHopDong")]
        public HopDong.HopDong HopDong
        {
            get
            {
                return _HopDong;
            }
            set
            {
                SetPropertyValue("HopDong", ref _HopDong, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        //Lưu vết
        [Browsable(false)]
        public TinhTrang TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        public QuyetDinhChamDutHopDong(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<HopDong.HopDong> HopDongList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChamDutHopDong;
            //
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chấm dứt hợp đồng"));
        }

        protected override void AfterNhanVienChanged()
        {
            HopDong = ThongTinNhanVien.HopDongHienTai;
            TinhTrang = ThongTinNhanVien.TinhTrang;
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            //if (GiayToHoSo == null)
            //{
            //    GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    if (GiayToList.Count > 0 && SoQuyetDinh != null)
            //    {
            //        GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định chấm dứt hợp đồng", SoQuyetDinh);
            //        if (GiayToList.Count > 0)
            //            GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
            //    }
            //}
        }

        [NonPersistent]
        [Browsable(false)]
        public bool IsThongBaoChamDutHopDong { get; set; }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && ThongTinNhanVien != null)
            {
                CriteriaOperator filter;
                ThongTinNhanVien.NgayNghiViec = TuNgay;
                //update tình trạng
                if (TuNgay <= HamDungChung.GetServerTime())
                {
                    filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ việc");
                    TinhTrang tinhtrang = Session.FindObject<TinhTrang>(filter);
                    if (tinhtrang == null)
                    {
                        tinhtrang = new TinhTrang(Session);
                        tinhtrang.TenTinhTrang = "Nghỉ việc";
                        tinhtrang.MaQuanLy = "NV";
                    }
                    ThongTinNhanVien.TinhTrang = tinhtrang;
                }
                //quản lý biến động
                //giảm lao động
                filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (hoSoBaoHiem != null && NgayPhatSinhBienDong != DateTime.MinValue)
                {
                    BienDongHelper.CreateBienDongGiamLaoDong(Session, this, NgayPhatSinhBienDong, LyDoNghiEnum.ThoiViec);
                }

                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        }

        protected override void OnDeleting()
        {
            if (ThongTinNhanVien != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?",
                                "%đang làm việc%");
                TinhTrang tinhtrang = Session.FindObject<TinhTrang>(filter);
                if (tinhtrang != null)
                    ThongTinNhanVien.TinhTrang = tinhtrang;
                
                //xóa biến động
                if (NgayPhatSinhBienDong != DateTime.MinValue)
                    BienDongHelper.DeleteBienDong<BienDong_GiamLaoDong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            }

            base.OnDeleting();
        }
    }

}
