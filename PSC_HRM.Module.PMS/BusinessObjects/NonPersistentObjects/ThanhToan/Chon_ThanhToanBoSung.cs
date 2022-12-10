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

namespace PSC_HRM.Module.PMS.NonPersistentObjects.ThanhToan
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn")]
    public class Chon_ThanhToanBoSung : BaseObject
    {
        private NamHoc _NamHoc;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

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

        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    listChiTiet.Reload();
                    Loaddata();
                }
            }
        }
        [ModelDefault("Caption", "Cán bộ/Giảng viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading)
                {
                    listChiTiet.Reload();
                    Loaddata();
                }
            }
        }
        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<dsChiTietThanhToanThuLaoBoSung> listChiTiet
        {
            get;
            set;
        }
        public Chon_ThanhToanBoSung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            listChiTiet = new XPCollection<dsChiTietThanhToanThuLaoBoSung>(Session, false);
        }
        public void Loaddata()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách thông tin chốt dữ liệu đợt tổng kết"))
            {
                if (NamHoc != null && BoPhan != null)
                {
                    listChiTiet.Reload();
                    DataSet DataSource = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_LayThongTinBangChot_TongKet", (SqlConnection)Session.Connection))
                    {
                        da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                        da.SelectCommand.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                        da.SelectCommand.Parameters.AddWithValue("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                        da.Fill(DataSource);
                    }

                    foreach (DataRow item in DataSource.Tables[0].Rows)
                    {
                        dsChiTietThanhToanThuLaoBoSung ct = new dsChiTietThanhToanThuLaoBoSung(Session);
                        ct.Key = this;
                        ct.DonVi = item["TenBoPhan"].ToString();
                        ct.HoTen = item["HoTen"].ToString();
                        ct.HoatDong = item["LoaiHoatDong"].ToString();
                        ct.TenMonHoc = item["TenMonHoc"].ToString();
                        ct.LopHocPhan = item["LopHocPhan"].ToString();
                        ct.BacDaoTao = item["BacDaoTao"].ToString();
                        ct.OidChiTietBangChotThuLaoGiangDay = new Guid(item["OidChiTiet"].ToString());
                        listChiTiet.Add(ct);
                    }
                }
            }
        }
    }
}