using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;


namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chấm dứt kỷ luật")]    
    
    public class QuyetDinhChamDutKyLuat : QuyetDinhCaNhan
    {
        private QuyetDinhKyLuat _QuyetDinhKyLuat;       
        private DateTime _TuNgay;

        [ModelDefault("Caption", "Quyết định kỷ luật")]
        public QuyetDinhKyLuat QuyetDinhKyLuat
        {
            get
            {
                return _QuyetDinhKyLuat;
            }
            set
            {
                SetPropertyValue("QuyetDinhKyLuat", ref _QuyetDinhKyLuat, value);
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

        public QuyetDinhChamDutKyLuat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = "chấm dứt kỷ luật";

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chấm dứt kỷ luật"));
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định chấm dứt kỷ luật", SoQuyetDinh);
                    if (GiayToList.Count > 0)
                        GiayToHoSo = Session.FindObject<GiayToHoSo>(CriteriaOperator.Parse("Oid = ?", GiayToList[0].Oid));
                }
            }
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            
        }

        protected override void OnDeleting()
        {           
            base.OnDeleting();
        }
    }
}
