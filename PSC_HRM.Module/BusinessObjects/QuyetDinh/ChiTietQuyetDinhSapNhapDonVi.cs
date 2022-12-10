using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.KhenThuong;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Danh sách Đơn vị")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhSapNhapDonVi;BoPhan")]
    public class ChiTietQuyetDinhSapNhapDonVi : BaseObject, IBoPhan
    {
        private QuyetDinhSapNhapDonVi _QuyetDinhSapNhapDonVi;
        private BoPhan _BoPhan;
        private string _GhiChu;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định sáp nhập đơn vị")]
        [Association("QuyetDinhSapNhapDonVi-ListChiTietQuyetDinhSapNhapDonVi")]
        public QuyetDinhSapNhapDonVi QuyetDinhSapNhapDonVi
        {
            get
            {
                return _QuyetDinhSapNhapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhSapNhapDonVi", ref _QuyetDinhSapNhapDonVi, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị cũ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public ChiTietQuyetDinhSapNhapDonVi(Session session) : base(session) { }

    }

}
