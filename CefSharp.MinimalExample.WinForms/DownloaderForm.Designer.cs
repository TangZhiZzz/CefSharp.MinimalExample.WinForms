namespace CefSharp.MinimalExample.WinForms
{
    partial class DownloaderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NickName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownloadUrl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下载DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NickName,
            this.Type,
            this.Description,
            this.DownloadUrl,
            this.Status});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 450);
            this.dataGridView1.TabIndex = 0;
            // 
            // NickName
            // 
            this.NickName.DataPropertyName = "NickName";
            this.NickName.HeaderText = "用户昵称";
            this.NickName.Name = "NickName";
            this.NickName.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "类型";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Visible = false;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "作品描述";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // DownloadUrl
            // 
            this.DownloadUrl.DataPropertyName = "DownloadUrl";
            this.DownloadUrl.HeaderText = "作品链接";
            this.DownloadUrl.Name = "DownloadUrl";
            this.DownloadUrl.ReadOnly = true;
            this.DownloadUrl.Visible = false;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选AToolStripMenuItem,
            this.下载DToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // 全选AToolStripMenuItem
            // 
            this.全选AToolStripMenuItem.Name = "全选AToolStripMenuItem";
            this.全选AToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.全选AToolStripMenuItem.Text = "全选(ALT+&A)";
            this.全选AToolStripMenuItem.Click += new System.EventHandler(this.全选AToolStripMenuItem_Click);
            // 
            // 下载DToolStripMenuItem
            // 
            this.下载DToolStripMenuItem.Name = "下载DToolStripMenuItem";
            this.下载DToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.下载DToolStripMenuItem.Text = "下载(ALT+&D)";
            this.下载DToolStripMenuItem.Click += new System.EventHandler(this.下载DToolStripMenuItem_Click);
            // 
            // DownloaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "DownloaderForm";
            this.Text = "DownloaderForm";
            this.Load += new System.EventHandler(this.DownloaderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NickName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownloadUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下载DToolStripMenuItem;
    }
}