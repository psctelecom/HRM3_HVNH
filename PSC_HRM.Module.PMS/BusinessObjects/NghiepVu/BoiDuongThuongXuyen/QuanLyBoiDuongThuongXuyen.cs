using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.PMS.NghiepVu.QuanLyBoiDuongThuongXuyen
{
    [ModelDefault("Caption", "Quản lý bồi dưỡng thường xuyên")]
    [DefaultProperty("ThongTin")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")]
    public class QuanLyBoiDuongThuongXuyen : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;
        private BacDaoTao _BacDaoTao;
        private KyTinhPMS _KyTinhPMS;
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
            }
        }
        [ModelDefault("Caption", "Trường")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        

        [ModelDefault("Caption", "Kỳ tính PMS")]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set { SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value); }
        }

        [Aggregated]
        [Association("QuanLyBoiDuongThuongXuyen-ListChiTietThuLaoBoiDuongThuongXuyen")]
        [ModelDefault("Caption", "Chi tiết bồi dưỡng")]
        public XPCollection<ChiTietThuLaoBoiDuongThuongXuyen> ListChiTietThuLaoBoiDuongThuongXuyen
        {
            get
            {
                return GetCollection<ChiTietThuLaoBoiDuongThuongXuyen>("ListChiTietThuLaoBoiDuongThuongXuyen");
            }
        }

        [Aggregated]
        [Association("QuanLyBoiDuongThuongXuyen-ListChiTietThuLaoCongTacPhi_BDTX")]
        //[Browsable(false)]
        [ModelDefault("Caption", "Chi tiết công tác phí")]
        public XPCollection<ChiTietThuLaoCongTacPhi_BDTX> ListChiTietThuLaoCongTacPhi_BDTX
        {
            get
            {
                return GetCollection<ChiTietThuLaoCongTacPhi_BDTX>("ListChiTietThuLaoCongTacPhi_BDTX");
            }
        }
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        //[NonPersistent]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public String ThongTin
        {
            get
            {
                return String.Format("{0} {1} {2} {3}", ThongTinTruong != null ? ThongTinTruong.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", BacDaoTao != null ? " Bậc đào tạo " + BacDaoTao.TenBacDaoTao : "", KyTinhPMS != null ? " - Đợt " + KyTinhPMS.Dot.ToString() : "");
            }
        }

        public QuanLyBoiDuongThuongXuyen(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

       
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }

    }
}