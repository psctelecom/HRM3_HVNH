//Chú ý : khi sử dụng editor này cần viết thêm lệnh gán ObjectType vào bên dưới khi user click chọn field dữ liệu
using System;


using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;
using PSC_HRM.Module.Win.CustomControllers.Editor;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(DateTime), false)]
    public class YearEditor: DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.DateEditor);

        public YearEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        { 
            ControlBindingProperty = "Value"; 
        }

        protected override object CreateControlCore()
        {
            DateEdit date = editor.Control as DateEdit;
            if (date != null)
            {
                date.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                date.Properties.VistaCalendarViewStyle = VistaCalendarViewStyle.YearView;
                date.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                date.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            }
            return editor.Control;
        }

        protected override DevExpress.XtraEditors.Repository.RepositoryItem CreateRepositoryItem()
        {
            return editor.RepositoryItem;
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
                Control.Enabled = true;
            }
        }
    }

}
