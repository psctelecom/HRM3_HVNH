using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module.HoSo;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace PSC_HRM.Module.NonPersistentObjects
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn giấy tờ hồ sơ")]
    public class GiayToHoSo_ChonGiayToHoSo : BaseObject
    {
        private ThongTinNhanVien _ThongTinNhanVien;
        private GiayToHoSo _GiayToHoSo;

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [ModelDefault("AllowEdit", "False")]
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
                    AfterNhanVienChanged();
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Giấy tờ hồ sơ")]
        [DataSourceProperty("ListGiayToHoSo")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Danh sách giấy tờ hồ sơ")]
        public XPCollection<GiayToHoSo> ListGiayToHoSo { get; set; }

        public GiayToHoSo_ChonGiayToHoSo(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            AfterNhanVienChanged();   
        }

        protected void AfterNhanVienChanged()
        {
            if (ListGiayToHoSo == null)
                ListGiayToHoSo = new XPCollection<GiayToHoSo>();
            //          
            if (ThongTinNhanVien != null)
                ListGiayToHoSo.Filter= CriteriaOperator.Parse("HoSo=?", ThongTinNhanVien.Oid);
        }
    }

}
