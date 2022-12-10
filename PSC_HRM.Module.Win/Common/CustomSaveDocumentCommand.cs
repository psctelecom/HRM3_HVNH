using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit;
using System.IO;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.MailMerge;

namespace PSC_HRM.Module.Win.Common
{
    public class CustomSaveDocumentCommand : SaveDocumentCommand
    {
        public MailMergeTemplate MailMerge { get; set; }
        public XPObjectSpace ObjectSpace { get; set; }

        public CustomSaveDocumentCommand(IRichEditControl control)
            : base(control)
        { }

        public override void Execute()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Control.Document.SaveDocument(stream, DocumentFormat.Rtf);
                MailMerge.LuuTru = stream.ToArray();
                
                //Control.Document. = false;

                ObjectSpace.CommitChanges();
            }
        }
    }
}
