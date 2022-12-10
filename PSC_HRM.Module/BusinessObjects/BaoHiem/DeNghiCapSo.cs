using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.BaoHiem
{
    [ImageName("BO_DeNghiCapSo")]    
    [ModelDefault("Caption", "Cán bộ đề nghị cấp sổ BHXH, thẻ BHYT")]
    public class DeNghiCapSo : BaseObject, IBoPhan
    {
        // Fields...
        private CongViec _ChucDanhCongViec;
        private TinhThanh _TinhThanh;
        private BenhVien _BenhVien;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuanLyDeNghiCapSo _QuanLyDeNghiCapSo;

        [Browsable(false)]
        [Association("QuanLyDeNghiCapSo-ListDeNghiCapSo")]
        public QuanLyDeNghiCapSo QuanLyDeNghiCapSo
        {
            get
            {
                return _QuanLyDeNghiCapSo;
            }
            set
            {
                SetPropertyValue("QuanLyDeNghiCapSo", ref _QuanLyDeNghiCapSo, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNVList();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ChucDanhCongViec = value.CongViecHienNay;
                    HoSoBaoHiem hoso = Session.FindObject<HoSoBaoHiem>(CriteriaOperator.Parse("ThongTinNhanVien=?", value.Oid));
                    if (hoso != null)
                    {
                        BenhVien = hoso.NoiDangKyKhamChuaBenh;
                    }
				}	
            }
        }

        [ModelDefault("Caption", "Chức danh công việc")]
        public CongViec ChucDanhCongViec
        {
            get
            {
                return _ChucDanhCongViec;
            }
            set
            {
                SetPropertyValue("ChucDanhCongViec", ref _ChucDanhCongViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Bệnh viện")]
        public BenhVien BenhVien
        {
            get
            {
                return _BenhVien;
            }
            set
            {
                SetPropertyValue("BenhVien", ref _BenhVien, value);
                if (!IsLoading && value != null)
                    TinhThanh = value.TinhThanh;
            }
        }

        [ModelDefault("Caption", "Tỉnh thành")]
        public TinhThanh TinhThanh
        {
            get
            {
                return _TinhThanh;
            }
            set
            {
                SetPropertyValue("TinhThanh", ref _TinhThanh, value);
            }
        }

        public DeNghiCapSo(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
