using System;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi
{

    [ModelDefault("Caption", "Chi tiết công tác phí")]
    public class ChiTietQuanLyHoatDongQuanLy : BaseObject
    {

        private QuanLyHoatDongQuanLy _QuanLyHoatDongQuanLy;

        private NhanVien _NhanVien;
        private HoatDongQuanLy _HoatDongQuanLy;
        private string _NoiDungCongViec;
        private DateTime _NgayThucHien;
        private decimal _TongThoiGiang;
        private decimal _HeSoQuyDoi;
        private decimal _SoTietQuyDoi;
        private string _GhiChu;
        private bool _XacNhan;

        [ModelDefault("Caption", "QuanLyHoatDongQuanLy")]
        [Association("QuanLyHoatDongQuanLy-ListChiTietQuanLyHoatDongQuanLy")]
        [Browsable(false)]
        public QuanLyHoatDongQuanLy QuanLyHoatDongQuanLy
        {
            get { return _QuanLyHoatDongQuanLy; }
            set { SetPropertyValue("QuanLyHoatDongQuanLy", ref _QuanLyHoatDongQuanLy, value); }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }     

        [ModelDefault("Caption", "Hoạt động quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public HoatDongQuanLy HoatDongQuanLy
        {
            get { return _HoatDongQuanLy; }
            set { SetPropertyValue("HoatDongQuanLy", ref _HoatDongQuanLy, value);
                if(!IsLoading && value != null)
                {
                    HeSoQuyDoi = value.GioQuyDoi / value.SoLuong;
                }
            }
        }

        [ModelDefault("Caption", "Nội dung công việc")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Size(-1)]
        public string NoiDungCongViec
        {
            get { return _NoiDungCongViec; }
            set { SetPropertyValue("NoiDungCongViec", ref _NoiDungCongViec, value); }
        }

        [ModelDefault("Caption", "Ngày thực hiện")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayThucHien
        {
            get { return _NgayThucHien; }
            set { SetPropertyValue("NgayThucHien", ref _NgayThucHien, value); }
        }

        [ModelDefault("Caption", "Tổng thời giang(Giờ)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("TongThoiGiang", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        [ImmediatePostData]
        public decimal TongThoiGiang
        {
            get { return _TongThoiGiang; }
            set
            {
                SetPropertyValue("TongThoiGiang ", ref _TongThoiGiang, value);
                if(!IsLoading && value != 0 && HeSoQuyDoi != 0)
                {
                    SoTietQuyDoi = value / HeSoQuyDoi;
                    OnChanged("SoTietQuyDoi");
                }
            }
        }

        [ModelDefault("Caption", "Hệ số quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal HeSoQuyDoi
        {
            get { return _HeSoQuyDoi; }
            set
            {
                SetPropertyValue("HeSoQuyDoi ", ref _HeSoQuyDoi, value);
                if (!IsLoading && value != 0 && TongThoiGiang != 0)
                {
                    SoTietQuyDoi = TongThoiGiang / value;
                    OnChanged("SoTietQuyDoi");
                }
            }
        }

        [ModelDefault("Caption", "Số tiết quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal SoTietQuyDoi
        {
            get { return _SoTietQuyDoi; }
            set
            {
                SetPropertyValue("SoTietQuyDoi ", ref _SoTietQuyDoi, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Xác nhận")]
        [ImmediatePostData]
        public bool XacNhan
        {
            get { return _XacNhan; }
            set
            {
                SetPropertyValue("XacNhan", ref _XacNhan, value);
            }
        }
       
        public ChiTietQuanLyHoatDongQuanLy(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}