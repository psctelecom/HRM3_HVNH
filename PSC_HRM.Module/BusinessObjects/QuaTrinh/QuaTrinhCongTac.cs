using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using PSC_HRM.Module.HoSo;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace PSC_HRM.Module.QuaTrinh
{
    //Ví dụ: Nguyễn Văn A, 1/1/2000, 31/12/2004, Trưởng phòng Tổ chức cán bộ
    //chỉ dùng để lưu trữ thông tin công tác của cán bộ ở tại đơn vị, những quá trình khác
    //sẽ lưu vào lịch sử bản thân
    //[Appearance("Hide_QuaTrinhCongTac_VHU", TargetItems = "TuNam;DenNam;NoiDung;QuyetDinh;SoQuyetDinh"
    //                                            , Visibility = ViewItemVisibility.Hide, Criteria = "Hide = true")]
    //[Appearance("Hide_QuaTrinhCongTac<>VHU", TargetItems = "TenCu;TenMoi"
    //                                            , Visibility = ViewItemVisibility.Hide, Criteria = "Hide = false")]
    [ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình công tác")]
    public class QuaTrinhCongTac : BaseObject
    {
        private int _STT;
        private HoSo.HoSo _HoSo;
        private string _TuNam;
        private string _DenNam;
        private string _NoiDung;
        private QuyetDinh.QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private LoaiNhanVien _TenCu;
        private LoaiNhanVien _TenMoi;
        private bool _Hide;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        public HoSo.HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
                if (!IsLoading && value != null)
                {
                    object obj = Session.Evaluate<QuaTrinhCongTac>(CriteriaOperator.Parse("COUNT()"), CriteriaOperator.Parse("HoSo=?", value));
                    if (obj != null)
                        STT = (int)obj + 1;
                    else
                        STT = 1;
                }
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh.QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    if (string.IsNullOrEmpty(SoQuyetDinh))
                        SoQuyetDinh = value.SoQuyetDinh;
                    if (NgayQuyetDinh != DateTime.MinValue)
                        NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày bắt đầu")]
        public string TuNam
        {
            get
            {
                return _TuNam;
            }
            set
            {
                SetPropertyValue("TuNam", ref _TuNam, value);
            }
        }

        [ModelDefault("Caption", "Ngày kết thúc")]
        public string DenNam
        {
            get
            {
                return _DenNam;
            }
            set
            {
                SetPropertyValue("DenNam", ref _DenNam, value);
            }
        }
        
        [Size(8000)]
        [ModelDefault("Caption", "Nội dung")]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
            }
        }

        [ModelDefault("Caption", "Tên củ")]
        public LoaiNhanVien TenCu
        {
            get
            {
                return _TenCu;
            }
            set
            {
                SetPropertyValue("TenCu", ref _TenCu, value);
            }
        }
        [ModelDefault("Caption", "Tên mới")]
        public LoaiNhanVien TenMoi
        {
            get
            {
                return _TenMoi;
            }
            set
            {
                SetPropertyValue("TenMoi", ref _TenMoi, value);
            }
        }

        [ModelDefault("Caption", "Hide")]
        [NonPersistent]
        [Browsable(false)]
        public bool Hide
        {
            get
            {
                return _Hide;
            }
            set
            {
                SetPropertyValue("Hide", ref _Hide, value);
            }
        }

        public QuaTrinhCongTac(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Module.HoSo.HoSo.CurrentHoSo != null)
                HoSo = Session.GetObjectByKey<Module.HoSo.HoSo>(Module.HoSo.HoSo.CurrentHoSo.Oid);
        }

        protected override void OnLoading()
        {
            base.OnLoading();
            if (TruongConfig.MaTruong != "VHU")
            {
                Hide = true;
            }
            else
            {
                Hide = false;
            }
        }
        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();         
        //    //if (QuyetDinh != null)
        //    //{
        //    //    SoQuyetDinh = QuyetDinh.SoQuyetDinh;
        //    //    NgayQuyetDinh = QuyetDinh.NgayQuyetDinh;
        //    //}

        //}
    }

}
