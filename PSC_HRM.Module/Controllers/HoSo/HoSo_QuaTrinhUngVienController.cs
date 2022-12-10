using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Layout;
using PSC_HRM.Module.HoSo;
using System.Windows.Forms;
using DevExpress.Xpo;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HoSo_QuaTrinhUngVienController : ViewController
    {
        public HoSo_QuaTrinhUngVienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_QuaTrinhUngVienController");
        }

        private void HoSo_QuaTrinhController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            if (view != null)
            {
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id.Contains("Custom"))
                    {
                        LinkLabel link = item.Control as LinkLabel;
                        link.Name = item.Id;
                        if (link != null)
                        {
                            link.BackColor = System.Drawing.Color.Transparent;
                            if (link.Name == "CustomQuaTrinhCongTac")
                                link.Text = "1. Quá trình công tác";
                            else if (link.Name == "CustomQuaTrinhKhenThuong")
                                link.Text = "2. Quá trình khen thưởng";
                            else if (link.Name == "CustomQuaTrinhNghienCuuKhoaHoc")
                                link.Text = "3. Quá trình nghiên cứu khoa học";
                            else if (link.Name == "CustomQuaTrinhThamGiaHoatDongXaHoi")
                                link.Text = "4. Quá trình tham gia hoạt động xã hội";
                            else if (link.Name == "CustomLichSuBanThan")
                                link.Text = "5. Lịch sử bản thân";
                            link.Click += link_Click;
                        }
                    }
                }
            }
        }

        void link_Click(object sender, EventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            if (link != null && HoSo.HoSo.CurrentHoSo != null)
            {
                CriteriaOperator filter1 = CriteriaOperator.Parse("HoSo=?", HoSo.HoSo.CurrentHoSo.Oid);
                if (link.Name == "CustomQuaTrinhCongTac")
                    CreateView(typeof(QuaTrinhCongTac), filter1);
                else if (link.Name == "CustomQuaTrinhKhenThuong")
                    CreateView(typeof(QuaTrinhKhenThuong), filter1);
                else if (link.Name == "CustomQuaTrinhThamGiaHoatDongXaHoi")
                    CreateView(typeof(QuaTrinhThamGiaHoatDongXaHoi), filter1);
                else if (link.Name == "CustomQuaTrinhNghienCuuKhoaHoc")
                    CreateView(typeof(QuaTrinhNghienCuuKhoaHoc), filter1);
                else if (link.Name == "CustomLichSuBanThan")
                    CreateView(typeof(LichSuBanThan), filter1);
            }
        }

        private void CreateView(Type type, CriteriaOperator filter)
        {
            CollectionSource dataSource = new CollectionSource(View.ObjectSpace, type);
            ((XPBaseCollection)dataSource.Collection).Criteria = filter;
            ShowViewParameters showView = new ShowViewParameters();
            showView.CreatedView = Application.CreateListView(Application.GetListViewId(type), dataSource, false);
            showView.TargetWindow = TargetWindow.NewModalWindow;
            showView.Context = TemplateContext.View;
            showView.CreateAllControllers = true;

            Application.ShowViewStrategy.ShowView(showView, new ShowViewSource(null, null));
        }        
    }
}
