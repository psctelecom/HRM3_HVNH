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
    [DefaultProperty("TenTieuChuanDanhGiaTapThe")]
    [ModelDefault("Caption", "Tiêu chuẩn đánh giá tập thể")]
    public class TieuChuanDanhGiaTapThe : BaseObject
    {
        private NhomTieuChuanDanhGiaTapThe _NhomTieuChuanDanhGiaTapThe;
        private int _DiemChuan;
        private bool _LuuTru;
        private int _SoThuTu;
        private string _TenTieuChuanDanhGiaTapThe;


        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Nhóm tiêu chuẩn đánh giá")]
        [Association("NhomTieuChuanDanhGiaTapThe-ListTieuChuanDanhGiaTapThe")]
        public NhomTieuChuanDanhGiaTapThe NhomTieuChuanDanhGiaTapThe
        {
            get
            {
                return _NhomTieuChuanDanhGiaTapThe;
            }
            set
            {
                SetPropertyValue("NhomTieuChuanDanhGiaTapThe", ref _NhomTieuChuanDanhGiaTapThe, value);
                if (!IsLoading && value != null)
                {
                    int count = value.ListTieuChuanDanhGiaTapThe.Count;
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
        [ModelDefault("Caption", "Tên tiêu chuẩn đánh giá tập thể")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTieuChuanDanhGiaTapThe
        {
            get
            {
                return _TenTieuChuanDanhGiaTapThe;
            }
            set
            {
                SetPropertyValue("TenTieuChuanDanhGiaTapThe", ref _TenTieuChuanDanhGiaTapThe, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Điểm chuẩn")]
        public int DiemChuan
        {
            get
            {
                return _DiemChuan;
            }
            set
            {
                SetPropertyValue("DiemChuan", ref _DiemChuan, value);
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

        public TieuChuanDanhGiaTapThe(Session session) : base(session) { }
    }

}
