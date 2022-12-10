using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.GiayTo;
using DevExpress.ExpressApp.Model;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_ChungChi")]
    [DefaultProperty("TenChungChi")]
    [ModelDefault("Caption", "Chứng chỉ")]
    public class ChungChi : BaseObject
    {
        // Fields...
        private string _TenChungChi;
        private LoaiChungChi _LoaiChungChi;
        private HoSo _HoSo;
        private decimal _Diem;
        private XepLoaiChungChiEnum _XepLoai = XepLoaiChungChiEnum.KhongChon;
        private string _NoiCap;
        private DateTime _NgayCap;
        private GiayToHoSo _GiayToHoSo;
        private string _GhiChu;

        [Browsable(false)]
        [ImmediatePostData]
        [Association("HoSo-ListChungChi")]
        public HoSo HoSo
        {
            get
            {
               
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
                ////
                if (value != null)
                {
                    UpdateGiayToList();
                }
            }
        }

        [ModelDefault("Caption", "Loại chứng chỉ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public LoaiChungChi LoaiChungChi
        {
            get
            {
                return _LoaiChungChi;
            }
            set
            {
                SetPropertyValue("LoaiChungChi", ref _LoaiChungChi, value);
            }
        }

        [ModelDefault("Caption", "Tên chứng chỉ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public string TenChungChi
        {
            get
            {
                return _TenChungChi;
            }
            set
            {
                SetPropertyValue("TenChungChi", ref _TenChungChi, value);
            }
        }

        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public string NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }

        [ModelDefault("Caption", "Điểm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal Diem
        {
            get
            {
                return _Diem;
            }
            set
            {
                SetPropertyValue("Diem", ref _Diem, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiChungChiEnum XepLoai
        {
            get
            {
                return _XepLoai;
            }
            set
            {
                SetPropertyValue("XepLoai", ref _XepLoai, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        
        [ImmediatePostData]
        [ModelDefault("Caption", "Lưu trữ chứng chỉ")]
        [DataSourceProperty("ChungChiList", DataSourcePropertyIsNullMode.SelectAll)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
                if (_GiayToHoSo != null && _GiayToHoSo.HoSo == null && _HoSo != null )
                {
                    //Cập nhật lại hồ sơ cho giấy tờ
                    _GiayToHoSo.HoSo = _HoSo;                    
                }
                if (!IsLoading && value != null)
                    XemGiayToHoSo();
            }
        }

        public ChungChi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //InitializeGiayTo();
        }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> ChungChiList { get; set; }

        private void UpdateGiayToList()
        {
            //if (ChungChiList == null)
            //    ChungChiList = new XPCollection<GiayToHoSo>(Session);
            ChungChiList = HoSo.ListGiayToHoSo;
            //ChungChiList.Criteria = CriteriaOperator.Parse("HoSo=? and GiayTo.TenGiayTo like ?", _HoSo, "%Chứng chỉ%");
        }

        private void InitializeGiayTo()
        {
            if (GiayToHoSo == null)
                GiayToHoSo = new GiayToHoSo(Session);

            if (HoSo != null)
            {
                GiayToHoSo.HoSo = HoSo;

                DanhMuc.GiayTo giayTo = Session.FindObject<DanhMuc.GiayTo>(CriteriaOperator.Parse("TenGiayTo like ?", "%chứng chỉ%"));
                if (giayTo != null)
                {
                    GiayToHoSo.GiayTo = giayTo;
                }
            }
        }

        protected override void OnDeleting()
        {
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }

        private void XemGiayToHoSo()
        {
            using (DialogUtil.AutoWait())
            {
                try
                {
                    byte[] data = FptProvider.DownloadFile(GiayToHoSo.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                    if (data != null)
                    {
                        string strTenFile = "TempFile.pdf";
                        //Lưu file vào thư mục bin\Debug
                        HamDungChung.SaveFilePDF(data, strTenFile);
                        //Đọc file pdf
                        frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                        viewer.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }

}
