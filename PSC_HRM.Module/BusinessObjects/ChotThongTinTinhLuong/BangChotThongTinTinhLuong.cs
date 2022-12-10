using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using PSC_HRM.Module;
using System.Data.SqlClient;
using System.Data;

namespace PSC_HRM.Module.ChotThongTinTinhLuong
{
    [DefaultClassOptions]
    [ImageName("BO_TroCap")]
    [DefaultProperty("Caption")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Bảng chốt thông tin tính lương")]
    [Appearance("BangChotThongTinTinhLuong.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]
    public class BangChotThongTinTinhLuong : TruongBaseObject,IThongTinTruong
    {
        // Fields...
        private bool _KhoaSo;
        private DateTime _Thang;
        private ThongTinTruong _ThongTinTruong;
        private bool _DaCapNhatThamNienCuaThang;
        private LoaiLuongEnum _LoaiLuong;
        private int _Nam;

        [ModelDefault("Caption", "Tháng")]
        [ModelDefault("EditMask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return "Tháng " + Thang.ToString("MM/yyyy");
            }
        }

        [ModelDefault("Caption", "Đã cập nhật thâm niên")]
        [Browsable(false)]
        public bool DaCapNhatThamNienCuaThang
        {
            get
            {
                return _DaCapNhatThamNienCuaThang;
            }
            set
            {
                SetPropertyValue("DaCapNhatThamNienCuaThang", ref _DaCapNhatThamNienCuaThang, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại lương")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "MaTruong = 'QNU'")]
        public LoaiLuongEnum LoaiLuong
        {
            get
            {
                return _LoaiLuong;
            }
            set
            {
                SetPropertyValue("LoaiLuong", ref _LoaiLuong, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tính lương")]
        [Association("BangChotThongTinTinhLuong-ListThongTinTinhLuong")]
        public XPCollection<ThongTinTinhLuong> ListThongTinTinhLuong
        {
            get
            {
                return GetCollection<ThongTinTinhLuong>("ListThongTinTinhLuong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách truy lương")]
        [Association("BangChotThongTinTinhLuong-ListThongTinTinhTruyLinh")]
        [Browsable(false)]
        public XPCollection<ThongTinTinhTruyLinh> ListThongTinTinhTruyLinh
        {
            get
            {
                return GetCollection<ThongTinTinhTruyLinh>("ListThongTinTinhTruyLinh");
            }
        }

        public BangChotThongTinTinhLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            Thang = HamDungChung.GetServerTime();
           
            ThongTinTruong = HamDungChung.ThongTinTruong(Session);
            LoaiLuong = 0;
        }

        [Browsable(false)]
        public ThongTinTruong ThongTinTruong
        {
            get
            {
                return _ThongTinTruong;
            }
            set
            {
                SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value);
            }
        }
        
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int Nam
        {
            get
            {
                return Thang.Year;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
            }
        }
    }

}
