using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BoNhiem;
using PSC_HRM.Module.GiayTo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thôi chức kiêm nhiệm")]
    
    public class QuyetDinhThoiChucKiemNhiem : QuyetDinhCaNhan
    {
        private decimal _HSPCKiemNhiemCu;
        private ChucVu _ChucVuKiemNhiemCu;
        private decimal _HSPCKiemNhiemMoi;
        private ChucVu _ChucVuKiemNhiemMoi;
        private QuyetDinhBoNhiemKiemNhiem _QuyetDinhBoNhiemKiemNhiem;
        private DateTime _NgayThoiHuongHSPCChucVu;
        private bool _QuyetDinhMoi;

        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định bổ nhiệm kiêm nhiệm")]
        public QuyetDinhBoNhiemKiemNhiem QuyetDinhBoNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemKiemNhiem", ref _QuyetDinhBoNhiemKiemNhiem, value);
                if (!IsLoading && value != null)
                {
                    XuLyChucVu();
                }
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

        //tính toán chức vụ kiêm nhiệm mới (cho phép người dùng thay đổi)
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

        public QuyetDinhThoiChucKiemNhiem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiChucKiemNhiem;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thôi chức kiêm nhiệm"));
        }

        protected override void AfterNhanVienChanged()
        {
            ChucVuKiemNhiemCu = ThongTinNhanVien.ChucVuKiemNhiem;
            HSPCKiemNhiemCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem;
            QuyetDinhBoNhiemKiemNhiem = null;

            UpdateQuyetDinhList();
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiemKiemNhiem> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            //if (GiayToHoSo == null)
            //{
            //    GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            //    if (GiayToList.Count > 0 && SoQuyetDinh != null)
            //    {
            //        GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
            //        if (GiayToList.Count > 0)
            //            GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
            //    }
            //}
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                QuyetDinhBoNhiemKiemNhiem.HetHieuLuc = true;
                //cập nhật hồ sơ
                if (QuyetDinhMoi)
                {
                    ThongTinNhanVien.ChucVuKiemNhiem = ChucVuKiemNhiemMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem = HSPCKiemNhiemMoi;
                }

                if (NgayHieuLuc != DateTime.MinValue)
                {
                    //cập nhật đến ngày của quá trình công tác
                    QuaTrinhHelper.UpdateQuaTrinhCongTac(Session, QuyetDinhBoNhiemKiemNhiem, NgayHieuLuc);

                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    QuaTrinhHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemKiemNhiem, NgayHieuLuc);

                    //Cập nhật Diễn biến lương
                    QuaTrinhHelper.UpdateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc);
                }

                //quan ly bo nhiem
                BoNhiemHelper.CreateMienNhiem(Session, this, QuyetDinhBoNhiemKiemNhiem.ChucVuKiemNhiemMoi, true);

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
            //cập nhật hồ sơ
            if (QuyetDinhMoi)
            {
                QuyetDinhBoNhiemKiemNhiem.HetHieuLuc = false;
                //kiem tra xem quyet dinh nay co phai la quyet dinh moi nhat khong?
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                using (XPCollection<QuyetDinhMienNhiemKiemNhiem> quyetdinh = new XPCollection<QuyetDinhMienNhiemKiemNhiem>(Session, filter, sort))
                {
                    quyetdinh.TopReturnedObjects = 1;
                    if (quyetdinh.Count > 0)
                    {
                        if (quyetdinh[0] == This)
                        {
                            ThongTinNhanVien.ChucVuKiemNhiem = ChucVuKiemNhiemCu;
                            ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiem = HSPCKiemNhiemCu;
                        }
                    }
                }
            }

            //Xóa đến ngày trong quá trình bổ nhiệm
            QuaTrinhHelper.ResetQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemKiemNhiem);

            //Xóa den ngay diễn biến lương
            QuaTrinhHelper.ResetDienBienLuong(Session, ThongTinNhanVien);

            //Xóa đến ngày trong quá trình công tác
            QuaTrinhHelper.ResetQuaTrinhCongTac(Session, QuyetDinhBoNhiemKiemNhiem);

            //xoa quan ly bo nhiem
            BoNhiemHelper.DeleteBoNhiem<ChiTietMienNhiem>(Session, this);

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            base.OnDeleting();
        }

        private void XuLyChucVu()
        {
            int chucVuKiemNhiem = 0;
            //kiem nhiem
            CriteriaOperator filter = CriteriaOperator.Parse("Oid!=? and ThongTinNhanVien=? and !HetHieuLuc", QuyetDinhBoNhiemKiemNhiem.Oid, ThongTinNhanVien);
            SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCChucVu", SortingDirection.Descending);
            using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter, sort))
            {
                foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
                {
                    if (item.ChucVuKiemNhiemMoi == ThongTinNhanVien.ChucVu)
                        chucVuKiemNhiem++;
                    else
                        chucVuKiemNhiem = 0;

                    if (chucVuKiemNhiem == 0
                        || chucVuKiemNhiem == 2)
                    {
                        ChucVuKiemNhiemMoi = item.ChucVuKiemNhiemMoi;
                        HSPCKiemNhiemMoi = item.ChucVuKiemNhiemMoi.HSPCChucVu * 0.1m;
                    }
                }
            }
        }

        private void XuLyPhuCap()
        {
            //if (ChucVuKiemNhiemMoi != null)
            //{
            //    int chucVu = 0, chucVuKiemNhiem = 0;
            //    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? and !HetHieuLuc", ThongTinNhanVien.Oid);
            //    SortProperty sort = new SortProperty("ChucVuKiemNhiemMoi.HSPCQuanLy", SortingDirection.Descending);
            //    using (XPCollection<QuyetDinhBoNhiemKiemNhiem> qdList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter, sort))
            //    {
            //        foreach (QuyetDinhBoNhiemKiemNhiem item in qdList)
            //        {
            //            if (item.ChucVuKiemNhiemMoi == ThongTinNhanVien.ChucVu)
            //                chucVu++;
            //            else
            //                chucVu = 0;
            //            if (item.ChucVuKiemNhiemMoi == ThongTinNhanVien.ChucVuKiemNhiem)
            //                chucVuKiemNhiem++;
            //            else
            //                chucVuKiemNhiem = 0;
            //            if ((chucVu == 0 && chucVuKiemNhiem == 0)
            //                || chucVuKiemNhiem == 2)
            //            {
            //                HSPCChucVu3Moi = item.ChucVuKiemNhiemMoi.HSPCQuanLy * HamDungChung.CauHinhChung.CauHinhHoSo.TyLeHeSoKiemNhiem2 / 100m;
            //                break;
            //            }
            //        }
            //    }
            //}
        }
    }
}
