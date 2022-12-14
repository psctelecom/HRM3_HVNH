using System;
using System.ComponentModel;

using DevExpress.Xpo;
using DevExpress.Data.Filtering;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace PSC_HRM.Module.DanhMuc
{
    [ImageName("BO_Address")]
    [DefaultProperty("FullDiaChi")]
    [ModelDefault("Caption", "Địa chỉ")]

    [Appearance("QuocGiaKhac", TargetItems = "TinhThanh;QuanHuyen;XaPhuong;", Enabled = false, Criteria = "QuocGia.TenQuocGia <> 'Việt Nam'")]

    public class DiaChi : BaseObject
    {
        private QuocGia _QuocGia;
        private TinhThanh _TinhThanh;
        private QuanHuyen _QuanHuyen;
        private XaPhuong _XaPhuong;
        private string _SoNha;

        public DiaChi(Session session) : base(session) { }

        [ModelDefault("Caption", "Quốc gia")]
        [ImmediatePostData()]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
                OnChanged("FullDiaChi");
                if (!IsLoading)
                {
                    TinhThanh = null;
                    QuanHuyen = null;
                    XaPhuong = null;
                }
            }
        }

        [ModelDefault("Caption", "Tỉnh thành")]
        [DataSourceProperty("QuocGia.TinhThanhList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData()]
        public TinhThanh TinhThanh
        {
            get
            {
                return _TinhThanh;
            }
            set
            {
                SetPropertyValue("TinhThanh", ref _TinhThanh, value);
                OnChanged("FullDiaChi");
                if (!IsLoading)
                {
                    QuanHuyen = null;
                    XaPhuong = null;
                }
            }
        }

        [ModelDefault("Caption", "Quận huyện")]
        [DataSourceProperty("TinhThanh.QuanHuyenList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData()]
        public QuanHuyen QuanHuyen
        {
            get
            {
                return _QuanHuyen;
            }
            set
            {
                SetPropertyValue("QuanHuyen", ref _QuanHuyen, value);
                OnChanged("FullDiaChi");
                if (!IsLoading)
                {
                    XaPhuong = null;
                }
            }
        }

        [ModelDefault("Caption", "Xã phường")]
        [DataSourceProperty("QuanHuyen.XaPhuongList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        public XaPhuong XaPhuong
        {
            get
            {
                return _XaPhuong;
            }
            set
            {
                SetPropertyValue("XaPhuong", ref _XaPhuong, value);
                OnChanged("FullDiaChi");
            }
        }

        [ModelDefault("Caption", "Số nhà")]
        [ImmediatePostData()]
        public string SoNha
        {
            get
            {
                return _SoNha;
            }
            set
            {
                SetPropertyValue("SoNha", ref _SoNha, value);
                OnChanged("FullDiaChi");
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        [Persistent()]
        public string FullDiaChi
        {
            get
            {
                if (QuocGia != null && QuocGia.TenQuocGia != "Việt Nam")
                    return ObjectFormatter.Format("{SoNha}, {XaPhuong.TenXaPhuong}, {QuanHuyen.TenQuanHuyen}, {TinhThanh.TenTinhThanh}, {QuocGia.TenQuocGia}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
                else
                    return ObjectFormatter.Format("{SoNha}, {XaPhuong.TenXaPhuong}, {QuanHuyen.TenQuanHuyen}, {TinhThanh.TenTinhThanh}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
            }
        }

        public override void AfterConstruction()
        {
            //tìm quốc gia vn mặc định
            QuocGia vn = Session.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia=?", "Việt Nam"));
            if (vn != null)
                _QuocGia = vn;
            base.AfterConstruction();
        }

        //protected override void OnLoaded()
        //{
        //    base.OnLoaded();
        //    OnChanged("FullDiaChi");
        //}
    }

}
