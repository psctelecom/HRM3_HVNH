using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.NonPersistentObjects;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.NangThamNien;
using PSC_HRM.Module;
using PSC_HRM.Module.PhuCapTruong;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PhuCapTruong1 = PSC_HRM.Module.PhuCapTruong.PhuCapTruong;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.DanhGia;
using PSC_HRM.Module.ChotThongTinTinhLuong;

namespace PSC_HRM.Module.Controllers
{
    public partial class DanhGiaCanBo_ChonCanBoController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien chonNhanVien;
        private Session ses;

        public DanhGiaCanBo_ChonCanBoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            DanhGiaCanBo qd = View.CurrentObject as DanhGiaCanBo;
            if (qd != null)
            {
                obs = Application.CreateObjectSpace();
                ses = ((XPObjectSpace)obs).Session;
                chonNhanVien = obs.CreateObject<HoSo_ChonNhanVien>();

                int thangDanhGia = qd.ThangNam.Month;
                int namDanhGia = qd.ThangNam.Year;

                InOperator filter = new InOperator("Oid", HamDungChung.GetCriteriaBoPhan());

                XPCollection<BoPhan> BoPhanList = new XPCollection<BoPhan>(ses, filter);

                foreach (var bp in BoPhanList)
                {
                    //CriteriaOperator filter1= CriteriaOperator.Parse("BoPhan =?",bp.Oid);
                    //XPCollection<ThongTinNhanVien> nvList =  new XPCollection<ThongTinNhanVien>(ses, filter1);
                    //foreach (var nv in nvList)
                    //{
                    //    HoSo_NhanVienItem nhanVien = new HoSo_NhanVienItem(ses);
                    //    nhanVien.Chon = true;
                    //    nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(bp.Oid);
                    //    nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nv.Oid);
                    //    chonNhanVien.ListNhanVien.Add(nhanVien);
                    //}//old

                    //Nguyen 7-20-2021
                    XPCollection<BangChotThongTinTinhLuong> bangChotList = new XPCollection<BangChotThongTinTinhLuong>(ses);
                    foreach (var bc in bangChotList)
                    {
                        if (bc.Thang.Month == thangDanhGia && bc.Thang.Year == namDanhGia)
                        {
                            CriteriaOperator filter2 = CriteriaOperator.Parse("BangChotThongTinTinhLuong =? and BoPhan =?", bc.Oid, bp.Oid);
                            XPCollection<ThongTinTinhLuong> nvList2 = new XPCollection<ThongTinTinhLuong>(ses, filter2);
                            foreach (var nv2 in nvList2)
                            {
                                HoSo_NhanVienItem nhanVien = new HoSo_NhanVienItem(ses);
                                nhanVien.Chon = true;
                                nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(bp.Oid);
                                nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nv2.ThongTinNhanVien.Oid);
                                chonNhanVien.ListNhanVien.Add(nhanVien);
                            }
                        }
                    }
                }
                e.View = Application.CreateDetailView(obs, chonNhanVien);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DanhGiaCanBo danhSach = View.CurrentObject as DanhGiaCanBo;
            if (danhSach != null)
            {
                obs = View.ObjectSpace;
                DanhGiaLan1 danhGia;
                foreach (HoSo_NhanVienItem item in chonNhanVien.ListNhanVien)
                {
                    if (item.Chon)
                    {
                        danhGia = obs.FindObject<DanhGiaLan1>(CriteriaOperator.Parse("ThongTinNhanVien=? and DanhGiaCanBo.ThangNam=?", item.ThongTinNhanVien.Oid, danhSach.ThangNam));
                        if (danhGia == null)
                        {
                            danhGia = obs.CreateObject<DanhGiaLan1>();
                            danhGia.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            danhGia.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                            danhGia.XepLoai = obs.FindObject<XepLoaiCanBo>(CriteriaOperator.Parse("TenXepLoai=?", "A"));
                            danhSach.DanhSachDanhGiaLan1.Add(danhGia);
                        }
                    }
                }
            }
        }
    }
}
