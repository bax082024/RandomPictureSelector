namespace RandomPictureSelector.UI
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
            numMinShuffleCount = new NumericUpDown();
            numShuffleSpeed = new NumericUpDown();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMinShuffleCount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numShuffleSpeed).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(numShuffleSpeed);
            panel1.Controls.Add(numMinShuffleCount);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 683);
            panel1.TabIndex = 0;
            // 
            // numMinShuffleCount
            // 
            numMinShuffleCount.Location = new Point(308, 133);
            numMinShuffleCount.Name = "numMinShuffleCount";
            numMinShuffleCount.Size = new Size(120, 23);
            numMinShuffleCount.TabIndex = 0;
            // 
            // numShuffleSpeed
            // 
            numShuffleSpeed.Location = new Point(308, 182);
            numShuffleSpeed.Name = "numShuffleSpeed";
            numShuffleSpeed.Size = new Size(120, 23);
            numShuffleSpeed.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)numMinShuffleCount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numShuffleSpeed).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private NumericUpDown numShuffleSpeed;
        private NumericUpDown numMinShuffleCount;
    }
}