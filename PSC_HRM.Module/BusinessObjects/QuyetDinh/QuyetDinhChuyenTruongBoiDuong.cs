using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chuyển trường bồi dưỡng")]
    public class QuyetDinhChuyenTruongBoiDuong : QuyetDinhCaNhan
    {
        // Fields...
        private QuocGia _QuocGia;
        private TruongDaoTao _TruongDaoTaoMoi;
        private QuyetDinhBoiDuong _QuyetDinhBoiDuong;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bồi dưỡng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhList")]
        public QuyetDinhBoiDuong QuyetDinhBoiDuong
        {
            get
            {
                return _QuyetDinhBoiDuong;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoiDuong", ref _QuyetDinhBoiDuong, value);
               
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Quốc gia")]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
                if (!IsLoading)
                {
                    TruongDaoTaoMoi = null;
                    UpdateTruongList();
                }
            }
        }

        [ModelDefault("Caption", "Trường đào tạo mới")]
        [DataSourceProperty("TruongList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public TruongDaoTao TruongDaoTaoMoi
        {
            get
            {
                return _TruongDaoTaoMoi;
            }
            set
            {
                SetPropertyValue("TruongDaoTaoMoi", ref _TruongDaoTaoMoi, value);
            }
        }

        public QuyetDinhChuyenTruongBoiDuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChuyenTruongBoiDuong;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateQuyetDinhList();
            UpdateTruongList();
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhBoiDuong = null;
            //cập nhật danh sách quyết định
            UpdateQuyetDinhList();

            //lấy quyết định đào tạo mới nhất
            CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietBoiDuong[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
            using (XPCollection<QuyetDinhBoiDuong> qdList = new XPCollection<QuyetDinhBoiDuong>(Session, filter, sort))
            {
                qdList.TopReturnedObjects = 1;
                if (qdList.Count == 1)
                    QuyetDinhBoiDuong = qdList[0];
            }
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhBoiDuong> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoiDuong>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietBoiDuong[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
        }

        [Browsable(false)]
        private XPCollection<TruongDaoTao> TruongList { get; set; }

        private void UpdateTruongList()
        {
            if (TruongList == null)
                TruongList = new XPCollection<TruongDaoTao>(Session);
            TruongList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia.Oid);
        }
    }

}
