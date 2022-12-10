using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenHocHam")]
    [ModelDefault("Caption", "Học hàm")]
    public class HocHam : TruongBaseObject
    {
        private string _MaQuanLy;
        private string _TenHocHam;
        private decimal _CapDo;
        private string _MaQuanLy_UIS;
        private string _MaQuanLyEng;
        private string _TenHocHamEng;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý (UIS)")]
        //[ModelDefault("DisplayFormat", "##")]
        //[ModelDefault("EditMask", "##")]        
        [ModelDefault("AllowEdit", "False")]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public string MaQuanLy_UIS
        {
            get
            {
                return _MaQuanLy_UIS;
            }
            set
            {
                SetPropertyValue("MaQuanLy_UIS", ref _MaQuanLy_UIS, value);
            }
        }

        [ModelDefault("Caption", "Tên học hàm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHocHam
        {
            get
            {
                return _TenHocHam;
            }
            set
            {
                SetPropertyValue("TenHocHam", ref _TenHocHam, value);
            }
        }

        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }

        [ModelDefault("Caption", "Mã quản lý (English)")]        
        public string MaQuanLyEng
        {
            get
            {
                return _MaQuanLyEng;
            }
            set
            {
                SetPropertyValue("MaQuanLyEng", ref _MaQuanLyEng, value);
            }
        }

        [ModelDefault("Caption", "Tên học hàm (English)")]
        public string TenHocHamEng
        {
            get
            {
                return _TenHocHamEng;
            }
            set
            {
                SetPropertyValue("TenHocHamEng", ref _TenHocHamEng, value);
            }
        }

        public HocHam(Session session) : base(session) { }

        protected override void OnSaving()
        {
            base.OnSaving();
            //if (!IsDeleted && string.IsNullOrWhiteSpace(MaQuanLy_UIS))
            //    LayMaQuanLyUIS();
        }

        protected void LayMaQuanLyUIS()
        {
            try
            {
                string sql = "SELECT TOP 1 ISNULL(MaQuanLy_UIS,0)"
                            + " FROM HocHam"
                            + " ORDER BY CONVERT(INT,MaQuanLy_UIS) DESC";
                object kq = null;
                kq = DataProvider.GetValueFromDatabase(sql, CommandType.Text);
                int ma = Convert.ToInt32(kq) + 1;
                if (ma < 10)
                {
                    MaQuanLy_UIS = string.Concat("0", (Convert.ToInt32(kq) + 1).ToString());
                }
                else
                    MaQuanLy_UIS = ma.ToString();
            }
            catch (Exception)
            {
                XtraMessageBox.Show("Mã quản lý UIS của danh mục Học hàm không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }

}
