using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Text;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.TaoMaQuanLy;
using System.Data.SqlClient;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using System.Data;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng thỉnh giảng chất lượng cao")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyHopDongThinhGiang;SoHopDong")]
    
    public class HopDong_ThinhGiangChatLuongCao : HopDong
    {
        // Fields...
        private decimal _SoTien1Tiet;

        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("Caption", "Số tiền 1 tiết")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal SoTien1Tiet
        {
            get
            {
                return _SoTien1Tiet;
            }
            set
            {
                SetPropertyValue("SoTien1Tiet", ref _SoTien1Tiet, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách môn")]
        [Association("HopDong_ThinhGiangChatLuongCao-ListChiTietHopDongThinhGiangChatLuongCao")]
        public XPCollection<ChiTietHopDongThinhGiangChatLuongCao> ListChiTietHopDongThinhGiangChatLuongCao
        {
            get
            {
                return GetCollection<ChiTietHopDongThinhGiangChatLuongCao>("ListChiTietHopDongThinhGiangChatLuongCao");
            }
        }

        public HopDong_ThinhGiangChatLuongCao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
            //
            //
            LoaiHopDong = "Hợp đồng thỉnh giảng chất lượng cao";
        }

        protected void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<NhanVien>(Session, HamDungChung.GetCriteriaGiangVienThinhGiang(Session));
        }

        protected override void AfterNhanVienChanged()
        {
            if (NhanVien != null)
            {
                //quốc tịch
                QuocTich = NhanVien.QuocTich;
            }
        }
        protected override void OnDeleting()
        {
            //
            CriteriaOperator filter = CriteriaOperator.Parse("QuanLyDeNghiMoiGiang.NamHoc=? and QuanLyDeNghiMoiGiang.HocKy=? and NhanVien=?", QuanLyHopDongThinhGiang.NamHoc.Oid, QuanLyHopDongThinhGiang.HocKy.Oid, NhanVien.Oid);
            DeNghiMoiGiang deNghiMoiGiang = Session.FindObject<DeNghiMoiGiang>(filter);
            if (deNghiMoiGiang != null)
            {
                deNghiMoiGiang.LapHopDong = false;
            }
            base.OnDeleting();
        }
        /*
        private decimal GetSoTien1TietChatLuongCao(NhanVien nv)
        {
            decimal sotien = 0;
            string hocham = "", hocvi = "";
            using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_PMS.bin")))
            {
                if (cnn.State != ConnectionState.Open)
                    cnn.Open();

                if (nv.NhanVienTrinhDo.HocHam != null)
                    hocham = nv.NhanVienTrinhDo.HocHam.MaQuanLy;
                else if (nv.CongViecHienNay != null)
                    hocham = nv.CongViecHienNay.MaQuanLy;
                if (nv.NhanVienTrinhDo.TrinhDoChuyenMon != null)
                    hocvi = nv.NhanVienTrinhDo.TrinhDoChuyenMon.MaQuanLy;
                string query = @"SELECT a.DonGiaClc FROM dbo.DonGia a
                                 INNER JOIN dbo.HocHam b ON a.MaHocHam = b.MaHocHam
                                 INNER JOIN dbo.HocVi c ON a.MaHocVi = c.MaHocVi
                                 where b.MaQuanLy like '" + hocham + "' and c.MaQuanLy like '" + hocvi + "'";

                object obj = DataProvider.GetObject(cnn, query, CommandType.Text);
                if (obj != null)
                    sotien = Convert.ToDecimal(obj);
                else
                    sotien = 0;
                return sotien;
            }
        }*/
    }

}
