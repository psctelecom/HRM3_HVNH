using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.PMS.NghiepVu;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.DanhMuc;

namespace PSC_HRM.Module.PMS.BusinessObjects.NonPersistentObjects.UEL
{
    [DefaultClassOptions]
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách thực hiện thanh toán tính thù lao")]
    public class QuanLyNV_ThanhToanThuLao : BaseObject
    {

        private Guid _QuanLyHoatDongKhac;
        private NhanVien _NhanVien;
        private bool _ThinhGiang;
        private BoPhan _BoPhan;


        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thù lao")]
        public Guid QuanLyHoatDongKhac
        {
            get { return _QuanLyHoatDongKhac; }
            set { SetPropertyValue("QuanLyHoatDongKhac", ref _QuanLyHoatDongKhac, value); }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "User")]
        public string User
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Trường")]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Năm học")]
        [ModelDefault("AllowEdit", "False")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get;
            set;
        }
        //[ModelDefault("Caption", "Đợt tính")]
        //[ModelDefault("AllowEdit", "False")]
        //public KyTinhPMS KyTinhPMS
        //{
        //    get;
        //    set;
        //}
        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        //[DataSourceProperty("listNV", DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set {SetPropertyValue("NhanVien",ref _NhanVien,value);}
        }



        [ModelDefault("Caption","Load thỉnh giảng")]
        public bool ThinhGiang
        {
            get { return _ThinhGiang; }
            set { SetPropertyValue("ThinhGiang", ref _ThinhGiang, value); }
        }
        //
        //[Browsable(false)]
        //public XPCollection<NhanVien> listNV
        //{
        //    get;
        //    set;
        //}
        [ModelDefault("Caption", "Danh sách bảng chốt thù lao")]
        public XPCollection<dsQuanLyNV_ThanhToanThuLao> listBangChot { get; set; }


        public QuanLyNV_ThanhToanThuLao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateNV();
            
        }


        //Thực hiện Load danh sách NV thuộc trường UEL
        public void UpdateNV()
        {
            //XPCollection<NhanVien> ls = new XPCollection<NhanVien>(Session, CriteriaOperator.Parse("ThongTinTruong.TenVietTat like ?", "UEL"));
            //    if (listNV != null)
            //    {
            //        foreach (NhanVien item in ls)
            //        {
            //            listNV.Add(item);
            //        }
            //    }
            //    else
            //        listNV = new XPCollection<NhanVien>(Session, false);          
        }


        //Thực hiện việc Load toàn bộ dữ liệu 
        public void LoadData()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách chi tiết khối lượng thanh toán"))
            {
                if (QuanLyHoatDongKhac != null&&QuanLyHoatDongKhac !=Guid.Empty)
                {
                    listBangChot = new XPCollection<dsQuanLyNV_ThanhToanThuLao>(Session, false);
                    SqlParameter[] param = new SqlParameter[5]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@QuanLyHoatDongKhac", QuanLyHoatDongKhac);
                    param[1] = new SqlParameter("@User", User);
                    param[2] = new SqlParameter("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                    param[3] = new SqlParameter("@BoPhan", BoPhan != null ? BoPhan.Oid : Guid.Empty);
                    param[4] = new SqlParameter("@ThinhGiang", ThinhGiang.GetHashCode());
                    DataTable dt = DataProvider.GetDataTable("spd_PMS_BangChotThuLao_LayKhoiLuong", System.Data.CommandType.StoredProcedure, param);
                    if (dt != null)
                    {
                        dsQuanLyNV_ThanhToanThuLao ctbangchot;
                        foreach (DataRow item in dt.Rows)
                        {
                            try
                            {
                                ctbangchot = new dsQuanLyNV_ThanhToanThuLao(Session);
                                ctbangchot.Oid_ChiTiet = Guid.Parse(item["Oid_ChiTiet"].ToString());
                                ctbangchot.NhanVien = Session.GetObjectByKey<NhanVien>(Guid.Parse(item["NhanVien"].ToString()));
                                if(string.IsNullOrEmpty(item["BoPhan"].ToString()) == false)
                                {
                                    ctbangchot.BoPhan = Session.GetObjectByKey<BoPhan>(Guid.Parse(item["BoPhan"].ToString()));
                                }
                                if(string.IsNullOrEmpty(item["BacDaoTao"].ToString()) == false)
                                {
                                    ctbangchot.BacDaoTao = Session.GetObjectByKey<BacDaoTao>(Guid.Parse(item["BacDaoTao"].ToString()));
                                }
                                ctbangchot.KhoanChi = item["KhoanChi"].ToString();
                                ctbangchot.TenMon = item["TenMonHoc"].ToString();
                                ctbangchot.LopHocPhan = item["LopHocPhan"].ToString();
                                ctbangchot.MaLopSinhVien = item["MaLopSV"].ToString();
                                ctbangchot.CNTN_CLC = bool.Parse(item["CuNhanTN_CLC"].ToString());
                                ctbangchot.SiSo = int.Parse(item["SiSo"].ToString());
                                ctbangchot.TongSoTietThucDay = decimal.Parse(item["SoTietThucDay"].ToString());
                                ctbangchot.HeSoChucDanhNhanVien = decimal.Parse(item["HeSoChucDanhNhanVien"].ToString());
                                ctbangchot.HeSoDiaDiem = decimal.Parse(item["HeSoDiaDiem"].ToString());
                                ctbangchot.HeSoLopDong = decimal.Parse(item["HeSoLopDong"].ToString());
                                ctbangchot.HeSoCuNhanTN_CLC = decimal.Parse(item["HeSoCLC"].ToString());
                                ctbangchot.HeSoKhac = decimal.Parse(item["HeSoKhac"].ToString());
                                ctbangchot.TongHeSo = decimal.Parse(item["TongHeSo"].ToString());
                                ctbangchot.GioQuyDoi = decimal.Parse(item["GioQuyDoi"].ToString());
                                listBangChot.Add(ctbangchot);

                            }
                            catch(Exception ex)
                            {
                                string Loi = "Lỗi : " + ex.ToString();
                            }
                        }
                    }
                }
            }
        }
    }
}