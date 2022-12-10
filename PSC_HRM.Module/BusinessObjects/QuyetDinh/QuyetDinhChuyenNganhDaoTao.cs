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
    [ModelDefault("Caption", "Quyết định chuyển ngành đào tạo")]
    public class QuyetDinhChuyenNganhDaoTao : QuyetDinhCaNhan
    {
        // Fields...
        private QuocGia _QuocGia;
        private NganhDaoTao _NganhDaoTaoMoi;
        private QuyetDinhDaoTao _QuyetDinhDaoTao;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhList")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
               
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
                    NganhDaoTaoMoi = null;
                    UpdateNganhList();
                }
            }
        }

        [ModelDefault("Caption", "Ngành đào tạo mới")]
        [DataSourceProperty("NganhList", DataSourcePropertyIsNullMode.SelectAll)]
        [RuleRequiredField(DefaultContexts.Save)]
        public NganhDaoTao NganhDaoTaoMoi
        {
            get
            {
                return _NganhDaoTaoMoi;
            }
            set
            {
                SetPropertyValue("NganhDaoTaoMoi", ref _NganhDaoTaoMoi, value);
            }
        }

        public QuyetDinhChuyenNganhDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //if (string.IsNullOrWhiteSpace(NoiDung))
            //NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhChuyenNganhDaoTao;

            GiayToHoSo.GiayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "Quyết định chuyển ngành đào tạo"));
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            UpdateQuyetDinhList();
            UpdateNganhList();
        }

        protected override void AfterNhanVienChanged()
        {
            QuyetDinhDaoTao = null;
            //cập nhật danh sách quyết định
            UpdateQuyetDinhList();

            //lấy quyết định đào tạo mới nhất
            CriteriaOperator filter = CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
            SortProperty sort = new SortProperty("NgayHieuLuc", DevExpress.Xpo.DB.SortingDirection.Descending);
            using (XPCollection<QuyetDinhDaoTao> qdList = new XPCollection<QuyetDinhDaoTao>(Session, filter, sort))
            {
                qdList.TopReturnedObjects = 1;
                if (qdList.Count == 1)
                    QuyetDinhDaoTao = qdList[0];
            }
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhDaoTao> QuyetDinhList { get; set; }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhDaoTao>(Session);
            QuyetDinhList.Criteria = CriteriaOperator.Parse("ListChiTietDaoTao[ThongTinNhanVien=?]", ThongTinNhanVien.Oid);
        }

        [Browsable(false)]
        private XPCollection<NganhDaoTao> NganhList { get; set; }

        private void UpdateNganhList()
        {
            if (NganhList == null)
                NganhList = new XPCollection<NganhDaoTao>(Session);
            NganhList.Criteria = CriteriaOperator.Parse("QuocGia=?", QuocGia.Oid);
        }
    }

}
