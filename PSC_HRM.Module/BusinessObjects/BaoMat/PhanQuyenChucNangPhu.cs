using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;

namespace PSC_HRM.Module.BusinessObjects.BaoMat
{
    [ModelDefault("Caption","Phân quyền chức năng phụ")]
    public class PhanQuyenChucNangPhu : BaseObject
    {
        private PhanHeEnum _PhanHe;
        private string _TenNhomQuyen;

        [ModelDefault("Caption", "Phân hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanHeEnum PhanHe
        {
            get { return _PhanHe; }
            set { SetPropertyValue("PhanHe", ref _PhanHe, value); }
        }

        [ModelDefault("Caption", "Tên nhóm quyền")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomQuyen
        {
            get { return _TenNhomQuyen; }
            set { SetPropertyValue("TenNhomQuyen", ref _TenNhomQuyen, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chức năng phụ")]
        [Association("PhanQuyenChucNangPhu-listChucNangPhu")]
        public XPCollection<DanhSachChucNangPhu> listChucNangPhu
        {
            get
            {
                return GetCollection<DanhSachChucNangPhu>("listChucNangPhu");
            }
        }
        [Aggregated]
        [ModelDefault("Caption", "Người sử dụng")]
        [Association("PhanQuyenChucNangPhu-listNguoiSuDung")]
        public XPCollection<NguoiSuDungChucNangPhu> listNguoiSuDung
        {
            get
            {
                return GetCollection<NguoiSuDungChucNangPhu>("listNguoiSuDung");
            }
        }

        public PhanQuyenChucNangPhu(Session session)
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