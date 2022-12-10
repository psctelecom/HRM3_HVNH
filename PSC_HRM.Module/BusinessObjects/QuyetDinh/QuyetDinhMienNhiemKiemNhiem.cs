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
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định miễn nhiệm kiêm nhiệm")]
    
    public class QuyetDinhMienNhiemKiemNhiem : QuyetDinhCaNhan
    {
        private decimal _HSPCKiemNhiemMoi;
        private ChucVu _ChucVuKiemNhiemMoi;
        private QuyetDinhBoNhiemKiemNhiem _QuyetDinhBoNhiemKiemNhiem;
        private string _LyDo;
        private bool _QuyetDinhMoi;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bổ nhiệm kiêm nhiệm")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhList")]
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

        public QuyetDinhMienNhiemKiemNhiem(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhMienNhiemKiemNhiem;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định miễn nhiệm kiêm nhiệm"));
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhBoNhiemKiemNhiem = null;

            UpdateQuyetDinhList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateQuyetDinhList();
            MaTruong = TruongConfig.MaTruong;
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiemKiemNhiem> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);
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

                    //Cập nhật bảng chức vụ kiêm nhiệm
                    ChucVuKiemNhiem chucvukiemnhiem = Session.FindObject<ChucVuKiemNhiem>(CriteriaOperator.Parse("ThongTinNhanVien =? AND QuyetDinhBoNhiemKiemNhiem = ?",
                        QuyetDinhBoNhiemKiemNhiem.ThongTinNhanVien.Oid, QuyetDinhBoNhiemKiemNhiem.Oid));
                    if (chucvukiemnhiem != null)
                        chucvukiemnhiem.DaMienNhiem = true;
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

            }
        }

        protected override void OnDeleting()
        {
            //cập nhật hồ sơ
            if (QuyetDinhMoi)
            {
                QuyetDinhBoNhiemKiemNhiem.HetHieuLuc = false;

                //Cập nhật bảng chức vụ kiêm nhiệm
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien =? AND BoPhan = ? AND ChucVu = ? AND NgayBoNhiem = ?",
                       QuyetDinhBoNhiemKiemNhiem.ThongTinNhanVien.Oid, QuyetDinhBoNhiemKiemNhiem.BoPhanMoi.Oid, QuyetDinhBoNhiemKiemNhiem.ChucVuKiemNhiemMoi.Oid, QuyetDinhBoNhiemKiemNhiem.NgayHieuLuc);
                XPCollection<ChucVuKiemNhiem> dschucvukiemnhiem = new XPCollection<ChucVuKiemNhiem>(Session);
                ChucVuKiemNhiem chucvukiemnhiem = dschucvukiemnhiem.Session.FindObject<ChucVuKiemNhiem>(filter);
                chucvukiemnhiem.DaMienNhiem = false;
            }

            //Xóa đến ngày trong quá trình bổ nhiệm
            QuaTrinhHelper.ResetQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemKiemNhiem);

            //Xóa den ngay diễn biến lương
            QuaTrinhHelper.ResetDienBienLuong(Session, ThongTinNhanVien);

            //Xóa đến ngày trong quá trình công tác
            QuaTrinhHelper.ResetQuaTrinhCongTac(Session, QuyetDinhBoNhiemKiemNhiem);

            //xoa quan ly bo nhiem
            BoNhiemHelper.DeleteBoNhiem<ChiTietMienNhiem>(Session, this);

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

            XuLyPhuCap();
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
