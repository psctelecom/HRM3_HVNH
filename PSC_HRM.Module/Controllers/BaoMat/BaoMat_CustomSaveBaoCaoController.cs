using System;
using System.ComponentModel;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraTreeList.Nodes;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.BaoMat;
using DevExpress.XtraTreeList;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BaoMat_CustomSaveBaoCaoController : ViewController
    {
        PhanQuyenBaoCao _phanQuyenBaoCao = null;
        //
        public BaoMat_CustomSaveBaoCaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BaoMat_CustomSaveBaoCaoController");
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

        void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            Process();
        }

        private void Process()
        {
            _phanQuyenBaoCao = View.CurrentObject as PhanQuyenBaoCao;

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
            if (node.Checked && node.Tag is HRMReport)
            {
                if (!SearchQuyen(_phanQuyenBaoCao.Quyen, ((HRMReport)node.Tag).Oid.ToString()))
                    _phanQuyenBaoCao.Quyen += ((HRMReport)node.Tag).Oid.ToString() + ";";
            }
            if (!node.Checked && node.Tag is HRMReport)
            {
                if (SearchQuyen(_phanQuyenBaoCao.Quyen, ((HRMReport)node.Tag).Oid.ToString()))
                   _phanQuyenBaoCao.Quyen = _phanQuyenBaoCao.Quyen.Replace(((HRMReport)node.Tag).Oid.ToString() + ";",String.Empty);
            }

            if (node.HasChildren)
            {
                foreach (TreeListNode item in node.Nodes)
                    GetChildNode(item);
            }
        }

        private bool SearchQuyen(string quyen, string idReport)
        {
            if (!string.IsNullOrEmpty(quyen))
            {
                string[] quyenList = quyen.Split(";".ToCharArray());
                for (int i = 0; i < quyenList.Length; i++)
                    if (quyenList[i] == idReport)
                    {
                        return true;
                    }
            }
            return false;
        }
    }
}
