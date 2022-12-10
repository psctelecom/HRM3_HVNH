using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Controllers.PMS.UEL
{
    public partial class Clone_CauHinhQuyDoiPMS_UEL_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CauHinhQuyDoiPMS_UEL clone;
        CollectionSource collectionSource;
        ThongTinClone _source;
        public Clone_CauHinhQuyDoiPMS_UEL_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "CauHinhQuyDoiPMS_UEL_DetailView";
          
        }
    
        private void popCloneCH_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            clone = View.CurrentObject as CauHinhQuyDoiPMS_UEL;
            if (clone != null)
            {
                if (_source.NamHoc == null && _source.HocKy==null)
                {
                    XtraMessageBox.Show("Bạn chưa chọn năm học và học kỳ", "Thông báo!");
                }
                else if (_source.NamHoc == null)
                {
                    XtraMessageBox.Show("Bạn chưa chọn năm học", "Thông báo!");
                }
                else if (_source.HocKy == null)
                {
                    XtraMessageBox.Show("Bạn chưa chọn học kỳ", "Thông báo!");
                }
                else
                {
                    View.ObjectSpace.CommitChanges();
                    SqlParameter[] pDongBo = new SqlParameter[5];
                    pDongBo[0] = new SqlParameter("@ThongTinTruong", _source.ThongTinTruong.Oid);
                    pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                    pDongBo[2] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                    pDongBo[3] = new SqlParameter("@Loai", "CauHinhQuyDoiPMS");
                    pDongBo[4] = new SqlParameter("@Oid", clone.Oid);
                    object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone", CommandType.StoredProcedure, pDongBo);
                    if (kq != null)
                        XtraMessageBox.Show(kq.ToString(), "Thông báo!");
                }                
            }
        }

        private void popCloneCH_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));

            collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));
            _source = new ThongTinClone(ses);
            _source.An = false;
            e.View = Application.CreateDetailView(_obs, _source);
        }
    }
}
