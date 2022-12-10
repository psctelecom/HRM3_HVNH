using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class BoiDuong_DuyetBoiDuongController : ViewController
    {
        public BoiDuong_DuyetBoiDuongController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("BoiDuong_DuyetBoiDuongController");
        }

        private void DanhGia_ImportChamCongController_Activated(object sender, EventArgs e)
        {
            simpleAction.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyBoiDuong>()
                && HamDungChung.IsWriteGranted<DuyetDangKyBoiDuong>();
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý...", "Vui lòng chờ"))
            {
                DangKyBoiDuong dangKy = View.CurrentObject as DangKyBoiDuong;
                if (dangKy != null && dangKy.QuanLyBoiDuong != null)
                {
                    IObjectSpace obs = Application.CreateObjectSpace();
                    QuanLyBoiDuong quanLy = obs.GetObjectByKey<QuanLyBoiDuong>(dangKy.QuanLyBoiDuong.Oid);
                    DuyetDangKyBoiDuong duyet = obs.FindObject<DuyetDangKyBoiDuong>(CriteriaOperator.Parse("DangKyBoiDuong=? and QuanLyBoiDuong=?", dangKy.Oid,quanLy.Oid));
               
                        if (duyet == null)
                        {
                            duyet = obs.CreateObject<DuyetDangKyBoiDuong>();
                            duyet.QuanLyBoiDuong = obs.GetObjectByKey<QuanLyBoiDuong>(dangKy.QuanLyBoiDuong.Oid);
                            duyet.QuocGia = dangKy.QuocGia != null ? obs.GetObjectByKey<QuocGia>(dangKy.QuocGia.Oid) : null;
                            duyet.DangKyBoiDuong = obs.GetObjectByKey<DangKyBoiDuong>(dangKy.Oid);
                            duyet.ChuongTrinhBoiDuong = obs.GetObjectByKey<ChuongTrinhBoiDuong>(dangKy.ChuongTrinhBoiDuong.Oid);
                            duyet.NguonKinhPhi = dangKy.NguonKinhPhi != null ? obs.GetObjectByKey<NguonKinhPhi>(dangKy.NguonKinhPhi.Oid) : null;
                            duyet.TuNgay = dangKy.TuNgay;
                            duyet.DenNgay = dangKy.DenNgay;
                        }

                        ChiTietDuyetDangKyBoiDuong chiTiet;
                        foreach (ChiTietDangKyBoiDuong cItem in dangKy.ListChiTietDangKyBoiDuong)
                        {
                            chiTiet = obs.FindObject<ChiTietDuyetDangKyBoiDuong>(CriteriaOperator.Parse("DuyetDangKyBoiDuong=? and ThongTinNhanVien=?", duyet.Oid, cItem.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = obs.CreateObject<ChiTietDuyetDangKyBoiDuong>();
                                chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(cItem.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(cItem.ThongTinNhanVien.Oid);
                                duyet.ListChiTietDuyetDangKyBoiDuong.Add(chiTiet);
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
