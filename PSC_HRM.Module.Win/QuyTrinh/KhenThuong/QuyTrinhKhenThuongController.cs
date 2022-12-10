using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.KhenThuong;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.KhenThuong
{
    public partial class QuyTrinhKhenThuongController : QuyTrinhBaseController
    {
        private QuanLyKhenThuong _QuanLyKhenThuong;

        public QuyTrinhKhenThuongController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình khen thưởng");
        }

        private void QuyTrinhKhenThuongController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhKhenThuong);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyKhenThuong = ObjectSpace.GetObjectByKey<QuanLyKhenThuong>(obj);
                if (_QuanLyKhenThuong != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình khen thưởng năm học " + _QuanLyKhenThuong.NamHoc.TenNamHoc);
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
            if (HamDungChung.IsWriteGranted<QuanLyKhenThuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyKhenThuong>> popup = new frmPopUp<ChonDuLieuController<QuanLyKhenThuong>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyKhenThuong>(Application, ObjectSpace, "Quản lý khen thưởng", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý khen thưởng", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyKhenThuong = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyKhenThuong))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình khen thưởng năm học " + _QuanLyKhenThuong.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnDangKyThiDua_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietDangKyThiDua>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DangKyThiDuaController> popup = new frmPopUp<DangKyThiDuaController>(Application, ObjectSpace, new DangKyThiDuaController(ObjectSpace, _QuanLyKhenThuong, false, false), string.Format("Đăng ký thi đua năm học {0}", _QuanLyKhenThuong.NamHoc != null ? _QuanLyKhenThuong.NamHoc.TenNamHoc : ""), "Lưu", true);
                popup.Size = new System.Drawing.Size(669, 565);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    popup.CurrentControl.CreateDangKyThiDua();
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Đăng ký thi đua");
                    SetNotification("Đăng ký thi đua thành công.");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDThanhLapHoiDongKhenThuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhThanhLapHoiDongKhenThuong>())
            {
                if (_QuanLyKhenThuong != null && _QuanLyKhenThuong.NamHoc != null)
                {
                    ObjectSpace = Application.CreateObjectSpace();
                    QuyetDinhThanhLapHoiDongKhenThuong quyetDinh = ObjectSpace.FindObject<QuyetDinhThanhLapHoiDongKhenThuong>(CriteriaOperator.Parse("NamHoc=?", _QuanLyKhenThuong.NamHoc.Oid));
                    if (quyetDinh == null)
                    {
                        quyetDinh = ObjectSpace.CreateObject<QuyetDinhThanhLapHoiDongKhenThuong>();
                        quyetDinh.QuanLyKhenThuong = ObjectSpace.GetObjectByKey<QuanLyKhenThuong>(_QuanLyKhenThuong.Oid);
                    }

                    Application.ShowView<QuyetDinhThanhLapHoiDongKhenThuong>(ObjectSpace, quyetDinh);
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Lập QĐ thành lập hội đồng khen thưởng");
                    btnDeNghiKhenThuong.Enabled = true;
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnDeNghiKhenThuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietDeNghiKhenThuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DangKyThiDuaController> popup = new frmPopUp<DangKyThiDuaController>(Application, ObjectSpace, new DangKyThiDuaController(ObjectSpace, _QuanLyKhenThuong, true, false), string.Format("Đề nghị khen thưởng năm học {0}", _QuanLyKhenThuong.NamHoc != null ? _QuanLyKhenThuong.NamHoc.TenNamHoc : ""), "Lưu", true);
                popup.Size = new System.Drawing.Size(669, 565);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    if (popup.CurrentControl.IsValidate())
                    {
                        popup.CurrentControl.CreateDeNghiKhenThuong();
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Đề nghị khen thưởng");
                        SetNotification("Đề nghị khen thưởng thành công.");
                        btnQDKhenThuong.Enabled = true;
                    }
                    else
                        SetNotification("Đã xảy ra lỗi khi đề nghị khen thưởng.");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDKhenThuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhKhenThuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DangKyThiDuaController> popup = new frmPopUp<DangKyThiDuaController>(Application, ObjectSpace, new DangKyThiDuaController(ObjectSpace, _QuanLyKhenThuong, true, false), "Chọn danh hiệu khen thưởng", true);
                popup.Size = new System.Drawing.Size(669, 565);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    if (popup.CurrentControl.IsValidate())
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        DanhHieuKhenThuong danhHieu = ObjectSpace.GetObjectByKey<DanhHieuKhenThuong>(popup.CurrentControl.GetDanhHieuKhenThuong().Oid);
                        if (danhHieu != null && _QuanLyKhenThuong != null && _QuanLyKhenThuong.NamHoc != null)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and DanhHieuKhenThuong=?", _QuanLyKhenThuong.NamHoc, danhHieu.Oid);
                            QuyetDinhKhenThuong quyetDinh = ObjectSpace.FindObject<QuyetDinhKhenThuong>(filter);
                            if (quyetDinh == null)
                            {
                                quyetDinh = ObjectSpace.CreateObject<QuyetDinhKhenThuong>();
                                quyetDinh.NamHoc = ObjectSpace.GetObjectByKey<NamHoc>(_QuanLyKhenThuong.NamHoc.Oid);
                                quyetDinh.DanhHieuKhenThuong = danhHieu;
                            }

                            List<Guid> oid = popup.CurrentControl.GetNhanVienList();
                            if (oid.Count > 0)
                            {
                                filter = new InOperator("Oid", oid);
                                using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, filter))
                                {
                                    ChiTietKhenThuongNhanVien nhanVien;
                                    foreach (ThongTinNhanVien item in nvList)
                                    {
                                        filter = CriteriaOperator.Parse("QuyetDinhKhenThuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.Oid);
                                        nhanVien = ObjectSpace.FindObject<ChiTietKhenThuongNhanVien>(filter);
                                        if (nhanVien == null)
                                        {
                                            nhanVien = ObjectSpace.CreateObject<ChiTietKhenThuongNhanVien>();
                                            nhanVien.BoPhan = item.BoPhan;
                                            nhanVien.ThongTinNhanVien = item;
                                            quyetDinh.ListChiTietKhenThuongNhanVien.Add(nhanVien);
                                        }
                                    }
                                }
                            }

                            oid = popup.CurrentControl.GetBoPhanList();
                            if (oid.Count > 0)
                            {
                                filter = new InOperator("Oid", oid);
                                using (XPCollection<BoPhan> bpList = new XPCollection<BoPhan>(((XPObjectSpace)ObjectSpace).Session, filter))
                                {
                                    ChiTietKhenThuongBoPhan boPhan;
                                    foreach (BoPhan item in bpList)
                                    {
                                        filter = CriteriaOperator.Parse("QuyetDinhKhenThuong=? and BoPhan=?", quyetDinh.Oid, item.Oid);
                                        boPhan = ObjectSpace.FindObject<ChiTietKhenThuongBoPhan>(filter);
                                        if (boPhan == null)
                                        {
                                            boPhan = ObjectSpace.CreateObject<ChiTietKhenThuongBoPhan>();
                                            boPhan.BoPhan = item;
                                            quyetDinh.ListChiTietKhenThuongBoPhan.Add(boPhan);
                                        }
                                    }
                                }
                            }
                            Application.ShowView<QuyetDinhKhenThuong>(ObjectSpace, quyetDinh);

                            if (quyetDinh.Oid != Guid.Empty)
                            {
                                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Lập QĐ khen thưởng");
                                btnKetThuc.Enabled = true;
                            }
                        }
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyKhenThuong>())
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
            helper.XuLy("QuyTrinhKhenThuong", "Quy trình khen thưởng");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                ObjectSpace = Application.CreateObjectSpace();
                _QuanLyKhenThuong = ObjectSpace.GetObjectByKey<QuanLyKhenThuong>(obj);
                if (_QuanLyKhenThuong != null)
                {
                    Application.ShowView<QuanLyKhenThuong>(ObjectSpace, _QuanLyKhenThuong);
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
            btnDangKyThiDua.State = CustomButton.ButtonState.Normal;
            btnQDThanhLapHoiDongKhenThuong.State = CustomButton.ButtonState.Normal;

            if (_QuanLyKhenThuong != null &&
                (_QuanLyKhenThuong.ListHoiDongKhenThuong.Count > 0 ||
                _QuanLyKhenThuong.ListChiTietDangKyThiDua.Count > 0 ||
                _QuanLyKhenThuong.ListChiTietDeNghiKhenThuong.Count > 0))
                btnDeNghiKhenThuong.State = CustomButton.ButtonState.Normal;
            else
                btnDeNghiKhenThuong.State = CustomButton.ButtonState.Disable;

            if (_QuanLyKhenThuong != null &&
                _QuanLyKhenThuong.ListChiTietDeNghiKhenThuong.Count > 0)
                btnQDKhenThuong.State = CustomButton.ButtonState.Normal;
            else
                btnQDKhenThuong.State = CustomButton.ButtonState.Disable;

            btnKetThuc.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnDangKyThiDua.State = CustomButton.ButtonState.Disable;
            btnQDThanhLapHoiDongKhenThuong.State = CustomButton.ButtonState.Disable;
            btnDeNghiKhenThuong.State = CustomButton.ButtonState.Disable;
            btnQDKhenThuong.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
        }
    }
}
