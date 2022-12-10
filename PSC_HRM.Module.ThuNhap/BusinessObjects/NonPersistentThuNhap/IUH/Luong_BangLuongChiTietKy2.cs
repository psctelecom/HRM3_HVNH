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
    [ModelDefault("Caption", "Bảng lương chi tiết kỳ 2")]
    public class Luong_BangLuongChiTietKy2 : BaseObject, IThongTinTruong
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
                        DanhSachCanBoList = new XPCollection<Luong_BangLuongChiTietKy2Item>(Session, false);
                        GetBangLuong(Session, "spd_TaiChinh_Luong_BangThanhToanLuongKy2", System.Data.CommandType.StoredProcedure, value.Oid);
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
        
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [DataSourceProperty("DanhSachCanBoList")]
        public XPCollection<Luong_BangLuongChiTietKy2Item> ListDanhSachLuong
        {
            get
            {
                return DanhSachCanBoList;
            }
            set
            {
                value = DanhSachCanBoList;
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }
        
        [Browsable(false)]
        public XPCollection<Luong_BangLuongChiTietKy2Item> DanhSachCanBoList { get; set; }

        public Luong_BangLuongChiTietKy2(Session session)
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
                    Luong_BangLuongChiTietKy2Item luongNV = new Luong_BangLuongChiTietKy2Item(session);
                
                    luongNV.MaSo = item["MaSo"].ToString();
                    luongNV.HoTen = item["Ho"].ToString() + " " + item["Ten"].ToString();
                    luongNV.BoPhan = item["TenBoPhan"].ToString();
                    luongNV.HeSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                    luongNV.LuongKy2 = Convert.ToDecimal(item["LuongKy2"]);
                    luongNV.DanhGia = item["DanhGia"].ToString();
                    luongNV.SoNgayCong = Convert.ToDecimal(item["SoNgayCong"]);
                    luongNV.ThanhTien = Convert.ToDecimal(item["ThanhTien"]);
                    luongNV.TienNDC = Convert.ToDecimal(item["TienNDC"]);
                    luongNV.TienCongDoan = Convert.ToDecimal(item["TienCongDoan"]);
                    luongNV.TamUng = Convert.ToDecimal(item["TamUng"]);
                    luongNV.KhauTruKhac = Convert.ToDecimal(item["KhauTruKhac"]);
                    luongNV.ThuNhapTruocThue = Convert.ToDecimal(item["ThuNhapTruocThue"]);
                    luongNV.ThuNhapChiuThue = Convert.ToDecimal(item["ThuNhapChiuThue"]);
                    luongNV.ThueThuNhap = Convert.ToDecimal(item["ThueThuNhap"]);
                    luongNV.KhauTruUngHo = Convert.ToDecimal(item["KhauTruUngHo"]);
                    luongNV.ThucLinh = Convert.ToDecimal(item["ThucLinh"]);
                    DanhSachCanBoList.Add(luongNV);
                }
            }
        }
    }

}
