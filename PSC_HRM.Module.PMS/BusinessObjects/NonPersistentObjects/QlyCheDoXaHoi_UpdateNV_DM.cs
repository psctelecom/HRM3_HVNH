using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Cập nhật chế độ xã hội")]
    //[Appearance("Hide_TinhThang", TargetItems = "TuNgay;DenNgay",
    //                                Visibility = ViewItemVisibility.Hide,
    //                                Criteria = "CheDoXaHoi.TinhTheoThang = 0")]
    //[Appearance("!Hide_TinhThang", TargetItems = "Age",
    //                                Visibility = ViewItemVisibility.Hide,
    //                                Criteria = "CheDoXaHoi.TinhTheoThang = 1")]

    [Appearance("Kho_Age", TargetItems = "Age", Enabled = false, Criteria = "CheDoXaHoi.TinhTheoThang = 1")]
    [Appearance("Khoa", TargetItems = "TuNgay;DenNgay", Enabled = false, Criteria = "CheDoXaHoi.TinhTheoThang = 0")]
    public class QlyCheDoXaHoi_UpdateNV_DM : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private BoPhan _BoPhan;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        private CheDoXaHoi _CheDoXaHoi;

        private int _Age;
        private DateTime _TuNgay;
        private DateTime _DenNgay;


        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { _BoPhan = value; }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                {
                    updateHocKyList();
                }
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { _HocKy = value; }
        }

        [ModelDefault("Caption", "Chế độ")]
        [ImmediatePostData]
        public CheDoXaHoi CheDoXaHoi
        {
            get { return _CheDoXaHoi; }
            set { _CheDoXaHoi = value; }
        }
        [ModelDefault("Caption", "Độ tuổi")]
        [ModelDefault("DisplayFormat", "N")]
        [ModelDefault("EditMask", "N")]
        public int Age
        {
            get { return _Age; }
            set
            {
                SetPropertyValue("Age", ref _Age, value);
                if (!IsLoading)
                {
                    Load();
                }
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get { return _TuNgay; }
            set { SetPropertyValue("TuNgay", ref _TuNgay, value); }
        }

        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get { return _DenNgay; }
            set { SetPropertyValue("DenNgay", ref _DenNgay, value); }
        }

        [ModelDefault("Caption", "Danh sách Nhân Viên ")]
        public XPCollection<dsDMucCheDoXaHoi> listNV
        {
            get;
            set;
        }
        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }
        public void updateHocKyList()
        {
            if (NamHoc != null)
            {
                HocKyList = new XPCollection<HocKy>(Session);
                HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
                SortingCollection sortHK = new SortingCollection();
                sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
                HocKyList.Sorting = sortHK;
                OnChanged("HocKyList");
                HocKy = Session.FindObject<HocKy>(CriteriaOperator.Parse("NamHoc =? and MaQuanLy =?", NamHoc.Oid, "HK01"));
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            listNV = new XPCollection<dsDMucCheDoXaHoi>(Session, false);
            //Load();
        }
        public QlyCheDoXaHoi_UpdateNV_DM(Session session)
            : base(session)
        { }
        public void Load()
        {
            using (DialogUtil.AutoWait("Đang xử lý dữ liệu....."))
            {
                if (NamHoc != null)
                {
                    listNV.Reload();
                    //Lấy danh sách số parameter để truyền dữ liệu 
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@Age", Age);
                    param[1] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                    param[2] = new SqlParameter("@CheDo", CheDoXaHoi != null ? CheDoXaHoi.Oid : Guid.Empty);
                    DataTable dt = DataProvider.GetDataTable("spd_PMS_CheDoXaHoi_DSTuoiNV", System.Data.CommandType.StoredProcedure, param);
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            dsDMucCheDoXaHoi ds = new dsDMucCheDoXaHoi(Session);

                            ds.OidNV = new Guid(item["Oid"].ToString());
                            ds.MaQuanLy = item["MaGiangVien"].ToString();
                            ds.HoTen = item["HoTen"].ToString();
                            try
                            {
                                ds.TenBoPhan = item["TenBoPhan"].ToString();
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                ds.NgaySinh = Convert.ToDateTime(item["NgaySinh"]);
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                ds.TuoiTheoThang = Convert.ToInt32(item["TuoiTheoNam"]);
                            }
                            catch (Exception)
                            {
                            }
                            try
                            {
                                ds.TenTinhTrang = item["TenTinhTrang"].ToString();
                            }
                            catch (Exception)
                            {

                            }
                            listNV.Add(ds);
                        }
                    }
                }
            }
        }
    }
}
