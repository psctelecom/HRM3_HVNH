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
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NonPersistentObjects.TaiChinh
{
    [NonPersistent]
    [ModelDefault("Caption", "Phụ lục 01")]
    public class Pivot_PhuLuc01 : BaseObject
    {
        private NamHoc _NamHoc;
        private QuanlyTienCongKhaoThi _QuanLy;

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
                    QuanLy = null;
                    listChiTiet.Reload();
                    listQuanLy.Reload();
                    Loaddata_QuanLy();
                }
            }
        }

        [ModelDefault("Caption", "Quản lý")]
        [DataSourceProperty("listQuanLy")]
        [ImmediatePostData]
        public QuanlyTienCongKhaoThi QuanLy
        {
            get { return _QuanLy; }
            set
            {
                SetPropertyValue("QuanLy", ref _QuanLy, value);
                if (!IsLoading)
                {
                    listChiTiet.Reload();
                    Loaddata();
                }
            }
        }

        [ModelDefault("Caption", "Danh sách")]
        [NonPersistent]
        [Browsable(false)]
        public XPCollection<QuanlyTienCongKhaoThi> listQuanLy
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<Pivot_ChiTietPhuLuc01> listChiTiet
        {
            get;
            set;
        }
        public Pivot_PhuLuc01(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listChiTiet = new XPCollection<Pivot_ChiTietPhuLuc01>(Session, false);
            listQuanLy = new XPCollection<QuanlyTienCongKhaoThi>(Session, false);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        public void Loaddata_QuanLy()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách thông tin"))
            {
                if (NamHoc != null)
                {
                    listChiTiet.Reload();
                    DataSet DataSource = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_LayQuanLyTienCongKhaoThi_TaiChinhDHDN", (SqlConnection)Session.Connection))
                    {
                        da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@NamHoc", NamHoc.Oid);
                        da.Fill(DataSource);
                    }

                    foreach (DataRow item in DataSource.Tables[0].Rows)
                    {
                        QuanlyTienCongKhaoThi ct = Session.FindObject<QuanlyTienCongKhaoThi>(CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
                        if(ct != null)
                        {
                            listQuanLy.Add(ct);
                        }
                    }
                }
            }
        }

        public void Loaddata()
        {
            using (DialogUtil.AutoWait("Đang lấy dữ liệu"))
            {
                if (QuanLy != null)
                {
                    listChiTiet.Reload();
                    DataSet DataSource = new DataSet();
                    using (SqlDataAdapter da = new SqlDataAdapter("spd_PMS_LayChiTietTienCongKhaoThi_TaiChinhDHDN", (SqlConnection)Session.Connection))
                    {
                        da.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        da.SelectCommand.Parameters.AddWithValue("@QuanlyTienCongKhaoThi", QuanLy.Oid);
                        da.Fill(DataSource);
                    }

                    foreach (DataRow item in DataSource.Tables[0].Rows)
                    {
                        Pivot_ChiTietPhuLuc01 ct = new Pivot_ChiTietPhuLuc01(Session);
                        ct.MaGV = item["MaGV"].ToString();
                        ct.HoTen = item["HoTen"].ToString();
                        ct.SoTKNH = item["SoTKNH"].ToString();
                        ct.TenNganHang = item["TenNganHang"].ToString();
                        ct.TieuDe = item["TieuDe"].ToString();
                        ct.ThanhTien = Convert.ToDecimal(item["ThanhTien"].ToString());
                        ct.DonGia = Convert.ToDecimal(item["DonGia"].ToString());
                        ct.SoLuong = Convert.ToDecimal(item["SoLuong"].ToString());
                        listChiTiet.Add(ct);
                    }                   
                }
                
            }
        }
    }
}