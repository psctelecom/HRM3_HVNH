using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.Enum;
using System.ComponentModel;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using System.Data;
using System.Data.SqlClient;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn dữ liệu")]
    public class ThanhToanPhuLuc01 : BaseObject
    {
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Năm học")]
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
                    listChiTiet.Reload();
                    Loaddata();
                }
            }
        }

        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<dsThanhToanPhuLuc01> listChiTiet
        {
            get;
            set;
        }
        public ThanhToanPhuLuc01(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            listChiTiet = new XPCollection<dsThanhToanPhuLuc01>(Session, false);
        }
        public void Loaddata()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách thông tin chốt dữ liệu đợt tổng kết"))
            {
                if (NamHoc != null)
                {
                    listChiTiet.Reload();
                    DataSet DataSource = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_LayThongTinKhaoThi_TaiChinhDHDN", (SqlConnection)Session.Connection))
                    {
                        da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                        da.Fill(DataSource);
                    }

                    foreach (DataRow item in DataSource.Tables[0].Rows)
                    {
                        dsThanhToanPhuLuc01 ct = new dsThanhToanPhuLuc01(Session);
                        ct.QuanLyKhaoThi = new Guid(item["QuanLyKhaoThi"].ToString());
                        ct.DienGiai = item["DienGiai"].ToString();
                        listChiTiet.Add(ct);
                    }
                }
            }
        }
    }
}