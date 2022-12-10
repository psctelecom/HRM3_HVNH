using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
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
    [ModelDefault("Caption", "Báo cáo: Bảng thanh toán giờ giảng dạy thỉnh giảng(Đợt 2)")]
    public class Report_PMS_ThanhToanGioGiang_ThinhGiang_Dot2 : StoreProcedureReport
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;     

        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "false")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if(value != null)
                {
                    UpdateBangChot();
                }
            }
        }

        [ModelDefault("Caption", "Bảng chốt thù lao thỉnh giảng")]
        [DataSourceProperty("listbc", DataSourcePropertyIsNullMode.SelectAll)]
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get { return _BangChotThuLao_ThinhGiang; }
            set
            {
                SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value);
            }
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

        [Browsable(false)]
        public XPCollection<BangChotThuLao_ThinhGiang> listbc { get; set; }

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

        public void UpdateBangChot()
        {
            if (NamHoc != null)
            {
                if (listbc != null)
                {
                    listbc.Reload();
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
                    XPCollection<BangChotThuLao_ThinhGiang> ds_bc = new XPCollection<BangChotThuLao_ThinhGiang>(Session, filter);
                    foreach (BangChotThuLao_ThinhGiang item in ds_bc)
                    {
                        if (item.DotTinh.Dot == 2)
                            listbc.Add(item);
                    }
                }
            }
        }

        public Report_PMS_ThanhToanGioGiang_ThinhGiang_Dot2(Session session) : base(session) { }

        public override SqlCommand CreateCommand()
        {
            return null;
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            UpdateBoPhan();
            updateNV();
            listbc = new XPCollection<BangChotThuLao_ThinhGiang>(Session, false);
            UpdateBangChot();
         
        }
        public override void FillDataSource()
        {
            using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_Report_BangThanhToanGioGiang_ThinhGiang", (SqlConnection)Session.Connection))
            {
                da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@BangChotThuLao_ThinhGiang", BangChotThuLao_ThinhGiang.Oid);
                da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan == null ? Guid.Empty : BoPhan.Oid);
                da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien == null ? Guid.Empty : NhanVien.Oid);
                da.Fill(DataSource);
            }
        }     
    }
}
