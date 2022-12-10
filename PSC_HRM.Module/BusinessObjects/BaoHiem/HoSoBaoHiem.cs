using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.QuaTrinh;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.BaoHiem
{
    [DefaultClassOptions]
    [ImageName("BO_BaoHiem")]
    [DefaultProperty("SoSoBHXH")]
    [ModelDefault("Caption", "Hồ sơ bảo hiểm")]
    public class HoSoBaoHiem : TruongBaseObject, IBoPhan
    {
        // Fields...
        private TrangThaiThamGiaBaoHiemEnum _TrangThai;
        private bool _KhongThamGiaBHTN;
        private QuyenLoiHuongBHYT _QuyenLoiHuongBHYT;
        private BenhVien _NoiDangKyKhamChuaBenh;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private string _SoTheBHYT;
        private DateTime _NgayThamGiaBHXH;
        private string _SoSoBHXH;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        //mới thêm
        private int _SoThangThamGiaBHXH;

        //[Browsable (false)]
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
        [Association("ThongTinNhanVien-ListHoSoBaoHiem")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
            }
        }

        [ModelDefault("Caption", "Số sổ BHXH")]
        public string SoSoBHXH
        {
            get
            {
                return _SoSoBHXH;
            }
            set
            {
                SetPropertyValue("SoSoBHXH", ref _SoSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "Ngày tham gia BHXH")]
        public DateTime NgayThamGiaBHXH
        {
            get
            {
                return _NgayThamGiaBHXH;
            }
            set
            {
                SetPropertyValue("NgayThamGiaBHXH", ref _NgayThamGiaBHXH, value);
                if(!IsLoading && value != DateTime.MinValue)
                {
                    SoThangThamGiaBHXH = HamDungChung.TinhSoThang(value, DateTime.Now);
                }
            }
        }
        [ModelDefault("Caption", "Số tháng tham gia BHXH")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public int SoThangThamGiaBHXH
        {
            get
            {
                return _SoThangThamGiaBHXH;
            }
            set
            {
                SetPropertyValue("SoThangThamGiaBHXH", ref _SoThangThamGiaBHXH, value);
            }
        }

        [ModelDefault("Caption", "Số thẻ BHYT")]
        public string SoTheBHYT
        {
            get
            {
                return _SoTheBHYT;
            }
            set
            {
                SetPropertyValue("SoTheBHYT", ref _SoTheBHYT, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
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

        [ModelDefault("Caption", "Nơi đăng ký khám chữa bệnh")]
        public BenhVien NoiDangKyKhamChuaBenh
        {
            get
            {
                return _NoiDangKyKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("NoiDangKyKhamChuaBenh", ref _NoiDangKyKhamChuaBenh, value);
            }
        }

        [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
        public QuyenLoiHuongBHYT QuyenLoiHuongBHYT
        {
            get
            {
                return _QuyenLoiHuongBHYT;
            }
            set
            {
                SetPropertyValue("QuyenLoiHuongBHYT", ref _QuyenLoiHuongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không tham gia BHTN")]
        public bool KhongThamGiaBHTN
        {
            get
            {
                return _KhongThamGiaBHTN;
            }
            set
            {
                SetPropertyValue("KhongThamGiaBHTN", ref _KhongThamGiaBHTN, value);
            }
        }

        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiThamGiaBaoHiemEnum TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Quá trình tham gia BHXH")]
        [Association("HoSoBaoHiem-ListQuaTrinhThamGiaBHXH")]
        public XPCollection<QuaTrinhThamGiaBHXH> ListQuaTrinhThamGiaBHXH
        {
            get
            {
                return GetCollection<QuaTrinhThamGiaBHXH>("ListQuaTrinhThamGiaBHXH");
            }
        }

        public HoSoBaoHiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNVList();
            TrangThai = TrangThaiThamGiaBaoHiemEnum.DangThamGia;
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
      
    }

}
