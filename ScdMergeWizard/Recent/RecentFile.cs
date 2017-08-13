using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ScdMergeWizard.Recent
{
    class RecentFile
    {
        public DateTime LastDateOfUsage;
        public string FileName;

        public string ToFileString()
        {
            return LastDateOfUsage.ToString() + "\t" + FileName;
        }

        public RecentFile FromFileString(string fileString)
        {
            string[] s = Regex.Split(fileString, "\t");

            this.LastDateOfUsage = DateTime.Parse(s[0]);
            this.FileName = s[1];

            return this;
        }
    }
}
