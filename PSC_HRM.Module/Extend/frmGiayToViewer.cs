using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraPdfViewer;

namespace PSC_HRM.Module
{
    public partial class frmGiayToViewer : XtraForm
    {
        public frmGiayToViewer(string path)
        {
            InitializeComponent();
            pdfViewer1.LoadDocument(path);
        }
    }
}