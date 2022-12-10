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
    public partial class ThongTinLuong_CapNhatPCCVCongDoanController : ViewController
    {
        public ThongTinLuong_CapNhatPCCVCongDoanController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_CapNhatPCCVCongDoanController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                using (XPCollection<ChucVuDoanThe> cmList = new XPCollection<ChucVuDoanThe>(((XPObjectSpace)obs).Session))
                {
                    XPCollection<DoanThe> nvList = new XPCollection<DoanThe>(((XPObjectSpace)obs).Session);
                    foreach (ChucVuDoanThe item in cmList)
                    {
                        nvList.Criteria = CriteriaOperator.Parse("ChucVuDoanThe=?", item.Oid);
                        foreach (DoanThe nvItem in nvList)
                        {
                        	nvItem.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChucVuCongDoan = item.HSPCChucVuDoanThe;
                        }
                    }
                    obs.CommitChanges();
                }
            }
        }
    }
}
