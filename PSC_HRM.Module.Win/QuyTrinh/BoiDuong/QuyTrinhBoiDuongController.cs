using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.BoiDuong;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using PSC_HRM.Module.HoSo;
using DevExpress.Xpo;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.BoiDuong
{
    public partial class QuyTrinhBoiDuongController : QuyTrinhBaseController
    {
        private QuanLyBoiDuong _QuanLyBoiDuong;

        public QuyTrinhBoiDuongController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình bồi dưỡng");
        }

        private void QuyTrinhBoiDuongController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhBoiDuong);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyBoiDuong = ObjectSpace.GetObjectByKey<QuanLyBoiDuong>(obj);
                if (_QuanLyBoiDuong != null && _QuanLyBoiDuong.NamHoc != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình bồi dưỡng năm học " + _QuanLyBoiDuong.NamHoc.TenNamHoc);
                }
            }
            else
            {
                EndQuyTrinh();
                SetNotification("Nhấn nút 'Bắt đầu' để bắt đầu quy trình mới");
            }
        }

        protected override void btnBatDau_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyBoiDuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyBoiDuong>> popup = new frmPopUp<ChonDuLieuController<QuanLyBoiDuong>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyBoiDuong>(Application, ObjectSpace, "Quản lý bồi dưỡng", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý bồi dưỡng", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyBoiDuong = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyBoiDuong))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình bồi dưỡng năm học " + _QuanLyBoiDuong.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnDangKyBoiDuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DangKyBoiDuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DangKyBoiDuongController> popup = new frmPopUp<DangKyBoiDuongController>(Application, ObjectSpace, new DangKyBoiDuongController(ObjectSpace, _QuanLyBoiDuong, false, false), "Đăng ký bồi dưỡng", "Chọn", true);
                popup.Size = new System.Drawing.Size(320, 500);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ObjectSpace = Application.CreateObjectSpace();
                    ChuongTrinhBoiDuong chuongTrinhBoiDuong = popup.CurrentControl.GetChuongTrinhBoiDuong();
                    DangKyBoiDuong dangKy = ObjectSpace.FindObject<DangKyBoiDuong>(CriteriaOperator.Parse("QuanLyBoiDuong=? and ChuongTrinhBoiDuong=?", _QuanLyBoiDuong.Oid, chuongTrinhBoiDuong.Oid));

                    if (dangKy == null)
                    {
                        dangKy = ObjectSpace.CreateObject<DangKyBoiDuong>();
                        dangKy.QuanLyBoiDuong = ObjectSpace.GetObjectByKey<QuanLyBoiDuong>(_QuanLyBoiDuong.Oid);
                        dangKy.ChuongTrinhBoiDuong = ObjectSpace.GetObjectByKey<ChuongTrinhBoiDuong>(chuongTrinhBoiDuong.Oid);

                        List<Guid> oid = popup.CurrentControl.GetNhanVienList();
                        using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid)))
                        {
                            ChiTietDangKyBoiDuong chiTiet;
                            foreach (ThongTinNhanVien thongTinNhanVien in nvList)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietDangKyBoiDuong>();
                                chiTiet.BoPhan = thongTinNhanVien.BoPhan;
                                chiTiet.ThongTinNhanVien = thongTinNhanVien;

                                dangKy.ListChiTietDangKyBoiDuong.Add(chiTiet);
                            }
                        }
                    }
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnDangKyBoiDuong.Caption);
                    Application.ShowView<DangKyBoiDuong>(ObjectSpace, dangKy);
                    SetNotification("Đăng ký bồi dưỡng thành công.");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDanhSachBoiDuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DangKyBoiDuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DangKyBoiDuong>> popup = new frmPopUp<ChonDuLieuController<DangKyBoiDuong>>(Application, ObjectSpace, new ChonDuLieuController<DangKyBoiDuong>(Application, ObjectSpace, "Đăng ký bồi dưỡng", CriteriaOperator.Parse("QuanLyBoiDuong=?", _QuanLyBoiDuong.Oid), new string[] { "QuocGia.TenQuocGia","ChuongTrinhBoiDuong.TenChuongTrinhBoiDuong","TuNgay","DenNgay" }, new string[] { "Quốc gia","Chương trình bồi dưỡng","Từ ngày","Đến ngày" }, new int[] { 80, 150, 60, 50 }), "Chọn đăng ký bồi dưỡng", "Chọn", true);
                popup.Size = new System.Drawing.Size(320, 500);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ObjectSpace = Application.CreateObjectSpace();
                    DangKyBoiDuong dangKy = popup.CurrentControl.GetData();
                    if (dangKy != null)
                    {
                        DuyetDangKyBoiDuong duyet = ObjectSpace.FindObject<DuyetDangKyBoiDuong>(CriteriaOperator.Parse("QuanLyBoiDuong=? and DangKyBoiDuong=?", _QuanLyBoiDuong.Oid, dangKy.Oid));
                        if (duyet == null)
                        {
                            duyet = ObjectSpace.CreateObject<DuyetDangKyBoiDuong>();
                            duyet.QuanLyBoiDuong = ObjectSpace.GetObjectByKey<QuanLyBoiDuong>(_QuanLyBoiDuong.Oid);
                            duyet.DangKyBoiDuong = ObjectSpace.GetObjectByKey<DangKyBoiDuong>(dangKy.Oid);
                            duyet.QuocGia = dangKy.QuocGia != null ? ObjectSpace.GetObjectByKey<QuocGia>(dangKy.QuocGia.Oid) : null;
                            duyet.ChuongTrinhBoiDuong = dangKy.ChuongTrinhBoiDuong != null ? ObjectSpace.GetObjectByKey<ChuongTrinhBoiDuong>(dangKy.ChuongTrinhBoiDuong.Oid) : null;
                            duyet.NguonKinhPhi = dangKy.NguonKinhPhi != null ? ObjectSpace.GetObjectByKey<NguonKinhPhi>(dangKy.NguonKinhPhi.Oid) : null;
                            duyet.TuNgay = dangKy.TuNgay;
                            duyet.DenNgay = dangKy.DenNgay;
                            duyet.GhiChu = dangKy.GhiChu;
                        }

                        ChiTietDuyetDangKyBoiDuong chiTiet;
                        foreach (var item in dangKy.ListChiTietDangKyBoiDuong)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietDuyetDangKyBoiDuong>(CriteriaOperator.Parse("DuyetDangKyBoiDuong=? and ThongTinNhanVien=?", duyet.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietDuyetDangKyBoiDuong>();
                                chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                duyet.ListChiTietDuyetDangKyBoiDuong.Add(chiTiet);
                            }
                        }

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDanhSachBoiDuong.Caption);
                        Application.ShowView<DuyetDangKyBoiDuong>(ObjectSpace, duyet);
                        SetNotification("Duyệt đăng ký bồi dưỡng thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDBoiDuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhBoiDuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DangKyBoiDuongController> popup = new frmPopUp<DangKyBoiDuongController>(Application, ObjectSpace, new DangKyBoiDuongController(ObjectSpace, _QuanLyBoiDuong, false, true), "Lập QĐ bồi dưỡng", "Lưu", true);
                popup.Size = new System.Drawing.Size(320, 500);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ObjectSpace = Application.CreateObjectSpace();
                    ChuongTrinhBoiDuong chuongTrinhBoiDuong = popup.CurrentControl.GetChuongTrinhBoiDuong();
                    QuyetDinhBoiDuong qd = ObjectSpace.CreateObject<QuyetDinhBoiDuong>();
                    qd.ChuongTrinhBoiDuong = ObjectSpace.GetObjectByKey<ChuongTrinhBoiDuong>(chuongTrinhBoiDuong.Oid);

                    DuyetDangKyBoiDuong duyet = ObjectSpace.FindObject<DuyetDangKyBoiDuong>(CriteriaOperator.Parse("QuanLyBoiDuong=? and ChuongTrinhBoiDuong=?", _QuanLyBoiDuong.Oid, chuongTrinhBoiDuong.Oid));
                    if (duyet != null)
                    {
                        qd.DuyetDangKyBoiDuong = duyet;
                        //qd.TuNgay = duyet.TuNgay;
                        //qd.DenNgay = duyet.DenNgay;
                        //qd.NguonKinhPhi = duyet.NguonKinhPhi;
                    }

                    List<Guid> oid = popup.CurrentControl.GetNhanVienList();
                    using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid)))
                    {
                        ChiTietBoiDuong chiTiet;
                        foreach (ThongTinNhanVien thongTinNhanVien in nvList)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietBoiDuong>();
                            chiTiet.BoPhan = thongTinNhanVien.BoPhan;
                            chiTiet.ThongTinNhanVien = thongTinNhanVien;

                            qd.ListChiTietBoiDuong.Add(chiTiet);
                        }
                    }
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDBoiDuong.Caption);
                    Application.ShowView<QuyetDinhBoiDuong>(ObjectSpace, qd);
                    SetNotification("Lập QĐ bồi dưỡng thành công.");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyBoiDuong>())
            {
                ThucHienQuyTrinh.KetThuc(Application.CreateObjectSpace());
                EndQuyTrinh();
                SetNotification("Nhấn nút 'Bắt đầu' để bắt đầu quy trình mới");
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnHelp_Click(object sender, EventArgs e)
        {
            frmHelper helper = new frmHelper();
            helper.XuLy("QuyTrinhBoiDuong", "Quy trình bồi dưỡng");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                ObjectSpace = Application.CreateObjectSpace();
                _QuanLyBoiDuong = ObjectSpace.GetObjectByKey<QuanLyBoiDuong>(obj);
                if (_QuanLyBoiDuong != null)
                {
                    Application.ShowView<QuanLyBoiDuong>(ObjectSpace, _QuanLyBoiDuong);
                }
            }
            else
            {
                SetNotification("Hiện tại quy trình chưa được bắt đầu.");
            }
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnDangKyBoiDuong.State = CustomButton.ButtonState.Normal;
            btnLapDanhSachBoiDuong.State = CustomButton.ButtonState.Normal;
            btnQDBoiDuong.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnDangKyBoiDuong.State = CustomButton.ButtonState.Disable;
            btnLapDanhSachBoiDuong.State = CustomButton.ButtonState.Disable;
            btnQDBoiDuong.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
        }
    }
}
