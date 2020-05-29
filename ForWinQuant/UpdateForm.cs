using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForWinQuant
{
    public partial class UpdateForm : Form
    {
        private string filename;
        public UpdateForm(string url,string path)
        {

            filename = path;
            InitializeComponent();
            this.notifyIcon1.BalloonTipClosed += (sender, e) => {
                var thisIcon = (NotifyIcon)sender;
                thisIcon.Visible = false;
                thisIcon.Dispose();
            };
            download(url, path);
        }

        private void download(string url, string path)
        {
            WebClient client = new WebClient();
            client.DownloadFileCompleted += Client_DownloadFileCompleted; ;
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileAsync(new Uri(url), path, path.Substring(path.LastIndexOf("/") + 1));
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled && e.UserState != null)
            {
                Task.Factory.StartNew(() => {
                    Process p = new Process();
                    p.StartInfo.FileName = e.UserState.ToString();
                    try
                    {
                        p.Start();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误");
                    }
                    finally
                    {
                        Environment.Exit(0);
                    }
                    
                    
                });

            }
            else
            {
                MessageBox.Show(Properties.Resources.DownLoadFail, Properties.Resources.NewVersion, MessageBoxButtons.OK);
                Environment.Exit(0);
            }
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBarDownloading.Minimum = 0;
            this.progressBarDownloading.Maximum = (int)e.TotalBytesToReceive;
            this.progressBarDownloading.Value = (int)e.BytesReceived;
            this.labelProgress.Text = e.ProgressPercentage + "%";
        }

    }
}
