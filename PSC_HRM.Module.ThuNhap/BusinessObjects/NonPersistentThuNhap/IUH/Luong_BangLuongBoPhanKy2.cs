using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using System.Data.SqlClient;
using DevExpress.ExpressApp;
using System.Data;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng lương theo bộ phận kỳ 2")]
    public class Luong_BangLuongBoPhanKy2 : BaseObject, IThongTinTruong
    {
        private KyTinhLuong _KyTinhLuong;
        private ThongTinTruong _ThongTinTruong;

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [VisibleInListView(false)]
        [DataSourceProperty("KyTinhLuongList")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        DanhSachBoPhanList = new XPCollection<Luong_BangLuongBoPhanKy2Item>(Session, false);
                        GetBangLuong(Session, "spd_TaiChinh_Luong_BangThanhToanLuongKy2TheoBoPhan", System.Data.CommandType.StoredProcedure, value.Oid);
                    }
                }
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thông tin trường")]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }
        
        [ModelDefault("Caption", "Danh sách bộ phận")]
        [DataSourceProperty("DanhSachBoPhanList")]
        public XPCollection<Luong_BangLuongBoPhanKy2Item> ListDanhSachLuong
        {
            get
            {
                return DanhSachBoPhanList;
            }
            set
            {
                value = DanhSachBoPhanList;
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }
        
        [Browsable(false)]
        public XPCollection<Luong_BangLuongBoPhanKy2Item> DanhSachBoPhanList { get; set; }

        public Luong_BangLuongBoPhanKy2(Session session)
            : base(session)
        { }

        private void UpdateKyTinhLuongList()
        {
            if (KyTinhLuongList == null)
                KyTinhLuongList = new XPCollection<KyTinhLuong>(Session);

            if (ThongTinTruong != null)
                KyTinhLuongList.Criteria = CriteriaOperator.Parse("ThongTinTruong=? and !KhoaSo", ThongTinTruong);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }

        private void GetBangLuong(Session session, string query, CommandType type, params object[] args)
        {
            DataTable dt = new DataTable();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@KyTinhLuong", args[0]);

            SqlCommand cmd = DataProvider.GetCommand(query, type, param);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    Luong_BangLuongBoPhanKy2Item luongBP = new Luong_BangLuongBoPhanKy2Item(session);
                    luongBP.BoPhan = item["TenBoPhan"].ToString();
                    luongBP.SoNguoi = Convert.ToInt32(item["SoNguoi"].ToString());
                    luongBP.SoTien = Convert.ToDecimal(item["SoTien"]);
                    luongBP.TruNDC = Convert.ToDecimal(item["TruNDC"]);
                    luongBP.TruCongDoan = Convert.ToDecimal(item["TruCongDoan"]);
                    luongBP.TruTamUng = Convert.ToDecimal(item["TruTamUng"]);
                    luongBP.TruKhac = Convert.ToDecimal(item["TruKhac"]);
                    luongBP.ThuNhapChiuThue = Convert.ToDecimal(item["ThuNhapChiuThue"]);
                    luongBP.ThueThuNhap = Convert.ToDecimal(item["ThueThuNhap"]);
                    luongBP.TruUngHo = Convert.ToDecimal(item["TruUngHo"]);
                    luongBP.ThucLinh = Convert.ToDecimal(item["ThucLinh"]);
                    DanhSachBoPhanList.Add(luongBP);
                }
            }
        }
    }

}
