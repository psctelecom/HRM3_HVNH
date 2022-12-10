using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using PSC_HRM.Module.PMS.Enum;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [DefaultClassOptions]
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo :Gửi phòng đào tạo đại học ( khảo thí )")]
    public class Report_PMS_GuiPhongDaoTaoDaiHoc_KhaoThi : StoreProcedureReport
    {

        private NamHoc _NamHoc;
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhan;
        private HocKy _HocKy;
        private LoaiGiangVienEnum _LoaiGiangVienEnum;


        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [RuleRequiredField("NamHocPhaiTonTai_KhaoThi", DefaultContexts.Save, "Năm học phải có")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                updateHocKyList(); 
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        [ImmediatePostData]
        public LoaiGiangVienEnum LoaiGiangVienEnum
        {
            get { return _LoaiGiangVienEnum; }
            set
            {
                SetPropertyValue("LoaiGiangVienEnum", ref _LoaiGiangVienEnum, value);
            }
        }



        public Report_PMS_GuiPhongDaoTaoDaiHoc_KhaoThi(Session session)
            : base(session)
        {
        }
        public override SqlCommand CreateCommand()
        {
            return null;
        }

        

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            LoaiGiangVienEnum = LoaiGiangVienEnum.CoHuu;
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_GuiPhongDaoTaoDaiHoc_KhaoThi", (SqlConnection)Session.Connection))
             {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@Truong", ThongTinTruong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@LoaiGiangVien", LoaiGiangVienEnum);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.Fill(DataSource);
            }
        }
        public void updateHocKyList()
        {

        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (NamHoc != null)
                updateHocKyList();
        }
    }
}