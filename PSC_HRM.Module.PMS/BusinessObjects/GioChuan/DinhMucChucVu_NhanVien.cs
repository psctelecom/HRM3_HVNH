using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.PMS.CauHinh.HeSo;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.NonPersistentObjects.DanhMuc_View;
using PSC_HRM.Module.CauHinh;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.PMS.GioChuan
{
    [ImageName("BO_ChuyenNgach")]
    [DefaultProperty("TenNhomMonHoc")]
    [ModelDefault("Caption", "Định mức chức vụ")]
    [Appearance("SoGioDinhMuc", TargetItems = "SoGioDinhMuc", BackColor = "Aquamarine", FontColor = "Red")]

    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyGioChuan;NhanVien", "Chức vụ đã tồn tại")]
    [Appearance("Hide_HVNH", TargetItems = "ChucDanh;HeSoChucDanh;SoGioDinhMuc_NCKH_UngDung", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyGioChuan.ThongTinTruong.TenVietTat = 'NHH'")]
    //[Appearance("Khoa_SoGioDinhMuc", TargetItems = "SoGioDinhMuc", Enabled = false, Criteria = "QuanLyGioChuan.ThongTinTruong.TenVietTat = 'NHH'")]


    [Appearance("Hide_DinhMucChucVu_VHU", TargetItems = "DinhMucGioChuan"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyGioChuan.ThongTinTruong.TenVietTat = 'VHU'")]
    [Appearance("Hide_DinhMucChucVu_UEL", TargetItems = "SoGioDinhMuc_NCKH;SoGioDinhMuc_Khac;DinhMucChucVu;ChiTinhGioChuan;KhongDongBo"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyGioChuan.ThongTinTruong.TenVietTat = 'UEL'")]

    public class DinhMucChucVu_NhanVien : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;

        private NhanVien _NhanVien;
        private string _DinhMucChucVu;
        private decimal _SoGioDinhMuc;
        private decimal _SoGioDinhMuc_NCKH_UngDung;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;
        private decimal _DinhMucGioChuan;
        private bool _ChiTinhGioChuan;
        private string _GhiChu;
        private bool _KhongDongBo;

        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucChucVuNhanVien")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Định mức")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.chkComboxEdit_DinhMucChucVu")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string DinhMucChucVu
        {
            get { return _DinhMucChucVu; }
            set
            {
                SetPropertyValue("DinhMucChucVu", ref _DinhMucChucVu, value);

                if (!IsLoading && DinhMucChucVu != string.Empty)
                {
                    if (DinhMucChucVu != null)
                    {
                        if (DinhMucChucVu != null)
                        {
                            try
                            {
                                string sql = "SELECT TOP 1 DinhMuc.SoGioChuan";
                                sql += " FROM dbo.DinhMucChucVu DinhMuc";
                                sql += " JOIN dbo.func_SplitString('" + DinhMucChucVu + "',';') ds ON ds.VALUE=DinhMuc.Oid";
                                sql += " ORDER BY DinhMuc.SoGioChuan ASC";
                                object SoGio = DataProvider.GetValueFromDatabase(sql, System.Data.CommandType.Text);
                                if (SoGio != null)
                                {
                                    SoGioDinhMuc = Convert.ToDecimal(SoGio);
                                }
                            }
                            catch (Exception)
                            {

                                //throw;
                            }

                        }
                    }
                }
            }
        }
        [ModelDefault("Caption", "Đơn vị")]
        //[NonPersistent]
        public string DonVi
        {
            get
            {
                if (NhanVien != null)
                    return NhanVien.BoPhan != null ? NhanVien.BoPhan.TenBoPhan : "";
                else return "";
            }

        }
        [ModelDefault("Caption", "Chức vụ")]
        //[NonPersistent]
        public string ChucVu
        {
            get
            {
                if (NhanVien != null)
                {
                    ThongTinNhanVien ttnv = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid =?", NhanVien.Oid));
                    if (ttnv != null)
                        return ttnv.ChucVu != null ? ttnv.ChucVu.TenChucVu : "";
                    else
                        return "";
                }
                else
                    return "";
            }
        }
        [ModelDefault("Caption", "Chức danh")]
        //[NonPersistent]
        [VisibleInDetailView(false)]
        public string ChucDanh
        {
            get
            {
                if (NhanVien != null)
                    return NhanVien.ChucDanh != null ? NhanVien.ChucDanh.TenChucDanh : "";
                else
                    return "";
            }

        }

        [ModelDefault("Caption", "Hệ số chức danh")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        //[NonPersistent]
        [VisibleInDetailView(false)]
        public decimal HeSoChucDanh
        {
            get
            {
                if (NhanVien != null)
                {
                    CriteriaOperator f = CriteriaOperator.Parse("QuanLyHeSo.NamHoc =? and NhanVien =?", QuanLyGioChuan.NamHoc.Oid, NhanVien.Oid);
                    HeSo_ChucDanhNhanVien hsChucDanh = Session.FindObject<HeSo_ChucDanhNhanVien>(f);
                    if (hsChucDanh != null)
                        return hsChucDanh.HeSo_ChucDanh;
                    else
                        return 0;
                }
                else
                    return 0;
            }

        }
        [ModelDefault("Caption", "Định mức giờ chuẩn")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[NonPersistent]
        public decimal DinhMucGioChuan
        {
            get { return _DinhMucGioChuan; }
            set
            {
                SetPropertyValue("DinhMucGioChuan", ref _DinhMucGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Định mức giờ giảng dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal SoGioDinhMuc
        {
            get { return _SoGioDinhMuc; }
            set
            {
                SetPropertyValue("SoGioDinhMuc", ref _SoGioDinhMuc, value);
            }
        }
        [ModelDefault("Caption", "Định mức giờ chuẩn NCKH(ứng dụng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCKH_UngDung
        {
            get { return _SoGioDinhMuc_NCKH_UngDung; }
            set { SetPropertyValue("SoGioDinhMuc_NCKH_UngDung", ref _SoGioDinhMuc_NCKH_UngDung, value); }
        }
        [ModelDefault("Caption", "Định mức giờ chuẩn NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCHK
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCHK", ref _SoGioDinhMuc_NCKH, value); }
        }
        [ModelDefault("Caption", "Định mức giờ chuẩn TGQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Chỉ tính giờ chuẩn")]
        [Browsable(false)]
        public bool ChiTinhGioChuan
        {
            get { return _ChiTinhGioChuan; }
            set
            {
                SetPropertyValue("ChiTinhGioChuan", ref _ChiTinhGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Không đồng bộ")]
        public bool KhongDongBo
        {
            get { return _KhongDongBo; }
            set
            {
                SetPropertyValue("KhongDongBo", ref _KhongDongBo, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        //[Browsable(false)]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public DinhMucChucVu_NhanVien(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CauHinhChung chChung = Session.FindObject<CauHinhChung>(CriteriaOperator.Parse("ThongTinTruong =?", HamDungChung.ThongTinTruong(Session).Oid));

            if (chChung != null)
                SoGioDinhMuc = chChung.SoGioChuan;
        }
    }
}