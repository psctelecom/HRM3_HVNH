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
using DevExpress.ExpressApp.Editors;
using System.Data;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.TuyenDung;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng thỉnh giảng")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuanLyHopDongThinhGiang;SoHopDong")]

    public class HopDong_ThinhGiang : HopDong
    {
        // Fields...
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private decimal _SoTien1Tiet;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private string _DiaDiemGiangDay;

        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);

            }
        }
        [ModelDefault("Caption", "Số tiền 1 tiết")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
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
       
        [ModelDefault("Caption", "Từ ngày")]      
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
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading && value != DateTime.MinValue)
                {                   
                    DateTime currentDate = HamDungChung.GetServerTime();
                    if (value < currentDate)
                        HopDongCu = true;
                    else
                        HopDongCu = false;
                }
            }
        }

        [ModelDefault("Caption", "Đại điểm giảng dạy")]
        public string DiaDiemGiangDay
        {
            get
            {
                return _DiaDiemGiangDay;
            }
            set
            {
                SetPropertyValue("DiaDiemGiangDay", ref _DiaDiemGiangDay, value);
            }
        }
      

        [Aggregated]
        [ModelDefault("Caption", "Danh sách môn")]
        [Association("HopDong_ThinhGiang-ListChiTietHopDongThinhGiang")]
        public XPCollection<ChiTietHopDongThinhGiang> ListChiTietHopDongThinhGiang
        {
            get
            {
                return GetCollection<ChiTietHopDongThinhGiang>("ListChiTietHopDongThinhGiang");
            }
        }

        public HopDong_ThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNhanVienList();
           
            MaTruong = TruongConfig.MaTruong;
            //
            LoaiHopDong = "Hợp đồng thỉnh giảng";
            //
            PhanLoaiNguoiKy = NguoiKyEnum.DangTaiChuc;
            ChucVuNguoiKy = Session.FindObject<ChucVu>(CriteriaOperator.Parse("TenChucVu like ?", "Hiệu trưởng"));
            if (ChucVuNguoiKy != null)
                NguoiKy = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("ChucVu=? and TinhTrang.TenTinhTrang like ?", ChucVuNguoiKy.Oid, "%Đang làm việc%"));
           
            ChucDanhChuyenMon = "Giảng viên";
            if (ThongTinTruong != null)
                DiaDiemGiangDay = ThongTinTruong.TenBoPhan;

            
        }

        protected override void OnLoading()
        {
            base.OnLoading();
        }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
            {
                CriteriaOperator result = CriteriaOperator.Parse("");
                SqlParameter param;

                StringBuilder sb = new StringBuilder();
                foreach (string item in HamDungChung.DanhSachBoPhanDuocPhanQuyen(Session))
                {
                    sb.Append(item + ",");
                }
                param = new SqlParameter("@BoPhan", sb.ToString());

                using (DataTable dt = DataProvider.GetDataTable("spd_Filter_GiangVienThinhGiang", CommandType.StoredProcedure, param))
                {
                    List<Guid> oid = new List<Guid>();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            oid.Add(new Guid(item[0].ToString()));
                        }
                        result = new InOperator("Oid", oid);
                    }
                }
                NVList = new XPCollection<NhanVien>(Session, result);
            }
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

        protected override void TaoSoHopDong()
        {
            SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDongThinhGiang.Oid);
            SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongThinhGiang, param);
        }
    }

}
