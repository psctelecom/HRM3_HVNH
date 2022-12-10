using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoiDuong_LapQuyetDinhBoiDuongController : ViewController
    {
        private IObjectSpace obs;
        private DuyetDangKyBoiDuong duyet;
        private QuyetDinhBoiDuong quyetDinh;

        public BoiDuong_LapQuyetDinhBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoiDuong_LapQuyetDinhBoiDuongController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu Quản lý hợp đồng
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            duyet = View.CurrentObject as DuyetDangKyBoiDuong;
            if (duyet != null)
            {
                quyetDinh = obs.FindObject<QuyetDinhBoiDuong>(CriteriaOperator.Parse("DuyetDangKyBoiDuong=?", duyet.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhBoiDuong>();
                    quyetDinh.ChuongTrinhBoiDuong = obs.GetObjectByKey<ChuongTrinhBoiDuong>(duyet.ChuongTrinhBoiDuong.Oid);
                    if (duyet.NguonKinhPhi != null)
                        quyetDinh.NguonKinhPhi = obs.GetObjectByKey<NguonKinhPhi>(duyet.NguonKinhPhi.Oid);
                    quyetDinh.TuNgay = duyet.TuNgay;
                    quyetDinh.DenNgay = duyet.DenNgay;
                    quyetDinh.DuyetDangKyBoiDuong = obs.GetObjectByKey<DuyetDangKyBoiDuong>(duyet.Oid);
                }
                ChiTietBoiDuong chiTiet;
                foreach (ChiTietDuyetDangKyBoiDuong item in duyet.ListChiTietDuyetDangKyBoiDuong)
                {
                    chiTiet = obs.FindObject<ChiTietBoiDuong>(CriteriaOperator.Parse("QuyetDinhBoiDuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietBoiDuong>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        quyetDinh.ListChiTietBoiDuong.Add(chiTiet);
                    }
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
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhBoiDuong>()
                && HamDungChung.IsWriteGranted<ChiTietBoiDuong>();
        }
    }
}
