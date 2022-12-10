using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.NonPersistent;


namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn nhân viên")]
    [Appearance("Khoa", TargetItems = "BoPhan,listNhanVien", Enabled = false, Criteria = "TatCa")]
    
    public class ChonNhanVien : BaseObject
    {
        private bool _TatCa;
        private BoPhan _BoPhan;
        private bool _ThinhGiang;

        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        public bool TatCa
        {
            get { return _TatCa; }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                {
                    if (TatCa)
                        BoPhan = null;
                }
            }
        }

        [ModelDefault("Caption", "Thỉnh giảng")]
        [Browsable(false)]
        [ImmediatePostData]
        public bool ThinhGiang
        {
            get { return _ThinhGiang; }
            set
            {
                SetPropertyValue("ThinhGiang", ref _ThinhGiang, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    using (DialogUtil.AutoWait("Đang lấy danh sách nhân viên"))
                    //if (BoPhan != null)
                    {
                        listNhanVien.Reload();
                        ThongTinTruong ttt = HamDungChung.ThongTinTruong(Session);
                        SqlParameter[] param = new SqlParameter[3]; /*Số parameter trên Store Procedure*/
                        param[0] = new SqlParameter("@ThongTinTruong", ttt != null ? ttt.Oid : Guid.Empty);
                        param[1] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                        param[2] = new SqlParameter("@ThinhGiang", ThinhGiang.GetHashCode());
                        DataTable dt = DataProvider.GetDataTable("spd_pms_HoSo_LayThongTinHoSoNhanVien", System.Data.CommandType.StoredProcedure, param);
                        if (dt != null)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                dsThongTinNhanVien ttnv = new dsThongTinNhanVien(Session);
                                ttnv.OidThongTinNhanVien = new Guid(item["Oid"].ToString());
                                ttnv.MaQuanLy = item["MaQuanLy"].ToString();
                                ttnv.HoTen = item["HoTen"].ToString();
                                if (item["TenBoPhan"].ToString() != string.Empty)
                                    ttnv.BoPhan = item["TenBoPhan"].ToString();
                                ttnv.Chon = true;
                                listNhanVien.Add(ttnv);
                            }
                        }
                    }
                }
            }
        }
         [ModelDefault("Caption", "Danh sách nhân viên")]
        public XPCollection<dsThongTinNhanVien> listNhanVien
        {
            get;
            set;
        }
        public ChonNhanVien(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();            
            listNhanVien = new XPCollection<dsThongTinNhanVien>(Session, false);
            TatCa = true;
            ThinhGiang = false;
        }
    }

}
