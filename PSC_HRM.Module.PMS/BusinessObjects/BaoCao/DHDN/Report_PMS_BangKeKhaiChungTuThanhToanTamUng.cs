using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo: Bảng kê khai chứng từ thanh toán/tạm ứng")]
    public class Report_PMS_BangKeKhaiChungTuThanhToanTamUng : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private QuanlyTienCongKhaoThi _QuanlyTienCongKhaoThi;


        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        //[DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                    update();
            }
        }

        [ModelDefault("Caption", "Quản lý")]
        [DataSourceProperty("listquanly")]
        public QuanlyTienCongKhaoThi QuanlyTienCongKhaoThi
        {
            get
            {
                return _QuanlyTienCongKhaoThi;
            }
            set
            {
                SetPropertyValue("QuanlyTienCongKhaoThi", ref _QuanlyTienCongKhaoThi, value);
            }
        }
        [Browsable(false)]
        public XPCollection<QuanlyTienCongKhaoThi> listquanly { get; set; }

        public Report_PMS_BangKeKhaiChungTuThanhToanTamUng(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {            
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listquanly = new XPCollection<QuanlyTienCongKhaoThi>(Session, false);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
        void update()
        {
            if (listquanly.Count != 0)
                listquanly.Reload();
            else
            {
                CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
                XPCollection<QuanlyTienCongKhaoThi> ds = new XPCollection<QuanlyTienCongKhaoThi>(Session, filter);
                foreach(QuanlyTienCongKhaoThi item in ds)
                {
                    listquanly.Add(item);
                }
            }
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_BangKeKhaiChungTuThanhToanTamUng", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@QuanLyTienCong", QuanlyTienCongKhaoThi.Oid);  
                da.Fill(DataSource);
            }
        }
    }
}
