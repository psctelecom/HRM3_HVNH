using System;

using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using PSC_HRM.Module.BaoMat;

using PSC_HRM.Module.Win.Editors;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using System.Data.SqlClient;
using DevExpress.Xpo;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class FilterBoPhanController : ViewController
    {
        public FilterBoPhanController()
        {
            InitializeComponent();
            RegisterActions(components);
            //TargetViewType = ViewType.ListView;
            TargetObjectType = typeof(IBoPhan);
        }

        private void BoPhanFilter_Activated(object sender, EventArgs e)
        {
            ListView listView = View as ListView;

            if (listView != null)
            {
                //filter danh sách nhân viên theo bộ phận
                if (listView.Editor is CustomCategorizedListEditor)
                {
                    CustomCategorizedListEditor editor = listView.Editor as CustomCategorizedListEditor;
                    listView = editor.CategoriesListView;
                    if (listView != null)
                    {
                        NguoiSuDung user = SecuritySystem.CurrentUser as NguoiSuDung;

                        InOperator criteria = new InOperator();
                        bool state = false;
                        if (user != null && 
                            !HamDungChung.CheckAdministrator() &&
                            user.PhanQuyenBoPhan != null &&
                            (listView.ObjectTypeInfo.Type == typeof(BoPhan) ||
                            listView.ObjectTypeInfo.Implements<IBoPhan>()))
                        {
                            List<string> bpList = HamDungChung.DanhSachBoPhanDuocPhanQuyen(((XPObjectSpace)View.ObjectSpace).Session);
                            foreach (string item in bpList)
                            {
                                criteria.Operands.Add(new OperandValue(item));
                            }

                            if (listView.ObjectTypeInfo.Type == typeof(BoPhan))
                            {
                                criteria.LeftOperand = new OperandProperty("Oid");
                                state = true;
                            }
                            else if (listView.ObjectTypeInfo.Implements<IBoPhan>() && listView.ObjectTypeInfo.Type != typeof(ThongTinTruong))
                            {
                                criteria.LeftOperand = new OperandProperty("BoPhan.Oid");
                                state = true;
                            }

                            if (state)
                            {
                                listView.CollectionSource.Criteria["PhanQuyenBoPhan"] = criteria;
                            }
                        }
                    }

                    //
                }
                else//filter các đối tượng khác theo bộ phận
                {
                    NguoiSuDung user = SecuritySystem.CurrentUser as NguoiSuDung;
                    InOperator criteria = new InOperator();
                    bool state = false;

                    if (user != null &&
                        !HamDungChung.CheckAdministrator() &&
                        user.PhanQuyenBoPhan != null &&
                        (listView.ObjectTypeInfo.Type == typeof(BoPhan) ||
                        listView.ObjectTypeInfo.Implements<IBoPhan>()))
                    {
                        List<string> bpList = HamDungChung.DanhSachBoPhanDuocPhanQuyen(((XPObjectSpace)View.ObjectSpace).Session);
                        foreach (string item in bpList)
                        {
                            criteria.Operands.Add(new OperandValue(item));
                        }

                        if (listView.ObjectTypeInfo.Type == typeof(BoPhan))
                        {
                            criteria.LeftOperand = new OperandProperty("Oid");
                            state = true;
                        }
                        else if (listView.ObjectTypeInfo.Implements<IBoPhan>())
                        {
                            criteria.LeftOperand = new OperandProperty("BoPhan.Oid");
                            state = true;
                        }


                        if (state && TruongConfig.MaTruong != "VHU")
                        {
                            listView.CollectionSource.Criteria["PhanQuyenBoPhan"] = criteria;
                        }
                        //else
                        //{
                        //    IObjectSpace os1 = Application.CreateObjectSpace();
                        //    Session ses = ((XPObjectSpace)View.ObjectSpace).Session;
                        //    XPCollection<BoPhan> listBP = new XPCollection<BoPhan>(ses, criteria);
                        //    foreach (object item in listBP)
                        //    {
                        //        listView.CollectionSource.List.Add(item);
                        //    }
                        //    //BoPhan bp = new BoPhan(ses);
                        //    listView.CollectionSource.Criteria["PhanQuyenBoPhan"] = GroupOperator.And(new BinaryOperator("Oid", "1990fa49-7488-4474-8d01-34ee23147c03"));
                        //}
                    }
                }
            }
        }

       
    }
}
