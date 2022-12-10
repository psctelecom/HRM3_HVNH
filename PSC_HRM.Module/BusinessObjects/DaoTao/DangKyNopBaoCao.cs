using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [ModelDefault("Caption", "Đăng ký nộp báo cáo")]
    [DefaultProperty("Caption")]
    [RuleCombinationOfPropertiesIsUnique("DangKyNopBaoCao", DefaultContexts.Save, "QuanLyNopBaoCao")]
    public class DangKyNopBaoCao : BaseObject
    {
        private int _Dot = 1;
        QuanLyNopBaoCao _QuanLyNopBaoCao;

        [Browsable(false)]
        [Association("QuanLyNopBaoCao-ListDangKyNopBaoCao")]
        public QuanLyNopBaoCao QuanLyNopBaoCao
        {
            get
            {
                return _QuanLyNopBaoCao;
            }
            set
            {
                SetPropertyValue("QuanLyNopBaoCao", ref _QuanLyNopBaoCao, value);
            }
        }

        [ModelDefault("Caption", "Đợt")]
        public int Dot
        {
            get
            {
                return _Dot;
            }
            set
            {
                SetPropertyValue("Dot", ref _Dot, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("DangKyNopBapCao-ListChiTietDangKyNopBaoCao")]
        public XPCollection<ChiTietDangKyNopBaoCao> ListChiTietDangKyNopBaoCao
        {
            get
            {
                return GetCollection<ChiTietDangKyNopBaoCao>("ListChiTietDangKyNopBaoCao");
            }
        }

        public DangKyNopBaoCao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
