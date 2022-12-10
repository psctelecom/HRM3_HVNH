using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.ThoiKhoaBieu;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.NEU
{
    [NonPersistent]
    [ModelDefault("Caption", "Phân công giảng dạy")]
    public class PhanCongGiangDay_ThoiKhoiBieu : BaseObject, IBoPhan
    {
        private BoPhan _BoPhan;
        private ThoiKhoaBieu_KhoiLuongGiangDay _ThoiKhoaBieu;

        [ModelDefault("Caption", "Khoa/Bộ môn")]
        [ImmediatePostData]
        [DataSourceProperty("ListBoPhan", DataSourcePropertyIsNullMode.SelectAll)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if(!IsLoading)
                    if(BoPhan!=null)
                    {
                        LoadDanhSach(BoPhan);
                    }
            }
        }
        [ModelDefault("Caption", "TKB")]
        [ModelDefault("AllowEdit", "false")]
        [ImmediatePostData]
        public ThoiKhoaBieu_KhoiLuongGiangDay ThoiKhoaBieu_KhoiLuongGiangDay
        {
            get { return _ThoiKhoaBieu; }
            set
            {
                SetPropertyValue("ThoiKhoaBieu", ref _ThoiKhoaBieu, value);
                if(!IsLoading)
                    if(ThoiKhoaBieu_KhoiLuongGiangDay!=null)
                    {
                        updateBoPhan();
                    }
            }
        }

        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietPhanCong> ListChiTiet
        {
            get;
            set;
        }
        [ModelDefault("Caption", "ListBoPhan")]
        [Browsable(false)]
        public XPCollection<BoPhan> ListBoPhan
        {
            get;
            set;
        }
        private void updateBoPhan()
        {
            ListBoPhan.Reload();
            BoPhan bPhan = null;
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ThoiKhoaBieu", ThoiKhoaBieu_KhoiLuongGiangDay.Oid);
            DataTable dt = DataProvider.GetDataTable("spd_PMS_ThoiKhoaBieu_KhoiLuongGiangDay_DanhSachKhoa", CommandType.StoredProcedure, param);
            if(dt!=null)
            {
                foreach(DataRow item in dt.Rows)
                {
                    bPhan = Session.FindObject<BoPhan>(CriteriaOperator.Parse("Oid =?", item["BoMonQuanLyGiangDay"].ToString()));
                    if (bPhan != null)
                        ListBoPhan.Add(bPhan);
                }
            }
            OnChanged("ListBoPhan");
        }
        public PhanCongGiangDay_ThoiKhoiBieu(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            ListBoPhan = new XPCollection<BaoMat.BoPhan>(Session, false);
            ListChiTiet = new XPCollection<ChiTietPhanCong>(Session, false);
        }
        void LoadDanhSach( BoPhan boPhan)
        {
            ListChiTiet.Reload();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ThoiKhoaBieu", ThoiKhoaBieu_KhoiLuongGiangDay.Oid);
            param[1] = new SqlParameter("@BoMonQuanLy", BoPhan.Oid);
            DataTable dt = DataProvider.GetDataTable("spd_PMS_ThoiKhoaBieu_DanhSachPhanCongGiangDay", CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ChiTietPhanCong ct =new ChiTietPhanCong(Session);
                    ct.OidChiTiet = new Guid(item["OidChiTiet"].ToString());
                    ct.BoMon = item["TenBoPhan"].ToString();
                    ct.TenMonHoc = item["TenMonHoc"].ToString();
                    ct.MaMonHoc = item["MaMonHoc"].ToString();
                    ct.LopHocPhan = item["LopHocPhan"].ToString();
                    ListChiTiet.Add(ct);
                }
            }
        }
    }
}