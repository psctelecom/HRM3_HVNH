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
    [ModelDefault("Caption", "Quyết định giải thể đơn vị")]
    public class QuyetDinhGiaiTheDonVi : QuyetDinh
    {
        // Fields...
        private QuyetDinhThanhLapDonVi _QuyetDinhThanhLapDonVi;
        private DateTime _ThoiHanBanGiao;
        private BoPhan _BoPhan;
        //private string _LuuTru;

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
            }
        }

        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Thời hạn bàn giao")]
        public DateTime ThoiHanBanGiao
        {
            get
            {
                return _ThoiHanBanGiao;
            }
            set
            {
                SetPropertyValue("ThoiHanBanGiao", ref _ThoiHanBanGiao, value);
            }
        }

        //[RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Quyết định thành lập đơn vị")]
        public QuyetDinhThanhLapDonVi QuyetDinhThanhLapDonVi
        {
            get
            {
                return _QuyetDinhThanhLapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapDonVi", ref _QuyetDinhThanhLapDonVi, value);
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

        public QuyetDinhGiaiTheDonVi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhGiaiTheDonVi;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                BoPhan.NgungHoatDong = true;
            }
        }

        protected override void OnDeleting()
        {
            BoPhan.NgungHoatDong = false;

            base.OnDeleting();
        }
    }

}
