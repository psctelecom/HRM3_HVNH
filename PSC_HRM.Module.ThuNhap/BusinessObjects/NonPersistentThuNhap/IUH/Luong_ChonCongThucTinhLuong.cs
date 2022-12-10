using System;

using DevExpress.Xpo;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.BaoMat;
using PSC_HRM.Module.ThuNhap.Luong;

namespace PSC_HRM.Module.ThuNhap.NonPersistentThuNhap
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn công thức tính lương")]
    public class Luong_ChonCongThucTinhLuong : BaseObject
    {
        // Fields...
        private bool _ChonTatCa;

        [ImmediatePostData]
        [ModelDefault("Caption", "Chọn tất cả")]
        [ModelDefault("AllowEdit", "True")]
        public bool ChonTatCa
        {
            get
            {
                return _ChonTatCa;
            }
            set
            {
                SetPropertyValue("ChonTatCa", ref _ChonTatCa, value);
                {
                    if (!IsLoading)
                    {
                        //Check tất cả các công thức
                        CheckAllCongThucTinhLuong();
                    }
                }
            }
        }

        [ModelDefault("Caption", "Danh sách công thức tính lương")]
        [ModelDefault("AllowEdit", "True")]
        public XPCollection<Luong_ChonCongThucTinhLuongItem> ListLuong_ChonCongThucTinhLuong { get; set; }

        public Luong_ChonCongThucTinhLuong(Session session) : base(session) { }


        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //
            ListLuong_ChonCongThucTinhLuong = new XPCollection<Luong_ChonCongThucTinhLuongItem>(Session, false);
        }

        private void CheckAllCongThucTinhLuong()
        {
            foreach (Luong_ChonCongThucTinhLuongItem item in ListLuong_ChonCongThucTinhLuong)
            {
                if (item.Chon != ChonTatCa)
                    item.Chon = ChonTatCa;
            }
        }
    }

}
