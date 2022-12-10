using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định đổi tên đơn vị")]
    public class QuyetDinhDoiTenDonVi : QuyetDinh
    {
        // Fields...
        private BoPhan _BoPhanCu;
        private string _TenBoPhanCu;
        private string _TenBoPhanMoi;

        //private string _LuuTru;

        [ModelDefault("Caption", "Đơn vị đổi tên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanCu
        {
            get
            {
                return _BoPhanCu;
            }
            set
            {
                SetPropertyValue("BoPhanCu", ref _BoPhanCu, value);
                TenBoPhanCu = BoPhanCu.TenBoPhan;
            }
        }

        //[Browsable(false)]
        [ModelDefault("Caption", "Tên đơn vị cũ")]
        public string TenBoPhanCu
        {
            get
            {
                return _TenBoPhanCu;
            }
            set
            {
                SetPropertyValue("TenBoPhanCu", ref _TenBoPhanCu, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên đơn vị mới")]
        public string TenBoPhanMoi
        {
            get
            {
                return _TenBoPhanMoi;
            }
            set
            {
                SetPropertyValue("TenBoPhanMoi", ref _TenBoPhanMoi, value);
            }
        }

        //[Browsable(false)]
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

        public QuyetDinhDoiTenDonVi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhDoiTenDonVi;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                BoPhanCu.TenBoPhan = TenBoPhanMoi;
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                BoPhanCu.TenBoPhan = TenBoPhanCu;
            }
        }
    }

}
