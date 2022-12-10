using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;


namespace PSC_HRM.Module.PMS.NghiepVu.KhaoThi
{

    [ModelDefault("Caption", "Chi tiết chấm bài - coi thi")]
    public class ChiTietChamBaiCoiThi_HVNH : BaseObject
    {
        #region key
        private ThongTinChamBai _ThongTinChamBai;
        [Association("ThongTinChamBai-ListChiTietChamBaiCoiThi")]
        [ModelDefault("Caption", "Quản lý chấm bài - coi thi")]
        [Browsable(false)]
        public ThongTinChamBai ThongTinChamBai
        {
            get
            {
                return _ThongTinChamBai;
            }
            set
            {
                SetPropertyValue("ThongTinChamBai", ref _ThongTinChamBai, value);
            }
        }
        #endregion

        #region KhaiBao
        private string _TenMonHoc;
        private string _LopHocPhan;
        private decimal _SoBai;
        private decimal _TongGioQuyDoi;
        private string _HinHThucThi;
        private CoSoGiangDay _CoSo;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao _HeDaoTao;
        private DateTime _NgayThi;
        private string _GioThi;
        private string _CaThi;

        #endregion


        [ModelDefault("Caption", "Tên môn học")]
        public string TenMonHoc
        {
            get { return _TenMonHoc; }
            set { SetPropertyValue("TenMonHoc", ref _TenMonHoc, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }
      
       
        [ModelDefault("Caption", "Số bài")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoBai
        {
            get { return _SoBai; }
            set { SetPropertyValue("SoBai", ref _SoBai, value); }
        }
      
        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Hình thức thi")]
        public string HinHThucThi
        {
            get { return _HinHThucThi; }
            set { SetPropertyValue("HinHThucThi", ref _HinHThucThi, value); }
        }

        [ModelDefault("Caption", "Cơ sở")]
        public CoSoGiangDay CoSo
        {
            get { return _CoSo; }
            set { SetPropertyValue("CoSo", ref _CoSo, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [Browsable(false)]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        [Browsable(false)]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { SetPropertyValue("HeDaoTao", ref _HeDaoTao, value); }
        }

        [ModelDefault("Caption", "Ngày thi")]
        public DateTime NgayThi
        {
            get { return _NgayThi; }
            set { SetPropertyValue("NgayThi", ref _NgayThi, value); }
        }

        [ModelDefault("Caption", "Giờ thi")]
        [Browsable(false)]
        public string GioThi
        {
            get { return _GioThi; }
            set { SetPropertyValue("GioThi", ref _GioThi, value); }
        }

        [ModelDefault("Caption", "Ca thi")]
        [Browsable(false)]
        public string CaThi
        {
            get { return _CaThi; }
            set { SetPropertyValue("CaThi", ref _CaThi, value); }
        }
        public ChiTietChamBaiCoiThi_HVNH(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
       }
    }

}
