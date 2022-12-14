using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NonPersistent;
using PSC_HRM.Module.BusinessObjects.HoSo;
using DevExpress.Persistent.Base;
using System.Data;
using System.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Nâng hạng thâm niên trình độ")]
    public class TrinhDoChuyenMon_NangThoiHanApDung : BaseObject
    {
        // Fields...
        private BoPhan _BoPhan;
        private DateTime _NgayHieuLuc;
        private ThamNien _ThamNien;



        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                    if (BoPhan != null)
                    {
                        LoadDanhSachNhanVien();
                    }
                    else
                    {
                        listNhanVien.Reload();
                        listChiTiet.Reload();
                    }
            }
        }
        [ModelDefault("Caption", "Ngày hiệu lực")]
        public DateTime NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
            }
        }
        [ModelDefault("Caption", "Thâm niên")]
        public ThamNien ThamNien
        {
            get
            {
                return _ThamNien;
            }
            set
            {
                SetPropertyValue("ThamNien", ref _ThamNien, value);
            }
        }
        [ModelDefault("Caption", "Danh sách nhân viên")]
        public XPCollection<dsThongTinNhanVien> listNhanVien
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<TrinhDoChuyenMon_NangThamNien> listChiTiet
        {
            get;
            set;
        }


        public TrinhDoChuyenMon_NangThoiHanApDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listNhanVien = new XPCollection<dsThongTinNhanVien>(Session, false);
            listChiTiet = new XPCollection<TrinhDoChuyenMon_NangThamNien>(Session, false);
            NgayHieuLuc = DateTime.Now;
        }
        public void LoadDanhSachNhanVien()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách cán bộ/nhân viên"))
            {
                listNhanVien.Reload();
                SqlCommand cmd = new SqlCommand("spd_NhanSu_LayDanhSachNhanVien", DataProvider.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1800;
                cmd.Parameters.AddWithValue("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adt.Fill(dt);

                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dsThongTinNhanVien ct = new dsThongTinNhanVien(Session);
                        ct.BoPhan = item["TenBoPhan"].ToString();
                        ct.HoTen = item["HoTen"].ToString();
                        ct.MaQuanLy = item["MaQuanLy"].ToString();
                        ct.OidThongTinNhanVien = new Guid(item["NhanVien"].ToString());

                        listNhanVien.Add(ct);
                    }
                }
            }
        }
        public void KiemTra()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách thâm niên"))
            {
                listChiTiet.Reload();
                string sql = "";
                List<dsThongTinNhanVien> ds = (from d in listNhanVien
                                               where d.Chon == true
                                               select d).ToList();
                foreach (var item in ds)
                {
                    sql += " union all select '" + item.OidThongTinNhanVien + "' as NhanVien";
                }
                if (sql == "")
                {
                    XtraMessageBox.Show("Không có cán bộ/nhân viên được chọn", "Thông báo");
                    return;
                }
                SqlCommand cmd = new SqlCommand("spd_NhanSu_LayDanhSachNangThamNien", DataProvider.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1800;
                cmd.Parameters.AddWithValue("@String", sql.Substring(11));

                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                int Sodong = 0;
                foreach (DataRow item in dt.Rows)
                {
                    Sodong++;
                    TrinhDoChuyenMon_NangThamNien ct = new TrinhDoChuyenMon_NangThamNien(Session);
                    ct.NhanVien = new Guid(item["NhanVien"].ToString());
                    ct.VanBang = new Guid(item["VanBang"].ToString());
                    ct.HoTen = item["HoTen"].ToString();
                    ct.MaGiangVien = item["MaGiangVien"].ToString();
                    ct.DonVi = item["DonVi"].ToString();
                    ct.TenTrinhDo = item["TenTrinhDoChuyenMon"].ToString();
                    try
                    {
                        ct.NgayHieuLuc = Convert.ToDateTime(item["NgayHieuLuc"].ToString());
                    }
                    catch (Exception) { }

                    try
                    {
                        ct.NgayThucHien = Convert.ToDateTime(item["NgayThucHien"].ToString());
                    }
                    catch (Exception) { }
                    try
                    {
                        ct.NgayDuKienTangThamNien = Convert.ToDateTime(item["NgayDuKienTangThamNien"].ToString());
                    }
                    catch (Exception) { }
                    ct.ThamNien = Session.FindObject<ThamNien>(CriteriaOperator.Parse("Oid =?", item["ThamNien"].ToString()));

                    if (Convert.ToInt32(item["Chon"].ToString()) == 1)
                        ct.Chon = true;
                    else
                        ct.Chon = false;
                    listChiTiet.Add(ct);
                }
                if (Sodong == 0)
                    XtraMessageBox.Show("Không tìm thấy thông tin nâng thâm niên giảng viên", "Thông báo");
            }
        }
        public void CapNhat()
        {
            using (DialogUtil.AutoWait("Đang cập nhật dữ liệu"))
            {
                if (ThamNien == null)
                {
                    XtraMessageBox.Show("Vui lòng chọn thâm niên cần cập nhật", "Thông báo");
                    return;
                }
                List<TrinhDoChuyenMon_NangThamNien> ds = (from d in listChiTiet
                                                          where d.Chon == true
                                                          select d).ToList();
                foreach (var item in ds)
                {
                    SqlCommand cmd = new SqlCommand("spd_VanBang_CapNhatThamNien", DataProvider.GetConnection());
                    cmd.CommandTimeout = 1800;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@VanBang", item.VanBang.ToString());
                    cmd.Parameters.AddWithValue("@ThamNien", ThamNien.Oid);
                    cmd.Parameters.AddWithValue("@NgayHieuLuc", NgayHieuLuc.Date);
                    cmd.Parameters.AddWithValue("@User", HamDungChung.CurrentUser().UserName.ToString());
                    cmd.ExecuteNonQuery();
                }
                XtraMessageBox.Show("Cập nhật văn bằng thành công", "Thông báo");

                KiemTra();
            }
        }
    }
}