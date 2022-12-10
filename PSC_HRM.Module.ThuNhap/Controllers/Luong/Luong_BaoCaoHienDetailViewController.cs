using System;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.SystemModule;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.Report;
using DevExpress.XtraEditors;
using System.Text;
using System.Collections.Generic;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.ThuNhap.Luong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.ThuNhap.NonPersistentThuNhap;

namespace PSC_HRM.Module.ThuNhap.Controllers
{
    public partial class Luong_BaoCaoHienDetailViewController : ViewController
    {
        private IObjectSpace _obs;
        private KyTinhLuong _KyTinhLuong;
        private BangLuongNhanVien _bangLuongNhanVien;

        public Luong_BaoCaoHienDetailViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void Luong_BaoCaoHienDetailViewController_Activated(object sender, EventArgs e)
        {
            if (TruongConfig.MaTruong.Equals("IUH"))
            {
                singleChoiceAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangLuongNhanVien>();
                singleChoiceAction1.Items.Clear();
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Danh sách chi trả tiền hỗ trợ tiến sỹ", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng lương chi tiết kỳ 1", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng lương bộ phận kỳ 1", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Danh sách chi tiền phụ cấp trách nhiệm", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Danh sách phụ cấp thâm niên nhà giáo", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng lương chi tiết kỳ 2", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng lương bộ phận kỳ 2", ""));
            }
            else if (TruongConfig.MaTruong.Equals("LUH"))
            {
                singleChoiceAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangLuongNhanVien>();
                singleChoiceAction1.Items.Clear();
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng thanh toán tiền lương", ""));
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng tổng hợp tiền lương", ""));
            }
            else if (TruongConfig.MaTruong.Equals("NEU"))
            {
                singleChoiceAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<BangLuongNhanVien>();
                singleChoiceAction1.Items.Clear();
                singleChoiceAction1.Items.Add(new ChoiceActionItem("Bảng lương tháng", ""));
            }
 
            else
            {
                singleChoiceAction1.Active["TruyCap"] = false;
            }
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.SelectedChoiceActionItem.Caption == "Danh sách chi trả tiền hỗ trợ tiến sỹ")
                LayDSChiTraTienHoTroTienSy(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng lương chi tiết kỳ 1")
                LayBangLuongChiTietKy1(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng lương bộ phận kỳ 1")
                LayBangLuongBoPhanKy1(e);
            else if (e.SelectedChoiceActionItem.Caption == "Danh sách chi tiền phụ cấp trách nhiệm")
                LayDanhSachPhuCapTrachNhiem(e);
            else if (e.SelectedChoiceActionItem.Caption == "Danh sách phụ cấp thâm niên nhà giáo")
                LayDanhSachPhuCapThamNienNhaGiao(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng lương chi tiết kỳ 2")
                LayBangLuongChiTietKy2(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng lương bộ phận kỳ 2")
                LayBangLuongBoPhanKy2(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng thanh toán tiền lương")
                LayBangThanhToanTienLuongThangLUH(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng tổng hợp tiền lương")
                LayBangTongHopTienLuongThangLUH(e);
            else if (e.SelectedChoiceActionItem.Caption == "Bảng lương tháng")
                LayBangLuongThangNEU(e);
    
        
        }

        private void LayDSChiTraTienHoTroTienSy(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                PhuCap_HoTroTienSy dsPhuCapTienSy;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                dsPhuCapTienSy = _obs.CreateObject<PhuCap_HoTroTienSy>();
                dsPhuCapTienSy.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, dsPhuCapTienSy);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayBangLuongChiTietKy1(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangLuongChiTietKy1 bangChiTietLuongKy1;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangChiTietLuongKy1 = _obs.CreateObject<Luong_BangLuongChiTietKy1>();
                bangChiTietLuongKy1.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangChiTietLuongKy1);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayBangLuongBoPhanKy1(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangLuongBoPhanKy1 bangLuongBoPhanKy1;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangLuongBoPhanKy1 = _obs.CreateObject<Luong_BangLuongBoPhanKy1>();
                bangLuongBoPhanKy1.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangLuongBoPhanKy1);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayDanhSachPhuCapTrachNhiem(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                PhuCap_PhuCapTrachNhiem danhSachPhuCapTrachNhiem;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                danhSachPhuCapTrachNhiem = _obs.CreateObject<PhuCap_PhuCapTrachNhiem>();
                danhSachPhuCapTrachNhiem.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, danhSachPhuCapTrachNhiem);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayDanhSachPhuCapThamNienNhaGiao(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                PhuCap_PhuCapThamNienNhaGiao danhSachPhuCapThamNien;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                danhSachPhuCapThamNien = _obs.CreateObject<PhuCap_PhuCapThamNienNhaGiao>();
                danhSachPhuCapThamNien.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, danhSachPhuCapThamNien);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayBangLuongChiTietKy2(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangLuongChiTietKy2 bangChiTietLuongKy2;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangChiTietLuongKy2 = _obs.CreateObject<Luong_BangLuongChiTietKy2>();
                bangChiTietLuongKy2.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangChiTietLuongKy2);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayBangLuongBoPhanKy2(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangLuongBoPhanKy2 bangLuongBoPhanKy2;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangLuongBoPhanKy2 = _obs.CreateObject<Luong_BangLuongBoPhanKy2>();
                bangLuongBoPhanKy2.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangLuongBoPhanKy2);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayBangThanhToanTienLuongThangLUH(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangThanhToanTienLuongThang bangThanhToan;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangThanhToan = _obs.CreateObject<Luong_BangThanhToanTienLuongThang>();
                bangThanhToan.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangThanhToan);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }

        private void LayBangTongHopTienLuongThangLUH(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangTongHopTienLuongThang bangTongHop;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangTongHop = _obs.CreateObject<Luong_BangTongHopTienLuongThang>();
                bangTongHop.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangTongHop);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }


        private void LayBangLuongThangNEU(SingleChoiceActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                Luong_BangLuongThang bangLuongThang;
                _bangLuongNhanVien = View.CurrentObject as BangLuongNhanVien;
                _KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_bangLuongNhanVien.KyTinhLuong.Oid);
                bangLuongThang = _obs.CreateObject<Luong_BangLuongThang>();
                bangLuongThang.KyTinhLuong = _KyTinhLuong;

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(_obs, bangLuongThang);
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }
    }
}
