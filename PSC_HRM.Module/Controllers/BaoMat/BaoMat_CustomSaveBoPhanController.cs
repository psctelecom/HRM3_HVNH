using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraTreeList.Nodes;
using System.Text;
using PSC_HRM.Module.BaoMat;
using DevExpress.XtraTreeList;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoMat_CustomSaveBoPhanController : ViewController
    {
        PhanQuyenDonVi _phanQuyenDonVi = null;
        public BaoMat_CustomSaveBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BaoMat_CustomSaveBoPhanController");
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            ObjectSpace.Committing += ObjectSpace_Committing;
        }

        protected override void OnDeactivated()
        {
            ObjectSpace.Committing -= ObjectSpace_Committing;
            base.OnDeactivated();
        }

        void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Process();
        }

        private void Process()
        {
            _phanQuyenDonVi = View.CurrentObject as PhanQuyenDonVi;

            //cập nhật lại quyền
            DetailView view = View as DetailView;
           
            if (view != null)
            {
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id == "CustomControl")
                    {
                        TreeList treeList = item.Control as TreeList;
                        if (treeList != null)
                        {
                            foreach (TreeListNode node in treeList.Nodes)
                            {
                                GetChildNode(node);
                            }
                        }
                    }
                }
            }
        }

        private void GetChildNode(TreeListNode node)
        {
            if (node.Checked)
            {
                if (!SearchQuyen(_phanQuyenDonVi.Quyen, ((BoPhan)node.Tag).Oid.ToString()))
                    _phanQuyenDonVi.Quyen += ((BoPhan)node.Tag).Oid.ToString() + ";";
            }
            if (!node.Checked)
            {
                if (SearchQuyen(_phanQuyenDonVi.Quyen, ((BoPhan)node.Tag).Oid.ToString()))
                    _phanQuyenDonVi.Quyen = _phanQuyenDonVi.Quyen.Replace(((BoPhan)node.Tag).Oid.ToString() + ";", String.Empty);
            }

            if (node.HasChildren)
            {
                foreach (TreeListNode item in node.Nodes)
                    GetChildNode(item);
            }
        }
        private bool SearchQuyen(string quyen, string idBoPhan)
        {
            if (!string.IsNullOrEmpty(quyen))
            {
                string[] quyenList = quyen.Split(";".ToCharArray());
                for (int i = 0; i < quyenList.Length; i++)
                    if (quyenList[i] == idBoPhan)
                    {
                        return true;
                    }
            }
            return false;
        }
    }
}
