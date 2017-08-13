using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScdMergeWizard.ExcHandling
{
    public static class MyExceptionHandler
    {
        public static void NewEx(Exception ex)
        {
            new ExceptionForm(ex).ShowDialog();
        }
    }
}
