using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.BusinessObjects.BaoMat
{
    [ModelDefault("Caption","Người sử dụng - Chức năng phụ")]
    public class NguoiSuDungChucNangPhu : BaseObject
    {
        private NguoiSuDung _NguoiSuDung;
        private PhanQuyenChucNangPhu _PhanQuyenChucNangPhu;

        [Association("PhanQuyenChucNangPhu-listNguoiSuDung")]
        public PhanQuyenChucNangPhu PhanQuyenChucNangPhu
        { get { return _PhanQuyenChucNangPhu; }
            set { SetPropertyValue("PhanQuyenChucNangPhu", ref _PhanQuyenChucNangPhu, value); }
        }



        [ModelDefault("Caption", "Tài khoản")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NguoiSuDung NguoiSuDung
        {
            get { return _NguoiSuDung; }
            set { SetPropertyValue("NguoiSuDung", ref _NguoiSuDung, value); }
        }

        public NguoiSuDungChucNangPhu(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}