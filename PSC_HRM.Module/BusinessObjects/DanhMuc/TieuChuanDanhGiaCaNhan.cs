using System;
using System.ComponentModel;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.DanhMuc
{
    [ImageName("BO_List")]
    [DefaultProperty("TenTieuChuanDanhGiaCaNhan")]
    [ModelDefault("Caption", "Tiêu chuẩn đánh giá cá nhân")]
    public class TieuChuanDanhGiaCaNhan : BaseObject
    {
        private NhomTieuChuanDanhGiaCaNhan _NhomTieuChuanDanhGiaCaNhan;
        private int _DiemCaoNhat;
        private bool _LuuTru;
        private int _SoThuTu;
        private string _TenTieuChuanDanhGiaCaNhan;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Nhóm tiêu chuẩn đánh giá")]
        [Association("NhomTieuChuanDanhGiaCaNhan-ListTieuChuanDanhGiaCaNhan")]
        public NhomTieuChuanDanhGiaCaNhan NhomTieuChuanDanhGiaCaNhan
        {
            get
            {
                return _NhomTieuChuanDanhGiaCaNhan;
            }
            set
            {
                SetPropertyValue("NhomTieuChuanDanhGiaCaNhan", ref _NhomTieuChuanDanhGiaCaNhan, value);
                if (!IsLoading && value != null)
                {
                    int count = value.ListTieuChuanDanhGiaCaNhan.Count;
                    SoThuTu = count + 1;
                }
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
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

        [Size(500)]
        [ModelDefault("Caption", "Tên tiêu chuẩn đánh giá cá nhân")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTieuChuanDanhGiaCaNhan
        {
            get
            {
                return _TenTieuChuanDanhGiaCaNhan;
            }
            set
            {
                SetPropertyValue("TenTieuChuanDanhGiaCaNhan", ref _TenTieuChuanDanhGiaCaNhan, value);
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

        public TieuChuanDanhGiaCaNhan(Session session) : base(session) { }
    }

}
