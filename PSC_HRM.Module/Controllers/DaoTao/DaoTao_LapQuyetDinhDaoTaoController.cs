using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DaoTao_LapQuyetDinhDaoTaoController : ViewController
    {
        private IObjectSpace obs;
        private DuyetDangKyDaoTao duyet;
        private QuyetDinhDaoTao quyetDinh;

        public DaoTao_LapQuyetDinhDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            duyet = View.CurrentObject as DuyetDangKyDaoTao;

            if (duyet != null)
            {
                quyetDinh = obs.CreateObject<QuyetDinhDaoTao>();
                quyetDinh.DuyetDangKyDaoTao = obs.GetObjectByKey<DuyetDangKyDaoTao>(duyet.Oid);
                quyetDinh.TrinhDoChuyenMon = obs.GetObjectByKey<TrinhDoChuyenMon>(duyet.TrinhDoChuyenMon.Oid);
                if (duyet.NganhDaoTao != null)
                    quyetDinh.NganhDaoTao = obs.GetObjectByKey<NganhDaoTao>(duyet.NganhDaoTao.Oid);
                if (duyet.TruongDaoTao.QuocGia != null)
                    quyetDinh.QuocGia = obs.GetObjectByKey<QuocGia>(duyet.TruongDaoTao.QuocGia.Oid);
                quyetDinh.TruongDaoTao = obs.GetObjectByKey<TruongDaoTao>(duyet.TruongDaoTao.Oid);
                if (duyet.NguonKinhPhi != null)
                    quyetDinh.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(duyet.NguonKinhPhi.Oid);
                if (duyet.KhoaDaoTao != null)
                    quyetDinh.KhoaDaoTao = obs.GetObjectByKey<KhoaDaoTao>(duyet.KhoaDaoTao.Oid);

                ChiTietDaoTao chiTiet;
                foreach (ChiTietDuyetDangKyDaoTao item in duyet.ListChiTietDuyetDangKyDaoTao)
                {
                    chiTiet = obs.CreateObject<ChiTietDaoTao>();
                    chiTiet.QuyetDinhDaoTao = quyetDinh;
                    chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                    if (duyet.ChuyenMonDaoTao != null)
                        chiTiet.ChuyenMonDaoTao = obs.GetObjectByKey<ChuyenMonDaoTao>(duyet.ChuyenMonDaoTao.Oid);
                }

                e.Context = TemplateContext.View;
                e.View = Application.CreateDetailView(obs, quyetDinh);
                obs.Committed += obs_Committed;
            }
        }

        void obs_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void DanhGia_CopyBangQuyDoiThangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] =
                HamDungChung.IsWriteGranted<QuanLyDaoTao>() &&
                HamDungChung.IsWriteGranted<DuyetDangKyDaoTao>() &&
                HamDungChung.IsWriteGranted<QuyetDinhDaoTao>();
        }
    }
}
