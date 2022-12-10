using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.PMS.NghiepVu.GDTC_QP
{
    [ModelDefault("Caption", "Quảnl ý GDTC - QP")]
    //[DefaultProperty("TenNganh")]
    public class QuanLyGDTC_QP : ThongTinChungPMS
    {
        private BangChotThuLao _BangChotThuLao;

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set { SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value); }
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

        private BacDaoTao _BacDaoTao;
        private bool _Khoa;
        [ModelDefault("Caption", "Bậc đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("listBacDaoTao")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Khóa")]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [Aggregated]
        [Association("QuanLyGDTC_QP-ListChiTietThongKeGDQP_TC")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ChiTietThongKeGDQP_TC> ListChiTietThongKeGDQP_TC
        {
            get
            {
                return GetCollection<ChiTietThongKeGDQP_TC>("ListChiTietThongKeGDQP_TC");
            }
        }
        public QuanLyGDTC_QP(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }
        [Browsable(false)]
        [ModelDefault("Caption", " Danh sách Bậc đào tạo")]
        public XPCollection<BacDaoTao> listBacDaoTao
        {
            get;
            set;
        }
        public void LoadBacDaoTao()
        {

            listBacDaoTao = new XPCollection<DanhMuc.BacDaoTao>(Session, false);
            if (HamDungChung.CheckAdministrator())
            {
                XPCollection<BacDaoTao> listBDT = new XPCollection<BacDaoTao>(Session);
                if (listBDT != null)
                {
                    foreach (BacDaoTao item in listBDT)
                    {
                        listBacDaoTao.Add(item);
                    }
                }
            }
            else
            {
                CriteriaOperator filter = CriteriaOperator.Parse("NguoiSuDung = ?", HamDungChung.CurrentUser().Oid);
                XPCollection<NguoiSuDung_TheoBacDaoTao> dsBacDaoTao = new XPCollection<NguoiSuDung_TheoBacDaoTao>(Session, filter);
                if (dsBacDaoTao != null)
                {
                    BacDaoTao bdt = null;
                    foreach (var item in dsBacDaoTao)
                    {
                        bdt = Session.FindObject<BacDaoTao>(CriteriaOperator.Parse("Oid =?", item.BacDaoTao.Oid));
                        if (bdt != null)
                            listBacDaoTao.Add(bdt);
                    }
                }
            }
            OnChanged("listBacDaoTao");
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

    }

}