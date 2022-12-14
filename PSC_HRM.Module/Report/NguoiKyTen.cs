using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;

namespace PSC_HRM.Module.Report
{
    //thay người ký hàng loạt, dựa vào trường
    [DefaultClassOptions]
    [ImageName("BO_Position")]
    [DefaultProperty("NhanNguoiKy")]
    [ModelDefault("Caption", "Người ký tên báo cáo")]
    public class NguoiKyTen : BaseObject
    {
        private string _NhanNguoiKy;
        private string _TenNguoiKy;
        private string _Code;
        private BoPhan _BoPhan;
        private NguoiSuDung _NguoiSuDung;

        [ModelDefault("Caption", "Nhãn người ký")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string NhanNguoiKy
        {
            get
            {
                return _NhanNguoiKy;
            }
            set
            {
                SetPropertyValue("NhanNguoiKy", ref _NhanNguoiKy, value);
                if (!IsLoading && value != string.Empty && Code == string.Empty)
                {
                    GetCode();
                }
            }
        }

        [ModelDefault("Caption", "Tên người ký")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNguoiKy
        {
            get
            {
                return _TenNguoiKy;
            }
            set
            {
                SetPropertyValue("TenNguoiKy", ref _TenNguoiKy, value);
            }
        }

        [ModelDefault("Caption", "Code")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        //[ModelDefault("AllowEdit","False")]
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

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
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Người sử dụng")]
        [Association("NguoiSuDung-ListNguoiKyTenBaoCao")]
        public NguoiSuDung NguoiSuDung
        {
            get
            {
                return _NguoiSuDung;
            }
            set
            {
                SetPropertyValue("NguoiSuDung", ref _NguoiSuDung, value);
            }
        }

        public NguoiKyTen(Session session) : base(session) { }

        private void GetCode()
        {
          this.Code = StringHelper.ReplaceVietnameseChar(StringHelper.ToTitleCase(this.NhanNguoiKy)).Replace(" ", String.Empty);
        }
    }

}
