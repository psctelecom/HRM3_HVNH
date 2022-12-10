using System;
using System.Collections.Generic;

using DevExpress.ExpressApp;
using PSC_HRM.Module.HoSo;

namespace PSC_HRM.Module.Controllers
{
    public partial class ChangeCaptionThongTinNhanVienController : ViewController
    {
        public ChangeCaptionThongTinNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        void ChangeCaptionThongTinNhanVienController_Activated(object sender, EventArgs e)
        {
            if (View.Id.Contains("ThongTinNhanVien") && TruongConfig.MaTruong == "VHU")
            { 
                //Thay đổi caption of property
                ChangePropertyCaption("GhiChu", "Nơi làm việc");
            }
            DetailView detail = View as DetailView;
            ThongTinNhanVien nhanVien = View.CurrentObject as ThongTinNhanVien;
            if (detail != null && nhanVien != null)
            {
                if (nhanVien.BoPhan != null)
                    detail.Caption = String.Format("{0} - {1}", nhanVien.BoPhan.TenBoPhan, detail.Caption);               
            }
        }

        void ChangePropertyCaption(string propertyName, string propertyCaption)
        {
            /* Lưu ý chỉ có tác dụng đổi với Detailview */

            //
            var property = View.Model.AsObjectView.ModelClass.FindMember(propertyName);
            if (property != null)
            {
                property.Caption = propertyCaption;
            }
        }
    }
}
