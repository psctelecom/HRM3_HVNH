using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using PSC_HRM.Module.ChuyenNgach;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.HoSo;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BaoMat;
using DevExpress.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.ChuyenNgach
{
    public partial class QuyTrinhChuyenNgachController : QuyTrinhBaseController
    {
        private QuanLyChuyenNgach _QuanLyChuyenNgach;

        public QuyTrinhChuyenNgachController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình chuyển ngạch");
        }

        private void QuyTrinhChuyenNgachController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhChuyenNgach);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((DevExpress.ExpressApp.Xpo.XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyChuyenNgach = ObjectSpace.GetObjectByKey<QuanLyChuyenNgach>(obj);
                if (_QuanLyChuyenNgach != null)
                {
                    StartQuyTrinh();
                    SetNotification(String.Format("Đang chạy quy trình chuyển ngạch {0:####}", _QuanLyChuyenNgach.Nam));
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
            if (HamDungChung.IsWriteGranted<QuanLyChuyenNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyChuyenNgach>> popup = new frmPopUp<ChonDuLieuController<QuanLyChuyenNgach>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyChuyenNgach>(Application, ObjectSpace, "Quản lý chuyển ngạch", CriteriaOperator.Parse(""), new string[] { "Caption" }, new string[] { "Thời gian" }, new int[] { 150 }), "Chọn quản lý chuyển ngạch", true);
                if (popup.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _QuanLyChuyenNgach = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyChuyenNgach))
                    {
                        StartQuyTrinh();
                        SetNotification(String.Format("Đang chạy quy trình chuyển ngạch {0:####}", _QuanLyChuyenNgach.Nam));
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiChuyenNgach_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietDeNghiChuyenNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDanhSachNhanVienController> popup = new frmPopUp<ChonDanhSachNhanVienController>(Application, ObjectSpace, new ChonDanhSachNhanVienController(((XPObjectSpace)ObjectSpace).Session), "Chọn cán bộ", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    List<Guid> oid = popup.CurrentControl.GetNhanVienList();
                    XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid));
                    if (nvList.Count > 0)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        DateTime current = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                        DeNghiChuyenNgach deNghi = ObjectSpace.FindObject<DeNghiChuyenNgach>(CriteriaOperator.Parse("QuanLyChuyenNgach=? and Thang=?", _QuanLyChuyenNgach.Oid, current));
                        if (deNghi == null)
                        {
                            deNghi = ObjectSpace.CreateObject<DeNghiChuyenNgach>();
                            deNghi.QuanLyChuyenNgach = ObjectSpace.GetObjectByKey<QuanLyChuyenNgach>(_QuanLyChuyenNgach.Oid);
                        }

                        ChiTietDeNghiChuyenNgach chiTiet;
                        foreach (ThongTinNhanVien item in nvList)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietDeNghiChuyenNgach>(CriteriaOperator.Parse("DeNghiChuyenNgach=? and ThongTinNhanVien=?", deNghi.Oid, item.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietDeNghiChuyenNgach>();
                                chiTiet.DeNghiChuyenNgach = deNghi;
                                chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                                deNghi.ListChiTietDeNghiChuyenNgach.Add(chiTiet);
                            }
                        }

                        Application.ShowView<DeNghiChuyenNgach>(ObjectSpace, deNghi);
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiChuyenNgach.Caption);
                        SetNotification("Lập đề nghị chuyển ngạch lương thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDChuyenNgachLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhChuyenNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DeNghiChuyenNgach>> popup = new frmPopUp<ChonDuLieuController<DeNghiChuyenNgach>>(Application, ObjectSpace,
                    new ChonDuLieuController<DeNghiChuyenNgach>(Application, ObjectSpace, "Đề nghị", CriteriaOperator.Parse("QuanLyChuyenNgach=?", _QuanLyChuyenNgach.Oid),
                        new string[] { "Thang" },
                        new string[] { "Tháng" }, new int[] { 100 }), "Chọn đề nghị chuyển ngạch", true);
                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    DeNghiChuyenNgach deNghi = popup.CurrentControl.GetData();
                    if (deNghi != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        deNghi = ObjectSpace.GetObjectByKey<DeNghiChuyenNgach>(deNghi.Oid);

                        QuyetDinhChuyenNgach quyetDinh = ObjectSpace.FindObject<QuyetDinhChuyenNgach>(CriteriaOperator.Parse("DeNghiChuyenNgach=?", deNghi.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhChuyenNgach>();
                            quyetDinh.DeNghiChuyenNgach = deNghi;
                        }

                        ChiTietQuyetDinhChuyenNgach chiTiet;
                        foreach (ChiTietDeNghiChuyenNgach item in deNghi.ListChiTietDeNghiChuyenNgach)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhChuyenNgach>(CriteriaOperator.Parse("QuyetDinhChuyenNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhChuyenNgach>();
                                chiTiet.BoPhan = item.BoPhan;
                                chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                                chiTiet.NgachLuongCu = item.NgachLuongCu;
                                chiTiet.BacLuongCu = item.BacLuongCu;
                                chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                chiTiet.NgachLuongMoi = item.NgachLuongMoi;
                                chiTiet.BacLuongMoi = item.BacLuongMoi;
                                chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                                chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;

                                quyetDinh.ListChiTietQuyetDinhChuyenNgach.Add(chiTiet);
                            }
                        }

                        Application.ShowView<QuyetDinhChuyenNgach>(ObjectSpace, quyetDinh);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDChuyenNgachLuong.Caption);
                        SetNotification("Lập QĐ chuyển ngạch lương thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyChuyenNgach>())
            {
                ThucHienQuyTrinh.KetThuc(Application.CreateObjectSpace());
                EndQuyTrinh();
                SetNotification("Nhấn nút 'Bắt đầu' để bắt đầu quy trình mới");
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                ObjectSpace = Application.CreateObjectSpace();
                _QuanLyChuyenNgach = ObjectSpace.GetObjectByKey<QuanLyChuyenNgach>(obj);
                if (_QuanLyChuyenNgach != null)
                {
                    Application.ShowView<QuanLyChuyenNgach>(ObjectSpace, _QuanLyChuyenNgach);
                }
            }
            else
            {
                SetNotification("Hiện tại quy trình chưa được bắt đầu.");
            }
        }

        protected override void btnHelp_Click(object sender, EventArgs e)
        {
            frmHelper helper = new frmHelper();
            helper.XuLy("QuyTrinhChuyenNgach", "Quy trình chuyển ngạch");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiChuyenNgach.State = CustomButton.ButtonState.Normal;
            btnQDChuyenNgachLuong.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiChuyenNgach.State = CustomButton.ButtonState.Disable;
            btnQDChuyenNgachLuong.State = CustomButton.ButtonState.Disable;
        }
    }
}
