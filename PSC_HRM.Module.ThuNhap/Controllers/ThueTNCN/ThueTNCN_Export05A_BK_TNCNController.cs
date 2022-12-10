using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.Thue;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class ThueTNCN_Export05A_BK_TNCNController : ViewController
    {
        public ThueTNCN_Export05A_BK_TNCNController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ToKhaiQuyetToanThueTNCN toKhai = View.CurrentObject as ToKhaiQuyetToanThueTNCN;
            if (toKhai != null)
            {
                Export05AK_TNCN export = new Export05AK_TNCN();
                export.XuLy(View.ObjectSpace, toKhai);
            }
        }
    }
}
