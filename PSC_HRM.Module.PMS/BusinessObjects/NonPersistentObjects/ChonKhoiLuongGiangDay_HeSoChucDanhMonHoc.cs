using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.NghiepVu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn danh sách nhân viên")]
    public class ChonKhoiLuongGiangDay_HeSoChucDanhMonHoc : BaseObject
    {
        private Guid _KhoiLuongGiangDay;
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy; 
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private HeSo_ChucDanhMonHoc _HeSoChucDanhMonHoc;
        
        [Browsable(false)]
        public Guid KhoiLuongGiangDay
        {
            get { return _KhoiLuongGiangDay; }
            set { SetPropertyValue("KhoiLuongGiangDay", ref _KhoiLuongGiangDay, value); }
        }
        [ModelDefault("Caption", "Thông tin trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }
        [ModelDefault("Caption", "Năm học")]
        [ModelDefault("AllowEdit", "False")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Học kỳ")]
        [ModelDefault("AllowEdit", "False")]
        public HocKy HocKy
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set
            {
                SetPropertyValue("Bophan", ref _BoPhan, value);
            }
        }
        [ModelDefault("Caption", "Nhân viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set 
            { 
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null)
                {
                    UpdatelistKhoiLuong();
                    listhso.Reload();
                    CriteriaOperator filter = CriteriaOperator.Parse("NhanVien = ?", value.Oid);
                    XPCollection<HeSo_ChucDanhMonHoc> list = new XPCollection<HeSo_ChucDanhMonHoc>(Session,filter);
                    foreach (HeSo_ChucDanhMonHoc item in list)
                    {
                        listhso.Add(item);
                    }
                }
            }
        }
        [ModelDefault("Caption", "Hệ số chức danh theo môn học")]
        [DataSourceProperty("listhso", DataSourcePropertyIsNullMode.SelectAll)]
        public HeSo_ChucDanhMonHoc HeSoChucDanhMonHoc
        {
            get { return _HeSoChucDanhMonHoc; }
            set { SetPropertyValue("HeSoChucDanhMonHoc", ref _HeSoChucDanhMonHoc, value); }
        }

        //

        [ModelDefault("Caption", "Danh sách khối lượng")]
        public XPCollection<dsChiTietKhoiLuong_NhanVien> listKhoiLuong { get; set; }
        [Browsable(false)]
        public XPCollection<HeSo_ChucDanhMonHoc> listhso { get; set; }
        
        public ChonKhoiLuongGiangDay_HeSoChucDanhMonHoc(Session session)
            : base(session)
        { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            listhso = new XPCollection<HeSo_ChucDanhMonHoc>(Session, false);
            listKhoiLuong = new XPCollection<dsChiTietKhoiLuong_NhanVien>(Session, false);
        }

        public void UpdatelistKhoiLuong()
        {
            if (NhanVien != null)
            {
                if (listKhoiLuong != null)
                {
                    listKhoiLuong.Reload();
                }
                else
                {
                    listKhoiLuong = new XPCollection<dsChiTietKhoiLuong_NhanVien>(Session, false);
                }
                SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
                param[0] = new SqlParameter("@KhoiLuongGiangDay", KhoiLuongGiangDay);
                param[1] = new SqlParameter("@NhanVien", NhanVien.Oid);
                DataTable dt = DataProvider.GetDataTable("spd_PMS_KhoiLuongGiangDay_HeSoChucDanhMonHoc", System.Data.CommandType.StoredProcedure, param);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dsChiTietKhoiLuong_NhanVien khoiluong = new dsChiTietKhoiLuong_NhanVien(Session);
                        khoiluong.OidKhoiLuongGiangDay = new Guid(item["OidKhoiLuongGiangDay"].ToString());
                        khoiluong.Chon = true;
                        khoiluong.BoPhan = item["TenBoPhan"].ToString();
                        khoiluong.NhanVien = item["HoTen"].ToString();
                        khoiluong.MaQuanLy = item["MaNV"].ToString();
                        if (item["TenBacDaoTao"].ToString() != string.Empty)
                            khoiluong.BacDaoTao = item["TenBacDaoTao"].ToString();
                        if (item["MaHocPhan"].ToString() != string.Empty)
                            khoiluong.MaHocPhan = item["MaHocPhan"].ToString();
                        if (item["TenMonHoc"].ToString() != string.Empty)
                            khoiluong.TenMonHoc = item["TenMonHoc"].ToString();
                        if (item["LopHocPhan"].ToString() != string.Empty)
                            khoiluong.LopHocPhan = item["LopHocPhan"].ToString();
                        khoiluong.HeSo_ChucDanhMonHoc = Convert.ToDecimal(item["HeSo_ChucDanh"].ToString());
                        listKhoiLuong.Add(khoiluong);
                    }
                }
            }
        }
    }
}
