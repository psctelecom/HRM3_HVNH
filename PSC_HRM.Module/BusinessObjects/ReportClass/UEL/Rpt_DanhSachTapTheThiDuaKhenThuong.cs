using System;

using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Reports;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo tập thể thi đua khen thưởng")]
    [Appearance("Rpt_DanhSachTapTheThiDuaKhenThuong.TatCaBoPhan", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCaBoPhan")]
    public class Rpt_DanhSachTapTheThiDuaKhenThuong : Module.Report.StoreProcedureReport, IBoPhan
    {
        public Rpt_DanhSachTapTheThiDuaKhenThuong(Session session) : base(session) { }
        private int _namBatDau = DateTime.Today.Year - 3;
        [ModelDefault("Caption", "Năm bắt đầu")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamBatDau
        {
            get
            {
                return _namBatDau;
            }
            set
            {
                SetPropertyValue("NamBatDau", ref _namBatDau, value);
            }
        }
        private int _namKetThuc = DateTime.Today.Year;
        [ModelDefault("Caption", "Năm kết thúc")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamKetThuc
        {
            get
            {
                return _namKetThuc;
            }
            set
            {
                SetPropertyValue("NamKetThuc", ref _namKetThuc, value);
            }
        }

        private bool _TatCaBoPhan = true;
        [ModelDefault("Caption", "Tất cả Đơn vị")]
        [ImmediatePostData()]
        public bool TatCaBoPhan
        {
            get
            {
                return _TatCaBoPhan;
            }
            set
            {
                SetPropertyValue("TatCaBoPhan", ref _TatCaBoPhan, value);
            }
        }

        private BoPhan _BoPhan;
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save, TargetCriteria = "!TatCaBoPhan")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }


        public override void FillDataSource()
        {
            List<string> lstBP;
            if (TatCaBoPhan)
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen();
            else
                lstBP = HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan);
            SqlDataAdapter da = new SqlDataAdapter("Spd_DanhSachThiDuaKhenThuong_TapThe", (SqlConnection)Session.Connection);
            da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            if(TatCaBoPhan)
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", DBNull.Value);
            else
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan.Oid);

            da.SelectCommand.Parameters.AddWithValue("@TuNam", _namBatDau);
            da.SelectCommand.Parameters.AddWithValue("@DenNam", _namKetThuc);
            //foreach (var item in lstBP)
            //{
            //    da.SelectCommand.Parameters["@BoPhan"].Value = item;
                da.Fill(DataSource);
            //}
        }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
    }

}
