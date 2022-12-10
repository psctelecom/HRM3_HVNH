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

namespace PSC_HRM.Module.SinhNhat
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách sinh nhật cán bộ")]
    public class DanhSachSinhNhatCanBo : TruongBaseObject
    {
        private DateTime _TuNgay;
        private DateTime _DenNgay;

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
        public XPCollection<SinhNhatCanBo> ListSinhNhatCanBo { get; set; }

        public DanhSachSinhNhatCanBo(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListSinhNhatCanBo = new XPCollection<SinhNhatCanBo>(Session, false);
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
                ListSinhNhatCanBo.Reload();
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", this.TuNgay);
                param[1] = new SqlParameter("@DenNgay", this.DenNgay);
                param[2] = new SqlParameter("@ThongTinTruong", HamDungChung.ThongTinTruong(Session).Oid);
                //
                DataTable dt = DataProvider.GetDataTable("spd_Notification_SinhNhat", CommandType.StoredProcedure, param);
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        SinhNhatCanBo sinhNhat;
                        foreach (DataRow item in dt.Rows)
                        {
                            sinhNhat = new SinhNhatCanBo(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                                sinhNhat.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(new Guid(item["ThongTinNhanVien"].ToString()));
                            if (!item.IsNull("NgaySinh"))
                                sinhNhat.NgaySinh = DateTime.Parse(item["NgaySinh"].ToString());
                            if (sinhNhat.ThongTinNhanVien != null)
                            {
                                sinhNhat.BoPhan = sinhNhat.ThongTinNhanVien.BoPhan;
                                sinhNhat.Email = sinhNhat.ThongTinNhanVien.Email;
                            }
                            //
                            ListSinhNhatCanBo.Add(sinhNhat);
                        }
                    }
                }          
            }
        }
    }

}
