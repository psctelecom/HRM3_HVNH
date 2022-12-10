using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using DevExpress.XtraEditors;

namespace PSC_HRM.Module.Win.Forms
{
    public partial class frmHelper : XtraForm
    {
        public frmHelper()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Xu lý trợ giúp
        /// </summary>
        /// <param name="type"></param>
        /// <param name="caption"></param>
        public void XuLy(Type type, string caption)
        {
            int index = type.FullName.LastIndexOf('.') + 1;
            string fileName = type.FullName.Substring(index);

            if (!String.IsNullOrEmpty(fileName))
            {
                Text = "Hướng dẫn sử dụng: " + caption;
                fileName = String.Format(@"{0}\Help\{1}.mht", Application.StartupPath, fileName);

                FileInfo file = new FileInfo(fileName);
                if (file.Exists)
                {
                    webBrowser1.Url = new Uri(fileName);

                    //hiện cửa sổ trợ giúp lên
                    Show();
                }
            }
        }

        /// <summary>
        /// Xử lý trợ giúp
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="caption"></param>
        public void XuLy(string fileName, string caption)
        {
            if (!String.IsNullOrEmpty(fileName))
            {
                Text = "Hướng dẫn sử dụng: " + caption;
                fileName = String.Format(@"{0}\Help\{1}.mht", Application.StartupPath, fileName);

                FileInfo file = new FileInfo(fileName);
                if (file.Exists)
                {
                    webBrowser1.Url = new Uri(fileName);

                    //hiện cửa sổ trợ giúp lên
                    Show();
                }
            }
        }
    }
}