namespace RealLifeScratch
{
    partial class CustomGameCreator
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
            this.btn_whiteblock = new System.Windows.Forms.Button();
            this.btn_blackblock = new System.Windows.Forms.Button();
            this.btn_purpleblock = new System.Windows.Forms.Button();
            this.btn_goldenblock = new System.Windows.Forms.Button();
            this.ControlPanel = new System.Windows.Forms.Panel();
            this.btn_minimize = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.mazeBox = new System.Windows.Forms.PictureBox();
            this.ControlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mazeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_whiteblock
            // 
            this.btn_whiteblock.BackColor = System.Drawing.Color.White;
            this.btn_whiteblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_whiteblock.ForeColor = System.Drawing.Color.Black;
            this.btn_whiteblock.Location = new System.Drawing.Point(691, 68);
            this.btn_whiteblock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_whiteblock.Name = "btn_whiteblock";
            this.btn_whiteblock.Size = new System.Drawing.Size(133, 123);
            this.btn_whiteblock.TabIndex = 2;
            this.btn_whiteblock.Tag = "0";
            this.btn_whiteblock.UseVisualStyleBackColor = false;
            this.btn_whiteblock.Click += new System.EventHandler(this.buttonColors_MouseClick);
            // 
            // btn_blackblock
            // 
            this.btn_blackblock.BackColor = System.Drawing.Color.Black;
            this.btn_blackblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_blackblock.Location = new System.Drawing.Point(692, 198);
            this.btn_blackblock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_blackblock.Name = "btn_blackblock";
            this.btn_blackblock.Size = new System.Drawing.Size(133, 123);
            this.btn_blackblock.TabIndex = 3;
            this.btn_blackblock.Tag = "1";
            this.btn_blackblock.UseVisualStyleBackColor = false;
            this.btn_blackblock.Click += new System.EventHandler(this.buttonColors_MouseClick);
            // 
            // btn_purpleblock
            // 
            this.btn_purpleblock.BackColor = System.Drawing.Color.Purple;
            this.btn_purpleblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_purpleblock.Location = new System.Drawing.Point(692, 329);
            this.btn_purpleblock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_purpleblock.Name = "btn_purpleblock";
            this.btn_purpleblock.Size = new System.Drawing.Size(133, 123);
            this.btn_purpleblock.TabIndex = 4;
            this.btn_purpleblock.Tag = "2";
            this.btn_purpleblock.UseVisualStyleBackColor = false;
            this.btn_purpleblock.Click += new System.EventHandler(this.buttonColors_MouseClick);
            // 
            // btn_goldenblock
            // 
            this.btn_goldenblock.BackColor = System.Drawing.Color.Orange;
            this.btn_goldenblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_goldenblock.Location = new System.Drawing.Point(692, 459);
            this.btn_goldenblock.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_goldenblock.Name = "btn_goldenblock";
            this.btn_goldenblock.Size = new System.Drawing.Size(133, 123);
            this.btn_goldenblock.TabIndex = 5;
            this.btn_goldenblock.Tag = "3";
            this.btn_goldenblock.UseVisualStyleBackColor = false;
            this.btn_goldenblock.Click += new System.EventHandler(this.buttonColors_MouseClick);
            // 
            // ControlPanel
            // 
            this.ControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.ControlPanel.Controls.Add(this.btn_minimize);
            this.ControlPanel.Controls.Add(this.button2);
            this.ControlPanel.Controls.Add(this.btn_exit);
            this.ControlPanel.Controls.Add(this.lbl_title);
            this.ControlPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ControlPanel.Name = "ControlPanel";
            this.ControlPanel.Size = new System.Drawing.Size(849, 31);
            this.ControlPanel.TabIndex = 7;
            // 
            // btn_minimize
            // 
            this.btn_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_minimize.BackColor = System.Drawing.Color.Transparent;
            this.btn_minimize.BackgroundImage = global::RealLifeScratch.Properties.Resources.MinimzeButton;
            this.btn_minimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_minimize.FlatAppearance.BorderSize = 0;
            this.btn_minimize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_minimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_minimize.ForeColor = System.Drawing.Color.Transparent;
            this.btn_minimize.Location = new System.Drawing.Point(749, 4);
            this.btn_minimize.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(27, 25);
            this.btn_minimize.TabIndex = 3;
            this.btn_minimize.UseVisualStyleBackColor = false;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::RealLifeScratch.Properties.Resources.MaximizeButton;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Enabled = false;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(783, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(27, 25);
            this.button2.TabIndex = 4;
            this.button2.UseVisualStyleBackColor = false;
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
            this.btn_exit.Location = new System.Drawing.Point(816, 4);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Padding = new System.Windows.Forms.Padding(1);
            this.btn_exit.Size = new System.Drawing.Size(27, 25);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // lbl_title
            // 
            this.lbl_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(849, 31);
            this.lbl_title.TabIndex = 7;
            this.lbl_title.Text = "Creator";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_title.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbl_title_MouseMove);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.Transparent;
            this.btn_save.BackgroundImage = global::RealLifeScratch.Properties.Resources.SaveButton;
            this.btn_save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btn_save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(691, 591);
            this.btn_save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(133, 92);
            this.btn_save.TabIndex = 6;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // mazeBox
            // 
            this.mazeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mazeBox.Location = new System.Drawing.Point(16, 68);
            this.mazeBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mazeBox.Name = "mazeBox";
            this.mazeBox.Size = new System.Drawing.Size(666, 615);
            this.mazeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mazeBox.TabIndex = 1;
            this.mazeBox.TabStop = false;
            this.mazeBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mazeBox_MouseClick);
            // 
            // CustomGameCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(849, 705);
            this.Controls.Add(this.ControlPanel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_goldenblock);
            this.Controls.Add(this.btn_purpleblock);
            this.Controls.Add(this.btn_blackblock);
            this.Controls.Add(this.btn_whiteblock);
            this.Controls.Add(this.mazeBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "CustomGameCreator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CustomGameCreator";
            this.Load += new System.EventHandler(this.CustomGameCreator_Load);
            this.ControlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mazeBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox mazeBox;
        private System.Windows.Forms.Button btn_whiteblock;
        private System.Windows.Forms.Button btn_blackblock;
        private System.Windows.Forms.Button btn_purpleblock;
        private System.Windows.Forms.Button btn_goldenblock;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Panel ControlPanel;
        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.Button btn_minimize;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_exit;
    }
}