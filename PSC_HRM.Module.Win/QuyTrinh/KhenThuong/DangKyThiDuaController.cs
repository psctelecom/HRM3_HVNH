using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.KhenThuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.Win.QuyTrinh.Common;
using System.Threading.Tasks;

namespace PSC_HRM.Module.Win.QuyTrinh.KhenThuong
{
    public partial class DangKyThiDuaController : BaseController
    {
        private QuanLyKhenThuong _QuanLyKhenThuong;
        private IObjectSpace _Obs;
        private bool _IsDeNghi;
        private bool _IsKhenThuong;

        public DangKyThiDuaController()
        {
            InitializeComponent();
        }

        public DangKyThiDuaController(IObjectSpace obs, QuanLyKhenThuong quanLyKhenThuong, bool isDeNghi, bool isKhenThuong)
        {
            InitializeComponent();

            _Obs = obs;
            unitOfWork1 = new UnitOfWork(((XPObjectSpace)_Obs).Session.DataLayer);
            listDanhHieuKhenThuong.Session = unitOfWork1;

            _QuanLyKhenThuong = unitOfWork1.GetObjectByKey<QuanLyKhenThuong>(quanLyKhenThuong.Oid);

            chonDanhSachNhanVienController1.Session = unitOfWork1;
            chonDanhSachBoPhanController1.Session = unitOfWork1;

            _IsDeNghi = isDeNghi;
            _IsKhenThuong = isKhenThuong;

            if (_QuanLyKhenThuong != null && isDeNghi)
            {
                List<Guid> danhHieuList = new List<Guid>();
                foreach (ChiTietDangKyThiDua item in _QuanLyKhenThuong.ListChiTietDangKyThiDua)
                {
                    danhHieuList.Add(item.DanhHieuKhenThuong.Oid);
                }
            }
        }

        private void DangKyThiDuaController_Load(object sender, EventArgs e)
        {
            gridDanhHieuKhenThuong.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridDanhHieuKhenThuong.InitPopupFormSize(gridDanhHieuKhenThuong.Width, 300);
            gridViewDanhHieuKhenThuong.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewDanhHieuKhenThuong.ShowField(new string[] { "TenDanhHieu" }, new string[] { "Danh hiệu" });
        }

        /// <summary>
        /// Tạo đăng ký thi đua
        /// </summary>
        public void CreateDangKyThiDua()
        {
            DanhHieuKhenThuong danhHieu = GetDanhHieuKhenThuong();
            if (danhHieu != null && _QuanLyKhenThuong != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuanLyKhenThuong=? and DanhHieuKhenThuong=?",
                    _QuanLyKhenThuong.Oid, danhHieu.Oid);
                ChiTietDangKyThiDua dangKy = unitOfWork1.FindObject<ChiTietDangKyThiDua>(filter);
                if (dangKy == null)
                {
                    dangKy = new ChiTietDangKyThiDua(unitOfWork1);
                    dangKy.QuanLyKhenThuong = _QuanLyKhenThuong;
                    dangKy.DanhHieuKhenThuong = danhHieu;
                }

                filter = new InOperator("Oid", chonDanhSachNhanVienController1.GetNhanVienList());
                using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(unitOfWork1, filter))
                {
                    ChiTietCaNhanDangKyThiDua caNhan;
                    foreach (ThongTinNhanVien item in nvList)
                    {
                        filter = CriteriaOperator.Parse("ChiTietDangKyThiDua=? and ThongTinNhanVien=?", dangKy.Oid, item.Oid);
                        caNhan = unitOfWork1.FindObject<ChiTietCaNhanDangKyThiDua>(filter);
                        if (caNhan == null)
                        {
                            caNhan = new ChiTietCaNhanDangKyThiDua(unitOfWork1);
                            caNhan.BoPhan = item.BoPhan;
                            caNhan.ThongTinNhanVien = item;
                            dangKy.ListChiTietCaNhanDangKyThiDua.Add(caNhan);
                        }
                    }
                }

                filter = new InOperator("Oid", chonDanhSachBoPhanController1.GetBoPhanList());
                using (XPCollection<BoPhan> bpList = new XPCollection<BoPhan>(unitOfWork1, filter))
                {
                    ChiTietTapTheDangKyThiDua tapThe;
                    foreach (BoPhan item in bpList)
                    {
                        filter = CriteriaOperator.Parse("ChiTietDangKyThiDua=? and BoPhan=?", dangKy.Oid, item.Oid);
                        tapThe = unitOfWork1.FindObject<ChiTietTapTheDangKyThiDua>(filter);
                        if (tapThe == null)
                        {
                            tapThe = new ChiTietTapTheDangKyThiDua(unitOfWork1);
                            tapThe.BoPhan = item;
                            dangKy.ListChiTietTapTheDangKyThiDua.Add(tapThe);
                        }
                    }
                }

                unitOfWork1.CommitChanges();
            }
        }

        /// <summary>
        /// Tạo đề nghị khen thưởng
        /// </summary>
        public void CreateDeNghiKhenThuong()
        {
            DanhHieuKhenThuong danhHieu = GetDanhHieuKhenThuong();
            if (danhHieu != null && _QuanLyKhenThuong != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuanLyKhenThuong=? and DanhHieuKhenThuong=?",
                    _QuanLyKhenThuong.Oid, danhHieu.Oid);
                ChiTietDeNghiKhenThuong deNghi = unitOfWork1.FindObject<ChiTietDeNghiKhenThuong>(filter);
                if (deNghi == null)
                {
                    deNghi = new ChiTietDeNghiKhenThuong(unitOfWork1);
                    deNghi.QuanLyKhenThuong = _QuanLyKhenThuong;
                    deNghi.DanhHieuKhenThuong = danhHieu;
                }

                filter = new InOperator("Oid", chonDanhSachNhanVienController1.GetNhanVienList());
                using (XPCollection<ThongTinNhanVien> nvList = new XPCollection<ThongTinNhanVien>(unitOfWork1, filter))
                {
                    ChiTietCaNhanDeNghiKhenThuong caNhan;
                    foreach (ThongTinNhanVien item in nvList)
                    {
                        filter = CriteriaOperator.Parse("ChiTietDeNghiKhenThuong=? and ThongTinNhanVien=?", deNghi.Oid, item.Oid);
                        caNhan = unitOfWork1.FindObject<ChiTietCaNhanDeNghiKhenThuong>(filter);
                        if (caNhan == null)
                        {
                            caNhan = new ChiTietCaNhanDeNghiKhenThuong(unitOfWork1);
                            caNhan.BoPhan = item.BoPhan;
                            caNhan.ThongTinNhanVien = item;
                            deNghi.ListChiTietCaNhanDeNghiKhenThuong.Add(caNhan);
                        }
                    }
                }

                filter = new InOperator("Oid", chonDanhSachBoPhanController1.GetBoPhanList());
                using (XPCollection<BoPhan> bpList = new XPCollection<BoPhan>(unitOfWork1, filter))
                {
                    ChiTietTapTheDeNghiKhenThuong tapThe;
                    foreach (BoPhan item in bpList)
                    {
                        filter = CriteriaOperator.Parse("ChiTietDeNghiKhenThuong=? and BoPhan=?", deNghi.Oid, item.Oid);
                        tapThe = unitOfWork1.FindObject<ChiTietTapTheDeNghiKhenThuong>(filter);
                        if (tapThe == null)
                        {
                            tapThe = new ChiTietTapTheDeNghiKhenThuong(unitOfWork1);
                            tapThe.BoPhan = item;
                            deNghi.ListChiTietTapTheDeNghiKhenThuong.Add(tapThe);
                        }
                    }
                }

                unitOfWork1.CommitChanges();
            }
        }

        private void gridDanhHieuKhenThuong_EditValueChanged(object sender, EventArgs e)
        {
            if (_QuanLyKhenThuong != null)
            {
                DanhHieuKhenThuong danhHieu = GetDanhHieuKhenThuong();
                if (_IsDeNghi && danhHieu != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("QuanLyKhenThuong=? and DanhHieuKhenThuong=?", _QuanLyKhenThuong.Oid, danhHieu.Oid);
                    ChiTietDangKyThiDua dangKy = unitOfWork1.FindObject<ChiTietDangKyThiDua>(filter);
                    if (dangKy != null)
                    {
                        BoPhanList bpList = new BoPhanList();
                        foreach (ChiTietTapTheDangKyThiDua item in dangKy.ListChiTietTapTheDangKyThiDua)
                        {
                            bpList.Add(new BoPhanItem { Oid = item.BoPhan.Oid, BoPhan = item.BoPhan.TenBoPhan });
                        }
                        chonDanhSachBoPhanController1.DataSource = bpList;
                        chonDanhSachBoPhanController1.RefreshData();

                        NhanVienList nvList = new NhanVienList();
                        foreach (ChiTietCaNhanDangKyThiDua item in dangKy.ListChiTietCaNhanDangKyThiDua)
                        {
                            nvList.Add(new NhanVienItem { Oid = item.ThongTinNhanVien.Oid, Ho = item.ThongTinNhanVien.Ho, Ten = item.ThongTinNhanVien.Ten, BoPhan = item.ThongTinNhanVien.BoPhan.TenBoPhan });
                        }
                        chonDanhSachNhanVienController1.DataSource = nvList;
                        chonDanhSachNhanVienController1.RefreshData();
                    }
                }
                else if (_IsKhenThuong && danhHieu != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("QuanLyKhenThuong=? and DanhHieuKhenThuong=?", _QuanLyKhenThuong.Oid, danhHieu.Oid);
                    ChiTietDeNghiKhenThuong deNghi = unitOfWork1.FindObject<ChiTietDeNghiKhenThuong>(filter);
                    if (deNghi != null)
                    {
                        BoPhanList bpList = new BoPhanList();
                        foreach (ChiTietTapTheDeNghiKhenThuong item in deNghi.ListChiTietTapTheDeNghiKhenThuong)
                        {
                            bpList.Add(new BoPhanItem { Oid = item.BoPhan.Oid, BoPhan = item.BoPhan.TenBoPhan });
                        }
                        chonDanhSachBoPhanController1.DataSource = bpList;
                        chonDanhSachBoPhanController1.RefreshData();

                        NhanVienList nvList = new NhanVienList();
                        foreach (ChiTietCaNhanDeNghiKhenThuong item in deNghi.ListChiTietCaNhanDeNghiKhenThuong)
                        {
                            nvList.Add(new NhanVienItem { Oid = item.ThongTinNhanVien.Oid, Ho = item.ThongTinNhanVien.Ho, Ten = item.ThongTinNhanVien.Ten, BoPhan = item.ThongTinNhanVien.BoPhan.TenBoPhan });
                        }
                        chonDanhSachNhanVienController1.DataSource = nvList;
                        chonDanhSachNhanVienController1.RefreshData();
                    }
                }
            }
        }

        /// <summary>
        /// Get list nhân viên
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetNhanVienList()
        {
            return chonDanhSachNhanVienController1.GetNhanVienList();
        }

        /// <summary>
        /// Get list bộ phận
        /// </summary>
        /// <returns></returns>
        public List<Guid> GetBoPhanList()
        {
            return chonDanhSachBoPhanController1.GetBoPhanList();
        }

        public DanhHieuKhenThuong GetDanhHieuKhenThuong()
        {
            DanhHieuKhenThuong danhHieu = gridViewDanhHieuKhenThuong.GetFocusedRow() as DanhHieuKhenThuong;
            return danhHieu;
        }

        private void gridDanhHieuKhenThuong_Resize(object sender, EventArgs e)
        {
            gridDanhHieuKhenThuong.InitPopupFormSize(gridDanhHieuKhenThuong.Width, 300);
        }
    }
}
