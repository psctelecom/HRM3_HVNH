using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.NangLuong;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System.Linq;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using PSC_HRM.Module.Win.QuyTrinh.Common;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.NangLuong
{
    public partial class QuyTrinhNangLuongController : QuyTrinhBaseController
    {
        private QuanLyNangLuong _QuanLyNangLuong;

        public QuyTrinhNangLuongController(XafApplication application, IObjectSpace objectSpace)
            : base(application, objectSpace)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình nâng lương");
        }

        private void QuyTrinhNangLuongController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhNangLuong);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyNangLuong = ObjectSpace.GetObjectByKey<QuanLyNangLuong>(obj);
                if (_QuanLyNangLuong != null)
                {
                    StartQuyTrinh();
                    SetNotification(String.Format("Đang chạy quy trình nâng lương năm {0:####}", _QuanLyNangLuong.Nam));
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
            if (HamDungChung.IsWriteGranted<QuanLyNangLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyNangLuong>> popup = new frmPopUp<ChonDuLieuController<QuanLyNangLuong>>(Application, ObjectSpace,
                    new ChonDuLieuController<QuanLyNangLuong>(Application, ObjectSpace, "Quản lý nâng lương", CriteriaOperator.Parse(""),
                        new string[] { "Nam" }, new string[] { "Thời gian" }, new int[] { 150 }), "Chọn quản lý nâng lương", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyNangLuong = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyNangLuong))
                    {
                        StartQuyTrinh();
                        SetNotification(String.Format("Đang chạy quy trình nâng lương năm {0:####}", _QuanLyNangLuong.Nam));
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnTheoDoiNangLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<TheoDoiNangLuongController> popup = new frmPopUp<TheoDoiNangLuongController>(Application, ObjectSpace, new TheoDoiNangLuongController(Application, ObjectSpace, _QuanLyNangLuong), "Theo dõi nâng lương", false);
                popup.Size = new Size(601, 471);
                popup.Show();
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnTheoDoiNangLuong.Caption);
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnTheoDoiNangLuongSom_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<TheoDoiNangLuongSomController> popup = new frmPopUp<TheoDoiNangLuongSomController>(Application, ObjectSpace, new TheoDoiNangLuongSomController(Application, ObjectSpace, _QuanLyNangLuong), "Theo dõi nâng lương sớm", false);
                popup.Size = new Size(601, 471);
                popup.Show();
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnTheoDoiNangLuong.Caption);
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapDeNghiNangLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietDeNghiNangLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDanhSachNhanVienController> popup = new frmPopUp<ChonDanhSachNhanVienController>(Application, ObjectSpace,
                    new ChonDanhSachNhanVienController(GetDenHanNangLuongList()), "Chọn cán bộ", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    var oid = popup.CurrentControl.GetNhanVienList();
                    var nvList = new XPCollection<ThongTinNhanVien>(((XPObjectSpace)ObjectSpace).Session, new InOperator("Oid", oid));
                    ObjectSpace = Application.CreateObjectSpace();
                    DateTime current = HamDungChung.GetServerTime().SetTime(SetTimeEnum.StartMonth);
                    DeNghiNangLuong deNghi = ObjectSpace.FindObject<DeNghiNangLuong>(CriteriaOperator.Parse("QuanLyNangLuong=? and Thang=?", _QuanLyNangLuong.Oid, current));
                    if (deNghi == null)
                    {
                        deNghi = ObjectSpace.CreateObject<DeNghiNangLuong>();
                        deNghi.QuanLyNangLuong = ObjectSpace.GetObjectByKey<QuanLyNangLuong>(_QuanLyNangLuong.Oid);
                        deNghi.Thang = current;
                    }

                    ChiTietDeNghiNangLuong chiTiet;
                    foreach (var item in nvList)
                    {
                        chiTiet = ObjectSpace.FindObject<ChiTietDeNghiNangLuong>(CriteriaOperator.Parse("DeNghiNangLuong=? and ThongTinNhanVien=?", deNghi.Oid, item.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietDeNghiNangLuong>();
                            chiTiet.DeNghiNangLuong = deNghi;
                            chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
                            chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                            deNghi.ListChiTietDeNghiNangLuong.Add(chiTiet);
                        }
                    }

                    Application.ShowView<DeNghiNangLuong>(ObjectSpace, deNghi);
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDeNghiNangLuong.Caption);
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDNangLuong_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhNangLuong>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<DeNghiNangLuong>> popup = new frmPopUp<ChonDuLieuController<DeNghiNangLuong>>(Application, ObjectSpace,
                    new ChonDuLieuController<DeNghiNangLuong>(Application, ObjectSpace, "Đề nghị nâng lương",
                        CriteriaOperator.Parse("QuanLyNangLuong=?", _QuanLyNangLuong.Oid),
                        new string[] { "Thang" }, new string[] { "Tháng" }, new int[] { 100 }), "Chọn đề nghị nâng lương", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    DeNghiNangLuong deNghi = popup.CurrentControl.GetData();
                    if (deNghi != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        deNghi = ObjectSpace.GetObjectByKey<DeNghiNangLuong>(deNghi.Oid);
                        if (deNghi.ListChiTietDeNghiNangLuong.Count > 1)
                        {
                            if (HamDungChung.ShowMessage("Bạn có muốn lập quyết định nâng lương cho riêng từng cán bộ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                QuyetDinhNangLuong quyetDinh;
                                ChiTietQuyetDinhNangLuong chiTiet;
                                foreach (var item in deNghi.ListChiTietDeNghiNangLuong)
                                {
                                    quyetDinh = ObjectSpace.FindObject<QuyetDinhNangLuong>(CriteriaOperator.Parse("DeNghiNangLuong=? and ListChiTietQuyetDinhNangLuong[ThongTinNhanVien=?]", deNghi.Oid, item.ThongTinNhanVien.Oid));
                                    if (quyetDinh == null)
                                    {
                                        quyetDinh = ObjectSpace.CreateObject<QuyetDinhNangLuong>();
                                        quyetDinh.SoQuyetDinh = "      /QĐ-ĐHL";
                                        quyetDinh.DeNghiNangLuong = deNghi;
                                        quyetDinh.GhiChu = String.Format("{0} cho cán bộ {1}", HamDungChung.NangLuong(item.PhanLoai, item.VuotKhungMoi), item.ThongTinNhanVien.HoTen);
                                    }
                                    chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                                    if (chiTiet == null)
                                    {
                                        chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhNangLuong>();
                                        chiTiet.QuyetDinhNangLuong = quyetDinh;
                                        chiTiet.BoPhan = item.BoPhan;
                                        chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                                        chiTiet.NgachLuong = item.NgachLuong;
                                        chiTiet.BacLuongCu = item.BacLuongCu;
                                        chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                        chiTiet.VuotKhungCu = item.VuotKhungCu;
                                        chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                        chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                        chiTiet.BacLuongMoi = item.BacLuongMoi;
                                        chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                        chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                                        chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                        chiTiet.NangLuongTruocHan = item.PhanLoai != NangLuongEnum.ThuongXuyen;
                                        quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                                    }
                                }
                                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDNangLuong.Caption);
                            }
                            else
                            {
                                QuyetDinhNangLuong quyetDinh = ObjectSpace.FindObject<QuyetDinhNangLuong>(CriteriaOperator.Parse("DeNghiNangLuong=?", deNghi.Oid));
                                if (quyetDinh == null)
                                {
                                    quyetDinh = ObjectSpace.CreateObject<QuyetDinhNangLuong>();
                                    quyetDinh.SoQuyetDinh = "      /QĐ-ĐHL";
                                    quyetDinh.DeNghiNangLuong = deNghi;
                                    quyetDinh.GhiChu = String.Format("Nâng lương cho {0} cán bộ", deNghi.ListChiTietDeNghiNangLuong.Count);
                                }

                                ChiTietQuyetDinhNangLuong chiTiet;
                                foreach (var item in deNghi.ListChiTietDeNghiNangLuong)
                                {
                                    chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                                    if (chiTiet == null)
                                    {
                                        chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhNangLuong>();
                                        chiTiet.QuyetDinhNangLuong = quyetDinh;
                                        chiTiet.BoPhan = item.BoPhan;
                                        chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                                        chiTiet.NgachLuong = item.NgachLuong;
                                        chiTiet.BacLuongCu = item.BacLuongCu;
                                        chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                        chiTiet.VuotKhungCu = item.VuotKhungCu;
                                        chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                        chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                        chiTiet.BacLuongMoi = item.BacLuongMoi;
                                        chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                        chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                                        chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                        chiTiet.NangLuongTruocHan = item.PhanLoai != NangLuongEnum.ThuongXuyen;
                                        quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                                    }
                                }
                                Application.ShowView<QuyetDinhNangLuong>(ObjectSpace, quyetDinh);

                                if (quyetDinh.Oid != Guid.Empty)
                                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDNangLuong.Caption);
                            }
                        }
                        else
                        {
                            QuyetDinhNangLuong quyetDinh = ObjectSpace.FindObject<QuyetDinhNangLuong>(CriteriaOperator.Parse("DeNghiNangLuong=?", deNghi.Oid));
                            if (quyetDinh == null)
                            {
                                quyetDinh = ObjectSpace.CreateObject<QuyetDinhNangLuong>();
                                quyetDinh.SoQuyetDinh = "      /QĐ-ĐHL";
                                quyetDinh.DeNghiNangLuong = deNghi;
                            }

                            ChiTietQuyetDinhNangLuong chiTiet;
                            foreach (var item in deNghi.ListChiTietDeNghiNangLuong)
                            {
                                quyetDinh.GhiChu = String.Format("{0} cho cán bộ {1}", HamDungChung.NangLuong(item.PhanLoai, item.VuotKhungMoi), item.ThongTinNhanVien.HoTen);
                                chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhNangLuong>(CriteriaOperator.Parse("QuyetDinhNangLuong=? and ThongTinNhanVien=?", quyetDinh.Oid, item.ThongTinNhanVien.Oid));
                                if (chiTiet == null)
                                {
                                    chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhNangLuong>();
                                    chiTiet.QuyetDinhNangLuong = quyetDinh;
                                    chiTiet.BoPhan = item.BoPhan;
                                    chiTiet.ThongTinNhanVien = item.ThongTinNhanVien;
                                    chiTiet.NgachLuong = item.NgachLuong;
                                    chiTiet.BacLuongCu = item.BacLuongCu;
                                    chiTiet.HeSoLuongCu = item.HeSoLuongCu;
                                    chiTiet.VuotKhungCu = item.VuotKhungCu;
                                    chiTiet.NgayHuongLuongCu = item.NgayHuongLuongCu;
                                    chiTiet.MocNangLuongCu = item.MocNangLuongCu;
                                    chiTiet.BacLuongMoi = item.BacLuongMoi;
                                    chiTiet.HeSoLuongMoi = item.HeSoLuongMoi;
                                    chiTiet.VuotKhungMoi = item.VuotKhungMoi;
                                    chiTiet.MocNangLuongMoi = item.MocNangLuongMoi;
                                    chiTiet.NangLuongTruocHan = item.PhanLoai != NangLuongEnum.ThuongXuyen;
                                    quyetDinh.ListChiTietQuyetDinhNangLuong.Add(chiTiet);
                                }
                            }
                            Application.ShowView<QuyetDinhNangLuong>(ObjectSpace, quyetDinh);

                            if (quyetDinh.Oid != Guid.Empty)
                                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnQDNangLuong.Caption);
                        }
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyNangLuong>())
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
                _QuanLyNangLuong = ObjectSpace.GetObjectByKey<QuanLyNangLuong>(obj);
                if (_QuanLyNangLuong != null)
                {
                    Application.ShowView<QuanLyNangLuong>(ObjectSpace, _QuanLyNangLuong);
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
            helper.XuLy("QuyTrinhNangLuong", "Quy trình nâng lương");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnTheoDoiNangLuong.State = CustomButton.ButtonState.Normal;
            btnTheoDoiNangLuongSom.State = CustomButton.ButtonState.Normal;
            btnLapDeNghiNangLuong.State = CustomButton.ButtonState.Normal;
            btnQDNangLuong.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnTheoDoiNangLuong.State = CustomButton.ButtonState.Disable;
            btnTheoDoiNangLuongSom.State = CustomButton.ButtonState.Disable;
            btnLapDeNghiNangLuong.State = CustomButton.ButtonState.Disable;
            btnQDNangLuong.State = CustomButton.ButtonState.Disable;
        }

        private NhanVienList GetDenHanNangLuongList()
        {
            var result = new NhanVienList();
            using (DataTable dt = DataProvider.GetDataTable("spd_System_DenHanNangLuong", CommandType.StoredProcedure, new SqlParameter("@DenNgay", HamDungChung.GetServerTime().SetTime(SetTimeEnum.EndMonth))))
            {
                NhanVienItem nvItem;
                Guid oid;
                foreach (DataRow item in dt.Rows)
                {
                    nvItem = new NhanVienItem();
                    if (Guid.TryParse(item[0].ToString(), out oid))
                        nvItem.Oid = oid;
                    nvItem.Ho = item[1].ToString();
                    nvItem.Ten = item[2].ToString();
                    nvItem.BoPhan = item[3].ToString();
                    result.Add(nvItem);
                }
            }
            return result;
        }
    }
}
