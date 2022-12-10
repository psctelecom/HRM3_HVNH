using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.DaoTao
{
    public partial class QuyTrinhDaoTaoController : QuyTrinhBaseController
    {
        private QuanLyDaoTao _QuanLyDaoTao;

        public QuyTrinhDaoTaoController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình đào tạo");
        }

        private void QuyTrinhDaoTaoController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhDaoTao);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyDaoTao = ObjectSpace.GetObjectByKey<QuanLyDaoTao>(obj);
                if (_QuanLyDaoTao != null && _QuanLyDaoTao.NamHoc != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình đào tạo năm học " + _QuanLyDaoTao.NamHoc.TenNamHoc);
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
            if (HamDungChung.IsWriteGranted<QuanLyDaoTao>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyDaoTao>> popup = new frmPopUp<ChonDuLieuController<QuanLyDaoTao>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyDaoTao>(Application, ObjectSpace, "Quản lý đào tạo", CriteriaOperator.Parse(""), new string[] { "NamHoc.TenNamHoc" }, new string[] { "Năm học" }, new int[] { 150 }), "Chọn quản lý đào tạo", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyDaoTao = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyDaoTao))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình đào tạo năm học " + _QuanLyDaoTao.NamHoc.TenNamHoc);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnDangKyDaoTao_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DangKyDaoTao>())
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
                        DangKyDaoTao dangKy = ObjectSpace.CreateObject<DangKyDaoTao>();
                        dangKy.QuanLyDaoTao = ObjectSpace.GetObjectByKey<QuanLyDaoTao>(_QuanLyDaoTao.Oid);
                        ChiTietDangKyDaoTao chiTiet;
                        foreach (ThongTinNhanVien thongTinNhanVien in nvList)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietDangKyDaoTao>();
                            chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(thongTinNhanVien.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(thongTinNhanVien.Oid);
                            dangKy.ListChiTietDangKyDaoTao.Add(chiTiet);
                        }

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnDangKyDaoTao.Caption);
                        Application.ShowView<DangKyDaoTao>(ObjectSpace, dangKy);
                        SetNotification("Đăng ký đào tạo thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDanhSachDaoTao_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DuyetDangKyDaoTao>())
            {
                //reload quan ly dao tao
                XpoDefault.Session.DropIdentityMap();

                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DuyetDangKyDaoTaoController> popup = new frmPopUp<DuyetDangKyDaoTaoController>(Application, ObjectSpace, new DuyetDangKyDaoTaoController(_QuanLyDaoTao), "Duyệt đăng ký đào tạo", true);
                popup.Size = new Size(600, 700);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    List<DangKyDaoTao> result = popup.CurrentControl.GetDangKyDaoTao();
                    if (result != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        DuyetDangKyDaoTao duyet;
                        foreach (DangKyDaoTao dangKyDaoTao in result)
                        {
                            duyet = ObjectSpace.FindObject<DuyetDangKyDaoTao>(CriteriaOperator.Parse("QuanLyDaoTao=? and TrinhDoChuyenMon=? and ChuyenMonDaoTao=?", _QuanLyDaoTao.Oid, dangKyDaoTao.TrinhDoChuyenMon.Oid, dangKyDaoTao.ChuyenMonDaoTao.Oid));
                            if (duyet == null)
                            {
                                duyet = ObjectSpace.CreateObject<DuyetDangKyDaoTao>();
                                duyet.QuanLyDaoTao = ObjectSpace.GetObjectByKey<QuanLyDaoTao>(_QuanLyDaoTao.Oid);
                                duyet.TrinhDoChuyenMon = ObjectSpace.GetObjectByKey<TrinhDoChuyenMon>(dangKyDaoTao.TrinhDoChuyenMon.Oid);
                                duyet.ChuyenMonDaoTao = ObjectSpace.GetObjectByKey<ChuyenMonDaoTao>(dangKyDaoTao.ChuyenMonDaoTao.Oid);
                                duyet.QuocGia = ObjectSpace.GetObjectByKey<QuocGia>(dangKyDaoTao.QuocGia.Oid);
                                duyet.TruongDaoTao = ObjectSpace.GetObjectByKey<TruongDaoTao>(dangKyDaoTao.TruongDaoTao.Oid);
                                duyet.NguonKinhPhi = ObjectSpace.GetObjectByKey<NguonKinhPhi>(dangKyDaoTao.NguonKinhPhi.Oid);
                                duyet.KhoaDaoTao = ObjectSpace.GetObjectByKey<KhoaDaoTao>(dangKyDaoTao.KhoaDaoTao.Oid);
                                duyet.GhiChu = dangKyDaoTao.GhiChu;
                                ChiTietDuyetDangKyDaoTao chiTiet;
                                foreach (ChiTietDangKyDaoTao item in dangKyDaoTao.ListChiTietDangKyDaoTao)
                                {
                                    chiTiet = ObjectSpace.FindObject<ChiTietDuyetDangKyDaoTao>(CriteriaOperator.Parse("DuyetDangKyDaoTao=? and ThongTinNhanVien=?", duyet.Oid, item.ThongTinNhanVien.Oid));
                                    if (chiTiet == null)
                                    {
                                        chiTiet = ObjectSpace.CreateObject<ChiTietDuyetDangKyDaoTao>();
                                        chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                        chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                        duyet.ListChiTietDuyetDangKyDaoTao.Add(chiTiet);
                                    }
                                }
                            }
                        }
                        ObjectSpace.CommitChanges();
                        ObjectSpace = Application.CreateObjectSpace();

                        QuanLyDaoTao quanLy = ObjectSpace.GetObjectByKey<QuanLyDaoTao>(_QuanLyDaoTao.Oid);
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDanhSachDaoTao.Caption);
                        Application.ShowView<QuanLyDaoTao>(ObjectSpace, quanLy);
                        SetNotification("Duyệt đăng ký đào tạo thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDDaoTao_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhDaoTao>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DuyetDangKyDaoTao>> popup = new frmPopUp<ChonDuLieuController<DuyetDangKyDaoTao>>(Application, ObjectSpace, new ChonDuLieuController<DuyetDangKyDaoTao>(Application, ObjectSpace, "Duyệt đăng ký", CriteriaOperator.Parse("QuanLyDaoTao=?", _QuanLyDaoTao.Oid), new string[] { "TrinhDoChuyenMon.TenTrinhDoChuyenMon", "ChuyenMonDaoTao.TenChuyenMonDaoTao", "TruongDaoTao.TenTruongDaoTao" }, new string[] { "Trình độ đào tạo", "Chuyên ngành đào tạo", "Trường đào tạo" }, new int[] { 80, 150, 150 }), "Chọn duyệt đăng ký đào tạo", "Chọn", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    DuyetDangKyDaoTao duyet = popup.CurrentControl.GetData();
                    if (duyet != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        QuyetDinhDaoTao quyetDinh = ObjectSpace.FindObject<QuyetDinhDaoTao>(CriteriaOperator.Parse("DuyetDangKyDaoTao=?", duyet.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhDaoTao>();
                            quyetDinh.DuyetDangKyDaoTao = ObjectSpace.GetObjectByKey<DuyetDangKyDaoTao>(duyet.Oid);
                            quyetDinh.TrinhDoChuyenMon = ObjectSpace.GetObjectByKey<TrinhDoChuyenMon>(duyet.TrinhDoChuyenMon.Oid);
                            quyetDinh.NganhDaoTao = ObjectSpace.GetObjectByKey<NganhDaoTao>(duyet.NganhDaoTao.Oid);
                            quyetDinh.QuocGia = ObjectSpace.GetObjectByKey<QuocGia>(duyet.QuocGia.Oid);
                            quyetDinh.TruongDaoTao = ObjectSpace.GetObjectByKey<TruongDaoTao>(duyet.TruongDaoTao.Oid);
                            quyetDinh.NguonKinhPhi = ObjectSpace.GetObjectByKey<NguonKinhPhi>(duyet.NguonKinhPhi.Oid);
                            quyetDinh.KhoaDaoTao = ObjectSpace.GetObjectByKey<KhoaDaoTao>(duyet.KhoaDaoTao.Oid);
                        }
                        ChiTietDaoTao chiTiet;
                        foreach (ChiTietDuyetDangKyDaoTao item in duyet.ListChiTietDuyetDangKyDaoTao)
                        {
                            chiTiet = ObjectSpace.FindObject<ChiTietDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                            if (chiTiet == null)
                            {
                                chiTiet = ObjectSpace.CreateObject<ChiTietDaoTao>();
                                chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                                chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
                                quyetDinh.ListChiTietDaoTao.Add(chiTiet);
                            }
                        }

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDDaoTao.Caption);
                        Application.ShowView<QuyetDinhDaoTao>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ đào tạo thành công.");
                    }
                    else
                        SetNotification("Chưa chọn duyệt đăng ký đào tạo.");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDChuyenTruongDaoTao_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhChuyenTruongDaoTao>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetDaoTao()), "Chọn cán bộ", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        QuyetDinhChuyenTruongDaoTao quyetDinh = ObjectSpace.CreateObject<QuyetDinhChuyenTruongDaoTao>();
                        quyetDinh.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                        quyetDinh.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);

                        //ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDChuyenTruongDaoTao.Caption);
                        Application.ShowView<QuyetDinhChuyenTruongDaoTao>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ chuyển trường đào tạo thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDGiaHanDaoTao_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhGiaHanDaoTao>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonNhanVienController> popup = new frmPopUp<ChonNhanVienController>(Application, ObjectSpace, new ChonNhanVienController(ObjectSpace, GetDaoTao()), "Chọn cán bộ", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    ThongTinNhanVien nhanVien = popup.CurrentControl.GetThongTinNhanVien();
                    if (nhanVien != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        QuyetDinhGiaHanDaoTao quyetDinh = ObjectSpace.CreateObject<QuyetDinhGiaHanDaoTao>();
                        quyetDinh.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                        quyetDinh.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDGiaHanDaoTao.Caption);
                        Application.ShowView<QuyetDinhGiaHanDaoTao>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ gia hạn đào tạo thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDCongNhanDaoTao_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhCongNhanDaoTao>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuyetDinhDaoTao>> popup = new frmPopUp<ChonDuLieuController<QuyetDinhDaoTao>>(Application, ObjectSpace, new ChonDuLieuController<QuyetDinhDaoTao>(Application, ObjectSpace, "Quyết định", CriteriaOperator.Parse(""), new string[] { "SoQuyetDinh", "NgayHieuLuc" }, new string[] { "Số quyết định", "Ngày hiệu lực" }, new int[] { 80, 100 }), "Chọn quyết định đào tạo", "Chọn", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    QuyetDinhDaoTao qd = popup.CurrentControl.GetData();
                    if (qd != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        QuyetDinhCongNhanDaoTao quyetDinh = ObjectSpace.FindObject<QuyetDinhCongNhanDaoTao>(CriteriaOperator.Parse("QuyetDinhDaoTao=?", qd.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhCongNhanDaoTao>();
                            quyetDinh.QuyetDinhDaoTao = ObjectSpace.GetObjectByKey<QuyetDinhDaoTao>(qd.Oid);
                        }

                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDGiaHanDaoTao.Caption);
                        Application.ShowView<QuyetDinhCongNhanDaoTao>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ công nhận đào tạo thành công.");
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyDaoTao>())
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
                _QuanLyDaoTao = ObjectSpace.GetObjectByKey<QuanLyDaoTao>(obj);
                if (_QuanLyDaoTao != null)
                {
                    Application.ShowView<QuanLyDaoTao>(ObjectSpace, _QuanLyDaoTao);
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
            helper.XuLy("QuyTrinhDaoTao", "Quy trình đào tạo");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnDangKyDaoTao.State = CustomButton.ButtonState.Normal;
            btnLapDanhSachDaoTao.State = CustomButton.ButtonState.Normal;
            btnQDDaoTao.State = CustomButton.ButtonState.Normal;
            btnQDCongNhanDaoTao.State = CustomButton.ButtonState.Normal;
            //btnQDChuyenTruongDaoTao.State = CustomButton.ButtonState.Normal;
            btnQDGiaHanDaoTao.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnDangKyDaoTao.State = CustomButton.ButtonState.Disable;
            btnLapDanhSachDaoTao.State = CustomButton.ButtonState.Disable;
            btnQDDaoTao.State = CustomButton.ButtonState.Disable;
            btnQDCongNhanDaoTao.State = CustomButton.ButtonState.Disable;
            //btnQDChuyenTruongDaoTao.State = CustomButton.ButtonState.Disable;
            btnQDGiaHanDaoTao.State = CustomButton.ButtonState.Disable;
        }

        private List<Guid> GetDaoTao()
        {
            List<Guid> result = new List<Guid>();
            using (DataTable dt = DataProvider.GetDataTable("spd_System_GetChuaHoanThanhDaoTao", CommandType.StoredProcedure))
            {
                Guid oid;
                foreach (DataRow dr in dt.Rows)
                {
                    if (Guid.TryParse(dr[0].ToString(), out oid))
                        result.Add(oid);
                }
            }
            return result;
        }
    }
}
