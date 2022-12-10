using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using PSC_HRM.Module.DanhMuc;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.GiayTo;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace PSC_HRM.Module.HoSo
{
    [ImageName("BO_NgoaiNguKhac")]
    [DefaultProperty("NgoaiNgu")]
    [ModelDefault("Caption", "Trình độ ngoại ngữ")]
    public class TrinhDoNgoaiNguKhac : BaseObject
    {
        private GiayToHoSo _GiayToHoSo;
        private decimal _Diem;
        private HoSo _HoSo;
        private NgoaiNgu _NgoaiNgu;
        private TrinhDoNgoaiNgu _TrinhDoNgoaiNgu;
        private XepLoaiChungChiEnum _XepLoai = XepLoaiChungChiEnum.KhongChon;
        private string _NoiCap;
        private DateTime _NgayCap;
        private string _GhiChu;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("HoSo-ListNgoaiNgu")]
        public HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
                if (value != null)
                {
                    UpdateGiayToList();
                }
            }
        }

        [ModelDefault("Caption", "Ngoại ngữ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NgoaiNgu NgoaiNgu
        {
            get
            {
                return _NgoaiNgu;
            }
            set
            {
                SetPropertyValue("NgoaiNgu", ref _NgoaiNgu, value);
            }
        }

        [ModelDefault("Caption", "Trình độ ngoại ngữ")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public TrinhDoNgoaiNgu TrinhDoNgoaiNgu
        {
            get
            {
                return _TrinhDoNgoaiNgu;
            }
            set
            {
                SetPropertyValue("TrinhDoNgoaiNgu", ref _TrinhDoNgoaiNgu, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiChungChiEnum XepLoai
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

        [ModelDefault("Caption", "Điểm")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal Diem
        {
            get
            {
                return _Diem;
            }
            set
            {
                SetPropertyValue("Diem", ref _Diem, value);
            }
        }

        [ModelDefault("Caption", "Giấy tờ")]
        [DataSourceProperty("GiayToList", DataSourcePropertyIsNullMode.SelectAll)]
        //[ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public GiayToHoSo GiayToHoSo
        {
            get
            {
                return _GiayToHoSo;
            }
            set
            {
                SetPropertyValue("GiayToHoSo", ref _GiayToHoSo, value);
                if (_GiayToHoSo != null && _GiayToHoSo.HoSo == null && _HoSo != null)
                {
                    //Cập nhật lại hồ sơ cho giấy tờ
                    _GiayToHoSo.HoSo = _HoSo;
                }
                if (!IsLoading && value != null)
                    XemGiayToHoSo();
            }
        }

        [ModelDefault("Caption", "Nơi cấp")]
        public string NoiCap
        {
            get
            {
                return _NoiCap;
            }
            set
            {
                SetPropertyValue("NoiCap", ref _NoiCap, value);
            }
        }


        [ModelDefault("Caption", "Ngày cấp")]
        public DateTime NgayCap
        {
            get
            {
                return _NgayCap;
            }
            set
            {
                SetPropertyValue("NgayCap", ref _NgayCap, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public TrinhDoNgoaiNguKhac(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<GiayToHoSo> GiayToList { get; set; }

        private void UpdateGiayToList()
        {
            if (GiayToList == null)
                GiayToList = new XPCollection<GiayToHoSo>(Session);

            GiayToList.Criteria = CriteriaOperator.Parse("HoSo=?", HoSo);
        }

        protected override void OnDeleting()
        {
            TrinhDoNgoaiNguKhac trinhDogoaiNguKhac = Session.FindObject<TrinhDoNgoaiNguKhac>(CriteriaOperator.Parse("Oid=?", this.Oid));
            if (trinhDogoaiNguKhac != null)
            {
                NhanVien nhanVien = Session.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=?", trinhDogoaiNguKhac.HoSo.Oid));

                //Nếu ngoại ngữ vừa xóa là ngoại ngữ chính thì cập nhật lại ngoại ngữ chinh trong nhân viên trình độ
                if (trinhDogoaiNguKhac.NgoaiNgu != null && trinhDogoaiNguKhac.TrinhDoNgoaiNgu != null)
                {
                    if (nhanVien.NhanVienTrinhDo != null
                        && nhanVien.NhanVienTrinhDo.NgoaiNgu != null
                        && nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu != null
                        && nhanVien.NhanVienTrinhDo.NgoaiNgu.Oid == trinhDogoaiNguKhac.NgoaiNgu.Oid
                        && nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu.Oid == trinhDogoaiNguKhac.TrinhDoNgoaiNgu.Oid)
                    {
                        nhanVien.NhanVienTrinhDo.NgoaiNgu = null;
                        nhanVien.NhanVienTrinhDo.TrinhDoNgoaiNgu = null;
                    }
                }
            }

            //Xóa giấy tờ hồ sơ
            if (GiayToHoSo != null)
            {
                Session.Delete(GiayToHoSo);
                Session.Save(GiayToHoSo);
            }

            base.OnDeleting();
        }

        private void XemGiayToHoSo()
        {
            using (DialogUtil.AutoWait())
            {
                try
                {
                    byte[] data = FptProvider.DownloadFile(GiayToHoSo.DuongDanFile, HamDungChung.CauHinhChung.Username, HamDungChung.CauHinhChung.Password);
                    if (data != null)
                    {
                        string strTenFile = "TempFile.pdf";
                        //Lưu file vào thư mục bin\Debug
                        HamDungChung.SaveFilePDF(data, strTenFile);
                        //Đọc file pdf
                        frmGiayToViewer viewer = new frmGiayToViewer("TempFile.pdf");
                        viewer.ShowDialog();
                    }
                    else
                        XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch
                {
                    XtraMessageBox.Show("Giấy tờ hồ sơ không tồn tại trên máy chủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }

}
