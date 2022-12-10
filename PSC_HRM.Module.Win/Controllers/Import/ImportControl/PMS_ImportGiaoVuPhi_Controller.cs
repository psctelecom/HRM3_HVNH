﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using System.Windows.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.Controllers;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoHiem;
using PSC_HRM.Module.PMS.NghiepVu.ThanhToan;
using ERP.Module.Win.Controllers.Import.ImportClass;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using PSC_HRM.Module.PMS.NghiepVu.PhiGiaoVu;

namespace PSC_HRM.Module.Controllers.Import
{
    public partial class PMS_ImportGiaoVuPhi_Controller : ViewController
    {
        IObjectSpace _obs = null;
        public PMS_ImportGiaoVuPhi_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyPhiGiaoVu_DetailView";
        }

        private void btImportKhoiLuong_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            try
            {
                QuanLyPhiGiaoVu OidQuanLy = View.CurrentObject as QuanLyPhiGiaoVu;
                if (OidQuanLy != null)
                {
                    //if (OidQuanLy.BangChotThuLao == null)
                    //{

                    _obs = View.ObjectSpace;
                    View.ObjectSpace.CommitChanges();
                    SqlParameter[] param = new SqlParameter[1]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@QuanLyPhiGiaoVu", OidQuanLy.Oid);
                    DataProvider.ExecuteNonQuery("spd_PMS_XoaChiTietGiaoVu", System.Data.CommandType.StoredProcedure, param);
                    Imp_GiaoVuPhi.XuLy(_obs, OidQuanLy);
                    _obs.Refresh();
                    //}                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
