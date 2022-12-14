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
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Reports;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.PMS;
using PSC_HRM.Module.ThuNhap.ThuLao;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.NghiepVu.NCKH;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.PMS.BaoCao;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class PMS_tkb_LayThongTinChiTietController : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        DanhSachThongTinThoiKhoaBieuGiangVien _HoatDong;
        public PMS_tkb_LayThongTinChiTietController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            
            _HoatDong = View.CurrentObject as DanhSachThongTinThoiKhoaBieuGiangVien;

            IObjectSpace _obs = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)_obs).Session;
            if (_HoatDong != null)
            {
                _HoatDong.LoadData(_HoatDong.LopHocPhan, ses);

                CollectionSource collectionSource = new CollectionSource(_obs, typeof(ChiTietDanhSachThongTinThoiKhoaBieuGiangVien));
                foreach(ChiTietDanhSachThongTinThoiKhoaBieuGiangVien item in _HoatDong.ListDanhSach)
                {
                    collectionSource.Add(item);
                }

                ShowViewParameters showView = new ShowViewParameters();
                showView.CreatedView = Application.CreateListView(Application.GetListViewId(typeof(ChiTietDanhSachThongTinThoiKhoaBieuGiangVien)), collectionSource, false);
                showView.TargetWindow = TargetWindow.NewWindow;
                showView.Context = TemplateContext.PopupWindow;
                showView.CreateAllControllers = false;
                Application.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));

            }
        }
    }
}