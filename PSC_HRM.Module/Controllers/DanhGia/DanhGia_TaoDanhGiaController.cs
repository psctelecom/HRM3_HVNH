using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using PSC_HRM.Module.DanhGia;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Utils;
using PSC_HRM.Module.DanhMuc;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Controller
{
    public partial class DanhGia_TaoDanhGiaController : ViewController
    {
        private QuanLyDanhGia quanLy;
        private IObjectSpace obs;
        private DanhGia_TaoDanhGia obj;

        public DanhGia_TaoDanhGiaController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("DanhGia_TaoDanhGiaController");
        }

        private void DanhGia_XuLyViPhamController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active[""] = HamDungChung.IsWriteGranted<QuanLyDanhGia>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            obs = Application.CreateObjectSpace();
            obj = obs.CreateObject<DanhGia_TaoDanhGia>();
            e.View = Application.CreateDetailView(obs, obj);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            quanLy = View.CurrentObject as QuanLyDanhGia;
            if (quanLy != null)
            {
                using (WaitDialogForm dialog = new WaitDialogForm("Đang xử lý dữ liệu", "Vui lòng chờ"))
                {
                    obs = Application.CreateObjectSpace();
                    quanLy = obs.GetObjectByKey<QuanLyDanhGia>(quanLy.Oid);

                    CriteriaOperator filter = CriteriaOperator.Parse("DoiTuongDanhGia=? and !LuuTru", obj.DoiTuongDanhGia.Oid);
                    using (XPCollection<NhomTieuChuanDanhGiaCaNhan> tcList = new XPCollection<NhomTieuChuanDanhGiaCaNhan>(((XPObjectSpace)obs).Session, filter))
                    {
                        filter = CriteriaOperator.Parse(obj.DoiTuongDanhGia.DieuKienApDung);
                        string temp = CriteriaToWhereClauseHelper.GetMsSqlWhere(filter, new DevExpress.Data.Db.MsSqlFormatterHelper.MSSqlServerVersion(false, false, true, false));
                        List<Guid> oid = HamDungChung.GetCriteria(obs, "spd_DieuKien_ThongTinNhanVien",
                                CommandType.StoredProcedure, obj.DoiTuongDanhGia.DieuKienApDung,
                                quanLy.NamHoc.NgayBatDau, quanLy.NamHoc.NgayKetThuc);
                        using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)obs).Session, new InOperator("Oid", oid)))
                        {
                            DanhGiaCaNhan caNhan;
                            ChiTietDanhGiaCaNhan chiTiet;
                            NhomTieuChuanDanhGiaCaNhan nhomTieuChuan;
                            TieuChuanDanhGiaCaNhan tieuChuan;
                            List<TieuChuanDanhGiaCaNhan> danhSach = new List<TieuChuanDanhGiaCaNhan>();
                            foreach (NhomTieuChuanDanhGiaCaNhan tcItem in tcList)
                            {
                                nhomTieuChuan = HamDungChung.Copy<NhomTieuChuanDanhGiaCaNhan>(((XPObjectSpace)obs).Session, tcItem);
                                nhomTieuChuan.LuuTru = true;
                                foreach (TieuChuanDanhGiaCaNhan ctItem in tcItem.ListTieuChuanDanhGiaCaNhan)
                                {
                                    tieuChuan = HamDungChung.Copy<TieuChuanDanhGiaCaNhan>(((XPObjectSpace)obs).Session, ctItem);
                                    tieuChuan.NhomTieuChuanDanhGiaCaNhan = nhomTieuChuan;
                                    tieuChuan.LuuTru = true;
                                    danhSach.Add(tieuChuan);
                                }
                            }

                            foreach (ThongTinNhanVien item in nvList)
                            {
                                filter = CriteriaOperator.Parse("QuanLyDanhGia=? and ThongTinNhanVien=?", quanLy.Oid, item.Oid);
                                caNhan = obs.FindObject<DanhGiaCaNhan>(filter);
                                if (caNhan == null)
                                {
                                    caNhan = obs.CreateObject<DanhGiaCaNhan>();
                                    caNhan.BoPhan = item.BoPhan;
                                    caNhan.ThongTinNhanVien = item;

                                    foreach (TieuChuanDanhGiaCaNhan ctItem in danhSach)
                                    {
                                        filter = CriteriaOperator.Parse("DanhGiaCaNhan=? and TieuChuanDanhGiaCaNhan.TenTieuChuanDanhGiaCaNhan=?", caNhan.Oid, ctItem.TenTieuChuanDanhGiaCaNhan);
                                        chiTiet = obs.FindObject<ChiTietDanhGiaCaNhan>(filter);
                                        if (chiTiet == null)
                                        {
                                            chiTiet = obs.CreateObject<ChiTietDanhGiaCaNhan>();
                                            chiTiet.TieuChuanDanhGiaCaNhan = ctItem;

                                            caNhan.ListChiTietDanhGiaCaNhan.Add(chiTiet);
                                        }
                                    }
                                    quanLy.ListDanhGiaCaNhan.Add(caNhan);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
