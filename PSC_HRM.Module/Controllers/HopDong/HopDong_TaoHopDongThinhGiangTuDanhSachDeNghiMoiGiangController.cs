using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.XuLyQuyTrinh.HopDong;
using PSC_HRM.Module.HopDong;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.TaoMaQuanLy;


namespace PSC_HRM.Module.Controllers
{
    public partial class HopDong_TaoHopDongThinhGiangTuDanhSachDeNghiMoiGiangController : ViewController
    {
        QuanLyDeNghiMoiGiang _quanLyDeNghiMoiGiang;

        public HopDong_TaoHopDongThinhGiangTuDanhSachDeNghiMoiGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("HopDong_TaoHopDongThinhGiangTuDanhSachDeNghiMoiGiangController");
        }

        private void HopDong_TaoHopDongThinhGiangTuDanhSachDeNghiMoiGiangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuanLyDeNghiMoiGiang>()
                && HamDungChung.IsCreateGranted<DeNghiMoiGiang>() && HamDungChung.IsCreateGranted<ChiTietDeNghiMoiGiang>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu hiện tại lại
            View.ObjectSpace.CommitChanges();

            //Lấy quản lý đề nghị mời giảng hiện tại
            _quanLyDeNghiMoiGiang = View.CurrentObject as QuanLyDeNghiMoiGiang;
            //
            if (_quanLyDeNghiMoiGiang != null)
            {
                //
                IObjectSpace obs = Application.CreateObjectSpace();

                int soNguoi = 0;
                foreach (var item in _quanLyDeNghiMoiGiang.ListDeNghiMoiGiang)
                {
                    if (item.Chon)
                    {
                        soNguoi += 1;
                    }
                }
                if (soNguoi > 1)
                {
                    DialogUtil.ShowError("Hợp đồng chỉ lập cho một cán bộ.");
                    return;
                }
             
                //
                foreach (var itemDeNghiMoiGiang in _quanLyDeNghiMoiGiang.ListDeNghiMoiGiang)
                {
                    if (itemDeNghiMoiGiang.Chon && itemDeNghiMoiGiang.DongY && !itemDeNghiMoiGiang.LapHopDong)
                    {
                        HopDong.HopDong hopDong = null;

                        //Tìm quản lý hợp đồng
                        CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and HocKy=?", _quanLyDeNghiMoiGiang.NamHoc.Oid, _quanLyDeNghiMoiGiang.HocKy.Oid);
                        QuanLyHopDongThinhGiang quanLyHopDong = obs.FindObject<QuanLyHopDongThinhGiang>(filter);
                        if (quanLyHopDong == null)
                        {   // Tạo mới quản lý hợp đồng
                            quanLyHopDong = obs.CreateObject<QuanLyHopDongThinhGiang>();
                            quanLyHopDong.NamHoc = obs.GetObjectByKey<NamHoc>(_quanLyDeNghiMoiGiang.NamHoc.Oid);
                            quanLyHopDong.HocKy = obs.GetObjectByKey<HocKy>(_quanLyDeNghiMoiGiang.HocKy.Oid);
                        }

                        //Hợp đồng thỉnh giảng
                        if (itemDeNghiMoiGiang.LoaiHopDong == TaoHopDongThinhGiangEnum.HopDongThinhGiang)
                        {
                            hopDong = obs.CreateObject<HopDong_ThinhGiang>();
                            hopDong.QuanLyHopDongThinhGiang = quanLyHopDong;
                            hopDong.SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongThinhGiang);
                            hopDong.NhanVien = obs.GetObjectByKey<NhanVien>(itemDeNghiMoiGiang.NhanVien.Oid);
                            hopDong.BoPhan = obs.GetObjectByKey<BoPhan>(itemDeNghiMoiGiang.BoPhan.Oid);
                            hopDong.NoiLamViec = itemDeNghiMoiGiang.NoiLamViec;
                            //UTE Số tiền 1 tiết =Dơn giá đề nghị mời giảng
                            if (itemDeNghiMoiGiang.TinhTienTheoDonGiaCLC == false)
                                (hopDong as HopDong_ThinhGiang).SoTien1Tiet = itemDeNghiMoiGiang.DonGia;
                            else
                                (hopDong as HopDong_ThinhGiang).SoTien1Tiet = itemDeNghiMoiGiang.DonGiaCLC;
                            
                            //Lấy danh sách môn
                            foreach (var itemMonDay in itemDeNghiMoiGiang.ListChiTietDeNghiMoiGiang)
                            {
                               
                                    ChiTietHopDongThinhGiang chiTiet = obs.CreateObject<ChiTietHopDongThinhGiang>();
                                    chiTiet.TaiKhoa = obs.GetObjectByKey<BoPhan>(itemMonDay.TaiKhoa.Oid);
                                    if(itemMonDay.BoMon != null)
                                        chiTiet.BoMon = obs.GetObjectByKey<BoPhan>(itemMonDay.BoMon.Oid);
                                    chiTiet.MonHoc = itemMonDay.MonHoc;
                                    //
                                    (hopDong as HopDong_ThinhGiang).ListChiTietHopDongThinhGiang.Add(chiTiet);
                            }
                        }
                        //Hợp đồng thỉnh giảng chất lượng cao
                        if (itemDeNghiMoiGiang.LoaiHopDong == TaoHopDongThinhGiangEnum.HopDongThinhGiangChatLuongCao)
                        {
                            hopDong = obs.CreateObject<HopDong_ThinhGiangChatLuongCao>();
                            hopDong.QuanLyHopDongThinhGiang = quanLyHopDong;
                            hopDong.SoHopDong = MaQuanLyFactory.TaoMaQuanLy(MaQuanLyTypeEnum.SoHopDongThinhGiang);
                            hopDong.NhanVien = obs.GetObjectByKey<NhanVien>(itemDeNghiMoiGiang.NhanVien.Oid);
                            hopDong.BoPhan = obs.GetObjectByKey<BoPhan>(itemDeNghiMoiGiang.BoPhan.Oid);
                            hopDong.NoiLamViec = itemDeNghiMoiGiang.NoiLamViec;
                            //Lấy danh sách môn
                            foreach (var itemMonDay in itemDeNghiMoiGiang.ListChiTietDeNghiMoiGiang)
                            {
                                ChiTietHopDongThinhGiangChatLuongCao chiTiet = obs.CreateObject<ChiTietHopDongThinhGiangChatLuongCao>();
                                chiTiet.MonHoc = itemMonDay.MonHoc;
                                //
                                (hopDong as HopDong_ThinhGiangChatLuongCao).ListChiTietHopDongThinhGiangChatLuongCao.Add(chiTiet);
                            }
                        }

                        //Mở form tạo hợp đồng
                        e.ShowViewParameters.Context = TemplateContext.View;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, hopDong);
                        e.ShowViewParameters.CreatedView.ObjectSpace.Committed += ObjectSpace_Committed;
                    }
                }
            }
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
            catch (Exception ex)
            {

            }
        }
    }
}
