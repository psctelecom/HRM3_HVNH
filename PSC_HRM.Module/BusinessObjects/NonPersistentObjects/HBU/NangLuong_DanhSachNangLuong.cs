using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.QuyetDinh;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách nâng lương")]
    public class NangLuong_DanhSachNangLuong : TruongBaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<NangLuong_DanhSachNangLuongItem> ListChiTiet { get; set; }

        public NangLuong_DanhSachNangLuong(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListChiTiet = new XPCollection<NangLuong_DanhSachNangLuongItem>(Session, false);
            //
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue &&
                TuNgay <= DenNgay)
            {
                //
                ListChiTiet.Reload();
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@TuNgay", this.TuNgay);
                param[1] = new SqlParameter("@DenNgay", this.DenNgay);
                //
                DataTable dt = DataProvider.GetDataTable("spd_Report_NangLuong_DanhSachNangLuong_NonPersistent", CommandType.StoredProcedure, param);
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        NangLuong_DanhSachNangLuongItem chitiet;
                        foreach (DataRow item in dt.Rows)
                        {
                            chitiet = new NangLuong_DanhSachNangLuongItem(Session);
                            chitiet.SoQuyetDinh = item["SoQuyetDinh"].ToString();
                            chitiet.NgayQuyetDinh = Convert.ToDateTime(item["NgayQuyetDinh"]);
                            chitiet.NgayHieuLuc = Convert.ToDateTime(item["NgayHieuLuc"]);
                            chitiet.NguoiKy1 = item["NguoiKy1"].ToString();
                            chitiet.NhanVienText = item["NhanVienText"].ToString();
                            chitiet.BoPhanText = item["BoPhanText"].ToString();
                            if(item["NgayHuongLuongCu"] != DBNull.Value)
                            chitiet.NgayHuongLuongCu = Convert.ToDateTime(item["NgayHuongLuongCu"]);
                            chitiet.ThuongHieuQuaTheoThangCu = Convert.ToDecimal(item["ThuongHieuQuaTheoThangCu"]);
                            chitiet.MucLuongCu = Convert.ToDecimal(item["MucLuongCu"]);
                            if (item["NgayHuongLuongMoi"] != DBNull.Value)
                            chitiet.NgayHuongLuongMoi = Convert.ToDateTime(item["NgayHuongLuongMoi"]);
                            chitiet.ThuongHieuQuaTheoThangMoi = Convert.ToDecimal(item["ThuongHieuQuaTheoThangMoi"]);
                            chitiet.MucLuongMoi = Convert.ToDecimal(item["MucLuongMoi"]);

                            ListChiTiet.Add(chitiet);
                        }
                    }
                }          
            }
        }
    }

}
