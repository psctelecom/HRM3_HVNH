using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module;
using PSC_HRM.Module.PMS.Enum;
using PSC_HRM.Module.PMS.DanhMuc;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.PMS.CauHinh.HeSo
{

    [ModelDefault("Caption", "Hệ số cơ sở")]
    [DefaultProperty("TenCoSo")]
    [Appearance("Hide_QNU", TargetItems = "BacDaoTao;ThoiGiangHoc", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.TenVietTat = 'QNU'")]
    [Appearance("Hide_NEU", TargetItems = "BacDaoTao;ThoiGiangHoc", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.MaQuanLy = 'NEU'")]
    [Appearance("Hide_VHU", TargetItems = "BacDaoTao;ThoiGiangHoc;TenCoSo", Visibility = ViewItemVisibility.Hide, Criteria = "QuanLyHeSo.ThongTinTruong.MaQuanLy = 'VHU'")]
    public class HeSoCoSo : BaseObject
    {
        private QuanLyHeSo _QuanLyHeSo;
        [ModelDefault("Caption", "Quản lý hệ số")]
        [Browsable(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("QuanLyHeSo-ListHeSoCoSo")]
        public QuanLyHeSo QuanLyHeSo
        {
            get
            {
                return _QuanLyHeSo;
            }
            set
            {
                SetPropertyValue("QuanLyHeSo", ref _QuanLyHeSo, value);
            }
        }
        private BacDaoTao _BacDaoTao;
        private CoSoGiangDay _CoSoGiangDay;
        private string _TenCoSo;
        private ThoiGiangHocEnum _ThoiGiangHoc;
        private decimal _HeSo_CoSo;
        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Tên cơ sở")]
        [Browsable(false)]
        public string TenCoSo
        {
            get { return _TenCoSo; }
            set { SetPropertyValue("TenCoSo", ref _TenCoSo, value); }
        }
        [ModelDefault("Caption", "Tên cơ sở")]
        public CoSoGiangDay CoSoGiangDay
        {
            get { return _CoSoGiangDay; }
            set { SetPropertyValue("CoSoGiangDay", ref _CoSoGiangDay, value);
            if (!IsLoading)
                if (CoSoGiangDay != null)
                    TenCoSo = CoSoGiangDay.TenCoSo;
                else
                    TenCoSo = "";
            }
        }

        [ModelDefault("Caption", "Thời gian học")]
        public ThoiGiangHocEnum ThoiGiangHoc
        {
            get { return _ThoiGiangHoc; }
            set { SetPropertyValue("ThoiGiangHoc", ref _ThoiGiangHoc, value); }
        }
       
        [ModelDefault("Caption", "Hệ số")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [RuleRange("HeSoCoSo", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        public decimal HeSo_CoSo
        {
            get { return _HeSo_CoSo; }
            set { SetPropertyValue("HeSo_CoSo", ref _HeSo_CoSo, value); }
        }

        public HeSoCoSo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
