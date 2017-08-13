using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScdMergeWizard.BckGrndWorker
{
    public static class MyBackgroundWorker
    {
        public static object Result;
        public static BackgroundWorker Worker = null;

        /*
        public static MyBackgroundWorker()
        {
            Worker = new BackgroundWorker();
            //Worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
        }
        */

        /*
        static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var f = new BckGrndForm();
            f.ShowDialog();
            
            throw new NotImplementedException();
        }
         * */

        static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Result = e.Result;
        }        
    }
}
