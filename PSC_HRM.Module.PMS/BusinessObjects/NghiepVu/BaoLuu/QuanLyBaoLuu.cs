using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý bảo lưu")]
    public class QuanLyBaoLuu : BaseObject
    {
        private ThongTinTruong _ThongTinTruong;
        private NamHoc _NamHoc;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit","false")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }


        [Aggregated]
        [Association("QuanLyBaoLuu-ListChiTietBaoLuu")]
        [ModelDefault("Caption", "Chi tiết bảo lưu")]
        public XPCollection<ChiTietBaoLuu> ListChiTietBaoLuu
        {
            get
            {
                return GetCollection<ChiTietBaoLuu>("ListChiTietBaoLuu");
            }
        }
        public QuanLyBaoLuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
            // Place here your initialization code.
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
        }
    }
}