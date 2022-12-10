using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn NV Cập Nhật")]
    public class QLyGC_Update_DM : BaseObject
    {
        private Guid _OidQuanLyGC;
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;

        //

        [Browsable(false)]
        public Guid OidQuanLyGC
        {
            get { return _OidQuanLyGC; }
            set { SetPropertyValue("OidQuanLyGC", ref _OidQuanLyGC, value); }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Số giờ định mức(NCKH)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCKH
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCKH", ref _SoGioDinhMuc_NCKH, value); }
        }

        [ModelDefault("Caption", "Số giờ định mức(khác)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Danh sách định mức chức vụ")]
        public XPCollection<dsDinhMucChucVu> listDMucChucVu
        {
            get;
            set;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            listDMucChucVu = new XPCollection<dsDinhMucChucVu>(Session, false);
            Load();
        }

        public QLyGC_Update_DM(Session session)
            : base(session)
        { }

        public void Load()
        {                 
            if (NamHoc != null)
            {
                listDMucChucVu.Reload();
                //Lấy danh sách số parameter để truyền dữ liệu 
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                DataTable dt = DataProvider.GetDataTable("spd_pms_dsDinhMucChucVu", System.Data.CommandType.StoredProcedure, param);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dsDinhMucChucVu ds = new dsDinhMucChucVu(Session);
                        if (item["Oid"].ToString() != string.Empty)
                            ds.OidDinhMucChucVu = new Guid(item["Oid"].ToString());// Dòng để lấy ra dược thay đổi
                        ds.OidQLyGioChuan = new Guid(item["OidQly"].ToString());
                        ds.ChucVu = item["TenChucVu"].ToString();
                        ds.DinhMuc = Convert.ToDecimal(item["DinhMuc"]);
                        ds.SoGioChuan = Convert.ToDecimal(item["SoGioChuan"]);
                        ds.SoGiangVienToiThieu = Convert.ToInt32(item["SoGVToiThieu"]);
                        ds.SoSVToiThieu = Convert.ToInt32(item["SoSVToiThieu"]);
                        ds.SoGioDinhMuc_NCKH = Convert.ToDecimal(item["SoGioChuan_NCKH"]);
                        ds.SoGioDinhMuc_Khac = Convert.ToDecimal(item["SoGioChuan_Khac"]);
                        listDMucChucVu.Add(ds);
                    }
                }
            }        
        }
    }
}
