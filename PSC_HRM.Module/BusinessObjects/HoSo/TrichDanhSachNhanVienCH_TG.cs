using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.Data;
using System.Data.SqlClient;

namespace PSC_HRM.Module.HoSo
{
    [NonPersistent]
    [ImageName("BO_Extract")]
    [ModelDefault("Caption", "Trích danh sách cán bộ CH-TG")]
    public class TrichDanhSachNhanVienCH_TG : BaseObject
    {
        private string _TimKiem;

        [ModelDefault("Caption", "Họ tên")]
        public string TimKiem
        {
            get { return _TimKiem; }
            set { SetPropertyValue("TimKiem", ref _TimKiem, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<ChiTietTrichDanhSachNhanVienCH_TG> ListChiTietTrichDanhSachNhanVien { get; set; }

        public TrichDanhSachNhanVienCH_TG(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListChiTietTrichDanhSachNhanVien = new XPCollection<ChiTietTrichDanhSachNhanVienCH_TG>(Session, false);
        }
        public void LoadChiTiet(Session session)
        {

            ListChiTietTrichDanhSachNhanVien.Reload();
            using (DialogUtil.Wait("Vui lòng chờ....", "Đang load dữ liệu!"))
            #region Load dữ liệu
            {
                DataTable dt = null;
                SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                param[0] = new SqlParameter("@TimKiem", TimKiem == string.Empty ? " " : TimKiem );
                SqlCommand cmd = DataProvider.GetCommand("spd_HRM_LayThongTinGiangVienCH_TG", System.Data.CommandType.StoredProcedure, param);
                DataSet dataset = DataProvider.GetDataSet(cmd);
                if (dataset != null)
                {
                    dt = dataset.Tables[0];
                    foreach (DataRow r in dt.Rows)
                    {
                        ChiTietTrichDanhSachNhanVienCH_TG chitiet = new ChiTietTrichDanhSachNhanVienCH_TG(Session);
                        chitiet.NhanVien = session.GetObjectByKey<NhanVien>(Guid.Parse(r["NhanVien"].ToString()));
                        ListChiTietTrichDanhSachNhanVien.Add(chitiet);
                    }
                }
            }
            #endregion

        }
    }

}
