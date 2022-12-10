using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.NghiepVu.KhaoThi;

namespace PSC_HRM.Module.PMS.NghiepVu.HVNH
{
    [ModelDefault("Caption", "Quản lý bài kiểm tra")]

    [DefaultProperty("ThongTin")]

    public class QuanLyBaiKiemTra : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private decimal _HeSoBaiKTra;
        private Guid _BangChotThuLao;

        [Browsable(false)]
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
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if(value != null)
                {
                    HocKy = null;
                }
            }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("Caption", "Hệ số bài kiểm tra")]
        public decimal HeSoBaiKTra
        {
            get
            {
                return _HeSoBaiKTra;
            }
            set
            {
                SetPropertyValue("HeSoBaiKTra", ref _HeSoBaiKTra, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thù lao")]
        public Guid BangChotThuLao
        {
            get
            {
                return _BangChotThuLao;
            }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
            }
        }


        [Aggregated]
        [Association("QuanLyBaiKiemTra-ListDanhSachBaiKtra")]
        [ModelDefault("Caption", "Số lượng sv tham gia thi")]
        public XPCollection<DanhSachChiTietBaiKiemTra> ListDanhSachBaiKtra
        {
            get
            {
                return GetCollection<DanhSachChiTietBaiKiemTra>("ListDanhSachBaiKtra");
            }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1}", NamHoc != null ? "Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? HocKy.TenHocKy : "");
            }
        }
        public QuanLyBaiKiemTra(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

    }

}