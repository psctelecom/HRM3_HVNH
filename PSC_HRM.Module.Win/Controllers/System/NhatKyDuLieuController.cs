using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Win.Controller
{
    public partial class NhatKyDuLieu : ViewController
    {
        private IObjectSpace obs;
        private NhanVien nhanVien;

        public NhatKyDuLieu()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void actNhatKy_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();           

            if (View.CurrentObject is BaseObject)
            {
                CollectionSource audit = new CollectionSource(Application.CreateObjectSpace(), typeof(AuditDataItemPersistent));
                if (View.Id.Contains("ThongTinNhanVien_DetailView"))
                {
                    nhanVien = obs.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=?", (View.CurrentObject as BaseObject).Oid));
                    if (nhanVien.NhanVienThongTinLuong != null && nhanVien.NhanVienTrinhDo != null)
                        audit.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ? OR AuditedObject.GuidId = ? OR AuditedObject.GuidId = ?", (View.CurrentObject as BaseObject).Oid, nhanVien.NhanVienThongTinLuong.Oid, nhanVien.NhanVienTrinhDo.Oid);
                }
                else
                {
                    audit.Criteria["AuditObject"] = CriteriaOperator.Parse("AuditedObject.GuidId = ?", (View.CurrentObject as BaseObject).Oid);
                }
                e.View = Application.CreateListView(Application.GetListViewId(typeof(AuditDataItemPersistent)), audit, true);
                e.DialogController.Active["ByNhatKyDuLieu"] = false;
            }
        }
    }
}
