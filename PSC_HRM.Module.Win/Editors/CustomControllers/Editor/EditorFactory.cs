using System;
using System.Collections.Generic;
using System.Linq;

namespace PSC_HRM.Module.Win.CustomControllers.Editor
{
    public class EditorFactory
    {
        public static IEditor GetEditor(EditorTypeEnum editor)
        {
            switch (editor)
            {
                case EditorTypeEnum.TextEditor:
                    return new TextEditor();
                case EditorTypeEnum.GridLookupEditor:
                    return new GridLookUpEditor();
                case EditorTypeEnum.LookupEditor:
                    return new LookUpEditor();
                case EditorTypeEnum.ComboBoxEditor:
                    return new ComboBoxEditor();
                case EditorTypeEnum.DateEditor:
                    return new DateEditor();
                case EditorTypeEnum.ImageComboBoxEditor:
                    return new ImageComboBoxEditor();
                case EditorTypeEnum.CheckedComboBoxEdit:
                    return new CheckedComboBoxEditEditor();
                default:
                    return new TextEditor();
            }
        }
    }
}
