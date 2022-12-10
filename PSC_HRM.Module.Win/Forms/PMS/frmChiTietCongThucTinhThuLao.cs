using System;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Model;
using System.Reflection;
using DevExpress.Xpo;

namespace PSC_HRM.Module.Win.Editors
{
    public partial class frmChiTietCongThucTinhThuLao : XtraForm
    {
        internal IModelBOModel BONode;
        internal string ObjectType;

        public frmChiTietCongThucTinhThuLao()
        {
            InitializeComponent();
        }

        private void frmFormula_Load(object sender, EventArgs e)
        {
            AddNode(ObjectType, null);
        }

        private void AddNode(string typeName, TreeListNode parent)
        {
            trFields.BeginUnboundLoad();
            foreach (var obj in BONode)
            {
                if (obj.Name == typeName)
                {
                    PropertyInfo[] pi = obj.TypeInfo.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    object[] attributes;
                    foreach (PropertyInfo f in pi)
                    {
                        if (
                            f.PropertyType.FullName == "System.Decimal"
                            || f.PropertyType.FullName == "System.Double"
                            || f.PropertyType.FullName == "System.Single"
                            || f.PropertyType.FullName == "System.Int16"
                            || f.PropertyType.FullName == "System.Int32"
                            || f.PropertyType.FullName == "System.Int64"
                            || f.PropertyType.FullName.Contains("ThongTinBangChot")                            
                            || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS")
                            || f.PropertyType.FullName.Contains("DanhSachDonGiaThanhToan")
                            || f.PropertyType.FullName.Contains("NhanVien_GioGiang")
                            || f.PropertyType.FullName.Contains("Temp_TinhThuLao")
                            || f.PropertyType.FullName.Contains("HeSo")
                            || f.PropertyType.FullName.Contains("BangThanhToanGio")
                            )

                        {
                            TreeListNode node = null;
                            attributes = f.GetCustomAttributes(typeof(ModelDefaultAttribute), false);
                            if (attributes != null)
                            {
                                foreach (object att in attributes)
                                {
                                    ModelDefaultAttribute ca = (ModelDefaultAttribute)att;
                                    if (ca != null && ca.PropertyName == "Caption")
                                        node = trFields.AppendNode(new object[] { ca.PropertyValue, String.Format("[{0}]", f.Name) }, parent);
                                }

                                if (node == null)
                                    node = trFields.AppendNode(new object[] { f.Name, String.Format("[{0}]", f.Name) }, parent);
                            }
                            else
                                node = trFields.AppendNode(new object[] { f.Name, String.Format("[{0}]", f.Name) }, parent);
                            node.Tag = f.PropertyType.FullName;
                            node.HasChildren = f.PropertyType.FullName.Contains("ThongTinBangChot")
                                                || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS")
                                                || f.PropertyType.FullName.Contains("DanhSachDonGiaThanhToan")
                                                || f.PropertyType.FullName.Contains("NhanVien_GioGiang")
                                                || f.PropertyType.FullName.Contains("Temp_TinhThuLao")
                                                || f.PropertyType.FullName.Contains("BangThanhToanGio")
                                                || f.PropertyType.FullName.Contains("HeSo");
                        }
                    }
                }
            }
            trFields.EndUnboundLoad();
        }

        private void trFields_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            //chỉ add node lần đầu tiên, nhưng lần sau không add nữa
            if (e.Node.Nodes.Count == 0)
                AddNode(e.Node.Tag.ToString(), e.Node);
        }

        public string GetCurrentField()
        {
            string s = string.Empty;
            if (trFields.FocusedNode != null)
            {
                TreeListNode node = trFields.FocusedNode;
                s = node.GetDisplayText("HienThi");
            }
            return s;
        }

        private void trFields_DoubleClick(object sender, EventArgs e)
        {
            btnOK.PerformClick();
        }
    }
}