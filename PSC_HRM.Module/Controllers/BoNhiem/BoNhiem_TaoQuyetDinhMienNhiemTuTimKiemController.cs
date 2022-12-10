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
using PSC_HRM.Module.BoNhiem;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoNhiem_TaoQuyetDinhMienNhiemTuTimKiemController : ViewController
    {
        private IObjectSpace obs;
        private DanhSachHetHanBoNhiem dsHetHanBoNhiem;

        public BoNhiem_TaoQuyetDinhMienNhiemTuTimKiemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoNhiem_TaoQuyetDinhMienNhiemTuTimKiemController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //lưu danh sách hết hạn bổ nhiệm
            View.ObjectSpace.CommitChanges();
            dsHetHanBoNhiem = View.CurrentObject as DanhSachHetHanBoNhiem;

            if (dsHetHanBoNhiem != null)
            {
                obs = Application.CreateObjectSpace();

                using (DialogUtil.AutoWait())
                {
                    //Lấy danh sách cán bộ đã tìm được
                    foreach (HetHanBoNhiem item in dsHetHanBoNhiem.ListChiTietBoNhiem)
                    {
                        if (item.Chon)
                        {
                            if (item.ChucVuKiemNhiem == false)
                            {
                                //Tạo chi tiết quyết định nâng lương cho từng người
                                QuyetDinhMienNhiem quyetDinh = obs.CreateObject<QuyetDinhMienNhiem>();
                                quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                quyetDinh.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                quyetDinh.QuyetDinhBoNhiem = obs.GetObjectByKey<QuyetDinhBoNhiem>(item.QuyetDinh.Oid);
                                quyetDinh.QuyetDinhMoi = false;
                                quyetDinh.ThongTinTruong = obs.GetObjectByKey<ThongTinTruong>(HamDungChung.CauHinhChung.ThongTinTruong.Oid);

                                e.Context = TemplateContext.View;
                                e.View = Application.CreateDetailView(obs, quyetDinh);
                                obs.Committed += new EventHandler(obs_Committed);
                                break;
                            } 
                            else if (item.ChucVuKiemNhiem == true)
                            {
                                //Tạo chi tiết quyết định nâng lương cho từng người
                                QuyetDinhMienNhiemKiemNhiem quyetDinh = obs.CreateObject<QuyetDinhMienNhiemKiemNhiem>();
                                quyetDinh.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                quyetDinh.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                quyetDinh.QuyetDinhBoNhiemKiemNhiem = obs.GetObjectByKey<QuyetDinhBoNhiemKiemNhiem>(item.QuyetDinh.Oid);
                                quyetDinh.QuyetDinhMoi = false;
                                quyetDinh.ThongTinTruong = obs.GetObjectByKey<ThongTinTruong>(HamDungChung.CauHinhChung.ThongTinTruong.Oid);

                                e.Context = TemplateContext.View;
                                e.View = Application.CreateDetailView(obs, quyetDinh);
                                obs.Committed += new EventHandler(obs_Committed);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (dsHetHanBoNhiem != null)
                dsHetHanBoNhiem.LoadData();
        }

        //reload listview after save object in detailvew
        void obs_Committed(object sender, EventArgs e)
        {
            if (dsHetHanBoNhiem != null)
                dsHetHanBoNhiem.LoadData();
        }

        private void BoNhiem_TaoQuyetDinhMienNhiemTuTimKiemController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active.Clear();
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhSachHetHanBoNhiem>()
                && HamDungChung.IsWriteGranted<QuyetDinh.QuyetDinhMienNhiem>()
                && HamDungChung.IsWriteGranted<QuyetDinh.QuyetDinhMienNhiemKiemNhiem>()
                && HamDungChung.IsWriteGranted<HetHanBoNhiem>()
                && HamDungChung.IsWriteGranted<NhanVienThongTinLuong>();
        }
    }
}
