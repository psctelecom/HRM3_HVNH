using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình khen thưởng")]
    public class QuaTrinhKhenThuong : BaseObject
    {
        private QuyetDinhKhenThuong _QuyetDinh;
        private string _QuyetDinhNgoai;
        private HoSo.HoSo _HoSo;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private NamHoc _NamHoc;
        private DateTime _NgayKhenThuong;
        private string _LyDo;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin cán bộ")]
        public HoSo.HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhKhenThuong QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    if (string.IsNullOrEmpty(SoQuyetDinh))
                        SoQuyetDinh = value.SoQuyetDinh;
                    if (NgayQuyetDinh != DateTime.MinValue)
                        NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Quyết định ngoài")]
        public string QuyetDinhNgoai
        {
            get
            {
                return _QuyetDinhNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhNgoai", ref _QuyetDinhNgoai, value);
            }
        }

        [ModelDefault("Caption", "Năm học")]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    TuNgay = value.NgayBatDau;
                    DenNgay = value.NgayKetThuc;
                }
            }
        }

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

        [ModelDefault("Caption", "Ngày khen thưởng")]
        public DateTime NgayKhenThuong
        {
            get
            {
                return _NgayKhenThuong;
            }
            set
            {
                SetPropertyValue("NgayKhenThuong", ref _NgayKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Lý Do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public QuaTrinhKhenThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Module.HoSo.HoSo.CurrentHoSo != null)
                HoSo = Session.GetObjectByKey<Module.HoSo.HoSo>(Module.HoSo.HoSo.CurrentHoSo.Oid);
        }
    }

}
