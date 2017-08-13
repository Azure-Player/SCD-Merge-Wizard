using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using System.Reflection;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Management;
using Microsoft.Win32;
using System.Globalization;
using System.Collections;
using System.Threading;

namespace ScdMergeWizard.ExcHandling
{
    public partial class ExceptionForm : Form
    {
        private string _fullErrorDesc;

        public ExceptionForm(Exception ex)
        {
            InitializeComponent();

            textBox1.Text = getFullErrDesc(ex);
            _fullErrorDesc = getFullErrDesc(ex);
        }


        /*
        private void sendMail()
        {
            // Create report
            
        }
        */

        private string getFullErrDesc(Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Assembly.GetExecutingAssembly().GetName().Name + " Error Report");
            sb.AppendLine("-----------------------------");
            sb.AppendLine("");
            sb.AppendLine("Application Version: " + Assembly.GetEntryAssembly().GetName().Version);
            sb.AppendLine("Report date: " + DateTime.Now.ToString());


            // OS
            var name = (from x in new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().OfType<ManagementObject>() select x.GetPropertyValue("Caption")).First();
            sb.AppendLine(string.Format("OS: {0} [{1}]", Environment.OSVersion, (name != null ? name.ToString() : "Unknown")));

            sb.AppendLine("Processors count: " + Environment.ProcessorCount.ToString());

            // .NET
            sb.AppendLine("Installed .NET versions:");

            RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
            string[] version_names = installed_versions.GetSubKeyNames();
            //version names start with 'v', eg, 'v3.5' which needs to be trimmed off before conversion

            foreach (string versionName in version_names)
            {
                //double Framework = Convert.ToDouble(version_names[version_names.Length - 1].Remove(0, 1), CultureInfo.InvariantCulture);
                int SP = Convert.ToInt32(installed_versions.OpenSubKey(versionName).GetValue("SP", 0));

                sb.AppendLine(string.Format("  -> {0} SP{1}", versionName, SP));
            }
            sb.AppendLine("");

            int exCount = 1;
            AppendEx(ex, sb, exCount);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                exCount++;
                AppendEx(ex, sb, exCount);
            }

            return sb.ToString();
        }

        private void AppendEx(Exception ex, StringBuilder sb, int exNo = 1)
        {
            sb.AppendLine(string.Format("========== MESSAGE: {0} ==========", exNo));
            sb.AppendLine("");
            sb.AppendLine("Message:");
            sb.AppendLine(ex.Message);

            sb.AppendLine("");
            sb.AppendLine("Source:");
            sb.AppendLine(ex.Source);

            if (ex.HelpLink != null)
            {
                sb.AppendLine("");
                sb.AppendLine("Help link:");
                sb.AppendLine(ex.HelpLink);
            }

            sb.AppendLine("");
            sb.AppendLine("Stack Trace:");
            sb.AppendLine(ex.StackTrace);

            if (ex.Data != null && ex.Data.Count > 0)
            {
                sb.AppendLine("");
                sb.AppendLine("Extra details:");
                foreach (DictionaryEntry d in ex.Data)
                {
                    sb.AppendLine(string.Format("Key '{0}' has value '{1}'", d.Key, d.Value));
                }
            }
        }

        private void ExceptionForm_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText(_fullErrorDesc);
            System.Diagnostics.Process.Start(@"https://github.com/SQLPlayer/SCD-Merge-Wizard/issues");

            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabelSendEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string to = "kamil@nowinski.net";
                string subject = "Exception Occured at " + Assembly.GetExecutingAssembly().GetName().Name;
                string command = string.Format("mailto:{0}?subject={1}&body={2}", to, subject, _fullErrorDesc.Replace(Environment.NewLine, "%0D"));
                Process.Start(command);

                this.Close();
            }
            catch
            {
                //mailSendError = "Exception caught in CreateMessageWithAttachment(): " + ex.ToString();
            }
        }
    }
}
