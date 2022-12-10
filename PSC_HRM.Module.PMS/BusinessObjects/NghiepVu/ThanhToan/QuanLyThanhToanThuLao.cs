using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PSC_HRM.Module.PMS.DanhMuc;

using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.PMS.BusinessObjects.NghiepVu.ThanhToan
{
    [ModelDefault("Caption", "Quản lý thanh toán thù lao")]
    [Appearance("Hide_QNU", TargetItems = "HocKy"
                                          , Visibility = ViewItemVisibility.Hide, Criteria = "ThongTinTruong.TenVietTat = 'QNU'")]
    public class QuanLyThanhToanThuLao : ThongTinChungPMS
    {

        [Aggregated]
        [Association("QuanLyThanhToanThuLao-ListThanhToanVuotDinhMuc")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ThanhToanVuotDinhMuc> ListThanhToanVuotDinhMuc
        {
            get
            {
                return GetCollection<ThanhToanVuotDinhMuc>("ListThanhToanVuotDinhMuc");
            }
        }
        public QuanLyThanhToanThuLao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
