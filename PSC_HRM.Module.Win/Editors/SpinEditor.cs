//Chú ý : khi sử dụng editor này cần viết thêm lệnh gán ObjectType vào bên dưới khi user click chọn field dữ liệu
using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class SpinEditor: DXPropertyEditor
    {
        public SpinEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Text";
        }

        protected override object CreateControlCore()
        {
            SpinEdit ctrl = new SpinEdit();

            if (TruongConfig.MaTruong.Equals("UEL") || TruongConfig.MaTruong.Equals("QNU") || TruongConfig.MaTruong.Equals("NEU"))
            {
                ctrl.Properties.EditMask = "n4";
                ctrl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ctrl.Properties.DisplayFormat.FormatString = "n4";   
            }
            else
            {
                ctrl.Properties.EditMask = "n2";
                ctrl.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ctrl.Properties.DisplayFormat.FormatString = "n2";
            }
            //
            ctrl.Properties.IsFloatValue = true;

            return ctrl;
        }


        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            UpdateControlEnabled();
        }

        protected override void OnAllowEditChanged()
        {
            base.OnAllowEditChanged();
            UpdateControlEnabled();
        }

        private void UpdateControlEnabled()
        {
            if (Control != null)
            {
                if (AllowEdit)
                {
                    Control.Enabled = true;
                    Control.Properties.ReadOnly = false;
                }
                else
                {
                    Control.Enabled = false;
                    Control.Properties.ReadOnly = true;
                }
            }
        }
    }

}
