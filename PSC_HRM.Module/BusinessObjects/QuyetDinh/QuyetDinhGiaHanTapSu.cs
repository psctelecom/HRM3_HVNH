using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
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
    [ModelDefault("Caption", "Quyết định gia hạn tập sự")]
    public class QuyetDinhGiaHanTapSu : QuyetDinhCaNhan
    {
        private DateTime _NgayHetHanTapSu;
        private int _ThoiGianGiaHan;
        private QuyetDinhHuongDanTapSu _QuyetDinhHuongDanTapSu;
        private string _LyDo;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
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
                if (!IsLoading && value != null)
                {
                    ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                    if (tapSu != null)
                    {
                        TuNgay = tapSu.TuNgay;
                        DenNgay = tapSu.DenNgay;
                    }
                    TinhNgayHetHanTapSu();
                }
            }
        }
        
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

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Thời gian gia hạn (tháng)")]
        public int ThoiGianGiaHan
        {
            get
            {
                return _ThoiGianGiaHan;
            }
            set
            {
                SetPropertyValue("ThoiGianGiaHan", ref _ThoiGianGiaHan, value);
                if (!IsLoading && value > 0)
                {
                    TinhNgayHetHanTapSu();
                }
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
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

        public QuyetDinhGiaHanTapSu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhGiaHanTapSu;
            ThoiGianGiaHan = 12;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định gia hạn tập sự"));
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhHuongDanTapSu> QuyetDinhList { get; set; }

        protected override void AfterNhanVienChanged()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhHuongDanTapSu>(Session);

            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            QuyetDinhHuongDanTapSu = Session.FindObject<QuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("ListChiTietQuyetDinhHuongDanTapSu[ThongTinNhanVien=?]", ThongTinNhanVien.Oid));
        }

        //protected override void OnLoaded()
        //{
        //    base.OnLoading();

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
        //}

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
                {
                    //can bo huong dan se khong duoc huong HSPC trach nhiem
                    tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = 0;
                    //luu tru giay to ho so can bo huong dan
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.SoGiayTo = SoQuyetDinh;
                    GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                    GiayToHoSo.TrichYeu = NoiDung;
                }
            }
        }

        protected override void OnDeleting()
        {
            ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
            if (tapSu != null && tapSu.CanBoHuongDan is ThongTinNhanVien)
            {
                //khoi phuc lai HSPC trach nhiem cua can bo huong dan
                tapSu.CanBoHuongDan.NhanVienThongTinLuong.HSPCTrachNhiem = tapSu.QuyetDinhHuongDanTapSu.HSPCTrachNhiem;
                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, (ThongTinNhanVien)tapSu.CanBoHuongDan, SoQuyetDinh);
            }
            base.OnDeleting();
        }

        private void TinhNgayHetHanTapSu()
        {
            if (ThongTinNhanVien != null)
            {
                QuyetDinhTamHoanTapSu quyetDinh = Session.FindObject<QuyetDinhTamHoanTapSu>(CriteriaOperator.Parse("ThongTinNhanVien = ?", ThongTinNhanVien.Oid));
                if (quyetDinh != null)
                {
                    NgayHetHanTapSu = quyetDinh.TapSuDenNgay.AddMonths(ThoiGianGiaHan);
                }
                else
                {
                    ChiTietQuyetDinhHuongDanTapSu tapSu = Session.FindObject<ChiTietQuyetDinhHuongDanTapSu>(CriteriaOperator.Parse("QuyetDinhHuongDanTapSu=? and ThongTinNhanVien=?", QuyetDinhHuongDanTapSu.Oid, ThongTinNhanVien.Oid));
                    if (tapSu != null)
                        NgayHetHanTapSu = tapSu.DenNgay.AddMonths(ThoiGianGiaHan);
                }
            }
        }
    }
}
