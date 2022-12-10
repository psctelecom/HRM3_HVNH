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

namespace PSC_HRM.Module.Controllers
{
    public partial class DaoTao_DuyetDaoTaoController : ViewController
    {
        public DaoTao_DuyetDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DaoTao_DuyetDaoTaoController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyDaoTao>()
                && HamDungChung.IsCreateGranted<DuyetDangKyDaoTao>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý...", "Vui lòng chờ"))
            {
                DangKyDaoTao dangKy = View.CurrentObject as DangKyDaoTao;
                if (dangKy != null && dangKy.QuanLyDaoTao != null)
                {
                    IObjectSpace obs = Application.CreateObjectSpace();
                    QuanLyDaoTao quanLy = obs.GetObjectByKey<QuanLyDaoTao>(dangKy.QuanLyDaoTao.Oid);
                    DuyetDangKyDaoTao duyet = null;
                    ChiTietDuyetDangKyDaoTao chiTiet;
                    foreach (DangKyDaoTao item in View.SelectedObjects)
                    {
                        if (!quanLy.IsExist(item.TrinhDoChuyenMon, item.ChuyenMonDaoTao, item.TruongDaoTao))
                        {
                            duyet = obs.CreateObject<DuyetDangKyDaoTao>();
                            duyet.QuanLyDaoTao = quanLy;
                            duyet.DangKyDaoTao = obs.GetObjectByKey<DangKyDaoTao>(dangKy.Oid);
                            duyet.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(item.TrinhDoChuyenMon.Oid);
                            duyet.ChuyenMonDaoTao = obs.GetObjectByKey<ChuyenMonDaoTao>(item.ChuyenMonDaoTao.Oid);
                            duyet.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(item.TruongDaoTao.Oid);
                            if (duyet.NguonKinhPhi != null)
                                duyet.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(item.NguonKinhPhi.Oid);
                            if (duyet.KhoaDaoTao != null)
                                duyet.KhoaDaoTao = obs.GetObjectByKey<KhoaDaoTao>(item.KhoaDaoTao.Oid);
                            duyet.GhiChu = item.GhiChu;
                        }
                        foreach (ChiTietDangKyDaoTao ctItem in item.ListChiTietDangKyDaoTao)
                        {
                            if (!duyet.IsExists(ctItem.ThongTinNhanVien))
                            {
                                chiTiet = obs.CreateObject<ChiTietDuyetDangKyDaoTao>();
                                chiTiet.DuyetDangKyDaoTao = duyet;
                                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(ctItem.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(ctItem.ThongTinNhanVien.Oid);
                                chiTiet.GhiChu = ctItem.GhiChu;
                            }
                        }
                    }
                    obs.CommitChanges();
                    View.ObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();
                }
            }
        }
    }
}
