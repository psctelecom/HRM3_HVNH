using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit.Services.Implementation;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using PSC_HRM.Module.MailMerge;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Utils;

namespace PSC_HRM.Module.Win.Common
{
    public class CustomCommandFactoryService : IRichEditCommandFactoryService
    {
        private readonly IRichEditCommandFactoryService service;
        private readonly RichEditControl control;
        public MailMergeTemplate MailMerge { get; set; }
        public XPObjectSpace ObjectSpace { get; set; }

        public CustomCommandFactoryService(RichEditControl control, 
            IRichEditCommandFactoryService service)
        {
            Guard.ArgumentNotNull(control, "control");
            Guard.ArgumentNotNull(service, "service");
            this.control = control;
            this.service = service;
        }

        public RichEditCommand CreateCommand(RichEditCommandId commandId)
        {
            if (commandId == RichEditCommandId.FileSave)
            {
                CustomSaveDocumentCommand cmd = new CustomSaveDocumentCommand(control);
                cmd.MailMerge = MailMerge;
                cmd.ObjectSpace = ObjectSpace;

                return cmd;
            }
            else
                return service.CreateCommand(commandId);
        }
    }
}
