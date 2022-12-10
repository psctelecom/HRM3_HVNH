using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using PSC_HRM.Module.Win.Forms;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class HoSo_TimKiemNhanVienController : ViewController
    {
        public HoSo_TimKiemNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSo_TimKiemNhanVienController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            List<ThongTinNhanVien> nvList = new List<ThongTinNhanVien>();
            CriteriaOperator criteria;

            if (view != null)
            {
                foreach (ControlDetailItem item in view.GetItems<ControlDetailItem>())
                {
                    if (item.Id == "btnSearch")
                    {
                        SimpleButton btnSearch = item.Control as SimpleButton;
                        if (btnSearch != null)
                        {
                            btnSearch.Text = "Tìm kiếm";
                            btnSearch.Width = 80;
                            btnSearch.Click += (obj, ea) =>
                            {
                                using (DialogUtil.AutoWait())
                                {
                                    ChiTietTrichDanhSachNhanVien data;//
                                    TimKiemNhanVien search = view.CurrentObject as TimKiemNhanVien;
                                    if (search != null)
                                    {

                                        //
                                        IObjectSpace obs = Application.CreateObjectSpace();
                                        ///
                                        DateTime current = HamDungChung.GetServerTime();
                                        //Lấy danh sách thỏa điều kiện
                                        List<Guid> list = HamDungChung.GetCriteria(obs, "spd_DieuKien_ThongTinNhanVien", CommandType.StoredProcedure, search.DieuKienTimKiem, new object[] { current.SetTime(SetTimeEnum.StartYear), current.SetTime(SetTimeEnum.EndMonth) });
                                        //Lấy danh sách tất cả nhân viên
                                        XPCollection<ThongTinNhanVien> nhanVienList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)obs).Session);
                                        //Lọc lại danh sách nhân viên thỏa điều kiện
                                        nvList = (from x in nhanVienList
                                                  where list.Contains(x.Oid)
                                                  select x).ToList();                                  
                                    }

                                    if (search.ListChiTietTrichDanhSachNhanVien == null)
                                        search.ListChiTietTrichDanhSachNhanVien = new XPCollection<ChiTietTrichDanhSachNhanVien>(((XPObjectSpace)View.ObjectSpace).Session, false);
                                    else
                                        search.ListChiTietTrichDanhSachNhanVien.Reload();
                                    //
                                    foreach (ThongTinNhanVien nvItem in nvList)
                                    {
                                        data = new ChiTietTrichDanhSachNhanVien(((XPObjectSpace)View.ObjectSpace).Session);
                                        data.NhanVien = View.ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nvItem.Oid);
                                        
                                        search.ListChiTietTrichDanhSachNhanVien.Add(data);
                                    }
                                    View.Refresh();
                                }         
                            };
                        }
                    }
                }
            }
        }
    }
}
