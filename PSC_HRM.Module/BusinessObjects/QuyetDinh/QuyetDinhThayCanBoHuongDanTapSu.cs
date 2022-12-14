using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thay cán bộ hướng dẫn tập sự")]
    public class QuyetDinhThayCanBoHuongDanTapSu : QuyetDinhCaNhan
    {
        // Fields...
        private bool _QuyetDinhMoi;
        private DateTime _TuNgay;
        private ThongTinNhanVien _CanBoHuongDanMoi;
        private BoPhan _BoPhanCanBoHuongDanMoi;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
        public QuyetDinhHuongDanTapSu QuyetDinhHuongDanTapSu
        {
            get
            {
                return _QuyetDinhHuongDanTapSu;
            }
            set
            {
                SetPropertyValue("QuyetDinhHuongDanTapSu", ref _QuyetDinhHuongDanTapSu, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị cán bộ hướng dẫn mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanCanBoHuongDanMoi
        {
            get
            {
                return _BoPhanCanBoHuongDanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanCanBoHuongDanMoi", ref _BoPhanCanBoHuongDanMoi, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ hướng dẫn mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien CanBoHuongDanMoi
        {
            get
            {
                return _CanBoHuongDanMoi;
            }
            set
            {
                SetPropertyValue("CanBoHuongDanMoi", ref _CanBoHuongDanMoi, value);
            }
        }

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

        public QuyetDinhThayCanBoHuongDanTapSu(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<QuyetDinhHuongDanTapSu> QuyetDinhList { get; set; }

        protected override void AfterNhanVienChanged()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhHuongDanTapSu>(Session);

            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            QuyetDinhHuongDanTapSu = Session.FindObject<QuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid));

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThayCanBoHuongDanTapSu;
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

            if (!IsDeleted)
            {
                ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
                {
                    if (QuyetDinhMoi)
                    {
                        tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                        CanBoHuongDanMoi.NhanVienThongTinLuong.HSPCTrachNhiem = QuyetDinhHuongDanTapSu.HSPCTrachNhiem;
                    }
                    if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    {
                        GiayToHoSo.Delete();
                        //luu tru giay to ho so can bo huong dan moi
                        GiayToHoSoHelper.CreateGiayToQuyetDinh(Session, SoQuyetDinh, CanBoHuongDanMoi, NgayHieuLuc, GiayToHoSo.LuuTru, NoiDung);

                        //luu tru giay to ho so can bo huong dan cu
                        GiayToHoSoHelper.CreateGiayToQuyetDinh(Session, SoQuyetDinh, (ThongTinNhanVien)tapSu.CanBoHuongDan, NgayHieuLuc, GiayToHoSo.LuuTru, NoiDung);
                    }
                }
            }
        }

        protected override void OnDeleting()
        {
            ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
            if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
            {
                if (QuyetDinhMoi)
                {
                    //khoi phuc lai du lieu
                    tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = QuyetDinhHuongDanTapSu.HSPCTrachNhiem;
                    CanBoHuongDanMoi.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                }

                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                {
                    //xoa giay to ho so can bo huong dan moi
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, CanBoHuongDanMoi, SoQuyetDinh);

                    //xoa giay to can bo huong dan cu
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, (ThongTinNhanVien)tapSu.CanBoHuongDan, SoQuyetDinh);
                }
            }
            base.OnDeleting();
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thay cán bộ hướng dẫn tập sự"));
        }
    }

}
