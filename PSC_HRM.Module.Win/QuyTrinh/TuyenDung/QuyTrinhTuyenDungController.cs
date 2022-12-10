using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using PSC_HRM.Module.XuLyQuyTrinh;
using PSC_HRM.Module.Win.Forms;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.TuyenDung;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.NonPersistentObjects;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.Win.QuyTrinh.TuyenDung
{
    public partial class QuyTrinhTuyenDungController : QuyTrinhBaseController
    {
        private QuanLyTuyenDung _QuanLyTuyenDung;

        public QuyTrinhTuyenDungController(XafApplication app, IObjectSpace obs)
            : base(app, obs)
        {
            InitializeComponent();
            SetGroupCaption("Quy trình tuyển dụng");
        }

        private void QuyTrinhTuyenDungController_Load(object sender, EventArgs e)
        {
            ThucHienQuyTrinh = ThucHienQuyTrinhFactory.CreateThucHienQuyTrinh(ThucHienQuyTrinhTypeEnum.QuyTrinhTuyenDung);
            Guid obj = ThucHienQuyTrinh.DaBatDau(((XPObjectSpace)ObjectSpace).Session);
            if (obj != Guid.Empty)
            {
                _QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(obj);
                if (_QuanLyTuyenDung != null && _QuanLyTuyenDung.NamHoc != null)
                {
                    StartQuyTrinh();
                    SetNotification("Đang chạy quy trình tuyển dụng" + _QuanLyTuyenDung.Caption);
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
            if (HamDungChung.IsWriteGranted<QuanLyTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<QuanLyTuyenDung>> popup = new frmPopUp<ChonDuLieuController<QuanLyTuyenDung>>(Application, ObjectSpace, new ChonDuLieuController<QuanLyTuyenDung>(Application, ObjectSpace, "Quản lý tuyển dụng", CriteriaOperator.Parse(""), new string[] { "Caption" }, new string[] { "Thời gian" }, new int[] { 150 }), "Chọn quản lý tuyển dụng", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    _QuanLyTuyenDung = popup.CurrentControl.GetData();
                    if (ThucHienQuyTrinh.BatDau(ObjectSpace, _QuanLyTuyenDung))
                    {
                        StartQuyTrinh();
                        SetNotification("Đang chạy quy trình tuyển dụng " + _QuanLyTuyenDung.Caption);
                    }
                    else
                        HamDungChung.ShowWarningMessage("Bắt đầu quy trình không thành công. Vui lòng thử lại");
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnLapKeHoachTuyenDung_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<DangKyTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonBoPhanController> popup = new frmPopUp<ChonBoPhanController>(Application, ObjectSpace, new ChonBoPhanController(ObjectSpace, null), "Chọn đơn vị", true);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    BoPhan boPhan = popup.CurrentControl.GetBoPhan();
                    if (boPhan != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        CriteriaOperator filter;
                        if (boPhan.LoaiBoPhan == LoaiBoPhanEnum.BoMonTrucThuocKhoa
                            && boPhan.BoPhanCha != null
                            && (boPhan.BoPhanCha.LoaiBoPhan == LoaiBoPhanEnum.Khoa
                            || boPhan.BoPhanCha.LoaiBoPhan == LoaiBoPhanEnum.PhongBan))
                            filter = CriteriaOperator.Parse("QuanLyTuyenDung=? and BoPhan=? and BoMon=?", _QuanLyTuyenDung.Oid, boPhan.BoPhanCha.Oid, boPhan.Oid);
                        else
                            filter = CriteriaOperator.Parse("QuanLyTuyenDung=? and BoPhan=?", _QuanLyTuyenDung.Oid, boPhan.Oid);

                        DangKyTuyenDung obj = ObjectSpace.FindObject<DangKyTuyenDung>(filter);
                        if (obj == null)
                        {
                            obj = ObjectSpace.CreateObject<DangKyTuyenDung>();
                            obj.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);

                            if (boPhan.LoaiBoPhan == LoaiBoPhanEnum.BoMonTrucThuocKhoa
                            && boPhan.BoPhanCha != null
                            && (boPhan.BoPhanCha.LoaiBoPhan == LoaiBoPhanEnum.Khoa
                            || boPhan.BoPhanCha.LoaiBoPhan == LoaiBoPhanEnum.PhongBan))
                            {
                                obj.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(boPhan.BoPhanCha.Oid);
                                obj.BoMon = ObjectSpace.GetObjectByKey<BoPhan>(boPhan.Oid);
                            }
                            else
                                obj.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(boPhan.Oid);
                        }

                        Application.ShowModelView<DangKyTuyenDung>(ObjectSpace, obj);

                        if (obj.Oid != Guid.Empty)
                            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapKeHoachTuyenDung.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnDuyetKeHoachTuyenDung_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<NhuCauTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<DuyetDangKyTuyenDungController> popup = new frmPopUp<DuyetDangKyTuyenDungController>(Application, ObjectSpace, new DuyetDangKyTuyenDungController(ObjectSpace, _QuanLyTuyenDung), "Duyệt đăng ký tuyển dụng", true);
                popup.Size = new System.Drawing.Size(750, 600);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    popup.CurrentControl.XuLy();
                    SetNotification("Duyệt đăng ký tuyển dụng thành công.");
                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnDuyetKeHoachTuyenDung.Caption);

                    ObjectSpace = Application.CreateObjectSpace();
                    QuanLyTuyenDung quanLy = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                    Application.ShowModelView<QuanLyTuyenDung>(ObjectSpace, quanLy);
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnQDThanhLapHoiDongTuyenDung_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhThanhLapHoiDongTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();

                QuyetDinhThanhLapHoiDongTuyenDung obj = ObjectSpace.FindObject<QuyetDinhThanhLapHoiDongTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=?", _QuanLyTuyenDung.Oid));
                if (obj == null)
                {
                    obj = ObjectSpace.CreateObject<QuyetDinhThanhLapHoiDongTuyenDung>();
                    obj.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                }

                Application.ShowView<QuyetDinhThanhLapHoiDongTuyenDung>(ObjectSpace, obj);
                SetNotification("Lập QĐ thành lập hội đồng tuyển dụng thành công.");
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapKeHoachTuyenDung.Caption);
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        private void btnNhanChamDiemHoSo_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<ViTriTuyenDung>> popup = new frmPopUp<ChonDuLieuController<ViTriTuyenDung>>(Application, ObjectSpace, new ChonDuLieuController<ViTriTuyenDung>(Application, ObjectSpace, "Vị trí tuyển dụng", CriteriaOperator.Parse("QuanLyTuyenDung=?", _QuanLyTuyenDung.Oid), new string[] { "Caption" }, new string[] { "Vị trí tuyển dụng" }, new int[] { 250 }), "Chọn vị trí tuyển dụng", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    ViTriTuyenDung viTri = popup.CurrentControl.GetData();
                    if (viTri != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        ChiTietTuyenDung chiTiet = ObjectSpace.FindObject<ChiTietTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and ViTriTuyenDung=?", _QuanLyTuyenDung.Oid, viTri.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietTuyenDung>();
                            chiTiet.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                            chiTiet.ViTriTuyenDung = ObjectSpace.GetObjectByKey<ViTriTuyenDung>(viTri.Oid);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 1, "Duyệt hồ sơ", 100);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 2, "Phỏng vấn", 100);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 3, "Thi tuyển", 100);
                            ObjectSpace.CommitChanges();
                        }

                        ObjectSpace = Application.CreateObjectSpace();
                        VongTuyenDung vongTuyenDung = ObjectSpace.FindObject<VongTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and BuocTuyenDung.TenBuocTuyenDung like ?", chiTiet.Oid, "%Duyệt hồ sơ%"));
                        if (vongTuyenDung == null)
                        {
                            vongTuyenDung = ObjectSpace.CreateObject<VongTuyenDung>();
                            vongTuyenDung.ChiTietTuyenDung = ObjectSpace.GetObjectByKey<ChiTietTuyenDung>(chiTiet.Oid);
                            vongTuyenDung.BuocTuyenDung = ObjectSpace.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, "%Duyệt hồ sơ%"));
                        }
                        Application.ShowModelView<VongTuyenDung>(ObjectSpace, vongTuyenDung);
                        if (vongTuyenDung.Oid != Guid.Empty)
                        {
                            SetNotification("Duyệt hồ sơ thành công.");
                            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnNhanVaChamDiemHoSo.Caption);
                        }
                    }
                }
            }
        }

        private void btnLapDanhSachMoiPhongVan_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<ViTriTuyenDung>> popup = new frmPopUp<ChonDuLieuController<ViTriTuyenDung>>(Application, ObjectSpace, new ChonDuLieuController<ViTriTuyenDung>(Application, ObjectSpace, "Vị trí tuyển dụng", CriteriaOperator.Parse("QuanLyTuyenDung=?", _QuanLyTuyenDung.Oid), new string[] { "Caption" }, new string[] { "Vị trí tuyển dụng" }, new int[] { 250 }), "Chọn vị trí tuyển dụng", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    ViTriTuyenDung viTri = popup.CurrentControl.GetData();
                    if (viTri != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        ChiTietTuyenDung chiTiet = ObjectSpace.FindObject<ChiTietTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and ViTriTuyenDung=?", _QuanLyTuyenDung.Oid, viTri.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietTuyenDung>();
                            chiTiet.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                            chiTiet.ViTriTuyenDung = ObjectSpace.GetObjectByKey<ViTriTuyenDung>(viTri.Oid);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 1, "Duyệt hồ sơ", 100);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 2, "Phỏng vấn", 100);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 3, "Thi tuyển", 100);
                            ObjectSpace.CommitChanges();
                        }

                        ObjectSpace = Application.CreateObjectSpace();
                        VongTuyenDung vongTuyenDung = ObjectSpace.FindObject<VongTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and BuocTuyenDung.TenBuocTuyenDung like ?", chiTiet.Oid, "%Phỏng vấn%"));
                        if (vongTuyenDung == null)
                        {
                            vongTuyenDung = ObjectSpace.CreateObject<VongTuyenDung>();
                            vongTuyenDung.ChiTietTuyenDung = ObjectSpace.GetObjectByKey<ChiTietTuyenDung>(chiTiet.Oid);
                            vongTuyenDung.BuocTuyenDung = ObjectSpace.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, "%Phỏng vấn%"));
                        }
                        Application.ShowModelView<VongTuyenDung>(ObjectSpace, vongTuyenDung);
                        SetNotification("Lập danh sách phỏng vấn thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnNhanVaChamDiemHoSo.Caption);
                    }
                }
            }
        }

        private void btnLapDanhSachThiTuyen_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                frmPopUp<ChonDuLieuController<ViTriTuyenDung>> popup = new frmPopUp<ChonDuLieuController<ViTriTuyenDung>>(Application, ObjectSpace, new ChonDuLieuController<ViTriTuyenDung>(Application, ObjectSpace, "Vị trí tuyển dụng", CriteriaOperator.Parse("QuanLyTuyenDung=?", _QuanLyTuyenDung.Oid), new string[] { "Caption" }, new string[] { "Vị trí tuyển dụng" }, new int[] { 250 }), "Chọn vị trí tuyển dụng", true);
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    ViTriTuyenDung viTri = popup.CurrentControl.GetData();
                    if (viTri != null)
                    {
                        ObjectSpace = Application.CreateObjectSpace();
                        ChiTietTuyenDung chiTiet = ObjectSpace.FindObject<ChiTietTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and ViTriTuyenDung=?", _QuanLyTuyenDung.Oid, viTri.Oid));
                        if (chiTiet == null)
                        {
                            chiTiet = ObjectSpace.CreateObject<ChiTietTuyenDung>();
                            chiTiet.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                            chiTiet.ViTriTuyenDung = ObjectSpace.GetObjectByKey<ViTriTuyenDung>(viTri.Oid);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 1, "Duyệt hồ sơ", 100);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 2, "Phỏng vấn", 100);
                            TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 3, "Thi tuyển", 100);
                            ObjectSpace.CommitChanges();
                        }

                        ObjectSpace = Application.CreateObjectSpace();
                        VongTuyenDung vongTuyenDung = ObjectSpace.FindObject<VongTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and BuocTuyenDung.TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi tuyển%"));
                        if (vongTuyenDung == null)
                        {
                            vongTuyenDung = ObjectSpace.CreateObject<VongTuyenDung>();
                            vongTuyenDung.ChiTietTuyenDung = ObjectSpace.GetObjectByKey<ChiTietTuyenDung>(chiTiet.Oid);
                            vongTuyenDung.BuocTuyenDung = ObjectSpace.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi tuyển%"));
                        }
                        Application.ShowModelView<VongTuyenDung>(ObjectSpace, vongTuyenDung);
                        if (vongTuyenDung.Oid != Guid.Empty)
                        {
                            ObjectSpace = Application.CreateObjectSpace();
                            DanhSachThi danhSachThi = ObjectSpace.FindObject<DanhSachThi>(CriteriaOperator.Parse("ChiTietTuyenDung=? and BuocTuyenDung.TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi tuyển%"));
                            if (danhSachThi == null)
                            {
                                danhSachThi = ObjectSpace.CreateObject<DanhSachThi>();
                                danhSachThi.ChiTietTuyenDung = ObjectSpace.GetObjectByKey<ChiTietTuyenDung>(chiTiet.Oid);
                                danhSachThi.BuocTuyenDung = ObjectSpace.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi tuyển%"));
                            }
                            Application.ShowModelView<DanhSachThi>(ObjectSpace, danhSachThi);
                            SetNotification("Lập danh sách thi tuyển thành công.");
                            ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnNhanVaChamDiemHoSo.Caption);
                        }
                    }
                }
            }
        }

        //private void btnLapDanhSachThiChuyenMon_Click(object sender, EventArgs e)
        //{
        //    if (HamDungChung.IsWriteGranted<ChiTietTuyenDung>())
        //    {
        //        ObjectSpace = Application.CreateObjectSpace();
        //        frmPopUp<ChonDuLieuController<ViTriTuyenDung>> popup = new frmPopUp<ChonDuLieuController<ViTriTuyenDung>>(Application, ObjectSpace, new ChonDuLieuController<ViTriTuyenDung>(Application, ObjectSpace, "Vị trí tuyển dụng", CriteriaOperator.Parse("QuanLyTuyenDung=?", _QuanLyTuyenDung.Oid), new string[] { "Caption" }, new string[] { "Vị trí tuyển dụng" }, new int[] { 250 }), "Chọn vị trí tuyển dụng", true);
        //        if (popup.ShowDialog(this) == DialogResult.OK)
        //        {
        //            ViTriTuyenDung viTri = popup.CurrentControl.GetData();
        //            if (viTri != null)
        //            {
        //                ObjectSpace = Application.CreateObjectSpace();
        //                ChiTietTuyenDung chiTiet = ObjectSpace.FindObject<ChiTietTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=? and ViTriTuyenDung=?", _QuanLyTuyenDung.Oid, viTri.Oid));
        //                if (chiTiet == null)
        //                {
        //                    chiTiet = ObjectSpace.CreateObject<ChiTietTuyenDung>();
        //                    chiTiet.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
        //                    chiTiet.ViTriTuyenDung = ObjectSpace.GetObjectByKey<ViTriTuyenDung>(viTri.Oid);
        //                    TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 1, "Duyệt hồ sơ", 100);
        //                    TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 2, "Phỏng vấn", 100);
        //                    TuyenDungHelper.CreateBuocTuyenDung(ObjectSpace, chiTiet, 3, "Thi tuyển", 100);
        //                    ObjectSpace.CommitChanges();
        //                }

        //                ObjectSpace = Application.CreateObjectSpace();
        //                VongTuyenDung vongTuyenDung = ObjectSpace.FindObject<VongTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and BuocTuyenDung.TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi chuyên môn%"));
        //                if (vongTuyenDung == null)
        //                {
        //                    vongTuyenDung = ObjectSpace.CreateObject<VongTuyenDung>();
        //                    vongTuyenDung.ChiTietTuyenDung = ObjectSpace.GetObjectByKey<ChiTietTuyenDung>(chiTiet.Oid);
        //                    vongTuyenDung.BuocTuyenDung = ObjectSpace.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi chuyên môn%"));
        //                }
        //                HamDungChung.ShowModelView<VongTuyenDung>(Application, ObjectSpace, vongTuyenDung);
        //                if (vongTuyenDung.Oid != Guid.Empty)
        //                {
        //                    ObjectSpace = Application.CreateObjectSpace();
        //                    DanhSachThi danhSachThi = ObjectSpace.FindObject<DanhSachThi>(CriteriaOperator.Parse("ChiTietTuyenDung=? and BuocTuyenDung.TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi chuyên môn%"));
        //                    if (danhSachThi == null)
        //                    {
        //                        danhSachThi = ObjectSpace.CreateObject<DanhSachThi>();
        //                        danhSachThi.ChiTietTuyenDung = ObjectSpace.GetObjectByKey<ChiTietTuyenDung>(chiTiet.Oid);
        //                        danhSachThi.BuocTuyenDung = ObjectSpace.FindObject<BuocTuyenDung>(CriteriaOperator.Parse("ChiTietTuyenDung=? and TenBuocTuyenDung like ?", chiTiet.Oid, "%Thi chuyên môn%"));
        //                    }
        //                    HamDungChung.ShowModelView<DanhSachThi>(Application, ObjectSpace, danhSachThi);
        //                    SetNotification("Lập danh sách thi chuyên môn thành công.");
        //                    ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnNhanVaChamDiemHoSo.Caption);
        //                }
        //            }
        //        }
        //    }
        //}

        //private void btnLapDanhSachTrungTuyen_Click(object sender, EventArgs e)
        //{
        //    if (HamDungChung.IsWriteGranted<ChiTietTuyenDung>())
        //    {
        //        ObjectSpace = Application.CreateObjectSpace();
        //        QuanLyTuyenDung qlTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
        //        TuyenDungHelper.TrungTuyen(ObjectSpace, qlTuyenDung);
        //        ObjectSpace = Application.CreateObjectSpace();
        //        qlTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
        //        Application.ShowView<QuanLyTuyenDung>(ObjectSpace, qlTuyenDung);
        //        SetNotification("Xét ứng viên trúng tuyển thành công.");
        //        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDanhSachTrungTuyen.Caption);
        //    }
        //}

        private void btnLapDanhSachTrungTuyen_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<ChiTietTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                QuanLyTuyenDung qlTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                TuyenDungHelper.TrungTuyen(ObjectSpace, qlTuyenDung);
                ObjectSpace = Application.CreateObjectSpace();
                qlTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                Application.ShowView<QuanLyTuyenDung>(ObjectSpace, qlTuyenDung);
                SetNotification("Xét ứng viên trúng tuyển thành công.");
                ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnLapDanhSachTrungTuyen.Caption);
            }
        }

        //lap qd tuyen dung
        private void btnChuyenHoSoUngVien_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuyetDinhTuyenDung>())
            {
                ObjectSpace = Application.CreateObjectSpace();
                List<TuyenDung_TrungTuyen> list = new List<TuyenDung_TrungTuyen>();
                foreach (TrungTuyen item in _QuanLyTuyenDung.ListTrungTuyen)
                {
                    //khong lay thinh giang
                    //if (item.UngVien.NhuCauTuyenDung.ViTriTuyenDung.PhanLoai != LoaiNhanVienEnum.ThinhGiang)
                    //    list.Add(new TuyenDung_TrungTuyen(((XPObjectSpace)ObjectSpace).Session) { Chon = true, TrungTuyen = ObjectSpace.GetObjectByKey<TrungTuyen>(item.Oid) });
                    if (item.UngVien.NhuCauTuyenDung.ViTriTuyenDung.LoaiTuyenDung.TenLoaiTuyenDung != "Thỉnh giảng")
                        list.Add(new TuyenDung_TrungTuyen(((XPObjectSpace)ObjectSpace).Session) { Chon = true, TrungTuyen = ObjectSpace.GetObjectByKey<TrungTuyen>(item.Oid) });
              
                }

                frmChonUngVienTrungTuyen popup = new frmChonUngVienTrungTuyen(list);
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    List<TrungTuyen> uvList = popup.GetListUngVien();
                    if (uvList.Count > 0)
                    {
                        ObjectSpace = Application.CreateObjectSpace();

                        QuyetDinhTuyenDung quyetDinh = ObjectSpace.FindObject<QuyetDinhTuyenDung>(CriteriaOperator.Parse("QuanLyTuyenDung=?", _QuanLyTuyenDung.Oid));
                        if (quyetDinh == null)
                        {
                            quyetDinh = ObjectSpace.CreateObject<QuyetDinhTuyenDung>();
                            quyetDinh.QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(_QuanLyTuyenDung.Oid);
                        }

                        ChiTietQuyetDinhTuyenDung chiTiet;
                        ThongTinNhanVien nhanVien;
                        foreach (TrungTuyen item in uvList)
                        {
                            nhanVien = TuyenDungHelper.HoSoNhanVien(ObjectSpace, item);
                            if (nhanVien != null)
                            {
                                chiTiet = ObjectSpace.FindObject<ChiTietQuyetDinhTuyenDung>(CriteriaOperator.Parse("QuyetDinhTuyenDung=? and ThongTinNhanVien=?", quyetDinh.Oid, nhanVien.Oid));
                                if (chiTiet == null)
                                {
                                    chiTiet = ObjectSpace.CreateObject<ChiTietQuyetDinhTuyenDung>();
                                    chiTiet.BoPhan = ObjectSpace.GetObjectByKey<BoPhan>(nhanVien.BoPhan.Oid);
                                    chiTiet.ThongTinNhanVien = ObjectSpace.GetObjectByKey<ThongTinNhanVien>(nhanVien.Oid);
                                    quyetDinh.ListChiTietQuyetDinhTuyenDung.Add(chiTiet);
                                }
                            }
                        }

                        Application.ShowView<QuyetDinhTuyenDung>(ObjectSpace, quyetDinh);
                        SetNotification("Lập QĐ tuyển dụng thành công.");
                        ThucHienQuyTrinh.ChiTietQuyTrinh(Application.CreateObjectSpace(), btnChuyenHoSo.Caption);
                    }
                }
            }
            else
                SetNotification("Bạn không được cấp quyền.");
        }

        protected override void btnKetThuc_Click(object sender, EventArgs e)
        {
            if (HamDungChung.IsWriteGranted<QuanLyTuyenDung>())
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
                _QuanLyTuyenDung = ObjectSpace.GetObjectByKey<QuanLyTuyenDung>(obj);
                if (_QuanLyTuyenDung != null)
                {
                    Application.ShowView<QuanLyTuyenDung>(ObjectSpace, _QuanLyTuyenDung);
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
            helper.XuLy("QuyTrinhTuyenDung", "Quy trình tuyển dụng");
        }

        private void StartQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Disable;
            btnKetThuc.State = CustomButton.ButtonState.Normal;
            btnLapKeHoachTuyenDung.State = CustomButton.ButtonState.Normal;
            btnDuyetKeHoachTuyenDung.State = CustomButton.ButtonState.Normal;
            btnQDThanhLapHoiDongTuyenDung.State = CustomButton.ButtonState.Normal;
            btnNhanVaChamDiemHoSo.State = CustomButton.ButtonState.Normal;
            btnLapDanhSachPhongVan.State = CustomButton.ButtonState.Normal;
            btnLapDanhSachThiKienThucChung.State = CustomButton.ButtonState.Normal;
            btnLapDanhSachTrungTuyen.State = CustomButton.ButtonState.Normal;
            btnChuyenHoSo.State = CustomButton.ButtonState.Normal;
        }

        private void EndQuyTrinh()
        {
            btnBatDau.State = CustomButton.ButtonState.Normal;
            btnKetThuc.State = CustomButton.ButtonState.Disable;
            btnLapKeHoachTuyenDung.State = CustomButton.ButtonState.Disable;
            btnDuyetKeHoachTuyenDung.State = CustomButton.ButtonState.Disable;
            btnQDThanhLapHoiDongTuyenDung.State = CustomButton.ButtonState.Disable;
            btnNhanVaChamDiemHoSo.State = CustomButton.ButtonState.Disable;
            btnLapDanhSachPhongVan.State = CustomButton.ButtonState.Disable;
            btnLapDanhSachThiKienThucChung.State = CustomButton.ButtonState.Disable;
            btnLapDanhSachTrungTuyen.State = CustomButton.ButtonState.Disable;
            btnChuyenHoSo.State = CustomButton.ButtonState.Disable;
        }
    }
}
