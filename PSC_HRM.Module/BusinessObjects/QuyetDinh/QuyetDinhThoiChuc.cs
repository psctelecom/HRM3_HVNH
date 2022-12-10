using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BoNhiem;
using System.Collections.Generic;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thôi chức")]

    public class QuyetDinhThoiChuc : QuyetDinhCaNhan
    {
        private decimal _HSPCChucVu1Cu;
        private decimal _HSPCChucVu1Moi;
        private decimal _HSPCChucVuBaoLuu;//QNU
        private decimal _HSPCKiemNhiemMoi;
        private ChucVu _ChucVuKiemNhiemMoi;
        private decimal _HSPCChucVuMoi;
        private ChucVu _ChucVuMoi;
        private DateTime _NgayPhatSinhBienDong;
        private QuyetDinhBoNhiem _QuyetDinhBoNhiem;
        private bool _QuyetDinhMoi;
        private DateTime _NgayThoiHuongHSPCChucVu;
        private ChucVu _ChucVuKiemNhiemCu;
        private decimal _HSPCKiemNhiemCu;
        private string _LyDo;

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
        //[RuleRequiredField(DefaultContexts.Save)]
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
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayPhatSinhBienDong = value;
                }
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
                    //XuLyPhuCap();
                    HSPCChucVuMoi = value.HSPCChucVu;

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
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "HSPC chức vụ bảo lưu")]
        public decimal HSPCChucVuBaoLuu
        {
            get
            {
                return _HSPCChucVuBaoLuu;
            }
            set
            {
                SetPropertyValue("HSPCChucVuBaoLuu", ref _HSPCChucVuBaoLuu, value);
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

        //chức vụ kiêm nhiệm hiện tại
        [Browsable(false)]
        [ModelDefault("Caption", "Chức vụ kiêm nhiệm cũ")]
        public ChucVu ChucVuKiemNhiemCu
        {
            get
            {
                return _ChucVuKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("ChucVuKiemNhiemCu", ref _ChucVuKiemNhiemCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPC kiêm nhiệm cũ")]
        public decimal HSPCKiemNhiemCu
        {
            get
            {
                return _HSPCKiemNhiemCu;
            }
            set
            {
                SetPropertyValue("HSPCKiemNhiemCu", ref _HSPCKiemNhiemCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPC chức vụ 1 cũ")]
        public decimal HSPCChucVu1Cu
        {
            get
            {
                return _HSPCChucVu1Cu;
            }
            set
            {
                SetPropertyValue("HSPCChucVu1Cu", ref _HSPCChucVu1Cu, value);
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

        public QuyetDinhThoiChuc(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiChuc;

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thôi chức"));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateQuyetDinhList();

            //if (GiayToHoSo == null)
            //{
            //    GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    if (GiayToList.Count > 0 && SoQuyetDinh != null)
            //    {
            //        GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định thôi chức", SoQuyetDinh);
            //        if (GiayToList.Count > 0)
            //            GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
            //    }
            //}
        }

        protected override void QuyetDinhChanged()
        {
            if (NgayHieuLuc != DateTime.MinValue)
                NgayPhatSinhBienDong = NgayHieuLuc;
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhBoNhiem = null;
            UpdateQuyetDinhList();

            ChucVuKiemNhiemCu = ThongTinNhanVien.ChucVuKiemNhiem;
            HSPCKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem;
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
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuBaoLuu = HSPCChucVuBaoLuu;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHetHanHuongHSPChucVuBaoLuu = NgayThoiHuongHSPCChucVu;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = 0;

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
                    QuaTrinhHelper.UpdateQuaTrinhCongTac(Session, QuyetDinhBoNhiem, NgayHieuLuc);

                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    QuaTrinhHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiem, NgayHieuLuc);

                    //Cập nhật đến ngày Diễn biến lương
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
                BoNhiemHelper.CreateMienNhiem(Session, this, QuyetDinhBoNhiem.ChucVuMoi, false);

                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.NgayLap = NgayQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        }

        protected override void OnDeleting()
        {
            if (QuyetDinhMoi)
            {
                QuyetDinhBoNhiem.HetHieuLuc = false;
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhMienNhiem> quyetdinh = new XPCollection<QuyetDinhMienNhiem>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    if (quyetdinh.Count > 0 && quyetdinh[0] == This)
                    {
                        //cập nhật phụ cấp chức vụ
                        ThongTinNhanVien.ChucVu = QuyetDinhBoNhiem.ChucVuMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChucVu = QuyetDinhBoNhiem.NgayHuongHeSoMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVu = QuyetDinhBoNhiem.HSPCChucVuMoi;
                        ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem = QuyetDinhBoNhiem.HSPCTrachNhiemTruong;

                        //lần bổ nhiệm
                        object count = Session.Evaluate<QuaTrinhBoNhiem>(CriteriaOperator.Parse("Count()"),
                            CriteriaOperator.Parse("ThongTinNhanVien=? and ChucVu=?",
                            ThongTinNhanVien, QuyetDinhBoNhiem.ChucVuMoi));
                        if (count != null && (int)count > 0)
                            ThongTinNhanVien.LanBoNhiemChucVu = (int)count;
                        else
                            ThongTinNhanVien.LanBoNhiemChucVu = 1;
                    }
                }
            }

            if (NgayPhatSinhBienDong != DateTime.MinValue)
            {
                //Xóa biến động giảm mức đóng
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiLuong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

                //Xóa biến động thay đổi chức danh
                BienDongHelper.DeleteBienDong<BienDong_ThayDoiChucDanh>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);
            }

            //Xóa đến ngày trong quá trình bổ nhiệm
            QuaTrinhHelper.ResetQuaTrinhBoNhiem(Session, QuyetDinhBoNhiem);

            //Xóa đến ngày diễn biến lương
            QuaTrinhHelper.ResetDienBienLuong(Session, ThongTinNhanVien);

            //Xóa đến ngày trong quá trình công tác
            QuaTrinhHelper.ResetQuaTrinhCongTac(Session, QuyetDinhBoNhiem);

            //xoa quan ly bo nhiem
            BoNhiemHelper.DeleteBoNhiem<ChiTietMienNhiem>(Session, this);

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

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
                    if (item.ChucVuKiemNhiemMoi != QuyetDinhBoNhiem.ChucVuMoi
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
