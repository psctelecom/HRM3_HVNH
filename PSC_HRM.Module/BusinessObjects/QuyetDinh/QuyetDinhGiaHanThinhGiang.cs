using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.QuyetDinh
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định gia hạn thỉnh giảng")]
    public class QuyetDinhGiaHanThinhGiang : QuyetDinh
    {
        private int _ThoiGianGiaHan;
        private QuyetDinhThinhGiang _QuyetDinhThinhGiang;
        private DateTime _TuNgay;
        private DateTime _DenNgay;       

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định thỉnh giảng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuyetDinhThinhGiang QuyetDinhThinhGiang
        {
            get
            {
                return _QuyetDinhThinhGiang;
            }
            set
            {
                SetPropertyValue("QuyetDinhThinhGiang", ref _QuyetDinhThinhGiang, value);
                if (!IsLoading && value != null)
                {
                    TuNgay = value.DenNgay;

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
                if (!IsLoading && TuNgay != DateTime.MinValue)
                    DenNgay = value.AddMonths(ThoiGianGiaHan).AddDays(-1);
            }
        }
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Thời gian gia hạn (tháng)")]
        public int ThoiGianGiaHan
        {
            get
            {
                return _ThoiGianGiaHan;
            }
            set
            {
                SetPropertyValue("ThoiGianGiaHan", ref _ThoiGianGiaHan, value);
                if (!IsLoading && value > 0)
                {

                    if (!IsLoading && TuNgay != DateTime.MinValue)
                        DenNgay = TuNgay.AddMonths(value).AddDays(-1);
                }
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

        public QuyetDinhGiaHanThinhGiang(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhGiaHanThinhGiang;           
        }
    }
}
  

    

    

