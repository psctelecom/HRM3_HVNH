using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    
    [ModelDefault("Caption", "Quyết định gia hạn đi nước ngoài")]
    public class QuyetDinhGiaHanDiNuocNgoai : QuyetDinhCaNhan
    {
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private QuyetDinhDiNuocNgoai _QuyetDinhDiNuocNgoai;

        [ImmediatePostData]
        [DataSourceProperty("QuyetDinhList")]
        [ModelDefault("Caption", "Quyết định đi nước ngoài")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuyetDinhDiNuocNgoai QuyetDinhDiNuocNgoai
        {
            get
            {
                return _QuyetDinhDiNuocNgoai;
            }
            set
            {
                SetPropertyValue("QuyetDinhDiNuocNgoai", ref _QuyetDinhDiNuocNgoai, value);
                if (!IsLoading && value != null)
                    TuNgay = value.DenNgay;
            }
        }
        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
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

        public QuyetDinhGiaHanDiNuocNgoai(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhDiNuocNgoai = null;
            //cập nhật danh sách quyết định
            UpdateQuyetDinhList();

            //lấy quyết định đào tạo mới nhất
            CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietQuyetDinhDiNuocNgoai[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
            using (XPCollection<QuyetDinhDiNuocNgoai> qdList = new XPCollection<QuyetDinhDiNuocNgoai>(Session, filter, sort))
            {
                qdList.TopReturnedObjects = 1;
                if (qdList.Count == 1)
                    QuyetDinhDiNuocNgoai = qdList[0];
            }            
        }
        
        [Browsable(false)]
        public XPCollection<QuyetDinhDiNuocNgoai> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhDiNuocNgoai>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietQuyetDinhDiNuocNgoai[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
        }

        protected override void OnLoaded()
        {
            base.OnLoading();

            if (GiayToHoSo == null)
            {
                GiayToList = ThongTinNhanVien.ListGiayToHoSo;
                if (GiayToList.Count > 0 && SoQuyetDinh != null)
                {
                    GiayToList.Criteria = CriteriaOperator.Parse("GiayTo like ? and SoGiayTo = ?", "Quyết định", SoQuyetDinh);
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
