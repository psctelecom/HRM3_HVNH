using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.Forms;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Win.Controllers.NhanSu
{
    public partial class HoSo_TrichDanhSachThinhGiangController : ViewController
    {
        public HoSo_TrichDanhSachThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace obs = Application.CreateObjectSpace();
            using (frmChonThinhGiang chonCanBo = new frmChonThinhGiang(((XPObjectSpace)obs).Session))
            {
                if (chonCanBo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    (View.CurrentObject as TrichDanhSachThinhGiang).ListChiTietTrichDanhSachThinhGiang = chonCanBo.LayDanhSachThinhGiang();
                    (View as DetailView).Refresh();
                }
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                e.ShowViewParameters.Context = TemplateContext.View;
            }
        }
    }
}
