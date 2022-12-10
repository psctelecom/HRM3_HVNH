using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.ThuNhap.TamUng;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class TamUng_KetChuyenTamUngController : ViewController
    {
        private IObjectSpace obs;
        private KetChuyenTamUng ketChuyen;
        private BangTamUng bangTamUng;

        public TamUng_KetChuyenTamUngController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TruyLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangTamUng>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bangTamUng = View.CurrentObject as BangTamUng;
            obs = Application.CreateObjectSpace();
            ketChuyen = obs.CreateObject<KetChuyenTamUng>();
            e.View = Application.CreateDetailView(obs, ketChuyen);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            TamUng.TamUng tamUng;
            BangTamUng bang = obs.GetObjectByKey<BangTamUng>(ketChuyen.BangTamUng.Oid);
            foreach (TamUng.TamUng item in bang.ListTamUng)
            {
                if (item.SoTienConLai > 0)
                {
                    tamUng = obs.FindObject<TamUng.TamUng>(CriteriaOperator.Parse("BangTamUng=? and ThongTinNhanVien=?", bangTamUng.Oid, item.ThongTinNhanVien.Oid));
                    if (tamUng == null)
                    {
                        tamUng = obs.CreateObject<TamUng.TamUng>();
                        tamUng.BangTamUng = obs.GetObjectByKey<BangTamUng>(bangTamUng.Oid); ;
                        tamUng.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        tamUng.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        tamUng.KetChuyenTuNamTruoc = item.SoTienConLai;
                        tamUng.TongTamUng = item.SoTienConLai;
                        tamUng.MucKhauTruHangThang = item.MucKhauTruHangThang;
                    }
                }
            }
            obs.CommitChanges();
            View.ObjectSpace.ReloadObject(bangTamUng);
            (View as DetailView).Refresh();
            DialogUtil.ShowInfo("Kết chuyển tạm ứng thành công.");
        }
    }
}
