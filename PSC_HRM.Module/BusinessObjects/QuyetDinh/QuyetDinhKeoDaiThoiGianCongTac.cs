using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định kéo dài thời gian công tác")]
    public class QuyetDinhKeoDaiThoiGianCongTac : QuyetDinhCaNhan
    {
        // Fields...
        private DateTime _NghiViecTuNgay;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ImmediatePostData]
        [ModelDefault("Caption", "Nghỉ hưu từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NghiViecTuNgay
        {
            get
            {
                return _NghiViecTuNgay;
            }
            set
            {
                SetPropertyValue("NghiViecTuNgay", ref _NghiViecTuNgay, value);                
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
                    if (MaTruong.Equals("NEU"))
                        XuLyKeoDaiCongTac();
                    else
                        DenNgay = value.AddYears(1).AddDays(-1);
                }
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        public QuyetDinhKeoDaiThoiGianCongTac(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = HamDungChung.GetServerTime();
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhKeoDaiThoiGianCongTac;

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định kéo dài thời gian công tác"));
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

        protected override void AfterNhanVienChanged() 
        {
            if (ThongTinNhanVien != null && ThongTinNhanVien.NgaySinh != DateTime.MinValue)
            {
                QuyetDinhKeoDaiThoiGianCongTac quyetDinhKeoDai = Session.FindObject<QuyetDinhKeoDaiThoiGianCongTac>(CriteriaOperator.Parse("ThongTinNhanVien=?", this.ThongTinNhanVien));
                if (quyetDinhKeoDai != null)
                {
                    NghiViecTuNgay = quyetDinhKeoDai.DenNgay.AddDays(1);
                }
                else
                {
                    TuoiNghiHuu tuoiNghiHuu = Session.FindObject<TuoiNghiHuu>(CriteriaOperator.Parse("GioiTinh=?", ThongTinNhanVien.GioiTinh));
                    if (tuoiNghiHuu != null)
                        NghiViecTuNgay = ThongTinNhanVien.NgaySinh.AddYears(tuoiNghiHuu.Tuoi);
                    else if (ThongTinNhanVien.GioiTinh == GioiTinhEnum.Nam)
                        NghiViecTuNgay = ThongTinNhanVien.NgaySinh.AddYears(60);
                    else
                        NghiViecTuNgay = ThongTinNhanVien.NgaySinh.AddYears(55);
                }

                TuNgay = NghiViecTuNgay;
            }
        }

        protected void XuLyKeoDaiCongTac()
        {
            QuyetDinhKeoDaiThoiGianCongTac quyetDinhKeoDai = Session.FindObject<QuyetDinhKeoDaiThoiGianCongTac>(CriteriaOperator.Parse("ThongTinNhanVien=?", this.ThongTinNhanVien));
            if (quyetDinhKeoDai != null)
            {
                if (ThongTinNhanVien.NhanVienTrinhDo.HocHam != null && ThongTinNhanVien.NhanVienTrinhDo.HocHam.MaQuanLy.Equals("GS"))
                    DenNgay = TuNgay.AddYears(10).AddDays(-1);
                else
                    DenNgay = TuNgay.AddYears(1).AddDays(-1);
            }
            else
                DenNgay = TuNgay.AddYears(1).AddDays(-1);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && ThongTinNhanVien != null)
            {
                ThongTinNhanVien.NgayNghiHuu = DenNgay;

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
                ThongTinNhanVien.NgayNghiHuu = DateTime.MinValue;

                //xoa giay to
                if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                    GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);
            }
            base.OnDeleting();
        }
    }
}
