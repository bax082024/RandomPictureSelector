namespace RandomPictureSelector
{
    partial class RandomForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainPanel = new Panel();
            btnExit = new Button();
            label2 = new Label();
            btnAdd = new Button();
            btnMove = new Button();
            btnRandom = new Button();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            listBox2 = new ListBox();
            listBox1 = new ListBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // MainPanel
            // 
            MainPanel.BackColor = Color.Transparent;
            MainPanel.Controls.Add(btnExit);
            MainPanel.Controls.Add(label2);
            MainPanel.Controls.Add(btnAdd);
            MainPanel.Controls.Add(btnMove);
            MainPanel.Controls.Add(btnRandom);
            MainPanel.Controls.Add(label1);
            MainPanel.Controls.Add(pictureBox1);
            MainPanel.Controls.Add(listBox2);
            MainPanel.Controls.Add(listBox1);
            MainPanel.Controls.Add(statusStrip1);
            MainPanel.Controls.Add(menuStrip1);
            MainPanel.Dock = DockStyle.Fill;
            MainPanel.Location = new Point(0, 0);
            MainPanel.Name = "MainPanel";
            MainPanel.Size = new Size(1009, 669);
            MainPanel.TabIndex = 0;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.None;
            btnExit.BackColor = Color.LightGray;
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Location = new Point(868, 592);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(75, 23);
            btnExit.TabIndex = 10;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(808, 178);
            label2.Name = "label2";
            label2.Size = new Size(86, 17);
            label2.TabIndex = 8;
            label2.Text = "Used Images";
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.BackColor = Color.LightGray;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(61, 236);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(178, 34);
            btnAdd.TabIndex = 7;
            btnAdd.Text = "Add Image";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnMove
            // 
            btnMove.Anchor = AnchorStyles.None;
            btnMove.BackColor = Color.LightGray;
            btnMove.FlatStyle = FlatStyle.Popup;
            btnMove.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMove.Location = new Point(765, 234);
            btnMove.Name = "btnMove";
            btnMove.Size = new Size(178, 34);
            btnMove.TabIndex = 6;
            btnMove.Text = "<<<      Move ";
            btnMove.TextAlign = ContentAlignment.MiddleLeft;
            btnMove.UseVisualStyleBackColor = false;
            btnMove.Click += btnMove_Click;
            // 
            // btnRandom
            // 
            btnRandom.Anchor = AnchorStyles.None;
            btnRandom.BackColor = Color.LightGray;
            btnRandom.FlatStyle = FlatStyle.Popup;
            btnRandom.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRandom.Location = new Point(416, 565);
            btnRandom.Name = "btnRandom";
            btnRandom.Size = new Size(163, 50);
            btnRandom.TabIndex = 5;
            btnRandom.Text = "START";
            btnRandom.UseVisualStyleBackColor = false;
            btnRandom.Click += btnRandom_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(271, 24);
            label1.Name = "label1";
            label1.Size = new Size(453, 50);
            label1.TabIndex = 4;
            label1.Text = "Random Picture Chooser";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BackColor = Color.Black;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Location = new Point(301, 142);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(400, 400);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // listBox2
            // 
            listBox2.Anchor = AnchorStyles.None;
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 15;
            listBox2.Location = new Point(765, 283);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(178, 259);
            listBox2.TabIndex = 2;
            // 
            // listBox1
            // 
            listBox1.AllowDrop = true;
            listBox1.Anchor = AnchorStyles.None;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(61, 283);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(178, 259);
            listBox1.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 647);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1009, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(74, 17);
            toolStripStatusLabel1.Text = "Bax Creation";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.LightGray;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1009, 24);
            menuStrip1.TabIndex = 9;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(93, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // RandomForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightYellow;
            ClientSize = new Size(1009, 669);
            Controls.Add(MainPanel);
            Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MainMenuStrip = menuStrip1;
            Name = "RandomForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Random Picture Selector";
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel MainPanel;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ListBox listBox2;
        private ListBox listBox1;
        private PictureBox pictureBox1;
        private Label label1;
        private Button btnRandom;
        private Button btnMove;
        private Button btnAdd;
        private Label label2;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Button btnExit;
    }
}
