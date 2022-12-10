using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using System.Windows.Forms;
using DevExpress.ExpressApp;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    public class DanhSachThongTinThoiKhoaBieuGiangVien : BaseObject
    {
        private string _LopHocPhan;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _LoaiHocPhan;
        private int _TinChi;
        private int _SiSo;
        private string _MaGV;
        private string _TenGV;
        private string _LopSinhVien;
        private string _LichGiang;
        

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã học phần")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên học phần")]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Loại học phần")]
        public string LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp sinh viên")]
        public string LopSinhVien
        {
            get { return _LopSinhVien; }
            set { SetPropertyValue("LopSinhVien", ref _LopSinhVien, value); }
        }

        [ModelDefault("Caption", "Số tín chỉ")]
        public int TinChi
        {
            get { return _TinChi; }
            set { SetPropertyValue("TinChi", ref _TinChi, value); }
        }

        [ModelDefault("Caption", "Sỉ số")]
        public int SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }

        [ModelDefault("Caption", "Mã GV")]
        public string MaGV
        {
            get { return _MaGV; }
            set { SetPropertyValue("MaGV", ref _MaGV, value); }
        }

        [ModelDefault("Caption", "Tên GV")]
        public string TenGV
        {
            get { return _TenGV; }
            set { SetPropertyValue("TenGV", ref _TenGV, value); }
        }

        [ModelDefault("Caption", "Lịch giảng")]
        public string LichGiang
        {
            get { return _LichGiang; }
            set { SetPropertyValue("LichGiang", ref _LichGiang, value); }
        }

        [ModelDefault("Caption", "Danh sách")]
        public XPCollection<ChiTietDanhSachThongTinThoiKhoaBieuGiangVien> ListDanhSach { get; set; }

        public DanhSachThongTinThoiKhoaBieuGiangVien(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
        
        public void LoadData(string MaHP, Session ses)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@NamHoc", Guid.Empty);
            param[1] = new SqlParameter("@HocKy", Guid.Empty);
            param[2] = new SqlParameter("@DonVi", Guid.Empty);
            param[3] = new SqlParameter("@NhanVien", Guid.Empty);
            param[4] = new SqlParameter("@MaHocPhan", MaHP);

            SqlCommand cmd = DataProvider.GetCommand("PMS_XemLichGiangDayCuaGV", CommandType.StoredProcedure, param);
            DataSet dataset = DataProvider.GetDataSet(cmd);
            if (dataset != null)
            {
                DataTable dt = dataset.Tables[1];
                if (ListDanhSach != null)
                    ListDanhSach.Reload();
                else
                    ListDanhSach = new XPCollection<ChiTietDanhSachThongTinThoiKhoaBieuGiangVien>(ses, false);
                if (dt != null)
                {
                    foreach (DataRow itemRow in dt.Rows)
                    {
                        ChiTietDanhSachThongTinThoiKhoaBieuGiangVien ds = new ChiTietDanhSachThongTinThoiKhoaBieuGiangVien(ses);
                        ds.Tuan = Convert.ToInt32(itemRow["Tuan"].ToString());
                        ds.Thu = itemRow["Thu"].ToString();
                        ds.KhoanTiet = itemRow["KhoanTiet"].ToString();
                        ds.SoTiet = Convert.ToInt32(itemRow["SoTiet"].ToString());
                        ds.Phong = itemRow["Phong"].ToString();
                        ds.MaGV = itemRow["MaGV"].ToString();
                        ds.TenGV = itemRow["TenGV"].ToString();
                        ds.NgayDay = itemRow["NgayDay"].ToString();

                        ListDanhSach.Add(ds);

                    }
                }
            }
        }
    }

}