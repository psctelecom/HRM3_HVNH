using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định hợp đồng")]
    public class QuyetDinhHopDong : QuyetDinhCaNhan
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private decimal _MucLuong;
        private DateTime _NgayPhatSinhBienDong;
        private int _ThoiGianThuViec = 3;
        private bool _QuyetDinhMoi;

        [RuleRequiredField(DefaultContexts.Save)]
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

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Mức lương được hưởng")]
        public decimal MucLuong
        {
            get
            {
                return _MucLuong;
            }
            set
            {
                SetPropertyValue("MucLuong", ref _MucLuong, value);
            }
        }

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Thời gian thử việc (tháng)")]
        public int ThoiGianThuViec
        {
            get
            {
                return _ThoiGianThuViec;
            }
            set
            {
                SetPropertyValue("ThoiGianThuViec", ref _ThoiGianThuViec, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && ThoiGianThuViec > 0)
                {
                    DenNgay = TuNgay.AddMonths(ThoiGianThuViec).AddDays(-1);
                }
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
                    DenNgay = value.AddMonths(ThoiGianThuViec).AddDays(-1);
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        public QuyetDinhHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhHopDong;

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

            //phát sinh tăng lao động ở đây. bỏ phát sinh tăng lao động trong hợp đồng lần đầu
            if (!IsDeleted)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?",
                    ThongTinNhanVien);
                HoSoBaoHiem hoSoBaoHiem = Session.FindObject<HoSoBaoHiem>(filter);
                if (QuyetDinhMoi)
                {
                    //cập nhật thông tin
                    ThongTinNhanVien.NgayVaoBienChe = NgayHieuLuc;
                    ThongTinNhanVien.NhanVienThongTinLuong.PhanLoai = ThongTinLuongEnum.LuongKhoanKhongBHXH;
                    ThongTinNhanVien.NhanVienThongTinLuong.LuongKhoan = MucLuong;

                    //biến động tăng lao động
                    if (hoSoBaoHiem != null &&
                        NgayPhatSinhBienDong != DateTime.MinValue)
                    {
                        BienDongHelper.CreateBienDongTangLaoDong(Session, this, NgayPhatSinhBienDong);
                    }
                }

                //không cập nhật diễn biến lương do mới được tuyển dụng (không có diễn biến lương)
                //tạo mới diễn biến lương
                QuaTrinhHelper.CreateDienBienLuong(Session, this, ThongTinNhanVien, NgayHieuLuc,null);

                //Bảo hiểm xã hội
                if (hoSoBaoHiem != null &&
                    NgayHieuLuc != DateTime.MinValue)
                {
                    QuaTrinhHelper.CreateQuaTrinhThamGiaBHXH(Session, hoSoBaoHiem, this, NgayHieuLuc);
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
            //xóa diễn biến lương
            QuaTrinhHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this);

            //xóa quá trình tham gia BHXH
            QuaTrinhHelper.DeleteQuaTrinh<QuaTrinhThamGiaBHXH>(Session, CriteriaOperator.Parse("HoSoBaoHiem.ThongTinNhanVien=?", ThongTinNhanVien.Oid));

            //xóa biến động
            if (NgayPhatSinhBienDong != DateTime.MinValue)
                BienDongHelper.DeleteBienDong<BienDong_TangLaoDong>(Session, ThongTinNhanVien, NgayPhatSinhBienDong);

            //xoa giay to
            if (!String.IsNullOrWhiteSpace(SoQuyetDinh))
                GiayToHoSoHelper.DeleteGiayToHoSo(Session, ThongTinNhanVien, SoQuyetDinh);
            
            base.OnDeleting();
        }
    }

}
