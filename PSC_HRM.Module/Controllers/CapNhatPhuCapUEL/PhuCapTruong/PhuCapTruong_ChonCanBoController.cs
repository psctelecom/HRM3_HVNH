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

namespace PSC_HRM.Module.Controllers
{
    public partial class PhuCapTruong_ChonCanBoController : ViewController
    {
        private IObjectSpace obs;
        private HoSo_ChonNhanVien chonNhanVien;
        private Session ses;

        public PhuCapTruong_ChonCanBoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            DanhSachPhuCapTruong qd = View.CurrentObject as DanhSachPhuCapTruong;
            if (qd != null)
            {
                obs = Application.CreateObjectSpace();
                ses = ((XPObjectSpace)obs).Session;
                chonNhanVien = obs.CreateObject<HoSo_ChonNhanVien>();

                InOperator filter = new InOperator("Oid", HamDungChung.GetCriteriaBoPhan());

                XPCollection<BoPhan> BoPhanList = new XPCollection<BoPhan>(ses, filter);

                foreach (var bp in BoPhanList)
                {
                    CriteriaOperator filter1= CriteriaOperator.Parse("BoPhan =?",bp.Oid);
                    XPCollection<ThongTinNhanVien> nvList =  new XPCollection<ThongTinNhanVien>(ses, filter1);
                    foreach (var nv in nvList)
                    {
                        HoSo_NhanVienItem nhanVien = new HoSo_NhanVienItem(ses);
                        nhanVien.Chon = true;
                        nhanVien.BoPhan = obs.GetObjectByKey<BoPhan>(bp.Oid);
                        nhanVien.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(nv.Oid);
                        chonNhanVien.ListNhanVien.Add(nhanVien);
                    }
                }
                e.View = Application.CreateDetailView(obs, chonNhanVien);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            DanhSachPhuCapTruong danhSach = View.CurrentObject as DanhSachPhuCapTruong;
            if (danhSach != null)
            {
                foreach (var pcTruong in danhSach.ListTrachNhiem)
                {
                    PhuCapTruong1 trachNhiem;

                    int nam, thangHSPCTrachNhiem, thangHSPCThamNien, thang1;
                    CriteriaOperator filter;
                    foreach (var item in chonNhanVien.ListNhanVien)
                    {
                        if (!danhSach.IsExist(item.ThongTinNhanVien))
                        {
                            if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                                thangHSPCTrachNhiem = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.TinhSoThang(danhSach.DenNgay);
                            else
                                thangHSPCTrachNhiem = item.ThongTinNhanVien.NgayVaoCoQuan.TinhSoThang(danhSach.DenNgay);
                            thangHSPCThamNien = item.ThongTinNhanVien.NgayVaoCoQuan.TinhSoThang(danhSach.DenNgay);
                            nam = thangHSPCThamNien / 12;
                            thang1 = thangHSPCThamNien % 12;

                            if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue &&
                                item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem > 0)
                                filter = CriteriaOperator.Parse("TuThang<=? and DenThang>=? and HSPCTrachNhiem>?",
                                    thangHSPCTrachNhiem, thangHSPCTrachNhiem, item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem);
                            else
                                filter = CriteriaOperator.Parse("TuThang<=? and DenThang>=?",
                                    thangHSPCTrachNhiem, thangHSPCTrachNhiem);
                            HeSoPhuCapTrachNhiem HSPCTrachNhiem = ses.FindObject<HeSoPhuCapTrachNhiem>(filter);
                            HeSoPhuCapThamNien HSPCThamNien = ses.FindObject<HeSoPhuCapThamNien>(CriteriaOperator.Parse("TuNam<=? and DenNam>=?", nam, nam));
                            trachNhiem = new PhuCapTruong1(ses);
                            trachNhiem.BoPhan = item.ThongTinNhanVien.BoPhan;
                            trachNhiem.ThongTinNhanVien = item.ThongTinNhanVien;
                            trachNhiem.NgayVaoCoQuan = item.ThongTinNhanVien.NgayVaoCoQuan;
                            trachNhiem.ThoiGianCongTac = danhSach.ThoiGianCongTac(nam, thang1);
                            trachNhiem.HSPCLanhDao = item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCLanhDao;
                            trachNhiem.HSPCKiemNhiem = item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCKiemNhiemTrongTruong;
                            trachNhiem.HSPCChuyenMon = item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCChuyenMon;
                            trachNhiem.HSPCTrachNhiemCu = item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCTrachNhiem;
                            trachNhiem.HSPCThamNienCu = item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNienTrongTruong;
                            //trachNhiem.NgayHuongHSPCThamNien = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCThamNien;
                            //trachNhiem.NgayHuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem;
                            trachNhiem.HSPCKhac = item.ThongTinNhanVien.NhanVienThongTinLuong.HSPCKhac;

                            if (HSPCThamNien != null && trachNhiem.HSPCThamNienCu != HSPCThamNien.HSPCThamNien)
                            {
                                trachNhiem.HSPCThamNien = HSPCThamNien.HSPCThamNien;
                                trachNhiem.NgayHuongHSPCThamNien = item.ThongTinNhanVien.NgayVaoCoQuan.AddYears(nam);
                            }
                            else
                                trachNhiem.HSPCThamNien = trachNhiem.HSPCThamNienCu;
                            if (HSPCTrachNhiem != null && trachNhiem.HSPCTrachNhiemCu != HSPCTrachNhiem.HSPCTrachNhiem)
                            {
                                trachNhiem.HSPCTrachNhiem = HSPCTrachNhiem.HSPCTrachNhiem;
                                if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem == DateTime.MinValue &&
                                        item.ThongTinNhanVien.NgayVaoCoQuan != DateTime.MinValue)
                                    trachNhiem.NgayHuong = item.ThongTinNhanVien.NgayVaoCoQuan.AddMonths(HSPCTrachNhiem.TuThang);
                                else if (item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                                    trachNhiem.NgayHuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.AddMonths(HSPCTrachNhiem.TuThang);
                                //filter = CriteriaOperator.Parse("HSPCTrachNhiem<?", HSPCTrachNhiem.HSPCTrachNhiem);
                                //SortProperty sort = new SortProperty("HSPCTrachNhiem", DevExpress.Xpo.DB.SortingDirection.Descending);
                                //using (XPCollection<HeSoPhuCapTrachNhiem> pcTrachNhiemList = new XPCollection<HeSoPhuCapTrachNhiem>(Session, filter, sort))
                                //{
                                //    pcTrachNhiemList.TopReturnedObjects = 1;

                                //    if (pcTrachNhiemList.Count == 0 &&
                                //        item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem == DateTime.MinValue &&
                                //        item.ThongTinNhanVien.NgayVaoCoQuan != DateTime.MinValue)
                                //        trachNhiem.NgayHuong = item.ThongTinNhanVien.NgayVaoCoQuan.AddMonths(HSPCTrachNhiem.TuThang);
                                //    else if (pcTrachNhiemList.Count == 1 && item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem != DateTime.MinValue)
                                //        trachNhiem.NgayHuong = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCTracNhiem.AddMonths(HSPCTrachNhiem.TuThang - pcTrachNhiemList[0].TuThang);
                                //}
                            }
                            else
                                trachNhiem.HSPCTrachNhiem = trachNhiem.HSPCTrachNhiemCu;

                            ////chuyen mon nếu mới vào ngành giáo dục được 6 tháng
                            //object hsTrachNhiem = Session.Evaluate<HeSoPhuCapTrachNhiem>(CriteriaOperator.Parse("Min(HSPCTrachNhiem)"),
                            //    CriteriaOperator.Parse(""));
                            //if (hsTrachNhiem != null)
                            //{
                            HeSoChuyenMon hsChuyenMon = obs.FindObject<HeSoChuyenMon>(CriteriaOperator.Parse("TrinhDoChuyenMon=?", item.ThongTinNhanVien.NhanVienTrinhDo.TrinhDoChuyenMon));
                            if (hsChuyenMon != null && hsChuyenMon.HSPCChuyenMon > trachNhiem.HSPCChuyenMon)
                            {
                                trachNhiem.HSPCChuyenMon = hsChuyenMon.HSPCChuyenMon;
                                trachNhiem.NgayHuongHSPCChuyenMon = trachNhiem.NgayHuong;
                            }
                            else
                                trachNhiem.NgayHuongHSPCChuyenMon = item.ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongHSPCChuyenMon;

                            //chỉ lấy những người có hspc trách nhiệm < 2
                            //if ((HSPCThamNien != null && HSPCThamNien.HSPCThamNien != trachNhiem.HSPCThamNienCu) ||
                            //    (HSPCTrachNhiem != null && HSPCTrachNhiem.HSPCTrachNhiem != trachNhiem.HSPCTrachNhiemCu) ||
                            //    (trachNhiem.NgayHuongHSPCChuyenMon >= danhSach.TuNgay && trachNhiem.NgayHuongHSPCChuyenMon <= danhSach.DenNgay && trachNhiem.HSPCThamNien >= trachNhiem.HSPCThamNienCu && trachNhiem.HSPCTrachNhiem >= trachNhiem.HSPCTrachNhiemCu))
                            danhSach.ListTrachNhiem.Add(trachNhiem);
                        }
                    }
                }
            }
        }
    }
}
