using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Thue
{
    [NonPersistent]
    [ModelDefault("Caption", "Miễn giảm thuế TNCN")]
    public class MienGiamThueTNCN : BaseObject
    {
        // Fields...
        private int _SoThang;
        private decimal _MucTNTTDuocMienGiam;
        private DateTime _DenNgay;
        private DateTime _TuNgay;

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    SoThang = TuNgay.TinhSoThang(DenNgay) + 1;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
                    SoThang = TuNgay.TinhSoThang(DenNgay) + 1;
            }
        }

        [ModelDefault("Caption", "Số tháng")]
        public int SoThang
        {
            get
            {
                return _SoThang;
            }
            set
            {
                SetPropertyValue("SoThang", ref _SoThang, value);
            }
        }

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Mức TNTT được miễn giảm")]
        public decimal MucTNTTDuocMienGiam
        {
            get
            {
                return _MucTNTTDuocMienGiam;
            }
            set
            {
                SetPropertyValue("MucTNTTDuocMienGiam", ref _MucTNTTDuocMienGiam, value);
            }
        }

        public MienGiamThueTNCN(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = new DateTime(HamDungChung.GetServerTime().Year - 1, 7, 1);
            DenNgay = TuNgay.SetTime(SetTimeEnum.EndYear);
            MucTNTTDuocMienGiam = 5000000;
        }

        public void XuLy(IObjectSpace obs, ToKhaiQuyetToanThueTNCN obj)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ToKhai", obj.Oid);
            param[1] = new SqlParameter("@SoThang", SoThang);
            param[2] = new SqlParameter("@MucTNTTDuocMienGiam", MucTNTTDuocMienGiam);

            SqlCommand cmd = DataProvider.GetCommand("spd_TaiChinh_ThueTNCN_TinhGiamThueTNCN", System.Data.CommandType.StoredProcedure, param);
            cmd.CommandTimeout = 180;

            DataProvider.ExecuteNonQuery((SqlConnection)((XPObjectSpace)obs).Session.Connection, cmd);
        }
    }

}
