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
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.PMS.DanhMuc;
using PSC_HRM.Module.PMS.NghiepVu;
using System.Windows.Forms;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Chọn thông tin bản chốt")]
    [Appearance("Hide_HVNH", TargetItems = "KyTinhPMS"
        , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'NHH'")]

    public class ChonThongTinBangChot : BaseObject
    {
        private Guid _BangThuLaoNhanVien;
        [Browsable(false)]
        public Guid BangThuLaoNhanVien
        {
            get { return _BangThuLaoNhanVien; }
            set { SetPropertyValue("BangThuLaoNhanVien", ref _BangThuLaoNhanVien, value); }
        }


        private ThongTinTruong _ThongTinTruong;
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }
        private NamHoc _NamHoc;
        private KyTinhPMS _KyTinhPMS;

        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                {
                    UpdateKyTinhPMS();
                    LoadData();
                }
            }
        }

        [ModelDefault("Caption", "Kỳ tính PMS")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("listKyTinhPMS")]
        [ImmediatePostData]
        public KyTinhPMS KyTinhPMS
        {
            get { return _KyTinhPMS; }
            set
            {
                SetPropertyValue("KyTinhPMS", ref _KyTinhPMS, value);
                if (!IsLoading)
                {
                    LoadData();
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<KyTinhPMS> listKyTinhPMS
        {
            get;
            set;
        }
        public void UpdateKyTinhPMS()
        {
            listKyTinhPMS.Reload();
            if (NamHoc != null)
            {
                XPCollection<BangChotThuLao> listBangChotThuLao = new XPCollection<BangChotThuLao>(Session, CriteriaOperator.Parse("NamHoc =?", NamHoc.Oid));
                if (listBangChotThuLao != null)
                {
                    foreach (BangChotThuLao item in listBangChotThuLao)
                    {
                        listKyTinhPMS.Add(item.KyTinhPMS);
                    }
                }
            }
            OnChanged("listKyTinhPMS");
        }


        [ModelDefault("Caption", "Danh sách bảng chốt")]
        public XPCollection<dsBangChotThuLao> listBangChot
        {
            get;
            set;
        }
        public ChonThongTinBangChot(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            listBangChot = new XPCollection<dsBangChotThuLao>(Session, false);
            listKyTinhPMS = new XPCollection<DanhMuc.KyTinhPMS>(Session, false);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }

        public void LoadData()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách bảng chốt"))
            {
                listBangChot.Reload();
                if (NamHoc != null)
                {
                    if (TruongConfig.MaTruong == "UEL")
                    {
                        if (KyTinhPMS == null)
                        {
                            //MessageBox.Show("Vui lòng chọn đợt tính thù lao", "Thông báo");
                            return;
                        }
                    }
                    SqlParameter[] param = new SqlParameter[4]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@ThongTinTruong", ThongTinTruong != null ? ThongTinTruong.Oid : Guid.Empty);
                    param[1] = new SqlParameter("@NamHoc", NamHoc != null ? NamHoc.Oid : Guid.Empty);
                    param[2] = new SqlParameter("@KyTinhPMS", KyTinhPMS != null ? KyTinhPMS.Oid : Guid.Empty);
                    param[3] = new SqlParameter("@BangThuLaoNhanVien", BangThuLaoNhanVien != null ? BangThuLaoNhanVien : Guid.Empty);
                    DataTable dt = DataProvider.GetDataTable("spd_pms_BangThuLaoCanBo_LayThongTinBangChot", System.Data.CommandType.StoredProcedure, param);
                    if (dt != null)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            dsBangChotThuLao BangChotThuLao = new dsBangChotThuLao(Session);
                            if (item["Oid"].ToString() != string.Empty)
                                BangChotThuLao.OidThongTinBangChot = new Guid(item["Oid"].ToString());
                            BangChotThuLao.OidChiTietBangChotThuLaoGiangDay = item["OidChiTietBangChotThuLaoGiangDay"].ToString();
                            BangChotThuLao.MaQuanLy = item["MaQuanLy"].ToString();
                            BangChotThuLao.HoTen = item["HoTen"].ToString();
                            if (item["LopHocPhan"].ToString() != string.Empty)
                                BangChotThuLao.LopHocPhan = item["LopHocPhan"].ToString();
                            if (item["TenHoatDong"].ToString() != string.Empty)
                                BangChotThuLao.TenHoatDong = item["TenHoatDong"].ToString();
                            BangChotThuLao.TongGio = Convert.ToDecimal(item["TongGio"].ToString());
                            BangChotThuLao.SoTienThanhToan = Convert.ToDecimal(item["SoTienThanhToan"].ToString());
                            listBangChot.Add(BangChotThuLao);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bảng chốt chưa khóa!", "Thông báo");
                    }
                }
            }
        }
    }
}