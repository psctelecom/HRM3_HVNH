using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.BaoMat;
namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định miễn nhiệm")]

    public class QuyetDinhMienNhiem : QuyetDinhCaNhan
    {
        private decimal _HSPCChucVu1Moi;
        private decimal _PhuCapDienThoaiMoi;
        private decimal _HSPCKiemNhiemMoi;
        private ChucVu _ChucVuKiemNhiemMoi;
        private decimal _HSPCChucVuMoi;
        private ChucVu _ChucVuMoi;
        private DateTime _NgayPhatSinhBienDong;
        private QuyetDinhBoNhiem _QuyetDinhBoNhiem;
        private string _LyDo;
        private bool _QuyetDinhMoi;
        private DateTime _NgayThoiHuongHSPCChucVu;

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
        [ModelDefault("Caption", "Quyết định bổ nhiệm")]
        [DataSourceProperty("QuyetDinhList")]
        public QuyetDinhBoNhiem QuyetDinhBoNhiem
        {
            get
            {
                return _QuyetDinhBoNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiem", ref _QuyetDinhBoNhiem, value);
                if (!IsLoading && value != null)
                    XuLyChucVu();
            }
        }

        [ModelDefault("Caption", "Ngày thôi hưởng HSPC chức vụ")]
        public DateTime NgayThoiHuongHSPCChucVu
        {
            get
            {
                return _NgayThoiHuongHSPCChucVu;
            }
            set
            {
                SetPropertyValue("NgayThoiHuongHSPCChucVu", ref _NgayThoiHuongHSPCChucVu, value);
            }
        }

        [Size(500)]
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

        //tinh toan chuc vu moi
        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
                if (!IsLoading && value != null)
                    XuLyPhuCap();
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ mới")]
        public decimal HSPCChucVuMoi
        {
            get
            {
                return _HSPCChucVuMoi;
            }
            set
            {
                SetPropertyValue("HSPCChucVuMoi", ref _HSPCChucVuMoi, value);
            }
        }

        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("Caption", "HSPC chức vụ 1 mới")]
        public decimal HSPCChucVu1Moi
        {
            get
            {
                return _HSPCChucVu1Moi;
            }
            set
            {
                SetPropertyValue("HSPCChucVu1Moi", ref _HSPCChucVu1Moi, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Phụ cấp điện thoại mới")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm mới")]
        public ChucVu ChucVuKiemNhiemMoi
        {
            get
            {
                return _ChucVuKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiemMoi", ref _ChucVuKiemNhiemMoi, value);
                if (!IsLoading && value != null)
                    XuLyPhuCap();
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC kiêm nhiệm mới")]
        public decimal HSPCKiemNhiemMoi
        {
            get
            {
                return _HSPCKiemNhiemMoi;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemMoi", ref _HSPCKiemNhiemMoi, value);
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
        
        public QuyetDinhMienNhiem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhMienNhiem;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định miễn nhiệm"));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateQuyetDinhList();
            MaTruong = TruongConfig.MaTruong;
        }

        protected override void QuyetDinhChanged()
        {
            if (NgayHieuLuc != DateTime.MinValue)
                NgayPhatSinhBienDong = NgayHieuLuc;
        }
        [NonPersistent]
        [Browsable(false)]
        public string MaTruong { get; set; }
        protected override void AfterNhanVienChanged()
        {
            QuyetDinhBoNhiem = null;
            UpdateQuyetDinhList();
            //
            MaTruong = TruongConfig.MaTruong;
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiem> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoNhiem>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                if(QuyetDinhBoNhiem!=null)
                QuyetDinhBoNhiem.HetHieuLuc = true;

                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhMoi)
                {
                    if (hoSoBaoHiem != null &&
                        NgayHieuLuc != DateTime.MinValue &&
                        NgayPhatSinhBienDong != DateTime.MinValue)
                    {
                        //biến động thay đổi mức đóng
                        BienDongHelper.CreateBienDongThayDoiLuong(Session, this, NgayPhatSinhBienDong,
                            ThongTinNhanVien.NhanVienThongTinLuong.HeSoLuong,
                            HSPCChucVuMoi, ThongTinNhanVien.NhanVienThongTinLuong.VuotKhung,
                            ThongTinNhanVien.NhanVienThongTinLuong.ThamNien,
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac,
                            ThongTinNhanVien.NhanVienThongTinLuong.Huong85PhanTramLuong);

                        //biến động thay đổi chức danh
                        string noiDung = ChucVuMoi != null ? string.Format("{0} {1}", ChucVuMoi.TenChucVu, BoPhan.TenBoPhan) :
                            string.Format("Nhân viên {0}", BoPhan.TenBoPhan);
                        BienDongHelper.CreateBienDongThayDoiChucDanh(Session, this, NgayPhatSinhBienDong, noiDung);
                    }

                    //reset phụ cấp chức vụ 1,2,3
                    ThongTinNhanVien.ChucVu = ChucVuMoi;
                    ThongTinNhanVien.ChucVuKiemNhiem = ChucVuKiemNhiemMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = HSPCChucVuMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem = HSPCKiemNhiemMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoaiMoi;
                    ThongTinNhanVien.NgayBoNhiem = DateTime.MinValue;

                    if (ChucVuMoi == null)
                        ThongTinNhanVien.LanBoNhiemChucVu = 0;
                    else
                    {
                        //lần bổ nhiệm
                        object count = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("Count()"),
                            CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVu=?",
                            ThongTinNhanVien, ChucVuMoi));
                        if (count != null && (int)count > 0)
                            ThongTinNhanVien.LanBoNhiemChucVu = (int)count;
                        else
                            ThongTinNhanVien.LanBoNhiemChucVu = 1;
                    }
                }

                if (NgayHieuLuc != DateTime.MinValue)
                {
                    //cập nhật đến ngày của quá trình công tác
                    if (QuyetDinhBoNhiem != null)
                    QuaTrinhHelper.UpdateQuaTrinhCongTac(Session, QuyetDinhBoNhiem, NgayHieuLuc);

                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    if (QuyetDinhBoNhiem != null)
                    QuaTrinhHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiem, NgayHieuLuc);

                    //Cập nhật đến ngày Diễn biến lương
                    if (QuyetDinhBoNhiem != null)
                    QuaTrinhHelper.UpdateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc);

                    if (HSPCChucVuMoi > 0)
                    {
                        //Tạo mới diễn biến lương
                        QuaTrinhHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc,null);

                        //cập nhật đến năm của quá trình tham gia bhxh
                        QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, hoSoBaoHiem, this, NgayHieuLuc);
                    }
                }

                //quan ly bo nhiem
                if (QuyetDinhBoNhiem != null)
                BoNhiemHelper.CreateMienNhiem(Session, this, QuyetDinhBoNhiem.ChucVuMoi, false);
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                if (QuyetDinhBoNhiem != null)
                QuyetDinhBoNhiem.HetHieuLuc = false;
                //CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                //    ThongTinNhanVien);
                //SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                //using (XPCollection<QuyetDinhMienNhiem> quyetdinh = new XPCollection<QuyetDinhMienNhiem>(Session, filter, sort))
                //{
                //    quyetdinh.TopReturnedObjects = 1;
                //    if (quyetdinh.Count > 0 && quyetdinh[0] == This)
                //    {
                //        //cập nhật phụ cấp chức vụ
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu1 = HSPCChucVu1Cu;
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu2 = HSPCChucVu2Cu;
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu3 = HSPCChucVu3Cu;
                //        ThongTinNhanVien.NhanVienThongTinLuong.PhuCapDienThoai = PhuCapDienThoaiCu;
                //        ThongTinNhanVien.ChucVu = QuyetDinhBoNhiem.ChucVuMoi;
                //        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu = QuyetDinhBoNhiem.NgayHuongHeSoMoi;
                //        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = QuyetDinhBoNhiem.HSPCChucVuMoi;

                //        //lần bổ nhiệm
                //        object count = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("Count()"),
                //            CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVu=?",
                //            ThongTinNhanVien, QuyetDinhBoNhiem.ChucVuMoi));
                //        if (count != null && (int)count > 0)
                //            ThongTinNhanVien.LanBoNhiemChucVu = (int)count;
                //        else
                //            ThongTinNhanVien.LanBoNhiemChucVu = 1;
                //    }
                //}
            }

            if (NgayPhatSinhBienDong != DateTime.MinValue)
            {
                //Xóa biến động giảm mức đóng
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

                //Xóa biến động thay đổi chức danh
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiChucDanh>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);
            }

            //Xóa đến ngày trong quá trình bổ nhiệm
            if (QuyetDinhBoNhiem != null)
            QuaTrinhHelper.ResetQuaTrinhBoNhiem(Session, QuyetDinhBoNhiem);

            //Xóa đến ngày diễn biến lương
            if (QuyetDinhBoNhiem != null)
            QuaTrinhHelper.ResetDienBienLuong(Session, ThongTinNhanVien);

            //Xóa đến ngày trong quá trình công tác
            if (QuyetDinhBoNhiem != null)
            QuaTrinhHelper.ResetQuaTrinhCongTac(Session, QuyetDinhBoNhiem);

            //xoa quan ly bo nhiem
            if (QuyetDinhBoNhiem != null)
            BoNhiemHelper.DeleteBoNhiem<ChiTietMienNhiem>(Session, this);

            base.OnDeleting();
        }

        private void XuLyChucVu()
        {
            int count = 0;
            //kiem nhiem
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and !HetHieuLuc", ThongTinNhanVien);
            SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCChucVu", SortingDirection.Descending);
            using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter, sort))
            {
                foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
                {
                    //kiem tra xem quyet dinh bo nhiem kiem nhiem da duoc su dung de lam quyet dinh bo nhiem chua?
                    if (QuyetDinhBoNhiem != null && item.ChucVuKiemNhiemMoi != QuyetDinhBoNhiem.ChucVuMoi
                        && item.BoPhanMoi != QuyetDinhBoNhiem.BoPhan)
                    {
                        if (count == 0)
                        {
                            ChucVuMoi = item.ChucVuKiemNhiemMoi;
                            HSPCChucVuMoi = item.ChucVuKiemNhiemMoi.HSPCChucVu;
                            HSPCChucVu1Moi = ChucVuMoi.HSPCQuanLy;
                        }
                        else
                        {
                            ChucVuKiemNhiemMoi = item.ChucVuKiemNhiemMoi;
                            HSPCKiemNhiemMoi = item.ChucVuKiemNhiemMoi.HSPCChucVu * 0.1m;
                            break;
                        }
                        count++;
                    }
                }
            }
        }

        private void XuLyPhuCap()
        {
            //List<Guid> oid = new List<Guid>();
            //if (ChucVuMoi != null)
            //{
            //    oid.Add(ChucVuMoi.Oid);
            //}
            //if (ChucVuKiemNhiemMoi != null)
            //{
            //    oid.Add(ChucVuKiemNhiemMoi.Oid);
            //}

            //if (oid.Count == 2)
            //{
            //    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and !HetHieuLuc", ThongTinNhanVien.Oid);
            //    GroupOperator go = new GroupOperator(GroupOperatorType.And);
            //    go.Operands.Add(filter);
            //    go.Operands.Add(new InOperator("ChucVuKiemNhiemMoi.Oid", oid).Not());
            //    SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCQuanLy", SortingDirection.Descending);
            //    using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, go, sort))
            //    {
            //        foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
            //        {
            //            HSPCChucVu3Moi = item.ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem2 / 100m;
            //            break;
            //        }
            //    }
            //}
        }
    }
}
