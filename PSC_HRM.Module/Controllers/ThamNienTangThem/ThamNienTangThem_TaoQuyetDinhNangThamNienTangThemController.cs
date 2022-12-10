using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NangThamNien;
using DevExpress.Data.Filtering;
using PSC_HRM.Module;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Utils;
using PSC_HRM.Module.NangThamNienTangThem;

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNienTangThem_TaoQuyetDinhNangThamNienTangThemController : ViewController
    {
        public ThamNienTangThem_TaoQuyetDinhNangThamNienTangThemController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNienTangThem_TaoQuyetDinhNangThamNienTangThemController");
        }

        private void ThamNienTangThem_TaoQuyetDinhNangThamNienTangThemController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangThamNienTangThem>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            {//Tiến hành lưu dữ liệu hiện tại
                XafApplicationHelper.CascadeCommit(View.ObjectSpace, false);
                View.ObjectSpace.Refresh();
            }
            //
            DeNghiNangThamNienTangThem deNghi = View.CurrentObject as DeNghiNangThamNienTangThem;
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();

                if (deNghi.ListChiTietDeNghiNangThamNienTangThem.Count > 1)
                {
                    if (HamDungChung.ShowMessage("Bạn có muốn lập mỗi quyết định cho 1 người không?", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                            //Tạo quyết định một người
                            CreateQuyetDinhMotNguoi(deNghi, obs);
                    }
                    else
                    {
                        //Tạo quyết định nhiều người
                        CreateQuyetDinhNhieuNguoi(deNghi, obs);
                    }
                }
                else
                {
                    //Tạo quyết định nhiều người
                    CreateQuyetDinhNhieuNguoi(deNghi, obs);
                }
            }
        }

        private void CreateQuyetDinhNhieuNguoi(DeNghiNangThamNienTangThem deNghi, IObjectSpace obs)
        {
            QuyetDinhNangThamNienTangThem quyetDinh;
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                quyetDinh = obs.FindObject<QuyetDinhNangThamNienTangThem>(CriteriaOperator.Parse("DeNghiNangThamNienTangThem=?", deNghi.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhNangThamNienTangThem>();
                    quyetDinh.DeNghiNangThamNienTangThem = obs.GetObjectByKey<DeNghiNangThamNienTangThem>(deNghi.Oid);
                    quyetDinh.QuyetDinhMoi = true;
                    //
                    ChiTietQuyetDinhNangThamNienTangThem chiTiet;
                    foreach (ChiTietDeNghiNangThamNienTangThem item in deNghi.ListChiTietDeNghiNangThamNienTangThem)
                    {
                        chiTiet = obs.FindObject<ChiTietQuyetDinhNangThamNienTangThem>(CriteriaOperator.Parse("QuyetDinhNangThamNienTangThem=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = obs.CreateObject<ChiTietQuyetDinhNangThamNienTangThem>();
                            chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                            chiTiet.HSLTangThemTheoThamNienCu = item.HSLTangThemTheoThamNienCu;
                            chiTiet.MocHuongThamNienTangThemCu = item.MocHuongThamNienTangThemCu;
                            chiTiet.HSLTangThemTheoThamNienMoi = item.HSLTangThemTheoThamNienMoi;
                            chiTiet.MocHuongThamNienTangThemMoi = item.MocHuongThamNienTangThemMoi;
                            quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem.Add(chiTiet);
                        }
                    }
                }
            }
            Application.ShowView<QuyetDinhNangThamNienTangThem>(obs, quyetDinh);
        }

        private static void CreateQuyetDinhMotNguoi(DeNghiNangThamNienTangThem deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();
                try
                {   int count = 0;
                    using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                    {

                        foreach (ChiTietDeNghiNangThamNienTangThem item in deNghi.ListChiTietDeNghiNangThamNienTangThem)
                        {
                            QuyetDinhNangThamNienTangThem quyetDinh = uow.FindObject<QuyetDinhNangThamNienTangThem>(CriteriaOperator.Parse("DeNghiNangThamNienTangThem=? and ListChiTietQuyetDinhNangThamNienTangThem[ThongTinNhanVien=?]", deNghi.Oid, item.ThongTinNhanVien.Oid));
                            if (quyetDinh == null)
                            {
                                quyetDinh = new QuyetDinhNangThamNienTangThem(uow);
                                quyetDinh.SoQuyetDinh = item.SoQuyetDinh;
                                quyetDinh.QuyetDinhMoi = true;
                                quyetDinh.DeNghiNangThamNienTangThem = uow.GetObjectByKey<DeNghiNangThamNienTangThem>(deNghi.Oid);

                                ChiTietQuyetDinhNangThamNienTangThem chiTiet = uow.FindObject<ChiTietQuyetDinhNangThamNienTangThem>(CriteriaOperator.Parse("QuyetDinhNangThamNienTangThem=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                                if (chiTiet == null)
                                {
                                    chiTiet = new ChiTietQuyetDinhNangThamNienTangThem(uow);
                                    chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                    chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                    chiTiet.HSLTangThemTheoThamNienCu = item.HSLTangThemTheoThamNienCu;
                                    chiTiet.MocHuongThamNienTangThemCu = item.MocHuongThamNienTangThemCu;
                                    chiTiet.HSLTangThemTheoThamNienMoi = item.HSLTangThemTheoThamNienMoi;
                                    chiTiet.MocHuongThamNienTangThemMoi = item.MocHuongThamNienTangThemMoi;
                                    //
                                    quyetDinh.ListChiTietQuyetDinhNangThamNienTangThem.Add(chiTiet);
                                }

                                {//Trường hợp nếu trường không có hiệu trưởng như đại học công nghiệp thì cấu hình người ký tên của quyết định trong bảng người sử dụng

                                    if (quyetDinh.NguoiKy == null && HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen != null)
                                    {
                                        quyetDinh.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.ChucVu.Oid);
                                        quyetDinh.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.Oid);

                                    }
                                }
                                count += 1;
                            }
                        }
                    }
                    uow.CommitChanges();
                    //
                    DialogUtil.ShowInfo(string.Format("Thành công! Đã tạo {0} quyết định nâng phụ cấp thâm niên nhà giáo.", count));
                }
                catch (Exception ex)
                {
                    uow.RollbackTransaction();
                    //
                    DialogUtil.ShowError("Đã xảy ra lỗi trong quá trình tạo quyết định." + ex.Message);
                }
            }
        }
    }
}
