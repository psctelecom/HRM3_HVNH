using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.KhenThuong;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.Win.QuyTrinh.Common;
using System.Threading.Tasks;
using PSC_HRM.Module.BoiDuong;

namespace PSC_HRM.Module.Win.QuyTrinh.BoiDuong
{
    public partial class DangKyBoiDuongController : BaseController
    {
        private QuanLyBoiDuong _QuanLyBoiDuong;
        private IObjectSpace _Obs;
        private bool _IsDuyetDeNghi;
        private bool _IsQuyetDinh;

        public DangKyBoiDuongController()
        {
            InitializeComponent();
        }

        public DangKyBoiDuongController(IObjectSpace obs, QuanLyBoiDuong quanLyBoiDuong, bool isDuyetDeNghi, bool isQuyetDinh)
        {
            InitializeComponent();

            _Obs = obs;
            unitOfWork1 = new UnitOfWork(((XPObjectSpace)_Obs).Session.DataLayer);
            listChuongTrinhBoiDuong.Session = unitOfWork1;

            _QuanLyBoiDuong = unitOfWork1.GetObjectByKey<QuanLyBoiDuong>(quanLyBoiDuong.Oid);

            chonDanhSachNhanVienController1.Session = unitOfWork1;

            _IsDuyetDeNghi = isDuyetDeNghi;
            _IsQuyetDinh = isQuyetDinh;

            if (_QuanLyBoiDuong != null && isDuyetDeNghi)
            {
                List<Guid> chuongTrinhList = new List<Guid>();
                foreach (DangKyBoiDuong item in _QuanLyBoiDuong.ListDangKyBoiDuong)
                {
                    chuongTrinhList.Add(item.ChuongTrinhBoiDuong.Oid);
                }
                listChuongTrinhBoiDuong.Criteria = new InOperator("Oid", chuongTrinhList);
            }

            if (_QuanLyBoiDuong != null && isQuyetDinh)
            {
                List<Guid> chuongTrinhList = new List<Guid>();
                foreach (DuyetDangKyBoiDuong item in _QuanLyBoiDuong.ListDuyetDangKyBoiDuong)
                {
                    chuongTrinhList.Add(item.ChuongTrinhBoiDuong.Oid);
                }
                listChuongTrinhBoiDuong.Criteria = new InOperator("Oid", chuongTrinhList);
            }
        }

        private void DangKyThiDuaController_Load(object sender, EventArgs e)
        {
            gridObjects.InitGridLookUp(true, true, DevExpress.XtraEditors.Controls.TextEditStyles.Standard);
            gridObjects.InitPopupFormSize(gridObjects.Width, 300);
            gridViewObjects.InitGridView(true, false, DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect, false, false);
            gridViewObjects.ShowField(new string[] { "TenChuongTrinhBoiDuong", "DonViToChuc" }, new string[] { "Chương trình bồi dưỡng", "Đơn vị tổ chức" });
        }

        private void gridObjects_EditValueChanged(object sender, EventArgs e)
        {
            if (_QuanLyBoiDuong != null)
            {
                ChuongTrinhBoiDuong obj = GetChuongTrinhBoiDuong();
                if (_IsDuyetDeNghi && obj != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("QuanLyBoiDuong=? and ChuongTrinhBoiDuong=?", _QuanLyBoiDuong.Oid, obj.Oid);
                    DangKyBoiDuong dangKy = unitOfWork1.FindObject<DangKyBoiDuong>(filter);
                    if (dangKy != null)
                    {
                        NhanVienList nvList = new NhanVienList();
                        foreach (ChiTietDangKyBoiDuong item in dangKy.ListChiTietDangKyBoiDuong)
                        {
                            nvList.Add(new NhanVienItem { Oid = item.ThongTinNhanVien.Oid, Ho = item.ThongTinNhanVien.Ho, Ten = item.ThongTinNhanVien.Ten, BoPhan = item.ThongTinNhanVien.BoPhan.TenBoPhan });
                        }
                        chonDanhSachNhanVienController1.DataSource = nvList;
                        chonDanhSachNhanVienController1.RefreshData();
                    }
                } 
                if (_IsQuyetDinh && obj != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("QuanLyBoiDuong=? and ChuongTrinhBoiDuong=?", _QuanLyBoiDuong.Oid, obj.Oid);
                    DuyetDangKyBoiDuong dangKy = unitOfWork1.FindObject<DuyetDangKyBoiDuong>(filter);
                    if (dangKy != null)
                    {
                        NhanVienList nvList = new NhanVienList();
                        foreach (ChiTietDuyetDangKyBoiDuong item in dangKy.ListChiTietDuyetDangKyBoiDuong)
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

        public ChuongTrinhBoiDuong GetChuongTrinhBoiDuong()
        {
            ChuongTrinhBoiDuong obj = gridViewObjects.GetFocusedRow() as ChuongTrinhBoiDuong;
            return obj;
        }

        private void gridObjects_Resize(object sender, EventArgs e)
        {
            gridObjects.InitPopupFormSize(gridObjects.Width, 300);
        }
    }
}
