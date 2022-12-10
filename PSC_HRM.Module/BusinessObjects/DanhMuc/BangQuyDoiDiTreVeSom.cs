using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Bảng quy đổi đi trễ về sớm")]
    public class BangQuyDoiDiTreVeSom : BaseObject
    {
        // Fields...
        private HinhThucViPham _HinhThucViPham;
        private int _SoPhutDen;
        private int _SoPhutTu;

        [ModelDefault("Caption", "Số phút từ")]
        public int SoPhutTu
        {
            get
            {
                return _SoPhutTu;
            }
            set
            {
                SetPropertyValue("SoPhutTu", ref _SoPhutTu, value);
            }
        }

        [ModelDefault("Caption", "Số phút đến")]
        public int SoPhutDen
        {
            get
            {
                return _SoPhutDen;
            }
            set
            {
                SetPropertyValue("SoPhutDen", ref _SoPhutDen, value);
            }
        }

        [ModelDefault("Caption", "Hình thức vi phạm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public HinhThucViPham HinhThucViPham
        {
            get
            {
                return _HinhThucViPham;
            }
            set
            {
                SetPropertyValue("HinhThucViPham", ref _HinhThucViPham, value);
            }
        }

        public BangQuyDoiDiTreVeSom(Session session) : base(session) { }
    }

}
