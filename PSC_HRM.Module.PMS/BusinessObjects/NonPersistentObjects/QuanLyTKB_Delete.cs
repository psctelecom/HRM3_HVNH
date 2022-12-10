using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.Enum;
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
    public class QuanLyTKB_Delete : BaseObject
    {
        private Guid _OidQuanLyTKB;
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private LoaiXoaEnum? _LoaiXoa;

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

        [ModelDefault("Caption", "Loại")]
        [ImmediatePostData]
        public LoaiXoaEnum? LoaiXoa
        {
            get { return _LoaiXoa; }
            set 
            { 
                SetPropertyValue("LoaiXoa", ref _LoaiXoa, value);
                if (!IsLoading)
                    Load();
            }
        }

        [ModelDefault("Caption", "Danh sách Chi tiết Thời Khóa Biểu")]
        public XPCollection<dsChiTietTKB> listTKB
        {
            get;
            set;
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            LoaiXoa = LoaiXoaEnum.TatCa;
            listTKB = new XPCollection<dsChiTietTKB>(Session, false);
            Load();
        }

        public QuanLyTKB_Delete(Session session)
            : base(session)
        { }
        public void Load()
        {
            if (NamHoc != null)
            {
                listTKB.Reload();
                //Lấy danh sách số parameter để truyền dữ liệu 
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ThoiKhoaBieu_KhoiLuongGiangDay", OidQuanLyTKB);
                param[1] = new SqlParameter("@NamHoc", NamHoc.Oid);
                param[2] = new SqlParameter("@HocKy", HocKy.Oid);
                param[3] = new SqlParameter("@KQ", LoaiXoa);

                DataTable dt = DataProvider.GetDataTable("spd_PMS_DSChiTietTKB", System.Data.CommandType.StoredProcedure, param);
                if (dt != null)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        dsChiTietTKB ds = new dsChiTietTKB(Session);
                        if (item["OidTKB_ChiTietKhoiLuongGiangDay"].ToString() != string.Empty)
                            // Dòng để lấy ra dược thay đổi
                            ds.OidTKB_ChiTietKhoiLuongGiangDay = new Guid(item["OidTKB_ChiTietKhoiLuongGiangDay"].ToString());
                        ds.OidTKB_KhoiLuongGiangDay = new Guid(item["OidTKB_KhoiLuongGiangDay"].ToString());
                        ds.NhanVien = item["NhanVien"].ToString();
                        ds.BoPhan = item["BoPhan"].ToString();
                        ds.TenMonHoc = item["TenMonHoc"].ToString();
                        ds.LopHocPhan = item["LopHocPhan"].ToString();
                        ds.BoMonGiangDay = item["BoMonQuanLyGiangDay"].ToString();
                        ds.KhoaDaoTao = item["KhoaDaoTao"].ToString();
                        ds.SoTietHeThong = Convert.ToInt32(item["SoTietHeThong"]);
                        ds.SoTietThucDay = Convert.ToDecimal(item["SoTietThucDay"]);
                        ds.SoTietDungLop = Convert.ToDecimal(item["SoTietDungLop"]);
                        ds.SoTietQuyDoi = Convert.ToDecimal(item["SoTietQuyDoi"]);
                        ds.HeSoChucDanh = Convert.ToDecimal(item["HeSoChucDanh"]);
                        ds.HeSoLopDong = Convert.ToDecimal(item["HeSoLopDong"]);
                        ds.HeSoDaoTao = Convert.ToDecimal(item["HeSoDaoTao"]);
                        ds.HeSoCoSo = Convert.ToDecimal(item["HeSoCoSo"]);
                        ds.HeSoNgoaiGio = Convert.ToDecimal(item["HeSoNgoaiGio"]);
                        ds.HeSoTinChi = Convert.ToDecimal(item["HeSoTinChi"]);
                        ds.HeSoChucVu = Convert.ToDecimal(item["HeSoChucVu"]);
                        ds.HeSoTuXa = Convert.ToDecimal(item["HeSoTuXa"]);

                        listTKB.Add(ds);
                    }
                }
            }        
        }
    }
}
