using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using PSC_HRM.Module.TuyenDung;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng xét đền bù đào tạo")]  
    public class QuyetDinhThanhLapHoiDongXetDenBuDaoTao : QuyetDinh
    {       
        //private string _LuuTru;
        private string _TenDenBu;
        private string _DonViDenBu;

        //[ModelDefault("Caption", "Lưu trữ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        //public string LuuTru
        //{
        //    get
        //    {
        //        return _LuuTru;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LuuTru", ref _LuuTru, value);
        //    }
        //}

        [ModelDefault("Caption", "Đơn vị đền bù ")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string DonViDenBu
        {
            get
            {
                return _DonViDenBu;
            }
            set
            {
                SetPropertyValue("DonViDenBu", ref _DonViDenBu, value);
            }
        }

        [ModelDefault("Caption", "Tên đền bù đào tạo")]
        //[ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.FileEditor")]
        public string TenDenBu
        {
            get
            {
                return _TenDenBu;
            }
            set
            {
                SetPropertyValue("TenDenBu", ref _TenDenBu, value);
            }
        }
        
       
        [Aggregated]
        [ModelDefault("Caption", "Danh sách hội đồng")]
        [Association("QuyetDinhThanhLapHoiDongXetDenBuDaoTao-ListHoiDongXetDenBuDaoTao")]
        public XPCollection<HoiDongXetDenBuDaoTao> ListHoiDongXetDenBuDaoTao
        {
            get
            {
                return GetCollection<HoiDongXetDenBuDaoTao>("ListHoiDongXetDenBuDaoTao");
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhThanhLapHoiDongXetDenBuDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongTuyenDung;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if(!IsDeleted)
            {
                //Lưu tên bộ phận, nhân viên hiển thị ra listview
                if (ListHoiDongXetDenBuDaoTao.Count == 1)
                {
                    BoPhanText = ListHoiDongXetDenBuDaoTao[0].BoPhan.TenBoPhan;
                    NhanVienText = ListHoiDongXetDenBuDaoTao[0].ThongTinNhanVien.HoTen;
                }
                else
                {
                    BoPhanText = string.Empty;
                    NhanVienText = string.Empty;
                }
            }
        }

        
    }
}
