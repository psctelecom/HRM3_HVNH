using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Học phần")]
    [DefaultProperty("TenHocPhan")]
    public class HocPhan : BaseObject
    {
        private BacDaoTao _BacDaoTao;
        private string _TenHocPhan;
        private string _MaQuanLy;

        #region Dùng cho Sau đại học
        private string _MaHocPhan_Chu;
        private int _MaHocPhan_So;
        private decimal _SoTinChi;


        [ModelDefault("Caption", "Mã HP (Chữ)")]
        public string MaHocPhan_Chu
        {
            get { return _MaHocPhan_Chu; }
            set { SetPropertyValue("MaHocPhan_Chu", ref _MaHocPhan_Chu, value); }
        }
        [ModelDefault("Caption", "Mã HP (Số)")]
        public int MaHocPhan_So
        {
            get { return _MaHocPhan_So; }
            set { SetPropertyValue("MaHocPhan_So", ref _MaHocPhan_So, value); }
        }


        [ModelDefault("Caption", "Số tín chỉ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal SoTinChi
        {
            get { return _SoTinChi; }
            set { SetPropertyValue("SoTinChi", ref _SoTinChi, value); }
        }
        #endregion

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }
        [ModelDefault("Caption", "Tên HP")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Size(-1)]
        public string TenHocPhan
        {
            get { return _TenHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }
        public HocPhan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}