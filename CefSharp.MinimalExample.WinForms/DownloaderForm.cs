using CefSharp.MinimalExample.WinForms.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class DownloaderForm : Form
    {
        public DownloaderForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        public List<WorkOutput> douyinOutputs;


        private void DownloaderForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = douyinOutputs;

        }

        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = true;
                //row.Cells["Status"].Value = "已选中";
            }
        }

        private async void 下载DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 同时下载多个对象，最多允许4个并发下载
            string saveDirectory = Path.Combine(Application.StartupPath, "Downloads");
            SemaphoreSlim semaphore = new SemaphoreSlim(4); // 设置允许的最大并发数
            var downloadTasks = new List<Task>();
            var dataGridView1SelectedRows = dataGridView1.SelectedRows.Cast<DataGridViewRow>().ToList();
            string savePath = "";
            foreach (DataGridViewRow row in dataGridView1SelectedRows)
            {
                if (row.Cells["Status"].Value != null && row.Cells["Status"].Value.ToString().Contains("Complete")) continue;
                await semaphore.WaitAsync(); // 等待信号量，直到有空闲的下载槽
                savePath = Path.Combine(saveDirectory, row.Cells["NickName"].Value.ToString());
                Directory.CreateDirectory(savePath); // 创建保存路径

                // 开始下载任务，并在完成后释放信号量
                downloadTasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        await DownloadWithProgress(row, savePath);
                    }
                    finally
                    {
                        semaphore.Release(); // 下载任务完成后释放信号量，以便允许其他下载任务开始
                    }
                }));
            }

            // 等待所有下载任务完成
            await Task.WhenAll(downloadTasks);
            if (MessageBox.Show("是否打开文件夹", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                System.Diagnostics.Process.Start(savePath);
                this.Close();
            }

        }

        // 下载进度更新方法
        public async Task DownloadWithProgress(DataGridViewRow row, string savePath)
        {
            if (row.Cells["Type"].Value.ToString() == "4")
            {
                await DownloadVideoWithProgress(row, savePath);
            }
            else if (row.Cells["Type"].Value.ToString() == "2")
            {
                await DownloadImagesWithProgress(row, savePath);
            }
            else
            {
                await DownloadVideoWithProgress(row, savePath);
            }
        }

        // 下载视频并更新进度
        private async Task DownloadVideoWithProgress(DataGridViewRow row, string savePath)
        {
            var videoUrl = row.Cells["DownloadUrl"].Value.ToString();
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (sender, e) =>
                {
                    double percentage = (double)e.BytesReceived / e.TotalBytesToReceive * 100;
                    row.Cells["Status"].Value = $"Downloading video... {percentage:F2}%";
                };
                var filePath = Path.Combine(savePath, row.Cells["Description"].Value.ToString().Replace("\r", "").Replace("\n", "") + ".mp4");
                if (!File.Exists(filePath)&&!string.IsNullOrEmpty(videoUrl))
                {
                    var data = await client.DownloadDataTaskAsync(videoUrl);
                    File.WriteAllBytes(filePath, data);
                }
                row.Cells["Status"].Value = "Video Download Complete!";
            }
        }
        //row.Cells["Description"].Value.ToString().Replace("\r","").Replace("\n","")

        // 下载图片并更新进度
        private async Task DownloadImagesWithProgress(DataGridViewRow row, string savePath)
        {
            var imageUrls = row.Cells["DownloadUrl"].Value.ToString().Split(',').ToList();
            int imageCount = imageUrls.Count;
            int downloadedCount = 0;

            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (sender, e) =>
                {
                    double percentage = (double)downloadedCount / imageCount * 100;
                    row.Cells["Status"].Value = $"Downloading images... {percentage:F2}%";
                };

                foreach (var imageUrl in imageUrls)
                {

                    var filePath = Path.Combine(savePath, Path.Combine(savePath, row.Cells["Description"].Value.ToString().Replace("\\", "").Replace("\r", "").Replace("\n", "") + downloadedCount + ".jpg"));
                    if (!File.Exists(filePath) && !string.IsNullOrEmpty(imageUrl))
                    {
                        var data = await client.DownloadDataTaskAsync(imageUrl);
                        File.WriteAllBytes(filePath, data);
                        row.Cells["Status"].Value = "Image Download Complete!";
                    }
                    downloadedCount++;
                }

                row.Cells["Status"].Value = "Image Download Complete!";
            }
        }
    }
}
