using RandomPictureSelector.UI;
using System.Diagnostics;

namespace RandomPictureSelector
{
    public partial class RandomForm : Form
    {
        private List<string> imagePaths = new List<string>();
        private List<string> usedImagePaths = new List<string>();
        private Random random = new Random();

        private int shuffleIndex = 0;
        private int shuffleCount = 0;
        private int customShuffleSpeed = 100;
        private int maxShuffleCount;

        private const string SettingsFilePath = "UserSettings.json";

        private GradientType currentGradient = GradientType.LightBlueToDarkBlue;
        private string currentTheme = "Light";




        public RandomForm()
        {
            InitializeComponent();
            LoadSettings();

            shuffleTimer.Tick += shuffleTimer_Tick;

            if (string.IsNullOrEmpty(currentTheme))
            {
                currentTheme = "DesignView";
                ApplyTheme(currentTheme);
            }

            SaveSettings();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (string filePath in openFileDialog.FileNames)
                    {
                        if (!imagePaths.Contains(filePath))
                        {
                            imagePaths.Add(filePath);
                            listBox1.Items.Add(System.IO.Path.GetFileName(filePath));
                        }
                    }
                }
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                MessageBox.Show("Please add images first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            shuffleIndex = 0;
            shuffleCount = 0;

            maxShuffleCount = Math.Max(20, Math.Min(imagePaths.Count, shuffleProgressBar.Maximum));
            shuffleProgressBar.Maximum = maxShuffleCount;

            ResetProgressBar(maxShuffleCount);

            lblShuffleStatus.Text = $"Shuffling... 0/{maxShuffleCount}";

            if (imagePaths.Count > 0)
            {
                pictureBox1.Image = FixImageOrientation(LoadImageSafely(imagePaths[shuffleIndex]));
                shuffleProgressBar.Value = 1; 
            }

            shuffleTimer.Interval = customShuffleSpeed;
            shuffleTimer.Start();
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            if (usedImagePaths.Count == 0)
            {
                MessageBox.Show("No images to move back!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string filePath in usedImagePaths)
            {
                imagePaths.Add(filePath);
                listBox1.Items.Add(System.IO.Path.GetFileName(filePath));
            }

            usedImagePaths.Clear();
            listBox2.Items.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private Image FixImageOrientation(Image img)
        {
            const int ExifOrientationId = 0x112;

            try
            {
                if (img.PropertyIdList.Contains(ExifOrientationId))
                {
                    var prop = img.GetPropertyItem(ExifOrientationId);

                    if (prop?.Value != null)
                    {
                        int orientation = BitConverter.ToUInt16(prop.Value, 0);

                        switch (orientation)
                        {
                            case 3:
                                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 6:
                                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 8:
                                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
         
                        img.RemovePropertyItem(ExifOrientationId);
                    }
                }
            }
            catch
            {

            }

            return img;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                if (file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png") || file.EndsWith(".bmp"))
                {
                    if (!imagePaths.Contains(file))
                    {
                        imagePaths.Add(file);
                        listBox1.Items.Add(System.IO.Path.GetFileName(file));
                    }
                }
                else
                {
                    MessageBox.Show($"The file '{System.IO.Path.GetFileName(file)}' is not a supported image type.",
                                    "Invalid File",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                saveFileDialog.Title = "Save Image Lists";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {
                        writer.WriteLine("[Insert Images]");
                        foreach (string path in imagePaths)
                        {
                            writer.WriteLine(path);
                        }

                        writer.WriteLine("[Used Images]");
                        foreach (string path in usedImagePaths)
                        {
                            writer.WriteLine(path);
                        }
                    }

                    MessageBox.Show("Image lists saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files|*.txt";
                openFileDialog.Title = "Load Image Lists";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePaths.Clear();
                    usedImagePaths.Clear();
                    listBox1.Items.Clear();
                    listBox2.Items.Clear();

                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        string line;
                        bool loadingInsertImages = false;
                        bool loadingUsedImages = false;

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line == "[Insert Images]")
                            {
                                loadingInsertImages = true;
                                loadingUsedImages = false;
                                continue;
                            }

                            if (line == "[Used Images]")
                            {
                                loadingInsertImages = false;
                                loadingUsedImages = true;
                                continue;
                            }

                            if (loadingInsertImages)
                            {
                                if (System.IO.File.Exists(line))
                                {
                                    imagePaths.Add(line);
                                    listBox1.Items.Add(System.IO.Path.GetFileName(line));
                                }
                            }
                            else if (loadingUsedImages)
                            {
                                if (System.IO.File.Exists(line))
                                {
                                    usedImagePaths.Add(line);
                                    listBox2.Items.Add(System.IO.Path.GetFileName(line));
                                }
                            }
                        }
                    }

                    MessageBox.Show("Image lists loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void shuffleTimer_Tick(object sender, EventArgs e)
        {
            if (imagePaths.Count == 0)
            {
                shuffleTimer.Stop();
                shuffleProgressBar.Visible = false;
                lblShuffleStatus.Text = "No images to shuffle!";
                return;
            }

            pictureBox1.Image = FixImageOrientation(LoadImageSafely(imagePaths[shuffleIndex]));

            shuffleIndex = (shuffleIndex + 1) % imagePaths.Count;

            shuffleCount++;
            shuffleProgressBar.Value = Math.Min(shuffleCount, maxShuffleCount);
            lblShuffleStatus.Text = $"Shuffling... {shuffleCount}/{maxShuffleCount}";

            if (shuffleCount >= maxShuffleCount)
            {
                shuffleTimer.Stop();
                shuffleProgressBar.Value = shuffleProgressBar.Maximum;
                shuffleProgressBar.Visible = false;
                lblShuffleStatus.Text = "Shuffle complete!";
                SelectFinalRandomImage();
            }
        }

        private void SelectFinalRandomImage()
        {
            if (imagePaths.Count == 0)
            {
                lblShuffleStatus.Text = "No images to display!";
                return;
            }

            int randomIndex = random.Next(imagePaths.Count);
            string selectedImage = imagePaths[randomIndex];

            pictureBox1.Image = FixImageOrientation(Image.FromFile(selectedImage));

            usedImagePaths.Add(selectedImage);
            listBox2.Items.Add(System.IO.Path.GetFileName(selectedImage));

            imagePaths.RemoveAt(randomIndex);
            listBox1.Items.RemoveAt(randomIndex);

            lblShuffleStatus.Text = "Final image selected!";
        }

        private Image LoadImageSafely(string filePath)
        {
            try
            {
                return Image.FromFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void shuffleSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SettingsForm settingsForm = new SettingsForm())
            {
                settingsForm.MinShuffleCount = shuffleProgressBar.Minimum;
                settingsForm.ShuffleSpeed = customShuffleSpeed;

                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    if (settingsForm.MinShuffleCount >= 1 && settingsForm.ShuffleSpeed > 0)
                    {
                        shuffleProgressBar.Minimum = settingsForm.MinShuffleCount;
                        customShuffleSpeed = settingsForm.ShuffleSpeed;
                    }
                    else
                    {
                        MessageBox.Show("Invalid settings. Please ensure values are correct.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ResetProgressBar(int calculatedShuffleCount)
        {
            shuffleProgressBar.Minimum = 0;
            shuffleProgressBar.Maximum = calculatedShuffleCount;
            shuffleProgressBar.Value = 0;
            shuffleProgressBar.Visible = true;
        }

        private void SaveSettings()
        {
            try
            {
                var settings = new UserSettings
                {
                    MinShuffleCount = shuffleProgressBar.Minimum,
                    ShuffleSpeed = customShuffleSpeed,
                    SelectedTheme = currentTheme,
                    SelectedGradient = currentGradient.ToString()
                };

                string json = System.Text.Json.JsonSerializer.Serialize(settings, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true
                });

                File.WriteAllText(SettingsFilePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to save settings: {ex.Message}");
            }
        }

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsFilePath))
                {
                    string json = File.ReadAllText(SettingsFilePath);
                    var settings = System.Text.Json.JsonSerializer.Deserialize<UserSettings>(json);

                    if (settings != null)
                    {
                        shuffleProgressBar.Minimum = settings.MinShuffleCount;
                        customShuffleSpeed = settings.ShuffleSpeed;
                        currentTheme = settings.SelectedTheme;
                        currentGradient = Enum.TryParse(settings.SelectedGradient, out GradientType gradient)
                            ? gradient
                            : GradientType.LightBlueToDarkBlue;

                        ApplyTheme(currentTheme);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to load settings: {ex.Message}");
            }
        }

        private void ApplyTheme(string theme)
        {
            this.Paint -= RandomForm_Paint;

            Color backgroundColor;
            Color textColor;
            Color buttonBackgroundColor;
            Color buttonTextColor;
            Color listBoxBackgroundColor;
            Color listBoxTextColor;
            Color menuStripBackgroundColor;
            Color menuStripTextColor;

            switch (theme)
            {
                case "Light":
                    backgroundColor = Color.White;
                    textColor = Color.Black;
                    buttonBackgroundColor = Color.LightGray;
                    buttonTextColor = Color.Black;
                    listBoxBackgroundColor = Color.White;
                    listBoxTextColor = Color.Black;
                    menuStripBackgroundColor = Color.White;
                    menuStripTextColor = Color.Black;
                    break;

                case "Dark":
                    backgroundColor = Color.Black;
                    textColor = Color.White;
                    buttonBackgroundColor = Color.Gray;
                    buttonTextColor = Color.White;
                    listBoxBackgroundColor = Color.Black;
                    listBoxTextColor = Color.White;
                    menuStripBackgroundColor = Color.Black;
                    menuStripTextColor = Color.White;
                    break;

                case "Gradient":
                    this.Paint += RandomForm_Paint;

                    backgroundColor = Color.Transparent;
                    textColor = Color.White;
                    buttonBackgroundColor = Color.DarkGray;
                    buttonTextColor = Color.White;
                    listBoxBackgroundColor = Color.Gray;
                    listBoxTextColor = Color.White;
                    menuStripBackgroundColor = Color.DarkGray;
                    menuStripTextColor = Color.White;
                    break;

                case "DesignView":
                default:
                    backgroundColor = Color.LightYellow;
                    textColor = Color.Black;
                    buttonBackgroundColor = Color.LightGoldenrodYellow;
                    buttonTextColor = Color.Black;
                    listBoxBackgroundColor = Color.LightYellow;
                    listBoxTextColor = Color.Black;
                    menuStripBackgroundColor = Color.Beige;
                    menuStripTextColor = Color.Black;
                    break;
            }

            this.BackColor = theme == "Gradient" ? Color.White : backgroundColor;
            this.ForeColor = textColor;

            foreach (Control control in this.Controls)
            {
                switch (control)
                {
                    case Button button:
                        button.BackColor = buttonBackgroundColor;
                        button.ForeColor = buttonTextColor;
                        break;

                    case ListBox listBox:
                        listBox.BackColor = listBoxBackgroundColor;
                        listBox.ForeColor = listBoxTextColor;
                        break;

                    case MenuStrip menuStrip:
                        menuStrip.BackColor = menuStripBackgroundColor;
                        menuStrip.ForeColor = menuStripTextColor;
                        break;

                    case Label label:
                        label.ForeColor = textColor;
                        break;

                    case ProgressBar progressBar:
                        progressBar.BackColor = backgroundColor;
                        progressBar.ForeColor = textColor; 
                        break;

                    default:
                        control.BackColor = backgroundColor;
                        control.ForeColor = textColor;
                        break;
                }
            }

            SaveSettings();

 
            this.Invalidate();
        }


        private void RandomForm_Paint(object sender, PaintEventArgs e)
        {
            System.Drawing.Drawing2D.LinearGradientBrush gradientBrush;

            switch (currentGradient)
            {
                case GradientType.OrangeToYellow:
                    gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        this.ClientRectangle,
                        Color.Orange,
                        Color.Yellow,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    break;

                case GradientType.BlueToPurple:
                    gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        this.ClientRectangle,
                        Color.Blue,
                        Color.Purple,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    break;

                case GradientType.LightBlueToDarkBlue:
                default:
                    gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        this.ClientRectangle,
                        Color.LightBlue,
                        Color.DarkBlue,
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                    break;
            }

            e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            gradientBrush.Dispose();
        }


        public enum GradientType
        {
            LightBlueToDarkBlue,
            OrangeToYellow,
            BlueToPurple
        }

        private void ChangeTheme(string theme)
        {
            currentTheme = theme; 
            ApplyTheme(theme);
            SaveSettings();
        }

        private void saveSettiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void loadSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void RandomForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Light");
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Dark");
        }

        private void gradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("Gradient");
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme("DesignView");
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGradient = GradientType.LightBlueToDarkBlue;
            ApplyTheme("Gradient");
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGradient = GradientType.OrangeToYellow;
            ApplyTheme("Gradient");
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentGradient = GradientType.BlueToPurple;
            ApplyTheme("Gradient");
        }

    }
}
