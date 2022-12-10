using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.PMS.NghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSC_HRM.Module.PMS.BusinessObjects.DanhMuc
{
    [ModelDefault("Caption", "Chi tiết thù lao nhân viên")]
    public class ChiTietThuLaoNhanVien_Update : BaseObject
    {
        private NamHoc _NamHoc;
        private ThongTinTruong _ThongTinTruong;

        [ModelDefault("Caption", "Trường")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public ThongTinTruong ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceCriteria("ISNULL(KeKhai, 0) = 0")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
            }
        }

        //

        [DevExpress.Xpo.Aggregated]
        [ModelDefault("Caption", "Danh sách chi tiết")]
        [Association("ChiTietThuLaoNhanVien_Update-ListKeKhaiNhanVienThuLao")]
        public XPCollection<KeKhaiNhanVienThuLao> ListKeKhaiNhanVienThuLao
        {
            get
            {
                return GetCollection<KeKhaiNhanVienThuLao>("ListKeKhaiNhanVienThuLao");
            }
        }

     
        public ChiTietThuLaoNhanVien_Update(Session session)
            : base(session)
        { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            NamHoc = HamDungChung.GetCurrentNamHoc(Session);
        }
    }
}
