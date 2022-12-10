using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DoanDang;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CapNhatPCCVDangController : ViewController
    {
        public ThongTinLuong_CapNhatPCCVDangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_CapNhatPCCVDangController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                using (XPCollection<ChucVuDang> cmList = new XPCollection<ChucVuDang>(((XPObjectSpace)obs).Session))
                {
                    XPCollection<DangVien> nvList = new XPCollection<DangVien>(((XPObjectSpace)obs).Session);
                    foreach (ChucVuDang item in cmList)
                    {
                        nvList.Criteria = CriteriaOperator.Parse("ChucVuDang=?", item.Oid);
                        foreach (DangVien nvItem in nvList)
                        {
                        	nvItem.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuDang = item.HSPCChucVuDang;
                        }
                    }
                    obs.CommitChanges();
                }
            }
        }
    }
}
