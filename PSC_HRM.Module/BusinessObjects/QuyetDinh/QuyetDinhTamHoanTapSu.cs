using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.QuaTrinh;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TapSu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định tạm hoãn tập sự")]
    public class QuyetDinhTamHoanTapSu : QuyetDinhCaNhan
    {
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        private NgachLuong _NgachLuong;
        private DateTime _HoanTuNgay;
        private DateTime _HoanDenNgay;
        private DateTime _TapSuTuNgay;
        private DateTime _TapSuDenNgay;
        private bool _QuyetDinhMoi;
        private string _LyDo;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định hướng dẫn tập sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhList")]
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
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime HoanTuNgay
        {
            get
            {
                return _HoanTuNgay;
            }
            set
            {
                SetPropertyValue("HoanTuNgay", ref _HoanTuNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime HoanDenNgay
        {
            get
            {
                return _HoanDenNgay;
            }
            set
            {
                SetPropertyValue("HoanDenNgay", ref _HoanDenNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    TapSuTuNgay = value.AddDays(1);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày 1")]
        public DateTime TapSuTuNgay
        {
            get
            {
                return _TapSuTuNgay;
            }
            set
            {
                SetPropertyValue("TapSuTuNgay", ref _TapSuTuNgay, value);
                if (!IsLoading && value != DateTime.MinValue &&
                    QuyetDinhHuongDanTapSu != null)
                {
                    ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                    if (tapSu != null)
                    {
                        TimeSpan soNgay = tapSu.DenNgay.Subtract(HoanTuNgay);
                        TapSuDenNgay = value.AddDays(soNgay.Days);
                    }
                }
            }
        }

        [ModelDefault("Caption", "Đến ngày 1")]
        public DateTime TapSuDenNgay
        {
            get
            {
                return _TapSuDenNgay;
            }
            set
            {
                SetPropertyValue("TapSuDenNgay", ref _TapSuDenNgay, value);
            }
        }

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public QuyetDinhTamHoanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            HoanTuNgay = HamDungChung.GetServerTime();
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhTamHoanTapSu;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định tạm hoãn tập sự"));
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhHuongDanTapSu> QuyetDinhList { get; set; }

        protected override void AfterNhanVienChanged()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhHuongDanTapSu>(Session);

            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            QuyetDinhHuongDanTapSu = Session.FindObject<QuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid));

            NgachLuong = ThongTinNhanVien.NhanVienThongTinLuong.NgachLuong;

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

                        //cán bộ hướng dẫn tập sự
                        QuaTrinhHelper.UpdateDienBienLuong(Session, this, (ThongTinNhanVien)tapSu.CanBoHuongDan, NgayHieuLuc);
                    }

                    if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    {
                        GiayToHoSo.Delete();
                        //luu tru giay to ho so can bo 
                        GiayToHoSoHelper.CreateGiayToQuyetDinh(Session, SoQuyetDinh, tapSu.ThongTinNhanVien, NgayHieuLuc, GiayToHoSo.LuuTru, NoiDung);

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
                if (QuyetDinhMoi )
                {
                    //cập nhật hồ sơ
                    tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = QuyetDinhHuongDanTapSu.HSPCTrachNhiem;

                    //cập nhật diễn biến lương
                    QuaTrinhHelper.ResetDienBienLuong(Session, (ThongTinNhanVien)tapSu.CanBoHuongDan);

                }

                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                {
                    //xoa giay to ho so can bo 
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, tapSu.ThongTinNhanVien, SoQuyetDinh);

                    //xoa giay to can bo huong dan cu
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, (ThongTinNhanVien)tapSu.CanBoHuongDan, SoQuyetDinh);
                }
            }
            base.OnDeleting();
        }
    }
}
