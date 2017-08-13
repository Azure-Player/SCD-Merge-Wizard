using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScdMergeWizard.Recent
{
    class RecentFilesManager
    {
        private List<RecentFile> _recentFiles = new List<RecentFile>();
        private static string recentFilesFile = Path.GetTempPath() + "SCD Merge Wizard\\Recent.txt";
        private const int MAX_RECENT_FILES_COUNT = 10;

        public static string[] Read()
        {
            List<RecentFile> recentFilesList = new List<RecentFile>();
            if (File.Exists(recentFilesFile))
            {
                foreach (string rfs in File.ReadAllLines(recentFilesFile))
                {
                    recentFilesList.Add(new RecentFile().FromFileString(rfs));
                }
            }
            recentFilesList.Sort(delegate(RecentFile f1, RecentFile f2) { return (f1.LastDateOfUsage > f2.LastDateOfUsage) ? 1 : 0; });

            return recentFilesList.Select(rf => rf.FileName).ToArray();
            //return Directory.GetFiles(RecentFilesPath).Select(rf => Path.GetFileNameWithoutExtension(rf)).ToArray();

            //return null;
        }

        public static void Save()
        {

        }

        public static void Clear()
        {
            File.Delete(recentFilesFile);
        }

        public static void Add(string fileName)
        {
            List<RecentFile> recentFilesList = new List<RecentFile>();

            if (!Directory.Exists(Path.GetDirectoryName(recentFilesFile)))
                Directory.CreateDirectory(Path.GetDirectoryName(recentFilesFile));

            if (File.Exists(recentFilesFile))
            {
                foreach (string rfs in File.ReadAllLines(recentFilesFile))
                {
                    RecentFile rf = new RecentFile().FromFileString(rfs);
                    if (!rf.FileName.Equals(fileName))
                        recentFilesList.Add(rf);
                }
            }

            recentFilesList.Add(new RecentFile { FileName = fileName, LastDateOfUsage = DateTime.Now });

            recentFilesList.Sort(delegate(RecentFile f1, RecentFile f2) { return (f1.LastDateOfUsage > f2.LastDateOfUsage) ? 1 : 0; });

            File.WriteAllLines(recentFilesFile, recentFilesList.Select(r => r.ToFileString()).Take(MAX_RECENT_FILES_COUNT).ToArray());
        }
    }
}
