using System;

using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.HopDong;
using System.Text;
using PSC_HRM.Module.HoSo;
using System.IO;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Import hợp đồng thỉnh giảng từ file excel")]
    public class HopDongThinhGiang_ImportHopDongThinhGiang : BaseObject
    {
  
        private TaoHopDongThinhGiangEnum _TaoHopDongThinhGiangEnum;

        [ModelDefault("Caption", "Loại hợp đồng thỉnh giảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TaoHopDongThinhGiangEnum TaoHopDongThinhGiangEnum
        {
            get
            {
                return _TaoHopDongThinhGiangEnum;
            }
            set
            {
                SetPropertyValue("TaoHopDongThinhGiangEnum", ref _TaoHopDongThinhGiangEnum, value);
            }
        }

        public HopDongThinhGiang_ImportHopDongThinhGiang(Session session) : base(session) { }
      
    }

}
