using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Report
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn báo cáo")]
    public class ChonBaoCao : BaseObject
    {
        // Fields...
        private GroupReport _NhomBaoCao;
        private HRMReport _BaoCao;
        private bool _Chon;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Báo cáo")]
        public HRMReport BaoCao
        {
            get
            {
                return _BaoCao;
            }
            set
            {
                SetPropertyValue("BaoCao", ref _BaoCao, value);
                if (!IsLoading && value != null)
                    NhomBaoCao = value.NhomBaoCao;
            }
        }

        [ModelDefault("Caption", "Nhóm báo cáo")]
        public GroupReport NhomBaoCao
        {
            get
            {
                return _NhomBaoCao;
            }
            set
            {
                SetPropertyValue("NhomBaoCao", ref _NhomBaoCao, value);
            }
        }

        public ChonBaoCao(Session session)
            : base(session)
        { }
    }
}
