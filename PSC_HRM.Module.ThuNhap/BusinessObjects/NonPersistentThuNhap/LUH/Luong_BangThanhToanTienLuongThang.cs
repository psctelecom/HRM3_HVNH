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


namespace PSC_HRM.Module.ThuNhap.Luong
{
    [NonPersistent]
    [ModelDefault("Caption", "Thanh toán tiền lương tháng")]
    public class Luong_BangThanhToanTienLuongThang : BaseObject, IThongTinTruong
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
                        DanhSachChiTietList = new XPCollection<Luong_BangThanhToanTienLuongThangItem>(Session, false);
                        GetBangLuong(Session, "sp_Luong_BangThanhToanTienLuongThang", System.Data.CommandType.StoredProcedure, value.Oid);
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
        
        [ModelDefault("Caption", "Chi tiết thanh toán")]
        [DataSourceProperty("DanhSachBoPhanList")]
        public XPCollection<Luong_BangThanhToanTienLuongThangItem> ListDanhSachChiTiet
        {
            get
            {
                return DanhSachChiTietList;
            }
            set
            {
                value = DanhSachChiTietList;
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }
        
        [Browsable(false)]
        public XPCollection<Luong_BangThanhToanTienLuongThangItem> DanhSachChiTietList { get; set; }

        public Luong_BangThanhToanTienLuongThang(Session session)
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
                    Luong_BangThanhToanTienLuongThangItem luongItem = new Luong_BangThanhToanTienLuongThangItem(session);
                    luongItem.BoPhan = item["TenBoPhan"].ToString();
                    luongItem.HoTen = item["HoTen"].ToString();
                    luongItem.TenChucVu = item["TenChucVu"].ToString();
                    luongItem.NgachLuong = item["NgachLuong"].ToString();
                    luongItem.HeSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                    luongItem.HSPCChucVu = Convert.ToDecimal(item["HSPCChucVu"]);
                    luongItem.PhuCapUuDai = Convert.ToDecimal(item["PhuCapUuDai"]);
                    luongItem.VuotKhung = Convert.ToDecimal(item["VuotKhung"]);
                    luongItem.HSPCDocHai = Convert.ToDecimal(item["HSPCDocHai"]);
                    luongItem.HSPCTrachNhiem = Convert.ToDecimal(item["HSPCTrachNhiem"]);
                    luongItem.ThamNien = Convert.ToDecimal(item["ThamNien"]);
                    luongItem.TongHSLuongNSNN = Convert.ToDecimal(item["TongHSLuongNSNN"]);
                    luongItem.TongLuongNSNN = Convert.ToDecimal(item["TongLuongNSNN"]);
                    luongItem.HSPCChuyenMon = Convert.ToDecimal(item["HSPCChuyenMon"]);
                    luongItem.HSPCQuanLy = Convert.ToDecimal(item["HSPCQuanLy"]);
                    luongItem.HSPCKiemNhiem1 = Convert.ToDecimal(item["HSPCKiemNhiem1"]);
                    luongItem.HSPCKiemNhiem2 = Convert.ToDecimal(item["HSPCKiemNhiem2"]);
                    luongItem.TongHSLuongTT = Convert.ToDecimal(item["TongHSLuongTT"]);
                    luongItem.Huong = Convert.ToInt32(item["Huong"]);
                    luongItem.TienLuongTangThem = Convert.ToDecimal(item["TienLuongTangThem"]);
                    luongItem.TienPCTangThem = Convert.ToDecimal(item["TienPCTangThem"]);
                    luongItem.TongLuongTT = Convert.ToDecimal(item["TongLuongTT"]);
                    luongItem.BHXH = Convert.ToDecimal(item["BHXH"]);
                    luongItem.BHTN = Convert.ToDecimal(item["BHTN"]);
                    luongItem.BHYT = Convert.ToDecimal(item["BHYT"]);
                    luongItem.LFCD = Convert.ToDecimal(item["LFCD"]);
                    luongItem.ThuNhapChiuThue = Convert.ToDecimal(item["ThuNhapChiuThue"]);
                    luongItem.ThueThuNhap = Convert.ToDecimal(item["ThueThuNhap"]);
                    luongItem.TongCacKhoanTru = Convert.ToDecimal(item["TongCacKhoanTru"]);
                    luongItem.ThucNhan = Convert.ToDecimal(item["ThucNhan"]);
                    if (item["LoaiLuongChinh"].ToString() == "0")
                        luongItem.LoaiLuongChinh = LoaiLuongChinhEnum.LuongChinhBienChe;
                    else if (item["LoaiLuongChinh"].ToString() == "1")
                        luongItem.LoaiLuongChinh = LoaiLuongChinhEnum.LuongChinhHopDongTrongChiTieuBienChe;
                    else if (item["LoaiLuongChinh"].ToString() == "2")
                        luongItem.LoaiLuongChinh = LoaiLuongChinhEnum.LuongChinhHopDong;
                    else if (item["LoaiLuongChinh"].ToString() == "3")
                        luongItem.LoaiLuongChinh = LoaiLuongChinhEnum.LuongChinhHopDongKhoanLuong;
                    DanhSachChiTietList.Add(luongItem);
                }
            }
        }
    }

}
