using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Xpo;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_ThanhLyHopDongThinhGiangController : ViewController
    {
        HopDong_ThinhGiang HopDongThinhGiang;
        HopDong_ThinhGiangChatLuongCao HopDongThinhGiangChatLuongCao;
        ChiTietThanhLyHopDongThinhGiang ChiTietThanhLyHopDongThinhGiang;
        ChiTietThanhLyHopDongThinhGiangChatLuongCao ChiTietThanhLyHopDongThinhGiangChatLuongCao;
        
        public HopDong_ThanhLyHopDongThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_ThanhLyHopDongThinhGiangController");
        }


        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            
            View.ObjectSpace.CommitChanges();
            IObjectSpace obs = Application.CreateObjectSpace();

            HopDong.HopDong hopDong = View.CurrentObject as HopDong.HopDong;

            NamHoc namHoc = HamDungChung.GetCurrentNamHoc((((XPObjectSpace)obs).Session));
            HocKy hocKy = HamDungChung.GetCurrentHocKy((((XPObjectSpace)obs).Session));

            if (hopDong != null)
            {
                HopDongThinhGiang = obs.GetObjectByKey<HopDong_ThinhGiang>(hopDong.Oid);
                if (HopDongThinhGiang != null)
                {
                    ChiTietThanhLyHopDongThinhGiang = obs.CreateObject<ChiTietThanhLyHopDongThinhGiang>();


                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and HocKy=?", namHoc.Oid, hocKy.Oid);
                    QuanLyThanhLyHopDongThinhGiang quanLyThanhLyHopDong = obs.FindObject<QuanLyThanhLyHopDongThinhGiang>(filter);
                    if (quanLyThanhLyHopDong == null)
                    {   // Tạo mới quản lý hợp đồng
                        quanLyThanhLyHopDong = obs.CreateObject<QuanLyThanhLyHopDongThinhGiang>();
                        quanLyThanhLyHopDong.NamHoc = obs.GetObjectByKey<NamHoc>(namHoc.Oid);
                        quanLyThanhLyHopDong.HocKy = obs.GetObjectByKey<HocKy>(hocKy.Oid);
                    }
                    ChiTietThanhLyHopDongThinhGiang.QuanLyThanhLyHopDongThinhGiang = quanLyThanhLyHopDong;

                    ChiTietThanhLyHopDongThinhGiang.HopDongThinhGiang = HopDongThinhGiang;
                    Application.ShowView<ChiTietThanhLyHopDongThinhGiang>(obs, ChiTietThanhLyHopDongThinhGiang);
                }
                else
                {
                    HopDongThinhGiangChatLuongCao = obs.GetObjectByKey<HopDong_ThinhGiangChatLuongCao>(hopDong.Oid);
                    ChiTietThanhLyHopDongThinhGiangChatLuongCao = obs.CreateObject<ChiTietThanhLyHopDongThinhGiangChatLuongCao>();


                    QuanLyHopDongThinhGiang quanLyHopDongThinhGiang = obs.GetObjectByKey<QuanLyHopDongThinhGiang>(hopDong.QuanLyHopDongThinhGiang.Oid);
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and HocKy=?", namHoc.Oid, hocKy.Oid);
                    QuanLyThanhLyHopDongThinhGiangChatLuongCao quanLyThanhLyHopDong = obs.FindObject<QuanLyThanhLyHopDongThinhGiangChatLuongCao>(filter);
                    if (quanLyThanhLyHopDong == null)
                    {   // Tạo mới quản lý hợp đồng
                        quanLyThanhLyHopDong = obs.CreateObject<QuanLyThanhLyHopDongThinhGiangChatLuongCao>();
                        quanLyThanhLyHopDong.NamHoc = obs.GetObjectByKey<NamHoc>(namHoc.Oid);
                        quanLyThanhLyHopDong.HocKy = obs.GetObjectByKey<HocKy>(hocKy.Oid);
                    }
                    ChiTietThanhLyHopDongThinhGiangChatLuongCao.QuanLyThanhLyHopDongThinhGiangChatLuongCao = quanLyThanhLyHopDong;

                    ChiTietThanhLyHopDongThinhGiangChatLuongCao.HopDongThinhGiangChatLuongCao = HopDongThinhGiangChatLuongCao;
                    Application.ShowView<ChiTietThanhLyHopDongThinhGiangChatLuongCao>(obs, ChiTietThanhLyHopDongThinhGiangChatLuongCao);
                }
            }

        }

        private void HopDong_ThanhLyHopDongThinhGiangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyHopDongThinhGiang>()
            && HamDungChung.IsCreateGranted<QuanLyHopDongThinhGiang>();
        }
    }
}
