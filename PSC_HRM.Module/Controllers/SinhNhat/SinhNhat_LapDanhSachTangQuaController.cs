using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.SinhNhat;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.XuLyQuyTrinh.SinhNhat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class SinhNhat_LapDanhSachTangQuaController : ViewController
    {
        public SinhNhat_LapDanhSachTangQuaController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("SinhNhat_LapDanhSachTangQuaController");
        }

        private void BoNhiem_LapDeNghiBoNhiemController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyTangQuaSinhNhat>() &&
                HamDungChung.IsCreateGranted<ChiTietTangQuaSinhNhat>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ThongTinSinhNhat thongTin = View.CurrentObject as ThongTinSinhNhat;
            if (thongTin != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();
                ThongTinTruong truong = HamDungChung.ThongTinTruong(((XPObjectSpace)obs).Session);
                if (truong != null)
                {
                    DateTime current = HamDungChung.GetServerTime();
                    QuanLyTangQuaSinhNhat quanLy = obs.FindObject<QuanLyTangQuaSinhNhat>(CriteriaOperator.Parse("ThongTinTruong=? and Thang=?", truong.Oid, current.SetTime(SetTimeEnum.StartMonth)));
                    if (quanLy == null)
                    {
                        quanLy = obs.CreateObject<QuanLyTangQuaSinhNhat>();
                        quanLy.Thang = current;
                    }

                    ChiTietTangQuaSinhNhat obj = obs.FindObject<ChiTietTangQuaSinhNhat>(CriteriaOperator.Parse("QuanLyTangQuaSinhNhat=? and ThongTinNhanVien=?", quanLy.Oid, thongTin.ThongTinNhanVien.Oid));
                    if (obj == null)
                    {
                        obj = obs.CreateObject<ChiTietTangQuaSinhNhat>();
                        obj.QuanLyTangQuaSinhNhat = quanLy;
                        obj.BoPhan = obs.GetObjectByKey<BoPhan>(thongTin.BoPhan.Oid);
                        obj.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(thongTin.ThongTinNhanVien.Oid);
                        obj.NgaySinh = thongTin.Ngay;
                        if (HamDungChung.CauHinhChung != null
                            && HamDungChung.CauHinhChung.CauHinhHoSo != null)
                            obj.SoTien = HamDungChung.CauHinhChung.CauHinhHoSo.QuaSinhNhat;
                    }
                    Application.ShowView<ChiTietTangQuaSinhNhat>(obs, obj);
                }
            }
        }
    }
}
