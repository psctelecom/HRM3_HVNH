using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module.DaoTao;
using PSC_HRM.Module.QuyetDinh;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.ChamCong
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách cán bộ theo hình thức nghỉ")]
    public class TimKiemCanBoTheoHinhThucNghi : TruongBaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private HinhThucNghi _HinhThucNghi;

        [ModelDefault("Caption", "Hình thức nghỉ")]
        [ImmediatePostData]
        public HinhThucNghi HinhThucNghi
        {
            get
            {
                return _HinhThucNghi;
            }
            set
            {
                SetPropertyValue("HinhThucNghi", ref _HinhThucNghi, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [ImmediatePostData]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [ImmediatePostData]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<DanhSachCanBoTheoHinhThucNghi> ListCanBo { get; set; }

        public TimKiemCanBoTheoHinhThucNghi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListCanBo = new XPCollection<DanhSachCanBoTheoHinhThucNghi>(Session, false);
            //
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = new DateTime(current.Year, current.Month, 1);
            DenNgay = TuNgay.AddMonths(1).AddDays(-1);
        }

        public void LoadData()
        {
            if (TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue &&
                TuNgay <= DenNgay)
            {
                //
                ListCanBo.Reload();
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", this.TuNgay);
                param[1] = new SqlParameter("@DenNgay", this.DenNgay);
                param[2] = new SqlParameter("@HinhThucNghi", this.HinhThucNghi != null ? this.HinhThucNghi.Oid : Guid.Empty);
                //
                DataTable dt = DataProvider.GetDataTable("spd_TimKiemDanhSachTheoHinhThucNghi", CommandType.StoredProcedure, param);
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DanhSachCanBoTheoHinhThucNghi DanhSach;
                        foreach (DataRow item in dt.Rows)
                        {
                            DanhSach = new DanhSachCanBoTheoHinhThucNghi(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                                DanhSach.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                            if (DanhSach.ThongTinNhanVien != null)
                            {
                                DanhSach.BoPhan = DanhSach.ThongTinNhanVien.BoPhan;
                                DanhSach.Ngay = DateTime.Parse(item["TuNgay"].ToString());
                            }
                            //
                            ListCanBo.Add(DanhSach);
                        }
                    }
                }          
            }
        }
    }

}
