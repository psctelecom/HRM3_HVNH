using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Xpo.DB;
using PSC_HRM.Module.QuaTrinh;
using PSC_HRM.Module.BaoHiem;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định thành lập hội đồng nâng lương")]
    public class ChiTietQuyetDinhThanhLapHoiDongNangLuong : TruongBaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private string _BoPhanText;
        private ChucVu _ChucVu;
        
        private ChucDanhHoiDongNangLuong _ChucDanhHoiDongNangLuong;
        
        private QuyetDinhThanhLapHoiDongNangLuong _QuyetDinhThanhLapHoiDongNangLuong;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định thành lập hội đồng nâng lương")]
        [Association("QuyetDinhThanhLapHoiDongNangLuong-ListChiTietQuyetDinhThanhLapHoiDongNangLuong")]
        public QuyetDinhThanhLapHoiDongNangLuong QuyetDinhThanhLapHoiDongNangLuong
        {
            get
            {
                return _QuyetDinhThanhLapHoiDongNangLuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapHoiDongNangLuong", ref _QuyetDinhThanhLapHoiDongNangLuong, value);
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
                    UpdateNhanVienList();
                    BoPhanText = value.TenBoPhan;

                }
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Bộ phận")]
        public string BoPhanText
        {
            get
            {
                return _BoPhanText;
            }
            set
            {
                SetPropertyValue("BoPhanText", ref _BoPhanText, value);
            }
        }
        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
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
                    ChucVu = value.ChucVu;
                }
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Chức vụ")]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
            }
        }

        
        [ModelDefault("Caption", "Nhiệm vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucDanhHoiDongNangLuong ChucDanhHoiDongNangLuong
        {
            get
            {
                return _ChucDanhHoiDongNangLuong;
            }
            set
            {
                SetPropertyValue("ChucDanhHoiDongNangLuong", ref _ChucDanhHoiDongNangLuong, value);
            }
        }

        public ChiTietQuyetDinhThanhLapHoiDongNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            UpdateNhanVienList();
        }
                
        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateNhanVienList();
            if (BoPhan != null)
            {
                BoPhanText = BoPhan.TenBoPhan;
            } 

        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
             
       
    }

}
