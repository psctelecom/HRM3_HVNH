using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.NangNgach;
using PSC_HRM.Module.XuLyQuyTrinh;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System.Linq;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.ComponentModel;
using PSC_HRM.Module;
namespace PSC_HRM.Module.Win.QuyTrinh.NangNgach
{
    public partial class QuyTrinhNangNgachController : QuyTrinhBaseController
    {
        private QuanLyNangNgach _QuanLyNangNgach;

        public QuyTrinhNangNgachController(XafApplication application, IObjectSpace objectSpace)
            : base(application, objectSpace)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình nâng ngạch");
        }

        private void QuyTrinhNangNgachController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhNangNgach);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyNangNgach = ObjectSpace.GetObjectByKey<QuanLyNangNgach>(obj);
                if (_QuanLyNangNgach != null)
                {
                    StartQuyTrinh();
                    SetNotification(String.Format("Đang chạy quy trình nâng ngạch năm {0:####}", _QuanLyNangNgach.Nam));
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
            if (HamDungChung.IsWriteGranted<QuanLyNangNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyNangNgach>> popup = new frmPopUp<ChonDuLieuController<QuanLyNangNgach>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyNangNgach>(Application, ObjectSpace, "Quản lý nâng ngạch", CriteriaOperator.Parse(""), new string[] { "Caption" }, new string[] { "Thời gian" }, new int[] { 150 }), "Chọn quản lý nâng ngạch", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyNangNgach = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyNangNgach))
                    {
                        StartQuyTrinh();
                        SetNotification(String.Format("Đang chạy quy trình nâng ngạch năm {0:####}", _QuanLyNangNgach.Nam));
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiNangNgachLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DeNghiNangNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDanhSachNhanVienController> popup = new frmPopUp<ChonDanhSachNhanVienController>(Application, ObjectSpace, new ChonDanhSachNhanVienController(((XPObjectSpace)ObjectSpace).Session), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    var oid = popup.CurrentControl.GetNhanVienList();
                    var nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid));
                    ObjectSpace = Application.CreateObjectSpace();

                    DateTime current = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                    DeNghiNangNgach deNghi = ObjectSpace.FindObject<DeNghiNangNgach>(CriteriaOperator.Parse("QuanLyNangNgach=? and Thang=?", _QuanLyNangNgach.Oid, current));
                    if (deNghi == null)
                    {
                        deNghi = ObjectSpace.CreateObject<DeNghiNangNgach>();
                        deNghi.QuanLyNangNgach = ObjectSpace.GetObjectByKey<QuanLyNangNgach>(_QuanLyNangNgach.Oid);
                        deNghi.Thang = current;
                    }

                    ChiTietDeNghiNangNgach chiTiet;
                    foreach (var item in nvList)
                    {
                        chiTiet = ObjectSpace.FindObject<ChiTietDeNghiNangNgach>(CriteriaOperator.Parse("DeNghiNangNgach=? and ThongTinNhanVien=?", deNghi.Oid, item.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietDeNghiNangNgach>();
                            chiTiet.DeNghiNangNgach = deNghi;
                            chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BaoMat.BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<HoSo.ThongTinNhanVien>(item.Oid);
                            deNghi.ListChiTietDeNghiNangNgach.Add(chiTiet);
                        }
                    }
                    Application.ShowView<DeNghiNangNgach>(ObjectSpace, deNghi);
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiNangNgachLuong.Caption);
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDNangNgachLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhNangNgach>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DeNghiNangNgach>> popup = new frmPopUp<ChonDuLieuController<DeNghiNangNgach>>(Application, ObjectSpace,
                    new ChonDuLieuController<DeNghiNangNgach>(Application, ObjectSpace, "Đề nghị nâng ngạch", CriteriaOperator.Parse("QuanLyNangNgach=?", _QuanLyNangNgach.Oid),
                        new string[] { "Thang" },
                        new string[] { "Tháng" }, new int[] { 100 }), "Chọn đề nghị nâng ngạch", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    DeNghiNangNgach deNghi = popup.CurrentControl.GetData();
                    if (deNghi != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();                        

                        deNghi = ObjectSpace.GetObjectByKey<DeNghiNangNgach>(deNghi.Oid);
                        QuyetDinhNangNgach quyetDinh = ObjectSpace.FindObject<QuyetDinhNangNgach>(CriteriaOperator.Parse("DeNghiNangNgach=?", deNghi.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhNangNgach>();
                            quyetDinh.DeNghiNangNgach = deNghi;
                        }

                        ChiTietQuyetDinhNangNgach chiTiet;
                        foreach (var item in deNghi.ListChiTietDeNghiNangNgach)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhNangNgach>(CriteriaOperator.Parse("QuyetDinhNangNgach=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhNangNgach>();
                                chiTiet.QuyetDinhNangNgach = quyetDinh;
                                chiTiet.BoPhan = item.BoPhan;
                                chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                                quyetDinh.ListChiTietQuyetDinhNangNgach.Add(chiTiet);
                            }
                            chiTiet.NgachLuongCu = item.NgachLuongCu;
                            chiTiet.BacLuongCu = item.BacLuongCu;
                            chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                            chiTiet.NgayBoNhiemNgachCu = item.NgayBoNhiemNgachCu;
                            chiTiet.NgachLuongMoi = item.NgachLuongMoi;
                            chiTiet.BacLuongMoi = item.BacLuongMoi;
                            chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                            chiTiet.NgayBoNhiemNgachMoi = item.NgayBoNhiemNgachMoi;
                            chiTiet.NgayHuongLuongMoi = item.NgayHuongLuongMoi;
                            chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                        }

                        Application.ShowView<QuyetDinhNangNgach>(ObjectSpace, quyetDinh);

                        if (quyetDinh.Oid != Guid.Empty)
                            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDNangNgachLuong.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");

        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangNgach>())
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
                _QuanLyNangNgach = ObjectSpace.GetObjectByKey<QuanLyNangNgach>(obj);
                if (_QuanLyNangNgach != null)
                {
                    Application.ShowView<QuanLyNangNgach>(ObjectSpace, _QuanLyNangNgach);
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
            helper.XuLy("QuyTrinhNangNgach", "Quy trình nâng ngạch");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiNangNgachLuong.State = CustomButton.ButtonState.Normal;
            btnQDNangNgachLuong.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiNangNgachLuong.State = CustomButton.ButtonState.Disable;
            btnQDNangNgachLuong.State = CustomButton.ButtonState.Disable;
        }
    }
}
