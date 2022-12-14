using System;

using DevExpress.ExpressApp;
using PSC_HRM.Module.Report;
using DevExpress.Xpo;
using DevExpress.XtraTreeList.Columns;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraTreeList.Nodes;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using System.Data;
using System.Data.SqlClient;


namespace PSC_HRM.Module.Controllers
{
    public partial class BaoMat_CustomBaoCaoTreeListController : ViewController
    {
        TreeList _treeList;
        XPCollection<GroupReport> _groupReportList;
        PhanQuyenBaoCao _phanQuyenBaoCao;
        string[] _baoCaoDaPhanQuyenList;
        bool _checkGroup = false;
        TextBox _searchTextBox;
        bool _search = false;

        public BaoMat_CustomBaoCaoTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BaoMat_CustomBaoCaoTreeListController");
        }

        private void CustomBaoCaoTreeListController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null && view.Id.Equals("PhanQuyenBaoCao_DetailView"))
            {
                _phanQuyenBaoCao = view.CurrentObject as PhanQuyenBaoCao;
                //
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id == "CustomControl")
                    {
                        _treeList = item.Control as TreeList;

                        if (_treeList != null)
                        {
                            TreeListColumn colTenBaoCao = new TreeListColumn();
                            colTenBaoCao.Caption = "Tên báo cáo";
                            colTenBaoCao.Visible = true;
                            colTenBaoCao.VisibleIndex = 0;
                            _treeList.Columns.AddRange(new TreeListColumn[] { colTenBaoCao });

                            //Set cấu hình cơ bản của cây
                            TreeUtil.InitTreeView(_treeList);
                            //Vì ở đây chỉ xài riêng cho view này
                            _treeList.OptionsView.ShowCheckBoxes = true;

                            //Các sự kiện của cây
                            _treeList.BeforeCheckNode += treeList_BeforeCheckNode;
                            _treeList.AfterCheckNode += treeList_AfterCheckNode;


                            //Thêm dữ liệu vào từng node của cây
                            AddDataToNode();
                        }
                    }
                    if (item.Id.Equals("SearchTextBox"))
                    {
                        _searchTextBox = item.Control as TextBox;
                        if (_searchTextBox != null)
                        {
                            _searchTextBox.KeyDown += KeyDown;
                        }
                    }
                }
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //
                _search = true;
                //
                AddDataToNode();
            }
        }

        private void AddDataToNode()
        {
            //Lấy danh sách các nhóm báo cáo
            _groupReportList = new XPCollection<GroupReport>(((XPObjectSpace)View.ObjectSpace).Session);
            //
            AddNodeOfTreeList();
        }

        private void AddNodeOfTreeList()
        {
            //Danh sach phân quyền
            if (_phanQuyenBaoCao != null && !String.IsNullOrEmpty(_phanQuyenBaoCao.Quyen))
            {
                _baoCaoDaPhanQuyenList = _phanQuyenBaoCao.Quyen.Split(';');
            }

            //Xóa tất cả các node có sẵn
            _treeList.Nodes.Clear();
            //
            _treeList.BeginUnboundLoad();

            //Khởi tạo node root
            TreeListNode rootNode = _treeList.AppendNode(new object[] { "Tất cả báo cáo" }, null);

            foreach (GroupReport item in _groupReportList)
            {
                _checkGroup = false;
                XPCollection<HRMReport> reportList = null;

                //Danh sách báo cáo theo nhóm
                if (!_search || string.IsNullOrEmpty(_searchTextBox.Text.Trim()))
                {
                    reportList = new XPCollection<HRMReport>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("NhomBaoCao=?", item.Oid));
                }
                else
                {
                    string bien = "%" + _searchTextBox.Text.Trim() + "%";

                    CriteriaOperator criteria = CriteriaOperator.Parse("NhomBaoCao.Oid =? And Name like ? ", item.Oid, bien);
                    reportList = new XPCollection<HRMReport>(((XPObjectSpace)View.ObjectSpace).Session, criteria);
                }

                if (reportList != null && reportList.Count > 0)
                {
                    TreeListNode node = _treeList.AppendNode(new object[] { item.TenNhom }, rootNode);

                    //Thêm báo cáo vào cây
                    AddChildNode(node, reportList);

                    //Check vào group
                    if (_checkGroup)
                    {
                        node.CheckState = CheckState.Checked;
                    }

                    //
                    node.Tag = item;
                }
            }

            _treeList.EndUnboundLoad();

            //Bung tất cả các node ra
            _treeList.ExpandAll();


        }

        private void AddChildNode(TreeListNode rootNode, XPCollection<HRMReport> danhSachBaoCaoList)
        {
            foreach (HRMReport item in danhSachBaoCaoList)
            {
                TreeListNode node = _treeList.AppendNode(new object[] { item.ReportName }, rootNode);

                //Check vào các node đã phân quyền
                if (_baoCaoDaPhanQuyenList != null)
                {
                    foreach (string bp in _baoCaoDaPhanQuyenList)
                    {
                        if (item.Oid.ToString() == bp)
                        {
                            node.CheckState = CheckState.Checked;
                            //
                            _checkGroup = true;
                        }
                    }
                }
                //
                node.Tag = item;
            }
        }

        private void treeList_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }

        private void treeList_AfterCheckNode(object sender, NodeEventArgs e)
        {
            //
            SetCheckedNode(e.Node, e.Node.CheckState);
        }

        private void SetCheckedNode(TreeListNode node, CheckState check)
        {
            //Checked cả các node cha
            if (node.ParentNode != null && check == CheckState.Checked)
                CheckParentNode(node, check);

            //Checked or UnChecked tất các các node con
            CheckChildNode(node, check);
        }

        private void CheckParentNode(TreeListNode node, CheckState check)
        {
            node.ParentNode.CheckState = check;
            //Check vào node root
            foreach (TreeListNode item in _treeList.Nodes)
            {
                if (item.ParentNode == null)
                    item.CheckState = CheckState.Checked;
            }
        }

        private static void CheckChildNode(TreeListNode node, CheckState check)
        {

            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                CheckChildNode(node.Nodes[i], check);
            }
        }
    }
}
