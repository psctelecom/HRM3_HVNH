using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using PSC_HRM.Module.PMS.NonPersistent;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using PSC_HRM.Module.PMS.NonPersistentObjects;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class Clone_CauHinhQuyDoiPMS_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CauHinhQuyDoiPMS clone;
        CollectionSource collectionSource;
        ThongTinClone _source;
        public Clone_CauHinhQuyDoiPMS_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "CauHinhQuyDoiPMS_DetailView";
        }

        void Clone_PMS_Controller_Activated(object sender, System.EventArgs e)
        {
            
        }

        private void popClone_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));

            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));
            _source = new ThongTinClone(ses);
            e.View = Application.CreateDetailView(_obs, _source);
        }
        private void popClone_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            clone = View.CurrentObject as CauHinhQuyDoiPMS;
            if (clone != null)
            {
                View.ObjectSpace.CommitChanges();
                SqlParameter[] pDongBo = new SqlParameter[4];
                pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                pDongBo[2] = new SqlParameter("@Loai", "CauHinhQuyDoiPMS");
                pDongBo[3] = new SqlParameter("@Oid", clone.Oid);
                object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone", CommandType.StoredProcedure, pDongBo);
                if (kq != null)
                    XtraMessageBox.Show(kq.ToString(), "Thông báo!");
            }
        }
    }
}