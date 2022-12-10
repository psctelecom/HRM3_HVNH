using System;

using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.Report;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class FilterBaoCaoController : ViewController
    {
        public FilterBaoCaoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BaoCaoFilter_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            if (listView != null)
            {
                if (listView.ObjectTypeInfo.Type == typeof(HRMReport))
                {
                    NguoiSuDung user = SecuritySystem.CurrentUser as NguoiSuDung;
                    if (user != null)
                    {
                        if (!HamDungChung.CheckAdministrator())
                        {
                            InOperator criteria = new InOperator { LeftOperand = new OperandProperty("Oid") };

                            if (user.PhanQuyenBaoCao != null && !string.IsNullOrEmpty(user.PhanQuyenBaoCao.Quyen))
                            {
                                string[] bcList = user.PhanQuyenBaoCao.Quyen.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string item in bcList)
                                {
                                    criteria.Operands.Add(new OperandValue(item));
                                }

                                //listView.CollectionSource.Criteria.Clear();
                                listView.CollectionSource.Criteria["PhanQuyenXemBaoCao"] = criteria;
                            }
                            else
                            {
                                //không hiện báo cáo nào
                                //listView.CollectionSource.Criteria.Clear();
                                listView.CollectionSource.Criteria["PhanQuyenXemBaoCao"] = CriteriaOperator.Parse("NhomBaoCao.Oid=?", "8190f1f7-5353-4118-b760-18faaacdf8fb");
                            }
                        }
                    }
                }
            }
        }
    }
}
