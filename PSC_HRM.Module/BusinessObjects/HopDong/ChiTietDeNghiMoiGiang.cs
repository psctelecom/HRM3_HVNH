using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module;
using PSC_HRM.Module.DanhMuc;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;

namespace PSC_HRM.Module.HopDong
{
    [ImageName("BO_HopDong")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết đề nghị mời giảng")]
    [Appearance("DeNghiMoiGiang", TargetItems = "*", Enabled = false, Criteria = "DeNghiMoiGiang.DongY")]
    public class ChiTietDeNghiMoiGiang : BaseObject
    {
        private DeNghiMoiGiang _DeNghiMoiGiang;
        private BoPhan _TaiKhoa;
        private BoPhan _BoMon;
        private string _MonHoc;
        private int _SoTiet;

        [Browsable(false)]
        [ModelDefault("Caption", "Đề nghị mời giảng")]
        [Association("DeNghiMoiGiang-ListChiTietDeNghiMoiGiang")]
        public DeNghiMoiGiang DeNghiMoiGiang
        {
            get
            {
                return _DeNghiMoiGiang;
            }
            set
            {
                SetPropertyValue("DeNghiMoiGiang", ref _DeNghiMoiGiang, value);
            }
        }

        [ModelDefault("Caption", "Tại khoa")]
        //[RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public BoPhan TaiKhoa
        {
            get
            {
                return _TaiKhoa;
            }
            set
            {
                SetPropertyValue("TaiKhoa", ref _TaiKhoa, value);
            }
        }

        [ModelDefault("Caption", "Bộ môn")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceProperty("TaiKhoa.ListBoPhanCon", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
                
                if(BoMon != null)
                    TaiKhoa = value.BoPhanCha;

                if (BoMon != null && !IsLoading)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("DeNghiMoiGiang.NhanVien = ? and BoMon = ?", this.DeNghiMoiGiang.NhanVien.Oid, BoMon.Oid);
                    XPCollection<ChiTietDeNghiMoiGiang> chiTietDeNghiMoiGiang = new XPCollection<ChiTietDeNghiMoiGiang>(Session, filter);
                    if (chiTietDeNghiMoiGiang.Count > 0)
                    {
                        DialogUtil.ShowInfo(string.Format("[{0}] bị trùng bộ môn", this.DeNghiMoiGiang.NhanVien.HoTen));
                        BoMon = null;
                    }
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Môn dạy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "PSC_HRM.Module.Win.Editors.CurriculumEditor")]
        public string MonHoc
        {
            get
            {
                return _MonHoc;
            }
            set
            {
                SetPropertyValue("MonHoc", ref _MonHoc, value);
              
            }
        }

        [ModelDefault("Caption", "Số tiết")]
        public int SoTiet
        {
            get
            {
                return _SoTiet;
            }
            set
            {
                SetPropertyValue("SoTiet", ref _SoTiet, value);
            }
        }

        public ChiTietDeNghiMoiGiang(Session session) : base(session) { }

    }
}
