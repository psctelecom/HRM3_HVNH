using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.DanhMuc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn chi tiết TKB")]
    public class QLyTKB_KhoaDuLieu : BaseObject
    {
        private Guid _OidQuanLyTKB;
        private ThongTinTruong _ThongTinTruong;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private bool _Khoa;
        private bool _MoKhoa;

        [Browsable(false)]
        public Guid OidQuanLyTKB
        {
            get { return _OidQuanLyTKB; }
            set { SetPropertyValue("OidQuanLyTKB", ref _OidQuanLyTKB, value); }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
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
        [ModelDefault("Caption", "Học kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public HocKy HocKy
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ImmediatePostData]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set 
            {
                SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); 
                if(!IsLoading && HeDaoTao != null)
                {
                    Load();
                }
            }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set 
            { 
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
                if (!IsLoading && BacDaoTao != null)
                {
                    Load();
                }
            }
        }

        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set 
            { 
                SetPropertyValue("Khoa", ref _Khoa, value);
                if (!IsLoading && value.GetHashCode() == 1)
                {
                    MoKhoa = false;
                }
            }
        }
        [ModelDefault("Caption", "Mở khóa")]
        [ImmediatePostData]
        public bool MoKhoa
        {
            get { return _MoKhoa; }
            set
            {
                SetPropertyValue("MoKhoa", ref _MoKhoa, value);
                if (!IsLoading && value.GetHashCode() == 1)
                {
                    Khoa = false;
                }
            }
        }

        [ModelDefault("Caption", "Danh sách Chi tiết Thời Khóa Biểu")]
        public XPCollection<dsChiTietTKB_KhoaDuLieu> listTKB
        {
            get;
            set;
        }
         public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            Load();
        }

        public QLyTKB_KhoaDuLieu(Session session)
            : base(session)
        { }
        public void Load()
        {
            if (NamHoc != null)
            {
                //listTKB.Reload();
                listTKB = new XPCollection<dsChiTietTKB_KhoaDuLieu>(Session, false);
                //Lấy danh sách số parameter để truyền dữ liệu 
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@ThoiKhoaBieu_KhoiLuongGiangDay", OidQuanLyTKB);
                param[1] = new SqlParameter("@BacDaoTao", BacDaoTao != null ? BacDaoTao.Oid : Guid.Empty);
                param[2] = new SqlParameter("@HeDaoTao", HeDaoTao != null ? HeDaoTao.Oid : Guid.Empty);

                DataTable dt = DataProvider.GetDataTable("spd_PMS_DSKhoa_DuLieu_ChiTietTKB", System.Data.CommandType.StoredProcedure, param);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dsChiTietTKB_KhoaDuLieu ds = new dsChiTietTKB_KhoaDuLieu(Session);
                        if (item["OidTKB_ChiTietKhoiLuongGiangDay"].ToString() != string.Empty)
                            // Dòng để lấy ra dược thay đổi
                            ds.OidTKB_ChiTietKhoiLuongGiangDay = new Guid(item["OidTKB_ChiTietKhoiLuongGiangDay"].ToString());
                        ds.OidTKB_KhoiLuongGiangDay = new Guid(item["OidTKB_KhoiLuongGiangDay"].ToString());
                        ds.NhanVien = item["NhanVien"].ToString();
                        ds.BoPhan = item["BoPhan"].ToString();
                        ds.TenMonHoc = item["TenMonHoc"].ToString();
                        ds.MaLopHocPhan = item["MaLopHocPhan"].ToString();
                        ds.LopHocPhan = item["LopHocPhan"].ToString();
                        ds.TenLopSV = item["TenLopSV"].ToString();
                        ds.KhoaDaoTao = item["KhoaDaoTao"].ToString();
                        ds.Chon = true;
                        listTKB.Add(ds);
                    }
                }
            }        
        }
    }
}
