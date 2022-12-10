using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;
using PSC_HRM.Module.KhenThuong;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.ReportClass
{
    [NonPersistent]
    [ModelDefault("Caption","Danh sách Chiến sĩ thi đua cơ sở")]
    [Appearance("RptDanhSachChienSiThiDuaCoSo.DanhHieu", TargetItems =  "DanhHieu", Enabled = false)]
    public class RptClassDanhDachChienSiThiDuaCoSo : StoreProcedureReport
    {
        public RptClassDanhDachChienSiThiDuaCoSo(Session session) : base(session) { }

        private int _NamKhenThuong;
        [ModelDefault("Caption","Năm khen thưởng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamKhenThuong
        {
            get
            {
                return _NamKhenThuong;
            }
            set
            {
                SetPropertyValue("NamKhenThuong", ref _NamKhenThuong, value);
            }
        }
        private DanhHieuKhenThuong _DanhHieu;
        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        public DanhHieuKhenThuong DanhHieu
        {
            get
            {
                return _DanhHieu;
            }
            set
            {
                SetPropertyValue("DanhHieu", ref _DanhHieu, value);
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            DanhHieu = Session.FindObject<DanhHieuKhenThuong>(CriteriaOperator.Parse("TenDanhHieu like '%Chiến sĩ thi đua cơ sở%'"));
        }
        public override SqlCommand CreateCommand()
        {
            SqlCommand cm = new SqlCommand("spd_DanhSachKhenThuongNam");
            cm.CommandType = System.Data.CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@NamKhenThuong", NamKhenThuong);
            cm.Parameters.AddWithValue("@DanhHieuOid", DanhHieu.Oid);

            return cm;
        }
    }

}
