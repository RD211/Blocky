namespace RealLifeScratch
{
    partial class ResultMazeDialog
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
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.btn_exit = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_home = new System.Windows.Forms.Button();
            this.btn_retry = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.GIFBox = new System.Windows.Forms.PictureBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_playLevel = new System.Windows.Forms.Button();
            this.btn_creator = new System.Windows.Forms.Button();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GIFBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ControlPanel
            // 
            this.ControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ControlPanel.Controls.Add(this.btn_exit);
            this.ControlPanel.Controls.Add(this.lbl_title);
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(400, 25);
            this.ControlPanel.TabIndex = 6;
            // 
            // btn_exit
            // 
            this.btn_exit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_exit.BackColor = System.Drawing.Color.Transparent;
            this.btn_exit.BackgroundImage = global::RealLifeScratch.Properties.Resources.CloseButton;
            this.btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_exit.ForeColor = System.Drawing.Color.Transparent;
            this.btn_exit.Location = new System.Drawing.Point(375, 3);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Padding = new System.Windows.Forms.Padding(1);
            this.btn_exit.Size = new System.Drawing.Size(20, 20);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // lbl_title
            // 
            this.lbl_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.lbl_title.Location = new System.Drawing.Point(13, 5);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(375, 15);
            this.lbl_title.TabIndex = 1;
            this.lbl_title.Text = "Default";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_home
            // 
            this.btn_home.BackColor = System.Drawing.Color.Transparent;
            this.btn_home.BackgroundImage = global::RealLifeScratch.Properties.Resources.DashboardButton;
            this.btn_home.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_home.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_home.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.btn_home.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_home.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_home.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_home.Location = new System.Drawing.Point(69, 284);
            this.btn_home.Name = "btn_home";
            this.btn_home.Size = new System.Drawing.Size(263, 50);
            this.btn_home.TabIndex = 10;
            this.btn_home.UseVisualStyleBackColor = false;
            this.btn_home.Click += new System.EventHandler(this.btn_home_Click);
            // 
            // btn_retry
            // 
            this.btn_retry.BackColor = System.Drawing.Color.Transparent;
            this.btn_retry.BackgroundImage = global::RealLifeScratch.Properties.Resources.RetryButton;
            this.btn_retry.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_retry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_retry.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.btn_retry.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_retry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_retry.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_retry.Location = new System.Drawing.Point(13, 284);
            this.btn_retry.Name = "btn_retry";
            this.btn_retry.Size = new System.Drawing.Size(50, 50);
            this.btn_retry.TabIndex = 9;
            this.btn_retry.UseVisualStyleBackColor = false;
            this.btn_retry.Click += new System.EventHandler(this.btn_retry_Click);
            // 
            // btn_next
            // 
            this.btn_next.BackColor = System.Drawing.Color.Transparent;
            this.btn_next.BackgroundImage = global::RealLifeScratch.Properties.Resources.NextButton1;
            this.btn_next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_next.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_next.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.btn_next.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_next.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_next.Location = new System.Drawing.Point(338, 284);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(50, 50);
            this.btn_next.TabIndex = 8;
            this.btn_next.UseVisualStyleBackColor = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // GIFBox
            // 
            this.GIFBox.Location = new System.Drawing.Point(13, 32);
            this.GIFBox.Name = "GIFBox";
            this.GIFBox.Size = new System.Drawing.Size(382, 246);
            this.GIFBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GIFBox.TabIndex = 7;
            this.GIFBox.TabStop = false;
            // 
            // btn_connect
            // 
            this.btn_connect.BackColor = System.Drawing.Color.Transparent;
            this.btn_connect.BackgroundImage = global::RealLifeScratch.Properties.Resources.ConnectButton;
            this.btn_connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_connect.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_connect.Enabled = false;
            this.btn_connect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.btn_connect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_connect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_connect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_connect.Location = new System.Drawing.Point(13, 284);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(375, 50);
            this.btn_connect.TabIndex = 11;
            this.btn_connect.UseVisualStyleBackColor = false;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_playLevel
            // 
            this.btn_playLevel.BackColor = System.Drawing.Color.Transparent;
            this.btn_playLevel.BackgroundImage = global::RealLifeScratch.Properties.Resources.PlayButton;
            this.btn_playLevel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_playLevel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_playLevel.Enabled = false;
            this.btn_playLevel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.btn_playLevel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_playLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_playLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_playLevel.Location = new System.Drawing.Point(13, 284);
            this.btn_playLevel.Name = "btn_playLevel";
            this.btn_playLevel.Size = new System.Drawing.Size(180, 50);
            this.btn_playLevel.TabIndex = 12;
            this.btn_playLevel.UseVisualStyleBackColor = false;
            this.btn_playLevel.Click += new System.EventHandler(this.btn_playLevel_Click);
            // 
            // btn_creator
            // 
            this.btn_creator.BackColor = System.Drawing.Color.Transparent;
            this.btn_creator.BackgroundImage = global::RealLifeScratch.Properties.Resources.CreatorButton;
            this.btn_creator.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_creator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_creator.Enabled = false;
            this.btn_creator.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(170)))), ((int)(((byte)(181)))));
            this.btn_creator.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_creator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_creator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_creator.Location = new System.Drawing.Point(201, 284);
            this.btn_creator.Name = "btn_creator";
            this.btn_creator.Size = new System.Drawing.Size(187, 50);
            this.btn_creator.TabIndex = 13;
            this.btn_creator.UseVisualStyleBackColor = false;
            this.btn_creator.Click += new System.EventHandler(this.btn_creator_Click);
            // 
            // ResultMazeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(400, 343);
            this.Controls.Add(this.btn_creator);
            this.Controls.Add(this.btn_playLevel);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.btn_home);
            this.Controls.Add(this.btn_retry);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.GIFBox);
            this.Controls.Add(this.ControlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResultMazeDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomMessageBox";
            this.Load += new System.EventHandler(this.FinishedMazeDialog_Load);
            this.ControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GIFBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.PictureBox GIFBox;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_retry;
        private System.Windows.Forms.Button btn_home;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_playLevel;
        private System.Windows.Forms.Button btn_creator;
    }
}