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

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Bảng xuất thông tin Import BHXH")]
    public class BaoHiem_XuatThongTinImportBHXH : BaseObject
    {
        private string _NgayVaoCoQuan;

        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày vào cơ quan")]
        public string NgayVaoCoQuan
        {
            get
            {
                return _NgayVaoCoQuan;
            }
            set
            {
                SetPropertyValue("NgayVaoCoQuan", ref _NgayVaoCoQuan, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        DanhSachChiTietList = new XPCollection<BaoHiem_XuatThongTinImportBHXH_Item>(Session, false);
                        GetBangLuong(Session, "Report_BaoHiem_XuatThongTinImportBHXH_NonPersistent", System.Data.CommandType.StoredProcedure, NgayVaoCoQuan);
                    }
                }
            }
        }

      
        
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<BaoHiem_XuatThongTinImportBHXH_Item> ListDanhSachChiTiet
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
        public XPCollection<BaoHiem_XuatThongTinImportBHXH_Item> DanhSachChiTietList { get; set; }

        public BaoHiem_XuatThongTinImportBHXH(Session session)
            : base(session)
        { }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private void GetBangLuong(Session session, string query, CommandType type, params object[] args)
        {
            DataTable dt = new DataTable();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@NgayVaoCoQuan", args[0]);

            SqlCommand cmd = DataProvider.GetCommand(query, type, param);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    BaoHiem_XuatThongTinImportBHXH_Item Item = new BaoHiem_XuatThongTinImportBHXH_Item(session);
                    Item.HoTen = item["HoTen"].ToString();
                    Item.TinhTrang = item["TinhTrang"].ToString();
                    Item.MaPhongBan = item["TenBoPhanVietTat"].ToString();
                    Item.ChucVu = item["ChucVu"].ToString();
                    Item.MaSoSoBHXH = item["SoSoBHXH"].ToString();
                    Item.DiaChiKhaiSinh = item["DCKhaiSinh"].ToString();
                    Item.DiaChiThuongTru = item["DCThuongTru"].ToString();
                    Item.DiaChiLienHe = item["DCLienLac"].ToString();
                    Item.NgaySinh = Convert.ToDateTime(item["NgaySinh"] == DBNull.Value ? DateTime.MinValue : item["NgaySinh"]);
                    Item.CMND = item["CMND"].ToString();
                    Item.NgayCapCMND = Convert.ToDateTime(item["NgayCap"] == DBNull.Value ? DateTime.MinValue : item["NgayCap"]);
                    Item.NoiCapCMND = item["NoiCap"].ToString();
                    Item.GioiTinh = item["GioiTinh"].ToString();
                    Item.QuocTich = item["QuocTich"].ToString();
                    Item.DanToc = item["TenDanToc"].ToString();
                    Item.HeSoLuong = Convert.ToDecimal(item["HeSoLuong"]);
                    Item.PCCV = Convert.ToDecimal(item["HSPCChucVu"]);
                    Item.PCTNVK = Convert.ToDecimal(item["VuotKhung"]);

                    DanhSachChiTietList.Add(Item);
                }
            }
        }
    }

}
