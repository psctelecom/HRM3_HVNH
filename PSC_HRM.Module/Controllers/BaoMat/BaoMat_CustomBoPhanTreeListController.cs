using System;

using DevExpress.ExpressApp;
using System.Windows.Forms;
using DevExpress.XtraTreeList.Columns;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using System.Data;
using System.Data.SqlClient;


namespace PSC_HRM.Module.Controllers
{
    public partial class BaoMat_CustomBoPhanTreeListController : ViewController
    {
        private TreeList _treeList;
        private XPCollection<BoPhan> _boPhanList;
        private XPCollection<BoPhan> _searchBoPhanList;
        private PhanQuyenDonVi _phanQuyenDonVi;
        private string[] _bpDuocPhanQuyenList;
        TextBox _search;

        public BaoMat_CustomBoPhanTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BaoMat_CustomBoPhanTreeListController");
        }

        private void CustomBoPhanTreeListController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null && view.Id.Equals("PhanQuyenDonVi_DetailView"))
            {
                _phanQuyenDonVi = view.CurrentObject as PhanQuyenDonVi;
                //
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id.Equals("CustomControl"))
                    {
                        _treeList = item.Control as TreeList;

                        if (_treeList != null)
                        {
                            //Tên bộ phận
                            TreeListColumn colTenBoPhan = new TreeListColumn();
                            colTenBoPhan.Caption = "Tên bộ phận";
                            colTenBoPhan.Visible = true;
                            colTenBoPhan.VisibleIndex = 0;
                            //Mã quản lý
                            TreeListColumn colMaQuanLy = new TreeListColumn();
                            colMaQuanLy.Caption = "Mã quản lý";
                            colMaQuanLy.Visible = true;
                            colMaQuanLy.VisibleIndex = 1;
                            colMaQuanLy.Width = 50;
                            //
                            _treeList.Columns.AddRange(new TreeListColumn[] { colTenBoPhan, colMaQuanLy });

                            //Set cấu hình cơ bản của cây
                            TreeUtil.InitTreeView(_treeList);
                            //Vì ở đây chỉ xài riêng cho view này
                            _treeList.OptionsView.ShowCheckBoxes = true;

                            //Các sự kiện của cây
                            _treeList.BeforeCheckNode += treeList_BeforeCheckNode;
                            _treeList.AfterCheckNode += treeList_AfterCheckNode;

                            //Thêm dữ liệu vào node của cây
                            AddDataToNode();
                        }
                    }
                    if (item.Id.Equals("SearchTextBox"))
                    {
                        _search = item.Control as TextBox;
                        if (_search != null)
                        {
                            _search.KeyDown += KeyDown;
                        }
                    }
                }
            }
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _searchBoPhanList = BoPhanTimDuocList(_search.Text.Trim());
                //
                AddNodeOfTreeList(HamDungChung.ThongTinTruong(((XPObjectSpace)View.ObjectSpace).Session).Oid, true);
            }
        }
        private void AddDataToNode()
        {
            _boPhanList = new XPCollection<BoPhan>(((XPObjectSpace)View.ObjectSpace).Session,CriteriaOperator.Parse("NgungHoatDong=?",false));
            //
            AddNodeOfTreeList(HamDungChung.ThongTinTruong(((XPObjectSpace)View.ObjectSpace).Session).Oid, false);
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

        private void AddNodeOfTreeList(Guid idRoot, bool search)
        {
            //Danh sách phân quyền đơn vị
            if (_phanQuyenDonVi != null && !String.IsNullOrEmpty(_phanQuyenDonVi.Quyen))
            {
                _bpDuocPhanQuyenList = _phanQuyenDonVi.Quyen.Split(';');
            }

            //Xóa tất cả các node trên cây
            _treeList.Nodes.Clear();
            //
            _treeList.BeginUnboundLoad();
            //
            TreeListNode rootNode = null;

            //Lấy bộ phận root
            BoPhan boPhanRoot = (((XPObjectSpace)View.ObjectSpace).Session).FindObject<BoPhan>(CriteriaOperator.Parse("Oid=?", idRoot));

            if (boPhanRoot != null)
            {
                rootNode = _treeList.AppendNode(new object[] { boPhanRoot.TenBoPhan, boPhanRoot.MaQuanLy }, null);
                //Check vào các node đã phân quyền
                if (_bpDuocPhanQuyenList != null)
                {
                    foreach (string bp in _bpDuocPhanQuyenList)
                    {
                        if (boPhanRoot.Oid.ToString().Equals(bp))
                            rootNode.CheckState = CheckState.Checked;
                    }
                }
                //
                if (!search)
                    AddChildNode(rootNode, boPhanRoot, boPhanRoot.ListBoPhanCon,false);
                else
                    AddChildNode(rootNode, boPhanRoot, _searchBoPhanList,true);
                //
                rootNode.Tag = boPhanRoot;
            }
            //
            _treeList.EndUnboundLoad();
            _treeList.Refresh();

            // Bung tất cả các node ra
            _treeList.ExpandAll();

        }

        private void AddChildNode(TreeListNode rootNode, BoPhan boPhanCha, XPCollection<BoPhan> boPhanConList, bool search)
        {
            foreach (BoPhan item in boPhanConList)
            {
                if (item.BoPhanCha == boPhanCha && !item.NgungHoatDong)
                {
                    TreeListNode node = _treeList.AppendNode(new object[] { item.TenBoPhan, item.MaQuanLy }, rootNode);
                    //Check vào các node đã phân quyền
                    if (_bpDuocPhanQuyenList != null)
                    {
                        foreach (string bp in _bpDuocPhanQuyenList)
                        {
                            if (item.Oid.ToString().Equals(bp))
                                node.CheckState = CheckState.Checked;
                        }
                    }
                    //
                    if (!search)
                        AddChildNode(node, item, item.ListBoPhanCon, false);
                    else
                        AddChildNode(node, item, _searchBoPhanList, true);
                    //
                    node.Tag = item;
                }
            }
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
        private XPCollection<BoPhan> BoPhanTimDuocList(string tenBoPhan)
        {
            XPCollection<BoPhan> boPhanList = new XPCollection<BoPhan>((((XPObjectSpace)View.ObjectSpace).Session), false);
            //
            DataTable dt = new DataTable();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TenBoPhan", tenBoPhan);
            //param[1] = new SqlParameter("@Quyen", _phanQuyenDonVi.Quyen);

            SqlCommand cmd = DataProvider.GetCommand("spd_PhanQuyenDonVi_TimDanhSachDonViTheoTen", CommandType.StoredProcedure, param);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    BoPhan bp = (((XPObjectSpace)View.ObjectSpace).Session).FindObject<BoPhan>(CriteriaOperator.Parse("Oid=?", new Guid(item["Oid"].ToString())));
                    if (bp != null)
                        boPhanList.Add(bp);
                }
            }
            //
            return boPhanList;
        }
    }
}
