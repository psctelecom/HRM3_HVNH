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
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_Contract")]
    [DefaultProperty("SoHopDong")]
    [ModelDefault("Caption", "Hợp đồng kiêm giảng")]
 
    public class HopDong_KiemGiang : HopDong
    {
        // Fields...
        
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private string _DiaDiemGiangDay;
        private BoPhan _TaiKhoa;
        private string _MonHoc;
        private decimal _PhuCapKiemGiang;
         
        [ImmediatePostData]
        [ModelDefault("Caption", "Môn dạy kiêm giảng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CurriculumEditor")]
        public string MonHoc
        {
            get
            {
                return _MonHoc;
            }
            set
            {
                SetPropertyValue("MonHoc", ref _MonHoc, value);

            }
        }


        [ModelDefault("Caption", "Tại khoa")]
        public BoPhan TaiKhoa
        {
            get
            {
                return _TaiKhoa;
            }
            set
            {
                SetPropertyValue("TaiKhoa", ref _TaiKhoa, value);
            }
        }

        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("Caption", "Phụ cấp kiêm giảng")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public decimal PhuCapKiemGiang
        {
            get
            {
                return _PhuCapKiemGiang;
            }
            set
            {
                SetPropertyValue("PhuCapKiemGiang", ref _PhuCapKiemGiang, value);
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
      



        public HopDong_KiemGiang(Session session) : base(session) { }

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
     
 	         base.OnDeleting();
        }

        protected override void TaoSoHopDong()
        {
            SqlParameter param = new SqlParameter("@QuanLyHopDong", QuanLyHopDongThinhGiang.Oid);
            SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongThinhGiang, param);
        }
    }

}
