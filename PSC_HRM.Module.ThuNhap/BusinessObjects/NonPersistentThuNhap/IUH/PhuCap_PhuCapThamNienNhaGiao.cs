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
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;


namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng chi phụ cấp thâm niên nhà giáo")]
    public class PhuCap_PhuCapThamNienNhaGiao : BaseObject, IThongTinTruong
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
                        DanhSachCanBoList = new XPCollection<PhuCap_PhuCapThamNienNhaGiaoItem>(Session, false);
                        GetDanhSachPhuCapTrachNhiem(Session, "spd_TaiChinh_PhuCapThamNienNhaGiao", System.Data.CommandType.StoredProcedure, value.Oid);
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
        public XPCollection<PhuCap_PhuCapThamNienNhaGiaoItem> ListPhuCapThamNienNhaGiao
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
        public XPCollection<PhuCap_PhuCapThamNienNhaGiaoItem> DanhSachCanBoList { get; set; }

        public PhuCap_PhuCapThamNienNhaGiao(Session session)
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

        private void GetDanhSachPhuCapTrachNhiem(Session session, string query, CommandType type, params object[] args)
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
                    PhuCap_PhuCapThamNienNhaGiaoItem phuCap = new PhuCap_PhuCapThamNienNhaGiaoItem(session);
                    phuCap.HoTen = item["HoTen"].ToString();
                    phuCap.NgaySinh = Convert.ToDateTime(item["NgaySinh"]);
                    phuCap.ThangNamThamNien = item["ThangNamTNCongTac"].ToString();
                    phuCap.TongHeSo = Convert.ToDecimal(item["TongSo"]);
                    phuCap.HeSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                    phuCap.HSPCChucVu = Convert.ToDecimal(item["HSPCChucVu"]);
                    phuCap.HSVuotKhung = Convert.ToDecimal(item["HSPCVuotKhung"]);
                    phuCap.MocThoiGian = item["MocThoiGian"].ToString();
                    phuCap.ThamNien = Convert.ToInt32(item["PhanTramTN"]);
                    phuCap.LuongToiThieu = Convert.ToDecimal(item["LuongCoBan"]);
                    phuCap.TienPhuCapThamNien = Convert.ToDecimal(item["MucTienPCTNThang"]);
                    phuCap.TrichNopCaNhan = Convert.ToDecimal(item["CacKhoanTrichNop"]);
                    phuCap.CoQuanTrichNop = Convert.ToDecimal(item["CacKhoanCtyNop"]);
                    phuCap.SoThang = Convert.ToInt32(item["SoThang"]);
                    phuCap.KinhPhi = Convert.ToDecimal(item["KinhPhi"]);
                    phuCap.ThucLinh = Convert.ToDecimal(item["SoTienCaNhanLinh"]);
                    phuCap.BoPhan = item["TenBoPhan"].ToString();
                    DanhSachCanBoList.Add(phuCap);
                }
            }
        }
    }

}
