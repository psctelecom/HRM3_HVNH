using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.GiayTo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.BusinessObjects.HoSo;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_VanBang")]
    [DefaultProperty("TrinhDoChuyenMon")]
    [ModelDefault("Caption", "Văn bằng")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "HoSo;NgayHieuLuc", "Không được trùng ngày hiệu lực văn bằng")]
    [Appearance("Hide_Huflit", TargetItems = "ThamNien;NgayThucHien;NgayHieuLuc"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "HoSo.MaTruong != 'HUFLIT'")]
    public class VanBang : BaseObject
    {
        private XepLoaiChungChiEnum _XepLoai = XepLoaiChungChiEnum.KhongChon;
        private decimal _DiemTrungBinh;
        private HoSo _HoSo;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private HinhThucDaoTao _HinhThucDaoTao;private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private int _NamTotNghiep;
        private GiayToHoSo _GiayToHoSo;
        private GiayToHoSo _LuuTruBangDiem;
        private DateTime _NgayCapBang;
        private DateTime _NgayHieuLuc;
        private DateTime _NgayThucHien;
        private ThamNien _ThamNien;

        private string _GhiChu;

        [Browsable(false)]
        [ImmediatePostData]
        [Association("HoSo-ListVanBang")]
        [ModelDefault("Caption", "Nhân viên trình độ")]
        public HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
                if (value != null)
                {
                    //if (!IsLoading && value != null)
                    //{
                    //    InitializeGiayTo();
                    //}
                    UpdateGiayToList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
                if (!IsLoading)
                    SetTrichYeu();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
                if (!IsLoading)
                    SetTrichYeu();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
                if (!IsLoading)
                    SetTrichYeu();
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        //[RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong = 'LUH'")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return _NamTotNghiep;
            }
            set
            {
                SetPropertyValue("NamTotNghiep", ref _NamTotNghiep, value);
                if (!IsLoading)
                    SetTrichYeu();
            }
        }

        [ModelDefault("Caption", "Điểm trung bình")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal DiemTrungBinh
        {
            get
            {
                return _DiemTrungBinh;
            }
            set
            {
                SetPropertyValue("DiemTrungBinh", ref _DiemTrungBinh, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiChungChiEnum XepLoai
        {
            get
            {
                return _XepLoai;
            }
            set
            {
                SetPropertyValue("XepLoai", ref _XepLoai, value);
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Lưu trữ văn bằng")]
        [DataSourceProperty("VanBangList", DataSourcePropertyIsNullMode.SelectAll)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
                if (_GiayToHoSo != null && _GiayToHoSo.HoSo == null && _HoSo != null)
                {
                    //Cập nhật lại hồ sơ cho giấy tờ
                    _GiayToHoSo.HoSo = _HoSo;                   
                }
                if (!IsLoading && value != null)
                    XemGiayToHoSo();
            }
        }

        [ModelDefault("Caption", "Lưu trữ bảng điểm")]
        [DataSourceProperty("BangDiemList", DataSourcePropertyIsNullMode.SelectAll)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo LuuTruBangDiem
        {
            get
            {
                return _LuuTruBangDiem;
            }
            set
            {
                SetPropertyValue("LuuTruBangDiem", ref _LuuTruBangDiem, value);
                if (_LuuTruBangDiem != null && _LuuTruBangDiem.HoSo == null && _HoSo != null)
                {
                    //Cập nhật lại hồ sơ cho giấy tờ
                    _LuuTruBangDiem.HoSo = _HoSo;
                }
                if (!IsLoading && value != null)
                    XemGiayToHoSo();
            }
        }

        [ModelDefault("Caption", "Ngày cấp bằng")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCapBang
        {
            get
            {
                return _NgayCapBang;
            }
            set
            {
                SetPropertyValue("NgayCapBang", ref _NgayCapBang, value);
            }
        }

        [ModelDefault("Caption", "Ngày áp dụng thâm niên")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        //[ModelDefault("AllowEdit", "false")]
        public DateTime NgayHieuLuc
        {
            get
            {
                return _NgayHieuLuc;
            }
            set
            {
                SetPropertyValue("NgayHieuLuc", ref _NgayHieuLuc, value);
            }
        }
        [ModelDefault("Caption", "Ngày thực hiện")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("AllowEdit","false")]
        public DateTime NgayThucHien
        {
            get
            {
                return _NgayThucHien;
            }
            set
            {
                SetPropertyValue("NgayThucHien", ref _NgayThucHien, value);
            }
        }
        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        [ModelDefault("Caption", "Thâm niên")]
        //[ModelDefault("AllowEdit", "false")]
        public ThamNien ThamNien
        {
            get { return _ThamNien; }
            set { _ThamNien = value; }
        }
        public VanBang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MaTruong = TruongConfig.MaTruong;
        }

        private void UpdateGiayToList()
        {
            //if (VanBangList == null)
            //    VanBangList = new XPCollection<GiayToHoSo>(Session);
            //if (BangDiemList == null)
            //    BangDiemList = new XPCollection<GiayToHoSo>(Session);

            VanBangList = HoSo.ListGiayToHoSo;
            BangDiemList = HoSo.ListGiayToHoSo;
            //VanBangList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", HoSo, "%Văn bằng%");
            //BangDiemList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", HoSo, "%Bảng điểm%");
        }

        [Browsable(false)]
        [NonPersistent]
        private string MaTruong
        { get; set; }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> VanBangList { get; set; }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> BangDiemList { get; set; }

        private void SetTrichYeu()
        {
            string trichYeu = ObjectFormatter.Format("Văn bằng {TrinhDoChuyenMon.TenTrinhDoChuyenMon} {ChuyenMonDaoTao.TenChuyenMonDaoTao}, {TruongDaoTao.TenTruongDaoTao} cấp năm {NamTotNghiep:####}",
                this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
            if (GiayToHoSo != null)
                GiayToHoSo.TrichYeu = trichYeu;
        }
        protected override void OnDeleting()
        {
            VanBang vanBang = Session.FindObject<VanBang>(CriteriaOperator.Parse("Oid=?", this.Oid));
            if (vanBang != null)
            {
                NhanVien nhanVien = Session.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=?", vanBang.HoSo.Oid));

                //Nếu văn bằng vừa xóa là văn bằng cao nhất thì cập nhật lại trình độ chuyên môn cao nhất
                if (vanBang.TrinhDoChuyenMon != null && vanBang.TruongDaoTao != null && vanBang.ChuyenMonDaoTao != null)
                {
                    if (nhanVien.NhanVienTrinhDo != null
                         && nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null
                         && nhanVien.NhanVienTrinhDo.TruongDaoTao != null
                         && nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao != null
                         && nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.Oid == vanBang.TrinhDoChuyenMon.Oid
                         && nhanVien.NhanVienTrinhDo.TruongDaoTao.Oid == vanBang.TruongDaoTao.Oid
                         && nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao.Oid == vanBang.ChuyenMonDaoTao.Oid
                         && nhanVien.NhanVienTrinhDo.NamTotNghiep == vanBang.NamTotNghiep)
                    {
                        nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = null;
                        nhanVien.NhanVienTrinhDo.HinhThucDaoTao = null;
                        nhanVien.NhanVienTrinhDo.TruongDaoTao = null;
                        nhanVien.NhanVienTrinhDo.ChuyenMonDaoTao = null;
                        nhanVien.NhanVienTrinhDo.NamTotNghiep = 0;
                    }
                }
            }
            //Tiến hành xóa giấy tờ hồ sơ và bảng điểm nêu có
            if (LuuTruBangDiem != null)
            {
                Session.Delete(LuuTruBangDiem);
                Session.Save(LuuTruBangDiem);
            }
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }

        private void InitializeGiayTo()
        {
            if (GiayToHoSo == null)
                GiayToHoSo = new GiayToHoSo(Session);
            if (LuuTruBangDiem == null)
                LuuTruBangDiem = new GiayToHoSo(Session);

            if (HoSo != null)
            {
                GiayToHoSo.HoSo = HoSo;
                LuuTruBangDiem.HoSo = HoSo;

                DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%văn bằng%"));
                if (giayTo != null)
                {
                    GiayToHoSo.GiayTo = giayTo;
                }
                giayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%bảng điểm%"));
                if (giayTo != null)
                {
                    LuuTruBangDiem.GiayTo = giayTo;
                }
            }
        }

        private void XemGiayToHoSo()
        {
            using (DialogUtil.AutoWait())
            {
                try
                {
                    byte[] data = FptProvider.DownloadFile(GiayToHoSo.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                    if (data != null)
                    {
                        string strTenFile = "TempFile.pdf";
                        //Lưu file vào thư mục bin\Debug
                        HamDungChung.SaveFilePDF(data, strTenFile);
                        //Đọc file pdf
                        frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                        viewer.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }

}
