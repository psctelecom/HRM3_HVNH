using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PSC_HRM.Module.XuLyQuyTrinh;
using DevExpress.ExpressApp;
using PSC_HRM.Module.Win.Forms;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DiNuocNgoai;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.DiNuocNgoai
{
    public partial class QuyTrinhDiNuocNgoaiController : QuyTrinhBaseController
    {
        private QuanLyDiNuocNgoai _QuanLy;

        public QuyTrinhDiNuocNgoaiController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình đi nước ngoài");
        }

        private void QuyTrinhDiNuocNgoaiController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhDiNuocNgoai);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((DevExpress.ExpressApp.Xpo.XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLy = ObjectSpace.GetObjectByKey<QuanLyDiNuocNgoai>(obj);
                if (_QuanLy != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình đi nước ngoài năm học " + _QuanLy.NamHoc.TenNamHoc);
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
            if (HamDungChung.IsWriteGranted<QuanLyDiNuocNgoai>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyDiNuocNgoai>> popup = new frmPopUp<ChonDuLieuController<QuanLyDiNuocNgoai>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyDiNuocNgoai>(Application, ObjectSpace, "Quản lý đi nước ngoài", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý đi nước ngoài", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLy = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLy))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình đi nước ngoài năm học " + _QuanLy.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnDangKyDiNuocNgoai_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DangKyDiNuocNgoai>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDanhSachNhanVienController> popup = new frmPopUp<ChonDanhSachNhanVienController>(Application, ObjectSpace, new ChonDanhSachNhanVienController(((XPObjectSpace)ObjectSpace).Session), "Chọn cán bộ", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    List<Guid> oid = popup.CurrentControl.GetNhanVienList();
                    XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid));
                    if (nvList.Count > 0)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        DangKyDiNuocNgoai dangKy = ObjectSpace.CreateObject<DangKyDiNuocNgoai>();
                        dangKy.QuanLyDiNuocNgoai = ObjectSpace.GetObjectByKey<QuanLyDiNuocNgoai>(_QuanLy.Oid);

                        ChiTietDangKyDiNuocNgoai chiTiet;
                        foreach (ThongTinNhanVien item in nvList)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietDangKyDiNuocNgoai>();
                            chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                            dangKy.ListChiTietDangKyDiNuocNgoai.Add(chiTiet);
                        }

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Đăng ký đi nước ngoài");
                        Application.ShowView<DangKyDiNuocNgoai>(ObjectSpace, dangKy);
                        SetNotification("Đăng ký đi nước ngoài thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDDiNuocNgoai_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhDiNuocNgoai>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DangKyDiNuocNgoai>> popup = new frmPopUp<ChonDuLieuController<DangKyDiNuocNgoai>>(Application, ObjectSpace, new ChonDuLieuController<DangKyDiNuocNgoai>(Application, ObjectSpace, "Đăng ký đi nước ngoài", CriteriaOperator.Parse("QuanLyDiNuocNgoai=?", _QuanLy.Oid), new string[] { "QuocGia.TenQuocGia", "TuNgay", "DenNgay" }, new string[] { "Quốc gia", "Từ ngày", "Đến ngày" }, new int[] { 100, 200, 80 }), "Lập QĐ đi nước ngoài", true);
                popup.Size = new System.Drawing.Size(669, 565);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ObjectSpace = Application.CreateObjectSpace();
                    DangKyDiNuocNgoai dangKy = popup.CurrentControl.GetData();
                    if (dangKy != null)
                    {
                        QuyetDinhDiNuocNgoai quyetDinh = ObjectSpace.FindObject<QuyetDinhDiNuocNgoai>(CriteriaOperator.Parse("QuocGia=? and TuNgay=?", dangKy.QuocGia.Oid, dangKy.TuNgay));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhDiNuocNgoai>();
                            quyetDinh.DangKyDiNuocNgoai = ObjectSpace.GetObjectByKey<DangKyDiNuocNgoai>(dangKy.Oid);
                            quyetDinh.QuocGia = dangKy.QuocGia != null ? ObjectSpace.GetObjectByKey<QuocGia>(dangKy.QuocGia.Oid) : null;
                            quyetDinh.NguonKinhPhi = dangKy.NguonKinhPhi != null ? ObjectSpace.GetObjectByKey<NguonKinhPhi>(dangKy.NguonKinhPhi.Oid) : null;
                            quyetDinh.TuNgay = dangKy.TuNgay;
                            quyetDinh.DenNgay = dangKy.DenNgay;
                            quyetDinh.LyDo = dangKy.LyDo;
                        }

                        //ChiTietQuyetDinhDiNuocNgoai chiTiet;
                        //foreach (ChiTietDangKyDiNuocNgoai item in dangKy.ListChiTietDangKyDiNuocNgoai)
                        //{
                        //    chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhDiNuocNgoai>(CriteriaOperator.Parse("QuyetDinhDiNuocNgoai=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                        //    if (chiTiet == null)
                        //    {
                        //        chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhDiNuocNgoai>();
                        //        chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                        //        chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                        //        quyetDinh.ListChiTietQuyetDinhDiNuocNgoai.Add(chiTiet);
                        //    }
                        //}

                        Application.ShowView<QuyetDinhDiNuocNgoai>(ObjectSpace, quyetDinh);
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Lập QĐ đi nước ngoài");
                        SetNotification("Lập QĐ đi nước ngoài thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDTiepNhanDiNuocNgoai_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhTiepNhanVienChucDiNuocNgoai>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuyetDinhDiNuocNgoai>> popup = new frmPopUp<ChonDuLieuController<QuyetDinhDiNuocNgoai>>(Application, ObjectSpace, new ChonDuLieuController<QuyetDinhDiNuocNgoai>(Application, ObjectSpace, "QĐ đi nước ngoài", CriteriaOperator.Parse("DiNuocNgoaiTren30Ngay=?", "True"), new string[] { "SoQuyetDinh", "QuocGia.TenQuocGia", "TuNgay", "DenNgay" }, new string[] { "Số quyết định", "Quốc gia", "Từ ngày", "Đến ngày" }, new int[] { 100, 100, 200, 80 }), "Lập QĐ tiếp nhận đi nước ngoài", true);
                popup.Size = new System.Drawing.Size(669, 565);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    ObjectSpace = Application.CreateObjectSpace();
                    QuyetDinhDiNuocNgoai qd = popup.CurrentControl.GetData();
                    if (qd != null)
                    {
                        QuyetDinhTiepNhanVienChucDiNuocNgoai quyetDinh = ObjectSpace.FindObject<QuyetDinhTiepNhanVienChucDiNuocNgoai>(CriteriaOperator.Parse("QuyetDinhDiNuocNgoai=?", qd.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhTiepNhanVienChucDiNuocNgoai>();
                            quyetDinh.QuyetDinhDiNuocNgoai = ObjectSpace.GetObjectByKey<QuyetDinhDiNuocNgoai>(qd.Oid);
                        }

                        Application.ShowView<QuyetDinhTiepNhanVienChucDiNuocNgoai>(ObjectSpace, quyetDinh);
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), "Lập QĐ tiếp nhận viên chức đi nước ngoài");
                        SetNotification("Lập QĐ tiếp nhận viên chức đi nước ngoài thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyDiNuocNgoai>())
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
            helper.XuLy("QuyTrinhDiNuocNgoai", "Quy trình đi nước ngoài");
        }

        protected override void btnOpen_Click(object sender, EventArgs e)
        {
            ObjectSpace = Application.CreateObjectSpace();
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                ObjectSpace = Application.CreateObjectSpace();
                _QuanLy = ObjectSpace.GetObjectByKey<QuanLyDiNuocNgoai>(obj);
                if (_QuanLy != null)
                {
                    Application.ShowView<QuanLyDiNuocNgoai>(ObjectSpace, _QuanLy);
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
            btnDangKyDiNuocNgoai.State = CustomButton.ButtonState.Normal;
            btnQDDiNuocNgoai.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnDangKyDiNuocNgoai.State = CustomButton.ButtonState.Disable;
            btnQDDiNuocNgoai.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
        }
    }
}
