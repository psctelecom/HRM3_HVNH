using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmChonCanBoLapQuyetDinhDaoTao : XtraForm
    {
        private XPCollection<BoPhan> _boPhanDuocPhanQuyenList;
        private XPCollection<BoPhan> _boPhanTimDuocList;
        private XPCollection<ThongTinNhanVien> _nhanVienList;
        private XPCollection<TinhTrang> tinhtrangList;
        private ThongTinNhanVien _thongTinNhanVien;
        private Guid _tinhTrang;
        private Session _session;

        public frmChonCanBoLapQuyetDinhDaoTao()
        {
            InitializeComponent();
        }

        public frmChonCanBoLapQuyetDinhDaoTao(Session session)
        {
            InitializeComponent();
            this._session = session;
        }

        private void frmChonCanBoLapQuyetDinhDaoTao_Load(object sender, EventArgs e)
        {
            if (!HamDungChung.CheckAdministrator())
            {
                _boPhanDuocPhanQuyenList = new XPCollection<BoPhan>(_session, new InOperator("Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(_session)));
            }
            else
            {
                _boPhanDuocPhanQuyenList = new XPCollection<BoPhan>(_session);
            }

            //
            AddDataNodeOfTreeList(HamDungChung.ThongTinTruong(_session).Oid);
            //
            tinhtrangList = new XPCollection<TinhTrang>(_session);
            tinhtrangList.Criteria = CriteriaOperator.Parse("KhongConCongTacTaiTruong=false");
            tinhTranglke.Properties.DataSource = tinhtrangList;
            tinhTranglke.Properties.ValueMember = "Oid";
            tinhTranglke.Properties.DisplayMember = "TenTinhTrang";
            tinhTranglke.Properties.PopulateColumns();
            tinhTranglke.Properties.Columns["LoaiTinhTrang"].Visible = false;
            tinhTranglke.Properties.Columns["KhongConCongTacTaiTruong"].Visible = false;
            tinhTranglke.Properties.Columns["KhongTinhTNTT"].Visible = false;
            tinhTranglke.Properties.Columns["MaQuanLy"].Caption = "Mã quản lý";
            tinhTranglke.Properties.Columns["TenTinhTrang"].Caption = "Tên tình trạng";
            tinhTranglke.Properties.Columns["PhanTramHuongLuong"].Caption = "Phần trăn hưởng lương";
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
            AddDonVi(null, _boPhanTimDuocList);
            nhanVienTreeList.ExpandAll();
        }

        private void nhanVienTreeList_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            if (e.Node.Tag is BoPhan)
            {
                e.Node.Nodes.Clear();
                XPCollection<ThongTinNhanVien> nhanVienList = new XPCollection<ThongTinNhanVien>(_session, CriteriaOperator.Parse("BoPhan=?", ((BoPhan)e.Node.Tag).Oid));
                AddNhanVien(e.Node, nhanVienList);
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

        private void AddNhanVien(TreeListNode parent, XPCollection<ThongTinNhanVien> nvList)
        {
            nhanVienTreeList.BeginUnboundLoad();

            foreach (ThongTinNhanVien item in nvList)
            {
                if (item.TinhTrang != null
                    && (item.TinhTrang.KhongConCongTacTaiTruong == false))
                {
                    TreeListNode node = nhanVienTreeList.AppendNode(new object[] { item.HoTen }, parent);
                    node.Tag = item;
                }
            }

            nhanVienTreeList.EndUnboundLoad();
        }

        private void AddDataNodeOfTreeList(Guid idRoot)
        {
            donViTreeList.BeginUnboundLoad();
            //
            TreeListNode rootNode = null;

            //Lấy bộ phận root
            BoPhan boPhanRoot = _session.FindObject<BoPhan>(CriteriaOperator.Parse("Oid=?", idRoot));
            //
            if (boPhanRoot != null)
            {
                rootNode = donViTreeList.AppendNode(new object[] { boPhanRoot.TenBoPhan, boPhanRoot.MaQuanLy }, null);

                //Thêm các bộ phận con vào cây
                AddChildNode(rootNode, boPhanRoot, boPhanRoot.ListBoPhanCon);
            }
            //
            donViTreeList.EndUnboundLoad();
            //
            donViTreeList.ExpandAll();
        }
        private void AddChildNode(TreeListNode rootNode, BoPhan boPhanCha, XPCollection<BoPhan> boPhanConList)
        {
            foreach (BoPhan item in boPhanConList)
            {
                if (item.BoPhanCha == boPhanCha && !item.NgungHoatDong)
                {
                    //Duyệt qua các bộ phận được phân quyền
                    foreach (BoPhan bp in _boPhanDuocPhanQuyenList)
                    {
                        if (item.Oid == bp.Oid)
                        {
                            TreeListNode node = donViTreeList.AppendNode(new object[] { item.TenBoPhan, item.MaQuanLy }, rootNode);
                            //Gọi đệ qui
                            AddChildNode(node, item, item.ListBoPhanCon);
                            //
                            node.Tag = item;
                        }
                    }
                }
            }
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
            _boPhanTimDuocList = new XPCollection<BoPhan>(_session, false);
            //
            LayDanhSachDonVi(donViTreeList.Nodes);
        }

        private void LayDanhSachDonVi(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Checked)
                {
                    _boPhanTimDuocList.Add((BoPhan)node.Tag);
                }
                else if (node.CheckState == CheckState.Unchecked)
                    continue;

                if (node.Nodes.Count > 0)
                    LayDanhSachDonVi(node.Nodes);
            }
        }

        public XPCollection<ThongTinNhanVien> LayDanhSachNhanVien()
        {
            _nhanVienList = new XPCollection<ThongTinNhanVien>(_session, false);
            LayDanhSachNhanVien(nhanVienTreeList.Nodes);
            return _nhanVienList;
        }

        public Guid LayTinhTrang()
        {
            if (tinhTranglke.EditValue != null)
            {
                _tinhTrang = (Guid)tinhTranglke.EditValue;
                return _tinhTrang;
            }
            else
                return Guid.Empty;
        }

        private void LayDanhSachNhanVien(TreeListNodes nodes)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node.Tag is ThongTinNhanVien && node.CheckState == CheckState.Checked)
                {
                    _thongTinNhanVien = _session.GetObjectByKey<ThongTinNhanVien>(((ThongTinNhanVien)node.Tag).Oid);
                    if (_thongTinNhanVien != null)
                        _nhanVienList.Add(_thongTinNhanVien);
                }
                else if (node.CheckState == CheckState.Unchecked && node.Tag is ThongTinNhanVien)
                {
                    continue;
                }

                LayDanhSachNhanVien(node.Nodes);
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