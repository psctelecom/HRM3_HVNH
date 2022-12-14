using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.ThuNhap.Thue;
using DevExpress.Data.Filtering;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;


namespace PSC_HRM.Module.ThuNhap.ChungTu
{
    [DefaultClassOptions]
    [ImageName("BO_TienMat")]
    [DefaultProperty("SoChungTu")]
    [ModelDefault("Caption", "Chứng từ chi tiền mặt")]
    public class ChiTMLuongNhanVien : ChungTu
    {
        private bool _TatCaCanBo;

        [ModelDefault("Caption", "Tất cả cán bộ")]
        public bool TatCaCanBo
        {
            get
            {
                return _TatCaCanBo;
            }
            set
            {
                SetPropertyValue("TatCaCanBo", ref _TatCaCanBo, value);
            }
        }

        [Aggregated]
        [Association("ChiTMLuongNhanVien-ChiTietNhanVien")]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTMLuongNhanVienChiTiet> ChiTietList
        {
            get
            {
                return GetCollection<ChiTMLuongNhanVienChiTiet>("ChiTietList");
            }
        }

        public ChiTMLuongNhanVien(Session session) : base(session) { }
    }
}
