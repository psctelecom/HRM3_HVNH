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
    public partial class HoSo_QuaTrinhController : ViewController
    {
        public HoSo_QuaTrinhController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HoSo_QuaTrinhController");
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
                            if (item.Id.Equals("CustomDienBienLuong"))
                                link.Text = "1. Diễn biến lương và phụ cấp";
                            else if (link.Name == "CustomLichSuBanThan")
                                link.Text = "2. Lịch sử bản thân";
                            else if (link.Name == "CustomQuaTrinhBoNhiem")
                                link.Text = "3. Quá trình bổ nhiệm";
                            else if (link.Name == "CustomQuaTrinhCongTac")
                                link.Text = "4. Quá trình công tác";
                            else if (link.Name == "CustomQuaTrinhDaoTao")
                                link.Text = "5. Quá trình đào tạo";
                            else if (link.Name == "CustomQuaTrinhBoiDuong")
                                link.Text = "6. Quá trình bôi dưỡng";
                            else if (link.Name == "CustomQuaTrinhDiNuocNgoai")
                                link.Text = "7. Quá trình đi nước ngoài";
                            else if (link.Name == "CustomQuaTrinhGiangDay")
                                link.Text = "8. Quá trình tham gia giảng dạy";
                            else if (link.Name == "CustomQuaTrinhHoiThaoHoiNghi")
                                link.Text = "9. Quá trình tham dự hội thảo, hội nghị";
                            else if (link.Name == "CustomQuaTrinhKhenThuong")
                                link.Text = "10. Quá trình khen thưởng";
                            else if (link.Name == "CustomQuaTrinhKyLuat")
                                link.Text = "11. Quá trình kỷ luật";
                            else if (link.Name == "CustomQuaTrinhNghienCuuKhoaHoc")
                                link.Text = "12. Quá trình nghiên cứu khoa học";
                            else if (link.Name == "CustomQuaTrinhThamGiaHoatDongXaHoi")
                                link.Text = "13. Quá trình tham gia hoạt động xã hội";
                            else if (link.Name == "CustomQuaTrinhThanhTraHoatDongGiangDay")
                                link.Text = "14. Quá trình thanh tra hoạt động giảng dạy";
                            else if (link.Name == "CustomThamGiaLucLuongVuTrang")
                                link.Text = "15. Quá trình tham gia lực lượng vũ trang";
                            link.Click += link_Click;
                        }
                    }
                }
            }
        }

        void link_Click(object sender, EventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            if (link != null && ThongTinNhanVien.NhanVien != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.NhanVien.Oid);
                CriteriaOperator filter1 = CriteriaOperator.Parse("HoSo=?", ThongTinNhanVien.NhanVien.Oid);
                if (link.Name == "CustomDienBienLuong")
                    CreateView(typeof(DienBienLuong), filter);
                else if (link.Name == "CustomQuaTrinhDaoTao")
                    CreateView(typeof(QuaTrinhDaoTao), filter);
                else if (link.Name == "CustomQuaTrinhBoiDuong")
                    CreateView(typeof(QuaTrinhBoiDuong), filter);
                else if (link.Name == "CustomQuaTrinhKyLuat")
                    CreateView(typeof(QuaTrinhKyLuat), filter);
                else if (link.Name == "CustomQuaTrinhBoNhiem")
                    CreateView(typeof(QuaTrinhBoNhiem), filter);
                else if (link.Name == "CustomQuaTrinhHoiThaoHoiNghi")
                    CreateView(typeof(QuaTrinhHoiThaoHoiNghi), filter);
                else if (link.Name == "CustomThamGiaLucLuongVuTrang")
                    CreateView(typeof(ThamGiaLucLuongVuTrang), filter);
                else if (link.Name == "CustomQuaTrinhGiangDay")
                    CreateView(typeof(QuaTrinhGiangDay), filter);
                else if (link.Name == "CustomQuaTrinhThanhTraHoatDongGiangDay")
                    CreateView(typeof(QuaTrinhThanhTraHoatDongGiangDay), filter);
                else if (link.Name == "CustomQuaTrinhDiNuocNgoai")
                    CreateView(typeof(QuaTrinhDiNuocNgoai), filter);

                else if (link.Name == "CustomQuaTrinhCongTac")
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
