using System;
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
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGiaCanBo_DonViDanhGiaController : ViewController
    {
        public DanhGiaCanBo_DonViDanhGiaController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGiaCanBo_DonViDanhGiaController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhGiaCanBo danhGia = View.CurrentObject as DanhGiaCanBo;
            if (danhGia != null)
            {
                GroupOperator go = new GroupOperator();
                go.Operands.Add(new InOperator("Oid", HamDungChung.GetCriteriaBoPhan()));
                danhGia.DanhSachDanhGiaLan1.Criteria = go;

                foreach (DanhGiaLan1 item in danhGia.DanhSachDanhGiaLan1)
                {
                    item.XepLoai1 = item.XepLoai;
                }
                View.ObjectSpace.CommitChanges();
            }
        }

        private void DanhGiaCanBo_DonViDanhGiaController_Activated(object sender, EventArgs e)
        {
            simpleAction2.Active["TruyCap"] = true;
        }
    }
}
