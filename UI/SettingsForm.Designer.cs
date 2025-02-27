﻿namespace RandomPictureSelector.UI
{
    partial class SettingsForm
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
            panel1 = new Panel();
            panel2 = new Panel();
            btnCancel = new Button();
            btnLoad = new Button();
            btnSave = new Button();
            label5 = new Label();
            label4 = new Label();
            label2 = new Label();
            label1 = new Label();
            numMinShuffleCount = new NumericUpDown();
            numShuffleSpeed = new NumericUpDown();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinShuffleCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShuffleSpeed).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(statusStrip1);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 683);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.None;
            panel2.BackColor = Color.Gray;
            panel2.Controls.Add(btnCancel);
            panel2.Controls.Add(btnLoad);
            panel2.Controls.Add(btnSave);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(numMinShuffleCount);
            panel2.Controls.Add(numShuffleSpeed);
            panel2.Location = new Point(106, 69);
            panel2.Name = "panel2";
            panel2.Size = new Size(591, 550);
            panel2.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.None;
            btnCancel.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(193, 473);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(185, 53);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.None;
            btnLoad.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoad.Location = new Point(231, 257);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(98, 33);
            btnLoad.TabIndex = 9;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.None;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(231, 218);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(98, 33);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(171, 183);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 6;
            label5.Text = "Shouffle Speed :";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(179, 134);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 5;
            label4.Text = "Shuffle Count :";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(215, 85);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 3;
            label2.Text = "Shuffle Settings";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(206, 0);
            label1.Name = "label1";
            label1.Size = new Size(155, 47);
            label1.TabIndex = 2;
            label1.Text = "Settings";
            // 
            // numMinShuffleCount
            // 
            numMinShuffleCount.Anchor = AnchorStyles.None;
            numMinShuffleCount.BackColor = SystemColors.Info;
            numMinShuffleCount.Location = new Point(286, 128);
            numMinShuffleCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMinShuffleCount.Name = "numMinShuffleCount";
            numMinShuffleCount.Size = new Size(120, 23);
            numMinShuffleCount.TabIndex = 0;
            numMinShuffleCount.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // numShuffleSpeed
            // 
            numShuffleSpeed.Anchor = AnchorStyles.None;
            numShuffleSpeed.BackColor = SystemColors.Info;
            numShuffleSpeed.Location = new Point(286, 177);
            numShuffleSpeed.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numShuffleSpeed.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numShuffleSpeed.Name = "numShuffleSpeed";
            numShuffleSpeed.Size = new Size(120, 23);
            numShuffleSpeed.TabIndex = 1;
            numShuffleSpeed.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 661);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(74, 17);
            toolStripStatusLabel1.Text = "Bax Creation";
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(800, 683);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinShuffleCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShuffleSpeed).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private NumericUpDown numShuffleSpeed;
        private NumericUpDown numMinShuffleCount;
        private Panel panel2;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label4;
        private Button btnLoad;
        private Button btnSave;
        private Button btnCancel;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}