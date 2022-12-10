using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.NEU.DaoTaoTuXa
{
    [ModelDefault("Caption","Khối lượng đào tạo từ xa")]
    public class KhoiLuongGiangDay_TaoTaoTuXa : ThongTinChungPMS
    {

        [Aggregated]
        [Association("KhoiLuongGiangDay_TaoTaoTuXa-ListChiTiet")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTiet_KhoiLuongGiangDay_TaoTaoTuXa> ListChiTiet
        {
            get
            {
                return GetCollection<ChiTiet_KhoiLuongGiangDay_TaoTaoTuXa>("ListChiTiet");
            }
        }
        public KhoiLuongGiangDay_TaoTaoTuXa(Session session)
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