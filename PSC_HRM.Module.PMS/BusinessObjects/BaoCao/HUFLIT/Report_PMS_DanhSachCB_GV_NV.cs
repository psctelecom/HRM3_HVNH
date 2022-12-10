using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
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
    [ModelDefault("Caption", "Báo cáo: Danh sách CB-GV-NV")]
    public class Report_PMS_DanhSachCB_GV_NV : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bộ phận")]
        [DataSourceProperty("listbp", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    NhanVien = null;
                    updateNV();
                }
            }
        }

        [ModelDefault("Caption", "Nhân Viên")]
        [ImmediatePostData]
        [DataSourceProperty("listnv")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (value != null)
                {
                    BoPhan = NhanVien.BoPhan;
                }
            }
        }
        [Browsable(false)]
        public XPCollection<BoPhan> listbp { get; set; }
      
        [Browsable(false)]
        public XPCollection<NhanVien> listnv { get; set; }

        void updateNV()
        {
            if (listnv == null)
            {
                listnv = new XPCollection<NhanVien>(Session);
            }
            if (BoPhan != null)
            {
                listnv.Criteria = CriteriaOperator.Parse("ThongTinTruong = ? and BoPhan=?", ThongTinTruong.Oid, BoPhan.Oid);
            }
            OnChanged("listnv");
        }

        //Thực hiện cập nhật bộ phận
        public void UpdateBoPhan()
        {
            if (listbp == null)
            {
                listbp = new XPCollection<BoPhan>(Session);
            }
            if (ThongTinTruong != null)
            {
                listbp.Criteria = CriteriaOperator.Parse("ThongTinTruong = ?", ThongTinTruong.Oid);
            }
        }

        public Report_PMS_DanhSachCB_GV_NV(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            UpdateBoPhan();
            updateNV();
            OnChanged("listbp");
        }
       
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_DanhSachCanBo_GV_NV", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }
    }
}
