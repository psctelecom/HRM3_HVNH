//Chú ý : khi sử dụng editor này cần viết thêm lệnh gán ObjectType vào bên dưới khi user click chọn field dữ liệu
using System;


using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.ExpressApp.Model;

namespace PSC_HRM.Module.Win.Editors
{
    [PropertyEditor(typeof(DateTime), true)]
    public class CustomDateEditor: DatePropertyEditor
    {
        public CustomDateEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            
        }

        protected override void SetupRepositoryItem(DevExpress.XtraEditors.Repository.RepositoryItem item)
        {
            base.SetupRepositoryItem(item);
            ((RepositoryItemDateTimeEdit)item).Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            ((RepositoryItemDateTimeEdit)item).AllowMouseWheel = false;
        }
    }

}
