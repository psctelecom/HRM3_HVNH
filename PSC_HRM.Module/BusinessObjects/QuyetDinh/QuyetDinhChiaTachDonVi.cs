using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chia tách đơn vị")]
    public class QuyetDinhChiaTachDonVi : QuyetDinh
    {
        // Fields...
        private bool _QuyetDinhMoi;
        private BoPhan _BoPhanMoi;
        private BoPhan _BoPhan;
        //private string _LuuTru;

        [ModelDefault("Caption", "Tách Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanMoi
        {
            get
            {
                return _BoPhanMoi;
            }
            set
            {
                SetPropertyValue("BoPhanMoi", ref _BoPhanMoi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ Đơn vị")]
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
            }
        }

        [ModelDefault("Caption", "Quyết định còn hiệu lực")]
        public bool QuyetDinhMoi
        {
            get
            {
                return _QuyetDinhMoi;
            }
            set
            {
                SetPropertyValue("QuyetDinhMoi", ref _QuyetDinhMoi, value);
            }
        }

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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhChiaTachDonVi-ListChiTietChiaTachDonVi")]
        public XPCollection<ChiTietChiaTachDonVi> ListChiTietChiaTachDonVi
        {
            get
            {
                return GetCollection<ChiTietChiaTachDonVi>("ListChiTietChiaTachDonVi");
            }
        }

        public QuyetDinhChiaTachDonVi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
            
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChamDutHopDong;           
        }
    }

}
