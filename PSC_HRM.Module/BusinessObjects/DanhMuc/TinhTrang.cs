using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTinhTrang")]
    [ModelDefault("Caption", "Tình trạng")]
    public class TinhTrang : TruongBaseObject
    {
        private bool _KhongConCongTacTaiTruong;
        private string _MaQuanLy;
        private string _TenTinhTrang;
        private bool _KhongTinhTNTT;
        private string _MaQuanLy_UIS;
        private Trong_NgoaiNuocEnum _LoaiTinhTrang;
        private decimal _PhanTramHuongLuong;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField("",DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTinhTrang
        {
            get
            {
                return _TenTinhTrang;
            }
            set
            {
                SetPropertyValue("TenTinhTrang", ref _TenTinhTrang, value);
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

        [ModelDefault("Caption", "Không còn công tác tại trường")]
        public bool KhongConCongTacTaiTruong
        {
            get
            {
                return _KhongConCongTacTaiTruong;
            }
            set
            {
                SetPropertyValue("KhongConCongTacTaiTruong", ref _KhongConCongTacTaiTruong, value);
            }
        }

        [ModelDefault("Caption", "Không tính TNTT")]
        public bool KhongTinhTNTT
        {
            get
            {
                return _KhongTinhTNTT;
            }
            set
            {
                SetPropertyValue("KhongTinhTNTT", ref _KhongTinhTNTT, value);
            }
        }

        [ModelDefault("Caption", "Loại tình trạng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public Trong_NgoaiNuocEnum LoaiTinhTrang
        {
            get
            {
                return _LoaiTinhTrang;
            }
            set
            {
                SetPropertyValue("LoaiTinhTrang", ref _LoaiTinhTrang, value);
            }
        }

        [ModelDefault("Caption", "% Hưởng lương")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal PhanTramHuongLuong
        {
            get
            {
                return _PhanTramHuongLuong;
            }
            set
            {
                SetPropertyValue("PhanTramHuongLuong", ref _PhanTramHuongLuong, value);
            }
        }

        public TinhTrang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            LoaiTinhTrang = Trong_NgoaiNuocEnum.CaHai;
            //LayMaQuanLyUIS();
        }

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
                            + " FROM TinhTrang"
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
                XtraMessageBox.Show("Mã quản lý UIS của danh mục Tình trạng không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
        }
    }

}
