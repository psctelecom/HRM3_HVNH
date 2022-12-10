using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DaoTao;
using DevExpress.Utils;
using PSC_HRM.Module;
using PSC_HRM.Module.NghiHuu;
using PSC_HRM.Module.Win.XuLyMailMerge.XuLy;

namespace PSC_HRM.Module.Win.Controllers
{
    public partial class NghiHuu_TaoThongBaoTuTimKiemController : ViewController
    {
        public NghiHuu_TaoThongBaoTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("NghiHuu_TaoThongBaoTuTimKiemController");
        }

        private void NghiHuu_TaoThongBaoTuTimKiemController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachDenTuoiNghiHuu>()
                && HamDungChung.IsCreateGranted<NhanVienThongTinLuong>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            DanhSachDenTuoiNghiHuu danhSach = View.CurrentObject as DanhSachDenTuoiNghiHuu;
            if (danhSach != null)
            {
                List<DenTuoiNghiHuu> list = new List<DenTuoiNghiHuu>();
                foreach (DenTuoiNghiHuu item in danhSach.DenTuoiNghiHuuList)
                {
                    if (item.Chon)
                    {
                        list.Add(item);
                    }
                }
                new MailMerge_ThongBaoNghiHuuTimKiem().Merge(Application.CreateObjectSpace(), list);
            }
        }
    }
}
