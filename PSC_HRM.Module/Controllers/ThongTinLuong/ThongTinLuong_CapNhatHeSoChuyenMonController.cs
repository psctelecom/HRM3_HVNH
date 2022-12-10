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
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThongTinLuong_CapNhatHeSoChuyenMonController : ViewController
    {
        public ThongTinLuong_CapNhatHeSoChuyenMonController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThongTinLuong_CapNhatHeSoChuyenMonController");
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                using (XPCollection<HeSoChuyenMon> cmList = new XPCollection<HeSoChuyenMon>(((XPObjectSpace)obs).Session))
                {
                    XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)obs).Session);
                    foreach (HeSoChuyenMon item in cmList)
                    {
                        if (item.CoHocVi)
                            nvList.Criteria = CriteriaOperator.Parse("CongViec=? and NhanVienTrinhDo.TrinhDoChuyenMon=?", item.CongViecHienNay.Oid, item.TrinhDoChuyenMon.Oid);
                        else
                            nvList.Criteria = CriteriaOperator.Parse("NhanVienTrinhDo.TrinhDoChuyenMon is null");
                        foreach (ThongTinNhanVien nvItem in nvList)
                        {
                        	nvItem.NhanVienThongTinLuong.HSPCChuyenMon = item.HSPCChuyenMon;
                        }
                    }
                    obs.CommitChanges();
                }
            }
        }
    }
}
