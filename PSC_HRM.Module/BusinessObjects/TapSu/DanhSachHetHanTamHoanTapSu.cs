using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.QuyetDinh;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.HoSo;
using PSC_HRM.Module;

namespace PSC_HRM.Module.TapSu
{
    [NonPersistent]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Danh sách hết hạn tạm hoãn tập sự")]
    public class DanhSachHetHanTamHoanTapSu : BaseObject
    {
        // Fields...
        private DateTime _DenNgay;
        private DateTime _TuNgay;

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

        [ImmediatePostData]
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

        [ModelDefault("Caption", "Danh sách cán bộ")]
        public XPCollection<HetHanTamHoanTapSu> ListHetHanTamHoanTapSu { get; set; }

        public DanhSachHetHanTamHoanTapSu(Session session)
            : base(session) 
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            ListHetHanTamHoanTapSu = new XPCollection<HetHanTamHoanTapSu>(Session, false);
            DateTime current = HamDungChung.GetServerTime();
            TuNgay = current.SetTime(SetTimeEnum.StartMonth);
            DenNgay = current.SetTime(SetTimeEnum.EndMonth);
        }

        public void XuLy()
        {
            if (TuNgay != DateTime.MinValue &&
                DenNgay != DateTime.MinValue &&
                TuNgay < DenNgay)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("HoanDenNgay>=? and HoanDenNgay<=?",
                    HamDungChung.SetTime(TuNgay, 0), HamDungChung.SetTime(DenNgay, 1));
                XPCollection<QuyetDinhTamHoanTapSu> danhSach = new XPCollection<QuyetDinhTamHoanTapSu>(Session, filter);
                ListHetHanTamHoanTapSu.Reload();
                HetHanTamHoanTapSu hetHan;
                foreach (QuyetDinhTamHoanTapSu item in danhSach)
                {
                    hetHan = new HetHanTamHoanTapSu(Session);
                    hetHan.BoPhan = item.BoPhan;
                    hetHan.CanBoTapSu = item.ThongTinNhanVien;
                    hetHan.QuyetDinhTamHoanTapSu = Session.GetObjectByKey<QuyetDinhTamHoanTapSu>(item.Oid);
                    hetHan.QuyetDinhHuongDanTapSu = item.QuyetDinhHuongDanTapSu;
                    hetHan.TuNgay = item.HoanTuNgay;
                    hetHan.DenNgay = item.HoanDenNgay;
                    ListHetHanTamHoanTapSu.Add(hetHan);
                }
            }
        }
    }

}
