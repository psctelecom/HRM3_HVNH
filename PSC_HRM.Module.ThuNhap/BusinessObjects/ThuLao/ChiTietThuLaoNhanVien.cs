using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using System.Data;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ThuNhap.ThuLao
{
    [ImageName("BO_ThuLao")]
    [DefaultProperty("NhanVien")]
    [ModelDefault("Caption", "Chi tiết thù lao cán bộ")]
    //[Appearance("ChiTietThuLaoNhanVien.Khoa", TargetItems = "BangThuLaoNhanVien;BoPhan;NhanVien;SoTien;SoTienChiuThue", Enabled = false,
    //    Criteria = "BangThuLaoNhanVien is not null and ((BangThuLaoNhanVien.KyTinhLuong is not null and BangThuLaoNhanVien.KyTinhLuong.KhoaSo) or BangThuLaoNhanVien.ChungTu is not null)")]
    [Appearance("Hide_HVNH", TargetItems = "DaThanhToan;GiamTru1;GiamTru2;GiamTru3;DienGiai1;DienGiai2;DienGiai3"
                                    , Visibility = ViewItemVisibility.Hide, Criteria = "BangThuLaoNhanVien.ThongTinTruong.TenVietTat = 'NHH'")]
   
    public class ChiTietThuLaoNhanVien : ThuNhapBaseObject, IBoPhan
    {
        private int _ID;
        private DateTime _NgayLap;
        private BangThuLaoNhanVien _BangThuLaoNhanVien;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;
        private decimal _SoTien;
        private decimal _SoTienChiuThue;

        private decimal _GiamTru1;
        private decimal _GiamTru2;
        private decimal _GiamTru3;
        private string _DienGiai;
        private string _DienGiai1;
        private string _DienGiai2;
        private string _DienGiai3;
        private string _OidChiTietBangChotThuLaoGiangDay;
        private Guid _OidThôngTinBangChot;
        private decimal _SoGio;
        //private ChiTietBangChotThuLaoGiangDay 
        private bool _DaThanhToan;


        public ChiTietThuLaoNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        [Size(-1)]
        [ModelDefault("Caption", "Oid ThongTinBangChot")]
        public Guid OidThôngTinBangChot
        {
            get
            {
                return _OidThôngTinBangChot;
            }
            set
            {
                SetPropertyValue("OidThôngTinBangChot", ref _OidThôngTinBangChot, value);
            }
        }
        [Browsable(false)]
        [Size(-1)]
        [ModelDefault("Caption", "Chi tiết chốt thù lao giảng dạy")]
        public string OidChiTietBangChotThuLaoGiangDay
        {
            get
            {
                return _OidChiTietBangChotThuLaoGiangDay;
            }
            set
            {
                SetPropertyValue("OidChiTietBangChotThuLaoGiangDay", ref _OidChiTietBangChotThuLaoGiangDay, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng thù lao nhân viên")]
        [Association("BangThuLaoNhanVien-ListChiTietThuLaoNhanVien")]
        public BangThuLaoNhanVien BangThuLaoNhanVien
        {
            get
            {
                return _BangThuLaoNhanVien;
            }
            set
            {
                SetPropertyValue("BangThuLaoNhanVien", ref _BangThuLaoNhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && value != null)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (!IsLoading && value != null
                    && (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid))
                    BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
            }
        }

        [ModelDefault("Caption", "Số tiền")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTien
        {
            get
            {
                return _SoTien;
            }
            set
            {
                SetPropertyValue("SoTien", ref _SoTien, value);
            }
        }

        [ModelDefault("Caption", "Số tiền chịu thuế")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoTienChiuThue
        {
            get
            {
                return _SoTienChiuThue;
            }
            set
            {
                SetPropertyValue("SoTienChiuThue", ref _SoTienChiuThue, value);
            }
        }
        [ModelDefault("Caption", "Số giờ")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoGio
        {
            get
            {
                return _SoGio;
            }
            set
            {
                SetPropertyValue("SoGio", ref _SoGio, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Diễn giải")]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }
        [ModelDefault("Caption", "Giảm trừ 1")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTru1
        {
            get
            {
                return _GiamTru1;
            }
            set
            {
                SetPropertyValue("GiamTru1", ref _GiamTru1, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Diễn giải - Giảm trừ 1")]
        public string DienGiai1
        {
            get
            {
                return _DienGiai1;
            }
            set
            {
                SetPropertyValue("DienGiai1", ref _DienGiai1, value);
            }
        }
        [ModelDefault("Caption", "Giảm trừ 2")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTru2
        {
            get
            {
                return _GiamTru2;
            }
            set
            {
                SetPropertyValue("GiamTru2", ref _GiamTru2, value);
            }
        }
        [Size(-1)]
        [ModelDefault("Caption", "Diễn giải - Giảm trừ 2")]
        public string DienGiai2
        {
            get
            {
                return _DienGiai2;
            }
            set
            {
                SetPropertyValue("DienGiai2", ref _DienGiai2, value);
            }
        }

        [ModelDefault("Caption", "Giảm trừ 3")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal GiamTru3
        {
            get
            {
                return _GiamTru3;
            }
            set
            {
                SetPropertyValue("GiamTru3", ref _GiamTru3, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Diễn giải - Giảm trừ 3")]
        public string DienGiai3
        {
            get
            {
                return _DienGiai3;
            }
            set
            {
                SetPropertyValue("DienGiai3", ref _DienGiai3, value);
            }
        }

        [ModelDefault("Caption", "Đã thanh toán")]
        public bool DaThanhToan
        {
            get { return _DaThanhToan; }
            set { SetPropertyValue("DaThanhToan", ref _DaThanhToan, value); }
        }
        [Aggregated]
        [ModelDefault("Caption", "Chi tiết tạm ứng")]
        [Association("ChiTietThuLaoNhanVien-ListChiTietTamUng")]
        public XPCollection<ChiTietTamUngThuLao> ListChiTietTamUng
        {
            get
            {
                return GetCollection<ChiTietTamUngThuLao>("ListChiTietTamUng");
            }
        }
        //lưu vết ID bên phần mềm PMS
        //sau khi lập ủy nhiệm chi thì dùng cái ID này để chỉnh sửa cột chứng từ 
        //bên phần mềm PMS
        [Browsable(false)]
        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                SetPropertyValue("ID", ref _ID, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            NgayLap = HamDungChung.GetServerTime();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            GroupOperator go = new GroupOperator(GroupOperatorType.And);
            go.Operands.Add(new InOperator("BoPhan.Oid", HamDungChung.DanhSachBoPhanDuocPhanQuyen(BoPhan)));
            go.Operands.Add(CriteriaOperator.Parse("TinhTrang.TenTinhTrang not like ? or TinhTrang.TenTinhTrang not like ?", "%nghỉ việc%", "%nghỉ hưu%"));

            NVList.Criteria = go;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                //cập nhật số chứng từ cho PMS
                string Truong = HamDungChung.ThongTinTruong(Session).MaQuanLy;
                #region Hiện tại không dùng PMS_UIS
                //if (Truong != "QNU")
                //{
                //    using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_PMS.bin")))
                //    {
                //        if (cnn.State != ConnectionState.Open)
                //            cnn.Open();
                //        SqlParameter[] param = new SqlParameter[2];
                //        param[0] = new SqlParameter("@ID", ID);
                //        param[1] = new SqlParameter("@SoChungTuHRM", Oid.ToString());

                //        const string query = "Update dbo.ChiTienThuLaoGiangDay Set SoChungTuHRM = @SoChungTuHRM Where ID = @ID";
                //        using (SqlCommand cmd = DataProvider.GetCommand(query, CommandType.Text, param))
                //        {
                //            cmd.Connection = cnn;
                //            cmd.ExecuteNonQuery();
                //        }
                //    }
                //}
                #endregion
            }
        }

        protected override void OnDeleting()
        {
            if (ID > 0)
            {

                string Truong = HamDungChung.ThongTinTruong(Session).MaQuanLy;
                #region Hiện tại không dùng PMS_UIS
                //if (Truong != "QNU")
                //{
                //    //cập nhật số chứng từ cho PMS
                //    using (SqlConnection cnn = new SqlConnection(DataProvider.GetConnectionString("PSC_PMS.bin")))
                //    {
                //        if (cnn.State != ConnectionState.Open)
                //            cnn.Open();
                //        SqlParameter[] param = new SqlParameter[1];
                //        param[0] = new SqlParameter("@ID", ID);

                //        const string query = "Update dbo.ChiTienThuLaoGiangDay Set SoChungTuHRM = Null Where ID = @ID";
                //        using (SqlCommand cmd = DataProvider.GetCommand(query, CommandType.Text, param))
                //        {
                //            cmd.Connection = cnn;
                //            cmd.ExecuteNonQuery();
                //        }
                //    }
                //}
                #endregion
            }

            base.OnDeleting();
        }
    }

}
