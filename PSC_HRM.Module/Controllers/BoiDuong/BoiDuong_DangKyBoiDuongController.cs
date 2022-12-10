using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoiDuong_DangKyBoiDuongController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien danhSach;
        private DuyetDangKyBoiDuong duyet;

        public BoiDuong_DangKyBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoiDuong_DangKyBoiDuongController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = false;//HamDungChung.IsWriteGranted<QuanLyBoiDuong>()
            //    && HamDungChung.IsCreateGranted<DangKyBoiDuong>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChiTietDuyetDangKyBoiDuong chiTiet;
            foreach (var item in danhSach.ListNhanVien)
            {
                if (item.Chon)
                {
                    chiTiet = obs.FindObject<ChiTietDuyetDangKyBoiDuong>(CriteriaOperator.Parse("DuyetDangKyBoiDuong=? and ThongTinNhanVien=?", duyet.Oid, item.ThongTinNhanVien.Oid));
                    if (chiTiet == null)
                    {
                        chiTiet = obs.CreateObject<ChiTietDuyetDangKyBoiDuong>();
                        chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        duyet.ListChiTietDuyetDangKyBoiDuong.Add(chiTiet);
                    }
                }
            }
            View.ObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }
    }
}
