using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.XtraTreeList.Nodes;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;


namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmChonBoPhan : XtraForm
    {
        private XPCollection<BoPhan> donViList;
        private XPCollection<BoPhan> result;
        private Session session;

        public frmChonBoPhan()
        {
            InitializeComponent();
        }

        public frmChonBoPhan(Session session)
        {
            InitializeComponent();
            this.session = session;
        }

        private void frmBoPhan_Load(object sender, EventArgs e)
        {
            donViList = new XPCollection<BoPhan>(session, new InOperator("Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(session)));

            AddNode(null, donViList);
            donViTreeList.ExpandAll();
        }

        private void donViTreeList_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            e.Node.Nodes.Clear();
            AddNode(e.Node, ((BoPhan)e.Node.Tag).ListBoPhanCon);
        }

        private void donViTreeList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void donViTreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void AddNode(TreeListNode parent, XPCollection<BoPhan> donViList)
        {
            donViTreeList.BeginUnboundLoad();

            foreach (BoPhan item in donViList)
            {
                if (parent == null)
                {
                    if (item.BoPhanCha == null)
                    {
                        TreeListNode node = donViTreeList.AppendNode(new object[] { item.TenBoPhan }, parent);
                        node.Tag = item;
                        if (item.ListBoPhanCon != null && item.ListBoPhanCon.Count > 0)
                        {
                            node.HasChildren = true;
                        }
                    }
                }
                else
                {
                    TreeListNode node = donViTreeList.AppendNode(new object[] { item.TenBoPhan }, parent);
                    node.Tag = item;
                    if (item.ListBoPhanCon != null && item.ListBoPhanCon.Count > 0)
                    {
                        node.HasChildren = true;
                    }
                }
            }

            donViTreeList.EndUnboundLoad();
        }

        private static void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private static void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }

        public XPCollection<BoPhan> LayDanhSachDonVi()
        {
            result = new XPCollection<BoPhan>(session, false);

            LayDanhSachDonVi(donViTreeList.Nodes);

            return result;
        }

        private void LayDanhSachDonVi(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Checked)
                {
                    result.Add((BoPhan)node.Tag);
                }
                else if (node.CheckState == CheckState.Unchecked)
                    continue;

                if (node.Nodes.Count > 0)
                    LayDanhSachDonVi(node.Nodes);
            }
        }

        private void miCheckAll_Click(object sender, EventArgs e)
        {
            NodeState1(donViTreeList.Nodes, CheckState.Checked);
        }

        private void miCheckSelected_Click(object sender, EventArgs e)
        {
            NodeState(donViTreeList.Nodes, CheckState.Checked);
        }

        private void miUncheckAll_Click(object sender, EventArgs e)
        {
            NodeState1(donViTreeList.Nodes, CheckState.Unchecked);
        }

        private void miUncheckSelected_Click(object sender, EventArgs e)
        {
            NodeState(donViTreeList.Nodes, CheckState.Unchecked);
        }

        private static void NodeState(TreeListNodes nodes, CheckState state)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Selected)
                {
                    node.CheckState = state;
                }
                if (node.Nodes.Count > 0)
                    NodeState(node.Nodes, state);
            }
        }

        private static void NodeState1(TreeListNodes nodes, CheckState state)
        {
            foreach (TreeListNode node in nodes)
            {
                node.CheckState = state;
                if (node.Nodes.Count > 0)
                    NodeState1(node.Nodes, state);
            }
        }
    }
}