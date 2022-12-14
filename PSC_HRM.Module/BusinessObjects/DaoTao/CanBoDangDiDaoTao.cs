using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.BaoMat;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;

namespace PSC_HRM.Module.DaoTao
{
    [NonPersistent]
    [ImageName("BO_NghiHuu")]
    [ModelDefault("Caption", "Cán bộ đang đi đào tạo")]
    public class CanBoDangDiDaoTao : TruongBaseObject, ISupportController, IBoPhan
    {
        // Fields...
        private string _SoQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        //private NgachLuong _NgachLuong;
        private string _MaNgach;
        private string _TenNgachLuong;
        private string _TrinhDoChuyenMon;
        private ChuyenMonDaoTao _ChuyenMonDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private QuocGia _QuocGia;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private TinhTrang _TinhTrangQuyetDinh;
        private TinhTrang _TinhTrangHienTai;
        private string _GhiChu;

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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đơn vị")]
        [ImmediatePostData]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [ImmediatePostData]
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

        /*
        [ModelDefault("Caption", "Ngạch lương")]
        public NgachLuong NgachLuong
        {
            get
            {
                return _NgachLuong;
            }
            set
            {
                SetPropertyValue("NgachLuong", ref _NgachLuong, value);
            }
        }
        */

        [ModelDefault("Caption", "Mã ngạch")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaNgach
        {
            get
            {
                return _MaNgach;
            }
            set
            {
                SetPropertyValue("MaNgach", ref _MaNgach, value);
            }
        }

        [ModelDefault("Caption", "Tên ngạch lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNgachLuong
        {
            get
            {
                return _TenNgachLuong;
            }
            set
            {
                SetPropertyValue("TenNgachLuong", ref _TenNgachLuong, value);
            }
        }

        [ModelDefault("Caption", "Trình độ chuyện môn")]
        public string TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
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


        [ModelDefault("Caption", "Tình trạng quyết định")]
        public TinhTrang TinhTrangQuyetDinh
        {
            get
            {
                return _TinhTrangQuyetDinh;
            }
            set
            {
                SetPropertyValue("TinhTrangQuyetDinh", ref _TinhTrangQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng hiện tại")]
        public TinhTrang TinhTrangHienTai
        {
            get
            {
                return _TinhTrangHienTai;
            }
            set
            {
                SetPropertyValue("TinhTrangHienTai", ref _TinhTrangHienTai, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public CanBoDangDiDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
