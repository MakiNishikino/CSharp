using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

//Powered by Powered by WMIC (Windows Management Instrumentation Command-line) and perfmon.msc
//.NET Framework 2.0 for Windows 7 without .NET Framework 4
//20180723
//Kazakiri Hikaru

namespace CSharp2WMIC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //partnersupport.microsoft.com/zh-hans/par_servplat/forum/par_sysserv/%E5%A6%82%E4%BD%95%E5%8F%AA%E6%98%BE%E7%A4%BAwmic/8c53fac9-734d-42e8-87c3-e60710b2e34f
            string ARG = "wmic cpu get Name /value";
            Process PROCESSCMD = new Process();
            PROCESSCMD.StartInfo.FileName = "cmd.exe";
            PROCESSCMD.StartInfo.Arguments = "/C " + ARG;
            PROCESSCMD.StartInfo.UseShellExecute = false;
            PROCESSCMD.StartInfo.CreateNoWindow = true;
            PROCESSCMD.StartInfo.RedirectStandardOutput = true;
            PROCESSCMD.Start();
            Text = PROCESSCMD.StandardOutput.ReadToEnd().ToString().Substring(11).Trim();
            PROCESSCMD.WaitForExit();
            PROCESSCMD.Close();
            PROCESSCMD.Dispose();

            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Powered by perfmon.msc
            PerformanceCounter PC = new PerformanceCounter("Processor", "% Idle Time", "_Total");
            while (true)
            {

                MethodInvoker M = new MethodInvoker(() =>
                {
                    progressBar1.Value = Convert.ToInt32(100 - PC.NextValue());
                    label1.Text = progressBar1.Value.ToString();
                });
                BeginInvoke(M);
                Thread.Sleep(1000);
            }
            //{
            //    //Powered by Windows Management Instrumentation Command-line
            //    string ARG2 = "wmic cpu get LoadPercentage /value";
            //    Process PROCESSCMD2 = new Process();
            //    PROCESSCMD2.StartInfo.FileName = "cmd.exe";
            //    PROCESSCMD2.StartInfo.Arguments = "/C " + ARG2;
            //    PROCESSCMD2.StartInfo.UseShellExecute = false;
            //    PROCESSCMD2.StartInfo.CreateNoWindow = true;
            //    PROCESSCMD2.StartInfo.RedirectStandardOutput = true;
            //    while (true)
            //    {

            //        PROCESSCMD2.Start();
            //        MethodInvoker M = new MethodInvoker(() =>
            //        {
            //            progressBar1.Value = Convert.ToInt16(PROCESSCMD2.StandardOutput.ReadToEnd().ToString().Substring(21, 3).Trim());
            //            label1.Text = progressBar1.Value.ToString();
            //        });
            //        BeginInvoke(M);
            //        PROCESSCMD2.WaitForExit();
            //        PROCESSCMD2.Close();
            //        PROCESSCMD2.Dispose();
            //        Thread.Sleep(1000);
            //    }
            //}
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            if (TopMost == false)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Powered by WMIC (Windows Management Instrumentation Command-line) and perfmon.msc",Environment.MachineName + " - " + Environment.OSVersion,MessageBoxButtons.OK);
        }
    }
}