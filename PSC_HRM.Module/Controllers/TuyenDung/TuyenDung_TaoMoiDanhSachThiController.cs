using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class TuyenDung_TaoMoiDanhSachThiController : ViewController
    {
        private IObjectSpace obs;
        private TuyenDung_DanhSachThi danhSachThiSinh;
        private DanhSachThi danhSachThi;

        public TuyenDung_TaoMoiDanhSachThiController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("TuyenDung_TaoMoiDanhSachThiController");
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lưu danh sách thi
            View.ObjectSpace.CommitChanges();
            obs = Application.CreateObjectSpace();

            danhSachThi = View.CurrentObject as DanhSachThi;
            if (danhSachThi != null && danhSachThi.BuocTuyenDung != null)
            {
                danhSachThiSinh = obs.CreateObject<TuyenDung_DanhSachThi>();

                CriteriaOperator filter = CriteriaOperator.Parse("VongTuyenDung.ChiTietTuyenDung.ViTriTuyenDung=? and VongTuyenDung.BuocTuyenDung.ThuTu=?",
                            danhSachThi.ChiTietTuyenDung.ViTriTuyenDung.Oid, danhSachThi.BuocTuyenDung.ThuTu);
                using (XPCollection<ChiTietVongTuyenDung> chiTietList = new XPCollection<ChiTietVongTuyenDung>(((XPObjectSpace)obs).Session, filter))
                {
                    TuyenDung_ThiSinh ungVien;
                    foreach (ChiTietVongTuyenDung item in chiTietList)
                    {
                        filter = CriteriaOperator.Parse("DanhSachThi.BuocTuyenDung=? and DanhSachThi.MonThi=? and UngVien=?",
                            danhSachThi.BuocTuyenDung.Oid, danhSachThi.MonThi.Oid, item.UngVien.Oid);
                        ThiSinh thiSinh = obs.FindObject<ThiSinh>(filter);
                        if (thiSinh == null)
                        {
                            ungVien = obs.CreateObject<TuyenDung_ThiSinh>();
                            danhSachThiSinh.ListUngVien.Add(ungVien);
                            ungVien.UngVien = obs.GetObjectByKey<UngVien>(item.UngVien.Oid);
                        }
                    }
                    e.View = Application.CreateDetailView(obs, danhSachThiSinh);
                }
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            obs = View.ObjectSpace;
                
            ThiSinh thiSinh;
            foreach (TuyenDung_ThiSinh item in danhSachThiSinh.ListUngVien)
            {
                if (item.Chon && !danhSachThi.IsExist(item.UngVien))
                {
                    thiSinh = obs.CreateObject<ThiSinh>();
                    thiSinh.UngVien = obs.GetObjectByKey<UngVien>(item.UngVien.Oid);
                    danhSachThi.ListThiSinh.Add(thiSinh);
                }
            }

            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void BienDongAction_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] =
                HamDungChung.IsWriteGranted<DanhSachThi>();
        }
    }
}
