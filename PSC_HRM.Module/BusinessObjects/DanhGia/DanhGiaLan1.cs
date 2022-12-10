using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using PSC_HRM.Module.DanhMuc;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.HoSo;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhGia
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Đánh giá lần 1")]
    public class DanhGiaLan1 : BaseObject, IBoPhan
    {
        private XepLoaiCanBo _XepLoai2;
        private XepLoaiCanBo _XepLoai1;
        private DanhGiaCanBo _DanhGiaCanBo;
        private XepLoaiCanBo _XepLoai;
        private string _NhanXet;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Đánh giá cán bộ")]
        [Association("DanhGiaCanBo-DanhSachDanhGiaLan1")]
        public DanhGiaCanBo DanhGiaCanBo
        {
            get
            {
                return _DanhGiaCanBo;
            }
            set
            {
                SetPropertyValue("DanhGiaCanBo", ref _DanhGiaCanBo, value);
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    ThongTinNhanVien = null;
                    UpdateNVList();
                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [DataSourceProperty("NVList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Phần mềm đánh giá")]
        public XepLoaiCanBo XepLoai2
        {
            get
            {
                return _XepLoai2;
            }
            set
            {
                SetPropertyValue("XepLoai2", ref _XepLoai2, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ tự đánh giá")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public XepLoaiCanBo XepLoai
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

        [ModelDefault("Caption", "Đơn vị đánh giá")]
        public XepLoaiCanBo XepLoai1
        {
            get
            {
                return _XepLoai1;
            }
            set
            {
                SetPropertyValue("XepLoai1", ref _XepLoai1, value);
            }
        }

        [ModelDefault("Caption", "Nhận xét")]
        public string NhanXet
        {
            get
            {
                return _NhanXet;
            }
            set
            {
                SetPropertyValue("NhanXet", ref _NhanXet, value);
            }
        }

        public DanhGiaLan1(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            XepLoai = Session.FindObject<XepLoaiCanBo>(CriteriaOperator.Parse("TenXepLoai=?", "A"));
            XepLoai1 = Session.FindObject<XepLoaiCanBo>(CriteriaOperator.Parse("TenXepLoai=?", "A"));
            XepLoai2 = Session.FindObject<XepLoaiCanBo>(CriteriaOperator.Parse("TenXepLoai=?", "A"));
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNVList()
        {
            if (NVList == null)
                NVList = new XPCollection<HoSo.ThongTinNhanVien>(Session);
            NVList.Criteria = HamDungChung.CriteriaGetNhanVien(BoPhan);
        }
    }

}
