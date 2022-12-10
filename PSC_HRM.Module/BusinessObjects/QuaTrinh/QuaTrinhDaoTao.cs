using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.QuyetDinh;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuaTrinh
{
    [ImageName("BO_QuaTrinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Quá trình đào tạo")]
    public class QuaTrinhDaoTao : BaseObject
    {
        private QuyetDinhDaoTao _QuyetDinh;
        private QuocGia _QuocGia;
        private ThongTinNhanVien _ThongTinNhanVien;
        private int _NamNhapHoc;
        private int _NamTotNghiep;
        private TruongDaoTao _TruongDaoTao;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private HinhThucDaoTao _HinhThucDaoTao;
        private TrinhDoChuyenMon _BangCap;
        private string _SoVanBang;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;

        [Browsable(false)]
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
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinhDaoTao QuyetDinh
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

        [ModelDefault("Caption", "Năm nhập học")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        public int NamNhapHoc
        {
            get
            {
                return _NamNhapHoc;
            }
            set
            {
                SetPropertyValue("NamNhapHoc", ref _NamNhapHoc, value);
            }
        }

        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("EditMask", "####")]
        [ModelDefault("DisplayFormat", "####")]
        public int NamTotNghiep
        {
            get
            {
                return _NamTotNghiep;
            }
            set
            {
                SetPropertyValue("NamTotNghiep", ref _NamTotNghiep, value);
            }
        }

        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
            }
        }

        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenMonDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Văn bằng được cấp")]
        public TrinhDoChuyenMon BangCap
        {
            get
            {
                return _BangCap;
            }
            set
            {
                SetPropertyValue("BangCap", ref _BangCap, value);
            }
        }

        [ModelDefault("Caption", "Số văn bằng")]
        public string SoVanBang
        {
            get
            {
                return _SoVanBang;
            }
            set
            {
                SetPropertyValue("SoVanBang", ref _SoVanBang, value);
            }
        }

        public QuaTrinhDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (ThongTinNhanVien.NhanVien != null)
                ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(ThongTinNhanVien.NhanVien.Oid);
        }
    }
}
