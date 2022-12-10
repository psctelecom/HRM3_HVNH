using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
//using System.Globalization.CultureInfo;

using System.Threading;
using System.Configuration;

namespace PSC_NormalizationData
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.tjtyjtjsds
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += (object sender, System.Threading.ThreadExceptionEventArgs e) =>
            {
                MessageBox.Show(e.Exception.Message);
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            String connectionString =  ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            Application.Run(new frmNormalizationData(connectionString));

        }

    }
}
