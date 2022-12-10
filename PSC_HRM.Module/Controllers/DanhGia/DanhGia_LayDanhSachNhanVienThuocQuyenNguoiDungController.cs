using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using DevExpress.Utils;
using PSC_HRM.Module.ChamCong;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGia_LayDanhSachNhanVienThuocQuyenNguoiDungController : ViewController
    {
        private IObjectSpace _obs;
        private BoPhan_ChonBoPhan _danhSachBoPhan;
        private DanhGiaCanBoCuoiNam _danhGia;
        private XPCollection<BoPhan> _boPhanList;

        public DanhGia_LayDanhSachNhanVienThuocQuyenNguoiDungController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_LayDanhSachNhanVienThuocQuyenNguoiDungController");
        }

        private void DanhGia_LayDanhSachNhanVienThuocQuyenNguoiDungController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<DanhGiaCanBoCuoiNam>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _danhGia = View.CurrentObject as DanhGiaCanBoCuoiNam;
            if (_danhGia != null)
            {
                _obs = Application.CreateObjectSpace();
                BoPhan_ChonBoPhanItem boPhan;
                _danhSachBoPhan = _obs.CreateObject<BoPhan_ChonBoPhan>();

                //Lấy bộ phận thuộc quyền người dùng
                BoPhanDuocPhanQuyenList();
                //
                foreach (BoPhan item in _boPhanList)
                {
                    boPhan = _obs.CreateObject<BoPhan_ChonBoPhanItem>();
                    boPhan.BoPhan = _obs.GetObjectByKey<BoPhan>(item.Oid);
                    _danhSachBoPhan.ListBoPhan.Add(boPhan);
                }
                e.View = Application.CreateDetailView(_obs, _danhSachBoPhan);
            }
            else
                HamDungChung.ShowWarningMessage("Chưa chọn năm đánh giá.");
        }

        //Lấy bộ phận thuộc quyền người dùng
        private void BoPhanDuocPhanQuyenList()
        {
            if (_boPhanList == null)
                _boPhanList = new XPCollection<BoPhan>(((XPObjectSpace)_obs).Session);
            GroupOperator go = new GroupOperator();
            go.Operands.Add(new InOperator("Oid", HamDungChung.GetCriteriaBoPhan()));

            _boPhanList.Criteria = go;
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            _obs = View.ObjectSpace;
            //Lấy từng bộ phận được chọn
            foreach (BoPhan_ChonBoPhanItem item in _danhSachBoPhan.ListBoPhan)
            {
                if (item.Chon)
                {
                    //Lấy nhân viên thuộc bộ phận
                    CriteriaOperator filter = CriteriaOperator.Parse("BoPhan=? And TinhTrang.KhongConCongTacTaiTruong=false", item.BoPhan.Oid);
                    XPCollection<ThongTinNhanVien> thongTinNhanVienList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)_obs).Session, filter);
                    foreach (ThongTinNhanVien nhanVien in thongTinNhanVienList)
                    {
                        ChiTietDanhGiaCanBoCuoiNamLan1 chiTiet = _obs.FindObject<ChiTietDanhGiaCanBoCuoiNamLan1>(CriteriaOperator.Parse("DanhGiaCanBoCuoiNamLan1=? and ThongTinNhanVien=? and BoPhan = ?", _danhGia.Oid, nhanVien.Oid, item.BoPhan.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = _obs.CreateObject<ChiTietDanhGiaCanBoCuoiNamLan1>();
                            chiTiet.BoPhan = _obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = _obs.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                            _danhGia.ListChiTietDanhGiaCanBoCuoiNamLan1.Add(chiTiet);
                        }
                    }
                }
            }
            //Đưa danh sách chi tiết đánh giá cán bộ lên view
            View.Refresh();
        }
    }
}
