using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.ChamCong
{
    [ImageName("BO_ChamCong")]
    [ModelDefault("Caption", "Chi tiết chấm công ngày nghỉ")]
    [DefaultProperty("Caption")]

    [Appearance("HideKhacQNU", TargetItems = "TuNgay1;DenNgay1", Visibility = ViewItemVisibility.Hide, Criteria = "MaTruong != 'QNU'")]
    public class CC_ChamCongNgayNghi : TruongBaseObject
    {
        private BangChamCongNgayNghi _BangChamCongNgayNghi;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private CC_HinhThucNghi _CC_HinhThucNghi;
        private HinhThucNghi _IDHinhThucNghi;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private CacBuoiTrongNgay _CacBuoiTrongNgay_TuNgay;
        private CacBuoiTrongNgay _CacBuoiTrongNgay_DenNgay;
        private decimal _SoNgay;
        private XepLoaiDanhGiaEnum _XepLoaiDanhGia;
        private string _DienGiai;
        private WebUsers _IDWebUsers;
        private DateTime _NgayTao;
        //QNU
        private DateTime _TuNgay1;
        private DateTime _DenNgay1;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Chấm công ngày nghỉ")]
        public string Caption
        {
            get
            {
                if (IDHinhThucNghi != null && IDNhanVien != null)
                    return String.Format("{0}-{1}-{2}-{3}", IDNhanVien.HoTen, IDHinhThucNghi.KyHieu, TuNgay, DenNgay);
                else
                    return "";
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chấm công ngày nghỉ")]
        [Association("BangChamCongNgayNghi-ListChiTietChamCongNgayNghi")]
        public BangChamCongNgayNghi BangChamCongNgayNghi
        {
            get
            {
                return _BangChamCongNgayNghi;
            }
            set
            {
                SetPropertyValue("BangChamCongNgayNghi", ref _BangChamCongNgayNghi, value);
            }
        }

        [Browsable(false)]
        public BoPhan IDBoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("IDBoPhan", ref _BoPhan, value);
            }
        }

       [Browsable(false)]
        public ThongTinNhanVien IDNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("IDNhanVien", ref _ThongTinNhanVien, value);
            }
        }

       [ModelDefault("Caption", "Hình thức nghỉ")]
       [Browsable(false)]
       public CC_HinhThucNghi CC_HinhThucNghi
        {
            get
            {
                return _CC_HinhThucNghi;
            }
            set
            {
                SetPropertyValue("CC_HinhThucNghi", ref _CC_HinhThucNghi, value);
            }
        }

       [ModelDefault("Caption", "Hình thức nghỉ")]
       [RuleRequiredField("", DefaultContexts.Save)]
       public HinhThucNghi IDHinhThucNghi
       {
           get
           {
               return _IDHinhThucNghi;
           }
           set
           {
               SetPropertyValue("IDHinhThucNghi", ref _IDHinhThucNghi, value);
           }
       }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày hiệu lực")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.Date;
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading)
                    SoNgay = TuNgay.TinhSoNgay(DenNgay, Session) ;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày hiệu lực")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.Date;
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading)
                    SoNgay = TuNgay.TinhSoNgay(DenNgay, Session);
            }
        }

        [ModelDefault("Caption", "Buổi - Từ ngày")]
        public CacBuoiTrongNgay CacBuoiTrongNgay_TuNgay
        {
            get
            {
                return _CacBuoiTrongNgay_TuNgay;
            }
            set
            {
                SetPropertyValue("CacBuoiTrongNgay_TuNgay", ref _CacBuoiTrongNgay_TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Buổi - Đến ngày")]
        public CacBuoiTrongNgay CacBuoiTrongNgay_DenNgay
        {
            get
            {
                return _CacBuoiTrongNgay_DenNgay;
            }
            set
            {
                SetPropertyValue("CacBuoiTrongNgay_DenNgay", ref _CacBuoiTrongNgay_DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Số ngày")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiDanhGiaEnum XepLoaiDanhGia
        {
            get
            {
                return _XepLoaiDanhGia;
            }
            set
            {
                SetPropertyValue("XepLoaiDanhGia", ref _XepLoaiDanhGia, value);
            }
        }

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

        [Browsable(false)]
        public WebUsers IDWebUsers
        {
            get
            {
                return _IDWebUsers;
            }
            set
            {
                SetPropertyValue("IDWebUsers", ref _IDWebUsers, value);
            }
        }

        [Browsable(false)]
        public DateTime NgayTao
        {
            get
            {
                return _NgayTao;
            }
            set
            {
                SetPropertyValue("NgayTao", ref _NgayTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày ")]
        public DateTime TuNgay1
        {
            get
            {
                return _TuNgay1;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.Date;
                SetPropertyValue("TuNgay1", ref _TuNgay1, value);
                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày ")]
        public DateTime DenNgay1
        {
            get
            {
                return _DenNgay1;
            }
            set
            {
                if (value != DateTime.MinValue)
                    value = value.Date;
                SetPropertyValue("DenNgay1", ref _DenNgay1, value);
             
            }
        }
        public CC_ChamCongNgayNghi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            TuNgay = HamDungChung.GetServerTime();
            NgayTao = HamDungChung.GetServerTime();
            //
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {
                if (Oid == Guid.Empty || Session is NestedUnitOfWork)
                {
                    if (BangChamCongNgayNghi != null)
                    {
                        if (BangChamCongNgayNghi.BoPhan != null)
                        {
                            IDBoPhan = BangChamCongNgayNghi.BoPhan;
                        }
                        if (BangChamCongNgayNghi.ThongTinNhanVien != null)
                        {
                            IDNhanVien = BangChamCongNgayNghi.ThongTinNhanVien;
                        }
                    }
                }
            }
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
        }

        public void XuLy_Save_BUH()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CC_ChamCongNgayNghi", this.Oid);
            DataProvider.ExecuteNonQuery("spd_WebChamCong_CC_ChamCongNgayNghi_Duyet", CommandType.StoredProcedure, parameter);

            SqlParameter[] parameter1 = new SqlParameter[1];
            parameter1[0] = new SqlParameter("@CC_ChamCongNgayNghi", this.Oid);
            DataProvider.ExecuteNonQuery("spd_WebChamCong_CC_ChamCongNgayNghi_QuanLyNghiPhep_Duyet", CommandType.StoredProcedure, parameter1);
        }

        public void XuLy_Delete_BUH()
        {
            SqlParameter[] parameter = new SqlParameter[1];
            parameter[0] = new SqlParameter("@CC_ChamCongNgayNghi", this.Oid);
            DataProvider.ExecuteNonQuery("spd_WebChamCong_CC_ChamCongNgayNghi_HuyDuyet", CommandType.StoredProcedure, parameter);

            SqlParameter[] parameter1 = new SqlParameter[1];
            parameter1[0] = new SqlParameter("@CC_ChamCongNgayNghi", this.Oid);
            DataProvider.ExecuteNonQuery("spd_WebChamCong_CC_ChamCongNgayNghi_QuanLyNghiPhep_HuyDuyet", CommandType.StoredProcedure, parameter1);
        }
    }

}
