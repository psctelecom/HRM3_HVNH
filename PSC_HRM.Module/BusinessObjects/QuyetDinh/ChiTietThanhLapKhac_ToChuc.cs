using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("TenToChuc")]
    [ModelDefault("Caption", "Chi tiết quyết định thành lập khác - Tổ chức")]
    public class ChiTietThanhLapKhac_ToChuc : TruongBaseObject
    {
        private int _SoThuTu;
        private string _TenToChuc;
        private string _GhiChu;
        private QuyetDinhThanhLapKhac _QuyetDinhThanhLapKhac;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định khác")]
        [Association("QuyetDinhThanhLapKhac-ListChiTietThanhLapKhac_ToChuc")]
        public QuyetDinhThanhLapKhac QuyetDinhThanhLapKhac
        {
            get
            {
                return _QuyetDinhThanhLapKhac;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapKhac", ref _QuyetDinhThanhLapKhac, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("ChiTietThanhLapKhac_ToChuc-ListChiTietThanhLapKhac_ThanhVien")]
        public XPCollection<ChiTietThanhLapKhac_ThanhVien> ListChiTietThanhLapKhac_ThanhVien
        {
            get
            {
                return GetCollection<ChiTietThanhLapKhac_ThanhVien>("ListChiTietThanhLapKhac_ThanhVien");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("ChiTietThanhLapKhac_ToChuc-ListChiTietThanhLapKhac_BoPhan")]
        public XPCollection<ChiTietThanhLapKhac_BoPhan> ListChiTietThanhLapKhac_BoPhan
        {
            get
            {
                return GetCollection<ChiTietThanhLapKhac_BoPhan>("ListChiTietThanhLapKhac_BoPhan");
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }
        
        [ModelDefault("Caption", "Tên tổ chức")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenToChuc
        {
            get
            {
                return _TenToChuc;
            }
            set
            {
                SetPropertyValue("TenToChuc", ref _TenToChuc, value);
            }
        }

        [Size(-1)]
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

        public ChiTietThanhLapKhac_ToChuc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
                
        protected override void OnLoaded()
        {
            base.OnLoaded();
        }
    }

}
