using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chuyển công tác")]

    //[Appearance("Hide_BUH", TargetItems = "NgayPhatSinhBienDong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'BUH'")]
    ////[Appearance("Hide_IUH", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong ='IUH'")]
    ////[Appearance("Hide_UTE", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'UTE'")]
    ////[Appearance("Hide_LUH", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'LUH'")]
    //[Appearance("Hide_DLU", TargetItems = "NgayPhatSinhBienDong;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'DLU'")]
    ////[Appearance("Hide_HBU", TargetItems = "TrinhDoChuyenMonCaoNhat;", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong = 'HBU'")]

    public class QuyetDinhChuyenCongTac : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrang;
        private DateTime _NgayPhatSinhBienDong;
        private DateTime _TuNgay;
        private DateTime _BanGiaoCongViecTuNgay;
        private string _CoQuanMoi;
        private bool _QuyetDinhMoi;

        [Browsable(false)]
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

        [Size(200)]
        [ModelDefault("Caption", "Cơ quan mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string CoQuanMoi
        {
            get
            {
                return _CoQuanMoi;
            }
            set
            {
                SetPropertyValue("CoQuanMoi", ref _CoQuanMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayPhatSinhBienDong = value;
                    BanGiaoCongViecTuNgay = value;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bàn giao từ ngày")]
        public DateTime BanGiaoCongViecTuNgay
        {
            get
            {
                return _BanGiaoCongViecTuNgay;
            }
            set
            {
                SetPropertyValue("BanGiaoCongViecTuNgay", ref _BanGiaoCongViecTuNgay, value);
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
            }
        }

        //dùng để lưu vết
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

        public QuyetDinhChuyenCongTac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChuyenCongTac;
            TuNgay = HamDungChung.GetServerTime();
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chuyển công tác"));
        }

        protected override void AfterNhanVienChanged()
        {
            base.AfterNhanVienChanged();
            TinhTrang = ThongTinNhanVien.TinhTrang;
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
                    if (GiayToList.Count > 0)
                        GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
                }
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if (QuyetDinhMoi)
                {
                    ThongTinNhanVien.NgayNghiViec = TuNgay;
                    //
                    CriteriaOperator filter;
                
                    //update tình trạng
                    if(TuNgay <= HamDungChung.GetServerTime())
                    {
                        filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Chuyển công tác");
                        TinhTrang tinhtrang = Session.FindObject<TinhTrang>(filter);
                        if (tinhtrang == null)
                        {
                            tinhtrang = new TinhTrang(Session);
                            tinhtrang.TenTinhTrang = "Chuyển công tác";
                            tinhtrang.MaQuanLy = "CCT";
                        }
                        ThongTinNhanVien.TinhTrang = tinhtrang;
                    }

                    //quản lý biến động
                    //giảm lao động
                    filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                    HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                    if (hoSoBaoHiem != null && NgayPhatSinhBienDong != DateTime.MinValue)
                    {
                        BienDongHelper.SetTrangThaiThamGiaBHXH(Session, this, LyDoNghiEnum.ThoiViec);
                        BienDongHelper.CreateBienDongGiamLaoDong(Session, this, NgayPhatSinhBienDong, LyDoNghiEnum.ThoiViec);
                    }
                }

                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.QuyetDinh = this;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        }

        protected override void OnDeleting()
        {
            RecoverData();
            base.OnDeleting();
        }

        private void RecoverData()
        {
            //thiết lập tình trạng
            if (TinhTrang != null)
                ThongTinNhanVien.TinhTrang = TinhTrang;
            else
            {
                CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?", "%đang làm việc%");
                TinhTrang tinhtrang = Session.FindObject<TinhTrang>(filter);
                if (tinhtrang != null)
                    ThongTinNhanVien.TinhTrang = tinhtrang;
            }

            //xóa biến động
            if (NgayPhatSinhBienDong != DateTime.MinValue)
            {
                BienDongHelper.ResetTrangThaiThamGiaBHXH(Session, this);
                BienDongHelper.DeleteBienDong<BienDong_GiamLaoDong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);
            }

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);
        }
    }
}
