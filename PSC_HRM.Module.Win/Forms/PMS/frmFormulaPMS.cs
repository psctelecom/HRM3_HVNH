using System;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Model;
using System.Reflection;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Win.Editors
{
    public partial class frmFormulaPMS : XtraForm
    {
        internal IModelBOModel BONode;
        internal string ObjectType;
        int Ktra = 0;

        public frmFormulaPMS()
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
            string MaTruong = HamDungChung.CurrentUser().ThongTinTruong.TenVietTat;
            foreach (var obj in BONode)
            {
                if (obj.Name == typeName)
                {
                    PropertyInfo[] pi = obj.TypeInfo.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    object[] attributes;
                    if (typeName == "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS" || Ktra == 1)
                    #region CauHinhTinhThuLaoGiangDay
                    {
                        #region HUFFLIT
                        if (MaTruong == "HUFLIT")
                        {
                            foreach (PropertyInfo f in pi)
                            {
                                if ((f.PropertyType.FullName == "System.Decimal"
                                    || f.PropertyType.FullName == "System.Double"
                                    || f.PropertyType.FullName == "System.Single"
                                    || f.PropertyType.FullName == "System.Int16"
                                    || f.PropertyType.FullName == "System.Int32"
                                    || f.PropertyType.FullName == "System.Int64"
                                    || f.PropertyType.FullName == "System.String"
                                    || f.PropertyType.FullName.Contains("Enum")
                                    || f.PropertyType.FullName.Contains("ChiTietChotThuLao")
                                    //|| f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_Moi")
                                    ))
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
                                    node.HasChildren = f.PropertyType.FullName.Contains("ChiTietChotThuLao")
                                        //|| f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_Moi")
                                        ;
                                }
                            }
                        }
                        #endregion
                        #region NEU
                        else if (TruongConfig.MaTruong == "NEU")
                        {
                            #region ThoiKhoaBieu
                            foreach (PropertyInfo f in pi)
                            {
                                if ((f.PropertyType.FullName == "System.Decimal"
                                    || f.PropertyType.FullName == "System.Double"
                                    || f.PropertyType.FullName == "System.Single"
                                    || f.PropertyType.FullName == "System.Int16"
                                    || f.PropertyType.FullName == "System.Int32"
                                    || f.PropertyType.FullName == "System.Int64"
                                    || f.PropertyType.FullName.Contains("ChiTiet_ThoiKhoaBieu")
                                    || f.PropertyType.FullName.Contains("ChiTietThongTinChungPMS")
                                    || f.PropertyType.FullName.Contains("ThongTinBangChotThuLao")
                                    )
                                    && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
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
                                    node.HasChildren = f.PropertyType.FullName.Contains("ChiTiet_ThoiKhoaBieu")
                                         || f.PropertyType.FullName.Contains("ChiTietThongTinChungPMS")
                                         || f.PropertyType.FullName.Contains("ThongTinBangChotThuLao")
                                        ;
                                }
                            }
                            #endregion
                        }
                        #endregion
                        #region DH Đồng Nai
                        else if (MaTruong == "DNU")
                        {
                            foreach (PropertyInfo f in pi)
                            {
                                if ((f.PropertyType.FullName == "System.Decimal"
                                    || f.PropertyType.FullName == "System.Double"
                                    || f.PropertyType.FullName == "System.Single"
                                    || f.PropertyType.FullName == "System.Int16"
                                    || f.PropertyType.FullName == "System.Int32"
                                    || f.PropertyType.FullName == "System.Int64"
                                    || f.PropertyType.FullName.Contains("ThongTinBangChot")
                                    || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay")
                                    )
                                    && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
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
                                                        || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay")
                                        ;
                                }
                            }
                        }
                        #endregion
                        #region VHU
                        else if (MaTruong == "VHU")
                        {
                            foreach (PropertyInfo f in pi)
                            {
                                if ((f.PropertyType.FullName == "System.Decimal"
                                    || f.PropertyType.FullName == "System.Double"
                                    || f.PropertyType.FullName == "System.Single"
                                    || f.PropertyType.FullName == "System.Int16"
                                    || f.PropertyType.FullName == "System.Int32"
                                    || f.PropertyType.FullName == "System.Int64"
                                    || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_Moi")
                                    || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS")
                                    )
                                    && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
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
                                    node.HasChildren = f.PropertyType.FullName.Contains("HeSoChucDanh")
                                        || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_Moi")
                                        || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS")
                                        ;
                                }
                            }
                        }
                        #endregion                       
                        #region Các trường khác
                        else
                        {
                            #region
                            foreach (PropertyInfo f in pi)
                            {
                                if ((f.PropertyType.FullName == "System.Decimal"
                                    || f.PropertyType.FullName == "System.Double"
                                    || f.PropertyType.FullName == "System.Single"
                                    || f.PropertyType.FullName == "System.Int16"
                                    || f.PropertyType.FullName == "System.Int32"
                                    || f.PropertyType.FullName == "System.Int64"
                                    || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay")
                                    || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS")
                                    || f.PropertyType.FullName.Contains("ChiTietKeKhaiSauGiang")
                                    || f.PropertyType.FullName.Contains("ThongTinBangChot")
                                    ////|| f.PropertyType.FullName.Contains("HeSo")
                                    //|| f.PropertyType.FullName.Contains("SauDaiHoc")//SauDaiHoc
                                    )
                                    && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
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
                                    node.HasChildren = f.PropertyType.FullName.Contains("HeSoChucDanh")
                                        || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay")
                                        || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS")
                                        || f.PropertyType.FullName.Contains("ChiTietKeKhaiSauGiang")
                                        || f.PropertyType.FullName.Contains("ThongTinBangChot")
                                        //|| f.PropertyType.FullName.Contains("HeSo")
                                        //|| f.PropertyType.FullName.Contains("SauDaiHoc")
                                        ;
                                }
                            }
                            #endregion
                        }
                        #region Không biết đoạn này sử dụng làm gì
                        //if (MaTruong == "HUFLIT")
                        //{
                        //    #region "HUFLIT"
                        //    foreach (PropertyInfo f in pi)
                        //    {
                        //        if ((f.PropertyType.FullName == "System.Decimal"
                        //            || f.PropertyType.FullName == "System.Double"
                        //            || f.PropertyType.FullName == "System.Single"
                        //            || f.PropertyType.FullName == "System.Int16"
                        //            || f.PropertyType.FullName == "System.Int32"
                        //            || f.PropertyType.FullName == "System.Int64"
                        //            || f.PropertyType.FullName == "System.String"
                        //            || f.PropertyType.FullName.Contains("Enum")
                        //            || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_Moi")
                        //            )
                        //            && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
                        //        {
                        //            TreeListNode node = null;
                        //            attributes = f.GetCustomAttributes(typeof(ModelDefaultAttribute), false);
                        //            if (attributes != null)
                        //            {
                        //                foreach (object att in attributes)
                        //                {
                        //                    ModelDefaultAttribute ca = (ModelDefaultAttribute)att;
                        //                    if (ca != null && ca.PropertyName == "Caption")
                        //                        node = trFields.AppendNode(new object[] { ca.PropertyValue, String.Format("[{0}]", f.Name) }, parent);
                        //                }

                        //                if (node == null)
                        //                    node = trFields.AppendNode(new object[] { f.Name, String.Format("[{0}]", f.Name) }, parent);
                        //            }
                        //            else
                        //                node = trFields.AppendNode(new object[] { f.Name, String.Format("[{0}]", f.Name) }, parent);
                        //            node.Tag = f.PropertyType.FullName;
                        //            node.HasChildren = f.PropertyType.FullName.Contains("HeSoChucDanh")
                        //                || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_Moi")
                        //                ;
                        //        }
                        //    }
                        //    #endregion
                        //}                              
                        #endregion
                    }
                    #endregion
                    #endregion
                    else
                        if (typeName == "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_NgoaiGiangDay")
                    #region ChonGiaTriLapCongThucPMS_NgoaiGiangDay
                    {
                        foreach (PropertyInfo f in pi)
                        {
                            //if ((f.PropertyType.FullName == "System.Decimal"
                            //    || f.PropertyType.FullName == "System.Double"
                            //    || f.PropertyType.FullName == "System.Single"
                            //    || f.PropertyType.FullName == "System.Int16"
                            //    || f.PropertyType.FullName == "System.Int32"
                            //    || f.PropertyType.FullName == "System.Int64"
                            //    || f.PropertyType.FullName == ("ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu")
                            //    )
                            //    || !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
                            if (f.PropertyType.FullName.Contains("ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu"))
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
                                node.HasChildren = f.PropertyType.FullName.Contains("ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu");
                            }
                        }
                    }
                    #endregion
                    else if (typeName == "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_TaiChinh")
                    {
                        #region ChonGiaTriLapCongThucPMS_TaiChinh
                        foreach (PropertyInfo f in pi)
                        {
                            if ((f.PropertyType.FullName == "System.Decimal"
                                || f.PropertyType.FullName == "System.Double"
                                || f.PropertyType.FullName == "System.Single"
                                || f.PropertyType.FullName == "System.Int16"
                                || f.PropertyType.FullName == "System.Int32"
                                || f.PropertyType.FullName == "System.Int64"
                                || f.PropertyType.FullName.Contains("ThanhToanVuotDinhMuc")
                                || f.PropertyType.FullName.Contains("ThongTinBangChot")
                                || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS_TaiChinh")
                                )
                                && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
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
                                node.HasChildren = f.PropertyType.FullName.Contains("ThanhToanVuotDinhMuc")
                                                || f.PropertyType.FullName.Contains("ThongTinBangChot")
                                                || f.PropertyType.FullName.Contains("CauHinhQuyDoiPMS_TaiChinh");
                            }
                        }
                        #endregion
                    }
                    else if (typeName == "PSC_HRM.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS_UEL")
                    #region CauHinhTinhThuLaoGiangDay
                    {
                        if (MaTruong == "UEL")
                        {
                            foreach (PropertyInfo f in pi)
                            {
                                if ((f.PropertyType.FullName == "System.Decimal"
                                    || f.PropertyType.FullName == "System.Double"
                                    || f.PropertyType.FullName == "System.Single"
                                    || f.PropertyType.FullName == "System.Int16"
                                    || f.PropertyType.FullName == "System.Int32"
                                    || f.PropertyType.FullName == "System.Int64"
                                    || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_UEL")
                                    || f.PropertyType.FullName.Contains("CotChonChauHinhQiuDoi")
                                    ))
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
                                    node.HasChildren = f.PropertyType.FullName.Contains("HeSoChucDanh")
                                        || f.PropertyType.FullName.Contains("ChiTietKhoiLuongGiangDay_UEL")
                                        || f.PropertyType.FullName.Contains("CotChonChauHinhQiuDoi")
                                        ;
                                }
                            }
                        }
                    }
                        #endregion
                        #region ChonGiaTriLapCongThucPMS_NgoaiGiangDay(tạm tắt)
                        //if (typeName.Contains("ChonGiaTriLapCongThucPMS_NgoaiGiangDay"))
                        //{
                        //    foreach (PropertyInfo f in pi)
                        //    {
                        //        if ((f.PropertyType.FullName == "System.Decimal"
                        //            || f.PropertyType.FullName == "System.Double"
                        //            || f.PropertyType.FullName == "System.Single"
                        //            || f.PropertyType.FullName == "System.Int16"
                        //            || f.PropertyType.FullName == "System.Int32"
                        //            || f.PropertyType.FullName == "System.Int64"
                        //            || f.PropertyType.FullName.Contains("ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu")
                        //            )
                        //            && !f.PropertyType.FullName.Contains("BangChotThongTinTinhLuong"))
                        //        {
                        //            TreeListNode node = null;
                        //            attributes = f.GetCustomAttributes(typeof(ModelDefaultAttribute), false);
                        //            if (attributes != null)
                        //            {
                        //                foreach (object att in attributes)
                        //                {
                        //                    ModelDefaultAttribute ca = (ModelDefaultAttribute)att;
                        //                    if (ca != null && ca.PropertyName == "Caption")
                        //                        node = trFields.AppendNode(new object[] { ca.PropertyValue, String.Format("[{0}]", f.Name) }, parent);
                        //                }

                        //                if (node == null)
                        //                    node = trFields.AppendNode(new object[] { f.Name, String.Format("[{0}]", f.Name) }, parent);
                        //            }
                        //            else
                        //                node = trFields.AppendNode(new object[] { f.Name, String.Format("[{0}]", f.Name) }, parent);
                        //            node.Tag = f.PropertyType.FullName;
                        //            node.HasChildren = f.PropertyType.FullName.Contains("ChiTietKeKhai_CacHoatDong_ThoiKhoaBieu");
                        //        }
                        //    }

                        //}
                        #endregion

                    }
                }
            trFields.EndUnboundLoad();
        }

        private void trFields_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            //chỉ add node lần đầu tiên, nhưng lần sau không add nữa
            if (e.Node.Nodes.Count == 0)
            {
                Ktra = 1;
                AddNode(e.Node.Tag.ToString(), e.Node);
            }
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