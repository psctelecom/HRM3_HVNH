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

namespace PSC_HRM.Module.Win.Controllers.PMS
{
    public partial class KhoiLuongGiangDay_Onload_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public KhoiLuongGiangDay_Onload_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetObjectType = typeof(KhoiLuongGiangDay);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            _obs = View.ObjectSpace;
            Session ses = ((XPObjectSpace)_obs).Session;
            KhoiLuongGiangDay obj = View.CurrentObject as KhoiLuongGiangDay;
            if (obj != null)
            {
                //obj.LoadDanhSachNhaNVien();
                SortingCollection sortNV = new SortingCollection();
                sortNV.Add(new SortProperty("NhanVien", DevExpress.Xpo.DB.SortingDirection.Ascending));
                obj.ListChiTietKhoiLuongGiangDay.Sorting = sortNV;
            }
        }

    }
}