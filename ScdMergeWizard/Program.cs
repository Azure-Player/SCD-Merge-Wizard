using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using ScdMergeWizard.ExcHandling;
using System.Diagnostics;
using ScdMergeWizard.Formz;

namespace ScdMergeWizard
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mf = new MainForm();
            if (args != null && args.Length > 0 && !string.IsNullOrEmpty(args[0]) && args[0].EndsWith(".mwpxml"))
                mf.ReadFile(args[0]);
            Application.Run(new MainForm());
        }


        public static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            try
            {
                MyExceptionHandler.NewEx(e.Exception);
            }
            finally
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                MyExceptionHandler.NewEx(ex);
            }
            finally
            {
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
