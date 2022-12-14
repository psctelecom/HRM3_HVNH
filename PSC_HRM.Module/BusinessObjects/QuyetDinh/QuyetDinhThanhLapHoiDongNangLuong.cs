using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng nâng lương")]
    public class QuyetDinhThanhLapHoiDongNangLuong : QuyetDinh
    {
        private int _Nam;

        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("Caption", "Năm")]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhThanhLapHoiDongNangLuong-ListChiTietQuyetDinhThanhLapHoiDongNangLuong")]
        public XPCollection<ChiTietQuyetDinhThanhLapHoiDongNangLuong> ListChiTietQuyetDinhThanhLapHoiDongNangLuong
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhThanhLapHoiDongNangLuong>("ListChiTietQuyetDinhThanhLapHoiDongNangLuong");
            }
        }

        public QuyetDinhThanhLapHoiDongNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongNangLuong;
        }
        
    }

}
