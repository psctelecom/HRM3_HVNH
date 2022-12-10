using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using PSC_HRM.Module;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.ThuNhap;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.Controllers;
using PSC_HRM.Module.ThuNhap.Import;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class ShowTemplateOnToolbarController : ViewController
    {
        private XPCollection<BieuMau> bieuMauList;
        private IObjectSpace obs;

        public ShowTemplateOnToolbarController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ShowReportOnToolbarController_Activated(object sender, EventArgs e)
        {
            if (!(View is DashboardView))
            {
                Type type = View.ObjectTypeInfo.Type;
                obs = Application.CreateObjectSpace();

                if (type != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=?", type.FullName);
                    SortProperty sort = new SortProperty("TenBieuMau", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    bieuMauList = new XPCollection<BieuMau>(((XPObjectSpace)obs).Session, filter, sort);

                    if (bieuMauList.Count > 0)
                        singleChoiceAction1.Active["ByMainForm"] = true;
                    else
                        singleChoiceAction1.Active["ByMainForm"] = false;
                }
                else
                {
                    singleChoiceAction1.Active["ByMainForm"] = false;
                }
            }
            else
            {
                singleChoiceAction1.Active["ByMainForm"] = false;
            }
        }

        private void ShowReportOnToolbarController_ViewControlsCreated(object sender, EventArgs e)
        {
            if (bieuMauList != null
                && bieuMauList.Count > 0)
            {
                ChoiceActionItem subItem;
                singleChoiceAction1.Items.Clear();
                foreach (BieuMau item in bieuMauList)
                {
                    subItem = new ChoiceActionItem();
                    subItem.Id = item.Oid.ToString();
                    subItem.Caption = item.TenBieuMau;
                    subItem.ImageName = "ChiTietLuong";
                    singleChoiceAction1.Items.Add(subItem);
                }
            }
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            foreach (BieuMau item in bieuMauList)
            {
                if (item.Oid.ToString() == e.SelectedChoiceActionItem.Id)
                {
                    obs = Application.CreateObjectSpace();
                    using (frmChonNhanVienExport frm = new frmChonNhanVienExport(((XPObjectSpace)obs).Session))
                    {
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            using (SaveFileDialog dialog = new SaveFileDialog())
                            {
                                dialog.AddExtension = true;
                                dialog.Filter = "Excel|*.xls|All file|*.*";
                                if (item.File != null)
                                {
                                    dialog.FileName = item.File.FileName;
                                    if (dialog.ShowDialog() == DialogResult.OK)
                                    {
                                        FileStream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                                        item.File.SaveToStream(stream);
                                        stream.Flush();
                                        stream.Close();
                                        stream.Dispose();

                                        OleDbHelper.ExportData(dialog.FileName, "[Sheet1$]", frm.GetData());

                                        Process.Start(new ProcessStartInfo(dialog.FileName));
                                    }
                                }
                                else
                                {
                                    XtraMessageBox.Show("File biểu mẫu không tồn tại trong cơ sở dữ liệu", "Thông báo");
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
