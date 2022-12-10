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
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.Report;
using System.Data.SqlClient;

namespace PSC_HRM.Module.PMS.BaoCao
{
    [DefaultClassOptions]
    [NonPersistent]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo :Bảng tính thù lao vượt chuẩn của giảng viên ( khảo thí ) tính cho Thỉnh Giảng,Văn Phòng (VD)")]
    public class Report_PMS_BangTinhThuLaoVuotGioChuanCuaGiangVien_KhaoThi_ThinhGiangVaVanPhong_VD : StoreProcedureReport
    {
        public Report_PMS_BangTinhThuLaoVuotGioChuanCuaGiangVien_KhaoThi_ThinhGiangVaVanPhong_VD(Session session)
            : base(session)
        {
        }
        private NamHoc _NamHoc;
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhan;
        private HocKy _HocKy;


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
        [RuleRequiredField("NamHocPhaiTonTai_KhaoThi_TGVP_VD", DefaultContexts.Save, "Năm học phải có")]
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
        [DataSourceProperty("HocKyList")]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        [Browsable(false)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }






        public override SqlCommand CreateCommand()
        {
            return null;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        [ImmediatePostData]
        public XPCollection<HocKy> HocKyList
        {
            get;
            set;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }

        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_Report_PMS_BangTinhThuLaoVuotGioChuanCuaGiangVien_KhaoThi_ThinhGiangVaVanPhong_VD", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                da.SelectCommand.Parameters.AddWithValue("@Truong", ThongTinTruong.Oid);
                da.SelectCommand.Parameters.AddWithValue("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                da.Fill(DataSource);
            }
        }
        public void updateHocKyList()
        {
            if (HocKyList != null)
                HocKyList.Reload();
            else
                HocKyList = new XPCollection<HocKy>(Session);

            HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            SortingCollection sortHK = new SortingCollection();
            sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            HocKyList.Sorting = sortHK;
            OnChanged("HocKyList");
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (NamHoc != null)
                updateHocKyList();
        }
    }
}