using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.QuyetDinhService;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tiếp nhận nghỉ không lương")]    
    public class QuyetDinhTiepNhan : QuyetDinhCaNhan
    {
        // Fields...
        private QuyetDinhNghiKhongHuongLuong _QuyetDinhNghiKhongHuongLuong;
        private TinhTrang _TinhTrangCu; //KHÔNG cần vì tình trang trước khi tiếp nhận mặc định là "Nghỉ không lương" ???
        private DateTime _MocNangLuongDieuChinhCu;
        private DateTime _NgayPhatSinhBienDong;
        private bool _QuyetDinhMoi;
        private DateTime _MocNangLuongDieuChinhMoi;
        private DateTime _TuNgay;
        private DateTime _NgayXinTiepNhan;

        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "QĐ nghỉ không hưởng lương")]
        public QuyetDinhNghiKhongHuongLuong QuyetDinhNghiKhongHuongLuong
        {
            get
            {
                return _QuyetDinhNghiKhongHuongLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhNghiKhongHuongLuong", ref _QuyetDinhNghiKhongHuongLuong, value);
                if (!IsLoading && value != null)
                    TinhMocNangLuongDieuChinh();
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
                }
            }
        }

        [ModelDefault("Caption", "Ngày xin tiếp nhận")]
        public DateTime NgayXinTiepNhan
        {
            get
            {
                return _NgayXinTiepNhan;
            }
            set
            {
                SetPropertyValue("NgayXinTiepNhan", ref _NgayXinTiepNhan, value);
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
        [ModelDefault("Caption", "Tình trạng cũ")]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh cũ")]
        public DateTime MocNangLuongDieuChinhCu
        {
            get
            {
                return _MocNangLuongDieuChinhCu;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinhCu", ref _MocNangLuongDieuChinhCu, value);
            }
        }

        [ModelDefault("Caption", "Mốc nâng lương điều chỉnh mới")]
        public DateTime MocNangLuongDieuChinhMoi
        {
            get
            {
                return _MocNangLuongDieuChinhMoi;
            }
            set
            {
                SetPropertyValue("MocNangLuongDieuChinhMoi", ref _MocNangLuongDieuChinhMoi, value);
            }
        }

        public QuyetDinhTiepNhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTiepNhan;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định tiếp nhận nghỉ không lương"));
            //
            QuyetDinhMoi = true;
        }

        protected override void AfterNhanVienChanged()
        {
            TinhTrangCu = ThongTinNhanVien.TinhTrang;
            MocNangLuongDieuChinhCu = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh;
            UpdateQuyetDinhList();
        }

        [Browsable(false)]
        private XPCollection<QuyetDinhNghiKhongHuongLuong> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhNghiKhongHuongLuong>(Session);

            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
            SortProperty sort = new SortProperty("NgayQuyetDinh", DevExpress.Xpo.DB.SortingDirection.Descending);
            QuyetDinhList.Criteria = filter;

            XPCollection<QuyetDinhNghiKhongHuongLuong> qdList = new XPCollection<QuyetDinhNghiKhongHuongLuong>(Session, filter, sort);
            qdList.TopReturnedObjects = 1;
            if (qdList.Count == 1)
                QuyetDinhNghiKhongHuongLuong = qdList[0];
        }

        //protected override void OnLoaded()
        //{
        //    base.OnLoading();

        //    if (GiayToHoSo == null)
        //    {
        //        GiayToList = ThongTinNhanVien.ListGiayToHoSo;
        //        if (GiayToList.Count > 0 && SoQuyetDinh != null)
        //        {
        //            GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
        //            if (GiayToList.Count > 0)
        //                GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
        //        }
        //    }
        //}

        protected override void OnSaving()
        {
            base.OnSaving();

            SystemContainer.Resolver<IQuyetDinhTiepNhanService>("QDTiepNhan" + TruongConfig.MaTruong).Save(Session, this);
        }
        protected override void OnDeleting()
        {
            SystemContainer.Resolver<IQuyetDinhTiepNhanService>("QDTiepNhan" + TruongConfig.MaTruong).Delete(Session, this);

            base.OnDeleting();
        }

        protected void TinhMocNangLuongDieuChinh()
        {          
            if (QuyetDinhMoi && QuyetDinhNghiKhongHuongLuong.TuNgay != DateTime.MinValue && QuyetDinhNghiKhongHuongLuong.DenNgay != DateTime.MinValue)
            {
                if (ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong != DateTime.MinValue)
                {
                    int soThang = QuyetDinhNghiKhongHuongLuong.TuNgay.TinhSoThang(QuyetDinhNghiKhongHuongLuong.DenNgay);
                    MocNangLuongDieuChinhMoi = ThongTinNhanVien.NhanVienThongTinLuong.MocNangLuong.AddMonths(soThang);
                }
            }           
        }
    }

}
