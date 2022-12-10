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

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thỉnh giảng")]
    public class QuyetDinhThinhGiang : QuyetDinh
    {
        private BoPhan _BoPhan;
        private GiangVienThinhGiang _GiangVienThinhGiang;
        private string _MonDay;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

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
                if (!IsLoading && GiangVienThinhGiang != null)
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
        public GiangVienThinhGiang GiangVienThinhGiang
        {
            get
            {
                return _GiangVienThinhGiang;
            }
            set
            {
                SetPropertyValue("GiangVienThinhGiang", ref _GiangVienThinhGiang, value);
                if (!IsLoading && value != null)
                {
                    AfterNhanVienChanged();
                    if (GiayToHoSo != null)
                        GiayToHoSo.HoSo = value;

                    if (BoPhan == null
                        || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                }
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
        [ImmediatePostData]
        [ModelDefault("Caption", "Môn dạy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CurriculumEditor")]
        public string MonDay
        {
            get
            {
                return _MonDay;
            }
            set
            {
                SetPropertyValue("MonDay", ref _MonDay, value);
            }
        }       

        public QuyetDinhThinhGiang(Session session)
            : base(session)
        { }


        public override void AfterConstruction()
        {
            base.AfterConstruction();            
           
            GiayToHoSo.DangLuuTru = Session.FindObject<DangLuuTru>(CriteriaOperator.Parse("TenDangLuuTru like ?", "Bản gốc"));
            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định thỉnh giảng"));
            UpdateNhanVienList();

        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateNhanVienList();

            if (GiangVienThinhGiang != null)
            {
                GiayToList = GiangVienThinhGiang.ListGiayToHoSo;
            }
        }

        //protected override void QuyetDinhChanged()
        //{
        //    if (GiayToHoSo != null)
        //    {
        //        GiayToHoSo.SoGiayTo = SoQuyetDinh;
        //        GiayToHoSo.NgayBanHanh = NgayHieuLuc;
        //        GiayToHoSo.TrichYeu = NoiDung;
        //    }
        //}

        protected virtual void AfterNhanVienChanged() { }

        protected virtual void AfterBoPhanChanged() { }

        [Browsable(false)]
        public XPCollection<GiangVienThinhGiang> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<GiangVienThinhGiang>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
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
