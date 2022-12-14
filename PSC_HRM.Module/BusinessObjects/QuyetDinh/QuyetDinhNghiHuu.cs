using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nghỉ hưu")]

    public class QuyetDinhNghiHuu : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrang;
        private DateTime _NgayPhatSinhBienDong;
        private DateTime _NghiViecTuNgay;
        private DiaChi _NoiCuTruSauKhiThoiViec;

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
        [ModelDefault("Caption", "Nghỉ việc từ ngày")]
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
                if (!IsLoading && value != DateTime.MinValue)
                {
                    NgayPhatSinhBienDong = value;
                }
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("Caption", "Nơi cư trú sau khi nghỉ")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public DiaChi NoiCuTruSauKhiThoiViec
        {
            get
            {
                return _NoiCuTruSauKhiThoiViec;
            }
            set
            {
                SetPropertyValue("NoiCuTruSauKhiThoiViec", ref _NoiCuTruSauKhiThoiViec, value);
            }
        }

        //Lưu vết tình trạng
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

        public QuyetDinhNghiHuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhNghiHuu;
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định nghỉ hưu"));
        }

        protected override void AfterNhanVienChanged()
        {
            if (ThongTinNhanVien.NoiOHienNay == null)
                ThongTinNhanVien.NoiOHienNay = new DiaChi(Session);
            NoiCuTruSauKhiThoiViec = ThongTinNhanVien.NoiOHienNay != null ?
                                        ThongTinNhanVien.NoiOHienNay:
                                        ThongTinNhanVien.DiaChiThuongTru;

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
            if (!IsDeleted && ThongTinNhanVien != null)
            {
                CriteriaOperator filter;
                ThongTinNhanVien.NgayNghiViec = NghiViecTuNgay;
                ThongTinNhanVien.NgayNghiHuu = NghiViecTuNgay;
                //update tình trạng - LUH không xài do thay đổi theo ngày bắt đầu nghỉ việc
                if (NghiViecTuNgay <= HamDungChung.GetServerTime())
                {
                    filter = CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ hưu");
                    TinhTrang tinhtrang = Session.FindObject<TinhTrang>(filter);
                    if (tinhtrang == null)
                    {
                        tinhtrang = new TinhTrang(Session);
                        tinhtrang.TenTinhTrang = "Nghỉ hưu";
                        tinhtrang.MaQuanLy = "NH";
                    }
                    ThongTinNhanVien.TinhTrang = tinhtrang;
                }
                //quản lý biến động
                //giảm lao động
                filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (hoSoBaoHiem != null && NgayPhatSinhBienDong != DateTime.MinValue)
                {
                    BienDongHelper.CreateBienDongGiamLaoDong(Session, this, NgayPhatSinhBienDong, LyDoNghiEnum.ThoiViec);
                }

                //luu tru giay to ho so can bo huong dan
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.TrichYeu = NoiDung;
            }
        }

        protected override void OnDeleting()
        {
            if (TinhTrang != null)
                ThongTinNhanVien.TinhTrang = TinhTrang;
            else
            {
                CriteriaOperator filter = CriteriaOperator.Parse("TenTinhTrang like ?",
                                "%đang làm việc%");
                TinhTrang tinhtrang = Session.FindObject<TinhTrang>(filter);
                if (tinhtrang != null)
                    ThongTinNhanVien.TinhTrang = tinhtrang;
            }
            ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;
            ThongTinNhanVien.NgayNghiHuu = DateTime.MinValue;
            //xóa biến động
            BienDongHelper.DeleteBienDong<BienDong_GiamLaoDong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);

            base.OnDeleting();
        }
    }

}
