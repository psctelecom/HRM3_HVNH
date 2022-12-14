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
using PSC_HRM.Module.DinhBien;
using DevExpress.XtraBars;
using System.Drawing;

namespace PSC_HRM.Module.Controllers
{
    public partial class DinhBien_CustomDinhBienTreeListController : ViewController
    {
        private TreeList _treeList;
        private string[] _bpDuocPhanQuyenList;
        BoPhan _boPhanCurrent = null;
        DinhBienChucDanhCongViec _dinhBienChucDanhCongViecCurrent;
        bool _allowRefeshData = false;

        public DinhBien_CustomDinhBienTreeListController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DinhBien_CustomDinhBienTreeListController");
        }

        private void CustomDinhBienTreeListController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null && view.Id.Equals("DinhBienCongViec_DetailView"))
            {
                DinhBienCongViec dinhBienCongViec = view.CurrentObject as DinhBienCongViec;
                //
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id.Equals("DinhBienCongViec_TreeList"))
                    {
                        _treeList = item.Control as TreeList;

                        if (_treeList != null)
                        {
                            //Tên bộ phận
                            TreeListColumn colTenBoPhan = new TreeListColumn();
                            colTenBoPhan.Caption = "Tên bộ phận";
                            colTenBoPhan.Visible = true;
                            colTenBoPhan.VisibleIndex = 0;
                            colTenBoPhan.OptionsColumn.AllowEdit = false;

                            //
                            _treeList.Columns.AddRange(new TreeListColumn[] { colTenBoPhan });

                            //Set cấu hình cơ bản của cây
                            //TreeUtil.InitTreeView(_treeList);

                            //Thêm dữ liệu vào node của cây
                            AddDataToNode();

                            //Các sự kiện của cây
                            _treeList.MouseClick += TreeList_MouseClick;
                            _treeList.AfterFocusNode += TreeList_AfterFocusNode;
                            _treeList.NodeCellStyle += TreeList_NodeCellStyle;
                            _treeList.DoubleClick += TreeList_DoubleClick;
                            _treeList.MouseUp += TreeList_MouseUp;
                        }
                    }

                }
            }
        }
        private void TreeList_MouseUp(object sender, EventArgs e)
        {
            ////
            //if (_allowRefeshData)
            //AddDataToNode();
        }
        private void TreeList_DoubleClick(object sender, EventArgs e)
        {
            if (_dinhBienChucDanhCongViecCurrent != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                DinhBienChucDanhCongViec dinhBien = (((XPObjectSpace)obs).Session).GetObjectByKey<DinhBienChucDanhCongViec>(_dinhBienChucDanhCongViecCurrent.Oid);
                //
                ShowViewParameters showView = new ShowViewParameters();
                showView.CreatedView = Application.CreateDetailView(obs, dinhBien);
                showView.TargetWindow = TargetWindow.NewModalWindow;
                showView.Context = TemplateContext.View;
                showView.CreateAllControllers = true;
                //
                Application.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
            }
        }
        private void TreeList_NodeCellStyle(object sender, DevExpress.XtraTreeList.GetCustomNodeCellStyleEventArgs e)
        {
            BoPhan obj = (e.Node.Tag) as BoPhan;

            //tô màu tree cho dễ nhìn
            if (obj == null)
            {
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Appearance.ForeColor = Color.Brown;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else
            {
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Appearance.ForeColor = Color.Teal;
            }
        }

        private void TreeList_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            //
            if (_allowRefeshData)
            {
                AddDataToNode();
                //
                _allowRefeshData = false;
            }
            _boPhanCurrent = null;
            _boPhanCurrent = (e.Node.Tag) as BoPhan;
            _dinhBienChucDanhCongViecCurrent = (e.Node.Tag) as DinhBienChucDanhCongViec;
        }
        private void TreeList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                    // Thêm mới định biên
                    ToolStripMenuItem btnThemDinhBien = new ToolStripMenuItem();
                    btnThemDinhBien.Name = "btnThemDinhBien";
                    btnThemDinhBien.Size = new System.Drawing.Size(80, 22);
                    btnThemDinhBien.Text = "Thêm";
                    btnThemDinhBien.Click += new System.EventHandler(this.btnThemDinhBien_Click);
                    //Xóa định biên
                    ToolStripMenuItem btnXoaDinhBien = new ToolStripMenuItem();
                    btnXoaDinhBien.Name = "btnXoaDinhBien";
                    btnXoaDinhBien.Size = new System.Drawing.Size(80, 22);
                    btnXoaDinhBien.Text = "Xóa";
                    btnXoaDinhBien.Click += new System.EventHandler(this.btnXoaDinhBien_Click);
                    //Refesh lại định biên
                    ToolStripMenuItem btnRefeshDinhBien = new ToolStripMenuItem();
                    btnRefeshDinhBien.Name = "btnRefeshDinhBien";
                    btnRefeshDinhBien.Size = new System.Drawing.Size(80, 22);
                    btnRefeshDinhBien.Text = "Làm mới";
                    btnRefeshDinhBien.Click += new System.EventHandler(this.btnRefeshDinhBien_Click);

                    ContextMenuStrip cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
                    cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnThemDinhBien,btnXoaDinhBien,btnRefeshDinhBien });
                    cmsMenu.Name = "cmsMenu";
                    cmsMenu.Size = new System.Drawing.Size(119, 48);
                    //
                     _treeList.ContextMenuStrip = cmsMenu;
            }
        }
        private void btnThemDinhBien_Click(object sender, EventArgs e)
        {
            if (_boPhanCurrent != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                DinhBienChucDanhCongViec dinhBien = new DinhBienChucDanhCongViec((((XPObjectSpace)obs).Session));
                dinhBien.BoPhan = (((XPObjectSpace)obs).Session).GetObjectByKey<BoPhan>(_boPhanCurrent.Oid);
                //
                ShowViewParameters showView = new ShowViewParameters();
                showView.CreatedView = Application.CreateDetailView(obs, dinhBien);
                showView.TargetWindow = TargetWindow.NewModalWindow;
                showView.Context = TemplateContext.View;
                showView.CreateAllControllers = true;
                //
                Application.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
                //
                _allowRefeshData = true;
            }
            
        }
        private void btnRefeshDinhBien_Click(object sender, EventArgs e)
        {
            //
            AddDataToNode();
        }
        private void btnXoaDinhBien_Click(object sender, EventArgs e)
        {
             if (_dinhBienChucDanhCongViecCurrent != null)
            {
                if (DialogUtil.ShowYesNo("Bạn thật sự muốn xóa định biên [" + _dinhBienChucDanhCongViecCurrent.ChucDanhCongViec.TenCongViec + "]?") == DialogResult.Yes)
                {
                    _dinhBienChucDanhCongViecCurrent.Delete();

                    //Lưu dữ liệu
                    View.ObjectSpace.CommitChanges();
                    //Refesh dữ liệu 
                    AddDataToNode();
                }
            }
        }
        private void AddDataToNode()
        {
            //Lấy danh sách bộ phận được phân quyền
            _bpDuocPhanQuyenList = HamDungChung.GetPhanQuyenBoPhan().Split(';');
            //
            AddNodeOfTreeList(HamDungChung.ThongTinTruong(((XPObjectSpace)View.ObjectSpace).Session).Oid, false);
        }

        private void AddNodeOfTreeList(Guid idRoot, bool search)
        {
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

                //
                AddChildNode(rootNode, boPhanRoot, boPhanRoot.ListBoPhanCon, false);
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

                    //Kiểm tra phân quyền
                    if (_bpDuocPhanQuyenList != null)
                    {
                        foreach (string bp in _bpDuocPhanQuyenList)
                        {
                            if (item.Oid.ToString().Equals(bp))
                            {
                                AddChildNode(node, item, item.ListBoPhanCon, false);
                                if (item.ListBoPhanCon != null && item.ListBoPhanCon.Count == 0)
                                {
                                    XPCollection<DinhBienChucDanhCongViec> dinhBienChucDanhCongViecList = new XPCollection<DinhBienChucDanhCongViec>((((XPObjectSpace)View.ObjectSpace).Session),CriteriaOperator.Parse("BoPhan=?", item.Oid));
                                    //
                                    foreach (DinhBienChucDanhCongViec itemChucDanhCongViec in dinhBienChucDanhCongViecList)
                                    {
                                        TreeListNode nodeChucDanhCongViec = _treeList.AppendNode(new object[] { itemChucDanhCongViec.ChucDanhCongViec.TenCongViec, itemChucDanhCongViec.ChucDanhCongViec.MaQuanLy }, node);
                                        //
                                        nodeChucDanhCongViec.Tag = itemChucDanhCongViec;
                                    }
                                }
                            }
                        }
                    }
                    //
                    node.Tag = item;
                }
            }
        }
    }
}
