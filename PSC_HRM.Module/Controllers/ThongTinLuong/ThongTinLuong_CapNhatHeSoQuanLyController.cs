using System;
using System.Collections.Generic;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CapNhatHeSoQuanLyController : ViewController
    {
        public ThongTinLuong_CapNhatHeSoQuanLyController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_CapNhatHeSoQuanLyController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                using (XPCollection<ChucVu> chucVuList = new XPCollection<ChucVu>(((XPObjectSpace)obs).Session))
                {
                    CriteriaOperator filter;
                    foreach (ChucVu item in chucVuList)
                    {
                        filter = CriteriaOperator.Parse("ChucVu=?", item.Oid);
                        XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)obs).Session);

                        foreach (ThongTinNhanVien nvItem in nvList)
                        {

                        }

                        filter = CriteriaOperator.Parse("ChucVuKiemNhiem=?", item.Oid);
                    }
                }
            }
        }
    }
}
