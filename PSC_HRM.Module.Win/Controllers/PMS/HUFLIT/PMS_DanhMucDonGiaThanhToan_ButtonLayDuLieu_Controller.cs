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
using PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.CongTacPhi;
using System.Windows.Forms;
using PSC_HRM.Module.PMS.BaoCao;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.PMS.NonPersistentObjects;
using PSC_HRM.Module.PMS.NghiepVu.HUFLIT;

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class PMS_DanhMucDonGiaThanhToan_ButtonLayDuLieu_Controller : ViewController
    {
        IObjectSpace _obs = null;
        Session session;
        public PMS_DanhMucDonGiaThanhToan_ButtonLayDuLieu_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "DanhSachDonGiaThanhToanMonHoc_DetailView";
        }

        private void PMS_DanhMucDonGiaThanhToan_ButtonLayDuLieu_Controller_ViewControlsCreated(object sender, System.EventArgs e)
        {
            IObjectSpace os1 = Application.CreateObjectSpace();
            Session ses = ((XPObjectSpace)os1).Session;//Session được sử dụng để load và lưu lại  

            DetailView view = View as DetailView;

            DanhSachDonGiaThanhToanMonHoc qly = View.CurrentObject as DanhSachDonGiaThanhToanMonHoc;
            //
            if (view != null)
            {
                ViewItem item = ((DetailView)View).FindItem("btnLoadData") as ViewItem;//ControlViewItem;
                //
                if (item != null)
                {
                    SimpleButton btnSearch = item.Control as SimpleButton;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Lấy dữ liệu";
                        btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            //Gọi hàm createCommand() bên class TestDemo
                            qly.LoadData();
                        };
                    }
                }

            }
        }
    }
}