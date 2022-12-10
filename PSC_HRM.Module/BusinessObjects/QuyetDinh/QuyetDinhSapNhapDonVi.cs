using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.QuyetDinh
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định sáp nhập đơn vị")]
    public class QuyetDinhSapNhapDonVi : QuyetDinh
    {
        // Fields...
        private BoPhan _BoPhanMoi;
        //private string _LuuTru;

        [ModelDefault("Caption", "Đơn vị mới")]
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

        [Aggregated]
        [ModelDefault("Caption", "Danh sách đơn vị")]
        [Association("QuyetDinhSapNhapDonVi-ListChiTietQuyetDinhSapNhapDonVi")]
        public XPCollection<ChiTietQuyetDinhSapNhapDonVi> ListChiTietQuyetDinhSapNhapDonVi
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhSapNhapDonVi>("ListChiTietQuyetDinhSapNhapDonVi");
            }
        }

        public QuyetDinhSapNhapDonVi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhSapNhapDonVi;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted && (Session is NestedUnitOfWork))
            {
                foreach (ChiTietQuyetDinhSapNhapDonVi chiTiet in ListChiTietQuyetDinhSapNhapDonVi)
                {
                    XPCollection<NhanVien> listNhanVien = new XPCollection<NhanVien>(Session);
                    listNhanVien.Criteria = new InOperator("BoPhan", chiTiet.BoPhan.Oid);

                    //đưa nhân viên qua bộ phận mới
                    foreach (NhanVien item in listNhanVien)
                    {
                        item.BoPhanCu = item.BoPhan;
                        item.BoPhan = BoPhanMoi;
                    }
                    //cho bộ phận cũ hoạt động
                    chiTiet.BoPhan.NgungHoatDong = true;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                XPCollection<NhanVien> listNhanVien = new XPCollection<NhanVien>(Session);
                listNhanVien.Criteria = new InOperator("BoPhan", BoPhanMoi.Oid);

                //đưa nhân viên qua bộ phận cũ
                foreach (NhanVien item in listNhanVien)
                {
                    if (item.BoPhanCu != null)
                    {
                        item.BoPhan = item.BoPhanCu;
                        item.BoPhanCu = null;
                    }
                }

                //cho bộ phận cũ hoạt động
                foreach (ChiTietQuyetDinhSapNhapDonVi chiTiet in ListChiTietQuyetDinhSapNhapDonVi)
                {
                    chiTiet.BoPhan.NgungHoatDong = false;
                }

            }

            base.OnDeleting();
        }
    }

}
