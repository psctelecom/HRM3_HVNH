using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraSplashScreen;
using System.Windows.Forms;

namespace PSC_HRM.Module
{
    public class AutoWaitForm : IDisposable
    {
        String _caption;

        public String Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                _manager.SetWaitFormCaption(_caption);
            }
        }
        String _description;

        public String Description
        {
            get { return _description; }
            set
            {
                _description = value;
                _manager.SetWaitFormDescription(_description);
            }
        }
        private bool _disposed = false;
        SplashScreenManager _manager = null;

        public AutoWaitForm(String description, String caption)
        {

            _manager = new SplashScreenManager(typeof(WaitForm1),new SplashFormProperties());

            _manager.ShowWaitForm();

            Caption = caption;
            Description = description;

        }

        ~AutoWaitForm()
        {
            Dispose();
        }
        public void Dispose()
        {
            if (!_disposed)
            {

                _manager.CloseWaitForm();
                //_manager.WaitForSplashFormClose();
                _manager.Dispose();
                _disposed = true;
                //GC.ReRegisterForFinalize(this);
            }
        }

    }
}
