using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.ThanhToan
{
    [ModelDefault("Caption", "Đơn giá thanh toán (PMS)")]
    [Appearance("Hide_NEU", TargetItems = "KyTinhPMS;HocKy"
            , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.MaQuanLy = 'NEU'")]
    public class DonGiaThanhToan_VuotGio : ThongTinChungPMS
    {
        private decimal _DonGiaTienGiang;
        private decimal _DonGiaChamBaiDH;
        private decimal _DonGiaChamBaiCH;
        private decimal _DonGiaChamBaiNCS;
        private decimal _DonGiaChamBTL_TL;
        private decimal _DonGiaCongTacPhi;
        private decimal _DonGiaDinhMuc;
        private decimal _DonGiaLuongCoBan;
        private decimal _DonGiaTraLoiHeThong;
        private decimal _DonGiaChamDiemTraLoiCauHoiSV;

        [ModelDefault("Caption", "Đơn giá tiền giảng")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaTienGiang", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaTienGiang
        {
            get { return _DonGiaTienGiang; }
            set { SetPropertyValue("DonGiaTienGiang", ref _DonGiaTienGiang, value); }
        }
        [ModelDefault("Caption", "Đơn giá giờ định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaCongTacPhi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaDinhMuc
        {
            get { return _DonGiaDinhMuc; }
            set { SetPropertyValue("DonGiaDinhMuc", ref _DonGiaDinhMuc, value); }
        }

        [ModelDefault("Caption", "Đơn giá lương cơ bản")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaCongTacPhi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaLuongCoBan
        {
            get { return _DonGiaLuongCoBan; }
            set { SetPropertyValue("DonGiaLuongCoBan", ref _DonGiaLuongCoBan, value); }
        }
        [ModelDefault("Caption", "Đơn giá chấm bài DH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaChamBaiDH", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaChamBaiDH
        {
            get { return _DonGiaChamBaiDH; }
            set { SetPropertyValue("DonGiaChamBaiDH", ref _DonGiaChamBaiDH, value); }
        }
        [ModelDefault("Caption", "Đơn giá chấm bài CH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaChamBaiCH", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaChamBaiCH
        {
            get { return _DonGiaChamBaiCH; }
            set { SetPropertyValue("DonGiaChamBaiCH", ref _DonGiaChamBaiCH, value); }
        }
        [ModelDefault("Caption", "Đơn giá chấm bài NCS")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaChamBaiNCS", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaChamBaiNCS
        {
            get { return _DonGiaChamBaiNCS; }
            set { SetPropertyValue("DonGiaChamBaiNCS", ref _DonGiaChamBaiNCS, value); }
        }
        [ModelDefault("Caption", "Đơn giá chấm BTL_TL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaChamBTL_TL", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaChamBTL_TL
        {
            get { return _DonGiaChamBTL_TL; }
            set { SetPropertyValue("DonGiaChamBTL_TL", ref _DonGiaChamBTL_TL, value); }
        }
        [ModelDefault("Caption", "Đơn giá trả lời trên hệ thống")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaCongTacPhi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaTraLoiHeThong
        {
            get { return _DonGiaTraLoiHeThong; }
            set { SetPropertyValue("DonGiaTraLoiHeThong", ref _DonGiaTraLoiHeThong, value); }
        }
        [ModelDefault("Caption", "Đơn giá chấm điểm trả lới SV")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaCongTacPhi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaChamDiemTraLoiCauHoiSV
        {
            get { return _DonGiaChamDiemTraLoiCauHoiSV; }
            set { SetPropertyValue("DonGiaChamDiemTraLoiCauHoiSV", ref _DonGiaChamDiemTraLoiCauHoiSV, value); }
        }
        [ModelDefault("Caption", "Đơn giá công tác phí")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaCongTacPhi", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal DonGiaCongTacPhi
        {
            get { return _DonGiaCongTacPhi; }
            set { SetPropertyValue("DonGiaCongTacPhi", ref _DonGiaCongTacPhi, value); }
        }
        public DonGiaThanhToan_VuotGio(Session session) : base(session) { }
       
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}