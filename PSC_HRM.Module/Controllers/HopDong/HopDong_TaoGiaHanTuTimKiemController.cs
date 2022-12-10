using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoGiaHanTuTimKiemController : ViewController
    {
        private IObjectSpace obs;
        private DanhSachHetHanHopDong dsHetHanHopDong;
        private QuanLyGiaHanHopDong qlGiaHan;

        public HopDong_TaoGiaHanTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoGiaHanTuTimKiemController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu danh sách đến hạn nâng lương
            View.ObjectSpace.CommitChanges();
            dsHetHanHopDong = View.CurrentObject as DanhSachHetHanHopDong;

            if (dsHetHanHopDong != null)
            {
                obs = Application.CreateObjectSpace();
                ChiTietGiaHanHopDong chitietGiaHanHD;
                int dem = 0;

                using (DialogUtil.AutoWait())
                {
                    //qlGiaHan = obs.FindObject<QuanLyGiaHanHopDong>(CriteriaOperator.Parse("NamHoc = ?", HamDungChung.CauHinhChung.NamHoc.Oid));
                    qlGiaHan = obs.FindObject<QuanLyGiaHanHopDong>(CriteriaOperator.Parse("NamHoc=?", HamDungChung.GetCurrentNamHoc(((XPObjectSpace)obs).Session)));

                    if (qlGiaHan == null)
                    {
                        qlGiaHan = obs.CreateObject<QuanLyGiaHanHopDong>();
                        qlGiaHan.NamHoc = obs.GetObjectByKey<NamHoc>(HamDungChung.CauHinhChung.NamHoc.Oid);
                    }
                    //Lấy danh sách cán bộ đã tìm được
                    foreach (ChiTietHetHanHopDong item in dsHetHanHopDong.ChiTietHetHanHopDongList)
                    {
                        if (item.Chon)
                        {   //Tạo chi tiết quyết định nâng lương cho từng người
                            chitietGiaHanHD = obs.CreateObject<ChiTietGiaHanHopDong>();
                            chitietGiaHanHD.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                            chitietGiaHanHD.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chitietGiaHanHD.HopDongNhanVien = obs.GetObjectByKey<HopDong_NhanVien>(item.HopDongLaoDong.Oid);
                            chitietGiaHanHD.NgayLap = HamDungChung.GetServerTime();

                            //Lưu chi tiết quyết định nâng lương vào quyết định
                            qlGiaHan.ListChiTietGiaHanHopDong.Add(chitietGiaHanHD);
                            dem++;
                        }
                    }
                    qlGiaHan.ThongTinTruong = obs.GetObjectByKey<ThongTinTruong>(HamDungChung.CauHinhChung.ThongTinTruong.Oid);

                    e.View = Application.CreateDetailView(obs, qlGiaHan);
                    e.Context = TemplateContext.View;
                }

            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (dsHetHanHopDong != null)
                dsHetHanHopDong.LoadData();
        }

        //reload listview after save object in detailvew
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void HopDong_TaoGiaHanTuTimKiemController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachHetHanHopDong>()
                && HamDungChung.IsWriteGranted<QuanLyGiaHanHopDong>()
                && HamDungChung.IsWriteGranted<ChiTietGiaHanHopDong>()
                && HamDungChung.IsWriteGranted<HopDong_NhanVien>();
        }
    }
}
