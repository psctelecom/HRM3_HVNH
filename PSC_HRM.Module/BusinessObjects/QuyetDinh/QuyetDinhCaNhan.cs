using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module;
using DevExpress.ExpressApp.Editors;


namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    //[Appearance("QuyetDinhCaNhan", TargetItems = "ThongTinNhanVien", Enabled = false, Criteria = "ThongTinNhanVien is not null")]  
    public class QuyetDinhCaNhan : QuyetDinh, IBoPhan
    {
        //private GiayToHoSo _GiayToHoSo;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;       

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading && ThongTinNhanVien != null)
                {
                    if (value != null)
                    {
                        AfterBoPhanChanged();
                        BoPhanText = value.TenBoPhan;
                    }
                    UpdateNhanVienList();                    
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
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
                    AfterNhanVienChanged();
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;
                  
                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;

                }
                UpdateGiayToList();               
            }
        }

        //[Aggregated]
        //[VisibleInListView(false)]
        //[ModelDefault("Caption", "Lưu trữ")]
        //[ExpandObjectMembers(ExpandObjectMembers.Never)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        //public GiayToHoSo GiayToHoSo
        //{
        //    get
        //    {
        //        return _GiayToHoSo;
        //    }
        //    set
        //    {
        //        SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
        //    }
        //}

        public QuyetDinhCaNhan(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();                      
            UpdateNhanVienList();
        }
        
        protected override void OnLoaded()
        {
            base.OnLoaded();            
            UpdateNhanVienList();

            if (ThongTinNhanVien != null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
            }
        }

        protected override void QuyetDinhChanged()
        {
            if (GiayToHoSo != null)
            {
                GiayToHoSo.QuyetDinh = this;
                GiayToHoSo.SoGiayTo = SoQuyetDinh;
                GiayToHoSo.NgayBanHanh = NgayHieuLuc;
                GiayToHoSo.NgayLap = NgayQuyetDinh;
                GiayToHoSo.TrichYeu = NoiDung;
                // thảo thêm
                DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%Quyết định%"));
                if (giayTo != null)
                {
                    GiayToHoSo.GiayTo = giayTo;
                }
                
            }       
        }

        protected virtual void AfterNhanVienChanged() { }

        protected virtual void AfterBoPhanChanged() { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }

        private void UpdateGiayToList()
        {
            //if (GiayToList == null)
            //    GiayToList = new XPCollection<GiayToHoSo>(Session);

            if (ThongTinNhanVien != null)
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                //GiayToList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", ThongTinNhanVien.Oid, "%Quyết định%");
        }
        protected override void OnDeleting()
        {
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }
    }

}
