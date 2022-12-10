using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;

using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmChonThinhGiang : XtraForm
    {
        private XPCollection<BoPhan> donViList;
        private XPCollection<BoPhan> filter;
        private XPCollection<GiangVienThinhGiang> nhanVienList;
        private XPCollection<ChiTietTrichDanhSachThinhGiang> trichList;
        private GiangVienThinhGiang nv;
        private Session session;

        public frmChonThinhGiang()
        {
            InitializeComponent();
        }

        public frmChonThinhGiang(Session session)
        {
            InitializeComponent();
            this.session = session;
        }

        private void frmChonCanBo_Load(object sender, EventArgs e)
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

            //filter nhan vien list
            LayDanhSachDonVi();

            //clear nhan vien treelist
            nhanVienTreeList.Nodes.Clear();

            //add nv to nvtreelist
            AddDonVi(null, filter);
            nhanVienTreeList.ExpandAll();
        }

        private void nhanVienTreeList_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Tag is BoPhan)
            {
                XPCollection<GiangVienThinhGiang> nhanVienList = new XPCollection<GiangVienThinhGiang>(session, CriteriaOperator.Parse("BoPhan=?", ((BoPhan)e.Node.Tag).Oid));
                AddThinhGiang(e.Node, nhanVienList);
            }
        }

        private void nhanVienTreeList_BeforeCheckNode(object sender, DevExpress.XtraTreeList.CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void nhanVienTreeList_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            SetCheckedParentNodes(e.Node, e.Node.CheckState);
        }

        private void AddDonVi(TreeListNode parent, XPCollection<BoPhan> bpList)
        {
            nhanVienTreeList.BeginUnboundLoad();

            foreach (BoPhan item in bpList)
            {
                if (item.ListNhanVien.Count > 0)
                {
                    TreeListNode node = nhanVienTreeList.AppendNode(new object[] { item.TenBoPhan }, parent);
                    node.Tag = item;
                    node.HasChildren = true;
                }
            }

            nhanVienTreeList.EndUnboundLoad();
        }

        private void AddThinhGiang(TreeListNode parent, XPCollection<GiangVienThinhGiang> nvList)
        {
            nhanVienTreeList.BeginUnboundLoad();

            foreach (GiangVienThinhGiang item in nvList)
            {
                if (item.TinhTrang != null 
                    && item.TinhTrang.TenTinhTrang.ToLower().Contains("đang làm việc"))
                {
                    TreeListNode node = nhanVienTreeList.AppendNode(new object[] { item.HoTen }, parent);
                    node.Tag = item;
                }
            }

            nhanVienTreeList.EndUnboundLoad();
        }

        private void AddNode(TreeListNode parent, XPCollection<BoPhan> donViList)
        {
            donViTreeList.BeginUnboundLoad();

            foreach (BoPhan item in donViList)
            {
                TreeListNode node = null;
                if (parent == null || donViList.Count == 1)
                {
                    if (item.BoPhanCha == null || donViList.Count == 1)
                    {
                        node = donViTreeList.AppendNode(new object[] { item.TenBoPhan }, parent);
                        node.Tag = item;
                        if (item.ListBoPhanCon != null && item.ListBoPhanCon.Count > 0)
                        {
                            node.HasChildren = true;
                        }
                    }
                }
                else
                {
                    node = donViTreeList.AppendNode(new object[] { item.TenBoPhan }, parent);
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

        private void LayDanhSachDonVi()
        {
            filter = new XPCollection<BoPhan>(session, false);

            LayDanhSachDonVi(donViTreeList.Nodes);
        }

        private void LayDanhSachDonVi(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Checked)
                {
                    filter.Add((BoPhan)node.Tag);
                }
                else if (node.CheckState == CheckState.Unchecked)
                    continue;

                if (node.Nodes.Count > 0)
                    LayDanhSachDonVi(node.Nodes);
            }
        }

        public XPCollection<ChiTietTrichDanhSachThinhGiang> LayDanhSachThinhGiang()
        {
            trichList = new XPCollection<ChiTietTrichDanhSachThinhGiang>(session, false);
            LayDanhSachThinhGiang(nhanVienTreeList.Nodes);
            return trichList;
        }

        public XPCollection<GiangVienThinhGiang> LayDanhSachThinhGiang1()
        {
            nhanVienList = new XPCollection<GiangVienThinhGiang>(session, false);
            LayDanhSachThinhGiang1(nhanVienTreeList.Nodes);
            return nhanVienList;
        }

        private void LayDanhSachThinhGiang(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Tag is GiangVienThinhGiang && node.CheckState == CheckState.Checked)
                {
                    nv = session.GetObjectByKey<GiangVienThinhGiang>(((GiangVienThinhGiang)node.Tag).Oid);
                    if (nv != null)
                    {
                        ChiTietTrichDanhSachThinhGiang chiTiet = new ChiTietTrichDanhSachThinhGiang(session);
                        chiTiet.ThinhGiang = nv;
                        trichList.Add(chiTiet);
                    }
                }
                else if (node.CheckState == CheckState.Unchecked && node.Tag is GiangVienThinhGiang)
                {
                    continue;
                }

                LayDanhSachThinhGiang(node.Nodes);
            }
        }

        private void LayDanhSachThinhGiang1(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Tag is GiangVienThinhGiang && node.CheckState == CheckState.Checked)
                {
                    nv = session.GetObjectByKey<GiangVienThinhGiang>(((GiangVienThinhGiang)node.Tag).Oid);
                    if (nv != null)
                        nhanVienList.Add(nv);
                }
                else if (node.CheckState == CheckState.Unchecked && node.Tag is GiangVienThinhGiang)
                {
                    continue;
                }

                LayDanhSachThinhGiang1(node.Nodes);
            }
        }

        private void miCheckAll_Click(object sender, EventArgs e)
        {
            NodeState1(nhanVienTreeList.Nodes, CheckState.Checked);
        }

        private void miCheckSelected_Click(object sender, EventArgs e)
        {
            NodeState(nhanVienTreeList.Nodes, CheckState.Checked);
        }

        private void miUncheckAll_Click(object sender, EventArgs e)
        {
            NodeState1(nhanVienTreeList.Nodes, CheckState.Unchecked);
        }

        private void miUncheckSelected_Click(object sender, EventArgs e)
        {
            NodeState(nhanVienTreeList.Nodes, CheckState.Unchecked);
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}