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
    [ModelDefault("Caption", "Bảng lương tháng")]
    public class Luong_BangLuongThang : BaseObject, IThongTinTruong
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
                        DanhSachChiTietList = new XPCollection<Luong_BangLuongThangItem>(Session, false);
                        GetBangLuong(Session, "spd_Report_TaiChinh_BangLuongThang_NonPersistent", System.Data.CommandType.StoredProcedure, value.Oid);
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
        public XPCollection<Luong_BangLuongThangItem> ListDanhSachChiTiet
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
        public XPCollection<Luong_BangLuongThangItem> DanhSachChiTietList { get; set; }

        public Luong_BangLuongThang(Session session)
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
                    Luong_BangLuongThangItem luongItem = new Luong_BangLuongThangItem(session);
                    luongItem.BoPhan = item["TenBoPhan"].ToString();
                    luongItem.MaQuanLy = item["MaQuanLy"].ToString();
                    luongItem.HoTen = item["HoTen"].ToString();
                    luongItem.TenChucVu = item["TenChucDanh"].ToString();
                    luongItem.HeSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                    luongItem.VuotKhung = Convert.ToDecimal(item["VuotKhung"]);
                    luongItem.HSPCVuotKhung = Convert.ToDecimal(item["HSPCVuotKhung"]);
                    luongItem.HSPCChucVu = Convert.ToDecimal(item["HSPCChucVu"]);
                    luongItem.ThamNien = Convert.ToDecimal(item["ThamNien"]);
                    luongItem.HSPCDocHai = Convert.ToDecimal(item["HSPCDocHai"]);
                    luongItem.HSPCUuDai = Convert.ToDecimal(item["HSPCUuDai"]);
                    luongItem.PhuCapKhac = Convert.ToDecimal(item["HSPCKhac"]);
                    luongItem.TongHeSoLuong = Convert.ToDecimal(item["TongHeSoLuong"]);
                    luongItem.PhanTramHuongLuong = Convert.ToDecimal(item["PhanTramHuongLuong"]);
                    luongItem.TongLuongHeSo = Convert.ToDecimal(item["TongLuongHeSo"]);
                    luongItem.TienTruNghiKhongLuong = Convert.ToDecimal(item["TienTruNghiKhongLuong"]);
                    luongItem.BHTN = Convert.ToDecimal(item["BHTN"]);
                    luongItem.BHXH = Convert.ToDecimal(item["BHXH"]);
                    luongItem.BHYT = Convert.ToDecimal(item["BHYT"]);
                    luongItem.TongKhoanTru = Convert.ToInt32(item["TongKhoanTru"]);
                    luongItem.Luong2 = Convert.ToDecimal(item["Luong2"]);
                    luongItem.Luong3 = Convert.ToInt32(item["Luong3"]);
                    luongItem.HSPCTrachNhiem = Convert.ToDecimal(item["HSPCTrachNhiem"]);
                    luongItem.PhuCapQuanLy = Convert.ToDecimal(item["PhuCapQuanLy"]);
                    luongItem.PCDienThoai = Convert.ToDecimal(item["PCDienThoai"]);
                    luongItem.PCAnTrua = Convert.ToDecimal(item["PCAnTrua"]);
                    luongItem.PhuCapNganh = Convert.ToDecimal(item["PhuCapNganh"]);
                    luongItem.HSPCThamNienHC = Convert.ToDecimal(item["HSPCThamNienHC"]);
                    luongItem.ThucLanh = Convert.ToDecimal(item["ThucLanh"]);
                    luongItem.TruyLinh = Convert.ToDecimal(item["TruyLinh"]);
                    luongItem.TongKhoanCong = Convert.ToDecimal(item["TongKhoanCong"]);
                    luongItem.ThueTNCNTamTru = Convert.ToDecimal(item["ThueTNCNTamTru"]);
                    luongItem.ThueTNCNQuyetToan = Convert.ToDecimal(item["ThueTNCNQuyetToan"]);
                   
                    DanhSachChiTietList.Add(luongItem);
                }
            }
        }
    }

}
