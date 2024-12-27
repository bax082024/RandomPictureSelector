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
            label1 = new Label();
            numMinShuffleCount = new NumericUpDown();
            numShuffleSpeed = new NumericUpDown();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btnSave = new Button();
            btnLoad = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinShuffleCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShuffleSpeed).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
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
            panel2.Controls.Add(btnLoad);
            panel2.Controls.Add(btnSave);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(numMinShuffleCount);
            panel2.Controls.Add(numShuffleSpeed);
            panel2.Location = new Point(106, 69);
            panel2.Name = "panel2";
            panel2.Size = new Size(591, 550);
            panel2.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(200, 9);
            label1.Name = "label1";
            label1.Size = new Size(155, 47);
            label1.TabIndex = 2;
            label1.Text = "Settings";
            // 
            // numMinShuffleCount
            // 
            numMinShuffleCount.Anchor = AnchorStyles.None;
            numMinShuffleCount.BackColor = SystemColors.Info;
            numMinShuffleCount.Location = new Point(258, 148);
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
            numShuffleSpeed.Location = new Point(258, 197);
            numShuffleSpeed.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numShuffleSpeed.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            numShuffleSpeed.Name = "numShuffleSpeed";
            numShuffleSpeed.Size = new Size(120, 23);
            numShuffleSpeed.TabIndex = 1;
            numShuffleSpeed.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(210, 101);
            label2.Name = "label2";
            label2.Size = new Size(131, 21);
            label2.TabIndex = 3;
            label2.Text = "Shuffle Settings";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(178, 254);
            label3.Name = "label3";
            label3.Size = new Size(200, 21);
            label3.TabIndex = 4;
            label3.Text = "Save And Load Functions";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label4.Location = new Point(151, 154);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 5;
            label4.Text = "Shuffle Count :";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label5.Location = new Point(143, 203);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 6;
            label5.Text = "Shouffle Speed :";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(85, 458);
            label6.Name = "label6";
            label6.Size = new Size(58, 17);
            label6.TabIndex = 7;
            label6.Text = "Settings";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.None;
            btnSave.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(210, 290);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(125, 33);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.None;
            btnLoad.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLoad.Location = new Point(210, 338);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(125, 33);
            btnLoad.TabIndex = 9;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(800, 683);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMinShuffleCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShuffleSpeed).EndInit();
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
        private Label label3;
        private Label label6;
        private Button btnLoad;
        private Button btnSave;
    }
}