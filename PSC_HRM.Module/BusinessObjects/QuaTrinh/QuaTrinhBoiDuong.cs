using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.QuyetDinh;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Quá trình bồi dưỡng")]
    public class QuaTrinhBoiDuong : BaseObject
    {
        private int _STT;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _TuNgay;
        private string _DenNgay;
        private string _NoiBoiDuong;
        private string _NoiDungBoiDuong;
        private QuyetDinhBoiDuong _QuyetDinh;
        private LoaiChungChi _ChungChi;
        private LoaiHinhBoiDuong _LoaiHinhBoiDuong;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
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
                    object obj = Session.Evaluate<QuaTrinhBoiDuong>(CriteriaOperator.Parse("COUNT()"), CriteriaOperator.Parse("ThongTinNhanVien=?", value));
                    if (obj != null)
                        STT = (int)obj + 1;
                    else
                        STT = 1;
                }
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhBoiDuong QuyetDinh
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
                    if(string.IsNullOrEmpty(SoQuyetDinh))
                        SoQuyetDinh = value.SoQuyetDinh;
                    if(NgayQuyetDinh != DateTime.MinValue)
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

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu")]
        public string TuNgay
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

        [ModelDefault("Caption", "Ngày kết thúc")]
        public string DenNgay
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

        [ModelDefault("Caption", "Loại hình bồi dưỡng")]
        public LoaiHinhBoiDuong LoaiHinhBoiDuong
        {
            get
            {
                return _LoaiHinhBoiDuong;
            }
            set
            {
                SetPropertyValue("LoaiHinhBoiDuong", ref _LoaiHinhBoiDuong, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Nơi bồi dưỡng")]
        public string NoiBoiDuong
        {
            get
            {
                return _NoiBoiDuong;
            }
            set
            {
                SetPropertyValue("TruongBoiDuong", ref _NoiBoiDuong, value);
            }
        }

        [Size(8000)]
        [ModelDefault("Caption", "Nội dung bồi dưỡng")]
        public string NoiDungBoiDuong
        {
            get
            {
                return _NoiDungBoiDuong;
            }
            set
            {
                SetPropertyValue("NoiDungBoiDuong", ref _NoiDungBoiDuong, value);
            }
        }

        [ModelDefault("Caption", "Loại chứng chỉ")]
        public LoaiChungChi ChungChi
        {
            get
            {
                return _ChungChi;
            }
            set
            {
                SetPropertyValue("ChungChi", ref _ChungChi, value);
            }
        }

        public QuaTrinhBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }
    }

}
