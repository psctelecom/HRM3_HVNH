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

namespace PSC_HRM.Module.Controllers
{
    public partial class ThamNien_TaoQuyetDinhNangPhuCapThamNienController : ViewController
    {
        public ThamNien_TaoQuyetDinhNangPhuCapThamNienController()
        {
            InitializeComponent();
            RegisterActions(components);
            HamDungChung.DebugTrace("ThamNien_TaoQuyetDinhNangPhuCapThamNienController");
        }

        private void ThamNien_TaoQuyetDinhNangPhuCapThamNienController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = HamDungChung.IsWriteGranted<QuyetDinhNangPhuCapThamNienNhaGiao>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            {//Tiến hành lưu dữ liệu hiện tại
                XafApplicationHelper.CascadeCommit(View.ObjectSpace, false);
                View.ObjectSpace.Refresh();
            }
            //
            DeNghiNangPhuCapThamNien deNghi = View.CurrentObject as DeNghiNangPhuCapThamNien;
            if (deNghi != null)
            {
                IObjectSpace obs = Application.CreateObjectSpace();

                if (deNghi.ListChiTietDeNghiNangPhuCapThamNien.Count > 1)
                {
                    if (HamDungChung.ShowMessage("Bạn có muốn lập mỗi quyết định cho 1 người không?", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        //Tạo quyết định một người
                        if (TruongConfig.MaTruong.Equals("BUH"))
                        {
                            CreateQuyetDinhMotNguoi_BUH(deNghi, obs);
                        }
                        else
                        {
                            CreateQuyetDinhMotNguoi(deNghi, obs);
                        }
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

        private void CreateQuyetDinhNhieuNguoi(DeNghiNangPhuCapThamNien deNghi, IObjectSpace obs)
        {
            QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh;
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                quyetDinh = obs.FindObject<QuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("DeNghiNangPhuCapThamNien=?", deNghi.Oid));
                if (quyetDinh == null)
                {
                    quyetDinh = obs.CreateObject<QuyetDinhNangPhuCapThamNienNhaGiao>();
                    quyetDinh.DeNghiNangPhuCapThamNien = obs.GetObjectByKey<DeNghiNangPhuCapThamNien>(deNghi.Oid);
                    quyetDinh.QuyetDinhMoi = true;
                    quyetDinh.Imporrt = true;
                    //
                    ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet;
                    foreach (ChiTietDeNghiNangPhuCapThamNien item in deNghi.ListChiTietDeNghiNangPhuCapThamNien)
                    {
                        chiTiet = obs.FindObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("QuyetDinhNangPhuCapThamNienNhaGiao=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = obs.CreateObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>();
                            chiTiet.BoPhan = obs.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = obs.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                            chiTiet.ThamNienCu = item.ThamNienCu;
                            chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                            chiTiet.ThamNienMoi = item.ThamNienMoi;
                            chiTiet.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi;
                            quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);
                        }
                    }
                }
            }
            Application.ShowView<QuyetDinhNangPhuCapThamNienNhaGiao>(obs, quyetDinh);
        }

        private static void CreateQuyetDinhMotNguoi(DeNghiNangPhuCapThamNien deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                uow.BeginTransaction();
                try
                {   int count = 0;
                    using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                    {

                        foreach (ChiTietDeNghiNangPhuCapThamNien item in deNghi.ListChiTietDeNghiNangPhuCapThamNien)
                        {
                            QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh = uow.FindObject<QuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("DeNghiNangPhuCapThamNien=? and ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[ThongTinNhanVien=?]", deNghi.Oid, item.ThongTinNhanVien.Oid));
                            if (quyetDinh == null)
                            {
                                quyetDinh = new QuyetDinhNangPhuCapThamNienNhaGiao(uow);
                                quyetDinh.SoQuyetDinh = item.SoQuyetDinh;
                                quyetDinh.QuyetDinhMoi = true;
                                quyetDinh.Imporrt = true;
                                quyetDinh.DeNghiNangPhuCapThamNien = uow.GetObjectByKey<DeNghiNangPhuCapThamNien>(deNghi.Oid);
                                
                                ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet = uow.FindObject<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>(CriteriaOperator.Parse("QuyetDinhNangPhuCapThamNienNhaGiao=? and ThongTinNhanVien=? and ThamNienMoi=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid, item.ThamNienMoi));
                                if (chiTiet == null)
                                {
                                    chiTiet = new ChiTietQuyetDinhNangPhuCapThamNienNhaGiao(uow);
                                    chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                    chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                    chiTiet.ThamNienCu = item.ThamNienCu;
                                    chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                                    chiTiet.ThamNienMoi = item.ThamNienMoi;
                                    chiTiet.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi;
                                    //
                                    quyetDinh.NgayPhatSinhBienDong = item.NgayHuongThamNienMoi;
                                    quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);
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

        //1 người 1 quyết định nhiều giai đoạn nâng thâm niên
        private static void CreateQuyetDinhMotNguoi_BUH(DeNghiNangPhuCapThamNien deNghi, IObjectSpace obs)
        {
            using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
            {
                XPCollection<QuyetDinhNangPhuCapThamNienNhaGiao> listQuyetDinh = new XPCollection<QuyetDinhNangPhuCapThamNienNhaGiao>(uow);
                XPCollection<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao> listChiTiet = new XPCollection<ChiTietQuyetDinhNangPhuCapThamNienNhaGiao>(uow);
                uow.BeginTransaction();
                try
                {
                    int count = 0;
                    using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                    {
                        foreach (ChiTietDeNghiNangPhuCapThamNien item in deNghi.ListChiTietDeNghiNangPhuCapThamNien)
                        {
                            listQuyetDinh.Filter = CriteriaOperator.Parse("DeNghiNangPhuCapThamNien=? and ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao[ThongTinNhanVien=?]", deNghi.Oid, item.ThongTinNhanVien.Oid);
                            QuyetDinhNangPhuCapThamNienNhaGiao quyetDinh = null;
                            if (listQuyetDinh.Count == 1)
                            {
                                quyetDinh = listQuyetDinh[0];
                                listChiTiet.Filter = CriteriaOperator.Parse("QuyetDinhNangPhuCapThamNienNhaGiao=? and ThongTinNhanVien=? and ThamNienMoi=?",
                                quyetDinh.Oid, item.ThongTinNhanVien.Oid, item.ThamNienMoi);
                            }

                            if (quyetDinh == null)
                            {
                                quyetDinh = new QuyetDinhNangPhuCapThamNienNhaGiao(uow);
                                quyetDinh.SoQuyetDinh = item.SoQuyetDinh;
                                quyetDinh.QuyetDinhMoi = true;
                                quyetDinh.DeNghiNangPhuCapThamNien = uow.GetObjectByKey<DeNghiNangPhuCapThamNien>(deNghi.Oid);
                                listQuyetDinh.Add(quyetDinh);
                            }

                            if (listChiTiet.Count != 1)
                            {
                                ChiTietQuyetDinhNangPhuCapThamNienNhaGiao chiTiet = new ChiTietQuyetDinhNangPhuCapThamNienNhaGiao(uow);
                                chiTiet.BoPhan = uow.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = uow.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                chiTiet.ThamNienCu = item.ThamNienCu;
                                chiTiet.NgayHuongThamNienCu = item.NgayHuongThamNienCu;
                                chiTiet.ThamNienMoi = item.ThamNienMoi;
                                chiTiet.NgayHuongThamNienMoi = item.NgayHuongThamNienMoi;
                                //
                                quyetDinh.NgayPhatSinhBienDong = item.NgayHuongThamNienMoi;
                                quyetDinh.ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);

                                listChiTiet.Add(chiTiet);
                                listQuyetDinh[0].ListChiTietQuyetDinhNangPhuCapThamNienNhaGiao.Add(chiTiet);
                                count += 1;
                            }

                            {//Trường hợp nếu trường không có hiệu trưởng như đại học công nghiệp thì cấu hình người ký tên của quyết định trong bảng người sử dụng

                                if (quyetDinh.NguoiKy == null && HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen != null)
                                {
                                    quyetDinh.ChucVuNguoiKy = uow.GetObjectByKey<ChucVu>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.ChucVu.Oid);
                                    quyetDinh.NguoiKy = uow.GetObjectByKey<ThongTinNhanVien>(HamDungChung.CauHinhChung.CauHinhQuyetDinh.NguoiKyTen.Oid);

                                }
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
