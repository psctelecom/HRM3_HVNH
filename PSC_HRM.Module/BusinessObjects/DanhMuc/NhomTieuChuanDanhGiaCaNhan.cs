using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNhomTieuChiDanhGiaCaNhan")]
    [ModelDefault("Caption", "Nhóm tiêu chuẩn đánh giá cá nhân")]
    public class NhomTieuChuanDanhGiaCaNhan : BaseObject
    {
        private int _DiemCaoNhat;
        private DoiTuongDanhGia _DoiTuongDanhGia;
        private bool _LuuTru;
        private int _SoThuTu;
        private string _TenNhomTieuChuanDanhGiaCaNhan;

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
            }
        }

        [ModelDefault("Caption", "Tên nhóm tiêu chuẩn đánh giá")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomTieuChuanDanhGiaCaNhan
        {
            get
            {
                return _TenNhomTieuChuanDanhGiaCaNhan;
            }
            set
            {
                SetPropertyValue("TenNhomTieuChuanDanhGiaCaNhan", ref _TenNhomTieuChuanDanhGiaCaNhan, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đối tượng đánh giá")]
        public DoiTuongDanhGia DoiTuongDanhGia
        {
            get
            {
                return _DoiTuongDanhGia;
            }
            set
            {
                SetPropertyValue("DoiTuongDanhGia", ref _DoiTuongDanhGia, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Điểm cao nhất")]
        public int DiemCaoNhat
        {
            get
            {
                return _DiemCaoNhat;
            }
            set
            {
                SetPropertyValue("DiemCaoNhat", ref _DiemCaoNhat, value);
            }
        }

        // lữu trữ mỗi lần đánh giá
        [Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        public bool LuuTru
        {
            get
            {
                return _LuuTru;
            }
            set
            {
                SetPropertyValue("LuuTru", ref _LuuTru, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách tiêu chí đánh giá")]
        [Association("NhomTieuChuanDanhGiaCaNhan-ListTieuChuanDanhGiaCaNhan")]
        public XPCollection<TieuChuanDanhGiaCaNhan> ListTieuChuanDanhGiaCaNhan
        {
            get
            {
                return GetCollection<TieuChuanDanhGiaCaNhan>("ListTieuChuanDanhGiaCaNhan");
            }
        }

        public NhomTieuChuanDanhGiaCaNhan(Session session) : base(session) { }
    }

}
