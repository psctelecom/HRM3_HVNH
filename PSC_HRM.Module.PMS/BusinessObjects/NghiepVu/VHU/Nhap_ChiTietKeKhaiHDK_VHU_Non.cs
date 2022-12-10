using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.NonPersistent;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.PMS.NghiepVu.NCKH
{

    [ModelDefault("Caption", "Nhập Chi tiết hoạt động khác khác(VHU)_Non")]
    [NonPersistent]
    [DefaultProperty("Caption")]
    [Appearance("Show_Nhap_ChiTietKeKhaiHDK_VHU", TargetItems = "ChiTietKhoiLuongGiangDay_Moi"
                                          , Visibility = ViewItemVisibility.Show, Criteria = "DanhMucHoatDongKhac.ChonMonHoc =  true")]
    [Appearance("Hide_Nhap_ChiTietKeKhaiHDK_VHU", TargetItems = "ChiTietKhoiLuongGiangDay_Moi"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "DanhMucHoatDongKhac.ChonMonHoc = false")]
    //[Appearance("Nhap_ChiTietKeKhaiHDK_VHU_Non_Khoa", TargetItems = "SoLuong;SoGioQuyDoi;", Enabled = false, Criteria = "ThanhToanTienMat")]
    [Appearance("Nhap_ChiTietKeKhaiHDK_VHU_Non_Khoa_1", TargetItems = "SoTienThanhToan;", Enabled = false, Criteria = "!ThanhToanTienMat")]
    public class Nhap_ChiTietKeKhaiHDK_VHU_Non : BaseObject
    {
        #region Khai báo nhân viên
        private NhanVien _NhanVien;
        #endregion

        #region Khai báo
        private DanhMucHoatDongKhac _DanhMucHoatDongKhac;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private bool _ThanhToanTienMat = false;
        private decimal _SoTienThanhToan;
        private decimal _SoLuong;
        private decimal _SoGioQuyDoi;
        private string _GhiChu;
        private ChiTietMonHoc_Non _ChiTietKhoiLuongGiangDay_Moi;
        private decimal _SoTC;
        #endregion

        #region Giá trị nhân viên
        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null)
                {
                    LoadMonHoc();                  
                }
            }
        }
        #endregion
        #region Giá trị

        [ModelDefault("Caption", "Loại HĐK")]
        [ImmediatePostData]
        [DataSourceProperty("listDanhMucHoatDongKhac")]
        //[ModelDefault("AllowEdit", "False")]
        public DanhMucHoatDongKhac DanhMucHoatDongKhac
        {
            get { return _DanhMucHoatDongKhac; }
            set
            {
                SetPropertyValue("DanhMucHoatDongKhac", ref _DanhMucHoatDongKhac, value);
                if(!IsLoading && value != null && SoLuong != 0)
                {
                    SoGioQuyDoi = ((SoLuong / value.SoLuong) * value.HeSo);
                    SoTienThanhToan = ((SoLuong / value.SoLuongThanhTien) * value.SoTienThanhToan);
                }
            }
        }
        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        //[ModelDefault("AllowEdit", "False")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if(value != null)
                {
                    HocKy = null;
                    DanhMucHoatDongKhac = null;
                    LoadHDK();
                }
                if (!IsLoading && value != null)
                {
                    LoadMonHoc();
                }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        //[ModelDefault("AllowEdit", "False")]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if (!IsLoading && value != null)
                {
                    LoadMonHoc();
                }
            }
        }
        [ModelDefault("Caption", "Số lượng")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [ImmediatePostData]
        //[ModelDefault("AllowEdit", "False")]
        public decimal SoLuong
        {
            get { return _SoLuong; }
            set
            {
                SetPropertyValue("SoLuong", ref _SoLuong, value);
                if (!IsLoading && value != 0 && DanhMucHoatDongKhac != null)
                {
                    SoGioQuyDoi = (((value / DanhMucHoatDongKhac.SoLuong) * SoTC) * DanhMucHoatDongKhac.HeSo);
                    SoTienThanhToan = ((SoLuong / DanhMucHoatDongKhac.SoLuongThanhTien) * DanhMucHoatDongKhac.SoTienThanhToan);
                }
            }
        }

        [ModelDefault("Caption", "Thanh toán bằng tiền")]
        [ImmediatePostData]
        public bool ThanhToanTienMat
        {
            get { return _ThanhToanTienMat; }
            set { SetPropertyValue("ThanhToanTienMat", ref _ThanhToanTienMat, value); }
        }

        [ModelDefault("Caption", "Số giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("AllowEdit", "False")]
        public decimal SoGioQuyDoi
        {
            get { return _SoGioQuyDoi; }
            set { SetPropertyValue("SoGioQuyDoi", ref _SoGioQuyDoi, value); }
        }

        [ModelDefault("Caption", "Số tiền thanh toán")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        //[ModelDefault("AllowEdit", "False")]
        public decimal SoTienThanhToan
        {
            get { return _SoTienThanhToan; }
            set { SetPropertyValue("SoTienThanhToan", ref _SoTienThanhToan, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Môn học")]
        [DataSourceProperty("listChiTietKhoiLuongGiangDay_Moi")]
        [ImmediatePostData]
        public ChiTietMonHoc_Non ChiTietKhoiLuongGiangDay_Moi
        {
            get { return _ChiTietKhoiLuongGiangDay_Moi; }
            set
            {
                SetPropertyValue("ChiTietKhoiLuongGiangDay_Moi", ref _ChiTietKhoiLuongGiangDay_Moi, value);
                if(!IsLoading && value != null)
                {
                    SoTC = value.SoTinChi;
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ghi chú")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoTC
        {
            get { return _SoTC; }
            set
            {
                SetPropertyValue("SoTC", ref _SoTC, value);
                if (!IsLoading && value != 0 && DanhMucHoatDongKhac != null)
                {
                    SoGioQuyDoi = (((value / DanhMucHoatDongKhac.SoLuong) * SoTC) * DanhMucHoatDongKhac.HeSo);
                }
            }
        }
        #endregion

        [Browsable(false)]
        [ModelDefault("Caption", "DanhMuc List")]
        public XPCollection<DanhMucHoatDongKhac> listDanhMucHoatDongKhac
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Môn học")]
        public XPCollection<ChiTietMonHoc_Non> listChiTietKhoiLuongGiangDay_Moi
        {
            get;
            set;
        }

        public Nhap_ChiTietKeKhaiHDK_VHU_Non(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
            SoTC = 1;
        }

        public void LoadHDK()
        {
            if (listDanhMucHoatDongKhac != null)
                listDanhMucHoatDongKhac.Reload();
            else
                listDanhMucHoatDongKhac = new XPCollection<DanhMucHoatDongKhac>(Session, false);

            if (NamHoc != null)
            {
                CriteriaOperator filte = CriteriaOperator.Parse("NamHoc = ? ", NamHoc.Oid);
                XPCollection<DanhMucHoatDongKhac> ds = new XPCollection<DanhMucHoatDongKhac>(Session, filte);
                foreach(DanhMucHoatDongKhac item in ds)
                {
                    listDanhMucHoatDongKhac.Add(item);
                }
            }
        }

        public void LoadMonHoc()
        {
            if (NamHoc != null && HocKy != null && NhanVien != null)
            {
                if (listChiTietKhoiLuongGiangDay_Moi != null)
                    listChiTietKhoiLuongGiangDay_Moi.Reload();
                else
                    listChiTietKhoiLuongGiangDay_Moi = new XPCollection<ChiTietMonHoc_Non>(Session, false);

                if (NamHoc != null)
                {
                    SqlParameter[] param = new SqlParameter[3];
                    param[0] = new SqlParameter("@NamHoc", NamHoc.Oid);
                    param[1] = new SqlParameter("@NhanVien", NhanVien.Oid);
                    param[2] = new SqlParameter("@HocKy", HocKy == null ? Guid.Empty : HocKy.Oid);
                    SqlCommand cmd = DataProvider.GetCommand("spd_PMS_MonHoc_LayDuLieuMonHoc", CommandType.StoredProcedure, param);
                    DataSet dataset = DataProvider.GetDataSet(cmd);
                    if (dataset != null)
                    {
                        DataTable dt = dataset.Tables[0];
                        foreach (DataRow itemRow in dt.Rows)
                        {
                            ChiTietMonHoc_Non ds = new ChiTietMonHoc_Non(Session);
                            ds.MaMonHoc = itemRow["MaMonHoc"].ToString();
                            ds.TenMonHoc = itemRow["TenMonHoc"].ToString();
                            ds.LopHocPhan = itemRow["LopHocPhan"].ToString();
                            ds.SoTinChi = Convert.ToDecimal(itemRow["SoTinChi"].ToString());
                            listChiTietKhoiLuongGiangDay_Moi.Add(ds);
                        }
                    }                     
                }
            }
        }
    }
}