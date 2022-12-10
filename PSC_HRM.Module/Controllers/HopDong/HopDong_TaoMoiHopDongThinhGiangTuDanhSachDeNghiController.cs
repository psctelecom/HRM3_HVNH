using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module;
using System.Data.SqlClient;
using PSC_HRM.Module.TaoMaQuanLy;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController : ViewController
    {
        private IObjectSpace _obs;
        private HopDong_TaoHopDongThinhGiang _chonHopDongThinhGiang;
        private QuanLyHopDongThinhGiang _quanLyHopDong;
        private QuanLyDeNghiMoiGiang _quanLyDeNghiMoiGiang;
        private HopDong.HopDong _hopDong;

        public HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController");
        }

        //reload listview after save object in detailvew
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            try
            {
                View.ObjectSpace.CommitChanges();

                //Cập nhật đã lập hợp đồng
                foreach (var item in _quanLyDeNghiMoiGiang.ListDeNghiMoiGiang)
                {
                    if (item.Chon && item.DongY && !item.LapHopDong)
                    {
                        item.LapHopDong = true;
                    }
                }
                //
                View.ObjectSpace.CommitChanges();
                View.ObjectSpace.Refresh();
            }
            catch(Exception ex)
            {
            
            }
        }

        private void HopDong_TaoMoiHopDongThinhGiangTuDanhSachDeNghiController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction2.Active["TruyCap"] =
                HamDungChung.IsWriteGranted<HopDong.HopDong>() &&
                HamDungChung.IsWriteGranted<HopDong_ThinhGiang>() &&
                HamDungChung.IsWriteGranted<HopDong_ThinhGiangChatLuongCao>();
        }

        private void popupWindowShowAction2_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //Lấy quản lý đề nghị mời giảng hiện tại
            _quanLyDeNghiMoiGiang = View.CurrentObject as QuanLyDeNghiMoiGiang;
            //
            if (_quanLyDeNghiMoiGiang != null)
            {
                //
                _obs = Application.CreateObjectSpace();
                //
                _chonHopDongThinhGiang = _obs.CreateObject<HopDong_TaoHopDongThinhGiang>();
              
                int soNguoi = 0;
                foreach (var item in _quanLyDeNghiMoiGiang.ListDeNghiMoiGiang)
                {
                    if(item.Chon)
                    {
                        soNguoi += 1;
                        //
                        _chonHopDongThinhGiang.LoaiHopDong = item.LoaiHopDong;
                    }
                }
                if (soNguoi > 1)
                {
                    DialogUtil.ShowError("Hợp đồng chỉ lập cho một cán bộ.");
                    return;
                }

                //Tìm quản lý hợp đồng
                CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and HocKy=?", _quanLyDeNghiMoiGiang.NamHoc.Oid, _quanLyDeNghiMoiGiang.HocKy.Oid);
                _quanLyHopDong = _obs.FindObject<QuanLyHopDongThinhGiang>(filter);
                if (_quanLyHopDong == null)
                {   // Tạo mới quản lý hợp đồng
                    _quanLyHopDong = _obs.CreateObject<QuanLyHopDongThinhGiang>();
                    _quanLyHopDong.NamHoc = _obs.GetObjectByKey<NamHoc>(_quanLyDeNghiMoiGiang.NamHoc.Oid);
                    _quanLyHopDong.HocKy = _obs.GetObjectByKey<HocKy>(_quanLyDeNghiMoiGiang.HocKy.Oid);
                }
                //
                e.View = Application.CreateDetailView(_obs, _chonHopDongThinhGiang);
            }
        }

        private void popupWindowShowAction2_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            e.PopupWindow.View.ObjectSpace.CommitChanges();
            _obs = Application.CreateObjectSpace();

            
            switch (_chonHopDongThinhGiang.LoaiHopDong)
            {
                case TaoHopDongThinhGiangEnum.HopDongThinhGiang:
                    _hopDong = _obs.CreateObject<HopDong_ThinhGiang>();
                    break;
                case TaoHopDongThinhGiangEnum.HopDongThinhGiangChatLuongCao:
                    _hopDong = _obs.CreateObject<HopDong_ThinhGiangChatLuongCao>();
                    break;
                default:
                    _hopDong = _obs.CreateObject<HopDong_ThinhGiang>();
                    break;
            }
            _hopDong.QuanLyHopDongThinhGiang = _obs.GetObjectByKey<QuanLyHopDongThinhGiang>(_quanLyHopDong.Oid);
            //Tạo số hợp đồng tự động
            if (_hopDong.QuanLyHopDongThinhGiang != null)
            {
                _hopDong.SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongThinhGiang);
            }
            //Lấy danh sách cán bộ
            foreach (var item in _quanLyDeNghiMoiGiang.ListDeNghiMoiGiang)
            {
                if (item.Chon && item.DongY && !item.LapHopDong)
                {
                    _hopDong.NhanVien = _obs.GetObjectByKey<NhanVien>(item.NhanVien.Oid);
                    _hopDong.BoPhan = _obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                    //Lấy danh sách môn
                    foreach (var itemMonDay in item.ListChiTietDeNghiMoiGiang)
                    {  
                        //Hợp đồng thỉnh giảng
                        if ((_hopDong as HopDong_ThinhGiang) != null)
                        {
                            ChiTietHopDongThinhGiang chiTiet = _obs.CreateObject<ChiTietHopDongThinhGiang>();
                            chiTiet.TaiKhoa = _obs.GetObjectByKey<BoPhan>(itemMonDay.TaiKhoa.Oid);
                            chiTiet.BoMon = _obs.GetObjectByKey<BoPhan>(itemMonDay.BoMon.Oid);
                            chiTiet.MonHoc = itemMonDay.MonHoc;

                            (_hopDong as HopDong_ThinhGiang).ListChiTietHopDongThinhGiang.Add(chiTiet);
                        }
                        //Hợp đồng thỉnh giảng chất lượng cao
                        if ((_hopDong as HopDong_ThinhGiangChatLuongCao) != null)
                        {
                            ChiTietHopDongThinhGiangChatLuongCao chiTiet = _obs.CreateObject<ChiTietHopDongThinhGiangChatLuongCao>();
                            chiTiet.MonHoc = itemMonDay.MonHoc;
                            //
                            (_hopDong as HopDong_ThinhGiangChatLuongCao).ListChiTietHopDongThinhGiangChatLuongCao.Add(chiTiet);
                        }
                    }
                }
            }
            e.ShowViewParameters.Context = TemplateContext.View;
            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, _hopDong);
            e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
        }
    }
}
